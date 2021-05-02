using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionProxy _subscriptionProxy;
        public SubscriptionService(ISubscriptionProxy subscriptionProxy)
        {
            _subscriptionProxy = subscriptionProxy;
        }
        public async Task<List<SubscriptionModel>> GetCRsSubscriptionStatuses(List<string> CRs)
        {
            return await _subscriptionProxy.GetCRsSubscriptionStatuses(CRs);
        }
    }
}
