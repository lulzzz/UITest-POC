using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel.Mobile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IMobileCommands
    {

        Task<MobileAlert> AddMobileAlert(MobileAlert mobileAlert);
        Task<List<MobileAlert>> AddListMobileAlert(List<MobileAlert> mobileAlerts);
        Task<List<MobileAlert>> UpdatListeMobileAlert(List<MobileAlert> mobileAlerts);
        Task<StatusModel> RegisterToken(RegisterTokenModel model, List<Activity> activities);
        Task<StatusModel> UpdateNotificationsStatus(string deviceToken, int cid, bool status);


    }
}
