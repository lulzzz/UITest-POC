using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AssignUserCommitteeServiceQueries : IAssignUserCommitteeServiceQueries
    {
        private IAppDbContext _context;
        public AssignUserCommitteeServiceQueries(IAppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Find By Search criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<List<AssignUserCommittee>> Find(AssignUserCommitteeSearchCriteria criteria)
        {
            var result = await _context.AssignUserCommittees.Include(x => x.Committee)
                .Where(x => x.IsActive.HasValue && x.IsActive.Value
                && (x.UserId == criteria.UserId || criteria.UserId == 0)
                && (x.CommitteeId == criteria.CommitteeId || criteria.CommitteeId == 0))
                .OrderBy(x => x.AssignUserCommitteeId)
                .SortBy(criteria.Sort , criteria.SortDirection)
                //.Page(criteria.PageNumber, criteria.PageSize)
                .ToListAsync();

            return result;
        }


        /// <summary>
        /// Get By CommitteeId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AssignUserCommittee> GetById(int id)
        {
            var result = await _context.AssignUserCommittees.Include(x => x.Committee).FirstOrDefaultAsync(a => a.AssignUserCommitteeId == id);
            return result;
        }

    }
}
