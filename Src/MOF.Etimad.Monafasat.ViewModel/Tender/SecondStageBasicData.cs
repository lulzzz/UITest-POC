using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SecondStageBasicData
    {
        [JsonProperty]
        public string AgencyBudgetProjectNumber { get; set; }
        public List<AgencyBudgetNumberModel> AgencyBudgetNumber { set; get; }
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public string TenderTypeName { get; set; }
        public List<LookupModel> TenderTypes { set; get; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferPresentationWay")]
        public int? OfferPresentationWayId { get; set; }
        //public List<LookupModel> OfferPresentationWay { set; get; }
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AlternativeOffer")]
        //public bool HasAlternativeOffer { get; set; }
        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.Messages)), ErrorMessageResourceName = "EnterInitialGuranteePresentationAddress")] 
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InitialGuranteePresentationAddress")]
        //public string InitialGuaranteeAddress { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PublishedBy")]
        public string CreatedBy { get; set; }
        public int? TenderFirstStageId { get; set; }
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "NeddedInitialGurantee")]
        //public bool NeedInitialGuarantee { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumberForAgency")]
        public string TenderNumber { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderPurpose")]
        public string Purpose { get; set; }


        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "PleaseEnterRealValue")]
        [Range(1, 9999999999999999.99, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EstimatedValueCannotBeZero")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValue")]
        public decimal EstimatedValue { get; set; }
        public string EstimatedValueString { get; set; }


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int TechnicalOrganizationId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }
        public List<CommitteeModel> TechnicalCommittees { set; get; }
        public List<CommitteeModel> OfferOpenningCommittees { set; get; }
        public List<CommitteeModel> OfferCheckingCommittees { set; get; }

        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        //public bool? SupplierNeedSample { get; set; }

        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.DisplayInputs)), ErrorMessageResourceName = "SamplesDeliveryAddress")]
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        //public string SamplesDeliveryAddress { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersOpeningAddress")]
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        //public int? OffersOpeningAddressId { get; set; }
        //public List<AddressModel> OffersOpeningAddressList { get; set; }

        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        //public string OffersOpeningAddress { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ReferenceTenderNumber")]
        public string ReferenceNumber { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBookletPrice")]
        public Decimal? ConditionsBookletPrice { get; set; }
        public bool IsAgancyHasTechnicalCommittee { get; set; }
        public string InitialGuaranteeAddress { get; set; }
        public decimal? InitialGuaranteePercentage { get; set; }
        public int? QuantityTableVersionId { get; set; }
    }
}
