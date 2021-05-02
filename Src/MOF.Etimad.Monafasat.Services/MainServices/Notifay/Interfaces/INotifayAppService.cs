using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 

namespace MOF.Etimad.Monafasat.Services
{
    public interface INotifayAppService
    {
        Task<UserProfile> GetUser(int userId);
        Task<List<SupplierUserProfile>> GetUserByCR(string userId);
         
        Task<List<UserProfile>> GetUsersByType(Enums.UserType userType);
        Task<List<UserProfile>> GetUsersByRole(String roleName);

        Task<List<UserProfile>> GetUsersByRoleNameAndAgencyCode(string roleName, string agencyCode);
        Task<List<UserProfile>> GetUsersByRoleNameAndBranchId(string roleName, int branchId);
        Task<UserProfile> GetCurrentUser();
        Task<UserProfile> AddUser(UserProfile userProfile);
        Task<UserProfile> Update(UserProfileModel userProfile);
        Task<Tuple<BaseNotification, UserProfile, NotificationBy>> AddNotifay(BaseNotification notification, Enums.UserType userType);
        Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAll(int userId, Enums.NotifayType notifayType, string title, string content, Enums.UserType userType);
        Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAll(int userId, Enums.NotifayType notifayType, string title, string content, string link, Enums.UserType userType);
        Task<Tuple<BaseNotification, UserProfile>> AddNotifayWithSend(BaseNotification notification, Enums.UserType userType);
        Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, Enums.UserType userType);
        Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, string link, Enums.UserType userType);
        Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content, string link, bool AttachLink, Enums.UserType userType);

        Task<Tuple<List<BaseNotification>, UserProfile>> AddNotifayToAllWithSend(int userId, Enums.NotifayType notifayType, string title, string content,
        string link, bool AttachLink, Enums.UserType userType, string entityId, Enums.NotificationEntityType type);

        Task<List<NotificationPanel>> GetNotificationPanel(int userId, int branchId);
        Task<int> GetNotificationPanelCount(int userId, int branchId);
        Task SetReadStateToNotification(int notificationId,int userId);
        Task<bool> ExistUser(int userId);

        Task ResendFailNotification();
        Task<bool> SendInvitationBySms(List<string> suppliersMobileNo, string message);
        Task<bool> SendInvitationByEmail(List<string> suppliersEmails, string subject, string body);

    }
}
