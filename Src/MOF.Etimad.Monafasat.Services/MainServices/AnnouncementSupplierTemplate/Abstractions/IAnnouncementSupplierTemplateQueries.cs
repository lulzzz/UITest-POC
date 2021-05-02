using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{

    public interface IAnnouncementSupplierTemplateQueries
    {
        Task<QueryResult<AllAnnouncementSupplierTemplateAgencyModel>> FindAllAnnouncementSupplierTemplates(AgencyAnnouncementSupplierTemplateSearchCriteria criteria);
        Task<QueryResult<AnnouncementSupplierTemplateSupplierGridModel>> GetAllAnnouncementSupplierTemplatesForSupplier(AnnouncementSupplierTemplateSupplierSearchCriteriaModel criteria);
        Task<QueryResult<JoinRequestModel>> GetJoinRequestsByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria);
        Task<QueryResult<JoinRequestModel>> GetApprovedSuppliersJoinRequestsByAnnouncementId(AnnouncementSupplierTemplateBasicSearchCriteria criteria);
        Task<List<TenderTypeModel>> GetTenderTypes();
        Task<AnnouncementSupplierTemplate> GetAnnouncementByIdForCreationStep(int announcementId);
        Task<ApproveAnnouncemntSupplierTemplateModel> GetAnnouncementSupplierTemplateDetailsForApproval(int announcementId);
        Task<AnnouncementSuppliersTemplateDetailsModel> FindAnnouncementDetailsByAnnouncementId(int announcementId);
        Task<AnnouncementTemplateDetailsForPrintModel> GetAnnouncementDetailsByAnnouncementIdForPrint(int announcementId);
        Task<AnnouncementTemplateDetailsForSupplierForPrintModel> GetAnnouncementDetailsForSupplierForPrint(int announcementId, string cr);
        Task<AnnouncementBasicDetailModel> FindAnnouncementBasicDetailsByAnnouncementId(int announcementId);
        Task<AnnouncementTemplateListDetailsModel> FindAnnouncementTemplateListDetailsByAnnouncementId(int announcementId);

        Task<AnnouncementDescriptionModel> FindAnnouncementDescriptionDetailsByAnnouncementId(int announcementId);
        Task<AnnouncementTemplateActivityAndAreaDetailsModel> GetAnnouncementTemplateActivityAndAddressDetails(int announcementId);
        Task<AnnouncementTemplateMainDetailsModel> GetAnnouncementTemplateDetailsForSuppliers(int announcementId, string cr);
        Task<AddPublicListToAgencyAnnouncementListsModel> GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(int announcementId, string agencyCode);
        Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetails(int joinRequestId, string cr);
        Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(int announcementId, string agencyCode, string userRole);
        Task<AnnouncementSupplierTemplate> GetAnnouncementWithNoIncludsById(int announcementId);
        Task<AnnouncementSupplierTemplate> GetAnnouncementWithJoinRequestsById(int announcementId);
        Task<AnnouncementSupplierTemplate> GetAnnouncementWithLinkedAgencyById(int announcementId);
        Task<CreateAnnouncementSupplierTemplateModel> GetAnnouncementByIdForEdit(int announcementId);
        Task<UpdateAnnouncementSupplierTemplateModel> GetAnnouncementByIdForEditApproval(int announcementId);
        Task<QueryResult<TenderAnnouncementSuppliersTemplateDetails>> GetTendersByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria);
        Task<List<string>> GetJoinedAnnouncementSuppliers(int announcementId);
        Task<AnnouncementSuppliersTemplateCancelModel> FindAnnouncementDetailsByAnnouncementIdForCancelation(int announcementId);
        Task<List<string>> GetAcceptedAnnouncementSuppliers(int announcementId);
        Task<ExtendAnnouncementSupplierTemplateModel> GetAnnouncementByIdForExtendAnnouncement(int announcementId);
        Task<QueryResult<JoinRequestModel>> GetJoinRequestsSuppliersByAnnouncementIdAsync(JoinRequestSuppliersSearchCriteria criteria);
        Task<QueryResult<LinkedAgenciesAnnouncementTemplateModel>> GetBeneficiaryAgencyByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria);

    }
}
