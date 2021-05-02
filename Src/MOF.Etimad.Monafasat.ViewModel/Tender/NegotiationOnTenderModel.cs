using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationOnTenderModel : SearchCriteria
    {
        public string tenderAddress { get; set; }
        public string tenderIdString { get; set; }
        public string referenceNumber { get; set; }
        public string tenderTypeName { get; set; }
        public decimal conditionalAmount { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string agencyName { get; set; }
        public string negotiationStartDate { get; set; }
        public string agencyBranchName { get; set; }
        public string statusName { get; set; }
        public int NegotiationRequestsCount { get; set; }
        public string requestDate { get; set; }
        public string negotiationIdString { get; set; }
    }
}

