using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderChangeRequestModel
    {
        public int TenderChangeRequestId { get; set; }
        public int TenderId { get; set; }
        public int ChangeRequestStatusId { get; set; }
        public string ChangeRequestStatusString { set; get; }
        public int ChangeRequestTypeId { get; set; }
        public string ChangeRequestTypeString { set; get; }
        public string RequestedByRoleName { get; set; }
        public string RejectionReason { get; set; }
        public List<string> SupplierViolatorCRs { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.Messages)), ErrorMessageResourceName = "RejectionReasonRequired")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CancelationReason")]
        public int? CancelationReasonId { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.Messages)), ErrorMessageResourceName = "SelectSupplierViolator")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierViolator")]
        public int? SupplierValidatorId { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "CancelationReasonDescriptionRequired")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CancelationReasonDescription")]
        public string CancelationReasonDescription { get; set; }



    }
}
