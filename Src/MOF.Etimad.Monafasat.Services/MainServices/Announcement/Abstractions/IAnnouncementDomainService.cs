using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementDomainService
    {
        int GetUserId();
        string GetUserFullName();
        Task CreateVerificationCode(int id);
        Task<bool> CheckVerificationCode(int id, string verificationCode, int typeId);

        Task SendAnnouncementToApptovementNotification(Announcement announcement);
        Task ApproveAnnouncementNotification(Announcement announcement);
        Task RejectApproveAnnouncementNotification(Announcement announcement);
    }
}
