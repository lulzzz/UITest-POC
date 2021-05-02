using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IPrePlanningCommands
    {
        Task<PrePlanning> CreateAsync(PrePlanning prePlanning);
        Task<PrePlanning> UpdateAsync(PrePlanning prePlanning);
    }
}
