using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderOffersModel
    {
        public string postQualificationTenderIdString;

        
        public int VersionId { get; set; }

        public int TenderId { get; set; }
        public bool? IsPasssPreqaulification { get; set; }
        public bool? IsTenderHasAwardingValue { get; set; }
        public bool IsOldTender { get; set; }
        public bool? IsPreqaulificationExpired { get; set; }

        public bool IsShowAwardingDiscountPercentage { get; set; }

        
        public string TenderIdString { get; set; }
        public int TenderTypeId { get; set; }
        //[Display(Name = "إسم المنافسة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }
        //[Display(Name = "رقم المنافسة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumber")]
        public string TenderNumber { get; set; }
        //[Display(Name = "الرقم المرجعى")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderRefrenceNumber")]
        public string TenderRefrenceNumber { get; set; }
        public string TenderTypeName { get; set; }
        public string ExcutionPlace { get; set; }
        //[Display(Name = "حالة المنافسة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }
        //[Display(Name = "مكان التنفيذ")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }
        //[Display(Name = "عدد المشترين")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "BuyersCount")]
        public int InvitationsCount { get; set; }
        //[Display(Name = "عدد العروض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersCount")]
        public int OffersCount { get; set; }
        //[Display(Name = "القيمة التقديرية")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValue")]
        public Decimal? EstimatedValue { get; set; }
        //[Display(Name = "القيمة التقديرية (كتابة)")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValueWritten")]
        public string EstimatedValueText { get; set; }
        //[Display(Name = "سبب الرفض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "RejectionReason")]
        public string RejectionReason { get; set; }
        public List<string> TenderAreaNameList { get; set; }
        public bool? TenderAwardingType { get; set; }
        public int BuyersCount { get; set; }
        public bool IsValidToSubmit { get; set; }
        public bool IsValidToCheck { get; set; }
        public string AgencyCode { get; set; }
        public DateTime? OffersOpeningDate { get; set; }
        public int OfferPresentationWayId { get; set; }
        public bool IsTenderTechnicalChecked { get; set; }
        public bool IsTenderFinancialChecked { get; set; }
        public bool IsValidToGoToFinancailCheck { get; set; }
        public bool IsOldFlow { get; set; }
        public int CombinedCount { get; set; }
        public int CombinedDetailsCount { get; set; }

        public int UnOpenCombinedSuppliersOffers { get; set; }
        public int? AwardingStoppingPeriod { get; set; }
        public string AwardingReportFileNameId { get; set; }
        public string AwardingReportFileName { get; set; }
        public bool DoIHaveApprovedSideAction { get; set; }
        public bool DoIHavePendingSideAction { get; set; }
        public bool IsExtendOfferValidityFinished { get; set; }
        public int? OfferPresentationWay { get; set; }
        public int? NumberOfWinners { get; set; }
        public decimal? BonusValue { get; set; }
        public decimal? AwardingDiscountPercentage { get; set; }
        public int? AwardingMonths { get; set; }
        public string FinalGuaranteeDeliveryAddress { get; set; }

        public bool? HasGuarantee { get; set; }
        public bool HasCheckingDate { get; set; }
        public bool CanAuthorityHolderDoAction { get; set; } = false;
        public DateTime? CheckingDate { get; set; }
        public bool CheckingDateSet { get; set; }
        public bool IsValidToCheckFinancial { get; set; }
        public int UncheckedCombinedSuppliers { get; set; }
        public bool IsAllOffersNotIdenticallyMatched { get; set; }
        public DateTime? BiddingRoundStartDate { get; set; }
        public DateTime? BiddingRoundEndDate { get; set; }
        public bool ContainSupply { get; set; }
        public bool NPCalcHaveBeenDone { get; set; }
        public bool showUnacceptedFinanciallyMessage { get; set; }
        public int? DirectPurchaseReasonId { get; set; }
        public bool IsTenderHasStopPeriod { get; set; }
        public bool? IsLowBudgetDirectPurchase { get; set; }
        public int? DirectPurchaseMemberId { get; set; }
        public bool CanApproveInitially { get; set; }
        public bool? Containtawreed { get; set; }
        public bool CanInsertAwardingData { get; set; }
        public int? CancelRequestId { get; set; }
        public bool isOffersPreferenceChecked { get; set; }
        public bool isApplyOfferPreference { get; set; }
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

        public bool TenderHasPendingStatusOpenStage
        {
            get
            {
                return (TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending
                    || TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending
                    );
            }
        }

        public bool TenderHasPendingStatusAwardingStage
        {
            get
            {
                return (TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending
                    || TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
                    );
            }
        }

    }
}
