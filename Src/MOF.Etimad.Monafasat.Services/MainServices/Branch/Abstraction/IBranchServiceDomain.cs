using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IBranchServiceDomain
    {
        Task<bool> CheckUserExist(int userId, int branchId, string roleName);
        Task<bool> CheckCommitteeExist(int comitteeId, int branchId);
        Task<bool> CheckBranchExist(string branchName, string agecyCode);
        void AssignBranchUserExist(int branchId, string roleName, UserProfile user);
        Task<bool> CheckTenderExist(int branchId);
    }
}
