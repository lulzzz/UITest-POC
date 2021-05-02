using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CreateAnnouncementModel
    {
        public int AnnouncementId { get; set; }
        public string CreatedBy { get; set; }
        public int? BranchId { get; set; }
        public string AgencyCode { get; set; }
        public string AnnouncementIdString { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseEnterAnnouncementName")]
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AnnouncementName")]
        public string AnnouncementName { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AnnouncementPeriod")]
        public int AnnouncementPeriod { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseEnterTenderIntro")]
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "IntroAboutTender")]
        public string IntroAboutTender { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "InsideKSA")]
        public bool InsideKsa { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Details")]
        public string Details { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ActivityDescription")]
        public string ActivityDescription { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ReasonForTenderTypeSelection")]
        public int ReasonIdForSelectingTenderType { get; set; }

        [MinLength(1, ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectActivities")]
        [Required(ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectActivities")]

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Activities")]
        public List<int> ActivityIds { get; set; } = new List<int>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<int> MaintenanceRunnigWorkIds { get; set; } = new List<int>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EstablishingActions")]
        public List<int> ConstructionWorkIds { get; set; } = new List<int>();

        [Required(ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectAreas")]
        [MinLength(1, ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectAreas")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        public List<int> AreasIds { get; set; } = new List<int>();

        [Required(ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectCountries")]
        [MinLength(1, ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectCountries")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]
        public List<int> CountriesIds { get; set; } = new List<int>();

        public List<LookupModel> TenderTypes
        {
            get
            {
                return new List<LookupModel>() {
                    new LookupModel { Id = (int)Enums.TenderType.NewDirectPurchase, Name =  Resources.TenderResources.DisplayInputs.DirectPurshase } ,
                    new LookupModel { Id = (int)Enums.TenderType.LimitedTender, Name =  Resources.TenderResources.DisplayInputs.LimitedTender }
                    };
            }
        }

        public List<LookupModel> TenderReasons
        {
            get
            {
                return new List<LookupModel>() {
                    new LookupModel { Id = (int)Enums.ReasonForPurchaseTenderType.BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative, Name =  Resources.TenderResources.DisplayInputs.BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative } ,
                    new LookupModel { Id = (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers, Name =  "الاعمال و المشتريات التي لا تتوفر إلا لدى عدد محدود من المقاولين أو الموردين أو المتعهدين" }
                    };
            }
        }
    }
}
