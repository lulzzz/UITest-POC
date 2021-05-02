using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Abstractions;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models;
using Microsoft.Extensions.Configuration;

namespace MOF.Etimad.Monafasat.Services
{
    public class NotifayAppService : INotifayAppService
    {
        private readonly INotifayQueries _notifayQueries;
        private readonly INotifayCommands _notifayCommands;
        private readonly INotifayDomainService _notifayDomainService;
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public NotifayAppService(INotifayQueries notifayQueries, INotifayCommands notifayCommands, INotifayDomainService notifayDomainService, INotificationService notificationService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _notifayQueries = notifayQueries;
            _notifayCommands = notifayCommands;
            _notifayDomainService = notifayDomainService;
            _notificationService = notificationService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<Tuple<BaseNotification, UserProfile, NotificationBy>> AddNotifay(BaseNotification notification, Enums.UserType userType)
        {
           return null;
        }

        public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAll(int userId, Enums.NotifayType notifayType, string title, string content, Enums.UserType userType)
        {
           return null;
        }

        public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAll(int userId, Enums.NotifayType notifayType, string title, string content, string link, Enums.UserType userType)
        {
           return null;
        }

        public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, Enums.UserType userType)
        {
           return null;
        }

        public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, string link, Enums.UserType userType)
        {
           return null;
        }

