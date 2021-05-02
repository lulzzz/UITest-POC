using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SecondStageNegotiationModelcs
    {
        public string NegotiationIdString { get; set; }
        public int NegotiationId { get; set; }
        [Required(ErrorMessageResourceName = "HoursCountFormat", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        [Display(Name = "Hours", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Range(0, 23, ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages), ErrorMessageResourceName = "HoursCount")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages), ErrorMessageResourceName = "HoursCountFormat")]

        public int Hours { get; set; }


        [Required(ErrorMessageResourceName = "DaysCount", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        [Display(Name = "Days", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Range(0, 999999999999, ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages), ErrorMessageResourceName = "DaysCount")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages), ErrorMessageResourceName = "DaysCountFormat")]

        public int Days { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string RejectionReason { get; set; }
        public string TenderIdString { get; set; }
        public string CR { get; set; }
        public int tenderStatusId { get; set; }
        public int RemainingDays { get; set; }
        public int RemainingHours { get; set; }
        public int RemaininMinutes { get; set; }
        public bool IsUserHasAccessToLowBudgetTender { get; set; }
        public SupplierTenderMainInfo SupplierTenderMainInfo { get; set; }


        public QuantitiesTemplateModel QuantitiesTemplateModel { get; set; }

    }
}
