using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
  public  interface IAssignUserCommitteeServiceQueries
    {
        Task<List<AssignUserCommittee>> Find(AssignUserCommitteeSearchCriteria criteria);
        Task<AssignUserCommittee> GetById(int id);
    }
}
