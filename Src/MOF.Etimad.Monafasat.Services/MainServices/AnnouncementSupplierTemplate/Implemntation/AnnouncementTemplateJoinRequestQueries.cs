using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Linq;
using System.Threading.Tasks;


namespace MOF.Etimad.Monafasat.Services
{

    public class AnnouncementTemplateJoinRequestQueries : IAnnouncementTemplateJoinRequestQueries
    {
        private readonly IAppDbContext _context;
        public AnnouncementTemplateJoinRequestQueries(IAppDbContext context)
        {
            _context = context;
        }


        public async Task<AnnouncementJoinRequestSupplierTemplate> GetAnnouncementJoinRequestByRequestId(int joinRequestId)
        {
            var request = await _context.AnnouncementRequestSupplierTemplate
                .Where(r => r.Id == joinRequestId && r.IsActive == true).FirstOrDefaultAsync();
            return request;
        }
        public async Task<AnnouncementJoinRequestSupplierTemplate> GetAnnouncementJoinRequestWithAttachmentsByRequestId(int joinRequestId)
        {
            var request = await _context.AnnouncementRequestSupplierTemplate
                .Include(r => r.Attachments)
                .Where(r => r.Id == joinRequestId && r.IsActive == true).FirstOrDefaultAsync();
            return request;
        }
        public async Task<AnnouncementJoinRequestSupplierTemplate> GetAnnouncementJoinRequestWithAttachmentsAndAnnouncementByRequestId(int joinRequestId)
        {
            var request = await _context.AnnouncementRequestSupplierTemplate
                .Include(r => r.AnnouncementSupplierTemplate)
                .Include(r => r.Attachments)
                .Where(r => r.Id == joinRequestId && r.IsActive == true).FirstOrDefaultAsync();
            return request;
        }

        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementJoinRequestByAnnouncementIdAndCR(int announcementId, string cr)
        {
            var request = await _context.AnnouncementRequestSupplierTemplate
                .Where(r => r.AnnouncementId == announcementId && r.Cr == cr && r.IsActive == true)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new AnnouncementSuppliersTemplateJoinRequestsDetailsModel
                {
                    JoinRequestStatusId = r.StatusId,
                    JoinRequestStatusName = r.JoinRequestStatus.NameAr,
                    JoinRequestIdString = Util.Encrypt(r.Id),
                    JoinRequestSendingDate = r.CreatedAt.Date.ToHijriDateWithFormat("yyyy-MM-dd"),
                    WithdrawalDate = r.JoinRequestHistories.Where(h => h.JoinRequestStatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn).OrderByDescending(x => x.CreatedAt).FirstOrDefault().CreatedAt.Date.ToHijriDateWithFormat("yyyy-MM-dd"),
                    RejectionReason = !string.IsNullOrEmpty(r.RejectionReason) && r.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected ? r.RejectionReason : Resources.SharedResources.DisplayInputs.NotExist,
                    DeleteReason = !string.IsNullOrEmpty(r.DeleteReason) && r.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted ? r.DeleteReason : Resources.SharedResources.DisplayInputs.NotExist,
                    TemplateExtendMechanism = !string.IsNullOrEmpty(r.AnnouncementSupplierTemplate.TemplateExtendMechanism) ? r.AnnouncementSupplierTemplate.TemplateExtendMechanism : Resources.SharedResources.DisplayInputs.NotExist,
                    StopUsingAnnouncemetMechanism = !string.IsNullOrEmpty(r.AnnouncementSupplierTemplate.CancelationReason) ? r.AnnouncementSupplierTemplate.CancelationReason : Resources.SharedResources.DisplayInputs.NotExist
                }).FirstOrDefaultAsync();

            return request;
        }

    }
}
