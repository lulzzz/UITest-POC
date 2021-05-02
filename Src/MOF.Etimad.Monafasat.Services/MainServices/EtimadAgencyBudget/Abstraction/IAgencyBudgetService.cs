using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAgencyBudgetService
    {
        Task<AgencyBudgetModel> GetAgencyProjectBudget(string ProjectNumber, bool IsGfs, string AgencyCode);
    }
}
