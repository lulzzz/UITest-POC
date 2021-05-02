using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationAgencyActionStatusModel
    {
        public Enums.enNegotiationStatus Status { get; set; }
        public string NegotiationIdString { get; set; }
        public string RejectionReason { get; set; }
        public string VerificationCode { get; set; }
    }
}
