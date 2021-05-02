using CheckFundService;
using MOF.Etimad.Monafasat.Integration.Enums;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface ICheckFundProxy
    {
        Task<AgencyBudgetCalcFundsRsBody_Type> GetProjectBudgetByType(Project project, BudgetType budgetType, bool isGfsCode);
        Task<AgencyBudgetCalcFundsRsBody_Type> GetProjectBudgetByType(string agencyCode, string projectNumber, BudgetType budgetType, bool isGfsCode);
    }
}
