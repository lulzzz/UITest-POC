using MOF.Etimad.Monafasat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface INotifayCommands
    {
        Task<UserProfile> AddUser(UserProfile userProfile);
        Task<UserProfile> UpdateAsync(UserProfile profile);
        Task<BaseNotification> AddNotifay(BaseNotification notifay);
        Task<BaseNotification> UpdateNotifay(BaseNotification notifay);
        void UpdateNotifayWithOutSave(BaseNotification notifay);
        Task SaveChangesAsync();
        //Task SetReadStateToNotification(BaseNotification notification);


    }
}
