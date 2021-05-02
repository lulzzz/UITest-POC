using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel.MobileHelper;
using MOF.Etimad.Monafasat.SharedKernel.MobileHelper.Models;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IMobileQueries
    {
        Task<DeviceTokenModel> FindDeviceTokenByIdAsync(int deviceTokenId);
        Task<List<int>> FindInterstedTDeviceokensInTenderActivity(List<int> activities);
        Task<List<MobileAlert>> GetPendingMessages();
        Task<Response<TenderModel>> GetTenders(string tendertype, int page, string title, string ref_no, string ga_head, int main_category, TimeSpan publish_date, bool no_old_tenders);
        Dictionary<string, string> GetActivities();
        Task<List<Activity>> GetActivitiesObj();
        Task<List<int>> GetMainActivites(List<int> activites);
        Task<List<AgencModel>> GovAgencies();
        FetchDataStatus FetchNotificationsStatus(string deviceToken);
    }
}