        public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, string link, bool AttachLink, Enums.UserType userType)
        {
           return null;
        }

        public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, string link, bool AttachLink, Enums.UserType userType, string entityId, Enums.NotificationEntityType type)
        {
           return null;
        }

        public async Task<Tuple<BaseNotification, UserProfile>> AddNotifayWithSend(BaseNotification notification, Enums.UserType userType)
        {
           return null;
        }

        public  async Task<UserProfile> AddUser(UserProfile userProfile)
        {
            return new UserProfile();
        }

        public async Task<bool> ExistUser(int userId)
        {
           return true;
        }

        public async Task<UserProfile> GetCurrentUser()
        {
           return null;
        }

        public async  Task<List<NotificationPanel>> GetNotificationPanel(int userId, int branchId)
        {

            return new List<NotificationPanel>();
          
        }

        public async Task<int> GetNotificationPanelCount(int userId, int branchId)
        {

            return 1;
        }

        public async Task<UserProfile> GetUser(int userId)
        {
            return new UserProfile() ;
        }

        public async Task<List<SupplierUserProfile>> GetUserByCR(string userId)
        {
            return new List<SupplierUserProfile>();
        }

        public async Task<List<UserProfile>> GetUsersByRole(string roleName)
        {
           return null;
        }

        public async Task<List<UserProfile>> GetUsersByRoleNameAndAgencyCode(string roleName, string agencyCode)
        {
            return new List<UserProfile>(); ;
        }

        public async Task<List<UserProfile>> GetUsersByRoleNameAndBranchId(string roleName, int branchId)
        {
            return new List<UserProfile>(); ;
        }

        public async Task<List<UserProfile>> GetUsersByType(Enums.UserType userType)
        {
           return null;
        }

        public  async Task ResendFailNotification()
        {
          // return  new ;
        }

        public async Task<bool> SendInvitationByEmail(List<string> suppliersEmails, string subject, string body)
        {
           return  true;
        }

        public async Task<bool> SendInvitationBySms(List<string> suppliersMobileNo, string message)
        {
            return true; ;
        }

        public async Task SetReadStateToNotification(int notificationId, int userId)
        {
          // return null;
        }

        public async Task<UserProfile> Update(UserProfileModel userProfile)
        {
           return new UserProfile();
        }

        //public async Task<List<UserProfile>> GetUsersByType(Enums.UserType userType)
        //{
        //    return await _notifayQueries.GetUsersByType(userType);
        //}

        //public async Task<List<UserProfile>> GetUsersByRoleNameAndAgencyCode(string roleName, string agencyCode)
        //{
        //    return await _notifayQueries.GetUsersByRoleNameAndAgencyCode(roleName, agencyCode);
        //}
        //public async Task<List<UserProfile>> GetUsersByRoleNameAndBranchId(string roleName, int branchId)
        //{
        //    return await _notifayQueries.GetUsersByRoleNameAndBranchId(roleName, branchId);
        //}

        //public async Task<List<UserProfile>> GetUsersByRole(String roleName)
        //{
        //    Enums.UserType type = (Enums.UserType)System.Enum.Parse(typeof(Enums.UserType), roleName, true);
        //    return await GetUsersByType(type);
        //}

        //public async Task<UserProfile> GetCurrentUser()
        //{
        //   return null;
        //}

        //public async Task<UserProfile> GetUser(int userId)
        //{
        //    return await _notifayQueries.GetUser(userId);
        //}

        //public async Task<bool> ExistUser(int userId)
        //{
        //    return await _notifayQueries.ExistUser(userId);
        //}

        //public async Task<List<SupplierUserProfile>> GetUserByCR(string commercialRegistration)
        //{
        //    return await _notifayQueries.GetUserByCR(commercialRegistration);
        //}

        //public async Task<UserProfile> AddUser(UserProfile userProfile)
        //{
        //    return await _notifayCommands.AddUser(userProfile);
        //}

        //public async Task<UserProfile> Update(UserProfileModel userProfile)
        //{
        //    var user = await _notifayQueries.GetUser(userProfile.UserId);
        //    var setting = user.NotificationSetting.FirstOrDefault(x => x.GetType() == UserProfile.GetObjectType(userProfile.UserType));
        //    if (setting == null) return null;

        //    if (setting.GetType().GetProperty(nameof(NotificationSettingModel.InquiriesAboutTender)) != null)
        //        setting.GetType().GetProperty(nameof(NotificationSettingModel.InquiriesAboutTender)).SetValue(setting, mapNotifayObject(userProfile.NotificationSetting.InquiriesAboutTender));
        //    if (setting.GetType().GetProperty(nameof(NotificationSettingModel.OfferStatus)) != null)
        //        setting.GetType().GetProperty(nameof(NotificationSettingModel.OfferStatus)).SetValue(setting, mapNotifayObject(userProfile.NotificationSetting.OfferStatus));
        //    if (setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsNeedToBeAccredited)) != null)
        //        setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsNeedToBeAccredited)).SetValue(setting, mapNotifayObject(userProfile.NotificationSetting.OperationsNeedToBeAccredited));
        //    if (setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsOnTheTender)) != null)
        //        setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsOnTheTender)).SetValue(setting, mapNotifayObject(userProfile.NotificationSetting.OperationsOnTheTender));
        //    if (setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsOnYourAccount)) != null)
        //        setting.GetType().GetProperty(nameof(NotificationSettingModel.OperationsOnYourAccount)).SetValue(setting, mapNotifayObject(userProfile.NotificationSetting.OperationsOnYourAccount));
        //    if (setting.GetType().GetProperty(nameof(NotificationSettingModel.PurchaseInvoiceNumber)) != null)
        //        setting.GetType().GetProperty(nameof(NotificationSettingModel.PurchaseInvoiceNumber)).SetValue(setting, mapNotifayObject(userProfile.NotificationSetting.PurchaseInvoiceNumber));
        //    if (setting.GetType().GetProperty(nameof(NotificationSettingModel.ReceiveTheAmountOfTheBooklet)) != null)
        //        setting.GetType().GetProperty(nameof(NotificationSettingModel.ReceiveTheAmountOfTheBooklet)).SetValue(setting, mapNotifayObject(userProfile.NotificationSetting.ReceiveTheAmountOfTheBooklet));
        //    if (setting.GetType().GetProperty(nameof(NotificationSettingModel.TenderRelatedToYourActivity)) != null)
        //        setting.GetType().GetProperty(nameof(NotificationSettingModel.TenderRelatedToYourActivity)).SetValue(setting, mapNotifayObject(userProfile.NotificationSetting.TenderRelatedToYourActivity));

        //    return await _notifayCommands.UpdateAsync(user);
        //}
        //Func<NotificationByModel, NotificationBy> mapNotifayObject = (src) => new NotificationBy { Mobile = src.Mobile, Email = src.Email };

        //public async Task<Tuple<BaseNotification, UserProfile, NotificationBy>> AddNotifay(BaseNotification notification, Enums.UserType userType)
        //{
        //    var user = await _notifayQueries.GetUser(notification.UserId);

        //    if (user == null) return null;
        //    if (user.NotificationSetting.Count < 1) return null;
        //    var setting = user.NotificationSetting.FirstOrDefault(x => x.GetType() == UserProfile.GetObjectType(userType));
        //    var haveProperty = setting.GetType().GetProperty(((Enums.NotifayType)notification.NotifayTypeId).ToString());
        //    if (haveProperty == null)
        //        throw new Exception("this user is not have this type");
        //    var notifay = (NotificationBy)haveProperty.GetValue(setting);
        //    if (notification is NotificationSMS && notifay.Mobile == true)
        //    {
        //        var result = await _notifayCommands.AddNotifay(notification);
        //        return new Tuple<BaseNotification, UserProfile, NotificationBy>(result, user, notifay);
        //    }
        //    else if (notification is NotificationEmail && notifay.Email == true)
        //    {
        //        var result = await _notifayCommands.AddNotifay(notification);
        //        return new Tuple<BaseNotification, UserProfile, NotificationBy>(result, user, notifay);
        //    }
        //    else if (notification is NotificationPanel)
        //    {
        //        var result = await _notifayCommands.AddNotifay(notification);
        //        return new Tuple<BaseNotification, UserProfile, NotificationBy>(result, user, notifay);
        //    }
        //    else
        //    {
        //        return new Tuple<BaseNotification, UserProfile, NotificationBy>(notification, user, notifay);
        //    }
        //}

        //public async Task<Tuple<BaseNotification, UserProfile, NotificationBy>> AddNotifay(BaseNotification notification, UserProfile user, NotificationBy notifay)
        //{
        //    if (notification is NotificationSMS && notifay.Mobile == true)
        //    {
        //        var result = await _notifayCommands.AddNotifay(notification);
        //        return new Tuple<BaseNotification, UserProfile, NotificationBy>(result, user, notifay);
        //    }
        //    else if (notification is NotificationEmail && notifay.Email == true)
        //    {
        //        var result = await _notifayCommands.AddNotifay(notification);
        //        return new Tuple<BaseNotification, UserProfile, NotificationBy>(result, user, notifay);
        //    }
        //    else if (notification is NotificationPanel)
        //    {
        //        var result = await _notifayCommands.AddNotifay(notification);
        //        return new Tuple<BaseNotification, UserProfile, NotificationBy>(result, user, notifay);
        //    }
        //    else
        //        return new Tuple<BaseNotification, UserProfile, NotificationBy>(notification, user, notifay);
        //}

        //public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAll(int userId, Enums.NotifayType notifayType, string title, string content, Enums.UserType userType)
        //{
        //    var user = await _notifayQueries.GetUser(userId);
        //    var haveProperty = user.GetObjectType()
        //        .FirstOrDefault(x => x.GetType() == UserProfile.GetObjectType(userType)).GetProperty(notifayType.ToString());
        //    if (haveProperty == null)
        //        throw new BusinessRuleException("this user is not have this type");
        //    var notifay = (NotificationBy)haveProperty.GetValue(user.NotificationSetting);
        //    var result = new List<BaseNotification>();
        //    var panel = new NotificationPanel(userId, title, content, notifayType, null, _httpContextAccessor.HttpContext.User.UserBranch());
        //    result.Add(await _notifayCommands.AddNotifay(panel));
        //    if (notifay.Mobile == true)
        //    {
        //        var sms = new NotificationSMS(userId, content, notifayType, null);
        //        result.Add(await _notifayCommands.AddNotifay(sms));
        //    }
        //    if (notifay.Email == true)
        //    {
        //        var email = new NotificationEmail(userId, title, content, notifayType, null);
        //        result.Add(await _notifayCommands.AddNotifay(email));
        //    }
        //    return new Tuple<List<BaseNotification>, UserProfile>(result, user);
        //}

        //public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAll(int userId, Enums.NotifayType notifayType, string title, string content, string link, Enums.UserType userType)
        //{


        //    var user = await _notifayQueries.GetUser(userId);
        //    var haveProperty = user.GetObjectType()
        //        .FirstOrDefault(x => x.GetType() == UserProfile.GetObjectType(userType)).GetProperty(notifayType.ToString());
        //    if (haveProperty == null)
        //        throw new BusinessRuleException("this user is not have this type");
        //    var notifay = (NotificationBy)haveProperty.GetValue(user.NotificationSetting);
        //    var result = new List<BaseNotification>();
        //    var panel = new NotificationPanel(userId, title, content, notifayType, link, _httpContextAccessor.HttpContext.User.UserBranch());
        //    result.Add(await _notifayCommands.AddNotifay(panel));
        //    if (notifay.Mobile == true)
        //    {
        //        var sms = new NotificationSMS(userId, content, notifayType, link);
        //        result.Add(await _notifayCommands.AddNotifay(sms));
        //    }
        //    if (notifay.Email == true)
        //    {
        //        var email = new NotificationEmail(userId, title, content, notifayType, link);
        //        result.Add(await _notifayCommands.AddNotifay(email));
        //    }
        //    return new Tuple<List<BaseNotification>, UserProfile>(result, user);
        //}

        //public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAll(UserProfile user, Enums.NotifayType notifayType, string title, string content, string link, Enums.UserType userType)
        //{
        //    var haveProperty = user.GetObjectType()
        //         .FirstOrDefault(x => x.GetType() == UserProfile.GetObjectType(userType)).GetProperty(notifayType.ToString());
        //    if (haveProperty == null)
        //        throw new BusinessRuleException("this user is not have this type");
        //    var notifay = (NotificationBy)haveProperty.GetValue(user.NotificationSetting);
        //    var result = new List<BaseNotification>();
        //    var panel = new NotificationPanel(user.Id, title, content, notifayType, link, _httpContextAccessor.HttpContext.User.UserBranch());
        //    result.Add(await _notifayCommands.AddNotifay(panel));
        //    if (notifay.Mobile == true)
        //    {
        //        var sms = new NotificationSMS(user.Id, content, notifayType, link);
        //        result.Add(await _notifayCommands.AddNotifay(sms));
        //    }
        //    if (notifay.Email == true)
        //    {
        //        var email = new NotificationEmail(user.Id, title, content, notifayType, link);
        //        result.Add(await _notifayCommands.AddNotifay(email));
        //    }
        //    return new Tuple<List<BaseNotification>, UserProfile>(result, user);
        //}

        //public async Task<Tuple<BaseNotification, UserProfile>> AddNotifayWithSend(BaseNotification notification, Enums.UserType userType)
        //{
        //    var resul = await AddNotifay(notification, userType);
        //    if (resul == null) return null;
        //    if (resul.Item1 is NotificationSMS && resul.Item1.NotifacationStatusId == (int)Enums.NotifacationStatus.Unsent && resul.Item3.Mobile)
        //    {
        //        var sms = new Common.NotificationServices.Models.SmsModel();
        //        sms.To.Add(resul.Item2.Mobile);
        //        sms.Body = string.Format("{0}\n{1}", ((NotificationSMS)notification).Content, $"{ notification.Link}");
        //        var state = await _notificationService.SendOneSms(sms);
        //        if (state == true)
        //        {
        //            ((NotificationSMS)notification).Update(Enums.NotifacationStatus.Send);
        //        }
        //        else
        //        {
        //            ((NotificationSMS)notification).Update(Enums.NotifacationStatus.FailedToSend);
        //        }
        //        await _notifayCommands.UpdateNotifay(notification);
        //    }
        //    else if (resul.Item1 is NotificationEmail && resul.Item1.NotifacationStatusId == (int)Enums.NotifacationStatus.Unsent && resul.Item3.Email)
        //    {
        //        var domain = _httpContextAccessor.HttpContext.Request.Scheme;

        //        var email = new Common.NotificationServices.Models.EmailModel
        //        {
        //            Subject = ((NotificationEmail)notification).Title
        //        };
        //        email.To.Add(resul.Item2.Email);
        //        email.Body = string.Format("{0}\n{1}", ((NotificationEmail)notification).Content, $"{ notification.Link}");

        //        var state = await _notificationService.SendOneEmail(email);
        //        if (state == true)
        //        {
        //            ((NotificationEmail)notification).Update(Enums.NotifacationStatus.Send);
        //            await _notifayCommands.UpdateNotifay(notification);
        //        }
        //        else
        //        {
        //            ((NotificationEmail)notification).Update(Enums.NotifacationStatus.FailedToSend);
        //            await _notifayCommands.UpdateNotifay(notification);
        //        }
        //    }
        //    return new Tuple<BaseNotification, UserProfile>(notification, resul.Item2);
        //}



        //public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, Enums.UserType userType)
        //{
        //    var result = new List<BaseNotification>();
        //    var sms = new NotificationSMS(userId, content, notifayType, null);
        //    var email = new NotificationEmail(userId, title, content, notifayType, null);
        //    var panel = new NotificationPanel(userId, title, content, notifayType, null, _httpContextAccessor.HttpContext.User.UserBranch());
        //    var smsResult = await AddNotifayWithSend(sms, userType);
        //    var emailResult = await AddNotifayWithSend(email, userType);
        //    var panelResult = await AddNotifayWithSend(panel, userType);
        //    if (smsResult == null || emailResult == null || panelResult == null) return null;
        //    result.Add(smsResult.Item1);
        //    result.Add(emailResult.Item1);
        //    result.Add(panelResult.Item1);
        //    return new Tuple<List<BaseNotification>, UserProfile>(result, panelResult.Item2);
        //}

        //public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, string link, Enums.UserType userType)
        //{
        //    var result = new List<BaseNotification>();
        //    var sms = new NotificationSMS(userId, content, notifayType, link);
        //    var email = new NotificationEmail(userId, title, content, notifayType, link);
        //    var panel = new NotificationPanel(userId, title, content, notifayType, link, _httpContextAccessor.HttpContext.User.UserBranch());
        //    var smsResult = await AddNotifayWithSend(sms, userType);
        //    var emailResult = await AddNotifayWithSend(email, userType);
        //    var panelResult = await AddNotifayWithSend(panel, userType);
        //    if (smsResult == null || emailResult == null || panelResult == null) return null;
        //    result.Add(smsResult.Item1);
        //    result.Add(emailResult.Item1);
        //    result.Add(panelResult.Item1);
        //    return new Tuple<List<BaseNotification>, UserProfile>(result, panelResult.Item2);
        //}



        //public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content,
        //    string link, bool AttachLink, Enums.UserType userType
        //)
        //{
        //    var result = new List<BaseNotification>();
        //    var sms = new NotificationSMS(userId, content, notifayType, AttachLink == true ? link : "");
        //    var email = new NotificationEmail(userId, title, content, notifayType, AttachLink == true ? link : "");
        //    var panel = new NotificationPanel(userId, title, content, notifayType, link, _httpContextAccessor.HttpContext.User.UserBranch());
        //    var smsResult = await AddNotifayWithSend(sms, userType);
        //    var emailResult = await AddNotifayWithSend(email, userType);
        //    var panelResult = await AddNotifayWithSend(panel, userType);
        //    if (smsResult == null || emailResult == null || panelResult == null) return null;
        //    result.Add(smsResult.Item1);
        //    result.Add(emailResult.Item1);
        //    result.Add(panelResult.Item1);
        //    return new Tuple<List<BaseNotification>, UserProfile>(result, panelResult.Item2);
        //}

        //Func<string, Enums.NotificationEntityType, string> TempletKey = (id, type) => $"{type.ToString()} => {id}";
        //public async Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content,
        //string link, bool AttachLink, Enums.UserType userType, string entityId, Enums.NotificationEntityType type)
        //{

        //    var temp = TempletKey(entityId, type);


        //    var result = new List<BaseNotification>();
        //    var sms = new NotificationSMS(userId, content, notifayType, AttachLink == true ? link : "", temp);
        //    var email = new NotificationEmail(userId, title, content, notifayType, AttachLink == true ? link : "", temp);
        //    var panel = new NotificationPanel(userId, title, content, notifayType, link, _httpContextAccessor.HttpContext.User.UserBranch(), temp);
        //    var smsResult = await AddNotifayWithSend(sms, userType);
        //    var emailResult = await AddNotifayWithSend(email, userType);
        //    var panelResult = await AddNotifayWithSend(panel, userType);
        //    if (smsResult == null || emailResult == null || panelResult == null) return null;
        //    result.Add(smsResult.Item1);
        //    result.Add(emailResult.Item1);
        //    result.Add(panelResult.Item1);
        //    return new Tuple<List<BaseNotification>, UserProfile>(result, panelResult.Item2);
        //}

        //public async Task<List<NotificationPanel>> GetNotificationPanel(int userId, int branchId)
        //{
        //    return await _notifayQueries.GetNotificationPanel(userId, branchId);
        //}

        //public async Task<int> GetNotificationPanelCount(int userId, int branchId)
        //{
        //    return await _notifayQueries.GetNotificationPanelCount(userId, branchId);
        //}

        //public async Task SetReadStateToNotification(int notificationId, int userId)
        //{
        //    var notification = await _notifayQueries.GetNotificationById(notificationId);
        //    if (notification is NotificationPanel && notification.UserId == userId)
        //    {
        //        ((NotificationPanel)notification).Update(Enums.NotifacationStatus.Read);
        //        await _notifayCommands.UpdateNotifay(notification);
        //    }
        //}

        //public async Task ResendFailNotification()
        //{
        //    var notifications = await _notifayQueries.GetFailNotification();
        //    List<Task> tasks = new List<Task>();
        //    notifications.ToList()
        //    .ForEach(x =>
        //    {
        //        if (x is NotificationSMS)
        //            tasks.Add(new Task(() =>
        //          {
        //              var sms = new Common.NotificationServices.Models.SmsModel();
        //              sms.To.Add(x.User.Mobile);
        //              sms.Body = string.Format("{0}\n{1}", ((NotificationSMS)x).Content, $"{ x.Link}");
        //              if (_notificationService.SendOneSms(sms).Result)
        //              {
        //                  ((NotificationSMS)x).Update(Enums.NotifacationStatus.Send);
        //                  _notifayCommands.UpdateNotifayWithOutSave(x);
        //              }

        //          }));
        //        if (x is NotificationEmail)
        //            tasks.Add(new Task(() =>
        //            {
        //                var email = new Common.NotificationServices.Models.EmailModel();
        //                email.To.Add(x.User.Mobile);
        //                email.Body = string.Format("{0}\n{1}", ((NotificationEmail)x).Content, $"{ x.Link}");
        //                if (_notificationService.SendOneEmail(email).Result)
        //                {
        //                    ((NotificationEmail)x).Update(Enums.NotifacationStatus.Send);
        //                    _notifayCommands.UpdateNotifayWithOutSave(x);
        //                }

        //            }));
        //    });

        //    tasks.ForEach(x => x.Start());
        //    Task.WaitAll(tasks.ToArray());
        //    if (tasks.Count > 0)
        //        await _notifayCommands.SaveChangesAsync();



        //}

        //public async Task<bool> SendInvitationBySms(List<string> suppliersMobileNo, string message)
        //{
        //    SmsModel smsModel = new SmsModel();
        //    //var monafasatUrl = "(https://monafasat.etimad.sa)";
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    foreach (var item in suppliersMobileNo)
        //    {
        //        dic.Add(item, message);
        //    }
        //    smsModel.ListOfNumbers = dic;
        //    return await _notificationService.SendSms(smsModel);
        //}
        //public async Task<bool> SendInvitationByEmail(List<string> suppliersEmails, string subject, string body)
        //{
        //    // get supplier email form identity server by (offer.SuplierId)
        //    EmailModel emailModel = new EmailModel();
        //    //var monafasatUrl = "(https://monafasat.etimad.sa)";
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    foreach (var item in suppliersEmails)
        //    {
        //        dic.Add(item, body);
        //        emailModel.Subject = subject;
        //    };
        //    emailModel.ListOfEmails = dic;
        //    return await _notificationService.SendEmail(emailModel);
        //}
    }
}
