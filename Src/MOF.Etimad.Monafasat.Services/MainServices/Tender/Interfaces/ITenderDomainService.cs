using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ITenderDomainService
    {
        void HasAccessToTender(AllBasicTenderDataModel allBasicTenderDataModel);
        void IsValidToCreateTender(CreateTenderBasicDataModel tender);

        void IsValidToUpdateModel(Tender tender);
        void IsValidToUpdateDate(TenderDatesModel tenderDatesModel, Tender tender);
        void IsValidToUpdateExtendDate(TenderDatesModel tenderDatesModel, Tender tender);
        void IsValidVerificationCode(DateTime expireDate, string verificationCodeInput, string verificationCode);

        bool CheckIfCanCancelTender(Tender tender);

        void IsValidToStartCheckingTender(Tender tender);
        #region Two files checking

        void IsValidToApproveOrRejectTenderFinancialOpening(Tender tender, string agencyCode);
        void IsValidToReOpenTenderFinancialOpening(Tender tender, string agencyCode);
        #endregion Two files checking

        Task<bool> IsTenderHasActiveNegotiationRequests(int tenderId);
        Task<List<PostQualificationSuppliersInvitations>> GetPostqualificationOnTender(int tenderId);

        Task<bool> IsTenderHasActiveExtendOfferValidityRequests(int tenderId);

        void IsValidToUpdateTender(Tender tender, string agencyCode);

        void CheckIfQuantityTablesHasItems(Tender tender);
        void CheckIfAllE3ashaQuantityTablesHasItems(Tender tender, Func<int, int, Task<List<int>>> GetTemplateFormsByTemplateId);
        ContractResponseViewModel BuildContractResponseViewModel(Tender tender, string agencyCode);

        #region
        void IsValidToGetTenderOfferDetailsForOppeningStage(TenderOffersModel tenderOffersModel, string agencyId);

        #endregion

        #region Unit
        void IsValidToOpenUnitStageByUnitSecretary(Tender tender);
        void IsValidToOpenUnitStageByUnitSecretaryLevelTwo(Tender tender);
        void IsValidToSendToLevelTwoFromUnitSecretaryLevelOne(Tender tender, string userName);
        void IsValidToUpdateApproveTenderByUnitSecretary(Tender tender);
        void IsValidToUpdateApproveTenderByUnitSecretaryLevelTwo(Tender tender);
        void IsValidToReturnTenderToDataEntryForEdit(Tender tender);
        void IsValidToReturnTenderToDataEntryForEditLevelTwo(Tender tender);
        void IsValidToUpdateRejectTenderByUnitSecretary(Tender tender, string comment);
        void IsValidToUpdateRejectTenderByUnitSecretaryLevelTwo(Tender tender, string comment);
        void IsValidToSendToApproveByUnitManager(Tender tender);
        void IsValidToSendToApproveFromLevelToByUnitManager(Tender tender);
        void IsValidToReviewTenderByUnitManager(Tender tender);
        void IsValidToUpdateApproveTenderByUnitManager(Tender tender);
        void IsValidToUpdateRejectTenderByUnitManager(Tender tender, string comment);
        void IsValidToReOpenTenderByUnitSecretary(Tender tender);
        void IsValidToSendToNewUserUnitBusinessManagement(Tender tender, UserProfile user);
        #endregion

        #region Bidding
        void IsValidToSendToApproveTenderCheckingWithBidding(Tender tender, string agencyId, BiddingDateTimeViewModel biddingDateTimeViewModel);
        void IsValidToGetTenderBiddings(Tender tender, string cR);
        void IsValidToEndBiddingRound(BiddingRound biddingRound);
        void IsValidToStartNewRouind(Tender tender);
        void IsValidToAddNewBiddingRound(Tender tender, string agencyId, BiddingDateTimeViewModel biddingDateTimeViewModel);
        #endregion Bidding
         

        #region Added Value
        void IsValidToUpdateTenderTypeCosts(TenderTypeWithAddedValueModel tenderType);
        #endregion

        Task<List<Offer>> GetReceivedOffersByTenderId(int tenderId);
        Task<List<Offer>> GetReceivedAndIdenticalOffersByTenderId(int tenderId);
        Task<List<Offer>> GetNotIdenticalOffersByTenderId(int tenderId);

        Task<List<Offer>> GetFinancialAcceeptedOffersByTenderId(int tenderId);
        Task<List<Offer>> GetFinancialRejectedOffersByTenderId(int tenderId);
        Task CheckIfUserCanAccessLowBudgetTender(bool? isTenderLowBudget, int? tenderDirectPurchasememberId, int userId);
    }
}
