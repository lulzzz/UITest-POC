using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.DTO;
using MOF.Etimad.Monafasat.ViewModel.LCGDto;
using MOF.Etimad.Monafasat.ViewModel.Tender;
using MOF.Etimad.Monafasat.ViewModel.Tender.LocalContent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public partial interface ITenderAppService
    {
        #region Queries
        Task<TemplateQuantityTableModel> FindTenderQuantityItemsForConditionsBookletById(int tenderId);
        Task<Tender> FindTenderByTenderId(int tenderId);
        Task SendAwardTenderToInitialApprove(int tenderId);
        Task<int> IsSupplierPassedForTenderAwarding(int tenderId, string cr, string agencyCode);
        Task IsTenderHasPendingRequestsOrNoExeclusionReasons(int tenderId);
        Task<int> CheckSendTenderToApproveAwarding(int tenderId, List<string> crs, string agencyCode);
        Task<QueryResult<AllTendersViewModel>> GetAllTendersForIndexPage(TenderSearchCriteria criteria);
        Task<QueryResult<BasicTenderModel>> FindTenderBySearchCriteriaForIndexPage(TenderSearchCriteria criteria);
        Task<List<int>> FindTenderByAgencyCodeForPurchaseMethod(string agencyCode);
        Task<QueryResult<LinkTendersToCommitteeModel>> GetTendersToJoinCommittees(LinkTendersToCommitteeSearchCriteriaModel criteria);
        Task<QueryResult<BasicTenderModel>> FindTendersForUnderOperationsStageBySearchCriteria(TenderSearchCriteria criteria);
        Task<QueryResult<BasicTenderModel>> FindTenderForAppliedSuppliersReport(TenderSearchCriteria creteria);
        Task<QueryResult<TenderCheckingAndAwardingModel>> FindTendersForOpeningStageBySearchCriteria(TenderSearchCriteria creteria);
        Task<QueryResult<TenderCheckingAndAwardingModel>> FindTendersForCheckingStageBySearchCriteria(TenderSearchCriteria criteria);
        Task<QueryResult<VROTendersDTO>> GetVROTendersCreatedByGovAgency(TenderSearchCriteria criteria);
        Task<QueryResult<VROTenderCheckingAndOpeningModel>> FindTendersForVROCheckingAndOpeningStageBySearchCriteria(TenderSearchCriteria criteria);
        Task<QueryResult<TenderCheckingAndAwardingModel>> GetTendersForCheckingDirectPuchaseStageAsync(TenderSearchCriteria criteria);
        Task<QueryResult<TenderCheckingAndAwardingModel>> FindTendersForAwardingStageBySearchCriteria(TenderSearchCriteria criteria);
        Task<BasicOfferTenderInfoModel> GetBasicOfferTenderInfoById(int id);
        Task<List<Invitation>> GetInvitation(List<string> commericalRegisterNos, int tenderId);
        Task<QueryResult<Invitation>> GetNewInvitationsByCRNo(TenderSearchCriteria tenderSearchCriteria);
        Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierTenders(TenderSearchCriteria tenderSearch);
        Task<QueryResult<Tender>> GetAllTenders(string cr);
        Task<QueryResult<TenderInvitationDetailsModel>> GetAllSupplierTenders(TenderSearchCriteria criteria, List<SupplierAgencyBlockModel> allSuppliersBlock);
        Task<QueryResult<AllTendersForVisitorModel>> GetAllSupplierTendersForVisitor(TenderSearchCriteria criteria);
        Task<QueryResult<TenderInvitationDetailsModel>> GetAllUnsubscribedSupplierTenders(TenderSearchCriteria criteria);
        Task<QueryResult<Invitation>> GetSuppliersJoiningRequestsByTenderId(int tenderId, string agencyCode, int BranchId);
        Task<TenderInvitationDetailsModel> GetJoiningRequestByInvitationId(int invitationId, string agencyCode, int BranchId);
        Task<QueryResult<TenderOpenOfferModel>> GetOffersForOpenByTenderIdAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria);
        Task<QueryResult<TenderCheckOfferModel>> GetOffersForCheckByTenderIdAsync(int tenderId);
        Task<QueryResult<TenderCheckOfferModel>> GetOffersForAwardingStageByTenderId(TenderBasicSearchCriteria tenderBasicSearchCriteria);
        Task<List<CommitteeModel>> GetBasicCommitteesOnBranch(string agencyCode);
        Task<List<CommitteeModel>> GetTechincalAndDirectPurchaseCommittees(string agencyCode);
        Task<List<CommitteeUserModel>> GetDirectPurchaseCommitteesMembers(string agencyCode, int branchId);
        Task<List<CommitteeModel>> GetVROAndTechnicalCommittees(string agencyCode);
        Task<Tender> FindTenderByAgencyCode(string agencyCode);
        Task<QueryResult<TenderCheckingAndAwardingModel>> FindAwardedTenderBySearchCriteria(TenderSearchCriteria criteria);
        Task<TenderDetailsModel> GetMainTenderDetailsByTenderId(int tenderId, int branchId);
        Task<TenderDetailsModel> GetMainTenderDetailsForUnit(int tenderId, string agencyCode);
        Task<TenderDetailsModel> GetMainTenderDetailsForVisitor(int tenderId);
        Task<TenderDetailsModel> GetMainTenderDetailsForSuppliers(int tenderId, string cr);
        Task<AllBasicTenderDataModel> GetBasicTenderDetailsById(int tenderId, string agencyCode);
        Task<QueryResult<Tender>> FindTendersByLogedUser(string agencyCode);
        Task<EditeCommitteesModel> GetTenderCommitteesByTenderId(int tenderId, int branchId);
        Task<Tender> GetTenderSamplesDeliveryAddress(int tenderId, int branchId);
        Task<SecondStageBasicData> GetBasicDataForSecondStageTenderCreation(int tenderId, int branchId);
        Task<CreateTenderBasicDataModel> GetBasicTenderByIdAndBranch(int tenderId, int branchId);
        Task<TenderInformationModel> GetTenderByIdForEnquiry(int tenderId);
        Task<TenderInformationModel> GetTenderByIdForJoinigRequests(int tenderId);
        Task<Tender> GetTenderByIdToApplyOffer(int tenderId);
        Task<PurchaseModel> GetTenderByIdToPurchaseConditionBooklet(int tenderId, string cr);
        Task<PurchaseModel> FindTenderForInvitationBillDetailsByTenderIdAndCr(int tenderId, string cr);
        Task<TenderCommunicationRequestModel> GetCommunicationRequestsDetailsTenderId(int tenderId, string cr);
        Task<Tender> FindTenderDetailsByTenderId(int tenderId);
        Task<TenderDatesModel> FindTenderDatesByTenderId(int tenderId, int branchId);
        Task<GetConditionTemplateModel> FindTenderConditionTemplateByTenderId(string tenderIdString, int branchId);
        Task<GetConditionTemplateModel> FindConditionTemplate(string tenderIdString, int branchId);
        Task<TenderDatesModel> FindTenderExtendDatesByTenderId(int tenderId, int branchId);
        Task<TenderDatesModel> GetTenderDatesDetailsById(int tenderId);
        Task<TenderOffersModel> FindTenderInvitationOfferDetailsByTenderId(int tenderId);
        Task<TenderOffersModel> GetTenderOfferDetailsForOppeningStage(int tenderId, int userId);

        Task<TenderOffersModel> GetTenderOffersDetailsForOpenAwarding(int tenderId, int userId, string agencyCode);
        Task<TenderOffersModel> GetTenderAwardingDetails(int tenderId, int userId, string agencyCode);
        Task<int> GetUnOpenOffersForCombinedSuppliers(int tenderId);
        Task<Tender> FindTenderOfferDetailsByTenderId(int tenderId);
        Task<TenderRelationsModel> FindTenderRelationsModelByTenderId(int tenderId, int branchId);
        Task<TenderRelationsModel> FindRelationsDetailsByTenderId(int tenderId);
        Task<TenderLocalContentValuesViewModel> GetTenderLocalContenetValuesByTenderId(int tenderId);
        Task<LocalContentDetailsViewModel> GetLocalContentDetailsForSupplierByTenderId(int tenderId);
        Task<Tender> FindTenderByInvitationId(int invitationId);
        Task<TenderHistory> FindTenderHistoryByUserIdAndTenderIdAndStatusId(int tenderId, int tenderStatusId);
        Task<Tender> FindTenderAttachmentsById(int tenderId, int branchId);
        Task<AttachmentsModel> FindTenderAttachmentsModelById(int tenderId, int branchId);
        Task<List<AddressModel>> GetAllAddresses();
        Task<List<AddressModel>> GetOfferOpeningAddresses(int branchId);
        Task<List<AddressModel>> GetOfferOpeningAddressesByBranchId(int branchId);
        Task<List<VacationsDate>> FindAllVacationDates();
        Task<TenderChangeRequest> FindTenderRevisionCancelByTenderId(int tenderId, bool isActive);
        Task<TenderCanelationModel> GetTenderDataForCanelation(int tenderId);
        Task<TenderRevisionDateModel> FindActiveTenderRevisionDateByTenderId(int tenderId);
        Task<TenderRevisionDateModel> FindRejectedRevisionDateByTenderId(int tenderId);
        Task<QueryResult<TenderExtendDateModel>> FindTenderRevisionDateBySearchCriteria(TenderRevisionSearchCriteria criteria);
        Task<QueryResult<TenderChangeRequest>> FindTenderRevisionCancelBySearchCriteria(TenderRevisionSearchCriteria criteria);
        Task<AwardingDetailsModel> GetAwardingInformationForSupplier(int tenderId, string cr);
        Task<CheckingResultsModel> GetCheckingResultsInformation(int tenderid);

        #endregion

        #region Commands
        Task<bool> ResendInvitationToSuppliersAsync(SupplierSearchCretriaModel cretria);
        Task<Tender> DeleteAsync(int tenderId);
        Task AwardTenderOffers(int tenderId);
        Task SendAwardTenderToApprove(int tenderId);
        Task StartTenderCheckingOffers(int tenderId);
        Task RejectInitialAwardTenderOffer(int tenderId, string rejectionReason, string agencyCode);
        Task ApproveTenderAwarding(int tenderId, string verificationCode);
        Task ApproveTenderOppening(int tenderId);
        Task ReopenTenderChecking(int tenderId);
        Task ReopenTender(int tenderId);
        Task ReopenTenderAfterCancel(int tenderId);
        Task CancelTenderCancellationRequest(int tenderId);
        Task SendTenderToApprove(int tenderId);
        Task SendTenderToApproveOppenning(int tenderId);
        Task SendTenderToApproveChecking(int tenderId);
        Task ReopenTenderAwarding(int tenderId);
        Task ApproveTenderChecking(int tenderId);
        Task ApproveTenderExtendDatesChangeRequest(int tenderId, string verificationCode, int typeId);
        Task ApproveTenderQuantityTablesChangeRequest(int tenderId, string verificationCode, int typeId);
        Task ApproveTenderAttachmentsChangeRequest(int tenderId, string verificationCode, int typeId);
        Task SendUpdateQuantityTableToApprove(int tenderId);
        Task SendUpdateAttachmentsToApprove(int tenderId);
        Task SendUpdateDatesToApprove(int tenderId);
        Task RejectTender(int tenderId, string rejectionReason);
        Task<Tender> RejectOpenTenderOffers(int tenderId, string rejectionReason);
        Task OpenTenderOffer(int tenderId);
        Task ReopenTenderOffer(int tenderId);
        Task RejectCheckTenderOffers(int tenderId, string rejectionReason);
        Task RejectAwardTenderOffer(int tenderId, string rejectionReason);
        Task ApproveTenderInitialAwarding(int tenderId, string agencyCode, string verificationCode);
        Task<TenderDatesModel> CreateBasicTender(CreateTenderBasicDataModel model);
        Task<TenderRelationsModel> CreateVROTenderByRelatedAgency(CreateTenderBasicDataModel model);
        Task SendInvitationsAsync(List<InvitationCrModel> suppliersModel);
        Task<bool> SendRequestToApplayForTender(int tenderId, string commericalRegisterNo);
        Task<TenderRelationsModel> UpdateTenderDates(TenderDatesModel tenderDatesModel);
        Task UpdateTenderExtendDates(TenderDatesModel tenderDatesModel, string userRole, string agencyCode);

        #region Two file checking
        Task SendTenderToApproveTechnicalCheckingAsync(int tenderId, string agencyCode);
        Task ApproveTendeTechnicalCheckingAsync(int tenderId, string verificationCode, string agencyCode);
        Task RejectTendeTechnicalCheckingAsync(int tenderId, string rejectionReason, string agencyCode);
        Task ReOpenTendeTechnicalCheckingAsync(int tenderId, string agencyCode);
        Task MoveTenderToFinancialOffersChecking(int tenderId);
        Task SendTenderToApproveFinancailCheckingAsync(int tenderId, string agencyCode);
        Task EndOpenFinantialOffersStage(int tenderId);
        Task ApproveTendeFinancialOpeningAsync(int tenderId, string verificationCode);
        Task RejectTendeFinancialOpeningAsync(int tenderId, string rejectionReason);
        Task ReOpenTendeFinancialOpeningAsync(int tenderId);
        Task ApproveTendeFinancialCheckingAsync(int tenderId, string verificationCode, string agencyCode);
        Task RejectTendeFinancialCheckingAsync(int tenderId, string rejectionReason, string agencyCode);
        Task ReOpenTendeFinancialCheckingAsync(int tenderId, string agencyCode);
        #endregion Two file checking
        Task<QuantitiesTemplateModel> UpdateTenderRelations(TenderRelationsModel model);
        Task UpdateTenderRelationsWithoutQuantityTable(TenderRelationsModel model);
        Task<Invitation> UpdateInvitationStatus(int invitationId, int invitationStatusId, string UserCR);
        Task AcceptInvitationWithFees(TenderFinantialCostModel costModel);
        Task JoinDirectPurchaseTender(int tenderId, string cR);
        Task<Invitation> RejectJoiningRequestStatus(int invitationId, int invitationStatusId, string rejectionReason);
        Task SendInvitationsByEmail(int tenderId, List<string> emails);
        Task SendInvitationBySms(int tenderId, List<string> mobilNoList);
        Task<BasicTenderInfoModel> UpdateTenderAttachmentsAsync(List<TenderAttachmentModel> attachments, int branchId);
        Task<Tender> UpdateTenderAttachmentsAfterApprovment(int tenderId, int tenderStatusId, List<TenderAttachmentModel> attachments, string userRole, int branchId);
        Task<PurchaseModel> PurshaseTender(int tenderId, string CR, string mobile, string email);
        Task<bool> IsTenderPurchasedBySupplier(int tenderId, string CR);
        Task<bool> DeleteTenderFromSupplierTendersFavouriteList(int tenderId, string cr);
        Task<bool> AddTenderToSupplierTendersFavouriteList(int tenderId, string cr);
        Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierFavouritTendersList(TenderSearchCriteria tenderSearchCriteria);
        Task<QueryResult<TenderInformationModel>> GetAllTendersHasEnquiry(TenderSearchCriteria tenderSearchCriteria);
        Task<List<string>> FinancialYear();
        Task<TenderChangeRequest> CreateCancelRequest(TenderRevisionCancelModel tenderRevisionCancelModel);

        Task<TenderChangeRequest> CreateCancelRequestForQualification(TenderRevisionCancelModel tenderRevisionCancelModel);

        #region Direct Purchase offer checking
        #endregion Direct Purchase offer checking

        #region Direct Purchase 

        Task<TenderOffersModel> FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(int tenderId, int userId);
        Task StartDirectPurchaseTenderCheckingOffers(int tenderId, string agencyCode);
        Task SendDirectPurchaseTenderOffersCheckingToApprove(int tenderId, string agencyCode);
        Task SendDirectPurchaseTenderOffersTechnicalCheckingToApprove(int tenderId, string agencyCode);
        Task ApproveDirectPurchaseTenderOffersTechnicalChecking(int tenderId, string verificationCode, string agencyCode);
        Task ApproveDirectPurchaseTenderOffersChecking(int tenderId, string verificationCode, string agencyCode);
        Task RejectDirectPurchaseTenderOffersChecking(int tenderId, string rejectionReason, string agencyCode);
        Task ReopenDirectPurchaseTenderOffersChecking(int tenderId, string agencyCode);
        Task RejectDirectPurchaseTenderOffersTechnicalChecking(int tenderId, string rejectionReason, string agencyCode);
        Task ReopenDirectPurchaseTenderOffersTechnicalChecking(int tenderId, string agencyCode);
        Task SendDirectPurchaseTenderOffersFinanceCheckingToApprove(int tenderId, string agencyCode);
        Task ApproveDirectPurchaseTenderOffersFinanceChecking(int tenderId, string verificationCode, string agencyCode);
        Task RejectDirectPurchaseTenderOffersFinanceChecking(int tenderId, string rejectionReason, string agencyCode);
        Task ReopenDirectPurchaseTenderOffersFinancialChecking(int tenderId, string agencyCode);


        #endregion Direct Purchase

        #region Unit

        Task<QueryResult<BasicTenderModel>> FindTendersForUnitStageBySearchCriteria(TenderSearchCriteria criteria);
        Task OpenTenderByUnitSecretaryAsync(string tenderIdString);
        Task SendTenderByUnitSecretaryForModificationAsync(ReturnTenderToDataEntryFromUnitFormodificationsModel returnTenderToDataEntryFromUnitFormodificationsModel);
        Task ApproveTenderByUnitSecretaryAsync(string tenderIdString, bool IWouldLikeToAttendTheommitte);
        Task RejectTenderByUnitSecretaryAsync(string tenderIdString, string comment);
        Task SendToLevelTwoFromUnitSecretaryLevelOneAsync(string tenderIdString, string userName, string notes);
        Task SendToApproveFromUnitSecretaryToUnitMangerAsync(string tenderIdString);
        Task ReviewTenderByUnitManager(string tenderIdString);
        Task ApproveTenderByUnitManagerAsync(string tenderIdString, string verificationCode);
        Task RejectTenderByUnitManagerAsync(string tenderIdString, string comment);
        Task ReOpenTenderByUnitSecertaryAsync(string tenderIdString);

        Task OpenTenderByUnitSecretaryLevelTwoAsync(string tenderIdString);
        Task SendTenderByUnitSecretaryLevelTwoForModificationAsync(ReturnTenderToDataEntryFromUnitFormodificationsModel returnTenderToDataEntryFromUnitFormodificationsModel);
        Task RejectTenderByUnitSecretaryLevelTwoAsync(string tenderIdString, string comment);
        Task ApproveTenderByUnitSecretaryLevelTwoAsync(string tenderIdString);
        Task SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync(string tenderIdString);
        Task ReOpenTenderByUnitSecertaryLevelAsync(string tenderIdString);
        Task<List<LookupModel>> GetAllUnitUsersByRoleName(string roleName);
        Task SendToNewUserUnitBusinessManagementAsync(string tenderIdString, string userName);

        #endregion

        #endregion 
        Task<QueryResult<TenderMovement>> GetTenderMovementsByTenderId(SimpleTenderSearchCriteria criteria);
        Task ApproveTenderWithInbudget(int tenderId, string verificationCode, bool iagree, bool competitionIsBudgeted);
        Task ApproveTenderRelatedWithVRO(int tenderId);
        Task ApproveTenderVRO(ApproveVROModel approveVROModel, int branchId);
        Task<TenderDetialsReportModel> OpenTenderDetailsReport(int tenderId);
        Task<TenderDetialsReportModel> OpenTenderDetailsReportForVisitor(int tenderId);

        Task<AwardingReportModel> AwardingReport(int tenderId);
        Task SendTenderToApproveCheckingWithNewBiddingRound(BiddingDateTimeViewModel biddingDateTimeViewModel);
        Task SaveCheckingDateTime(CheckingDateTimeViewModel checkingDateTimeViewModel);
        Task SaveFinancialCheckingDateTime(CheckingDateTimeViewModel financialCheckingDateTimeViewModel);
        Task<OfferReportModel> OffersReport(int tenderId);
        Task<PrintTenderReceiptReportModel> PrintTenderReceiptReport(int tenderId, string CommericalRegisterNo);
        Task<CountAndCloseAppliedOffersModel> CountAndCloseAppliedOffers(int tenderId);
        Task RejectTenderExtendDate(int tenderId, string rejectionReason);
        Task RejectTenderForUpdateQuantityTable(int tenderId, string rejectionReason);
        Task RejectTenderUpdateAttachment(int tenderId, string rejectionReason);
        Task CancelTenderExtendDateAsync(int tenderId);
        Task<bool> DeleteAttachmentAsync(string referenceId);
        Task DeleteTenderAttachmentChangesAsync(string referenceId);
        Task DeleteQualificationAttachments(string referenceId);
        Task<bool> ApproveTenderCancelRequest(TenderCancelModel cancelModel);
        Task<bool> ApproveQualificationCancelRequestAsync(int id, string verificationCode, int typeId);
        Task<bool> RejectTenderCancelRequestAsync(int tenderId, string rejectionReason);
        Task RejectTenderCancelRequestWhileTenderApproval(Tender tender, string requestedByRoleName);
        Task<bool> RejectQualificationCancelRequestAsync(int tenderId, string rejectionReason);
        Task<Tender> EditCommittees(EditeCommitteesModel committeeModel);
        Task<Tender> EditSamplesDeliveryAddress(int tenderId, string address);
        Task<Tender> ConvertTenderInvitationToPublic(int tenderId);
        Task<Tender> GetTenderForEditAreas(int tenderId, int branchId);

        Task<Tender> EditAreas(TenderAreasModel areasModel);

        Task<Tender> EditCountries(int tenderId, List<int> countriesIds);

        Task JoinCommitteeToAllTenders(CommitteeTenderModel committeeTenderModel);
        Task JoinCommitteeToTenders(CommitteeTenderModel committeeTenderModel);
        Task JoinCommitteeToBranchTenders(CommitteeTenderModel committeeTenderModel);
        Task CancelJoinCommitteeToTender(CommitteeTenderModel committeeTenderModel);
        Task<List<Tender>> GetRelatedTendersByActivities(int tenderId);
        Task<bool> GetFavouriteTenderSuppliersToApplyOffer();


        Task CancelUpdatesInQuantitiesTablesAsync(int tenderId);
        Task CancelUpdatesInAttachmentsAsync(int tenderId);
        Task<TenderNewsModel> GetTenderNewsByTenderId(int tenderId);

        Task<SupplierFullDataViewModel> GetSupplierInfoByCR(string CR);
        Task<Invitation> ApproveJoiningRequestStatus(int invitationId, int statusId);
        Task<SyncDataWithOldMonafasat> SaveSyncInformation(int tenderId, string requestContent, bool tenderUpdateStatus);

        #region Invitation
        Task SubmitTenderInvitationsStep(int tenderId);
        Task SendInvitationsInTenderCreation(InvitationsInCreateTenderModel invitations);
        Task<InvitationStepModel> GetTenderDetailsForInvitationStep(int tenderId, string CR);
        Task<TenderDatesModel> CreateSecondStageBasicAsync(SecondStageBasicData secondStageBasicModel);
        #endregion


        #region Bidding 
        Task<BiddingTenderDetailsModel> FindBiddingRoundOffersByBiddingRoundId(string tenderStringId, string cR);
        Task<BiddingTenderDetailsModel> FindBiddingRoundDetailsByBiddingRoundId(string tenderStringId, string cR);
        Task EndTenderPedding(string tenderIdString, string biddingRoundIdString, string verificationCode);
        Task StartNewRound(BiddingDateTimeViewModel biddingDateTimeViewModel/*, string verificationCode*/);
        #endregion Bidding

        #region Tender Conditions Template
        Task AddEditIntroductionTemplate(EditConditionTemplateSecondSectionModel model, int branchId);

        Task AddEditLocalContentTemplate(LocalContentModel model, int branchId);

        

        Task AddEditPreparingOffersTemplate(EditConditionTemplateThridSectionModel model, int branchId);

        Task AddEditTechnicalDeclerationsTemplate(EditConditionTemplateSeventhSectionModel model, int branchId);
        Task AddEditDescriptionAndConditionsTemplate(EditConditionTemplateEighthSectionModel model, int branchId);
        #endregion

        Task<QueryResult<TenderCheckOfferModel>> GetOffersForCheckByTenderIdForNormalCheckingAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria);

        #region VRO
        Task<VROTenderOffersModel> FindVROTenderOfferDetailsByTenderIdForCheckStage(string tenderIdString, int userId);
        Task StartVROTenderOffersCheckingAsync(string tenderIdString);
        Task StartVROTenderOffersFinancialCheckingAsync(string tenderIdString, decimal estimatedValue);
        Task SendVROTenderOffersTechnicalCheckingToApproveAsync(string tenderIdString);
        Task ReopenVROTenderOffersTechnicalCheckingAsync(string tenderIdString);
        Task ApproveVROTenderOffersTechnicalCheckingAsync(string tenderIdString);
        Task RejectVROTenderOffersTechnicalCheckingAsync(string tenderIdString, string rejectionReason);
        Task SendVROTenderOffersFinanceCheckingToApproveAsync(string tenderIdString);
        Task ReopenVROTenderOffersFinancialCheckingAsync(string tenderIdString);
        Task ApproveVROTenderOffersFinanceCheckingAsync(string tenderIdString);
        Task RejectVROTenderOffersFinanceCheckingAsync(string tenderIdString, string rejectionReason);


        #endregion VRo
        Task<QueryResult<NegotiationOnTenderModel>> GetAllTenderhasNegotiationbySearchCretriaForUnitUsers(NegotiationOnTenderSearchCriteriaModel criteria);

        Task<string> GetEmptyForm(int formid, int tenderId, int templateYears, string tableName);
        Task<long> UpdateTableName(long tableId, int tenderId, string tableName);
        Task<long> UpdateTableChangesName(long tableId, int tenderId, string tableName);
        Task UpdateHasAlternative(int tenderId, bool hasAlternative);
        Task DeleteTable(int tableId, int tenderId);
        Task RemoveUnRegisterSupplier(int tenderId, string crNumber);

        Task DeleteTableChange(int tableId, int tenderId, bool isNewTable);
        Task ChangeHasAlternativeOffer(int tenderId, bool hasAlternativeOffer);
        Task DeleteQuantityTableChangeRequest(int tenderId);
        Task DeleteExistingTableChange(long tableId, int tenderId);
        #region Unregistered-Suppliers-Invitations 
        Task SendInvitationsForUnRegisteredSupplier(UnRegisteredSuppliersInvitationsModel invitationsModel);
        Task<ValidationReturndTemplate> ValidateQuantitiesData(Dictionary<string, string> AuthorList);
        Task<ValidationReturndTemplate> ValidateQuantitiesChangesData(Dictionary<string, string> AuthorList);
        Task<ValidationReturndTemplate> DeleteTenderQuantityItem(Dictionary<string, string> authorList);
        Task<ValidationReturndTemplate> DeleteTenderQuantityItemChanges(Dictionary<string, string> authorList);
        #endregion
        Task<QueryResult<TableModel>> GetTenderTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<QueryResult<TableModel>> GetTenderTableQuantityItemsChanges(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel);
        Task<long> AddNewTable(int formid, int tenderId, int templateYears, string tableName);
        Task<int> GetQauntityTableVersionId();
        Task<long> AddNewTableChange(int formid, int tenderId, int templateYears, string tableName);
        Task<List<TenderQuantityItemDTO>> ExportTenderTableQuantityItems(int tableId, bool isNewChange);
        Task ImportTenderTableQuantityItems(UploadTableExcelModel model);
        Task<QueryResult<TenderCheckOfferModel>> GetVROOffersForCheckByTenderIdForNormalCheckingAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria);
        Task<QuantityTableStepDTO> FindTenderQuantityItemsById(int tenderId, bool isReadOnly);
        Task<QuantityTableStepDTO> FindTenderQuantityItemsByIdForCreation(int tenderId, int branchId, bool isReadOnly);
        Task<QuantityTableStepDTO> FindTenderQuantityItemsChangesById(int tenderId);


        Task RemoveAttachmentChangeById(int attachmentId);

        #region Added Value

        Task<List<TenderTypeWithAddedValueModel>> GetAllTenderTypesAddedValue();

        Task UpdateTenderTypeAddedValueAsync(List<TenderTypeWithAddedValueModel> tenderTypes);

        #endregion

        Task<QuantityTableModelForExcel> GenerateQuantityTableTemplateExcel(int stageId, int formId, int yearsCount);
        Task<ExcelHeader> GenerateQuantityTableTemplateExcel_New(int stageId, int formId, int yearsCount);
        Task<List<MandatoryListForExportModel>> GetAllMandatoryListForExport();

        Task<TenderDetailsForAppliedSuppliersReportModel> GetTenderDetailsForAppliedSuppliersReport(int tenderId);
        Task<QuantityTableStepDTO> FindTenderQuantityItemsByFormIds(int tenderId, bool isReadOnly);
        Task<QuantityTableStepDTO> FindTenderQuantityItemsChangesReadOnlyById(int tenderId);
        Task UpdateTenderLocalContentValues(UpdateTenderNationalProductValuesViewModel viewModel);

        Task<bool> EmarketPlace(List<int> offerIds);
        Task<TenderDetailsCheckingStageModel> GetTenderDetailsForCheckingStage(int tenderId, int userId, string agencyCode);

        #region LCG
        Task<QueryResult<TenderInfo>> GetTenderList(string supplierId, int pageSize, int pageNumber);
        Task<TenderInfo> GetTenderInfo(string tenderReferenceId);
        #endregion

        Task<bool> CheckIfActivityCanHasTawreed(ActivityVersionModel activityVersionModel);
        Task<int> GetCurrentTenderActivityVersion(int tenderId);
    }
}