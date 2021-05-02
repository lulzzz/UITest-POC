using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class GOSICertificateInquiryResponseModel
    {
        public GOSICertificateInquiryResponseModel() { }
        public GOSICertificateInquiryResponseModel(List<CompanyInformationModel> companyInformationModellist)
        {
            CompanyInformationList = companyInformationModellist;
        }
        public List<CompanyInformationModel> CompanyInformationList { get; set; }
    }
}
