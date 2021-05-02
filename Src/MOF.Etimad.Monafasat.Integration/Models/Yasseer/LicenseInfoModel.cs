using System;
namespace MOF.Etimad.Monafasat.Integration
{
    public class LicenseInfoModel
    {
        public LicenseInfoModel() { }
        public string LicenseNumber { get; set; }
        public string LicenseStatus { get; set; }
        public DateTime IssueDt { get; set; }
        public DateTime ExpiryDt { get; set; }
    }
}