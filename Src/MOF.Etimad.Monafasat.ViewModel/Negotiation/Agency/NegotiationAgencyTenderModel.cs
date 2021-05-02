namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationAgencyTenderModel
    {
        public string TenderIdString { get; set; }
        public string Tender { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderAgencyNumber { get; set; }
        public decimal? Amount { get; set; }
        public string AgencyName { get; set; }
        public string AmountText { get; set; }
        public int tenderStatusId { get; set; }
        public int tenderTypeId { get; set; }
        public int committeeId { get; set; }
    }
}
