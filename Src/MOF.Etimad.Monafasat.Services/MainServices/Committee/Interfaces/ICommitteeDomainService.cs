using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommitteeDomainService
    {
        Task<bool> CheckNameExist(string name, string agentId, int id = 0);
        Task<bool> CheckNameExistbyType(string name, string agentId, int typeId = 0);
        Task<bool> CheckUserExist(int userId, int committeeId);
        Task<bool> CheckIfHasTenders(int committeeId);
        Task<bool> CheckIfHasEnqiryReplies(int committeeId);
        Task<bool> CheckIfHasUsers(int committeeId);
    }
}
