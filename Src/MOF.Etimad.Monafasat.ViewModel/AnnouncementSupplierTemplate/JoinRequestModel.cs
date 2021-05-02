using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class JoinRequestModel
    {
        public string RequestIdString { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CommercialName")]
        public string CommericalRegisterName { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CommercialNumber")]
        public string CommericalRegisterNo { get; set; }

        public string MainActivity { get; set; }
        public string RejectionReason { get; set; }
        public string DeleteReason { get; set; }
        public string TemplateExtendMechanism { get; set; }
        public string StopUsingAnnouncemetMechanism { get; set; }
        public string Notes { get; set; }
        public string RequestStatus { get; set; }
        public int RequestStatusId { get; set; }
        public bool HasPendingRequests { get; set; }
        public string RequestAcceptanceDate { get; set; }
        public string RequestRejectionDate { get; set; }
        public string WithdrawalDate { get; set; }
        public string RequestSendingDate { get; set; }

        public bool IsOwnerAgency { get; set; }
    }
}
