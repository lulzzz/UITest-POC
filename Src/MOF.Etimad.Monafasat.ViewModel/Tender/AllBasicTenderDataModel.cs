using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AllBasicTenderDataModel
    {
        public bool ShowInvitationTab { get; set; }
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "SelectTenderType")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public string TenderTypeName { get; set; }
        public List<LookupModel> TenderTypes { set; get; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "RequiredTenderTypeSelectionReason")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ReasonForTenderTypeSelection")]
        public int? ReasonForPurchaseTenderTypeId { get; set; }
        public string ReasonForPurchaseTenderTypeName { get; set; }
        public List<LookupModel> ReasonForPurchaseTenderType { set; get; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "RequiredTenderTypeSelectionReason")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ReasonForTenderTypeSelection")]
        public int? ReasonForLimitedTenderTypeId { get; set; }
        public string ReasonForLimitedTenderTypeName { get; set; }
        public List<LookupModel> ReasonForLimitedTenderType { set; get; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "RequiredOtherReason")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Other")]
        public string TenderTypeOtherSelectionReason { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PreQualification")]
        public int? PreQualificationId { get; set; }
        public string PreQualificationName { get; set; }
        public List<LookupModel> PreQualifications { set; get; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "RequiredPurchaseCommittee")]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "SelectDirectPurchaseCommittee")]
        public int? DirectPurchaseCommitteeId { get; set; }
        public string DirectPurchaseCommitteeName { get; set; }
        public List<CommitteeModel> DirectPurchaseCommittee { set; get; }
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "OfferPresentationWay")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferPresentationWay")]
        public int OfferPresentationWayId { get; set; }
        public string OfferPresentationWayName { get; set; }
        public List<LookupModel> OfferPresentationWay { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InitialGuranteePresentationAddress")]
        public string InitialGuaranteeAddress { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PublishedBy")]
        public string CreatedBy { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "PleaseEnterRealValue")]
        [Range(1, 9999999999999999.99, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EstimatedValueCannotBeZero")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValue")]
        public decimal EstimatedValue { get; set; }
        public string EstimatedValueString { get; set; }
        public int? TenderFirstStageId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "HasPrequalification")]
        public bool HasQualification { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "NeddedInitialGurantee")]
        public bool NeedInitialGuarantee { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "InvitationType")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationType")]
        public int? InvitationTypeId { get; set; }
        public string InvitationTypeName { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterConditionBook")]
        [Range(0.0, 9999999999999999999, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "InvalidNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBookletPrice")]
        public Decimal? ConditionsBookletPrice { get; set; }

        [StringLength(500)]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterTenderName")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterTenderNumber")]
        [StringLength(100)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumberForAgency")]
        public string TenderNumber { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }
        public string TenderStatusName { get; set; }
        public string TenderStatusIdString { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterTenderPurpose")]
        [StringLength(1024, MinimumLength = 40, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "PurposeValiation")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderPurpose")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "TechnicalOrganizationRequired")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int TechnicalOrganizationId { get; set; }
        public string TechnicalOrganizationName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }
        public string OffersOpeningCommitteeName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }
        public string OffersCheckingCommitteeName { get; set; }
        public List<CommitteeModel> TechnicalCommittees { set; get; }
        public List<CommitteeModel> OfferOpenningCommittees { set; get; }
        public List<CommitteeModel> OfferCheckingCommittees { set; get; }
        public bool? IsCommitteeUpdate { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        public bool? SupplierNeedSample { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "SamplesDeliveryAddress")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        public string SamplesDeliveryAddress { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        public bool CurrentGreaterEnquiryDate { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersOpeningAddress")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public int? OffersOpeningAddressId { get; set; }
        public List<AddressModel> OffersOpeningAddressList { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public string OffersOpeningAddress { get; set; }


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
        public string ReferenceNumber { get; set; }
        public int RemainingDays { get; set; }
        public int RemainingHours { get; set; }
        public int RemainingMins { get; set; }

        public bool CanRenderEstimatedData
        {
            get
            {
                if (TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing || TenderStatusId == (int)Enums.TenderStatus.Established || TenderStatusId == (int)Enums.TenderStatus.Pending || TenderStatusId == (int)Enums.TenderStatus.Approved || TenderStatusId == (int)Enums.TenderStatus.Rejected || TenderStatusId == (int)Enums.TenderStatus.Canceled || TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending || TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected
              || TenderStatusId == (int)Enums.TenderStatus.UnitStaging || TenderStatusId == (int)Enums.TenderStatus.RejectedFromUnit)
                {
                    return false;
                }
                return true;
            }
        }

        public int? TenderUnitStatusId { get; set; }
        public string DeliveryDate { get; set; }
    }

}
