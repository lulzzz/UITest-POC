using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{

    public class SRMFrameworkAgreementManageProxyTest
    {
        private SRMFrameworkAgreementManageProxy _SRMFrameworkAgreementManageProxy;

        public SRMFrameworkAgreementManageProxyTest()
        {


        }

        [Fact]
        public async Task ShouldReturnTrueFromSRMFrameworkAgreementManageService()
        {
            //Arrange
            SRMFrameworkAgreementManageFunctionIDConfiguration fun = new SRMFrameworkAgreementManageFunctionIDConfiguration() { SRMFrameworkAgreementMngFunctionID = "15060000" };
            SRMFrameworkAgreementManageServiceIDConfiguration serv = new SRMFrameworkAgreementManageServiceIDConfiguration() { SRMFrameworkAgreementMngServiceID = "SRMFrameworkAgreementMng" };
            EsbSettingsConfiguration esbSettingsConfiguration = new EsbSettingsConfiguration() { IsProduction = false, ClientCertificateFindValue = "EtimadIDMProd" };
            ServicesConfiguration servicesConfiguration = new ServicesConfiguration() { SRMFrameworkAgreementManageService = "https://10.14.8.25:7923/SRMFrameworkAgreementManage11" };
            IsSRMFrameworkAgreementWorkConfiguration obj = new IsSRMFrameworkAgreementWorkConfiguration() { isSRMFrameworkAgreementWork = true };
            var options = new RootConfigurations()
            {
                SRMFrameworkAgreementManageFunctionIDConfiguration = fun,
                SRMFrameworkAgreementManageServiceIDConfiguration = serv,
                EsbSettingsConfiguration = esbSettingsConfiguration,
                ServicesConfiguration = servicesConfiguration,
                isSRMFrameworkAgreementWorkConfiguration = obj

            };
            var mock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            mock.Setup(m => m.Value).Returns(options);
            _SRMFrameworkAgreementManageProxy = new SRMFrameworkAgreementManageProxy(mock.Object);

            List<VendorList> vendorLst = new List<VendorList>();
            List<ProductList> productLists = new List<ProductList>();
            List<string> agencyList = new List<string>();
            var requestData = new SRMFrameworkAgreementManageModel();

            requestData.ReferenceNumber = "4000012345";
            requestData.ContractName = " Framework for Stationary purchase";
            requestData.ContractType = SRMContractType.Open;
            requestData.AwardDt = DateTime.Now;
            requestData.CreatedBy = "014014000000";
            requestData.ValidityInfo = new ValidityInfo()
            {
                NumOfDays = 15,
                NumOfMonths = 0,
                NumOfYears = 1
            };
            requestData.ValidFrom = DateTime.Now;
            requestData.Currency = "SAR";
            requestData.TotalAmt = 1000;
            requestData.Note = "note";

            var deliveryDurationInfo = new DeliveryDurationInfo() { NumOfDays = 1, NumOfMonths = 1, NumOfYears = 1 };

            productLists.Add(new ProductList() { ProductId = "1", ProductName = "chair", UnitPrice = 1, Quantity = 1, UnitOfMeasure = "Each", VATAmt = 123, DiscountPercen = 5, DeliveryDurationInfo = deliveryDurationInfo, Desc = "desc" });

            vendorLst.Add(new VendorList()
            {
                VendorId = "2050016410",
                AwardedAmt = 60000,
                PurchaseCurrency = "SAR",
                ProductList = productLists
            });

            agencyList.Add("041001000000");
            requestData.AgencyList = agencyList;
            requestData.VendorsList = vendorLst;
            //Act
            var result = await _SRMFrameworkAgreementManageProxy.SRMFrameworkAgreementManage(requestData);
            //Assert
            Assert.True(result);
        }

        //MNFST20200224153450240

        [Fact]
        public async Task SRMFrameworkAgreementManageReturnFalse()
        {
            //Arrange
            SRMFrameworkAgreementManageFunctionIDConfiguration fun = new SRMFrameworkAgreementManageFunctionIDConfiguration() { SRMFrameworkAgreementMngFunctionID = "15060000" };
            SRMFrameworkAgreementManageServiceIDConfiguration serv = new SRMFrameworkAgreementManageServiceIDConfiguration() { SRMFrameworkAgreementMngServiceID = "SRMFrameworkAgreementMng" };
            EsbSettingsConfiguration esbSettingsConfiguration = new EsbSettingsConfiguration() { IsProduction = false, ClientCertificateFindValue = "EtimadIDMProd" };
            ServicesConfiguration servicesConfiguration = new ServicesConfiguration() { SRMFrameworkAgreementManageService = "https://10.14.8.25:7923/SRMFrameworkAgreementManage11" };
            IsSRMFrameworkAgreementWorkConfiguration obj = new IsSRMFrameworkAgreementWorkConfiguration() { isSRMFrameworkAgreementWork = true };

            var options = new RootConfigurations()
            {
                SRMFrameworkAgreementManageFunctionIDConfiguration = fun,
                SRMFrameworkAgreementManageServiceIDConfiguration = serv,
                EsbSettingsConfiguration = esbSettingsConfiguration,
                ServicesConfiguration = servicesConfiguration,
                isSRMFrameworkAgreementWorkConfiguration = obj

            };
            var mock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            mock.Setup(m => m.Value).Returns(options);
            _SRMFrameworkAgreementManageProxy = new SRMFrameworkAgreementManageProxy(mock.Object);


            List<VendorList> vendorLst = new List<VendorList>();
            List<ProductList> productLists = new List<ProductList>();
            List<string> agencyList = new List<string>();
            var requestData = new SRMFrameworkAgreementManageModel();

            requestData.ReferenceNumber = null;
            requestData.ContractName = " Framework for Stationary purchase";
            requestData.ContractType = SRMContractType.Open;
            requestData.AwardDt = DateTime.Now;
            requestData.CreatedBy = "041001000000123";
            requestData.ValidityInfo = new ValidityInfo()
            {
                NumOfDays = 15,
                NumOfMonths = 0,
                NumOfYears = 1
            };
            requestData.ValidFrom = DateTime.Now;
            requestData.Currency = "SAR";
            requestData.TotalAmt = 1000;
            requestData.Note = "note";

            var deliveryDurationInfo = new DeliveryDurationInfo() { NumOfDays = 1, NumOfMonths = 1, NumOfYears = 1 };

            productLists.Add(new ProductList() { ProductId = "1", ProductName = "chair", UnitPrice = 1, Quantity = 1, UnitOfMeasure = "Each", VATAmt = 123, DiscountPercen = 5, DeliveryDurationInfo = deliveryDurationInfo, Desc = "desc" });

            vendorLst.Add(new VendorList()
            {
                VendorId = "1006680431",
                AwardedAmt = 60000,
                PurchaseCurrency = "123",
                ProductList = productLists
            });
            agencyList.Add("123");
            requestData.AgencyList = agencyList;
            requestData.VendorsList = vendorLst;
            //Act
            var result = await _SRMFrameworkAgreementManageProxy.SRMFrameworkAgreementManage(requestData);
            //Assert
            Assert.False(result);
        }



        [Fact]
        public async Task ShouldReturnExceptionIfSRMFrameworkAgreementManageModelIsNullAsync()
        {
            //Arrange
            SRMFrameworkAgreementManageFunctionIDConfiguration fun = new SRMFrameworkAgreementManageFunctionIDConfiguration() { SRMFrameworkAgreementMngFunctionID = "15060000" };
            SRMFrameworkAgreementManageServiceIDConfiguration serv = new SRMFrameworkAgreementManageServiceIDConfiguration() { SRMFrameworkAgreementMngServiceID = "SRMFrameworkAgreementMng" };
            EsbSettingsConfiguration esbSettingsConfiguration = new EsbSettingsConfiguration() { IsProduction = false, ClientCertificateFindValue = "EtimadIDMProd" };
            ServicesConfiguration servicesConfiguration = new ServicesConfiguration() { SRMFrameworkAgreementManageService = "https://10.14.8.25:7923/SRMFrameworkAgreementManage11" };
            IsSRMFrameworkAgreementWorkConfiguration obj = new IsSRMFrameworkAgreementWorkConfiguration() { isSRMFrameworkAgreementWork = true };

            var options = new RootConfigurations()
            {
                SRMFrameworkAgreementManageFunctionIDConfiguration = fun,
                SRMFrameworkAgreementManageServiceIDConfiguration = serv,
                EsbSettingsConfiguration = esbSettingsConfiguration,
                ServicesConfiguration = servicesConfiguration,
                isSRMFrameworkAgreementWorkConfiguration = obj

            };
            var mock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            mock.Setup(m => m.Value).Returns(options);
            _SRMFrameworkAgreementManageProxy = new SRMFrameworkAgreementManageProxy(mock.Object);

            SRMFrameworkAgreementManageModel requestData = null;

            //Act
            var result = await _SRMFrameworkAgreementManageProxy.SRMFrameworkAgreementManage(requestData);
            //Assert
            Assert.False(result);
        }
    }

}
