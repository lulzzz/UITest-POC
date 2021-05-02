using CheckFundService;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using static CheckFundService.AgencyBudgetCalcFundsClient;

namespace MOF.Etimad.Monafasat.Integration
{
    public class CheckFundProxy : ProxyBase, ICheckFundProxy
    {
        #region Properties
        protected override string ServiceID { get { return "AgencyBudgetCalcFunds"; } }
        protected override string FunctionID { get { return "18020000"; } }
        private readonly string _clientCertificateValue;
        private readonly bool _isProduction;

        public CheckFundProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
        }
        #endregion

        #region Methods
        public async Task<AgencyBudgetCalcFundsRsBody_Type> GetProjectBudgetByType(Project project, BudgetType budgetType, bool isGfsCode)
        {
            var service = new AgencyBudgetCalcFundsClient(EndpointConfiguration.AgencyBudgetCalcFundsSOAP11);
            service.ChannelFactory.Endpoint.EndpointBehaviors.Add(new DebugOutputEndpointBehavior());
            AmountType_Type amountType = AmountType_Type.PJTD;
            switch (budgetType)
            {
                case BudgetType.Income:
                    amountType = AmountType_Type.YTDE;
                    break;
                case BudgetType.Cost:
                    amountType = AmountType_Type.PJTD;
                    break;
                case BudgetType.Cash:
                    amountType = AmountType_Type.YTDE;
                    break;
            }
            var request = new AgencyBudgetCalcFundsRq_Type
            {
                Body = new AgencyBudgetCalcFundsRqBody_Type()
                {
                    AgencyId = project.Agency,
                    AmountType = amountType,
                    GFSCode = isGfsCode ? project.ProjectName : "0",
                    ProjectId = project.ProjectName,
                    SourceCode = Convert.ToInt16(budgetType).ToString(),
                    Item = DateTime.Now
                },
                MsgRqHdr = FillHeader()
            };
            var response = (await service.AgencyBudgetCalcFundsAsync(request)).AgencyBudgetCalcFundsRs;
            Logger.LogToFile(request, response);
            if (response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success && response?.Body != null)
            {
                return response.Body;
            }
            throw new WebServiceException(Logger.GetJsonString(request, response));
        }
        public async Task<AgencyBudgetCalcFundsRsBody_Type> GetProjectBudgetByType(string agencyCode, string projectNumber, BudgetType budgetType, bool isGfsCode)
        {
            var service = new AgencyBudgetCalcFundsClient(EndpointConfiguration.AgencyBudgetCalcFundsSOAP11, "https://10.14.8.25:7921/AgencyBudgetCalcFunds11");
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
            AmountType_Type amountType = AmountType_Type.PJTD;
            switch (budgetType)
            {
                case BudgetType.Income:
                    amountType = AmountType_Type.YTDE;
                    break;
                case BudgetType.Cost:
                    amountType = AmountType_Type.PJTD;
                    break;
                case BudgetType.Cash:
                    amountType = AmountType_Type.YTDE;
                    break;
            }
            var request = new AgencyBudgetCalcFundsRq_Type
            {
                Body = new AgencyBudgetCalcFundsRqBody_Type()
                {
                    AgencyId = agencyCode,
                    AmountType = amountType,
                    GFSCode = isGfsCode ? projectNumber : "0",
                    ProjectId = projectNumber,
                    SourceCode = Convert.ToInt16(budgetType).ToString(),
                    Item = DateTime.Now
                },
                MsgRqHdr = FillHeader()
            };

            var response = (await service.AgencyBudgetCalcFundsAsync(request)).AgencyBudgetCalcFundsRs;
            Logger.LogToFile(request, response);
            if (response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success && response?.Body != null)
            {
                return response.Body;
            }
            throw new WebServiceException(Logger.GetJsonString(request, response));


        }
        private MsgRqHdr_Type FillHeader()
        {
            return new MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = "ETIMAD",
                ServiceId = ServiceID,
                FuncId = FunctionID,
                ClientDt = ClientDate,
                Version = Version,
            };
        }
        #endregion
    }
}



