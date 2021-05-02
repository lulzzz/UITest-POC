using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementAppService
    {
        #region Commands
        Task CreateNewAnnouncement(CreateAnnouncementModel announcementModel);
        #endregion Commands
        #region Queries
        Task<AnnouncementDetailsModel> FindAnnouncementDetailsByAnnouncementId(int announcementId);
        Task<LookupModel> GetAnnouncementNameByAnnouncementId(int announcementId);
        Task<SupplierAnnouncementDetailsModel> GetAnnouncementDetailsForSupplierByAnnouncementId(int announcementId);
        Task<bool> JoinAnnouncement(int announcementId);
        Task<bool> WithdrawJoinRequest(int announcementId);
        Task<QueryResult<SupplierGridAnnouncementModel>> GetSupplierAnnouncements(SupplierAnnouncementSearchCriteria cretria);
        Task<CreateAnnouncementModel> GetAnnouncementByIdForEdit(int announcementId);
        Task CreateVerificationCode(CreateVerificationCodeModel createVerification);
        Task SendAnnouncementForApproval(int announcementId);
        Task<ApproveAnnouncemntModel> ApproveAnnouncement(VerificationModel verificationModel);
        Task RejectApproveAnnouncement(RejectionReasonModel rejectionReasonModel);
        Task ReOpenAnnouncement(int announcementId);
        Task<QueryResult<AllAnouuncementsForSupplierVisitorModel>> GetAllSupplierAnnouncements(AllSupplierAnnouncementSearchCriteria criteria);
        Task<QueryResult<AllAnouuncementsForAgencyModel>> GetAllAgencyAnnouncements(AllAgencyAnnouncementSearchCriteriaModel criteria);
        Task<QueryResult<UnderOperationAnouuncementsForAgencyModel>> GetUnderOperationAgencyAnnouncements(UnderOperationAgencyAnnouncementSearchCriteria criteria);
        Task<List<TenderTypeModel>> GetTenderTypes();

        #endregion Queries

        Task DeleteAnnouncement(int announcementId);
    }
}
