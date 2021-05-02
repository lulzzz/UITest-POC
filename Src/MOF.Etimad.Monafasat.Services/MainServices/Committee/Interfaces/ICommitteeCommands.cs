using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommitteeCommands
    {
        Task<Committee> CreateAsync(Committee committee);
        Task<Committee> UpdateAsync(Committee committee);
        Task<CommitteeUser> UpdateCommitteUserAsync(CommitteeUser user);
    }
}
