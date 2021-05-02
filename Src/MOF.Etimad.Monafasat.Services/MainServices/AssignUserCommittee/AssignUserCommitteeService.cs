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
   public class AssignUserCommitteeService : IAssignUserCommitteeService
    {

        private IAppDbContext _context;
        private IAssignUserCommitteeServiceQueries _assignUserCommitteeServiceQueries;
        public AssignUserCommitteeService(IAppDbContext context, IAssignUserCommitteeServiceQueries assignUserCommitteeServiceQueries)
        {
            _context = context;
            _assignUserCommitteeServiceQueries = assignUserCommitteeServiceQueries;
        }


        #region Service Query======================================================

        /// <summary>
        /// Find User Committees By SearchCriteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<List<AssignUserCommittee>> Find(AssignUserCommitteeSearchCriteria criteria)
        {
           Check.ArgumentNotNull(nameof(criteria), criteria);
          return await _assignUserCommitteeServiceQueries.Find(criteria);
        }

        #endregion

        #region  Service Command =====================================================

        /// <summary>
        /// Get Assign User Committees By CommitteeId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AssignUserCommittee> GetById(int id)
        {
            //Check.ArgumentNotNull(nameof(criteria), criteria);
            return await _assignUserCommitteeServiceQueries.GetById(id);
        }

        /// <summary>
        /// Creat Branch Committee
        /// </summary>
        /// <param name="committee"></param>
        /// <returns></returns>
        public async Task<AssignUserCommittee> CreateAsync(AssignUserCommittee assignUserCommittee)
        {
            Check.ArgumentNotNull("assign user committee", assignUserCommittee);

            //ToDo Check for Validate
            //ToDo Check for Business

            await _context.AssignUserCommittees.AddAsync(assignUserCommittee);
            await _context.SaveChangesAsync();

            return assignUserCommittee;
        }



        /// <summary>
        /// Update Branch Committee
        /// </summary>
        /// <param name="assign user committee"></param>
        /// <returns></returns>
        public async Task<AssignUserCommittee> UpdateAsync(AssignUserCommittee assignUserCommittee)
        {
            Check.ArgumentNotNull("assign user committee", assignUserCommittee);
            //ToDo Check for Validate
            //ToDo Check for Business

            _context.AssignUserCommittees.Update(assignUserCommittee);
            await _context.SaveChangesAsync();

            return assignUserCommittee;
        }


        /// <summary>
        /// DeActive Assign User Committee
        /// </summary>
        /// <param name="assign user committee"></param>
        /// <returns></returns>
        public async Task<bool> DeActiveAsync(int id)
        {
            //Check.ArgumentNotNull("committee", committee);
            //ToDo Check for Validate
            //ToDo Check for Business
            var assignUserCommittee = await _assignUserCommitteeServiceQueries.GetById(id);
            assignUserCommittee.DeActive();
            return await _context.SaveChangesAsync() == 1;
        }




        #endregion


    }
}
