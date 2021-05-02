using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INotificationQueries
    {
        Task<NotificationOperationCode> GetDefaultSettingByCodeId(int CodeId);
        Task<List<UserNotificationSetting>> GetDefaultSettingByUserType(Enums.UserRole userType);
        Task<List<UserNotificationSetting>> GetDefaultSettingByCr();
        Task<List<UserNotificationSetting>> GetDefaultSettingByUserTypes(List<int> userRoleIds);
        Task<List<UserNotificationSetting>> GetNotificationSettingByCrAndOperationCode(List<string> cr, int notificationCodeId);
        Task<List<UserNotificationSetting>> GetNotificationSettingByUserIdAndUserType(int notificationCodeId, int branchId, int committeeId);
        Task<UserProfile> GetUser(int userId);
        Task<Supplier> GetSupplierUser(string cr);
        Task<List<NotificationPanelModel>> GetNotificationPanel(string userRoleName, int userId, int branchId, int committeeId);
        Task<QueryResult<NotificationPanelModel>> GetAllNotificationsAsync(string userRoleName, int userId, int branchId, int committeeId, SearchCriteria criteria);

        Task<int> GetNotificationPanelCount(int userId, int branchId);
        Task<int> GetNotificationPanelCountForSupplier(string cr);
        Task<OperationCodeViewModel> GetOperationCodeDetails(int Id);
        Task<BaseNotification> GetNotificationById(int notificationId);
        Task<List<BaseNotification>> GetNewNotification();
        Task<List<UserCategoryNotificatioSettingModel>> FindUserNotificationSetting(int userId, string cr, Enums.UserRole userType);
        Task<List<NotificationOperationCode>> FindAllNotificationOperationCode();
        Task<NotificationOperationCode> FindNotificationOperationCode(int Id);
        Task<List<NotificationPanelModel>> GetNotificationPanelForCr(string cr);
        Task<QueryResult<NotificationPanelModel>> GetAllNotificationsForCrAsync(string cr, SearchCriteria criteria);
        Task<QueryResult<TenderNotificationStatus>> GetNotificationStatusReport(TenderNotificationSearchCriteria criteria);
        Task<QueryResult<OperationCodeViewModel>> GetNotificationCodes(OperationCodeSearchCriteria criteria);
        Task<UserNotificationSetting> GetNotificationSettingByUserId(int notificationCodeId, int userId);
        Task<List<UserNotificationSetting>> GetNotificationSettingsByCodeId(int notificationCodeId);
        List<OperationCodeModel> GetOperationCode();
        List<NotificationStatusModel> GetNotificationStatus();

        Task<List<UserNotificationSetting>> GetNotificationSettingByRoleAndOperationCode(List<int> userIds, int operationCodeId, int branchId = 0, int? committeeId = 0);

        Task<List<UserNotificationSetting>> FindUserNotificationSettingbyUserProfileId(int userId);
    }
}
