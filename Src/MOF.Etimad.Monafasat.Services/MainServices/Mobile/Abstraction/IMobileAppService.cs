﻿using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel.MobileHelper;
using MOF.Etimad.Monafasat.SharedKernel.MobileHelper.Models;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Mobile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IMobileAppService
    {
        Task<DeviceTokenModel> FindDeviceTokenByIdAsync(int deviceTokenId);
        Task<StatusModel> RegisterToken(RegisterTokenModel model);
        Task<StatusModel> UpdateNotificationsStatus(string deviceToken, int cid, bool status);
        Task<Response<TenderModel>> GetTenders(string tendertype, int page, string title, string ref_no, string ga_head, int main_category, TimeSpan publish_date, bool no_old_tenders);
        Task<Dictionary<string, string>> GetActivities();
        Task<List<AgencModel>> GovAgencies();
        Task<List<int>> FindInterstedTDeviceokensInTenderActivity(List<int> activitieIds);
        Task PostNotificationForSuppliersThatInterstedInTenderActivity(Tender tender);
        Task SendPostedNotificationForSuppliersJob();
        Task<FetchDataStatus> FetchNotificationsStatus(string deviceToken);

    }
}
