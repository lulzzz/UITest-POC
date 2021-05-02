using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderDetailsCheckingStageModel
    {
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string TenderRefrenceNumber { get; set; }
        public bool? InsideKSA { get; set; }
        public int TenderTypeId { get; set; }
        public Decimal? EstimatedValue { get; set; }
        public string EstimatedValueText { get; set; }
        public int BuyersCount { get; set; }
        public int OffersCount { get; set; }
        public bool IsAllOffersNotIdenticallyMatched { get; set; }
        public DateTime? BiddingRoundStartDate { get; set; }
        public DateTime? BiddingRoundEndDate { get; set; }
        public int TenderStatusId { get; set; }
        public string RejectionReason { get; set; }
        public string TenderIdString { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public bool IsTenderFinancialChecked { get; set; }
        public bool IsValidToGoToFinancailCheck { get; set; }
        public bool IsValidToCheck { get; set; }
        public bool IsTenderTechnicalChecked { get; set; }
        public bool CheckingDateSet { get; set; }
        public bool FinancialCheckingDateSet { get; set; }
        public bool ContainSupply { get; set; }
        public bool NPCalcHaveBeenDone { get; set; }
        public bool isOldFlow { get; set; }
        public bool canEnterOfferData { get; set; }
        public bool canViewOfferPrice { get; set; }
        public bool showUnacceptedFinanciallyMessage { get; set; }

        public int? CancelRequestId { get; set; }
        public bool TenderHasPendingStatus
        {
            get
            {
                return (TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending
                    || TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedPending
                    || TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending
                    || TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending
                    || TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending
                    );
            }
        }
        
        public bool isOffersPreferenceChecked { get; set; }
        public bool isApplyOfferPreference { get; set; }
    }
}
