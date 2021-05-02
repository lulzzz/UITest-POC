using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementTemplateJoinRequestQueries
    {
        Task<AnnouncementJoinRequestSupplierTemplate> GetAnnouncementJoinRequestByRequestId(int joinRequestId);
        Task<AnnouncementJoinRequestSupplierTemplate> GetAnnouncementJoinRequestWithAttachmentsByRequestId(int joinRequestId);
        Task<AnnouncementJoinRequestSupplierTemplate> GetAnnouncementJoinRequestWithAttachmentsAndAnnouncementByRequestId(int joinRequestId);
        Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementJoinRequestByAnnouncementIdAndCR(int announcementId, string cr);
    }
}
