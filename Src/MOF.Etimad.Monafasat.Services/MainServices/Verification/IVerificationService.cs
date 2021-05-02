using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IVerificationService
    {
        Task<bool> CheckForVerificationCode(int tenderId, string verificationCodeString, int typeId);
        Task CreateVerificationCode(int codeTypeId, string userMobile, string userEmail, int userID, int typeId);
    }
}
