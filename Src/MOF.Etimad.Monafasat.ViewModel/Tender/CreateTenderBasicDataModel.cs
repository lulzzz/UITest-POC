using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    /// <summary>
    /// Used for create and update first step (basic data) of the tender
    /// </summary>
    public class CreateTenderBasicDataModel
    {
        [JsonProperty]
        public string AgencyBudgetProjectNumber { get; set; }
        public List<AgencyBudgetNumberModel> AgencyBudgetNumbers { set; get; }

        public string EstimatedValueValidate { get; set; }
        public int TenderId { get; set; }
        public int? QuantityTableVersionId { get; set; }
        public bool ShowInvitationTab { get; set; }
        public bool IsUnitAgency { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ReferenceTenderNumber")]
        public string ReferenceNumber { get; set; }
        public string TenderIdString { get; set; }
        public int? PreviousFramWorkId { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "SelectTenderType")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public string TenderTypeName { get; set; }
        public List<LookupModel> TenderTypes { set; get; }

        // [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "RequiredTenderTypeSelectionReason")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ReasonForTenderTypeSelection")]
        public int? ReasonForPurchaseTenderTypeId { get; set; }
        public string ReasonForPurchaseTenderTypeName { get; set; }
        public List<LookupModel> ReasonForPurchaseTenderType { set; get; }

        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "RequiredTenderTypeSelectionReason")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ReasonForTenderTypeSelection")]
        public int? ReasonForLimitedTenderTypeId { get; set; }
        public string ReasonForLimitedTenderTypeName { get; set; }
        public List<LookupModel> ReasonForLimitedTenderType { set; get; }
        public List<LookupModel> SpendingCategories { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Other")]
        public string TenderTypeOtherSelectionReason { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PreQualification")]
        public int? PreQualificationId { get; set; }
        public string PreQualificationName { get; set; }
        public List<LookupModel> PreQualifications { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SelectDirectPurchaseCommittee")]
        public int? DirectPurchaseCommitteeId { get; set; }
        public string DirectPurchaseCommitteeName { get; set; }
        public List<CommitteeModel> DirectPurchaseCommittees { set; get; }

        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "VROCommittee")]
        public int? VROCommitteeId { get; set; }
        public string VROCommitteeName { get; set; }
        public int? TenderCreatedByType { get; set; }
        public bool IsAgencyRelatedByVRO { get; set; }
        public List<CommitteeModel> VROCommittees { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderAgreementAgencies")]
        public List<string> TenderAgreementAgencyIDs { set; get; } = new List<string>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderAgreementAgencies")]
        //public List<GovAgencyModel> GovAgencies { get; set; }
        public List<LookupModel> GovAgencies { get; set; }
        [Range(0.0, 9999999999999999999, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "InvalidNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Months")]
        public int? AgreementMonthes { get; set; }
        [Range(0.0, 9999999999999999999, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "InvalidNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AgreementDays")]
        public int? AgreementDays { get; set; }
        [Range(0.0, 9999999999999999999, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "InvalidNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AgreementYears")]
        public int? AgreementYears { get; set; }

        [Range(0.0, 9999999999999999999, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "InvalidNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "NumberOfWinners")]
        public int? NumberOfWinners { get; set; }

        [Range(0.0, 9999999999999999999, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "InvalidNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "BonusValue")]
        public decimal? BonusValue { get; set; }

        // [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "OfferPresentationWay")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferPresentationWay")]
        public int OfferPresentationWayId { get; set; }
        public string OfferPresentationWayName { get; set; }
        public List<LookupModel> OfferPresentationWay { set; get; }

        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InitialGuranteePresentationAddress")]
        //public string InitialGuaranteeAddress { get; set; }

        // [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "PleaseEnterRealValue")]
        //[Range(0.0, 9999999999999999999, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EstimatedValueCannotBeZero")]
        //  [Range(1, 9999999999999999.99, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EstimatedValueCannotBeZero")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValue")]
        public decimal? EstimatedValue { get; set; }
        public string EstimatedValueString { get; set; }
        public int? TenderFirstStageId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PublishedBy")]
        public string CreatedBy { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "HasPrequalification")]
        public bool HasQualification { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "IsLinkedToAnnouncement")]
        public bool IsLinkedToAnnouncement { get; set; }


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PreAnnouncement")]
        public int? PreAnnouncementId { get; set; }

        [Display(ResourceType = typeof(@MOF.Etimad.Monafasat.Resources.AnnouncementSupplierTemplateResources.DisplayInputs), Name = "AnnouncementSuppliersTemplate")]
        public int? AnnouncementTemplateId { get; set; }
        public string PreAnnouncementName { get; set; }
        public List<LookupModel> PreAnnouncementList { set; get; }

        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AlternativeOffer")]
        //public bool HasAlternativeOffer { get; set; }

        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "NeddedInitialGurantee")]
        //public bool NeedInitialGuarantee { get; set; }


        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "InvitationType")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationType")]
        public int? InvitationTypeId { get; set; }
        public string InvitationTypeName { get; set; }

        // [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterConditionBook")]
        [Range(0.0, 9999999999999999999, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "InvalidNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBookletPrice")]
        public Decimal? ConditionsBookletPrice { get; set; }

        [StringLength(500)]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterTenderName")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationType")]
        public Boolean InvitationCheck { get; set; }


        [StringLength(100)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumberForAgency")]
        public string TenderNumber { get; set; }


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }
        public string TenderStatusName { get; set; }
        public string TenderStatusIdString { get; set; }
        public bool IsVRO { get; set; }
        public bool IsExceptedAgency { get; set; }
        public List<int> PurchaseMethods { get; set; }
        public string PurchaseMethodString { get; set; }

        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public bool? IsFramWorkRecreation { get; set; } = null;

        public int BranchId { get; set; }

        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        //public bool? SupplierNeedSample { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AgreementType")]
        public int? AgreementTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        public bool? SupplierNeedSample { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        public string SamplesDeliveryAddress { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterTenderPurpose")]
        [StringLength(1024, MinimumLength = 40, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "PurposeValiation")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderPurpose")]
        public string Purpose { get; set; }
        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "TechnicalOrganizationRequired")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int? TechnicalOrganizationId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }
        public int PreQualificationCommitteeId { get; set; }
        public List<CommitteeModel> TechnicalCommittees { set; get; }
        public List<CommitteeModel> OfferOpenningCommittees { set; get; }
        public List<CommitteeModel> OfferCheckingCommittees { set; get; }

        public bool? IsCommitteeUpdate { get; set; }

        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "SamplesDeliveryAddress")]
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        //public string SamplesDeliveryAddress { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        public bool CurrentGreaterEnquiryDate { get; set; }

        // [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersOpeningAddress")]
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        //public int? OffersOpeningAddressId { get; set; }
        //public List<AddressModel> OffersOpeningAddressList { get; set; }
        public List<CommitteeModel> Committies { set; get; }
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        //public string OffersOpeningAddress { get; set; }


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
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

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SpendingCategory")]
        public int? SpendingCategoryId { get; set; }

        public int? TenderUnitStatusId { get; set; }
        public bool IsAgancyHasTechnicalCommittee { get; set; }
        public bool BranchHasTechnicalCommittees { get; set; }
        public bool NoTechnicalItems { get; set; }
        public string InitialGuaranteeAddress { get; set; }
        public decimal? InitialGuaranteePercentage { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DirectPurchaseProcedureType")]
        public bool? IsLowBudgetDirectPurchase { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DirectPurchaseCommitteeMemberId")]
        public int? DirectPurchaseCommitteeMemberId { get; set; }

        public List<CommitteeUserModel> PurchaseCommitteeMembers { get; set; }

        public int CurrentVersionId { get; set; }
    }
}
