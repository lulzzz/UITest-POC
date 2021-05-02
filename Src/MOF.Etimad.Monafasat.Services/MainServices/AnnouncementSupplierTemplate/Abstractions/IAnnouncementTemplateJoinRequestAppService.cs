using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementTemplateJoinRequestAppService
    {

        Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> SaveJoinRequestResult(AnnouncementSuppliersTemplateJoinRequestsDetailsModel joinRequestModel);
        Task WithdrawFromAnnouncementTemplate(int userId, int joinRequestId);
        Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementJoinRequestDetails(int announcementId, string cr);
        Task DeleteSupplierFromAnnouncementTemplate(DeleteSupplierFromAnnouncementModel deleteModel);

    }
}