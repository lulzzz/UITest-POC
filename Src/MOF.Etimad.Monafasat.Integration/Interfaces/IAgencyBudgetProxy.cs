using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IAgencyBudgetProxy
    {
        Task<AgencyBudgetModel> GetAgencyProjectBudget(string ProjectNumber, bool IsGfs, string AgencyCode);
    }
}
