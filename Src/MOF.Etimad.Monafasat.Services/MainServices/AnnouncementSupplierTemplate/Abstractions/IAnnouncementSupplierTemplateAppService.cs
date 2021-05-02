using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IAnnouncementSupplierTemplateAppService
    {
        Task<QueryResult<AllAnnouncementSupplierTemplateAgencyModel>> GetAllAnnouncementSupplierTemplates(AgencyAnnouncementSupplierTemplateSearchCriteria criteria);
        Task<QueryResult<AnnouncementSupplierTemplateSupplierGridModel>> GetAllAnnouncementSupplierTemplatesForSupplier(AnnouncementSupplierTemplateSupplierSearchCriteriaModel criteria);
        Task<QueryResult<JoinRequestModel>> GetJoinRequestsByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria);
        Task<QueryResult<JoinRequestModel>> GetApprovedSuppliersJoinRequestsByAnnouncementId(AnnouncementSupplierTemplateBasicSearchCriteria criteria);
        Task<CreateAnnouncementSupplierTemplateModel> CreateNewAnnouncementSupplierTemplate(CreateAnnouncementSupplierTemplateModel announcementModel);
        Task<AnnouncementTemplateMainDetailsModel> SendJoinRequestToAnnouncment(AnnouncementTemplateMainDetailsModel announcementModel);
        Task<CreateAnnouncementSupplierTemplateModel> SaveDraft(CreateAnnouncementSupplierTemplateModel announcementModel);
        Task<ApproveAnnouncemntSupplierTemplateModel> GetAnnouncementSupplierTemplateDetailsByannouncementId(int announcementId);
        Task CreateVerificationCode(CreateVerificationCodeModel createVerification);
        Task DeleteAnnouncement(int announcementId);
        Task<AnnouncementSuppliersTemplateDetailsModel> FindAnnouncementDetailsByAnnouncementId(int announcementId);
        Task<AnnouncementBasicDetailModel> GetAnnouncementBasicDetailsByAnnouncementId(int announcementId);
        Task<AnnouncementTemplateListDetailsModel> GetAnnouncementTemplateListDetailsByAnnouncementId(int announcementId);
        Task<AnnouncementDescriptionModel> GetAnnouncementDescriptionDetailsByAnnouncementId(int announcementId);
        Task<AnnouncementTemplateActivityAndAreaDetailsModel> GetAnnouncementTemplateActivityAndAddressDetails(int announcementId);
        Task<AnnouncementSuppliersTemplateDetailsModel> GettAnnouncementTemplateDetails(int announcementId);
        Task<AddPublicListToAgencyAnnouncementListsModel> GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(int announcementId, string agencyCode);
        Task<AnnouncementTemplateMainDetailsModel> GetAnnouncementTemplateDetailsForSuppliers(int announcementId, string cr);
        Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetails(int joinRequestId, string cr);
        Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(int announcementId, string agencyCode, string userRole);
        Task<ApproveAnnouncemntSupplierTemplateModel> ApproveAnnouncement(VerificationModel verificationModel);
        Task<CreateAnnouncementSupplierTemplateModel> GetAnnouncementByIdForEdit(int announcementId);
        Task<UpdateAnnouncementSupplierTemplateModel> GetAnnouncementByIdForEditApproval(int announcementId);
        Task<UpdateAnnouncementSupplierTemplateModel> UpdateAnnouncementSupplierTemplateList(UpdateAnnouncementSupplierTemplateModel announcementModel);
        Task<QueryResult<TenderAnnouncementSuppliersTemplateDetails>> GetTendersByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria);
        Task<AnnouncementTemplateMainDetailsModel> AnnouncementFinalApprove(AnnouncementFinalApprovalModel approvalModel);
        Task<AnnouncementSuppliersTemplateCancelModel> CancelAnnouncement(AnnouncementSuppliersTemplateCancelModel cancelModel);
        Task<AnnouncementSuppliersTemplateCancelModel> FindAnnouncementDetailsByAnnouncementIdForCancelation(int announcementId);

        Task<ExtendAnnouncementSupplierTemplateModel> ExtendAnnouncementTemplate(ExtendAnnouncementSupplierTemplateModel announcementModel);

        Task<ExtendAnnouncementSupplierTemplateModel> GetAnnouncementByIdForExtend(int announcementId);
        Task AddPublicAnnouncementListToAgency(int announcementId, string agencyCode);
        Task RemovePublicAnnouncementListFromAgency(int announcementId, string agencyCode);
        Task<QueryResult<JoinRequestModel>> GetJoinRequestsSuppliersByAnnouncementIdAsync(JoinRequestSuppliersSearchCriteria criteria);
        Task<QueryResult<LinkedAgenciesAnnouncementTemplateModel>> GetBeneficiaryAgencyByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria);

        Task<AnnouncementTemplateDetailsForPrintModel> GetAnnouncementDetailsByAnnouncementIdForPrint(int announcementId);
        Task<AnnouncementTemplateDetailsForSupplierForPrintModel> GetAnnouncementDetailsForSupplierForPrint(int announcementId, string cr);
    }
}
