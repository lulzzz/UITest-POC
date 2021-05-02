namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ContractResponseViewModel
    {
        public ContractResponseViewModel() { }
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public bool canRegisterContract { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public string TenderReferenceNumber { get; set; }
        public string TenderName { get; set; }
        public string AgencyCode { get; set; }
    }
}
