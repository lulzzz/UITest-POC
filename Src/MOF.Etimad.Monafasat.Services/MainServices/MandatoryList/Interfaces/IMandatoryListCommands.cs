using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IMandatoryListCommands
    {
        Task<MandatoryList> Add(MandatoryList entity);
        Task Update(MandatoryList entity);
        Task AddChangeRequest(MandatoryListChangeRequest mandatoryListUpdates);
        Task UpdateChangeRequest(MandatoryListChangeRequest mandatoryListUpdates);
    }
}
