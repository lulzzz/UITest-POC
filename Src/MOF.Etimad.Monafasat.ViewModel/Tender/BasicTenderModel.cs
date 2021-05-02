using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BasicTenderModel
    {
        public bool HasPreQualification { get; set; }
        public int TenderId { get; set; }
        public int? PreQualificationId { get; set; }
        public string TenderIdString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        [Required]
        public int TenderTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationType")]
        public int? InvitationTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBookletPrice")]
        public Decimal? ConditionsBookletPrice { get; set; }

        [Required]
        [StringLength(500)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }
        [Required]
        [StringLength(100)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumber")]
        public string TenderNumber { get; set; }
        public string TenderReferenceNumber { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
        [Required]
        [StringLength(1024, ErrorMessageResourceName = "PurposeValiation", ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), MinimumLength = 40)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderPurpose")]
        public string Purpose { get; set; }
        [Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int TechnicalOrganizationId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime? SubmitionDate { get; set; }

        public DateTime CreatedDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }
        public string OffersOpeningTime { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        public DateTime? OffersOpeningDate { get; set; }

        public string OffersCheckTime { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersCheckingDate")]
        public DateTime? OffersCheckingDate { get; set; }

        public string TenderTypeName { get; set; }
        public string RequestedByRoleName { get; set; }
        public string InvitationTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public string TechnicalOrganizationName { get; set; }
        public string OffersOpeningCommitteeName { get; set; } //لجنة فتح العروض
        public string OffersCheckingCommitteeName { get; set; } //لجنة فحص العروض
        public List<CommitteeModel> TechnicalCommittees { set; get; }
        public List<CommitteeModel> OfferOpenningCommittees { set; get; }
        public List<CommitteeModel> OfferCheckingCommittees { set; get; }
        [StringLength(1024)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ActivityDescription")]
        public string ActivityDescription { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        public bool? SupplierNeedSample { get; set; }
        [StringLength(1024)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        public string SamplesDeliveryAddress { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
        public string AgencyName { get; set; }
        public string BranchName { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public bool canViewSuppliersReportFinancialSupervisor { get; set; }
        public int OfferPresentationWayId { get; set; }

        public DateTime? WithdrawalDate { get; set; }
        public ClaimsPrincipal User { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValue")]
        public Decimal? EstimatedValue { get; set; }
        public string EstimatedValueString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }

        public int? InvitationStatusId { get; set; }
        public int? OffersCount { get; set; }
        public bool CurrentGreaterEnquiryDate { get; set; }
        public bool? IsCommitteeUpdate { get; set; }
        public bool? CanSupplierBuyBook { get; set; }
        public string InvitationStatus { get; set; }
        public List<SupplierInvitationModel> Invitations { get; set; }
        public List<PurchaseModel> ConditionsBooklets { get; set; }
        public bool IsFavouriteTender { get; set; }
        public bool HasEnquiry { get; set; }
        public int EnquiriesCount { get; set; }
        public int EnquiriesCountForTechnical { get; set; }
        public int EnquiriesCountForAuditor { get; set; }
        public int PendingEnquiryReplyCount { get; set; }
        public int ApprovedEnquiryReplyCount { get; set; }
        public DateTime? EnquirySendingDate { get; set; }
        public int UserCommitteeType { get; set; }
        public int? UserCommitteeId { get; set; }
        public int VerificationTypeId { get; set; }
        public string SubmitionDateHijri { get; set; }
        public string CreatedDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }
        public string OffersCheckingDateHijri { get; set; }
        public bool IsPurchased { get; set; }
        public bool SupplierHasOfffer { get; set; }
        public int SubscriptionTypeId { get; set; }
        public decimal? AwardingValue { get; set; }
        public List<int> TenderChangeRequestIds { get; set; }
        public List<int> TenderChangeRequestIdsForAuditor { get; set; }
        public List<int> TenderChangeRequestIdsForDataEntry { get; set; }
        public List<int> ChangeRequestStatusIds { get; set; }
        public List<string> ChangeRequestStatusNames { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public int QuantitiesTableStatus { get; set; }
        public int AttachmentsStatus { get; set; }
        public int ExtendDatesStatus { get; set; }
        public string ChangeRequestedBy { get; set; }
        public int CancelRequestStatus { get; set; }
        public bool HasSecondStage { get; set; }
        public bool CanRecreateFramWork { get; set; }

        public bool PerqualificationsTenderStatus { get; set; }
        public int RemainingDays
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Days : 0;

            }
        }
        public int RemainingHours
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Hours : 0;
            }
        }
        public int RemainingMins
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Minutes : 0;

            }
        }
        public DateTime? DayBeforeLastOfferPresentationDate
        {
            get
            {
                return LastOfferPresentationDate.HasValue ? LastOfferPresentationDate.Value.AddDays(-1) : DateTime.Now;
            }
        }

        public List<TenderMovement> tenderMovements { get; set; } = new List<TenderMovement>();
        public int? UnitStatusId { get; set; }
        public bool? IsUnitSecreteryAccepted { get; set; }
        public bool CanUnitDoAnyAction { get; set; }
        public bool HasAcceptedOffers { get; set; }
    }

}
