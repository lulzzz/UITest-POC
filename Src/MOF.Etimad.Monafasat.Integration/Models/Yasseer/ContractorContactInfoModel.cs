using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class ContractorContactInfoModel
    {
        public ContractorContactInfoModel() { }
        public string Region { get; set; }
        public string ZipCode { get; set; }
        public string POBox { get; set; }
        public ContractorCountryModel ContractorCountry { get; set; }
        public List<ContractorNationalityModel> CountryName { get; set; }
        public PhoneInfoModel PhoneInfo { get; set; }
        public FaxInfoModel FaxInfo { get; set; }
        public EmailInfoModel EmailInfo { get; set; }

    }
}