using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderRelationsModel
    { 
        public decimal? MinimumBaseline { get; set; }
        public decimal? MinimumPercentageLocalContentTarget { get; set; }
        public decimal? NationalProductPercentage { get; set; }
        public decimal? HighValueContractsAmmount { get; set; }
        public decimal? NationalProductPercentageLocalContent { get; set; }
        public decimal? LocalContentMaximumPercentage { get; set; }
        public decimal? PriceWeightAfterAdjustment { get; set; }
        public decimal? LocalContentWeightAndFinancialMarket { get; set; }
        public decimal? BaselineWeight { get; set; }
        public decimal? LocalContentTargetWeight { get; set; }
        public decimal? FinancialMarketListedWeight { get; set; }
        public int? TenderLocalContentId { get; set; }
        public List<int> LocalContentMechanismIds { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsTenderNewWithLocalContent { get; set; }
        public string TenderIdString { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public int BranchId { get; set; }
        public int ActivityVersionId { get; set; }
        public int? PreQualificationId { get; set; }
        public int? InvitationTypeId { get; set; }
        public int? TenderCreatedByTypeId { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public int? QuantityTableVersionId { get; set; }
        public bool ContainsSupply { get; set; }
        public bool IsTenderApproved { get; set; }
        public bool IsTenderTypeHasNPPrecentage { get; set; }

        public string ReferenceNumber { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TemplateYears")]
        public int? TemplateYears { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Activities")]
        public List<int> TenderActivitieIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<int> TenderMentainanceRunnigWorkIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EstablishingActions")]
        public List<int> TenderConstructionWorkIDs { set; get; } = new List<int>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "IsTenderContainsTawreed")]
        public bool IsTawreed { get; set; }

        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "Details")]
        [StringLength(1024, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "DetailsValiation")]
        public string Details { get; set; }
        public decimal? NationalProductPercentageForUnit { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        //[Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterTenderAreas")]
        public List<int> TenderAreaIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        public List<LookupModel> Areas { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]

        //[Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterTenderCountries")]
        public List<int> TenderCountriesIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]
        public List<LookupModel> Countries { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EstablishingActions")]
        public List<ConstructionWorkModel> ConstructionWorks { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<MaintenanceRunningWorkModel> MaintenanceWorks { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Activities")]
        public List<ActivityModel> Activities { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ActivityDescription")]
        [StringLength(2000)]
        public string ActivityDescription { get; set; }
        public int TenderStatusId { get; set; }
        public string AgencyCode { get; set; }
        public int TenderId { get; set; }
        public int TenderTypeId { get; set; }
        public int? TenderFirstStageId { get; set; }
        public List<int> ActivitiesWithYears { get; set; }
        public bool IsOldTender
        {
            get => TenderTypeId == (int)Enums.TenderType.CurrentTender || TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
        }
       
    }
}
