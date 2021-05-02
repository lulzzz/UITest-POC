using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommunicationQueries
    {
        #region Tender Communication Request
        Task<QueryResult<CommunicationRequestGrid>> GetSuppliersCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria);
        Task<QueryResult<CommunicationRequestGrid>> GetAgencyCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria);
        #endregion
        #region [Negotiation]
        Task<NegotiationAgencyFirstStageEditModel> GetNegotiationDatabyId(int tenderId, string CR);
        Task<CreateNegotiationFirstStageDataModel> GetNegotiationDataForFirstStagebyId(int negotiationFirstStageId);
        Task<NegotiationSecondStage> GetNegotiationSecondStageTenderId(int TenderId);

        #endregion

        #region Extend Offers Validity
        Task<ExtendOffersValidityModel> GetExtendOffersValidity(int agencyRequestId, int userId);
        Task<ExtendOffersValidity> GetExtendOffersValidityForAddInitialGuaranteeById(int extendOfferValidityId);
        Task<QueryResult<ExtendOffersGridModel>> GetTenderOffersPagingAsync(int tenderId, int extendOfferValidityId);
        Task<QueryResult<ExtendOffersGridModel>> GetOffersForExtendOfferValidityCreation(int tenderId);
        Task<ExtendOffersDisplayFilesModel> GetOfferToExtendbyId(int offerid, int userId);
        Task<OfferNegotiationFileModel> GetOfferFilesbyId(int offerid);
        Task<OfferDetailModel> GetOfferDetails(int combinedSupplierId);
        Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(int offerid, int userId);
        Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerid);
        Task<ExtendOffersValiditySupplier> GetExtendOffersValiditySupplierById(int extendOfferValidityId, string supplierCr);
        Task<AgencyCommunicationRequest> FindAgencyCommunicationRequestById(int requestId);
        Task<AgencyCommunicationRequest> FindAgencyCommunicationRequestByIdForSendToApproval(int requestId);
        Task<AgencyCommunicationRequest> FindAgencyCommunicationRequestByIdForApproval(int requestId);
        Task<AgencyCommunicationRequest> FindAgencyCommunicationRequestByIdForDelete(int requestId);
        Task<TenderCommunicationRequestModel> GetTenderHasValidExtendOffersValidity(int tenderId);
        Task<int> GetExtendOffersValidityDaysByTenderId(int tenderId);
        Task<TenderBasicInfoModel> GetTenderBasicInfoModel(int tenderId);
        Task<AgencyCommunicationRequest> GetAgencyCommunicationRequestById(int requestId);
        Task<List<Offer>> GetOffersToSendExtendOfferValidityByTenderId(int tenderId);
        Task<Tender> GetTenderByAgencyRequestId(int agencyRequestId);
        Task<SupplierExtendOffersValidityViewModel> GetSupplierExtendOffersValidityForSupplierViewModel(int agencyCommunicationRequestId, string Cr);
        Task<ExtendOffersValidityAttachementViewModel> GetExtendOffersValidityAttachmentModel(int extendOffersValiditySupplierId);
        Task<ExtendOffersValiditySupplier> GetSupplierExtendOffersValidityForSupplier(int agencyCommunicationRequestId, string Cr);
        Task<ExtendOffersValiditySupplier> GetSupplierExtendOffersValidityForSupplierByExtendOffersValiditySupplierId(int extendOffersValiditySupplierId, string Cr);
        Task<AgencyCommunicationRequest> GetAgencyCommunicationRequestForApproval(int requestId);
        Task<AgencyCommunicationRequest> GetAgencyCommunicationRequestForReject(int requestId);
        #endregion

        #region Plaint 
        Task<QueryResult<EscalatedTendersModel>> GetEscalatedTenders(EscalationSearchCriteria searchCriteria);
        Task<TenderPlaintDatailsModel> FindTenderForPlaintRequestById(int tenderId, string selectedCR);
        Task<TenderPlaintDatailsModel> FindTenderForEscalationRequestById(int tenderId, string selectedCR);
        Task<Tender> FindTenderWithPlaintRequestByTenderId(int tenderId, string selectedCR);
        Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintRequestByTenderIdAndCR(int tenderId, string selectedCR);
        Task<PlaintRequest> FindPlaintById(int plaintId, string agencyId, int committeeId);
        Task<EscalationRequest> FindEscalationByPlaintId(int plaintId, string agencyId);
        Task<QueryResult<TenderPlaintCheckingModel>> FindTenderPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria);
        Task<QueryResult<TenderPlaintCheckingModel>> FindEscalatedTenderPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria);
        Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsByTenderId(int agencyRequestId, string agencyId, int committeeId);
        Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsByRequestId(int agencyCommunicationId, string agencyId, int committeeId);
        Task<TenderEscalatedPLaintModel> FindAgencyCommunicationEscalatedPalintsByTenderId(int requestId);

        Task<PlaintRequestModel> FindPlaintRequestModelById(int plaintId, string agencyCode, int committeeId);
        Task<PlaintRequestModel> FindEscalationRequestModelById(int plaintId);
        Task<List<PlaintRequest>> GetAllPlaintRequestsByRequestId(int id);
        Task<AgencyCommunicationRequest> FindCommunicationRequestByIdForPlaint(int requestId);
        Task<AgencyCommunicationRequest> FindCommunicationRequestByIdForApprovePlaint(int requestId);
        Task<AgencyCommunicationRequest> FindCommunicationRequestByIdForRejectPlaint(int requestId);

        Task<SupplierExtendOfferDatesRequest> FindCommunicationRequestByTenderIdAndCr(int tenderId, string Cr);
        Task<AgencyCommunicationRequest> FindEscalationRequestById(int requestId);
        Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsById(int Id, string agencyId, int committeeId, bool isEscalation);

        Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintDetailsByPlaintIdAndCR(int agencyRequestId, string selectedCR);
        Task<PlaintRequest> FindPlaintRequestToEscalateById(int plaintId, string selectedCR);
        #endregion

        #region Extend offer presentation dates request
        Task<SupplierExtendOfferDatesAgencyRequestModel> FindSupplierExtendOfferDatesRequestById(int id);
        Task<List<SupplierExtendOfferDatesAgencyRequestModel>> FindSupplierExtendOfferDatesRequestsByTenderId(int tenderId);
        Task<AgencyCommunicationRequest> GetAgencyCommunicationRequestForSupplierExtendDatesRequest(int agencyRequestId);


        #endregion

        #region[Negotiation]
        Task<NegotiationFirstStage> GetLastNegotiationFirstStageWithSupplierByTenderId(int tenderId);
        Task<NegotiationFirstStage> GetLastAgreedNegotiationFirstStageByTenderId(int tenderId);
        Task<NegotiationAgencyFirstStageEditModel> GetNegotiationFirstStageDatabyId(int negotiationFirstStageId);
        #endregion
        Task<bool> IsFirstStageNegotiationExist(int tenderId);

        Task<List<string>> FindSuppliersThatRejectExtendOfferValidity(int tenderId);
        Task<List<string>> FindSuppliersThatNotResponseToExtendOfferValidity(int tenderId);
        Task<List<string>> FindSuppliersThatAcceptInitialyExtendOfferValidity(int tenderId);
        Task<bool> CanAddPostqualificationIfTenderHasExtendOfferValidity(int tenderId);
        Task<List<AgencyCommunicationRequest>> GetCommunicationRequestWithNegotiationAndExtendOffersValdityByTenderId(int tenderId);
        Task<List<ExtendOffersValiditySupplier>> FindExtendOfferValidtySupplier(int extendOfferValidityId);
        Task<bool> IsTenderHasActiveExtendOfferValidityRequests(int tenderId);

    }
}
