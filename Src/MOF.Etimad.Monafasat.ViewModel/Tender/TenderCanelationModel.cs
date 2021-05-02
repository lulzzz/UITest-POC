using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderCanelationModel
    {

        public List<SupplierModel> SupplierViolators { get; set; }
        public bool CanAuditCancelRequest { get; set; }
        public bool IsLowBudgetAndAssignedMember { get; set; }
        public bool IsLowBudgetTender { get; set; }
        public bool CanCreateCancelRequest { get; set; }
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

        public string CancelationReasonDescription { get; set; }
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
        public string TenderTypeName { get; set; }
        public string TenderStatusName { get; set; }


        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
        public string AgencyCode { get; set; }
        public string ExcutionPlace { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }

        public List<int> TenderChangeRequestIds { get; set; }
        public List<int> TenderChangeRequestIdsForAuditor { get; set; }
        public List<int> TenderChangeRequestIdsForDataEntry { get; set; }
        public List<int> ChangeRequestStatusIds { get; set; }

        public List<LookupModel> CancelationReasons { get; set; }

        public int TenderChangeRequestId { get; set; }
        public int ChangeRequestStatusId { get; set; }
        public string ChangeRequestStatusString { set; get; }
        public int ChangeRequestTypeId { get; set; }
        public string ChangeRequestTypeString { set; get; }
        public string RequestedByRoleName { get; set; }
        public string RejectionReason { get; set; }

        public int? CancelationReasonId { get; set; }
        public List<LookupModel> BuyerSuppliers { get; set; }
        public List<LookupModel> SuppliersHaveOffers { get; set; }
        public TenderChangeRequestModel TenderChangeRequestModel { get; set; }
    }

}
