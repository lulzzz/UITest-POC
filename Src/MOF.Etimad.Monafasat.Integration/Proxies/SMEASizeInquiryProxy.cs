using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel.LocalContent;
using SMEASizeInquiryService;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;
namespace MOF.Etimad.Monafasat.Integration
{
    public class SMEASizeInquiryProxy : ProxyBase, ISMEASizeInquiryProxy
    {
        protected override string Port { get { return _rootConfiguration.EsbSettingsConfiguration.PortValue; } }


        #region Constructors
        public SMEASizeInquiryProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
        }
        #endregion

        private MsgRqHdr_Type SMEASizeInquiryHeader()
        {
            string sMEASizeInquiryServiceID = _rootConfiguration.SMEASizeInquiryServiceIDConfiguration.SMEASizeInquiryServiceID;
            string sMEASizeInquiryFunctionID = _rootConfiguration.SMEASizeInquiryFunctionIDConfiguration.SMEASizeInquiryFunctionID;
            return new MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = ChannelID,
                ServiceId = sMEASizeInquiryServiceID,
                ClientDt = ClientDate,
                Version = Version,
                FuncId = sMEASizeInquiryFunctionID
            };
        }

        private SMEASizeInquiryClient SMEASizeInquiryServiceClient()
        {
            var _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            var _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
            string sMEASizeInquiryAddress = _rootConfiguration.ServicesConfiguration.SMEASizeInquiryService;
            var service = new SMEASizeInquiryClient(SMEASizeInquiryClient.EndpointConfiguration.SMEASizeInquirySOAP11, sMEASizeInquiryAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;
                service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = X509RevocationMode.NoCheck
                };
            }
            return service;
        }


        public async Task<LocalContentViewModel> SMEASizeInquiry(string crNumber)
        {
            // to do check service is down
            LocalContentViewModel localContentViewModel = new LocalContentViewModel();
            var isSMEASizeInquiryWork = _rootConfiguration.isSMEASizeInquiryWorkConfiguration.isSMEASizeInquiryWork;
            if (isSMEASizeInquiryWork)
            {
                try
                {
                    var service = SMEASizeInquiryServiceClient();
                    var request = new SMEASizeInquiryRq_Type
                    {
                        MsgRqHdr = SMEASizeInquiryHeader()
                    };
                    request.Body = new SMEASizeInquiryRqBody_Type()
                    {
                        CRNumber = crNumber
                    };
                    service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0);
                    Logger.LogToFile(request, "");
                    var response = (await service.SMEASizeInquiryAsync(request))?.SMEASizeInquiryRs;
                    Logger.LogToFile(request, response);
                    if (response != null && response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                    {
                        localContentViewModel.Size = response.Body?.EnterpriseSize;
                        localContentViewModel.RevenueDesc = response.Body?.RevenueDesc;
                        localContentViewModel.DescAr = response.Body?.RevenueDescAr;
                        localContentViewModel.LaborerCountDesc = response.Body?.LaborerCountDesc;
                        localContentViewModel.LaborerCountDescAr = response.Body?.LaborerCountDescAr;
                        localContentViewModel.Status = "Success";
                        if(localContentViewModel.Size.ToLower() == "large")
                        {
                            localContentViewModel.isSMEA = false;
                        }
                        else
                        {
                            localContentViewModel.isSMEA = true;
                        }
                        return localContentViewModel;
                    }
                    else
                    {
                        return new LocalContentViewModel() { Size = "لا يوجد منشأه", Status = "Error" };
                    }
                    throw new WebServiceException(Logger.GetJsonString(request, response));
                }
                catch (Exception)
                {
                    return new LocalContentViewModel() { Size = "لا يوجد منشأه", Status = "Error" };
                }
            }
            else
            {
                return new LocalContentViewModel() { Size = "لا يوجد منشأه", Status = "Error" };
            }
        }

    }
}
