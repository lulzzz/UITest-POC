using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.User;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IIDMAppService
    {
        Task<List<LookupModel>> GetAllAgenciesList();
        Task<GovAgency> FindGovAgencyByIdAsync(string agencyCode);
        Task<List<GovAgencyModel>> GetALlAgencies();
        Task<UserProfile> FindUserProfileByUserNameAsync(string UserName);
        Task SetUserDefaultRole(string userRole);
        Task<LoggedUserModel> SyncUserInfo(int userId, string userName, string fullName, string mobile, string email, string supplierNumber, string supplierName, string agencyCode, List<string> userRoles, string relatedAgencyCode, List<string> vroUserRoles);
        Task<List<UserLookupModel>> FindUsersByRoleNameAndAgencyCodeForCommitteeAssignAsync(string roleName, string agencyCode);
        Task<List<UserLookupModel>> GetAllDataEntryAndAuditorUsers(string roleName, string agencyCode);
        Task<List<UserLookupModel>> FindUsersByRoleNameAndAgencyCodeNotAssignedToCommitteeAssignAsync(string roleName, string agencyCode, int committeeId, AgencyType agencyType = (int)AgencyType.None);
        Task<List<UserLookupModel>> GetUsersbyCommitteeTypeId(string agencyCode, int committeeId, int committeeTypeId, string email = "", string roleName = "", int agencyType = (int)AgencyType.None);
        Task<List<UserLookupModel>> GetUsersbyCommitteeTypeId(CommitteeUserSearchCriteriaModel CriteriaModel);
        Task<List<string>> GetAllSupplierOnTender(int tenderId);
        Task<List<string>> GetAllSupplierOnQualification(int tenderId);
        Task<List<string>> QualificationToSendInvitation(int tenderId);
        Task<SupplierFullDataModel> GetSupplierInfoByCR(string CR);
        Task<List<int>> GetTechnicalCommitteeMembersOnTender(int committeeId);
        Task<QueryResult<SupplierIntegrationModel>> GetSupplierDetailsBySearchCriteria(SupplierIntegrationSearchCriteria searchCriteria);
        Task<AgencyInfoModel> GetAgencyDetailsRelatedToSadad(string agencyCode, int agencyType);
        Task<Dictionary<string, string>> GetAgencyLogos(List<string> agencyCode);
        List<IDMRolesModel> GetIDMRoles();
        Task<QueryResult<AgencyExceptedModel>> GetAgencyExceptedModel(BlockSearchCriteriaModel blockSearchCriteriaModel);
        Task<AgencyExceptedModel> GetAgencyExceptedById(string agencyId);
        Task<List<UserLookupModel>> GetUserBasedOnlistOfRoleIds(List<int> lstOfuserRoles, string agencyCode);
        Task<List<CommitteeModel>> FindCommitteesNotAssignedToBranch(string agencyCode, int branchId);
        Task<UserProfile> GetUserProfileByEmployeeId(string userName, string agencyCode, Enums.UserRole userType);
        Task<List<CommitteeModel>> FindCommitteeNotAssignedToUser(string agencyCode, int userId);
        Task<List<BranchModel>> FindBranchesNotAssignedToUser(string agencyCode, int userId);
        Task<ManageUsersAssignationModel> GetMonafastatEmployeeDetailById(string agencyCode, string nationalId, string roleName = "", int agencyType = (int)AgencyType.None);
        Task<List<CommitteeUserAssignModel>> FindCommitteeAssignedToUser(string agencyCode, int userId);
        Task<List<BranchUserAssignModel>> FindBranchesAssignedToUser(string agencyCode, int userId);
        Task<QueryResult<UserCommitteeBranchesModel>> GetUserCommitteeBranchesModel(string agencyCode, BranchCommitteeUserSearchCriteriaModel searchCriteria);
        Task<QueryResult<ManageUsersAssignationModel>> GetMonafasatUsers(UsersSearchCriteriaModel userSearchCriteriaModel);
        Task<QueryResult<EmployeeIntegrationModel>> GetMonafasatUsersForBlockUserList(UsersSearchCriteriaModel userSearchCriteriaModel);
        Task<List<ContactOfficerModel>> GetContactOfficersByCR(List<string> CRs);
        Task<List<EmployeeIntegrationModel>> GetEmployeeDetailsByRoleName(string roleName);
        Task<List<BranchModel>> FindBranchesNotAssignedByUserAndRole(string agencyCode, int userId, string roleName);
        Task UpdateAgencyStatus(string agencyId, bool isExcepted);
        Task UpdateAgencyIsOld(string agencyId, bool isOld);
        Task<bool> SaveAgency(AgencyExceptedModel agencyExceptedModel);
    }
}
