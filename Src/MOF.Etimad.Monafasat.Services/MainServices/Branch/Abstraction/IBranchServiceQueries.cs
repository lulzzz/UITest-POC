using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBranchServiceQueries
    {
        Task<CreateTenderBasicDataModel> FindInsertionTypeAndPurchaseMenthodsById(int id);
        Task<QueryResult<BranchModel>> Find(BranchSearchCriteriaModel criteria);
        Task<BranchUser> GetAssignedBranchUsers(int userId, int branchId, string roleName);
        Task<QueryResult<BranchUserModel>> GetBranchUsers(BranchUserSearchCriteriaModel criteria);
        Task<List<BranchUserModel>> GetBranchUsersHasEstimateValue(int userId, int branchId);
        Task<BranchUser> GetBranchUsersForAwardingApproval(int userId, int branchId);
        Task<BranchModel> FindById(int id);
        Task<Branch> FindBranchById(int id);
        Task<List<int>> GetVROBranchsByVROOfficeCode(string VROOfficeCode);
        Task<QueryResult<LookupModel>> GetBranchCommittees(BranchCommitteeSearchCriteriaModel criteria);
        Task<BranchCommittee> GetAssignedCommitteeByIdAndBranch(int committeeId, int branchId);
        Task<BranchModel> GetBranchByName(string branchName, string agecyCode);
        Task<int> GetUserDefultBranch(int userId, string agencyCode);
        Task<int> GetUserDefultCommittees(int userId, string agencyCode);
        Task<List<string>> GetUserRoleByCommittee(int committeeId, int userId);
        Task<List<string>> GetUserRolesByBranch(int userId, int branchId);
        Task<List<string>> GetUserRolesByBranchFromCommittees(int userId, int branchId);
        Task<List<UserRolesModel>> GetAllUserRolesFromBranches(int userId, string agencyCode);
        Task<List<UserRolesModel>> GetAllUserRolesFromCommittees(int userId, string agencyCode);
        Task<List<LookupModel>> GetAllBranchesByAgencyCode(string agencyCode);
        Task<List<LookupModel>> GetAllBranchesNotAssignedToCommittee(int committeeId, int committeeType, string agencyCode);
        Task<List<BranchModel>> GetBranchByAgency(string agencyCode);
        Task<GovAgency> GetAgencyCodeByBranchId(int id);
        Task<bool> GetHasTendersByBranch(int branchId);
        Task<BranchUser> GetAssignedUserForMangById(int userId, int branchId, int roleId);
    }
}
