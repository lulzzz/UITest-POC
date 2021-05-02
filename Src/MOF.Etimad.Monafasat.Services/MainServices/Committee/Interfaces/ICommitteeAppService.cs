using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommitteeAppService
    {
        Task<QueryResult<Committee>> Find(CommitteeSearchCriteria criteria);
        Task<List<Committee>> FindAll(string agencyCode);
        Task<Committee> GetById(int id);
        Task<Committee> CreateAsync(Committee committee);
        Task<Committee> UpdateAsync(CommitteeModel committeeModel);
        Task DeActiveAsync(int committeeId);
        Task<List<Committee>> FindByCommitteeTypeId(int committeeTypeId, string agencyCode);
        Task<QueryResult<CommitteeUserViewModel>> GetCommitteeUsers(CommitteeUserSearchCriteriaModel criteria);
        Task AddUserAsyn(CommitteeUserModel committeeUserModel, string agencyCode);
        Task RemoveAssignedUser(int userId, int commiteeId);
    }
}
