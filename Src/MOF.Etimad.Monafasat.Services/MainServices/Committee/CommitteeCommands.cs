using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommitteeCommands : ICommitteeCommands
    {
        private readonly IAppDbContext _context;

        public CommitteeCommands(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Committee> CreateAsync(Committee committee)
        {
            await _context.Committees.AddAsync(committee);
            await _context.SaveChangesAsync();
            return committee;
        }

        public async Task<Committee> UpdateAsync(Committee committee)
        {
            _context.Committees.Update(committee);
            await _context.SaveChangesAsync();
            return committee;
        }
        public async Task<CommitteeUser> UpdateCommitteUserAsync(CommitteeUser user)
        {
            _context.CommitteeUsers.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
