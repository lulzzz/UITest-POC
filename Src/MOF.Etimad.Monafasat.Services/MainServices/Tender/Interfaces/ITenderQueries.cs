using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.DTO;
using MOF.Etimad.Monafasat.ViewModel.LCGDto;
using MOF.Etimad.Monafasat.ViewModel.Tender.LocalContent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ITenderQueries
    {
        Task<Tender> GetTenderByTenderIdWithInvitations(int tenderId);
        Task<CreateTenderBasicDataModel> GetBasicTenderByIdAndBranch(int tenderId, int branchId);
        Task<Tender> GetTenderAgencyCommunicationById(int tenderId);
        Task<Tender> FindTenderByTenderId(int tenderId);
        Task<Tender> GetTenderWithDateAndAddressByTenderId(int tenderId);
        Task<QueryResult<BasicTenderModel>> FindTenderBySearchCriteriaForUnitStage(TenderSearchCriteria criteria);
        Task<Tender> FindTenderWithUnitHistoryById(int tenderId);
        Task<Tender> FindTenderAndAgencyByTenderId(int tenderId);
        Task<Tender> FindTenderForSendToApproval(int tenderId);
        Task<Tender> FindTenderWithConditionsTemplateById(int tenderId, int branchId);
        Task<Tender> FindTenderWithConditionsTemplateIntroductionById(int tenderId, int branchId);
        Task<List<Template>> FindTenderForLocalContentByTenderId(int tenderId, int branchId);
        Task<List<Template>> FindTemplatesby(int tenderId, int activityversionid, int branchId);
        Task<Tender> FindTenderWithConditionsTemplateTechnicalOutputs(int tenderId, int branchId);
        Task<Tender> FindTenderWithAreasById(int tenderId);
        Task<BasicOfferTenderInfoModel> GetBasicOfferTenderInfoById(int id);
        Task<QueryResult<AllTendersViewModel>> GetAllTendersForIndexPage(TenderSearchCriteria criteria);
        Task<QueryResult<BasicTenderModel>> FindTenderBySearchCriteriaForIndexPage(TenderSearchCriteria criteria);
        Task<List<int>> FindTenderByAgencyCodeForPurchaseMethod(string agencyCode);
        Task<QueryResult<TenderMovement>> GetTenderMovementsFromHistoryByTenderId(SimpleTenderSearchCriteria criteria);
        Task<QueryResult<BasicTenderModel>> FindTenderForAppliedSuppliersReport(TenderSearchCriteria criteria);

        Task<QueryResult<LinkTendersToCommitteeModel>> GetTendersToJoinCommitteeWithBranches(
            LinkTendersToCommitteeSearchCriteriaModel criteria);

        Task<QueryResult<BasicTenderModel>> FindTenderBySearchCriteriaForUnderOperationsStage(
            TenderSearchCriteria criteria);

        Task<QueryResult<TenderCheckingAndAwardingModel>> FindTenderBySearchCriteriaForOpeningStage(
            TenderSearchCriteria criteria);

        Task<QueryResult<TenderCheckingAndAwardingModel>> FindTenderBySearchCriteriaForCheckingStage(
            TenderSearchCriteria criteria);

        Task<QueryResult<VROTendersDTO>> GetVROTendersCreatedByGovAgency(TenderSearchCriteria criteria);
        Task<List<Tender>> GetTendersToJoinCommittees(LinkTendersToCommitteeSearchCriteriaModel criteria);

        Task<QueryResult<TenderCheckingAndAwardingModel>> GetTendersForCheckingDirectPuchaseStageAsync(
            TenderSearchCriteria criteria);

        Task<QueryResult<TenderCheckingAndAwardingModel>> FindTenderBySearchCriteriaForAwardingStage(
            TenderSearchCriteria criteria);

        Task<QueryResult<TenderCheckingAndAwardingModel>>
            FindTenderBySearchCriteriaForAwardingStageForApproveTenderAward(TenderSearchCriteria criteria);

        Task<QueryResult<TenderOpenOfferModel>> GetOffersToOpeningByTenderIdAsync(
            TenderBasicSearchCriteria tenderBasicSearchCriteria);

        Task<Tender> FindTenderForCheckingStatusByTenderId(int tenderId);
        Task<TenderUnitAssign> FindLastTenderUnitAssignsByTenderId(int tenderId);
        Task<Invitation> GetInvitation(string commericalRegisterNo, int tenderId);
        Task<Invitation> GetInvitationById(int invitationId);
        Task<QueryResult<Invitation>> GetNewInvitationsByCRNo(TenderSearchCriteria tenderSearchCriteria);
        Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierTenders(TenderSearchCriteria tenderSearch);
        Task<QueryResult<Tender>> GetAllTenders(string cr);

        Task<QueryResult<TenderInvitationDetailsModel>> GetAllSupplierTenders(TenderSearchCriteria criteria,
            List<SupplierAgencyBlockModel> allSupplierBlocks);

        Task<QueryResult<AllTendersForVisitorModel>> GetAllTendersForVisitor(TenderSearchCriteria criteria);

        Task<QueryResult<TenderInvitationDetailsModel>>
            GetAllUnsubscribedSupplierTenders(TenderSearchCriteria criteria);

        Task<QueryResult<Invitation>> GetSuppliersJoiningRequestsByTenderId(int tenderId);
        Task<Invitation> GetJoiningRequestByInvitationId(int invitationId);
        Task<QueryResult<Offer>> GetOffersByTenderIdAsync(int tenderId);
        Task<QueryResult<TenderCheckOfferModel>> GetOffersByTenderIdForCheckAsync(int tenderId);

        Task<QueryResult<TenderCheckOfferModel>> GetOffersByTenderIdForOpeningAwardingStage(
            TenderBasicSearchCriteria tenderBasicSearchCriteria);

        Task<Tender> FindTenderQuantityTablesById(int tenderId);
        Task<Tender> GetTenderWithQuantityTablesById(int tenderId);
        Task<QuantitiesTemplateModel> GetTenderQuantityItems(int tenderId);
        Task<Tender> FindTenderByInvitationId(int invitationId);
        Task<Tender> FindTenderByAgencyCode(string agencyCode);
        Task<Tender> FindTenderOfferDetailsByTenderId(int tenderId);
        Task<Tender> FindTenderDetailsByTenderId(int tenderId);
        Task<Tender> FindTenderInvitationsWithById(int tenderId);
        Task<Tender> GetTenderAgencyCommunicationByIdForExtendDatesRequist(int tenderId);
        Task<bool> FindUnRegisteredSuppliersInvitationByTenderIDAndEmail(int tenderId, string email, string CrNumber);

        Task<TenderDetailsModel> GetMainTenderDetailsForSuppliers(int tenderId, string cr,
            List<SupplierAgencyBlockModel> blockedSuppliers);

        Task<TenderDetailsModel> GetMainTenderDetailsByTenderId(int tenderId, int branchId);
        Task<TenderDetailsModel> GetMainTenderDetailsForVisitor(int tenderId);
        Task<TenderDetailsModel> GetMainTenderDetailsForUnit(int tenderId, string agencyCode);
        Task<TenderInformationModel> GetTenderById(int tenderId);
        Task<Tender> GetTenderByIdWithoutAnyIncluds(int tenderId);
        Task<Tender> GetTenderByIdWitOffersAndBidingRounds(int tenderId);
        Task<Tender> GetTenderByIdWithAwardingHistoy(int tenderId);
        Task<Tender> GetTenderWithTypeAndInvitations(int tenderId);
        Task<Tender> GetTenderByIdAndBranch(int tenderId, int branchId);
        Task<Tender> GetTendersRelatedToFirstStage(int tenderId, int branchId);
        Task<SecondStageBasicData> GetTenderByIdAndBranchForSecondStage(int tenderId, int branchId);
        Task<TenderRelationsModel> FindTenderRelationsModelByTenderId(int tenderId);
        Task<Tender> GetTenderWithAreas(int tenderId, int branchId);
        Task<AllBasicTenderDataModel> GetBasicTenderDetailsById(int tenderId, string agencyCode);
        Task<QueryResult<Tender>> FindTendersByLogedUser(string agencyCode);
        Task<EditeCommitteesModel> GetTenderCommitteesByTenderId(int tenderId, int branchId);
        Task<TenderDatesModel> FindTenderDatesByTenderId(int tenderId);
        Task<GetConditionTemplateModel> FindTenderConditionTemplateByTenderId(int tenderId, int branchId);
        Task<GetConditionTemplateModel> FindConditionTemplate(int tenderId, int branchId);
        Task<ConditionsBookletTemplateModel> GetTenderConditionsBookletTemplateDetails(int tenderId);
        Task<TenderDatesModel> FindTenderDatesDetailsByTenderId(int tenderId);
        Task<TenderOffersModel> GetTenderOfferDetailsForOppeningStage(int tenderId, int userId);
        Task<Tender> FindTenderOfferDetailsByTenderIdDisplayOnly(int tenderId); 
        Task<QueryResult<TenderCheckOfferModel>> GetVROOffersByTenderIdForCheckingGridAsync(TenderBasicSearchCriteria tenderId);
        Task<int> GetUnOpenOffersForCombinedSuppliers(int tenderId);
        Task<TenderOffersModel> GetTenderOffersDetailsForOpenAwarding(int tenderId, int userId, string agencyCode);
        Task<TenderOffersModel> GetTenderAwardingDetailsByTenderId(int tenderId, int userId, string agencyCode);
        Task<Tender> FindTenderInvitationOfferDetailsByTenderId(int tenderId);
        Task<Tender> FindTenderRelationsByTenderId(int tenderId);
        Task<TenderRelationsModel> FindTenderRelationsByTenderIdAfterUpdateDates(int tenderId);
        Task<Tender> FindTenderRelationsWithoutQuantityTables(int tenderId);
        Task<TenderRelationsModel> FindRelationsDetailsByTenderId(int tenderId);
        Task<TenderLocalContentValuesViewModel> GetTenderLocalContenetValuesByTenderId(int tenderId);
        Task<LocalContentDetailsViewModel> GetLocalContentDetailsForSupplierByTenderId(int tenderId);
        Task<Tender> FindTenderAttachmentsStepById(int tenderId, int branchId);
        Task<Tender> FindFavouriteSupplierTenderByTenderId(int tenderId);
        Task<FavouriteSupplierTender> FindFavouriteSupplierTenderByTenderId(int tenderId, string cr);
        Task<Tender> FindTenderInvitationByTenderId(int tenderId);
        Task<Tender> GetTenderForFramWorkReCreation(int tenderId);
        Task<TenderAttachment> FindTenderAttachementByReferenceId(string referenceId);
        Task<TenderAttachmentChanges> FindTenderAttachementChangesByReferenceId(string referenceId);
        Task<SupplierPreQualificationAttachment> GetQualificationAttachmentsByReferenceId(string referenceId);
        Task<TenderChangeRequest> FindActiveTenderRevisionCancelByTenderId(int tenderId);
        Task<TenderCanelationModel> GetTenderDataForCanelation(int tenderId);
        Task<TenderRevisionDateModel> FindActiveTenderRevisionDateByTenderId(int tenderId);
        Task<TenderDatesChange> FindActiveTenderRevisionDateForExtendDateApproval(int tenderId);
        Task<TenderRevisionDateModel> FindRejectedTenderRevisionDateByTenderId(int tenderId);

        Task<QueryResult<TenderExtendDateModel>> FindTenderRevisionDateBySearchCriteria(
            TenderRevisionSearchCriteria criteria);

        Task<QueryResult<TenderChangeRequest>> FindTenderRevisionCancelBySearchCriteria(
            TenderRevisionSearchCriteria criteria);

        Task<List<TenderHistory>> FindTenderHistoriesByTenderId(int tenderId);
        Task<Tender> FindTenderAttachmentsById(int tenderId, int branchId);
        Task<Tender> FindTenderWithAttachments(int tenderId);
        Task<AttachmentsModel> FindTenderAttachmentsModelById(int tenderId, int branchId);
        Task<bool> IsTenderPurchasedBySupplier(int tenderId, string CR);
        Task<List<Invitation>> GetInvitation(List<string> commericalRegisterNos, int tenderId);
        Task<Tender> FindTenderStatusByTenderId(int tenderId);
        Task<Tender> FindTenderForOpenCheckStageByTenderId(int tenderId);
        Task<Tender> FindTenderFoAwardingStageByTenderId(int tenderId);
        Task<Tender> FindTenderForAwarding(int tenderId);
        Task<Tender> FindTenderByIdForAwarding(int tenderId);
        Task<List<Offer>> FindSuppliersOffersInAwardingStageByTenderId(int tenderId);
        Task<Tender> FindTenderToApproveCancel(int tenderId);
        Task<Tender> FindTenderWithStatusAndOffersByTenderId(int tenderId);
        Task<Tender> FindTenderToApproveFinancialOpening(int tenderId);
        Task<Tender> FindTenderWithInvitationsBills(int tenderId);
        Task<Tender> FindTenderWithConditionsBookletsBills(int tenderId);
        Task<Tender> FindTenderForUpdateDates(int tenderId);
        Task<List<VacationsDate>> FindAllVacationDates();
        Task<List<AddressModel>> GetAllAddresses();
        Task<List<AddressModel>> GetOfferOpeningAddresses(int branchId);
        Task<VerificationCode> FindVerificationCode(int userId, int typeId);
        Task<bool> SupplierExistsInInvitationsOrConditionsBookletsByTenderIdAndCR(int tenderId, string CR);
        Task<bool> UserExistsInTechnicalCommitteeUsersByEnquiryIdAndUserId(int enquiryId, int userId);
        Task<bool> UserExistsInCommitteeUsersByEnquiryIdAndUserId(int enquiryId, int userId);
        Task<List<string>> FinancialYear();
        Task<QueryResult<TenderInformationModel>> GetAllTendersHasEnquiry(TenderSearchCriteria criteria);
        Task<bool> GetHasTendersByCommittee(int committeeId);

        Task<QueryResult<TenderCheckingAndAwardingModel>> FindAwardedTenderBySearchCriteria(
            TenderSearchCriteria criteria);

        Task<TenderDetialsReportModel> OpenTenderDetailsReport(int tenderId);
        Task<TenderDetialsReportModel> OpenTenderDetailsReportForVisitor(int tenderId);
        Task<AwardingReportModel> AwardingReport(int tenderId);
        Task<OfferReportModel> OffersReport(int tenderId);
        Task<ConditionsBooklet> GetConditionBookletByCrAndTenderId(int tenderId, string cr);
        Task<PrintTenderReceiptReportModel> PrintTenderReceiptReport(int tenderId, string CommericalRegisterNo);
        Task<CountAndCloseAppliedOffersModel> CountAndCloseAppliedOffers(int tenderId);
        Task<Tender> FindTenderWithOffer(int tenderId);
        Task<TenderChangeRequest> GetNotPendingCancelChangeRequest(int tenderId);
        Task<TenderChangeRequest> GetExtendDateChangeRequestForRejection(int tenderId);
        Task<Tender> FindTenderForApproval(int tenderId);
        Task<Tender> FindTenderForApprove(int tenderId);
        Task<bool> IsFrameworkTenderCheckValid(Tender tender);
        Task<List<Tender>> GetRelatedTendersByActivities(int tenderId);
        Task<AwardingDetailsModel> GetAwardingInformationForSupplier(int tenderId, string cr);
        Task<List<FavouriteSupplierTender>> GetFavouriteTenderSuppliersToApplyOffer();

        #region Change Requests

        Task<TenderChangeRequest> GetChangeRequest(int tenderid, int changeTypeId);
        Task<TenderChangeRequest> GetCancelChangeRequest(int tenderId);
        Task<TenderChangeRequest> GetPendingCancelChangeRequest(int tenderId, string requestedByRoleName);
        Task<TenderChangeRequest> GetExtendDateChangeRequest(int tenderId);
        Task<TenderChangeRequest> GetExtendDateChangeRequestForCancel(int tenderId);
        Task<TenderChangeRequest> GetAttachmentsChangeRequest(int tenderId);

        Task<TenderChangeRequest> FindTenderExtendDatesRequests(int tenderId);
        Task<Tender> GetTenderWithExtendDatesChangeRequest(int tenderId);
        Task<Tender> FindTenderForQuantitiesTableChangeRequests(int tenderId);
        Task<Tender> GetTenderWithAttachmentsChangeRequests(int tenderId);
        Task<Tender> GetTenderWithAttachmentsChangeRequestsForRejection(int tenderId);
        Task<Tender> GetTenderWithAttachmentsChangeRequestsForClosing(int tenderId);
        Task<TenderAttachmentChanges> GetAttachmentById(int attachmentId);

        #endregion

        Task<TenderNewsModel> GetTenderNewsByTenderId(int tenderId);
        Task<Invitation> FindInvitationByInvitationId(int invitationId);

        Task<PurchaseModel> FindTenderWithConditionBookletByTenderIdAndCr(int tenderId, string cr,
            List<SupplierAgencyBlockModel> allSupplierBlock);

        Task<PurchaseModel> FindTenderForInvitationBillDetailsByTenderIdAndCr(int tenderId, string cr,
            List<SupplierAgencyBlockModel> allSupplierBlock);

        Task<TenderCommunicationRequestModel> GetCommunicationRequestsDetailsTenderId(int tenderId, string cr);
        Task<List<string>> GetQualifiedSuppliers(int? preQualificationId);
        Task<List<string>> GetSuppliersJoinedToAnnouncemet(int? preAnnouncementId);
        Task<List<string>> GetAnnouncementSuppliersTemplateJoinedRequest(int? announcementTemplateId);
        Task<Tender> GetTenderByIdForInvitations(int? tenderId);
        Task<NegotiationAgencyTenderModel> GetTendeBasicDataForNegotiation(int TenderId);
        Task<InvitationStepModel> FindTenderDetailsByInvitationdAsync(int tenderId);
        Task<Tender> FindTenderWithInvitationsByTenderId(int tenderId);
        Task<List<string>> GetAcceptedSupplierInFirstStageTender(int? preQualificationId);
        Task<List<TenderUnitStatusesHistory>> GetUnitStatusesHistoryByTenderId(int tenderId);
        Task<Tender> FindTenderWithAgenyRequestsAndNegotiations(int TenderId);

        Task<ExtendOfferPresentationDatesModel> FindTenderDatesForExtendDatesRequestByTenderId(int tenderId,
            int agencyRequestId);

        Task<ExtendOfferPresentationDatesModel> FindTenderRevisionDateForExtendOffersRequestByTenderId(int tenderId,
            int agencyRequestId);

        Task<QueryResult<TenderCheckOfferModel>> GetOffersByTenderIdForCheckingGridAsync(
            TenderBasicSearchCriteria tenderBasicSearchCriteria);

        #region Direct Purchase

        Task<TenderOffersModel> FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(int tenderId, int userId);
        Task<Tender> FindTenderOfferDetailsByTenderIdForDirectPurchaseCheckStage(int tenderId, int userId);

        #endregion Direct Purchase

        #region bidding

        Task<Tender> FindBiddingRoundOffersByTenderId(int tenderId);
        Task<Tender> FindTenderForAddingBiddingOfferByTenderId(int tenderId);
        Task<List<Offer>> FindTenderOffersForBiddingRound(int tenderId);
        Task<Tender> FindBiddingRoundOffersByTenderIdForEndingBiddingRound(int tenderId);

        #endregion bidding

        #region VRO

        Task<QueryResult<VROTenderCheckingAndOpeningModel>> FindTendersForVROCheckingAndOpeningStageBySearchCriteria(
            TenderSearchCriteria criteria);

        Task<VROTenderOffersModel> FindVROTenderOfferDetailsByTenderIdForCheckStage(int tenderId, int userId);

        #endregion VRO

        Task<QueryResult<NegotiationOnTenderModel>> GetAllTenderhasNegotiationbySearchCretriaForUnitUsers(
            NegotiationOnTenderSearchCriteriaModel criteria);


        Task<List<SupplierTenderQuantityTableDTO>> getTenderQuantitytableItems(int tenderId);

        Task<QueryResult<TenderQuantityItemDTO>> GetTenderTableQuantityItems(
            QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);

        Task<QueryResult<TenderQuantityItemDTO>> GetTenderTableQuantityItemsChanges(
            QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);

        Task<int> GetTenderTableQuantityItems(Tender tender, long tableId);
        Task<int> GetTenderTableQuantityItemsChanges(Tender tender, long tableId);
        Task<List<TenderQuantityItemDTO>> GetTenderTableQuantityItemsByTableId(int tableId);
        Task<List<TenderQuantityItemDTO>> GetTenderTableQuantityItemsForNewByTableId(int tableId);
        Task<CheckingResultsModel> GetCheckingResultsInformation(int tenderId);

        #region Added Value

        Task<TenderType> GetTenderTypeById(int id);
        Task<List<TenderTypeWithAddedValueModel>> GetTenderTypesWithAddedValue();
        Task<List<MandatoryListForExportModel>> GetAllMandatoryListForExport();
        Task<TenderDetailsForAppliedSuppliersReportModel> GetTenderDetailsForAppliedSuppliersReport(int tenderId);

        #endregion

        Task<Tender> GetTenderWithFormQuantityTablesWithChanges(int tenderId);
        Task<Tender> GetTenderWithQuantityTable(int tenderId);
        Task<Tender> GetTenderWithFormQuantityItemsWithChanges(long tableId, bool isNewTable, int tenderId);
        Task<Tender> GetTenderWithChangeRequest(int tenderId);
        Task<TenderChangeRequest> GetChangeRequestWithFormQuantityTables(int tenderId);
        Task<Tender> GetTenderWithFormQuantityTablesAndActivitiesWithoutChanges(int tenderId);
        Task<Tender> GetTenderWithFormQuantityTablesWithoutActivitiesAndChanges(int tenderId);

        Task<Tender> GetTenderForLocalContent(int tenderId);
        Task<List<EmarketPlaceRequest>> GetAwardedSupplierQuantityTableItemsValue(List<int> offerIds);

        Task<SRMFrameworkAgreementManageModel> FillVendorProducts(List<int> offerIds,
            List<EmarketPlaceResponse> clotypes);

        Task<ConditionsBooklet> FindConditionsBookletForRePurchase(int tenderId, string CR);

        Task<TenderDetailsCheckingStageModel> GetTenderDetailsForCheckingStage(int tenderId, int userId,
            string agencyCode);

        Task<List<Offer>> GetFailedSuppliersOnTechnicalEvaluation(int tenderId);
        Task<List<Offer>> GetFailedSuppliersOnFinancialEvaluation(int tenderId);
        Task<List<Offer>> FindNotAwardedSuppliersCauseExcludedReason(int tenderId);
        Task<bool> IsSupplierFailedInPostqualification(int tenderId, string cr);
        Task<bool> IsTenderHasActivePostqualification(int tenderId);
        Task<Tender> FindTenderByAgencyAndReferenceNumberForContractLinking(string referenceNumber);
        Task<TenderChangeRequest> GetQTChangeRequest(int tenderid);

        Task<List<TenderQuantitiyItemsChangeJson>> GetQTItemsByTableIds(List<long> tableIds);
        Task DeleteQTUsingStoredProcedure(int tenderId);

        #region LCG

        Task<TenderInfo> FindTenderInfo(string tenderReferenceId);
        Task<QueryResult<TenderInfo>> FindTenderList(string supplierId, int pageSize, int pageNumber);

        #endregion
        Task<TenderDates> GetTenderDatesByTenderId(int tenderId);

        Task<VersionHistory> GetCurrentActivityVersion();
  
        Task<List<int>> GetTemplatesByActivityIdsAndVersionId(List<int> activityIds,int versionId);
        Task<int> GetCurrentTenderActivityVersion(int tenderId);
        //Task<List<int>> GetTemplatesWithActivityIds(List<string> activityIds);
        long getLastItemNumber(string tenderId, long quantityTableId);
        Task<List<long>> FindMandatoryListColumns(int tenderId, List<long> columnIds);
        Task<bool> IsTenderHasLocalContent(DateTime CreatedDate);
        Task<List<int>> GetTenderLocalContentMechanism(int tenderLocalContentId);
        Task<Tender> FindTenderWithLocalContentPreference(int tenderId);
        Task<TenderLocalContent> GetTenderLocalContentByTenderId(int tenderId);
    }
}
