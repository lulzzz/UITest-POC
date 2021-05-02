using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AgencyBudgetService : IAgencyBudgetService
    {
        private readonly IAgencyBudgetProxy _agencyBudgettProxy;
        public AgencyBudgetService(IAgencyBudgetProxy agencyBudgetProxy)
        {
            _agencyBudgettProxy = agencyBudgetProxy;
        }
        public async Task<AgencyBudgetModel> GetAgencyProjectBudget(string ProjectNumber, bool IsGfs, string AgencyCode)
        {
            return await _agencyBudgettProxy.GetAgencyProjectBudget(ProjectNumber, IsGfs, AgencyCode);
        }
    }
}
