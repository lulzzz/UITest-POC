using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
  public  interface IBranchCommitteeService
    {

        Task<QueryResult<BranchCommittee>> Find(BranchCommitteeSearchCriteria criteria);
        Task<BranchCommittee> GetById(int id);
        Task<BranchCommittee> CreateAsync(BranchCommittee branchCommittee);
        Task<BranchCommittee> UpdateAsync(BranchCommittee branchCommittee);
        Task<bool> DeActiveAsync(int id);

    }
}
