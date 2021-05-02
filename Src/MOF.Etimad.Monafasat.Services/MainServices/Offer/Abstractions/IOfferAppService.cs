using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Invitation;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.Monafasat.ViewModel.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IOfferAppService
    {
        #region Query
        Task<Offer> SaveDiscountNotes(DiscountNotesModel m);
        Task<OfferQuantityTableHtmlVM> GetQuantityTablesDisplay(int offerId, bool allowEdit, bool isNegotiation = false);
        Task<OfferQuantityTableHtmlVM> GetQuantityTablesForOpening(int offerId, bool allowEdit);
        Task<QuantitiesTemplateModel> GetQuantityTablesForOpeningTest(int offerId, bool allowEdit);
        Task<OfferDetailModel> GetTenderStatus(int tenderId);
        Task<OfferDetailModel> GetOfferDetails(int combinedId, int userId);
        Task<OpenOfferModel> GetOpenOfferbyId(int offerid, int userId);
        Task<OpenOfferModel> OpenOffersDetailsForAwarding(int offerid, int userId);
        Task<OfferModel> GeOfferByTenderIdAndOfferIdForOpening(int tenderId, int offerId);
        Task<QuantitiesTemplateModel> FindApplyOfferQuantityItemsById(int tenderId, int offerId, int branchId, bool isReadOnly);
        Task<OfferModel> GeOfferByTenderIdAndOfferIdForDirectPurchaseChecking(int tenderId, int offerId);
        Task<QuantitiesTablesForAwardingModel> GetOfferQuantityTableForAwarding(int tenderId, int offerId);
        Task<TableModel> GetCoastsTablForDirectPurchaseAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<TableModel> GetCoastsTablForApplyOfferAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<TableModel> GetCoastsTablForOpenDetails(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<QueryResult<TableModel>> GetApplyOfferTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<QueryResult<TableModel>> GetTableQuantityItemsOpenDetails(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<QueryResult<TableModel>> GetOfferTableQuantityItemsForDirectPurchase(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<OfferModel> OfferCheckingDetailsForAwarding(int tenderId, int offerId);
        Task<QueryResult<Offer>> Find(OfferSearchCriteria criteria);
        Task<QueryResult<AppliedSuppliersReportModel>> FindSuppliersReportByTenderId(int tenderId, int branchId, string AgencyCode, int committeeId, Enums.CommitteeType committeeType);
        Task<QueryResult<AppliedSuppliersReportDetailsModel>> FindSuppliersReportByTenderId__(int tenderId, int pageNumber);
        Task<List<AppliedSuppliersReportDetailsModel>> ExportAppliedSuppliersReport(int tenderId);
        Task<Offer> FindById(int offerId);
        Task<OfferCheckingDetailsModel> FindOfferDetailsById(int offerId);
        Task<OfferModel> GeOfferByTenderId(int tenderId);
        Task<Offer> WithdrawOffer(int offerId, string suplierCR);
        Task<OfferFinancialDetailsModel> OfferFinancialDetails(int offerId);
        Task<OfferLocalContentDetailsModel> GetOfferLocalContentDetails(int offerId);
        Task<QueryResult<OfferFinancialDetailsModel>> OfferFinancialDetailsForTender(int tenderId);
        Task<QueryResult<OfferFinancialDetailsModel>> GetOffersForAwardingByTenderId(int tenderId, string crsuppliers);
        Task<QueryResult<OfferFinancialDetailsModel>> OfferAwardeFinancialDetailsForTender(int tenderId);
        Task SaveOfferAwardingValues(OfferAwardingModel offerAwardingObj);
        Task<OfferAttachmentsModel> GetOfferAttachmentsDetails(int offerId, int combinedId, int userId);
        Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(CombinedSupplierModel model, int userId);
        Task AddExclusionReason(ExclusionReasonAwardingModel offerAwardingObj);
        Task RemoveExclusionReason(ExclusionReasonAwardingModel offerAwardingObj);

        #endregion

        #region Command

        Task<Offer> AddOffer(Offer offer);
        Task<Offer> SendOffer(int offerId, string cr);
        Task<Offer> ConfirmReceivedOfferAsync(int offerId);
        Task<QueryResult<TableModel>> GetBayanTableAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<QueryResult<TableModel>> GetBayanTableReadOnlyAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task AddCheckingFinancialDetails(OfferDetailModel model);
        Task<Offer> UpdateOfferCheckingStatus(OfferCheckingDetailsModel offerModel);
        Task<SupplierCombinedDetail> AddOfferAttachmentsDetails(OfferAttachmentsModel m, string cr);
        Task<SupplierSpecificationAttachment> AddClassificationAttachmentsAsync(SupplierSpecificationAttachment Attachments);
        Task<List<OfferDeatilsReportForTenderModel>> OfferDetailsReportForTender(int tenderId);
        Task<OfferCheckingAttachmentsDetailsModel> FindOfferDetailsForCheckById(int offerId);
        #region Checking Stage Direct Purchase
        Task<QuantitiesTemplateModel> GetQuantityTablesForDirectPurchase(int tenderId, int offerId);
        Task SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial(OfferCheckingContainer container);
        Task SaveOneFileDirectPurchaseChecking(CheckOfferResultModel checkingResult);
        Task SaveTwoFileTechnicalCheckingDataAndAttachemntsDetial(OfferCheckingContainer container);
        Task SaveTwoFileFinancialCheckingDataAndAttachemntsDetial(OfferCheckingContainer container);
        Task SaveTwoFilesTechnicalDirectPurchaseChecking(CheckOfferResultModel checkingResult);
        Task SaveTwoFilesFinancialDirectPurchaseChecking(CheckOfferResultModel checkingResult);
        Task<SupplierCombinedDetail> SaveDirectPurchaseAttachmentsChecking(OfferDetailModel m);
        Task<SupplierCombinedDetail> SaveCombinedTechnicalDetailsVRO(OfferDetailModel m);
        Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseTenderChecking(int combinedId, int userId);
        Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseTenderFinancialChecking(int offerId, int userId);
        #endregion Checking Stage Direct Purchase

        #region Bidding Round

        Task AddOfferBid(string tenderIdString, string biddingIdString, decimal biddingValue, string cR);
        #endregion Bidding Round
        Task<SupplierCombinedDetail> AddOfferDetails(OfferDetailModel m);
        #endregion
        Task<CheckOfferModel> GetOpenOfferDataForCheckStage(int offerid);
        Task<OfferDetailModel> GetOfferDetailsDisplayOnly(int combinedId, int userId);
        Task<OfferDetailModel> GetOfferDetailsForOpening(int combinedId, int userId);
        Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(int offerid, int userId);
        Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerId);
        Task<QueryResult<CombinedListModel>> GetAllCombinedInvitationForSupplier(CombinedSearchCriteria model);
        Task<CombinedInvitationDetailsModel> GetCombinedInvitationDetails(int offerId);
        Task<QueryResult<CombinedSuppliersListModel>> GetAllCombinedSuppliers(CombinedSearchCretriaModel model);
        Task<OfferDetailsForAcceptingSolidarityInviationsModel> GetOfferDetailsByCombinedId(int combinedId, int userId);
        Task<OfferDetailsForCheckingModel> GetOfferDetailsForTenderChecking(int combinedId, int userId);
        Task<OfferDetailsForCheckingModel> GetOfferDetailsForFinancialChecking(string offerIdString, int userId);
        Task<QueryResult<TableModel>> GetOfferTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<Offer> AddTwoFilesFinancialCheck(OfferDetailsForCheckingModel m);
        Task<Offer> AddFinantialCheckingResult(CheckOfferResultModel m);
        Task AddTwoFilesFinancialDetails(TenderTowFilesFinancialDetailsDetails model);
        Task AddTechnicalOfferResultForTwoFilesTender(OfferDetailsForCheckingTwoFilesModel model);
        Task<AlternativeItemResponseModel> DeleteAlternativeItem(int alternativeItemId, int tableId);
        Task<OfferFullDetailsModel> GetOfferFullDetails(int offerId);
        Task AcceptCombinedInvitation(int combinedId);
        Task RejectCombinedInvitation(int combinedId);
        Task<OfferQuantityTableHtmlVM> GetQuantityTablesReadOnly(int offerId);
        Task<OfferQuantityTableHtmlVM> GetQuantityTables(int offerId);
        #region VRO
        Task<string> ValidateandSaveVROCheckingQuantitiesTable(Dictionary<string, string> AuthorList);
        Task<QueryResult<TableModel>> GetOfferTableQuantityItemsVRO(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<VROOffersTechnicalCheckingViewModel> FindVROOfferDetailsById(string offerIdString);
        Task<VROOffersTechnicalCheckingViewModel> UpdateVROOfferCheckingStatus(VROOffersTechnicalCheckingViewModel offerMode);
        Task<OfferSummaryStatusModel> SendOfferToApprove(int offerId, string cr);
        Task<OfferSummaryStatusModel> GetOfferStatusForOfferSummary(int offerId, string CR);
        Task<VROFinantialCheckingModel> GeOfferByTenderIdAndOfferIdForFinantialChecking(int tenderId, int offerId);
        Task<OfferFileVModel> GetOfferFileVModel(int offerId);
        Task<OfferMainVModel> GetOfferMainVModel(int offerId, int tenderId);
        Task<OfferMainVModel> AddOfferMainVModel(OfferMainVModel offerMainVModel);
        Task<OfferFileVModel> Addofferfiles(OfferFileVModel offerFileVModel);
        Task<SupplierAttachmentPartialVModel> GetSupplierAllFiles(int OfferId);
        Task<Offer> AddFinancialOfferAttachmentsInVRO(OfferDetailsForCheckingModel m);
        #endregion VRO
        Task DeleteAttachement(int Id, string suplierCR);
        Task DeleteAttachement(string referenceNumber);
        Task<OfferSolidarityModel> GetOfferSolidarity(int offerId);
        Task<string> ValidateandSaveQuantitiesTable(Dictionary<string, string> AuthorList);
        Task<ValidationReturndTemplate> ValidateSupplierAlternativeItem(Dictionary<string, string> AuthorList);
        Task<OfferQuantityTableModel> GetOfferQuantityTableModel(int offerId);
        Task<OfferQuantityTableModel> GetApplyOfferQuantityTableModel(int offerId);
        Task<OfferQuantityTableModel> GetApplyOfferQuantityTableStepModel(int offerId);
        Task SendInvitationsAsync(List<SolidarityInvitedRegisteredSupplierModel> suppliersModel);
        Task AddSolidaritySupplier(SolidarityInvitationModel solidarity);
        Task SendInvitationsByEmail(int tenderId, List<string> emails);
        Task SendInvitationBySms(int tenderId, List<string> mobilNoList);
        Task<string> ValidateandSaveQuantitiesTableForOpening(Dictionary<string, string> AuthorList);
        Task<string> GetEmptyForm(int offerId, int tenderId);
        Task<ValidationReturndTemplate> ValidateOfferQuantitiesData(Dictionary<string, string> AuthorList);
        Task<QueryResult<TableModel>> ValidateandSaveCheckingQuantitiesTable(Dictionary<string, string> AuthorList);
        Task RemoveSolidaritySupplier(SolidarityRemoveInvitationModel solidarity);
        Task<ValidationReturndTemplate> ValidateBayanQuantitiesData(Dictionary<string, string> AuthorList);
        Task<ValidationReturndTemplate> DeleteOfferQuantityItem(Dictionary<string, string> AuthorList);
        #region Open Offers
        Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersByOfferid(CombinedSupplierModel model);
        Task<OfferDetailsDisplayModel> GetFinancialOfferDetailsForDisplay(int offerId);
        #endregion
        Task<OfferModel> GeOfferByTenderIdAndOfferIdForChecking(int offerId);
        Task<Offer> SaveOfferChecking(SaveOpeningOfferFinancialDetails model, CheckOfferResultModel checkingResult);
        Task<bool> RemoveOfferAwardingValue(int offerId);
        Task<List<int>> GetCanIgnoreMandatoryValidationColumnsId();
        Task<string> UpdateCorporationSize(int offerId);
        Task<string> UpdateIsCorporatioExistedInMoneyMarket(int offerId);

        Task<string> UpdateLocalContentBaseLine(int offerId);

        Task<string> UpdateLocalContentDesiredPercentage(int offerId);
        Task CalculateOfferFinancialPreferences(int tenderId);
        Task CalculateSMEAPreference(List<Offer> LargeCompaniesOffers, int tenderId);
        Task CalculateOffersFinancialEvaluation(List<Offer> offers, Tender tenderLocalContent, decimal? lowestOfferPrice);
    }
}
