using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MOF.Etimad.Monafasat.Services
{
    public class IDMJobQueries : IIDMJobQueries
    {
        private readonly IAppDbContext _context;
        public IDMJobQueries(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserProfile>> FindUsersProfileByIdAsync(List<int?> userIds)
        {
            var result = await _context.UserProfiles.Where(a => userIds.Contains(a.Id)).ToListAsync();
            return result;
        }
    }
}