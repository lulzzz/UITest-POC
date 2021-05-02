namespace MOF.Etimad.Monafasat.Integration
{
    public class ZakatCertificateInquiryResponseModel
    {
        public ZakatCertificateInquiryResponseModel() { }
        public ZakatCertificateModel ZakatCertificate { get; set; }
        public PartyModel Party { get; set; }
        public string StatusName { get; set; }
    }
}
