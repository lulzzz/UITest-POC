using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBranchAppService
    {
        Task<QueryResult<BranchModel>> Find(BranchSearchCriteriaModel criteria);
        Task<BranchModel> AddBranch(BranchModel branchModel);
        Task<BranchModel> FindById(int id);
        Task<CreateTenderBasicDataModel> FindInsertionTypeAndPurchaseMenthodsById(int id);
        Task<Branch> UpdateAsync(BranchModel branchModel);
        Task<bool> DeActiveAsync(int id);
        Task<List<int>> GetVROBranchsByVROOfficeCode(string VROOfficeCode);
        Task<QueryResult<BranchUserModel>> GetBranchUsers(BranchUserSearchCriteriaModel criteria);
        Task<BranchUser> GetBranchUsersForAwardingApproval(int userId, int branchId);
        Task RemoveAssignedUser(int userId, int branchId, int roleId);
        Task AddUserAsyn(BranchUserModel branchUserModel, string agencyCode);
        Task<BranchCommittee> GetAssignedCommitteeByIdAndBranch(int committeeId, int branchId);
        Task<QueryResult<LookupModel>> GetBranchCommittees(BranchCommitteeSearchCriteriaModel criteria);
        Task RemoveAssignedCommittee(int committeeId, int branchId);
        Task AddCommittee(BranchCommitteeModel branchCommitteeModel);
        Task<List<RoleModel>> GetUserRolesById(string userName, string agencyCode, int CommitteeTypeId, UsersSearchCriteriaModel searchCriteria);
        Task<int> GetUserDefultBranch(int userId, string agencyCode);
        Task<int> GetUserDefultCommittee(int userId, string agencyCode);
        Task<List<string>> GetUserRoleByCommittee(int userId, int committeeId);
        Task<List<string>> GetUserRolesByBranch(int userId, int branchId);
        Task<List<UserLookupModel>> GetIDMUsers(BranchUserSearchCriteriaModel criteria);
        Task<List<UserRolesModel>> GetAllUserRoles(int userId, string agencyCode);
        Task<List<LookupModel>> GetAllBranchesByAgencyCode(string agencyCode);
        Task<List<LookupModel>> GetAllBranchesNotAssignedToCommittee(int committeeId, int committeeType, string agencyCode);
        Task<List<BranchModel>> GetBranchByAgency(string agencyCode);
        Task RemoveAssignedUserForMang(int userId, int branchId, int roleId);
    }
}
