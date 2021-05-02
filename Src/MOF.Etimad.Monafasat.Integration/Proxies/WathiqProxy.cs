using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel.Wathiq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using WathiqService;

namespace MOF.Etimad.Monafasat.Integration
{
    public static class WathiqFunctionIDs
    {
        public const string GetCRDetailesInformation = "14150000";
        public const string GetCRRelatedList = "14150300";
    }
    public class WathiqProxy : ProxyBase, IWathiqProxy
    {
        private readonly string _wathiqDetailsInquiryAddress;
        private readonly string _clientCertificateValue;
        private readonly bool _isProduction;
        public WathiqProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
            _wathiqDetailsInquiryAddress = _rootConfiguration.ServicesConfiguration.WathiqDetailsInquiry;
        }
        public async Task<WathiqViewModel> GetCRDataByCR(string crNumber)
        {
            var result = (await HandleWathiqDetails(WathiqFunctionIDs.GetCRDetailesInformation, crNumber));
            if (result != null)
            {
                var data = result as WathiqService.CRInfo_Type;
                return new WathiqViewModel
                {
                    CR = data.CRNumber,
                    Name = data.Name,
                };
            }
            return null;
        }
        private WathiqService.MsgRqHdr_Type FillDetailsHeader()
        {
            return new WathiqService.MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = "IDM",
                ServiceId = "CRDtlsInquiry",
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private async Task<object> HandleWathiqDetails(string wathiqFunctionID, string crNumber)
        {
            var isWathiqDetailsWork = _rootConfiguration.isWathiqDetailsConfiguration.isWathiqDetailsWork;
            if (isWathiqDetailsWork)
            {
                var service = GetWathiqDetailsClient();
                var request = new WathiqService.CRDtlsInquiryRq_Type()
                {
                    Body = new CRDtlsInquiryRqBody_Type()
                    {
                        CRNumber = crNumber
                    },
                    MsgRqHdr = FillDetailsHeader()
                };
                request.MsgRqHdr.FuncId = wathiqFunctionID;
                try
                {
                    var response = (await service.CRDtlsInquiryAsync(request)).CRDtlsInquiryRs;
                    Logger.LogToFile(request, response);
                    if (response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                    {
                        return response?.Body?.Item;
                    }
                    else if (response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.WathiqNoDataFound || response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.WrongInputData)
                    {
                        return null;
                    }
                    throw new WebServiceException(Logger.GetJsonString(request, response));
                }
                catch (FaultException ex)
                {
                    Logger.LogToFile(request, ex.ToString());
                    throw new WebServiceException("Fault exception", ex);
                }
            }
            else
            {
                return null;
            }
        }

        private CRDtlsInquiryClient GetWathiqDetailsClient()
        {
            var service = new WathiqService.CRDtlsInquiryClient(WathiqService.CRDtlsInquiryClient.EndpointConfiguration.CRDtlsInquirySOAP11, _wathiqDetailsInquiryAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }
    }
}
