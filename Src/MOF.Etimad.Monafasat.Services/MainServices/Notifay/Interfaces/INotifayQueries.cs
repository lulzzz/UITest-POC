using System.Collections.Generic;
using System.Threading.Tasks;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
 

namespace MOF.Etimad.Monafasat.Services
{
    public interface INotifayQueries
    {
        Task<UserProfile> GetUser(int userId);
        Task<List<NotificationPanel>> GetNotificationPanel(int userId, int branchId);
        Task<List<SupplierUserProfile>> GetUserByCR(string userId);
        //Task<List<UserProfile>> GetUsersByType(Enums.UserType userType);
        Task<List<UserProfile>> GetUsersByRoleNameAndAgencyCode(string roleName, string agencyCode);
        Task<List<UserProfile>> GetUsersByRoleNameAndBranchId(string roleName, int branchId);
        Task<int> GetNotificationPanelCount(int userId, int branchId);
        Task<BaseNotification> GetNotificationById(int notificationId);
        Task<List<BaseNotification>> GetFailNotification();
        Task<bool> ExistUser(int userId);
    }
}
