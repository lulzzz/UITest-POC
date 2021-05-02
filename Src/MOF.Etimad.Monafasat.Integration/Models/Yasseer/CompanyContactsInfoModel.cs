using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class CompanyContactsInfoModel
    {
        public CompanyContactsInfoModel() { }
        public string Telex { get; set; }
        public string InternetAddr { get; set; }
        public List<EmailInfoModel> EmailInfo { get; set; }
        public List<PhoneInfoModel> PhoneInfo { get; set; }
    }
}