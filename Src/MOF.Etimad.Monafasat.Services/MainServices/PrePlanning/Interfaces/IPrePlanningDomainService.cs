using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public interface IPrePlanningDomainService
    {
        Task<bool> CheckNameExist(string name, int branchId, int quarterId, string agentId, int id);
    }
}
