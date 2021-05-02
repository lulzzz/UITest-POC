using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionModel>> GetCRsSubscriptionStatuses(List<string> CRs);
    }
}
