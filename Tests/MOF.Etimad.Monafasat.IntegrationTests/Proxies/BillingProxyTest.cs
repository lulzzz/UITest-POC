using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.IntegrationTests.Proxies
{
    public class BillingProxyTest
    {
        private readonly BillingProxy _BillingProxy;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfiguration;
        private readonly NewBillModel model;
        private readonly RootConfigurations _configuration;
        public BillingProxyTest()
        {
            _configuration = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _rootConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfiguration.Setup(m => m.Value).Returns(_configuration);
            _BillingProxy = new BillingProxy(_rootConfiguration.Object);

            model = new NewBillModel
            {
                AgencyJebayaCodeForSaddad = "041001000000002000",
                AmountDue = 100,
                DueDate = DateTime.Now,
                ExpDate = DateTime.Now.AddDays(15),
                RevList = new List<RevenueEntryInfoType>() {
                    new RevenueEntryInfoType{
                        Amt = 100,
                        BenAgencyId = "067000000000000000",
                        GFSCode = "1421901",
                    }
                },
                sadadInvoiceNumber = "10030161467",
                SupplierNameAr = "142154293000206",
                SupplierNameEn = "142154293000206",
                billRefIdForBillingForSaddad = "SubScriptions",
                ClientKey = "000000"
            };
        }

        [Fact]
        public async Task ShouldCreateNewBill()
        {
            //Arrange
            var requestData = model;
            //Act
            var result = await _BillingProxy.CreateNewBill(requestData);
            //Assert
            Assert.False(string.IsNullOrEmpty(result));

        }

        [Fact]
        public async Task ShouldCancelBill()
        {
            //Arrange
            var requestData = new BillModelForCancelRequest
            {
                sadadInvoiceNumber = "10030161467",
                ActionReason = "Test Cancel Bill",
                ClientKey = "000000"

            };
            //Act
            var result = await _BillingProxy.CancelBill(requestData);
            //Assert
            Assert.True(result);
        }


        [Fact]
        public async Task ShouldUpdateBill()
        {
            //Arrange
            var requestData = new BillModelForUpdateRequest
            {
                sadadInvoiceNumber = "10030161467",
                ExpDate = DateTime.Now.AddDays(15),
                ClientKey = "000000",
            };
            //Act
            var result = await _BillingProxy.UpdateBill(requestData);
            //Assert
            Assert.True(result);
        }
    }
}
