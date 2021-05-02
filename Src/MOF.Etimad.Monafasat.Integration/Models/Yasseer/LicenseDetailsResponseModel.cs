using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class LicenseDetailsResponseModel
    {
        public LicenseDetailsResponseModel() { }
        public LicenseInfoModel LicenseInfo { get; set; }
        public List<LicenseProductsListModel> LicenseProductsList { get; set; }
        public CompanyInfoModel CompanyInfo { get; set; }
        public bool hasLicense { get; set; }
    }
}
