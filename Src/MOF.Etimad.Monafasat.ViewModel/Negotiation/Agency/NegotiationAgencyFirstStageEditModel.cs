using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationAgencyFirstStageEditModel
    {
        [Display(Name = "NegotiationReason", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Required(ErrorMessageResourceName = "RequiredNegotiationReason", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]

        public int NegotiationReasonId { get; set; }
        public string NeagotiationReasonText { get; set; }

        //  [Required(ErrorMessageResourceName = "RequiredNegotiationReason", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        [Display(Name = "ProjectNumber", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string ProjectNumber { get; set; }


        [Required(ErrorMessageResourceName = "RequiredDiscountAmount", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        [Display(Name = "DiscountRatio", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Range(0.01, 99, ErrorMessageResourceType = typeof(Resources.CommunicationRequest.DisplayInputs), ErrorMessageResourceName = "RequiredDisountRatio")]
        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?$)?", ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "RequiredCorrectDecimal")]
        public decimal ReductionPercent { get; set; }

        public decimal AmountAfterDiscount { get; set; }
        [Display(Name = "ReductionLetterrefId", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Required(ErrorMessageResourceName = "RequiredReductionLetterrefId", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        public string ReductionLetterrefId { get; set; } = "";

        [Required(ErrorMessageResourceName = "DaysCount", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        [Display(Name = "Days", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Range(0, 999999999999, ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages), ErrorMessageResourceName = "DaysCount")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages), ErrorMessageResourceName = "DaysCountFormat")]
        public int Days { get; set; }

        [Required(ErrorMessageResourceName = "HoursCountFormat", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        [Display(Name = "Hours", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Range(0, 23, ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages), ErrorMessageResourceName = "HoursCount")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages), ErrorMessageResourceName = "HoursCountFormat")]
        public int Hours { get; set; }
        public NegotiationAttachmentViewModel Attachment { get; set; }
        public string NegotiationIdString { get; set; }
        public decimal ReductionAmount { get; set; }
        public string RejectionReason { get; set; }
        public string TenderIdString { get; set; }
        public int StatusId { get; set; }
        public bool IsEditAllowed { get; set; } = true;
        public bool isUserHasPrivilige { get; set; }
        public enOperationType EnOperationType { get; set; }
        public int CalculatedSupplierPeriod() => (Days * 24) + Hours;
        public enSubmitActionType ActionType { get; set; }

        public List<SelectListItem> ReductionReasons { get; set; }
        public NegotiationAgencyFirstStageViewModel negotiationFirstStageViewModel { get; set; }
    }
}
