using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class NotificationCommands : IINotificationCommands
    {
        private readonly IAppDbContext _context;

        public NotificationCommands(IAppDbContext context)
        {
            _context = context;

        }

        public async Task<UserProfile> UpdateAsync(UserProfile profile)
        {
            Check.ArgumentNotNull(nameof(profile), profile);
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }
        public async Task<Supplier> UpdateSupplierAsync(Supplier profile)
        {
            Check.ArgumentNotNull(nameof(profile), profile);
            _context.Suppliers.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<NotificationOperationCode> AddNotificationOperationCode(NotificationOperationCode notificationOperationCode)
        {
            Check.ArgumentNotNull(nameof(notificationOperationCode), notificationOperationCode);

            _context.NotificationOperationCodes.Add(notificationOperationCode);

            await _context.SaveChangesAsync();
            return notificationOperationCode;
        }
        public async Task<BaseNotification> AddNotifayWithOutSave(BaseNotification notifay)
        {

            Check.ArgumentNotNull(nameof(notifay), notifay);
            _context.Notifications.Add(notifay);
            return notifay;
        }
        public async Task<BaseNotification> UpdateNotifay(BaseNotification notifay)
        {
            Check.ArgumentNotNull(nameof(notifay), notifay);

            _context.Notifications.Update(notifay);
            await _context.SaveChangesAsync();
            return notifay;
        }
        public async Task<NotificationOperationCode> UpdateNotificationOperationCode(NotificationOperationCode notificationOperationCode)
        {
            Check.ArgumentNotNull(nameof(notificationOperationCode), notificationOperationCode);

            _context.NotificationOperationCodes.Update(notificationOperationCode);
            await _context.SaveChangesAsync();
            return notificationOperationCode;
        }

        public void UpdateNotifayWithOutSave(BaseNotification notifay)
        {
            Check.ArgumentNotNull(nameof(notifay), notifay);
            _context.Notifications.Update(notifay);
        }

        public async Task SaveChangesAsync()
        {

            await _context.SaveChangesAsync();

        }
    }
}
