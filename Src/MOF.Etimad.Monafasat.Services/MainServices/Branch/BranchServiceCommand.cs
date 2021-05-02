using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class BranchServiceCommand : IBranchServiceCommand
    {

        private readonly IAppDbContext _context;
        public BranchServiceCommand(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Branch> CreateBracnhAsync(Branch branch)
        {
            await _context.Branch.AddAsync(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<bool> DeActiveAsync(Branch branch)
        {
            branch.DeActive();
            _context.Branch.Update(branch);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<Branch> UpdateAsync(Branch branch)
        {
            _context.Branch.Update(branch);
            await _context.SaveChangesAsync();
            return branch;
        }
    }
}
