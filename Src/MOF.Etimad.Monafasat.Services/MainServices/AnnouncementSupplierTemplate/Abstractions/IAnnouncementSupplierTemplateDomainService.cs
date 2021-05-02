using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementSupplierTemplateDomainService
    {
        int GetUserId();
        string GetUserFullName();
        Task CreateVerificationCode(int id);
        Task<bool> CheckVerificationCode(int id, string verificationCode, int typeId);
    }
}
