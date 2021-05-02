using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;

using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernel;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class NotifayQueries : INotifayQueries
    {
        private readonly IAppDbContext _context;

        public NotifayQueries(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<UserProfile> GetUser(int userId)
        {
            var result = await _context.UserProfiles
                .Where(x => x.Id == userId)
                .Include(x => x.NotificationSetting)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;

        }

        public async Task<List<NotificationPanel>> GetNotificationPanel(int userId, int branchId)
        {
            var result = await _context.Notifications
              .Where(x => x.UserId == userId && x is NotificationPanel)
             // .WhereIf(branchId != 0, x => x.BranchId == branchId)
              .Select(x => x as NotificationPanel)
              .OrderByDescending(x => x.CreatedAt)
              .Skip(0)
              .Take(10)
              .ToListAsync();
            return result;
        }
        public async Task<BaseNotification> GetNotificationById(int notificationId)
        {
            var result = await _context.Notifications
              .Where(x => x.Id == notificationId)
              .FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> GetNotificationPanelCount(int userId, int branchId)
        {
            var result = await _context.Notifications
              .Where(x => x.UserId == userId && x is NotificationPanel && x.NotifacationStatusId == (int)Enums.NotifacationStatus.Unread)
            //  .WhereIf(branchId != 0, x => x.BranchId == branchId)
              .CountAsync();
            return result;
        }

        public async Task<List<SupplierUserProfile>> GetUserByCR(string cr)
        {
            var result = await _context.SupplierUserProfiles.Include(u => u.Supplier).Include(u => u.UserProfile)
                .Where(x => x.SelectedCr == cr)
                //.AsNoTracking()
                .ToListAsync();
            return result;

        }
        //public async Task<List<UserProfile>> GetUsersByType(Enums.UserType userType)
        //{
        //    var result = await _context.UserProfiles
        //        .Where(x => x.GetType() == UserProfile.GetObjectType(userType))
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return result;

        //}
        public async Task<List<UserProfile>> GetUsersByRoleNameAndAgencyCode(string roleName, string agencyCode)
        {
            var result = await _context.UserProfiles.Where(u => u.UserAgencyRoles.Any(y => y.RoleName == roleName && y.AgencyCode == agencyCode && y.IsActive == true))
                .AsNoTracking()
                .ToListAsync();
            return result;
        }
        public async Task<List<UserProfile>> GetUsersByRoleNameAndBranchId(string roleName, int branchId)
        {
            var result = await _context.UserProfiles.Where(u => u.BranchUsers.Any(b => b.BranchId == branchId && b.RoleName == roleName && b.IsActive == true) && u.IsActive == true)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<bool> ExistUser(int userId)
        {
            return false;
        }

        public async Task<List<BaseNotification>> GetFailNotification()
        {
            return await _context.Notifications
                .Where(x => x is NotificationEmail || x is NotificationSMS)
                .Where(x => (x.NotifacationStatusId == (int)NotifacationStatus.FailedToSend) || x.NotifacationStatusId == (int)NotifacationStatus.Unsent)
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}
