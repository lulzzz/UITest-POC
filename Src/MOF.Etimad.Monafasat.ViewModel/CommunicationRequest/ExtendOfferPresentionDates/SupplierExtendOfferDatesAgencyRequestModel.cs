using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierExtendOfferDatesAgencyRequestModel
    {
        public int AgencyRequestId { get; set; }
        public string AgencyRequestIdString { get; set; }
        public bool canApproveExtendOfferPresentation { get; set; }
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public int SupplierExtendOfferDatesRequestId { get; set; }
        public int AgencyRequestTypeId { get; set; }
        public string RejectionReason { get; set; }
        public bool IsReported { get; set; }
        public int StatusId { get; set; }
        public int TenderTypeId { get; set; }
        public string CreatedByRoleName { get; set; }
        public string CreatedBy { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SentDate")]
        public string CreatedAtString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SentDate")]
        public DateTime? CreatedAt { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "SupplierOfferExtendDatesReasonRequired")]
        [Display(ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs), Name = "ExtendOfferDatesRequestReason")]
        public string ExtendOfferDatesRequestReason { get; set; }
        public string ExtendOfferDatesRequestedDate { get; set; }
    }
}
