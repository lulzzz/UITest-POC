using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public class BranchServiceDomain : IBranchServiceDomain
    {
        private readonly IBranchServiceQueries _branchQueries;

        public BranchServiceDomain(IBranchServiceQueries branchQueries)
        {
            _branchQueries = branchQueries;
        }
        public async Task<bool> CheckBranchExist(string branchName, string agecyCode)
        {
            var result = await _branchQueries.GetBranchByName(branchName, agecyCode);
            if (result != null)
                throw new BusinessRuleException(Resources.BranchResources.ErrorMessages.BranchAlreadyExist);
            return true;
        }

        public async Task<bool> CheckCommitteeExist(int comitteeId, int branchId)
        {
            var result = await _branchQueries.GetAssignedCommitteeByIdAndBranch(comitteeId, branchId);
            if (result != null)
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.CommitteeNameExist);
            return true;
        }

        public void AssignBranchUserExist(int branchId, string roleName, UserProfile user)
        {
            Enums.UserRole choice;
            if (Enum.TryParse(roleName, out choice))
            {
                uint value = (uint)choice;
            }
            int roleId = (int)choice;

            if (user.BranchUsers.Any(b => b.IsActive == true && b.BranchId == branchId &&
            (
              (b.UserRoleId == (int)Enums.UserRole.NewMonafasat_ApproveTenderAward && roleId == (int)Enums.UserRole.NewMonafasat_ApproveTenderAward)
              ||
              ((b.UserRoleId == (int)Enums.UserRole.NewMonafasat_DataEntry || b.UserRoleId == (int)Enums.UserRole.NewMonafasat_Auditer) && (roleId == (int)Enums.UserRole.NewMonafasat_DataEntry || roleId == (int)Enums.UserRole.NewMonafasat_Auditer))
              ||
              ((b.UserRoleId == (int)Enums.UserRole.NewMonafasat_PlanningApprover || b.UserRoleId == (int)Enums.UserRole.NewMonafasat_PlanningOfficer) && (roleId == (int)Enums.UserRole.NewMonafasat_PlanningApprover || roleId == (int)Enums.UserRole.NewMonafasat_PlanningOfficer))
              ||
                ((b.UserRoleId == (int)Enums.UserRole.NewMonafasat_PurshaseSpecialist || b.UserRoleId == (int)Enums.UserRole.NewMonafasat_EtimadOfficer) && (roleId == (int)Enums.UserRole.NewMonafasat_PurshaseSpecialist || roleId == (int)Enums.UserRole.NewMonafasat_EtimadOfficer))
              )
            )
            )
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.UserAlreadyExist);
        }

        public async Task<bool> CheckTenderExist(int branchId)
        {
            var result = await _branchQueries.GetHasTendersByBranch(branchId);
            if (result)
                throw new BusinessRuleException(Resources.BranchResources.ErrorMessages.BranchHasTenders);
            return result;
        }
        public async Task<bool> CheckUserExist(int userId, int branchId, string roleName)
        {
            var result = await _branchQueries.GetAssignedBranchUsers(userId, branchId, roleName);
            if (result != null)
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.UserAlreadyExist);
            return true;
        }
    }
}
