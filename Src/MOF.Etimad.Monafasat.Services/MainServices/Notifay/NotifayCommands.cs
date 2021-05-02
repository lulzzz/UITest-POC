using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOF.Etimad.SharedKernel;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;

namespace MOF.Etimad.Monafasat.Services
{
    public class NotifayCommands : INotifayCommands
    {
        private readonly IAppDbContext _context;

        public NotifayCommands(IAppDbContext context)
        {
            _context = context;

        }

        public async Task<UserProfile> AddUser(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return userProfile;
        }


        public async Task<UserProfile> UpdateAsync(UserProfile profile)
        {
            Check.ArgumentNotNull(nameof(profile), profile);
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }


        public async Task<BaseNotification> AddNotifay(BaseNotification notifay)
        {
            Check.ArgumentNotNull(nameof(notifay), notifay);

            _context.Notifications.Add(notifay);

            await _context.SaveChangesAsync();
            return notifay;
        }
        public async Task<BaseNotification> UpdateNotifay(BaseNotification notifay)
        {
            Check.ArgumentNotNull(nameof(notifay), notifay);

            _context.Notifications.Update(notifay);
            await _context.SaveChangesAsync();
            return notifay;
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
