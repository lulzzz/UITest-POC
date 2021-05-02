using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PlaintRequestModel
    {
        //public CommunicationAttachmentModel Attachments { set; get; }
        public object tenderHistory { set; get; }
        [Display(Name = "PlaintReason", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string PlaintReason { get; set; }
        public string SelectedCrName { get; set; }
        public string CreationDate { get; set; }

        public string Notes { set; get; }
        public string PlainRequestId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? PlaintStatusId { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string PlaintStatusName { get; set; }
        public int AgencyCommunicationRequestId { get; set; }
        public DateTime TenderAwardingDate { get; set; }
        public string TenderId { set; get; }
        public int? AwardingStoppingPeriod { get; set; }
        public bool CanTakeAction { get; set; }
        [Display(Name = "RejectionReason", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string RejectionReason { get; set; }
        public int? EscalationStatusId { get; set; }
        public string EscalationStatusName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public string EscalationRejectionReason { get; set; }
        public string EscalationDate { get; set; }
        public string EncryptedAgencyCommunicationRequestId { get; set; }
        public List<CommunicationAttachmentModel> AttachmentList { get; set; }
        public CommunicationAttachmentModel EscalationAttachments { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.CommunicationRequest.ErrorMessages)), ErrorMessageResourceName = "EnterNotes")]
        public string EscalationNotes { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string EncryptedPlaintId { get; set; }
    }
}
