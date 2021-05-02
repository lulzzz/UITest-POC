using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderPLaintCommunicationModel
    {
        public int? plaintStatusId;

        public string TenderId { get; set; }
        [Display(Name = "RejectionReason", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string RejectionReason { get; set; }
        public int StatusId { get; set; }
        public int? procedureId { get; set; }
        [Display(Name = "DecidedProcedure", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string procedureName { get; set; }
        public string CreationDate { get; set; }
        public bool CanSecretaryTakeAction { get; set; }
        public bool CanManagerTakeAction { get; set; }
        public string EncryptedTenderId { get; set; }
        public int TenderStatusId { get; set; }
        public bool HasPlaints { get; set; }
        public int CommunicationRequestId { get; set; }
        //public bool HasAcceptedPlaints { get; set; }
        public object tenderHistory { get; set; }
        public int? AwardingStoppingPeriod { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string StatusName { get; set; }
        [Display(Name = "Details", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string Details { get; set; }
        public string PlaintStatusName { get; set; }
        public string EncryptedCommunicationRequestId { get; set; }
        public int? EscalationStatusId { get; set; }
        public int PlaintsNoNotes { get; set; }
    }
}
