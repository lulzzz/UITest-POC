using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel.MobileHelper;
using MOF.Etimad.Monafasat.SharedKernel.MobileHelper.Models;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Mobile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class MobileAppService : IMobileAppService
    {
        private IMobileQueries _mobileQueries;


        private IMobileCommands _mobileCommands;
        private IConfigurationRoot _configuration;
        private readonly IPushNotificationService _pushNotificationService;


        public MobileAppService(IMobileQueries mobileQueries, IMobileCommands mobileCommands/*, IConfiguration configuration*/, IPushNotificationService pushNotificationService)
        {
            _mobileQueries = mobileQueries;
            _mobileCommands = mobileCommands;
            _configuration = _configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();
            _pushNotificationService = pushNotificationService;
        }

        #region Queries =========================================

        public Task<DeviceTokenModel> FindDeviceTokenByIdAsync(int deviceTokenId)
        {
            return _mobileQueries.FindDeviceTokenByIdAsync(deviceTokenId);
        }

        public async Task<StatusModel> RegisterToken(RegisterTokenModel model)
        {
            var activities = (await _mobileQueries.GetActivitiesObj());
            var result = await _mobileCommands.RegisterToken(model, activities);
            return result;
        }

        public async Task<StatusModel> UpdateNotificationsStatus(string deviceToken, int cid, bool status)
        {
            var result = await _mobileCommands.UpdateNotificationsStatus(deviceToken, cid, status);
            return result;
        }

        public async Task<Response<TenderModel>> GetTenders(string tendertype, int page, string title, string ref_no, string ga_head, int main_category, TimeSpan publish_date, bool no_old_tenders)
        {
            var result = await _mobileQueries.GetTenders(tendertype, page, title, ref_no, ga_head, main_category, publish_date, no_old_tenders);
            return result;
        }

        public async Task<Dictionary<string, string>> GetActivities()
        {
            var result = _mobileQueries.GetActivities();
            return result;
        }

        public async Task<List<AgencModel>> GovAgencies()
        {
            var result = await _mobileQueries.GovAgencies();
            return result;
        }

        public async Task<List<int>> FindInterstedTDeviceokensInTenderActivity(List<int> activitieIds)
        {
            var result = await _mobileQueries.FindInterstedTDeviceokensInTenderActivity(activitieIds);
            return result;
        }
        public async Task PostNotificationForSuppliersThatInterstedInTenderActivity(Tender tender)
        {
            var message = string.Format(string.Format(_configuration.GetSection("SendNotificationForSuppliersForCreatNewRelatedTender:SendMessage:body").Value));
            var activitiesIds = tender.TenderActivities.Select(s => s.ActivityId).ToList();
            var mainActivity = await _mobileQueries.GetMainActivites(activitiesIds);
            var deviceIds = await _mobileQueries.FindInterstedTDeviceokensInTenderActivity(mainActivity);
            List<MobileAlert> mobileAlerts = new List<MobileAlert>();
            foreach (var id in deviceIds)
            {
                var mobileAlert = new MobileAlert(message, id, null, (int)SharedKernel.Enums.MessageStatus.Pending, DateTime.Now.Date);
                mobileAlerts.Add(mobileAlert);
            }
            await _mobileCommands.AddListMobileAlert(mobileAlerts);
        }
        public async Task SendPostedNotificationForSuppliersJob()
        {
            var mobileAlerts = await _mobileQueries.GetPendingMessages();
            foreach (var item in mobileAlerts)
            {
                item.MessageSent();
                _pushNotificationService.SendNotification(item.Message, item.DeviceToken.DeviceTokenValue);
            }
            await _mobileCommands.UpdatListeMobileAlert(mobileAlerts);
        }

        public async Task<FetchDataStatus> FetchNotificationsStatus(string deviceToken)
        {
            var result = _mobileQueries.FetchNotificationsStatus(deviceToken);
            return result;
        }
        #endregion

    }
}
