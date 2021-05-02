using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface ISubscriptionProxy
    {
        Task<List<SubscriptionModel>> GetCRsSubscriptionStatuses(List<string> CRs);
    }
}
