using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IIDMQueries
    {
        Task<GovAgency> FindGovAgencyByIdAsync(string agencyCode);
        Task<UserProfile> FindUserProfileByIdAsync(int userProfileId);
        Task<UserProfile> FindUserProfileByUserNameAsync(string UserName);
        Task<Supplier> FindSupplierObjectByUserIdAsync(string cr);
        Task<SupplierUserProfile> FindSupplierUserProfileByUserProfileIdAndCrAsync(int userProfileId, string cr);
        Task<Supplier> FindSupplierByCrAsync(string cr);
        Task<List<GovAgencyModel>> GetAllAgencies();
        Task<List<LookupModel>> GetAllAgenciesList();
        Task<List<UserLookupModel>> FindUsersByRoleNameAndAgencyCodeAsync(string roleName, string agencyCode);
        Task<List<UserLookupModel>> GetAllDataEntryAndAuditorUsers(string roleName, string agencyCode);
        Task<List<UserLookupModel>> GetUserBasedOnlistOfRoleName(List<int> lstOfuserRoles, string agencyCode);
        Task<List<int>> FindUsersByRoleNameAndAgencyCodeAssignedToCommitteeIdAsync(string roleName, string agencyCode, int committeeId);
        Task<List<string>> GetAllSupplierOnTender(int tenderId);
        Task<List<string>> GetAllSupplierOnQualification(int tenderId);
        Task<List<string>> GetAllSupplierQualificationToSendInvitation(int tenderId);
        Task<List<int>> GetTechnicalCommitteeMembersOnTender(int committeeId);
        Task<QueryResult<AgencyExceptedModel>> GetAgencyExceptedModel(BlockSearchCriteriaModel criteria);
        Task<AgencyExceptedModel> GetAgencyExceptedById(string agencyId);
        Task<List<CommitteeModel>> FindCommitteesNotAssignedToBranch(string agencyCode, int branchId);
        Task<List<CommitteeModel>> FindCommitteeNotAssignedToUser(string agencyCode, int userId);
        Task<List<BranchModel>> FindBranchesNotAssignedToUser(string agencyCode, int userId);
        Task<List<CommitteeUserAssignModel>> FindCommitteeAssignedToUser(string agencyCode, int userId);
        Task<List<BranchUserAssignModel>> FindBranchesAssignedToUser(string agencyCode, int userId);
        Task<List<string>> FindUsersAssignedToBranch(UsersSearchCriteriaModel userSearchCriteriaModel);
        Task<List<BranchModel>> FindBranchesNotAssignedByUserAndRole(string agencyCode, int userId, string roleName);
        Task<List<int>> GetAssignedUsersByAgency(string agencyCode);
        Task<UserProfile> FindUserProfileByIdWithoutIncludesAsync(int userProfileId);
    }
}
