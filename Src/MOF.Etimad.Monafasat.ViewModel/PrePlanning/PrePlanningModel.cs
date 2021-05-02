using Microsoft.AspNetCore.Mvc.Rendering;
using MOF.Etimad.Monafasat.Resources.PrePlanningResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PrePlanningModel
    {
        [Display(Name = "IndexNumber", ResourceType = typeof(DisplayInputs))]
        public int PrePlanningId { get; set; }
        [Display(Name = "AgencyCode", ResourceType = typeof(DisplayInputs))]
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]//, ErrorMessage =typeof( Resources.SharedResources.ErrorMessages.LessFourty))]
        public string AgencyCode { get; set; }
        [Display(Name = "AgencyName", ResourceType = typeof(DisplayInputs))]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]//, ErrorMessage =typeof( Resources.SharedResources.ErrorMessages.LessFourty))]
        public string AgencyName { get; set; }
        //[Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "EnterQualificationEnquiryDate")]
        [Display(Name = "ExecutionPlace", ResourceType = typeof(DisplayInputs))]
        public bool? InsideKSA { get; set; }
        [Display(Name = "ExecutionPlace", ResourceType = typeof(DisplayInputs))]
        public string InsideKSAString
        {
            get
            {
                if (InsideKSA.HasValue && InsideKSA.Value)
                {
                    return Resources.TenderResources.DisplayInputs.InsideKSA;
                }
                else
                    return Resources.TenderResources.DisplayInputs.OutsideKSA;
            }
        }
        [Display(Name = "Status", ResourceType = typeof(DisplayInputs))]
        public int? StatusId { get; set; }
        public bool? IsDeleteRequested { get; set; }
        [Display(Name = "Status", ResourceType = typeof(DisplayInputs))]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string StatusName { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "EnterProjectName")]
        [Display(Name = "ProjectName", ResourceType = typeof(DisplayInputs))]
        public string ProjectName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "SelectProjectType")]
        [Display(Name = "ProjectType", ResourceType = typeof(DisplayInputs))]
        public int ProjectTypeId { get; set; }
        [Display(Name = "ProjectType", ResourceType = typeof(DisplayInputs))]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string ProjectType { get; set; }
        [Display(Name = "ProjectNature", ResourceType = typeof(DisplayInputs))]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "SelectProjectNature")]
        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        public string ProjectNature { get; set; }
        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "EnterProjectDescription")]
        [Display(Name = "ProjectDescription", ResourceType = typeof(DisplayInputs))]
        public string ProjectDescription { get; set; }
        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        [Display(Name = "RejectionReason", ResourceType = typeof(DisplayInputs))]
        public string RejectionReason { get; set; }
        [StringLength(100, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less100")]
        [Display(Name = "Duration", ResourceType = typeof(DisplayInputs))]
        public string Duration { get; set; }

        [Range(0, 31, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ValueBetween")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "DurationInDaysRequired")]
        [Display(Name = "DurationInDays", ResourceType = typeof(DisplayInputs))]
        public int? DurationInDays { get; set; }

        [Range(0, 12, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ValueBetween")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "DurationInMonthsRequired")]
        [Display(Name = "DurationInMonths", ResourceType = typeof(DisplayInputs))]
        public int? DurationInMonths { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "DurationInYearsRequired")]
        [Range(0, 99, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ValueBetween")]
        [Display(Name = "DurationInYears", ResourceType = typeof(DisplayInputs))]
        public int? DurationInYears { get; set; }


        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        public string DeleteRejectionReason { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterTenderAreas")]
        public List<int> PrePlanningAreaIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        public List<LookupModel> Areas { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]
        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterTenderCountries")]
        public List<int> PrePlanningCountriesIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]
        public List<CountryModel> Countries { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string CreatedBy { get; set; }
        [Display(Name = "ProjectType", ResourceType = typeof(DisplayInputs))]
        public List<SelectListItem> ProjectTypesList { get; set; } = new List<SelectListItem>();
        [Display(Name = "ProjectType", ResourceType = typeof(DisplayInputs))]
        [Required(ErrorMessageResourceType = typeof(Resources.PrePlanningResources.ErrorMessages), ErrorMessageResourceName = "SelectProjectType")]
        public List<int> ProjectTypesIDs { set; get; } = new List<int>();
        public string EncyptedPrePlanningId { get; set; }
        [Display(Name = "YearQuarter", ResourceType = typeof(DisplayInputs))]
        [Required(ErrorMessageResourceType = typeof(Resources.PrePlanningResources.ErrorMessages), ErrorMessageResourceName = "SelectYearQuarter")]
        public int YearQuarterId { get; set; }
        [Display(Name = "YearQuarter", ResourceType = typeof(DisplayInputs))]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string YearQuarterName { get; set; }
        [Display(Name = "YearQuarter", ResourceType = typeof(DisplayInputs))]
        public List<LookupModel> YearQuarters { get; set; } = new List<LookupModel>();
        public int BranchId { get; set; }
        public string Year { get; set; }
    }
}