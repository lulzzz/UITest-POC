using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
  public  interface IAssignUserCommitteeService
    {

        Task<List<AssignUserCommittee>> Find(AssignUserCommitteeSearchCriteria criteria);
        Task<AssignUserCommittee> GetById(int id);
        Task<AssignUserCommittee> CreateAsync(AssignUserCommittee assignUserCommittee);
        Task<AssignUserCommittee> UpdateAsync(AssignUserCommittee assignUserCommittee);
        Task<bool> DeActiveAsync(int id);

    }
}
