using MOF.Etimad.Monafasat.Core;
using MOF.Eitimd.SharedKernel;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
 using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
   public class BranchCommitteeService: IBranchCommitteeService
    {

        private IAppDbContext _context;
        private IBranchCommitteeServiceQueries _branchCommitteeServiceQueries;
        public BranchCommitteeService(IAppDbContext context, IBranchCommitteeServiceQueries branchCommitteeServiceQueries)
        {
            _context = context;
            _branchCommitteeServiceQueries = branchCommitteeServiceQueries;
        }


        #region Service Query======================================================

        /// <summary>
        /// Find Branch Committees By SearchCriteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<QueryResult<BranchCommittee>> Find(BranchCommitteeSearchCriteria criteria)
        {
           Check.ArgumentNotNull(nameof(criteria), criteria);
          return await _branchCommitteeServiceQueries.Find(criteria);
        }

        #endregion

        #region  Service Command =====================================================

        /// <summary>
        /// Get Branch Committees By CommitteeId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BranchCommittee> GetById(int id)
        {
            //Check.ArgumentNotNull(nameof(criteria), criteria);
            return await _branchCommitteeServiceQueries.GetById(id);
        }

        /// <summary>
        /// Creat Branch Committee
        /// </summary>
        /// <param name="committee"></param>
        /// <returns></returns>
        public async Task<BranchCommittee> CreateAsync(BranchCommittee branchCommittee)
        {
            Check.ArgumentNotNull("branch committee", branchCommittee);

            //ToDo Check for Validate
            //ToDo Check for Business

            await _context.BranchCommittees.AddAsync(branchCommittee);
            await _context.SaveChangesAsync();

            return branchCommittee;
        }



        /// <summary>
        /// Update Branch Committee
        /// </summary>
        /// <param name="branch committee"></param>
        /// <returns></returns>
        public async Task<BranchCommittee> UpdateAsync(BranchCommittee branchCommittee)
        {
            Check.ArgumentNotNull("branch committee", branchCommittee);
            //ToDo Check for Validate
            //ToDo Check for Business

            _context.BranchCommittees.Update(branchCommittee);
            await _context.SaveChangesAsync();

            return branchCommittee;
        }


        /// <summary>
        /// DeActive Branch Committee
        /// </summary>
        /// <param name="branch committee"></param>
        /// <returns></returns>
        public async Task<bool> DeActiveAsync(int id)
        {
            //Check.ArgumentNotNull("committee", committee);
            //ToDo Check for Validate
            //ToDo Check for Business
            var branchCommittee = await _branchCommitteeServiceQueries.GetById(id);
            branchCommittee.DeActive();
            return await _context.SaveChangesAsync() == 1;
        }




        #endregion


    }
}
