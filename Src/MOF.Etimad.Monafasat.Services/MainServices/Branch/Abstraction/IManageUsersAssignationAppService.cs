using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IManageUsersAssignationAppService
    {
        Task AddUserToBranchAsyn(BranchUserAssignModel branchUserModel, string agencyCode);
        Task AddUserToCommitteeAsyn(CommitteeUserAssignModel committeeUserModel, string agencyCode);
        Task UserPermissionRest(int UserId);
        List<RoleModel> GetIDMRoles();
    }
}
