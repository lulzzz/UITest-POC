using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration.Enums;
using MOF.Etimad.Monafasat.Integration.Infrastructure;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using SRMFrameworkAgreementManageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public class SRMFrameworkAgreementManageProxy : ProxyBase, ISRMFrameworkAgreementManageProxy
    {
        protected override string Port { get { return _rootConfiguration.EsbSettingsConfiguration.PortValue; } }


        #region Constructors
        public SRMFrameworkAgreementManageProxy(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {

        }
        #endregion

        private MsgRqHdr_Type SRMFrameworkAgreementManageHeader()
        {
            string srmframeworkAgreementMngServiceID = _rootConfiguration.SRMFrameworkAgreementManageServiceIDConfiguration.SRMFrameworkAgreementMngServiceID;
            string srmFrameworkAgreementMngFunctionID = _rootConfiguration.SRMFrameworkAgreementManageFunctionIDConfiguration.SRMFrameworkAgreementMngFunctionID;
            return new MsgRqHdr_Type()
            {
                RqUID = RequestID,
                SCId = ChannelID,
                ServiceId = srmframeworkAgreementMngServiceID,
                ClientDt = ClientDate,
                Version = Version,
                FuncId = srmFrameworkAgreementMngFunctionID
            };
        }


        private SRMFrameworkAgreementManageClient SRMFrameworkAgreementClient()
        {
            var _isProduction = _rootConfiguration.EsbSettingsConfiguration.IsProduction;
            var _clientCertificateValue = _rootConfiguration.EsbSettingsConfiguration.ClientCertificateFindValue;
            string _FileUploadServiceAddress = _rootConfiguration.ServicesConfiguration.SRMFrameworkAgreementManageService;
            var service = new SRMFrameworkAgreementManageClient(SRMFrameworkAgreementManageClient.EndpointConfiguration.SRMFrameworkAgreementManageSOAP11, _FileUploadServiceAddress);
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

        public async Task<bool> SRMFrameworkAgreementManage(SRMFrameworkAgreementManageModel model)
        {
            var isSRMFrameworkAgreementWork = _rootConfiguration.isSRMFrameworkAgreementWorkConfiguration.isSRMFrameworkAgreementWork;
            if (isSRMFrameworkAgreementWork)
            {
                try
                {
                    List<string> agencyList = new List<string>();
                    List<VendorInfo_Type> vendorList = new List<VendorInfo_Type>();
                    List<ProductInfo_Type> productList = new List<ProductInfo_Type>();

                    if (model.VendorsList != null && model.VendorsList.Count > 0)
                    {
                        if (model.AgencyList.Any())
                        {
                            foreach (var item in model.AgencyList)
                            {
                                agencyList.Add(item);
                            }
                        }
                        else
                        {
                            agencyList.Add(model.CreatedBy);
                        }
                        foreach (var vendorItem in model.VendorsList)
                        {
                            foreach (var productItem in vendorItem.ProductList)
                            {
                                productList.Add(new ProductInfo_Type()
                                {
                                    ProductId = productItem.ProductId,
                                    ProductName = productItem.ProductName,
                                    UnitPrice = productItem.UnitPrice,
                                    Quantity = productItem.Quantity,
                                    UnitOfMeasure = productItem.UnitOfMeasure,
                                    VATAmt = productItem.VATAmt,
                                    DiscountPercen = productItem.DiscountPercen,
                                    DiscountPercenSpecified = true,
                                    Desc = productItem.Desc,
                                    DeliveryDurationInfo = new SRMPeriodCounter_Type()
                                    {
                                        NumOfDays = productItem.DeliveryDurationInfo.NumOfDays.ToString(),
                                        NumOfMonths = productItem.DeliveryDurationInfo.NumOfMonths.ToString(),
                                        NumOfYears = productItem.DeliveryDurationInfo.NumOfYears.ToString(),
                                    }
                                });
                            }
                            vendorList.Add(new VendorInfo_Type()
                            {
                                VendorId = vendorItem.VendorId,
                                AwardedAmt = vendorItem.AwardedAmt,
                                AwardedAmtSpecified = true,
                                PurchaseCurrency = vendorItem.PurchaseCurrency,
                                ProductList = productList.ToArray()
                            });
                        }
                    }

                    var service = SRMFrameworkAgreementClient();
                    var request = new SRMFrameworkAgreementManageRq_Type
                    {
                        MsgRqHdr = SRMFrameworkAgreementManageHeader()
                    };
                    request.Body = new SRMFrameworkAgreementManageRqBody_Type()
                    {
                        ReferenceNumber = model.ReferenceNumber,
                        ContractName = model.ContractName,
                        ContractType = model.ContractType == 0 ? SRMContractType_Type.Open : SRMContractType_Type.Close,
                        AwardDt = model.AwardDt,
                        CreatedBy = model.CreatedBy,
                        ValidityInfo = new SRMPeriodCounter_Type()
                        {
                            NumOfDays = model.ValidityInfo.NumOfDays.ToString(),
                            NumOfMonths = model.ValidityInfo.NumOfMonths.ToString(),
                            NumOfYears = model.ValidityInfo.NumOfYears.ToString()
                        },
                        ValidFromSpecified = true,
                        ValidFrom = model.ValidFrom,
                        Currency = model.Currency,
                        TotalAmtSpecified = true,
                        TotalAmt = model.TotalAmt,
                        Note = model.Note,
                        VendorList = vendorList.ToArray(),
                        AgencyList = agencyList.ToArray()
                    };
                    service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0);
                    Logger.LogToFile(request, "");
                    var response = (await service.SRMFrameworkAgreementManageAsync(request))?.SRMFrameworkAgreementManageRs;
                    Logger.LogToFile(request, response);
                    if (response != null && response.MsgRsHdr.ResponseStatus.StatusCode == ServiceStatusCodes.Success)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    throw new WebServiceException(Logger.GetJsonString(request, response));
                }
                catch (Exception ex)
                {
                    LogException.Log(ex, "Emarket place exception");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
