using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Invitation;
using MOF.Etimad.Monafasat.ViewModel.Negotiation;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.Monafasat.ViewModel.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IOfferQueries
    {
        Task<Offer> GetOfferWithQuantityItems(int OfferId);
        Task<Offer> GetOfferByOfferId(int offerId);
        Task<List<TenderQuantityItemDTO>> GetSupplierQTableItemsForOpening(int offerId);
        Task<List<TenderQuantityItemDTO>> GetNegotiationQTableItems(int offerId);
        Task<QuantitiesTemplateModel> GetOfferQuantityItemsForApplyOffer(int offerId);
        Task<OfferDetailModel> GetTenderStatus(int tenderId);
        Task<Tender> GetTenderbyTenderIdAsync(int TenderId);
        Task<QueryResult<NegotiationOfferModel>> GetOffersListForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel);
        Task<QueryResult<NegotiationOfferModel>> GetOffersForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel);
        Task<QueryResult<NegotiationOfferModel>> GetNotNegotitedOffersForFirstStageNegotiation(NegotiationOffersSearchModel negotiationOffersSearchModel);
        Task<List<NegotiationOfferModel>> GetOffersForNegotiationByTenderId(int TenderId, decimal Discount);
        Task<List<Offer>> GetOffersForSecondNegotiationByTenderId(int tenderId);
        Task<List<NegotiationAgencySupplierOfferList>> GetOffersForFirstStageNegotiationByTenderId(int TenderId);
        Task<List<NegotiationAgencySupplierOfferList>> GetAcceptedOffersForFirstStageNegotiationByTenderId(int TenderId);
        Task<List<Offer>> GetNotNegotiatedOffersForFirstStageNegotiationByTenderId(int tenderId);
        Task<Offer> GetOfferById(int offerId);
        Task<List<OfferSolidarity>> GetOtherOfferSolidarity(int offerId, int tenderid);
        Task<Offer> FindOfferByOfferId(int offerId);
        Task<Offer> GetOfferWithLocalContentDetailsById(int offerId);
        Task<Offer> GetOfferWithQTById(int offerId);
        Task<Offer> GetOfferWithQTByIdForOpenningDetails(int offerId);
        Task<Offer> GetOfferForDirectPurchaseChekingById(int offerId);
        Task<Offer> GetOfferWithQuantityTablesForApplyOfferById(int offerId);
        Task<QueryResult<Offer>> FindOffers(OfferSearchCriteria criteria);
        Task<Offer> FindOfferById(int offerId);
        Task<Offer> GetOfferWithTenderDetailsById(int offerId);
        Task<Offer> FindOfferWithCombinedById(int offerId);
        Task<Offer> GetOfferWithTenderById(int offerId);
        Task<Offer> FindOfferWithTenderAndStatusAndNegotiationById(int offerId);
        Task<List<OfferSolidarity>> GetSolidaritybyCRs(List<string> crs, int tenderId);
        Task<OfferSummaryStatusModel> FindOfferSummaryStatusModelById(int offerId, string cr);
        Task<OfferCheckingDetailsModel> FindOfferDetailsById(int offerId);
        Task<Offer> FindOfferByIdWithAttaches(int offerId);
        Task<QueryResult<AppliedSuppliersReportModel>> FindSuppliersReportByTenderId(int tenderId, int branchId, string AgencyCode, int committeeId, Enums.CommitteeType committeeType);
        Task<QueryResult<AppliedSuppliersReportDetailsModel>> FindSuppliersReportByTenderId__(int tenderId, int pageNumber);
        Task<List<AppliedSuppliersReportDetailsModel>> ExportAppliedSuppliersReport(int tenderId);
        Task<Offer> FindOfferWithQuantityTableByTenderIdAndCR(int tenderId, string suplierCR);
        Task<OfferModel> GeOfferByTenderId(int tenderId);
        Task<Tender> CheckOfferAdding(int tenderId, string suplierCR);
        Task<List<Offer>> GetIdenticalOffersByTenderId(int tenderId);
        Task<List<Offer>> GetReceivedOffersByTenderId(int tenderId);
        Task<List<Offer>> GetReceivedAndIdenticalOffersByTenderId(int tenderId);
        Task<List<Offer>> GetNotIdenticalOffersByTenderId(int tenderId);
        Task<List<Offer>> GetFinancialAcceeptedOffersByTenderId(int tenderId);
        Task<List<Offer>> GetFinancialRejectedOffersByTenderId(int tenderId);
        Task<OfferFinancialDetailsModel> OfferFinancialDetails(int offerId);
        Task<OfferLocalContentDetailsModel> GetOfferLocalContentDetails(int offerId);
        Task<List<Tender>> GetPostqualificationOnTenderForCancel(int tenderId);
        Task<QueryResult<OfferFinancialDetailsModel>> OfferFinancialDetailsForTender(int tenderId);
        Task<QueryResult<OfferFinancialDetailsModel>> GetOffersForAwardingByTenderId(int tenderId, string crsuppliers);
        Task<QueryResult<OfferFinancialDetailsModel>> OfferAwardeFinancialDetailsForTender(int tenderId);
        Task<List<TenderQuantityItemDTO>> GetSupplierQTableItemsForDirectPurchase(int offerId);
        List<SupplierAttachment> GetDeletedAttachment(int attachmentId, Offer offer);
        Task<OfferAttachmentsModel> GetOfferAttachmentsDetails(int offerId, int combinedId);
        Task<Offer> FindOfferWithSupplierQuantitiesByOfferId(int offerId);
        Task<List<OfferDeatilsReportForTenderModel>> OfferDetailsReportForTender(int tenderId);
        Task<OfferCheckingAttachmentsDetailsModel> FindOfferDetailsForCheckById(int offerId);
        Task<OfferModel> GeOfferByTenderIdAndOfferIdForOpening(int tenderId, int offerId);
        Task<OfferModel> OfferCheckingDetailsForAwarding(int tenderId, int offerId);
        Task<ConfigurationSetting> GetLocalContentSettingsDate();
        Task<OpenOfferModel> GetOpenOfferbyId(int offerid);
        Task<OpenOfferModel> OpenOffersDetailsForAwarding(int offerid);
        Task<Offer> GetOfferWithQuantitiesTablesByOfferId(int offerId);
        Task<OfferSolidarity> GetCombinedbyId(int combinedId);
        Task<SupplierCombinedDetail> GetCombinedOfferDetail(int combinedId);
        Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(CombinedSupplierModel model, int userId);
        Task<Offer> GetTheLowestOffer(int tenderId);
        Task<Offer> GetTheLowestOfferByTenderId(int tenderId);
        Task<Offer> GetOfferForNegotiation(int TenderId, string CR);
        Task<Offer> GetOfferByTenderAndCR(int TenderId, string CR);
        Task<OfferSolidarity> FindOfferByCombinedSupplierId(int combinedSupplierId);
        Task<Offer> GetOfferByIdForFinancialChecking(int offerId);
        Task<OfferDetailModel> GetOfferDetails(int combinedsupplierId);
        Task<CheckOfferModel> GetOpenOfferDataForCheckStage(int offerid);
        Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(int offerid, int UserId);
        Task<QueryResult<CombinedListModel>> GetAllCombinedInvitationForSupplier(CombinedSearchCriteria model);
        Task<CombinedInvitationDetailsModel> GetCombinedInvitationDetailsByOfferIdAndCr(int offerId, string cR);
        Task<QueryResult<CombinedSuppliersListModel>> GetAllCombinedSuppliers(CombinedSearchCretriaModel model);
        Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerid);
        Task<OfferSolidarity> GetCombinedWithOfferAndTenderById(int combinedId);
        Task<OfferDetailsForAcceptingSolidarityInviationsModel> GetOfferDetailsByCombinedId(int combinedId, int userId);
        Task<OfferSolidarity> GetCombinedWithOfferandTenderById(int combinedId);
        Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseChecking(int CombinedSupplierId);
        Task<OfferDetailsForCheckingModel> GetOfferDetailsForFinancialCheckingByOfferId(int offerId);
        Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseCheckingByOfferId(int offerId);
        #region VRO
        Task<VROOffersTechnicalCheckingViewModel> FindVROOfferDetailsById(int offerId);
        Task<VROFinantialCheckingModel> GeOfferByTenderIdAndOfferIdForFinancialChecking(int tenderId, int offerId);
        #endregion VRO
        Task<OfferFullDetailsModel> FindOfferFullDetailsModelbyOfferId(int offerId, string CR);
        Task<TechnicianReportAttachment> GetTechnicianReportAttachment(string referenceId);
        Task<SupplierAttachmentPartialVModel> FindOfferByIdWithAttachementsModel(int offerId);
        Task<OfferFileVModel> FindOfferFileVModelByOfferId(int offerId);
        Task<OfferFileVModel> FindOfferToCheckTenderTypeByOfferId(int offerId);
        Task<OfferMainVModel> FindActiveOfferMainVModelByOfferId(int offerId, string CR);
        Task<Offer> GetOfferWithCombinedById(int offerId);
        Task<List<TenderQuantityItemDTO>> GetSupplierQTableItemsByTableId(int offerId, int tableId); Task<SupplierTenderQuantityTable> GetSupplierQTableByTableId(int tableId);
        Task<List<TenderQuantityItemDTO>> GetSupplierQTableItems(int offerId, int tenderid);
        Task<SupplierAttachment> FindSupplierAttachmentById(int id);
        Task<SupplierAttachment> FindSupplierAttachmentById(string referenceNumber);
        Task<Offer> FindOfferWithStatusById(int offerId);
        Task<Offer> FindOfferWithQTJSONId(int offerId);
        Task<Offer> GetofferWithTenderAndSupplierQT(int offerId);
        Task<Offer> FindOfferWithSupplierQTItemsForApplyOfferByIdAndTableId(int offerId, int tableId);
        Task<OfferSolidarityModel> FindOfferSolidarity(int tenderId);
        Task<OfferQuantityTableModel> FindOfferQuantityTableModel(int offerId);
        Task<Offer> GetOfferByTenderIdAndCROfOfferOwner(int TenderId, string CR);
        Task<Offer> FindOfferWithSupplierTenderQuantitiesByOfferId(int offerId);
        Task<Offer> findOfferbyCRandTenderID(int tenderId, string CR);

        Task<Offer> GetOfferByTenderAndOwnerOrSolidarityCR(int TenderId, string CR);
        Task<Offer> GetOfferByTenderIdAndSolidarityCR(int TenderId, string CR);
        #region New Quantity tables 10/09/19
        Task<QueryResult<TenderQuantityItemDTO>> GetSupplierQTableItemsForChecking(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<QueryResult<TenderQuantityItemDTO>> GetSupplierQTableItemsForApplyOffer(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<QuantitiesTemplateModel> GetOfferQuantityItems(int offerId, long tableId);
        Task<QuantitiesTemplateModel> GetOfferQuantityTableTemplateForApplyOffer(int offerId);
        Task<int> GetOfferTableQuantityItems(long tableId);
        Task<Offer> FindOfferForCheckingById(int offerId);
        #endregion
        #region Open Offers
        Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersByOfferid(CombinedSupplierModel model);

        Task<OfferDetailsDisplayModel> GetFinancialOfferDetailsForDisplay(int offerId);
        #endregion
        Task<Offer> GeOfferByTenderIdAndOfferIdForChecking(int offerId);
        int GetOfferTableQuantityItems(Offer offer, long tableId);
        Task<Offer> GetOfferForQuantityTableForAwarding(int offerId);
        Task<SupplierCombinedDetail> GetCombinedOfferDetailForVRO(int combinedId);
        Task<List<NegotiationFirstStageSupplier>> GetNegotiationFirstStageSuppliersByTenderId(int tenderId);
        Task<List<string>> GetFailedSuppliersOnPostQualification(int tenderId);
        Task<bool> CanAddExclusionReasonIfTenderHasExtendOfferValidity(int tenderId);
        Task<long> addOfferQTItemsJson(List<SupplierTenderQuantityTableItem> items, long tableId, long offerId);
        Task<List<PostQualificationSuppliersInvitations>> GetPostqualificationOnTender(int tenderId);
        Task<OfferlocalContentDetails> GetOfferLocalContentDetailsByOfferId(int offerId);
        Task<OfferlocalContentDetails> GetOfferLocalContentDetailsWithOfferByOfferId(int offerId);
        Task<List<Offer>> GetIdenticalOffersWithLocalContentByTenderId(int tenderId);
        Task<MoneyMarketSuppliers> GetSupplierFromMonayMarketWithCr(string cr);
    }
}
