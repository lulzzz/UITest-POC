using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models;
using MOF.Etimad.Monafasat.Services.MainServices.Notifay.Models;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using MOF.Etimad.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class NotificationAppService : INotificationAppService
    {
        private readonly IMemoryCache _cache;
        private readonly IIDMProxy _idmProxy; 
        private readonly INotificationQueries _iNotificationQuerie;
        private readonly IBranchServiceQueries _BranchQuery;
        private readonly ICommitteeQueries _CommitteeQuery;
        private readonly IGenericCommandRepository _genericrepository;
        private readonly IINotificationCommands _notifayCommands;
        private readonly INotificationProxy _notificationProxy;
        private readonly ILogger<NotificationAppService> _logger;
        private readonly RootConfigurations _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationAppService(INotificationProxy notificationProxy, IIDMProxy iDMProxy, IMemoryCache cache, INotificationQueries iNotificationQuerie, IINotificationCommands notifayCommands, IGenericCommandRepository genericrepository, ILogger<NotificationAppService> logger,
            IMapper mapper, IBranchServiceQueries BrancheQuery, ICommitteeQueries CommitteeQuery, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<RootConfigurations> optionsSnapShot)
        {
            _notificationProxy = notificationProxy;
            _idmProxy = iDMProxy;
            _cache = cache;
            _genericrepository = genericrepository;
            _iNotificationQuerie = iNotificationQuerie;
            _notifayCommands = notifayCommands;
            _logger = logger;
            _mapper = mapper;
            _BranchQuery = BrancheQuery;
            _CommitteeQuery = CommitteeQuery;
            _configuration = optionsSnapShot.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<UserNotificationSetting>> GetDefaultSettingByUserType(Enums.UserRole userType)
        {
            return await _iNotificationQuerie.GetDefaultSettingByUserType(userType);
        }
        public async Task<List<UserNotificationSetting>> GetDefaultSettingByCr()
        {
            return await _iNotificationQuerie.GetDefaultSettingByCr();
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

        public async Task AddNotificationSettingByUserId(int notificationCodeId, UserProfile userProfile, int roleid)
        {
            List<UserProfile> users = new List<UserProfile>();
            users.Add(userProfile);
            List<int> userIds = new List<int>();
            userIds.Add(userProfile.Id);

            var userNotificationSettings = await _iNotificationQuerie.GetNotificationSettingsByCodeId(notificationCodeId);

            var usersHaveSetting = userNotificationSettings.Select(d => d.UserProfileId).ToList();

            var usersHavntSetting = userIds.Where(d => !usersHaveSetting.Contains(d)).ToList();

            var Code = await _iNotificationQuerie.GetDefaultSettingByCodeId(notificationCodeId);

            foreach (var usr in users.Where(d => usersHavntSetting.Contains(d.Id)).ToList())
            {
                usr.AddNotificationSetting(
                    new UserNotificationSetting(Code.OperationCode, usr.Id, Code.NotificationOperationCodeId, Code.DefaultSMS, Code.DefaultEmail, roleid));
            }
            _genericrepository.UpdateRange<UserProfile>(users);
            await _genericrepository.SaveAsync();
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
                var ContactOfficer = crs.Where(a => a.CR == setting.Cr).ToList();
                foreach (var currentCR in ContactOfficer)
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
        public async Task<bool> SendInvitationByEmail(List<string> suppliersEmails, Tender tender)
        {
            EmailModel emailModel = new EmailModel();

            var monafasatUrl = _configuration.MonafasatURLConfiguration.MonafasatURL;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersEmails)
            {
                if (!dic.ContainsKey(item))
                {
                    dic.Add(item, string.Format(_configuration.InvitationEmailConfiguration.SendInvitationByEmail.body,
                         tender.TenderName, monafasatUrl));
                    emailModel.Subject = string.Format(string.Format(_configuration.InvitationEmailConfiguration.SendInvitationByEmail.subject, tender.TenderName));
                }
            };
            emailModel.ListOfEmails = dic;
            return await SendEmail(emailModel);
        }
        public async Task SendNotificationByEmailAndSmsForRolesChanged(int userId, string email, string mobileNumber)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var emailObj = new NotificationEmail(userId, email, Resources.BranchResources.Messages.EmailSubject, Resources.BranchResources.Messages.EmailBody, null, null);
                await _notifayCommands.AddNotifayWithOutSave(emailObj);
                await _notifayCommands.SaveChangesAsync();
            }

            if (!string.IsNullOrEmpty(mobileNumber))
            {
                var smsObj = new NotificationSMS(userId, mobileNumber, Resources.BranchResources.Messages.EmailBody, null, null, null);
                await _notifayCommands.AddNotifayWithOutSave(smsObj);
                await _notifayCommands.SaveChangesAsync();
            }
        }
        public async Task<bool> SendSolidarityInvitationByEmail(List<string> suppliersEmails, Tender tender, string supplierName)
        {
            EmailModel emailModel = new EmailModel();

            var monafasatUrl = _configuration.MonafasatURLConfiguration.MonafasatURL;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersEmails)
            {
                if (!dic.ContainsKey(item))
                {
                    dic.Add(item, string.Format(_configuration.InvitationSolidarityEmailConfiguration.SendInvitationByEmail.body,
                     tender.ReferenceNumber, supplierName, monafasatUrl));
                    emailModel.Subject = string.Format(string.Format(_configuration.InvitationSolidarityEmailConfiguration.SendInvitationByEmail.subject, tender.ReferenceNumber));
                }
            };
            emailModel.ListOfEmails = dic;
            return await SendEmail(emailModel);
        }
        public async Task SendNotificationForBranchUsers(int notificationCodeId, int branchId, MainNotificationTemplateModel mainView)
        {
            mainView.CommitteeId = null;
            mainView.BranchId = branchId;
            await SendNotificationForUsers(notificationCodeId, branchId, 0, mainView);
        }
        public async Task SendNotificationForCommitteeUsers(int notificationCodeId, int? committeeId, MainNotificationTemplateModel mainView)
        {
            mainView.CommitteeId = committeeId;
            mainView.BranchId = null;
            await SendNotificationForUsers(notificationCodeId, 0, committeeId, mainView);
        }
        public async Task SaveOperationCode(OperationCodeViewModel operationCode)
        {
            int Id = string.IsNullOrEmpty(operationCode.IdString) ? 0 : Util.Decrypt(operationCode.IdString);
            if (Id == 0)
            {
                var op = new NotificationOperationCode(operationCode.OperationCode, operationCode.ArabicName, operationCode.EnglishName, operationCode.PanelTemplateAr, operationCode.PanelTemplateEn, operationCode.EmailBodyTemplateAr, operationCode.EmailBodyTemplateEn, operationCode.SmsTemplateAr, operationCode.SmsTemplateEn, operationCode.NotificationCategoryId, operationCode.UserRoleId);
                await _notifayCommands.AddNotificationOperationCode(op);
            }
            else
            {
                var opCode = await _iNotificationQuerie.FindNotificationOperationCode(Util.Decrypt(operationCode.IdString));

                opCode.UpdateNotificationOperationCode(operationCode.OperationCode, operationCode.ArabicName, operationCode.EnglishName, operationCode.PanelTemplateAr, operationCode.PanelTemplateEn, operationCode.EmailBodyTemplateAr, operationCode.EmailBodyTemplateEn, operationCode.SmsTemplateAr, operationCode.SmsTemplateEn, operationCode.NotificationCategoryId, operationCode.UserRoleId);
                await _notifayCommands.UpdateNotificationOperationCode(opCode);

            }
            await _notifayCommands.SaveChangesAsync();

        }
        public async Task SendNotificationDirectByUserId(int notificationCodeId, int userId, MainNotificationTemplateModel mainNotificationTemplateModel)
        {
            string output = JsonConvert.SerializeObject(mainNotificationTemplateModel);
            MainNotificationTemplateModel mainView = JsonConvert.DeserializeObject<MainNotificationTemplateModel>(output);
            var userNotificationSetting = await _iNotificationQuerie.GetNotificationSettingByUserId(notificationCodeId, userId);
            if (userNotificationSetting == null)
                return;
            var role = _httpContextAccessor.HttpContext.User.UserRole();
            var idmUsers = await _idmProxy.GetEmployeeDetailsByRoleName(role);
            var userDataFromIDM = idmUsers.FirstOrDefault(s => s.userId == userId);
            var userNotificationsModel = new UserNotificationSettingModel
            {
                UserId = userNotificationSetting.UserProfileId.Value,
                Email = userDataFromIDM != null ? userDataFromIDM.email : userNotificationSetting.User.Email,
                Mobile = userDataFromIDM != null ? userDataFromIDM.mobileNumber : userNotificationSetting.User.Mobile,
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
                await SendOneEmail(new EmailModel { Body = template.Email.Body, Subject = template.Email.Title, To = new List<string> { userNotificationsModel.Email } });
            }
            if (userNotificationSetting.Sms)
            {
                await SendOneSms(new SmsModel { Body = template.SMS.Body, To = new List<string> { userNotificationsModel.Mobile } });
            }
        }
        private async Task SendNotificationForUsers(int notificationCodeId, int branchId, int? committeeId, MainNotificationTemplateModel mainNotificationTemplateModel)
        {
            try
            {
                string output = JsonConvert.SerializeObject(mainNotificationTemplateModel);
                MainNotificationTemplateModel mainView = JsonConvert.DeserializeObject<MainNotificationTemplateModel>(output);
                var oldUserNotificationSettings = await _iNotificationQuerie.GetNotificationSettingByUserIdAndUserType(notificationCodeId, branchId, (committeeId ?? 0));
                if (oldUserNotificationSettings.Count == 0)
                {
                    return;
                }
                string UserRole = ((Enums.UserRole)oldUserNotificationSettings[0].UserRoleId).ToString();
                var agencyDetails = new GovAgency();
                if (branchId != 0)
                    agencyDetails = await _BranchQuery.GetAgencyCodeByBranchId(branchId);
                else if (committeeId != null && committeeId != 0)
                    agencyDetails = await _CommitteeQuery.FindAgencyCodeByCommitteeId(committeeId);
                var agencyCode = agencyDetails != null ? agencyDetails.AgencyCode : "";
                var agencyType = agencyDetails != null ? agencyDetails.CategoryId : 0;
                UsersSearchCriteriaModel _usersSearchCriteriaModel = new UsersSearchCriteriaModel()
                {
                    RoleName = UserRole,
                    AgencyId = agencyCode,
                    PageSize = 1000,
                    AgencyType = agencyType ?? 0
                };
                var idmUsers = await _idmProxy.GetMonafasatUsersByAgencyTypeAndRoleName(_usersSearchCriteriaModel);
                var Users = _mapper.Map<QueryResult<EmployeeIntegrationModel>>(idmUsers);
                List<int> users = Users.Items.Select(x => x.userId).ToList();
                var userNotificationSettings = await _iNotificationQuerie.GetNotificationSettingByRoleAndOperationCode(users, notificationCodeId, branchId, committeeId);
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
            catch (Exception ex)
            {

                return string.Format(Resources.SharedResources.Messages.DefaultNotificationMessage);
            }

        }
        public async Task<List<UserNotificationSetting>> GetDefaultSettingByUserTypes(List<int> userTypeIds)
        {
            return await _iNotificationQuerie.GetDefaultSettingByUserTypes(userTypeIds);

        }

        #region [Setting]
        public async Task<UserProfile> GetUser(int userId)
        {
            return await _iNotificationQuerie.GetUser(userId);
        }
        public async Task<List<UserCategoryNotificatioSettingModel>> GetUserNotificationSetting(int userId, string cr, Enums.UserRole userType)
        {
            return await _iNotificationQuerie.FindUserNotificationSetting(userId, cr, userType);
        }
        public async Task SaveNotificationSetting(List<UserNotificatioSettingModel> userNotificatioSettings, int userId,Enums.UserRole userRole, string CR = "")
        { 
            if (userRole == Enums.UserRole.NewMonafasat_Supplier)
            {
                var userProfile = await _iNotificationQuerie.GetSupplierUser(CR);

                foreach (var setting in userNotificatioSettings)
                {
                    userProfile.UpdateNotificationSetting(Util.Decrypt(setting.Id), setting.EmailEnabled, setting.SMSEnabled);

                }
                await _notifayCommands.UpdateSupplierAsync(userProfile);
            }
            else
            {
                var userProfile = await _iNotificationQuerie.GetUser(userId);

                foreach (var setting in userNotificatioSettings)
                {
                    userProfile.UpdateNotificationSetting(Util.Decrypt(setting.Id), setting.EmailEnabled, setting.SMSEnabled);

                }
                await _notifayCommands.UpdateAsync(userProfile);
            }
        }
        Func<string, NotificationEntityType, string> TempletKey = (id, type) => $"{type.ToString()} => {id}";

        #endregion

        public async Task<List<NotificationPanelModel>> GetNotificationPanel(string userRoleName, int userId, int branchId, int committeeId)
        {
            return await _iNotificationQuerie.GetNotificationPanel(userRoleName, userId, branchId, committeeId);
        }
        public async Task<List<NotificationPanelModel>> GetNotificationPanelForCr(string cr)
        {
            return await _iNotificationQuerie.GetNotificationPanelForCr(cr);
        }
        public async Task<QueryResult<NotificationPanelModel>> GetAllNotificationsAsync(string userRoleName, int userId, int branchId, int committeeId, SearchCriteria criteria)
        {
            return await _iNotificationQuerie.GetAllNotificationsAsync(userRoleName, userId, branchId, committeeId, criteria);
        }
        public async Task<QueryResult<NotificationPanelModel>> GetAllNotificationsForCrAsync(string cr, SearchCriteria criteria)
        {
            return await _iNotificationQuerie.GetAllNotificationsForCrAsync(cr, criteria);
        }
        public async Task<int> GetNotificationPanelCount(int userId, int branchId)
        {
            return await _iNotificationQuerie.GetNotificationPanelCount(userId, branchId);
        }

        public async Task<int> GetNotificationPanelCountForSupplier(string cr)
        {
            return await _iNotificationQuerie.GetNotificationPanelCountForSupplier(cr);
        }
        public async Task<OperationCodeViewModel> GetOperationCodeDetails(int Id)
        {
            return await _iNotificationQuerie.GetOperationCodeDetails(Id);
        }
        public async Task SetReadStateToNotification(int notificationId)
        {
            var notification = await _iNotificationQuerie.GetNotificationById(notificationId);
            ((NotificationPanel)notification).Update(NotifacationStatus.Read);
            await _notifayCommands.UpdateNotifay(notification);
        }

        public async Task ResendFailNotification()
        {
            var notifications = (await _iNotificationQuerie.GetNewNotification());
            int count = 0;
            foreach (var item in notifications)
            {
                count++;
                if (item is NotificationSMS)
                {
                    var sms = new Common.NotificationServices.Models.SmsModel();
                    sms.To.Add(((NotificationSMS)item).CurrentMobile);
                    sms.Body = ((NotificationSMS)item).Content;
                    if (await SendOneSms(sms))
                    {
                        ((NotificationSMS)item).Update(Enums.NotifacationStatus.Send);
                    }
                    else
                    {
                        ((NotificationSMS)item).Update(Enums.NotifacationStatus.FailedToSend);
                    }
                    _notifayCommands.UpdateNotifayWithOutSave(item);
                }
                if (item is NotificationEmail)
                {
                    var email = new Common.NotificationServices.Models.EmailModel();
                    email.To.Add(((NotificationEmail)item).CurrentEmail);
                    email.Body = ((NotificationEmail)item).Content;
                    email.Subject = ((NotificationEmail)item).Title;
                    if (await SendOneEmail(email))
                    {
                        ((NotificationEmail)item).Update(Enums.NotifacationStatus.Send);
                    }
                    else
                    {
                        ((NotificationEmail)item).Update(Enums.NotifacationStatus.FailedToSend);
                    }
                    _notifayCommands.UpdateNotifayWithOutSave(item);
                }
                if (count == 100)
                {
                    await _notifayCommands.SaveChangesAsync();
                    count = 0;
                }
            }
            await _notifayCommands.SaveChangesAsync();
        }

        public async Task UpdateUser(UserProfile userProfile)
        {
            Check.ArgumentNotNullOrEmpty(nameof(userProfile), userProfile.ToString());
            await _notifayCommands.UpdateAsync(userProfile);
        }

        public async Task<QueryResult<TenderNotificationStatus>> GetNotificationStatusReport(TenderNotificationSearchCriteria criteria)
        {
            criteria.Key = TempletKey(criteria.tId.ToString(), NotificationEntityType.Tender);
            QueryResult<TenderNotificationStatus> result = await _iNotificationQuerie.GetNotificationStatusReport(criteria);

            return result;

        }
        public async Task<QueryResult<OperationCodeViewModel>> FindNotificationCodesBySearchCriteriaForPage(OperationCodeSearchCriteria criteria)
        {
            QueryResult<OperationCodeViewModel> result = await _iNotificationQuerie.GetNotificationCodes(criteria);
            return result;

        }
        public async Task<bool> SendEmail(EmailModel model)
        {
            List<bool> tasks = new List<bool>();
            foreach (var item in model.ListOfEmails)
            {
                tasks.Add(await _notificationProxy.SendEmail(item.Key, model.Subject, item.Value));
            }
            return tasks.Any(x => x == false);

        }

        public async Task<bool> SendOneEmail(EmailModel model)
        {
            return await _notificationProxy.SendEmail(model.To.FirstOrDefault(), model.Subject, model.Body);
        }

        public async Task<bool> SendOneSms(SmsModel model)
        {
            return await _notificationProxy.SendSMS(model.To.FirstOrDefault(), model.Body);
        }

        public async Task<bool> SendSms(SmsModel model)
        {
            List<bool> tasks = new List<bool>();
            foreach (var item in model.ListOfNumbers)
            {
                await _notificationProxy.SendSMS(item.Key, item.Value);
            }
            return tasks.Any(x => x == false);


        }


        public async Task<bool> SendInvitationBySms(List<string> suppliersMobileNo, Tender tender)
        {
            SmsModel smsModel = new SmsModel();
            var monafasatUrl = "(" + _configuration.MonafasatURLConfiguration.MonafasatURL + ")";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersMobileNo)
            {
                if (!dic.ContainsKey(item))
                    dic.Add(item, string.Format(string.Format(_configuration.InvitationEmailConfiguration.SendInvitationBySms, tender.TenderName, monafasatUrl)));
            }
            smsModel.ListOfNumbers = dic;
            return await SendSms(smsModel);
        }

        public async Task<bool> SendSolidarityInvitationBySms(List<string> suppliersMobileNo, Tender tender, string name)
        {
            SmsModel smsModel = new SmsModel();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in suppliersMobileNo)
            {
                if (!dic.ContainsKey(item))
                    dic.Add(item, string.Format(string.Format(_configuration.InvitationSolidarityEmailConfiguration.SendInvitationBySms, tender.ReferenceNumber, name)));
            }
            smsModel.ListOfNumbers = dic;
            return await SendSms(smsModel);
        }

        public List<OperationCodeModel> GetOperationCode()
        {
            return _iNotificationQuerie.GetOperationCode();
        }
        public List<NotificationStatusModel> GetNotificationStatus()
        {
            return _iNotificationQuerie.GetNotificationStatus();
        }

        public async Task SendNotificationForUsersByRoleName(int notificationCodeId, string roleName, MainNotificationTemplateModel mainNotificationTemplateModel, List<int> userIds = null, string AgencyCode = "", int agencyType = 0)
        {
            string output = JsonConvert.SerializeObject(mainNotificationTemplateModel);
            MainNotificationTemplateModel mainView = JsonConvert.DeserializeObject<MainNotificationTemplateModel>(output);
            List<int> users = new List<int>();
            if ((userIds?.Count() ?? 0) == 0)
            {
                UsersSearchCriteriaModel _usersSearchCriteriaModel = new UsersSearchCriteriaModel()
                {
                    RoleName = roleName
                };
                if (AgencyCode != null)
                    _usersSearchCriteriaModel.AgencyId = AgencyCode;
                if (agencyType != 0)
                    _usersSearchCriteriaModel.AgencyType = agencyType;
                var idmUsers = await _idmProxy.GetMonafasatUsersByAgencyTypeAndRoleName(_usersSearchCriteriaModel);
                users = idmUsers.Items.Select(x => x.userId).ToList();
            }
            else
            {
                users = userIds;
            }

            var userNotificationSettings = await _iNotificationQuerie.GetNotificationSettingByRoleAndOperationCode(users, notificationCodeId);
            if (userNotificationSettings.Count == 0)
                return;
            var userNotificationsModel = userNotificationSettings.Select(x => new UserNotificationSettingModel { UserId = x.UserProfileId.Value, Email = x.User.Email, Mobile = x.User.Mobile }).ToList();
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


        public async Task SendNotificationForUsersByRoleNameAndAgency(int notificationCodeId, string roleName, MainNotificationTemplateModel mainNotificationTemplateModel, string AgencyCode, int agencyType, List<int> userIds = null)
        {
            UsersSearchCriteriaModel _usersSearchCriteriaModel = new UsersSearchCriteriaModel()
            {
                RoleName = roleName,
                AgencyId = AgencyCode,
                AgencyType = agencyType

            };
            string output = JsonConvert.SerializeObject(mainNotificationTemplateModel);
            MainNotificationTemplateModel mainView = JsonConvert.DeserializeObject<MainNotificationTemplateModel>(output);
            List<int> users = new List<int>();
            if ((userIds?.Count() ?? 0) == 0)
            {
                var idmUsers = await _idmProxy.GetMonafasatUsersByAgencyTypeAndRoleName(_usersSearchCriteriaModel);
                var Users = _mapper.Map<QueryResult<EmployeeIntegrationModel>>(idmUsers);
                users = Users.Items.Select(x => x.userId).ToList();
            }
            else
            {
                users = userIds;
            }

            var userNotificationSettings = await _iNotificationQuerie.GetNotificationSettingByRoleAndOperationCode(users, notificationCodeId);
            if (userNotificationSettings.Count == 0)
                return;
            var userNotificationsModel = userNotificationSettings.Select(x => new UserNotificationSettingModel { UserId = x.UserProfileId.Value, Email = x.User.Email, Mobile = x.User.Mobile }).ToList();
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
    }
}
