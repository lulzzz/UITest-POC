using System;

namespace MOF.Etimad.Monafasat.Integration
{
    public class ZakatCertificateModel
    {
        public ZakatCertificateModel()
        {
        }
        public ZakatCertificateModel(string certificateNumber, string certificateTIN, DateTime issueDate, string issueDateHjri, DateTime expiryDate, string expiryDateHjri, CerificateType cerificateType)
        {
            CertificateNumber = certificateNumber;
            CertificateTIN = certificateTIN;
            IssueDate = issueDate;
            IssueDateHjri = issueDateHjri;
            ExpiryDate = expiryDate;
            ExpiryDateHjri = expiryDateHjri;
            CerificateType = cerificateType;
        }
        public string CertificateNumber { get; set; }
        public string CertificateTIN { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueDateHjri { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ExpiryDateHjri { get; set; }
        public CerificateType CerificateType { get; set; }
    }
}
