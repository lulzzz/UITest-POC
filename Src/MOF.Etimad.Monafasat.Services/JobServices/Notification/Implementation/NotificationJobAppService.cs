using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services.MainServices.Notifay.Models;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services.JobServices
{
    public class NotificationJobAppService : INotificationJobAppService
    {
        private readonly IMemoryCache _cache;
        private readonly IIDMProxy _idmProxy;
        private readonly IGenericCommandRepository _genericCommandRepository;
        private readonly INotificationQueries _iNotificationQuerie;
        private readonly IINotificationCommands _notifayCommands;
        private readonly ILogger<NotificationAppService> _logger;
        private readonly RootConfigurations _configuration;
        private readonly IMapper _mapper;
        private readonly Func<string, NotificationEntityType, string> TempletKey = (id, type) => $"{type.ToString()} => {id}";


        public NotificationJobAppService(IGenericCommandRepository genericrepository, IIDMProxy iDMProxy, IMemoryCache cache, INotificationQueries iNotificationQuerie, IINotificationCommands notifayCommands, ILogger<NotificationAppService> logger,
            IMapper mapper, IOptionsSnapshot<RootConfigurations> optionsSnapShot)
        {
            _idmProxy = iDMProxy;
            _cache = cache;
            _iNotificationQuerie = iNotificationQuerie;
            _notifayCommands = notifayCommands;
            _logger = logger;
            _mapper = mapper;
            _configuration = optionsSnapShot.Value;
            _genericCommandRepository = genericrepository;
        }

        public async Task AddNotificationSettingByUserId(int notificationCodeId, List<UserProfile> users, int roleid)
        {
            var userIds = users.Select(d => d.Id).ToList();
            var userNotificationSettings = await _iNotificationQuerie.GetNotificationSettingsByCodeId(notificationCodeId);
            var usersHaveSetting = userNotificationSettings.Select(d => d.UserProfileId).ToList();
            var usersHavntSetting = userIds.Where(d => !usersHaveSetting.Contains(d)).ToList();

            var Code = await _iNotificationQuerie.GetDefaultSettingByCodeId(notificationCodeId);
            foreach (var usr in users.Where(d => usersHavntSetting.Contains(d.Id)).ToList())
            {
                usr.AddNotificationSetting(new UserNotificationSetting(Code.OperationCode, usr.Id, Code.NotificationOperationCodeId, Code.DefaultSMS, Code.DefaultEmail, roleid));
            }
            _genericCommandRepository.UpdateRange<UserProfile>(users);

            await _genericCommandRepository.SaveAsync();
        }

        public async Task SendNotificationByUserId(int notificationCodeId, int userId, string userName, MainNotificationTemplateModel mainNotificationTemplateModel)
        {
            string output = JsonConvert.SerializeObject(mainNotificationTemplateModel);
            MainNotificationTemplateModel mainView = JsonConvert.DeserializeObject<MainNotificationTemplateModel>(output);
            var userNotificationSetting = await _iNotificationQuerie.GetNotificationSettingByUserId(notificationCodeId, userId);
            var entityKey = TempletKey(mainView.EntityValue, mainView.EntityType);

            if (userNotificationSetting == null)
                return;
            var userDataFromIDM = await _idmProxy.GetUserbyUserName(userName);
            var userNotificationsModel = new UserNotificationSettingModel
            {
                UserId = userNotificationSetting.UserProfileId.Value,
                Email = userDataFromIDM != null ? userDataFromIDM.Email : userNotificationSetting.User.Email,
                Mobile = userDataFromIDM != null ? userDataFromIDM.PhoneNumber : userNotificationSetting.User.Mobile,
            };
            bool IsNullEmailFirstArrgs = string.IsNullOrEmpty(Convert.ToString(mainView.Args.BodyEmailArgs[0]));
            bool IsNullSmsmFirstArrgs = string.IsNullOrEmpty(Convert.ToString(mainView.Args.SMSArgs[0]));

            if (IsNullEmailFirstArrgs)
                mainView.Args.BodyEmailArgs[0] = Convert.ToString(userNotificationSetting.User.FullName);
            if (IsNullSmsmFirstArrgs)
                mainView.Args.SMSArgs[0] = Convert.ToString(userNotificationSetting.User.FullName);
            NotificationDataModel template = await BuildNotificationTemplate(userNotificationSetting.IsArabic, userNotificationSetting.NotificationCodeId, mainView.Args);
            if (userNotificationSetting.Email)
            {
                var email = new NotificationEmail(userId, userNotificationsModel.Email, template.Email.Title, template.Email.Body, userNotificationSetting.Id, mainView.Link, entityKey);
                await _notifayCommands.AddNotifayWithOutSave(email);
            }
            if (userNotificationSetting.Sms)
            {
                var sms = new NotificationSMS(userNotificationSetting.UserProfileId.Value, userNotificationsModel.Mobile, template.SMS.Body, userNotificationSetting.Id, mainView.Link, entityKey);
                await _notifayCommands.AddNotifayWithOutSave(sms);
            }
            var panel = new NotificationPanel(userNotificationSetting.UserProfileId.Value, template.PanelMessage, template.PanelMessage, userNotificationSetting.Id, mainView.Link, mainView.BranchId, mainView.CommitteeId, entityKey);
            await _notifayCommands.AddNotifayWithOutSave(panel);
            await _notifayCommands.SaveChangesAsync();

        }
        public async Task<bool> SendNotificationForSuppliers(int notificationCodeId, List<string> crsList, MainNotificationTemplateModel mainNotificationTemplateModel, Dictionary<string, Dictionary<int, string>> dynamicParameterList = null)
        {
            string output = JsonConvert.SerializeObject(mainNotificationTemplateModel);
            MainNotificationTemplateModel mainView = JsonConvert.DeserializeObject<MainNotificationTemplateModel>(output);
            List<CRNotificationModel> crs = new List<CRNotificationModel>();
            if (crsList.Any())
            {
                var idmContacts = await _idmProxy.GetContactOfficersByCR(crsList);
                crs = idmContacts.Items.AsEnumerable().Select(x => new CRNotificationModel
                {
                    CR = x.supplierNumber,
                    Mobile = x.mobile,
                    Email = x.email
                }).ToList();
            }
            var userNotificationSettings = await _iNotificationQuerie.GetNotificationSettingByCrAndOperationCode(crs.Select(x => x.CR).ToList(), notificationCodeId);
            if (userNotificationSettings.Count == 0)
                return true;
            var entityKey = TempletKey(mainView.EntityValue, mainView.EntityType);
            bool IsNullEmailFirstArrgs = string.IsNullOrEmpty(Convert.ToString(mainView.Args.BodyEmailArgs[0]));
            bool IsNullSmsmFirstArrgs = string.IsNullOrEmpty(Convert.ToString(mainView.Args.SMSArgs[0]));
            NotificationEmail email;
            NotificationSMS sms;
            NotificationPanel panel;
            foreach (var setting in userNotificationSettings)
            {
                if (IsNullEmailFirstArrgs)
                    mainView.Args.BodyEmailArgs[0] = setting.Supplier.SelectedCrName;
                if (IsNullSmsmFirstArrgs)
                    mainView.Args.SMSArgs[0] = setting.Supplier.SelectedCrName;
                if (dynamicParameterList != null)
                    foreach (var newValue in dynamicParameterList[setting.Cr])
                        mainView.Args.BodyEmailArgs[newValue.Key] = newValue.Value;

                NotificationDataModel template = await BuildNotificationTemplate(userNotificationSettings.FirstOrDefault().IsArabic, userNotificationSettings.FirstOrDefault().NotificationCodeId, mainView.Args);
                foreach (var currentCR in crs)
                {
                    if (setting.Email)
                    {
                        email = new NotificationEmail(setting.Cr, currentCR.Email, template.Email.Title, template.Email.Body, setting.Id, mainView.Link, entityKey);
                        await _notifayCommands.AddNotifayWithOutSave(email);
                    }
                    if (setting.Sms)
                    {
                        sms = new NotificationSMS(setting.Cr, currentCR.Mobile, template.SMS.Body, setting.Id, mainView.Link, entityKey);
                        await _notifayCommands.AddNotifayWithOutSave(sms);
                    }
                }
                panel = new NotificationPanel(setting.Cr, template.PanelMessage, template.PanelMessage, setting.Id, mainView.Link, mainView.BranchId, mainView.CommitteeId, entityKey);
                await _notifayCommands.AddNotifayWithOutSave(panel);
            }
            if (userNotificationSettings.Count > 0)
                await _notifayCommands.SaveChangesAsync();
            return true;
        }
        public async Task SendNotifications(int notificationCodeId, string agencyCode, int agencyType, MainNotificationTemplateModel mainView, string UserRole, int committeeId = 0, int branchid = 0)
        {
            mainView.CommitteeId = committeeId;
            mainView.BranchId = branchid;
            var oldUserNotificationSettings = await _iNotificationQuerie.GetNotificationSettingByUserIdAndUserType(notificationCodeId, branchid, committeeId);
            if (oldUserNotificationSettings.Count == 0)
            {
                return;
            }

            UsersSearchCriteriaModel _usersSearchCriteriaModel = new UsersSearchCriteriaModel()
            {
                RoleName = UserRole,
                AgencyId = agencyCode,
                PageSize = 1000,
                AgencyType = agencyType
            };
            var idmUsers = await _idmProxy.GetMonafasatUsersByAgencyTypeAndRoleName(_usersSearchCriteriaModel);
            if (idmUsers == null || !idmUsers.Items.Any())
            {
                return;
            }
            var Users = _mapper.Map<QueryResult<EmployeeIntegrationModel>>(idmUsers);
            List<int> users = Users.Items.Select(x => x.userId).ToList();
            var userNotificationSettings = await _iNotificationQuerie.GetNotificationSettingByRoleAndOperationCode(users, notificationCodeId, branchid, committeeId);

            await SendNotificationForUsers(idmUsers, userNotificationSettings, mainView);
        }
        private async Task SendNotificationForUsers(QueryResult<EmployeeIntegrationModel> idmUsers, List<UserNotificationSetting> userNotificationSettings, MainNotificationTemplateModel mainNotificationTemplateModel)
        {
            try
            {
                string output = JsonConvert.SerializeObject(mainNotificationTemplateModel);
                MainNotificationTemplateModel mainView = JsonConvert.DeserializeObject<MainNotificationTemplateModel>(output);

                var userNotificationsModel = userNotificationSettings.Select(x => new UserNotificationSettingModel { UserId = x.UserProfileId.Value, Email = idmUsers.Items.Where(i => i.userId == x.User.Id).FirstOrDefault().email, Mobile = idmUsers.Items.Where(i => i.userId == x.User.Id).FirstOrDefault().mobileNumber }).ToList();

                var entityKey = TempletKey(mainView.EntityValue, mainView.EntityType);
                bool IsNullEmailFirstArrgs = string.IsNullOrEmpty(Convert.ToString(mainView.Args.BodyEmailArgs[0]));
                bool IsNullSmsmFirstArrgs = string.IsNullOrEmpty(Convert.ToString(mainView.Args.SMSArgs[0]));
                NotificationEmail email;
                NotificationSMS sms;
                NotificationPanel panel;
                foreach (var setting in userNotificationSettings)
                {
                    if (IsNullEmailFirstArrgs)
                        mainView.Args.BodyEmailArgs[0] = Convert.ToString(setting.User.FullName);
                    if (IsNullSmsmFirstArrgs)
                        mainView.Args.SMSArgs[0] = Convert.ToString(setting.User.FullName);
                    NotificationDataModel template = await BuildNotificationTemplate(userNotificationSettings.FirstOrDefault().IsArabic, userNotificationSettings.FirstOrDefault().NotificationCodeId, mainView.Args);
                    if (setting.Email)
                    {
                        email = new NotificationEmail(setting.UserProfileId.Value, userNotificationsModel.FirstOrDefault(u => u.UserId == setting.UserProfileId.Value).Email, template.Email.Title, template.Email.Body, setting.Id, mainView.Link, entityKey);
                        await _notifayCommands.AddNotifayWithOutSave(email);
                    }
                    if (setting.Sms)
                    {
                        sms = new NotificationSMS(setting.UserProfileId.Value, userNotificationsModel.FirstOrDefault(u => u.UserId == setting.UserProfileId.Value).Mobile, template.SMS.Body, setting.Id, mainView.Link, entityKey);
                        await _notifayCommands.AddNotifayWithOutSave(sms);
                    }
                    panel = new NotificationPanel(setting.UserProfileId.Value, template.PanelMessage, template.PanelMessage, setting.Id, mainView.Link, mainView.BranchId, mainView.CommitteeId, entityKey);
                    await _notifayCommands.AddNotifayWithOutSave(panel);
                }
                if (userNotificationSettings.Count > 0)
                    await _notifayCommands.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
        private async Task<NotificationDataModel> BuildNotificationTemplate(bool isArabic, int TemplateId, NotificationArguments notificationArguments)
        {
            var OperationCodes = await _cache.GetOrCreateAsync("OperationCodesList", async entry =>
            {
                int seconds = int.Parse(_configuration.ChachingConfiguration.CachingMinutes);
                entry.SlidingExpiration = TimeSpan.FromSeconds(seconds);
                return await _iNotificationQuerie.FindAllNotificationOperationCode();
            });
            var Template = OperationCodes.FirstOrDefault(w => w.NotificationOperationCodeId == TemplateId);
            NotificationDataModel notificationDataModel = new NotificationDataModel();
            notificationDataModel.Email.Body = ReplaceKeysWithValues((isArabic ? Template.EmailBodyTemplateAr : Template.EmailBodyTemplateEn), notificationArguments.BodyEmailArgs);
            notificationDataModel.Email.Title = ReplaceKeysWithValues((isArabic ? Template.EmailSubjectTemplateAr : Template.EmailSubjectTemplateEn), notificationArguments.SubjectEmailArgs);
            notificationDataModel.SMS.Body = ReplaceKeysWithValues((isArabic ? Template.SmsTemplateAr : Template.SmsTemplateEn), notificationArguments.SMSArgs);
            notificationDataModel.PanelMessage = ReplaceKeysWithValues((isArabic ? Template.PanelTemplateAr : Template.PanelTemplateEn), notificationArguments.PanelArgs);
            return notificationDataModel;

        }
        private string ReplaceKeysWithValues(string template, object[] args)
        {

            if (args == null)
            {
                return (string.IsNullOrEmpty(template) ? String.Empty : template);
            }
            try
            {
                return string.Format(string.IsNullOrEmpty(template) ? String.Empty : template, args);
            }
            catch (Exception)
            {

                return string.Format(Resources.SharedResources.Messages.DefaultNotificationMessage);
            }

        }

    }
}
