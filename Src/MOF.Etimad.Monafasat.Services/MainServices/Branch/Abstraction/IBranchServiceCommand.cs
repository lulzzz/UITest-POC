using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBranchServiceCommand
    {
        Task<Branch> CreateBracnhAsync(Branch branch);
        Task<Branch> UpdateAsync(Branch branch);
        Task<bool> DeActiveAsync(Branch branch);
    }
}
