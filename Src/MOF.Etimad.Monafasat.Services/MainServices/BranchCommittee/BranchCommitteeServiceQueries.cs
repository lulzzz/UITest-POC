using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class BranchCommitteeServiceQueries : IBranchCommitteeServiceQueries
    {
        private IAppDbContext _context;
        public BranchCommitteeServiceQueries(IAppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Find By Search criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<QueryResult<BranchCommittee>> Find(BranchCommitteeSearchCriteria criteria)
        {
            var result = await _context.BranchCommittees.Include(x => x.Committee)
                .Where(x => x.IsActive.HasValue && x.IsActive.Value
                && (x.BranchId == criteria.BranchId || criteria.BranchId == 0)
                && (x.CommitteeId == criteria.CommitteeId || criteria.CommitteeId == 0))
                .OrderBy(x => x.BranchCommitteeId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
              

            return result;
        }


        /// <summary>
        /// Get By CommitteeId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BranchCommittee> GetById(int id)
        {
            var result = await _context.BranchCommittees.Include(x => x.Committee).FirstOrDefaultAsync(a => a.BranchCommitteeId == id);
            return result;
        }

    }
}
