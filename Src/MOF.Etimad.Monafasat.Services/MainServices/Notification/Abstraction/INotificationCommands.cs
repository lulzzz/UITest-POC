using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IINotificationCommands
    {
        Task<UserProfile> UpdateAsync(UserProfile profile);
        Task<NotificationOperationCode> AddNotificationOperationCode(NotificationOperationCode notificationOperationCode);
        Task<NotificationOperationCode> UpdateNotificationOperationCode(NotificationOperationCode notificationOperationCode);
        Task<BaseNotification> AddNotifayWithOutSave(BaseNotification notifay);
        Task<BaseNotification> UpdateNotifay(BaseNotification notifay);
        void UpdateNotifayWithOutSave(BaseNotification notifay);
        Task SaveChangesAsync();
        Task<Supplier> UpdateSupplierAsync(Supplier profile);
    }
}
