using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PlaintDashBoardModel
    {
        public string TenderId { get; set; }
        public string TenderName { get; set; }
        public string RejectionReason { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderNumber { get; set; }
        public int StatusId { get; set; }
        public string AgencyRequestId { get; set; }
    }

    public class NegotiationDashBoardModel
    {
        public string TenderId { get; set; }
        public string NegotiationId { get; set; }
        public Enums.enNegotiationType enType { get; set; }
        public string TenderName { get; set; }
        public string RejectionReason { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderNumber { get; set; }
        public int StatusId { get; set; }
        public string AgencyRequestId { get; set; }
    }
}
