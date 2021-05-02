using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderPlaintCommunicationRequestModel
    {
        public string EncryptedTenderId { get; set; }
        public string CreatedByRoleName { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? PlaintStatusId { get; set; }
        [Display(Name = "RequestStatus", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string PlaintStatusName { get; set; }
        [Display(Name = "RejectionReason", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string RejectionReason { get; set; }
        [StringLength(1000, MinimumLength = 1, ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "PlaintReasonValiation")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(MOF.Etimad.Monafasat.Resources.SharedResources.ErrorMessages))]
        [Display(Name = "PlaintReason", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string PlaintReason { get; set; }
        public string EncryptedOfferId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int TenderStatusId { get; set; }
        public int CommunicationRequestId { get; set; }
        public int OfferStatusId { get; set; }
        public int? AwardingStoppingPeriod { get; set; }
        public DateTime? TenderAwardingDate { get; set; }

        public object tenderHistory { get; set; }
        public int PlaintRequestId { get; set; }
        public string UserRoleName { get; set; }
        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "Attachments")]
        //public CommunicationAttachmentModel Attachments { get; set; } = new CommunicationAttachmentModel();
        public List<CommunicationAttachmentModel> AttachmentList { get; set; } = new List<CommunicationAttachmentModel>();
        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "Attachments")]
        public string AttachmentReference { get; set; } = "";
        [Display(Name = "RejectionReason", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string ParentRejectionReason { get; set; }
        public bool IsEscalation { get; set; }
        public bool CanEscalate { get; set; }
        public string EncryptedPlaintRequestId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int StatusId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string StatusName { get; set; }
        [Display(Name = "DecidedProcedure", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string ProcedureName { get; set; }
        [Display(Name = "Details", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string Details { get; set; }
        [Display(Name = "DecidedProcedure", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public int? ProcedureId { get; set; }
        public int? EscalationStatusId { get; set; }
        [Display(Name = "CommercialRegistrationNo", ResourceType = typeof(Resources.BlockResources.DisplayInputs))]
        public string SupplierCR { get; set; }
        [Display(Name = "SupplierName", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string SupplierName { get; set; }
        [Display(Name = "PlaintRequestApplyDate", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string RequestDate { get; set; }
        public string AgenctName { get; set; }
        public string RejectionDate { get; set; }
        public int TenderId { get; set; }
        public CommunicationAttachmentModel EscalationAttachments { get; set; }
        [Display(Name = "EscalationStatus", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string EscalationStatus { get; set; }
    }
}