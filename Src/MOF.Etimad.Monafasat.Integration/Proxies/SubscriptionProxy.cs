using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using SubscriptionStatusService;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration.Proxies
{
    public class SubscriptionProxy : ProxyBase, ISubscriptionProxy
    {
        #region Properties
        private readonly string _subscriptionsAddress;
        private readonly bool _isProduction;
        private readonly string _clientCertificateValue;
        #endregion
        #region Constructors
        public SubscriptionProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _subscriptionsAddress = _rootConfiguration.ServicesConfiguration.SubscriptionService;
            _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
        }
        #endregion
        public async Task<List<SubscriptionModel>> GetCRsSubscriptionStatuses(List<string> CRs)
        {

            var service = GetSubscriptionStatusValidateClient();
            var request = new SubscriptionStatusValidateRq_Type
            {
                MsgRqHdr = SubscriptionStatusValidateFillHeader()
            };
            List<PartyId_Type> _partyList = GeneratePartyList(CRs);
            List<SubscriptionModel> subscriptionModels = new List<SubscriptionModel>();
            request.Body = new SubscriptionStatusValidateRqBody_Type()
            {
                PartyList = _partyList.ToArray()
            };
            var result = (await service.SubscriptionStatusValidateAsync(request))?.SubscriptionStatusValidateRs;
            Logger.LogToFile(request, result);
            if (result != null && result.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
            {
                if (result.Body != null)
                {
                    GeneratesubscriptionModels(subscriptionModels, result);
                    return subscriptionModels;
                }
                return null;
            }
            Logger.LogToFile(request, result);
            throw new WebServiceException(Logger.GetJsonString(request, result));


        }

        private void GeneratesubscriptionModels(List<SubscriptionModel> subscriptionModels, SubscriptionStatusValidateRs_Type result)
        {
            foreach (var item in result.Body.SubscriptionStatusList.ToList())
            {
                subscriptionModels.Add(new SubscriptionModel
                {
                    CR = item.PartyId.PartyIdNum,
                    IsRenewal = item.IsRenewal == Flg_Type.Y,
                    IsSubscribed = item.IsSubscribed == Flg_Type.Y,
                    SubscriptionURL = item.SubscriptionURL
                });
            }
        }

        private List<PartyId_Type> GeneratePartyList(List<string> CRs)
        {
            List<PartyId_Type> _partyList = new List<PartyId_Type>();
            foreach (var item in CRs)
            {
                _partyList.Add(new PartyId_Type
                {
                    PartyIdNum = item,
                    PartyIdType = PartyIdType_Type.VendorCr
                });
            }
            return _partyList;
        }
        private MsgRqHdr_Type SubscriptionStatusValidateFillHeader()
        {
            return new MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = "MNFST",
                ServiceId = "SubscriptionStatusValidate",
                FuncId = "67010000",
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        private SubscriptionStatusValidateClient GetSubscriptionStatusValidateClient()
        {
            var service = new SubscriptionStatusValidateClient(SubscriptionStatusValidateClient.EndpointConfiguration.SubscriptionStatusValidateSOAP11, _subscriptionsAddress);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            if (_isProduction)
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                service.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, _clientCertificateValue);
            }
            else
            {
                ((BasicHttpBinding)service.ChannelFactory.Endpoint.Binding).Security.Mode = BasicHttpSecurityMode.Transport;//important
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
