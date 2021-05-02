namespace MOF.Etimad.Monafasat.Integration
{
    public class CompanyInfoModel
    {
        public CompanyInfoModel() { }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLaborSize { get; set; }
        public string CompanyLegalStatus { get; set; }
        public CompanyAddressInfoModel CompanyAddressInfo { get; set; }
        public CompanyContactsInfoModel CompanyContactsInfo { get; set; }
    }
}