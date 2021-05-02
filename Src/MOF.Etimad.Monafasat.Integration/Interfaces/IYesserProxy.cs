using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IYasserProxy
    {
        Task<MCICRInfoModel> ValidateMCICRAndGetInfo(MCICRInfoModelRequest model);
        Task<MCICRInfoModel> ValidateMCICRAndGetInfoWithOwners(MCICRInfoModelRequest model);
        Task<LicenseDetailsResponseModel> LicenseStatusInquiry(LicenseDetailsRequestModel model);
        Task<COCSubscriptionInquiryModel> GetCOCSubscriptionBySijilNumber(COCSubscriptionInquiryRequestModel model);
        Task<ZakatCertificateInquiryResponseModel> ZakatCertificateInquiryByCR(ZakatCertificateInquiryRequestModel model);
        Task<GOSICertificateInquiryResponseModel> GOSICertificateInquiry(GOSICertificateInquiryRequestModel model);
        Task<ContractorDetailsResponseModel> ContractorDetailsInquiry(ContractorDetailsRequestModel model);
        Task<NitaqatInformationResponseModel> NitaqatInformationInquiry(NitaqatInformationRequestModel model);
    }
}
