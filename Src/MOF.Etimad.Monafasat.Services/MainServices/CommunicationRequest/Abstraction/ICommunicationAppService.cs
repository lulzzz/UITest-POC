using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Negotiation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{

    public interface ICommunicationAppService
    {
        #region [Negotiotion]
        Task<AjaxResponse<OffersCompareViewModel>> DeleteNegotiationQitems(int tenderId, int negotiationId, int tableId, int rowNumber);
        Task<AjaxResponse<OffersCompareViewModel>> SaveNegotiationQitems(Dictionary<string, string> AuthorList);
        Task UpdateFirstStageNegotiation(NegotiationAgencyFirstStageEditModel negotiationModel);
        Task UpdateFirstStageNegotiationData(CreateNegotiationFirstStageDataModel negotiationModel);
        Task SendSecondFirstStageNegotiationToApprove(CreateNegotiationFirstStageDataModel negotiationModel);
        Task<bool> isAllowedToApplySecondStageNegotiation(int tenderId);
        Task CreateFirstStageNegotiation(NegotiationAgencyFirstStageEditModel negotiationModel);
        Task<NegotiationAgencyPageModel> GetNegotiationPageData(int negotiationId, int tenderId);
        Task<NegotiationAgencyPageModel> GetNegotiationDataFirstStage(int negotiationId, int tenderId);
        Task<List<NegotiationOfferModel>> GetOffersForNegotiation(int tenderId, decimal Discount);
        Task<List<NegotiationOfferModel>> PreviewOfferListDiscount(string TenderIdString, decimal ReductionAmount);
        Task<QueryResult<NegotiationOfferModel>> GetOffersListForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel);
        Task<QueryResult<NegotiationOfferModel>> GetOffersForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel);
        Task<QueryResult<NegotiationOfferModel>> GetNotNegotitedOffersForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel);
        Task<NegotiationAgencyFirstStageEditModel> GetNegotiationFirstStageDatabyId(string negotiationIdString);
        Task<string> CreateNegotiationRequest(StartNegotiationModel model);
        Task<SecondStageNegotiationModelcs> GetSecondStageNegotiation(int NegotiationId);
        Task<SecondStageNegotiationModelcs> GetSecondNegotiation(int NegotiationId);
        Task<SecondStageNegotiationModelcs> GetSecondNegotiation_NEW(int NegotiationId);
        Task<bool> SaveSecondStageNegotaion(SecondStageNegotiationModel NegotiationObj, List<NegotiationQuantityTableModel> QuantityTable, bool IsSend);
        #endregion

        #region Query

        Task<TenderPlaintDatailsModel> FindTenderForPlaintRequestById(int tenderId, string selectedCR);
        Task<TenderPlaintDatailsModel> FindTenderForEscalationRequestById(int tenderId, string selectedCR);
        #region Plaint 
        Task<QueryResult<EscalatedTendersModel>> GetEscalatedTenders(EscalationSearchCriteria searchCriteria);
        Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintRequestByTenderIdAndCR(int tenderId, string selectedCR);
        Task<PlaintRequestModel> FindPlaintRequestModelById(int plaintId, string agencyCode, int committeeId);
        Task<PlaintRequestModel> FindEscalationRequestModelById(int plaintId);
        Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintDetailsByPlaintIdAndCR(int agencyRequestId, string selectedCR);
        Task<QueryResult<TenderPlaintCheckingModel>> FindTenderPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria);
        Task<QueryResult<TenderPlaintCheckingModel>> FindTenderEscalatedPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria);
        Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsByRequestId(int communicationRequestId, string agencyId, int committeeID);
        Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsByTenderId(int agencyRequestId, string agencyId, int committeeId);
        Task<TenderEscalatedPLaintModel> FindAgencyCommunicationEscalatedPalintsByTenderId(int requestId);
        Task<AgencyCommunicationRequest> AcceptPlaint(string plaintId, int procedureId, string details, string agencyId, bool isEscalation, int committeeId);
        Task<AgencyCommunicationRequest> AcceptEscalationCommunicationRequest(int requestId, int procedureId, string details);
        Task<AgencyCommunicationRequest> RejectPlaint(string CommunicationRequestId, string rejectionReason, string agencyId, int committeeId, bool isEscalation);
        Task<AgencyCommunicationRequest> RejectEscalationCommunicationRequest(int requestId, string rejectionReason);
        Task<PlaintRequest> EscalatePlaint(string plaintId, string attachmentId, string attachmentName, string selectedCR);
        Task<AgencyCommunicationRequest> RejectPlaintCommunicationRequest(int requestId, string rejectionReason, string agencyId, int committeeId);
        Task ApprovePlaintCommunicationRequest(int requestId, string verficationCode, string agencyId, int committeeId, int typeId);
        Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsById(int Id, string agencyId, int committeeId, bool isEscalation);
        Task ApproveEscalationCommunicationRequest(int requestId, string verficationCode, string agencyId, int userId, int typeId);
        Task<AgencyCommunicationRequest> RejectEscalationCommunicationRequestApproval(int requestId, string rejectReason);
        #endregion

        Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerId);
        Task<List<PendingAgencyRequestAlarm>> GetPendingRequests(string CR);
        Task<AgencyCommunicationRequest> GetCommunicationRequestByRequestId(int communcicationRequestId);
        #endregion

        #region Command
        Task<EscalationRequest> FindEscalationByPlaintId(int plaintId, string agencyId);
        Task<PlaintRequest> FindPlaintById(int plaintId, string agencyId, int committeeId);
        Task<PlaintNotesModel> SavePlaintNotes(PlaintNotesModel model, string agencyId, int selectedCommittee);
        Task<PlaintRequestModel> SaveEscalationNotes(PlaintRequestModel model, string agencyId);
        Task<TenderPlaintCommunicationRequestModel> CreateCommunicationRequest(TenderPlaintCommunicationRequestModel tenderPlaintModel, string selectedCR);
        Task<List<NegotiationOfferModel>> PreviewOfferListDiscount(NegotiationAgencyFirstStageEditModel negotiationModel);
        #endregion

        #region Extend offer presentation dates request
        Task CreateSupplierExtendOfferDatesAgencyRequest(ExtendOffersDateRequestModel extendOffersDateRequestModel);
        Task<AgencyCommunicationRequest> ApproveSupplierExtendOfferDatesRequest(int SupplierExtendDatesAgencyCommunicationRequestId);
        Task<AgencyCommunicationRequest> RejectSupplierExtendOfferDatesRequest(int SupplierExtendDatesAgencyCommunicationRequestId);
        Task<SupplierExtendOfferDatesAgencyRequestModel> FindSupplierExtendOfferDatesAgencyRequestRequestById(int agencyRequestId);
        Task<List<SupplierExtendOfferDatesAgencyRequestModel>> FindSupplierExtendOfferDatesRequestsByTenderId(int tenderId);
        Task<ExtendOfferPresentationDatesModel> FindTenderExtendDatesByTenderId(int tenderId, int agencyRequestId);
        Task UpdateTenderExtendDates(ExtendOfferPresentationDatesModel tenderDatesModel, string userRole, string agencyCode);
        Task UpdateQualificationExtendDates(ExtendOfferPresentationDatesModel tenderDatesModel, string userRole, string agencyCode);
        #endregion

        #region Extend Offers Validity
        Task<ExtendOffersValidityModel> GetExtendOffersValidity(int agencyRequestId, int tenderId, int userId);
        Task<QueryResult<ExtendOffersGridModel>> GetTenderOffersPagingAsync(int tenderId, int extendOfferValidityId, int extendofferValidityStatusId);
        Task<ExtendOffersDisplayFilesModel> GetOfferToExtendbyId(int offerid, int userId);
        Task<OfferNegotiationFileModel> GetOfferFilesbyId(int offerid);
        Task<OfferDetailModel> GetOfferDetails(int combinedId, int userId);
        Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(int offerid, int userId);
        Task<string> AddExtendOffersValidity(ExtendOffersValiditySavingModel model);
        Task AcceptExtendOffersValidityBySupplier(int supplierExtendOffersValidtyId, string supplierCr, string supplierName);
        Task AddInitialGuaranteeAttachmentToEXV(ExtendOffersValidityAttachementViewModel extendOffersValidityAttachementViewModel, int supplierExtendOffersValidityId, string suplierCR, string supplierName);
        Task RejectExtendOffersValidityBySupplier(int extendOffersValidtyId, string supplierCr, string SupplierName);
        Task CancelSupplierExtendOfferValidity(int extendOffersValidtyId, string supplierCr);
        Task SendToApproveExtendOffersRequest(int agencyRequestId);
        Task RejectExtendOffersRequest(int AgencyRequestId, string RejectionReason);
        Task DeleteExtendOffersRequestAsync(int agencyRequestId);
        Task ApproveExtendOffersRequestAsync(int AgencyRequestId);
        Task<SupplierExtendOffersValidityViewModel> GetSupplierExtendOffersValidityForSupplier(int supplierExtendOffersValidtyId, string Cr);
        #endregion

        #region Tender Communication Request
        Task<QueryResult<CommunicationRequestGrid>> GetSuppliersCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria);
        Task<QueryResult<CommunicationRequestGrid>> GetAgencyCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria);
        #endregion

        Task<QueryResult<TableModel>> GetNegotiationTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
    }
}
