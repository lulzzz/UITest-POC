using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderPlaintCheckingModel
    {
        public int? ID { get; set; }

        public string EncryptedTenderId { get; set; }
        public int TenderStatusId { get; set; }
        public int? AwardingStoppingPeriod { get; set; }
        public int CommunicationRequestId { get; set; }
        public int StatusId { get; set; }
        public object tenderHistory { get; set; }
        public DateTime TenderAwardingDate { get; set; }
        public object PlaintReason { get; set; }
        public string SelectedCrName { get; set; }
        public string CreationDate { get; set; }
        public int? PlaintStatusId { get; set; }
        public string PlainRequestId { get; set; }
        public CommunicationAttachmentModel Attachments { get; set; }
        public bool IsEscalation { get; set; }
        public string SelectedCr { get; set; }
        public string PlaintStatusName { get; set; }
        public string StatusName { get; set; }
        public string EscalationDate { get; set; }
        public string AgencyRejectionDate { get; set; }
        public string CommitteeDesision { get; set; }
        public string AgencyRejectionReason { get; set; }
        public CommunicationAttachmentModel EscalationAttachments { get; set; }
        public string EscalationStatusName { get; set; }
        public string Notes { get; set; }
        public List<CommunicationAttachmentModel> AttachmentList { get; set; }
        public string CommericalRegisterNo { get; set; }
        public int? EscalationStatusId { get; set; }
        public int? EscalationAcceptanceStatusId { get; set; }
    }

}