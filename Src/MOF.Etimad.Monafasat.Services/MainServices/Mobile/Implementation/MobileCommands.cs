using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.ViewModel.Mobile;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class MobileCommands : IMobileCommands
    {
        private readonly IAppDbContext _appDbContext;

        public MobileCommands(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<StatusModel> RegisterToken(RegisterTokenModel model, List<Activity> activities)
        {
            var token = new DeviceToken(model.deviceToken, model.deviceVerion, model.deviceName, model.model, model.udid,
                activities.Select(x => new DeviceTokenNotification(x.ActivityId, false)).ToList());
            _appDbContext.DeviceTokens.Add(token);
            var result = await _appDbContext.SaveChangesAsync();
            if (result > 0)
                return new StatusModel { status = "success" };
            return new StatusModel { status = "Failure" };

        }

        public async Task<StatusModel> UpdateNotificationsStatus(string deviceToken, int cid, bool status)
        {
            var result = await _appDbContext.DeviceTokenNotifications
                .FirstOrDefaultAsync(x => x.DeviceToken.DeviceTokenValue == deviceToken && x.ActivityId == cid);
            if (result == null)
            {
                return new StatusModel("Failure");
            }
            result.Update(status);
            _appDbContext.DeviceTokenNotifications.Update(result);
            var saveResult = await _appDbContext.SaveChangesAsync();
            if (saveResult > 0)
                return new StatusModel { status = "success" };
            return new StatusModel { status = "Failure" };



        }
        public async Task<MobileAlert> AddMobileAlert(MobileAlert mobileAlert)
        {
            _appDbContext.MobileAlerts.Add(mobileAlert);
            await _appDbContext.SaveChangesAsync();
            return mobileAlert;
        }
        public async Task<List<MobileAlert>> AddListMobileAlert(List<MobileAlert> mobileAlerts)
        {
            _appDbContext.MobileAlerts.AddRange(mobileAlerts);
            await _appDbContext.SaveChangesAsync();
            return mobileAlerts;
        }
        public async Task<List<MobileAlert>> UpdatListeMobileAlert(List<MobileAlert> mobileAlerts)
        {
            _appDbContext.MobileAlerts.UpdateRange(mobileAlerts);
            await _appDbContext.SaveChangesAsync();
            return mobileAlerts;
        }
    }
}
