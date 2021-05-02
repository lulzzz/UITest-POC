using LocalContentScoreInquiryService;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public class LocalContentProxy : ProxyBase, ILocalContentProxy
    {
        protected override string Port { get { return _rootConfiguration.EsbSettingsConfiguration.PortValue; } }


        #region Constructors
        public LocalContentProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
        }
        #endregion

        private MsgRqHdr_Type BaselineScoreHeader()
        {
            string localContentServiceID = _rootConfiguration.LocalContentServiceIDConfiguration.LocalContentServiceID ;
            string baselineFunctionID = _rootConfiguration.LocalContentFunctionIDConfiguration.BaselineFunctionId;
            return new MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = ChannelID,
                ServiceId = localContentServiceID,
                ClientDt = ClientDate,
                Version = Version,
                FuncId = baselineFunctionID
            };
        }

        private MsgRqHdr_Type TargetPlanScoreHeader()
        {
            string localContentServiceID = _rootConfiguration.LocalContentServiceIDConfiguration.LocalContentServiceID;
            string targetPlanFunctionID = _rootConfiguration.LocalContentFunctionIDConfiguration.TargetPlanFunctionId;
            return new MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = ChannelID,
                ServiceId = localContentServiceID,
                ClientDt = ClientDate,
                Version = Version,
                FuncId = targetPlanFunctionID
            };
        }

        private LocalContentScoreInquiryClient LocalContentServiceClient()
        {
            var _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            var _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
            string baselineScoreAddress = _rootConfiguration.ServicesConfiguration.LocalContentService;
            var service = new LocalContentScoreInquiryClient(LocalContentScoreInquiryClient.EndpointConfiguration.LocalContentScoreInquirySOAP11, baselineScoreAddress);
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

        #region Methods
        public async Task<LocalContentServiceResponse<decimal?>> GetBaselineScoreInquiry(string supplierId)
        {
            LocalContentServiceResponse<decimal?> localContentModel = new LocalContentServiceResponse<decimal?>();
            var isBaseLineServiceWork = _rootConfiguration.IsLocalContentServiceConfiguration.isBaseLineService;
            if (isBaseLineServiceWork)
            {
                try
                {
                    var service = LocalContentServiceClient();
                    var request = new LocalContentScoreInquiryRq_Type
                    {
                        MsgRqHdr = BaselineScoreHeader()
                    };
                    request.Body = new LocalContentScoreInquiryRqBody_Type()
                    {
                        VendorId = supplierId
                    };
                    service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0);
                    Logger.LogToFile(request, "");
                    var response = (await service.LocalContentScoreInquiryAsync(request))?.LocalContentScoreInquiryRs;
                    Logger.LogToFile(request, response);
                    if (response != null && response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                    {
                        localContentModel.content = response.Body.LocalContentScoreList[0].Score;
                        localContentModel.isSuccess = true;
                        return localContentModel;
                    }
                    else
                    {
                        return new LocalContentServiceResponse<decimal?>() { isSuccess = false, msg = "لا يوجد" };
                    }
                    throw new WebServiceException(Logger.GetJsonString(request, response));
                }
                catch (Exception)
                {
                    return new LocalContentServiceResponse<decimal?>() { isSuccess = false, msg = "لا يوجد" };
                }
            }
            else
            {
                return new LocalContentServiceResponse<decimal?>() { isSuccess = false, msg = "لا يوجد" };
            }
        }

        public async Task<LocalContentServiceResponse<decimal?>> GetTargetPlanScoreInquiry(string supplierId, string tenderId)
        {
            LocalContentServiceResponse<decimal?> localContentModel = new LocalContentServiceResponse<decimal?>();
            var isTargetPlanServiceeWork = _rootConfiguration.IsLocalContentServiceConfiguration.isTargetPlanService;
            if (isTargetPlanServiceeWork)
            {
                try
                {
                    var service = LocalContentServiceClient();
                    var request = new LocalContentScoreInquiryRq_Type
                    {
                        MsgRqHdr = TargetPlanScoreHeader()
                    };
                    request.Body = new LocalContentScoreInquiryRqBody_Type()
                    {
                        VendorId = supplierId,
                        TenderId = tenderId
                    };
                    service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0);
                    Logger.LogToFile(request, "");
                    var response = (await service.LocalContentScoreInquiryAsync(request))?.LocalContentScoreInquiryRs;
                    Logger.LogToFile(request, response);
                    if (response != null && response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                    {
                        localContentModel.content = response.Body.LocalContentScoreList[0].Score;
                        localContentModel.isSuccess = true;
                        return localContentModel;
                    }
                    else
                    {
                        return new LocalContentServiceResponse<decimal?>() { isSuccess = false, msg = "لا يوجد" };
                    }
                    throw new WebServiceException(Logger.GetJsonString(request, response));
                }
                catch (Exception)
                {
                    return new LocalContentServiceResponse<decimal?>() { isSuccess = false, msg = "لا يوجد" };
                }
            }
            else
            {
                return new LocalContentServiceResponse<decimal?>() { isSuccess = false, msg = "لا يوجد" };
            }
        }

       #endregion

       
    }
}