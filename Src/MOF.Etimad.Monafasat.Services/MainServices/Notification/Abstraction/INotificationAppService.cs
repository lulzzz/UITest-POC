using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INotificationAppService
    {
        Task<List<UserNotificationSetting>> GetDefaultSettingByUserType(SharedKernel.Enums.UserRole userType);
        Task<List<UserNotificationSetting>> GetDefaultSettingByCr();
        Task<List<UserNotificationSetting>> GetDefaultSettingByUserTypes(List<int> userTypeIds);
        Task SendNotificationForBranchUsers(int notificationCodeId, int branchId, MainNotificationTemplateModel mainView);
        Task SendNotificationForCommitteeUsers(int notificationCodeId, int? committeeId, MainNotificationTemplateModel mainView);
        Task<bool> SendNotificationForSuppliers(int notificationCodeId, List<string> crsList, MainNotificationTemplateModel mainNotificationTemplateModel, Dictionary<string, Dictionary<int, string>> dynamicParameterList = null);
        Task<List<UserCategoryNotificatioSettingModel>> GetUserNotificationSetting(int userId, string cr, SharedKernel.Enums.UserRole userType);
        Task<UserProfile> GetUser(int userId);
        Task SaveNotificationSetting(List<UserNotificatioSettingModel> userNotificatioSettings, int userId, SharedKernel.Enums.UserRole userRole, string CR = "");
        Task ResendFailNotification();
        Task<List<NotificationPanelModel>> GetNotificationPanel(string userRoleName, int userId, int branchId, int committeeId);
        Task<List<NotificationPanelModel>> GetNotificationPanelForCr(string cr);
        Task<QueryResult<NotificationPanelModel>> GetAllNotificationsAsync(string userRoleName, int userId, int branchId, int committeeId, SearchCriteria criteria);
        Task<QueryResult<NotificationPanelModel>> GetAllNotificationsForCrAsync(string cr, SearchCriteria criteria);
        Task<int> GetNotificationPanelCount(int userId, int branchId);
        Task<int> GetNotificationPanelCountForSupplier(string cr);
        Task<OperationCodeViewModel> GetOperationCodeDetails(int Id);
        Task SetReadStateToNotification(int notificationId);
        Task UpdateUser(UserProfile userProfile);
        Task<QueryResult<TenderNotificationStatus>> GetNotificationStatusReport(TenderNotificationSearchCriteria criteria);
        Task SendNotificationDirectByUserId(int notificationCodeId, int userId, MainNotificationTemplateModel mainNotificationTemplateModel);
        List<OperationCodeModel> GetOperationCode();
        List<NotificationStatusModel> GetNotificationStatus();
        #region [Refactor by Mohammed Ibrahim but still needs some Enhancement]
        Task<bool> SendOneEmail(EmailModel model);
        Task<bool> SendOneSms(SmsModel model);
        Task<bool> SendSms(SmsModel model);
        Task<QueryResult<OperationCodeViewModel>> FindNotificationCodesBySearchCriteriaForPage(OperationCodeSearchCriteria criteria);
        Task<bool> SendEmail(EmailModel model);
        Task<bool> SendInvitationBySms(List<string> suppliersMobileNo, Tender tender);
        Task<bool> SendSolidarityInvitationBySms(List<string> suppliersMobileNo, Tender tender, string name);
        Task<bool> SendInvitationByEmail(List<string> suppliersEmails, Tender tender);

        Task SendNotificationByEmailAndSmsForRolesChanged(int userId, string email, string mobileNumber);

        Task<bool> SendSolidarityInvitationByEmail(List<string> suppliersEmails, Tender tender, string supplierName);
        #endregion
        Task SendNotificationForUsersByRoleName(int notificationCodeId, string roleName, MainNotificationTemplateModel mainNotificationTemplateModel, List<int> userIds = null, string AgencyCode = "", int agencyType = 0);
        Task SendNotificationForUsersByRoleNameAndAgency(int notificationCodeId, string roleName, MainNotificationTemplateModel mainNotificationTemplateModel, string AgencyCode, int agencyType, List<int> userIds = null);
        Task SaveOperationCode(OperationCodeViewModel operationCode);
        Task SendNotificationByUserId(int notificationCodeId, int userId, string userName, MainNotificationTemplateModel mainNotificationTemplateModel);
        Task AddNotificationSettingByUserId(int notificationCodeId, UserProfile userProfile, int roleid);
    }
}
