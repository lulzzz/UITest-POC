using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.BillDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Bill
{
    public class BillAppServiceTest
    {
        private readonly BillDefaults billDefaults = new BillDefaults();
        private readonly AppDbContext _moqAppDbContext;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly BillAppService _billAppService;
        private readonly Mock<IBillingProxy> _billProxyMock;
        private readonly Mock<IBillCommand> _billCommandsMock;
        private readonly Mock<IBillQueries> _billQueriesMock;
        private readonly Mock<INotificationAppService> _notificationAppServiceMock;
        private readonly Mock<IIDMAppService> _idmAppServiceMock;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        private readonly Mock<ILogger<BillAppService>> _loggerMock;
        private readonly Mock<IBillArchiveCommand> _billArchiveCommandMock;

        public BillAppServiceTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase(databaseName: "TestingDB")
                 .Options;
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);

            //Mock
            _billProxyMock = new Mock<IBillingProxy>();
            _billCommandsMock = new Mock<IBillCommand>();
            _billQueriesMock = new Mock<IBillQueries>();
            _notificationAppServiceMock = new Mock<INotificationAppService>();
            _idmAppServiceMock = new Mock<IIDMAppService>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _loggerMock = new Mock<ILogger<BillAppService>>();
            _billArchiveCommandMock = new Mock<IBillArchiveCommand>();


            _moqCommandRepository = new Mock<IGenericCommandRepository>();

            _rootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForBilling());
            // Arrange
            _billAppService = new BillAppService(_rootConfigurationMock.Object,
                _billProxyMock.Object,
                _notificationAppServiceMock.Object,
                _billCommandsMock.Object,
                _billQueriesMock.Object,
                _idmAppServiceMock.Object,
                _loggerMock.Object,
                _billArchiveCommandMock.Object);

        }

        [Fact]
        public void shouldConstructBillService()
        {
            Assert.NotNull(_billAppService);
            Assert.IsType<BillAppService>(_billAppService);
        }

        [Fact]
        public async Task ShouldCreateBillInfo()
        {
            _moqCommandRepository.Setup(x => x.CreateAsync(It.IsAny<BillInfo>()))
                         .Returns((BillInfo obj) =>
                         {
                             return Task.FromResult(obj);
                         });

            _moqCommandRepository.Setup(x => x.SaveAsync())
                        .Returns(Task.CompletedTask);

            var _billObj = billDefaults.returnBillInfoDefaults();

            var result = await _moqCommandRepository.Object.CreateAsync(_billObj);

            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        public async Task ShouldCreateBillInvoiceNumberSucess(int requestType, int tenderFees)
        {
            List<TenderFinantialCostModel> TenderFees = new List<TenderFinantialCostModel>();


            BillModel billModel = new BillDefaults().GetBillModelData();
            BillInfo billInfo = new BillDefaults().GetBillInfoData();
            _idmAppServiceMock.Setup(x => x.GetSupplierInfoByCR(It.IsAny<string>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<SupplierFullDataModel>(new BillDefaults().GetSupplierFullData());
                     });

            _idmAppServiceMock.Setup(x => x.GetAgencyDetailsRelatedToSadad(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(() =>
                  {
                      return Task.FromResult<AgencyInfoModel>(new BillDefaults().GetAgencyInfoModelData());
                  });

            _billProxyMock.Setup(x => x.CreateNewBill(It.IsAny<NewBillModel>()))
               .Returns(() =>
               {
                   return Task.FromResult<string>(new BillDefaults()._billInvoicenumber);
               });
            billModel.RequestType = requestType;
            TenderFees.Add(new TenderFinantialCostModel() { FessType = tenderFees });
            var result = await _billAppService.StoreBillsAndUploadToSadad(billModel, billInfo, new BillDefaults()._cr, new BillDefaults()._agencyType, true);
            Assert.IsType<bool>(result);
        }


        [Fact]
        public async Task ShouldUpdateFreeTenderPurchaseDetailsSucess()
        {
            BillModel billModel = new BillDefaults().GetBillModelData();
            BillInfo billInfo = new BillDefaults().GetBillInfoData();
            _idmAppServiceMock.Setup(x => x.GetSupplierInfoByCR(It.IsAny<string>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<SupplierFullDataModel>(new BillDefaults().GetSupplierFullData());
                     });

            _idmAppServiceMock.Setup(x => x.GetAgencyDetailsRelatedToSadad(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(() =>
                  {
                      return Task.FromResult<AgencyInfoModel>(new BillDefaults().GetAgencyInfoModelData());
                  });

            _billProxyMock.Setup(x => x.CreateNewBill(It.IsAny<NewBillModel>()))
               .Returns(() =>
               {
                   return Task.FromResult<string>(new BillDefaults()._billInvoicenumber);
               });
            billModel.AmountDue = 0;
            var result = await _billAppService.StoreBillsAndUploadToSadad(billModel, billInfo, new BillDefaults()._cr, new BillDefaults()._agencyType, true);
            Assert.IsType<bool>(result);
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task ShouldThowBussinessExceptionWhenBillInvoicNnumberIsNull(int requestType)
        {
            BillModel billModel = new BillDefaults().GetBillModelData();
            BillInfo billInfo = new BillDefaults().GetBillInfoData();
            _idmAppServiceMock.Setup(x => x.GetSupplierInfoByCR(It.IsAny<string>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<SupplierFullDataModel>(new BillDefaults().GetSupplierFullData());
                     });

            _idmAppServiceMock.Setup(x => x.GetAgencyDetailsRelatedToSadad(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(() =>
                  {
                      return Task.FromResult<AgencyInfoModel>(new BillDefaults().GetAgencyInfoModelData());
                  });
            billModel.RequestType = requestType;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _billAppService.StoreBillsAndUploadToSadad(billModel, billInfo, new BillDefaults()._cr, new BillDefaults()._agencyType, true));
        }


        [Fact]
        public async Task ShouldSetBillPaidSucess()
        {
            PaymentNotificationServiceModel postedModel = new BillDefaults().GetPaymentNotificationServiceData();

            _billQueriesMock.Setup(x => x.FindBulkBillByInvoiceNumberWithTenderInfoAsync(It.IsAny<string>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<BillInfo>(new BillDefaults().GetBillInfoData());
                 });

            _billQueriesMock.Setup(x => x.FindBillByInvoiceNumberWithTender(It.IsAny<string>()))
             .Returns(() =>
             {
                 return Task.FromResult<BillInfo>(new BillDefaults().GetBillInfoData());
             });


            var result = await _billAppService.SetBillPaid(postedModel);
            Assert.IsType<string>(result);
            Assert.NotNull(result);
        }

       

        [Fact]
        public async Task ShouldSetBillPaidWhenBillStatusIsPaidSucess()
        {
            PaymentNotificationServiceModel postedModel = new BillDefaults().GetPaymentNotificationServiceData();

            _billQueriesMock.Setup(x => x.FindBulkBillByInvoiceNumberWithTenderInfoAsync(It.IsAny<string>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<BillInfo>(new BillDefaults().GetBillInfoDataForPaidBill());
                 });

            _billQueriesMock.Setup(x => x.FindBillByInvoiceNumberWithTender(It.IsAny<string>()))
             .Returns(() =>
             {
                 return Task.FromResult<BillInfo>(new BillDefaults().GetBillInfoDataForPaidBill());
             });

            var result = await _billAppService.SetBillPaid(postedModel);
            Assert.IsType<string>(result);
            Assert.NotNull(result);
        }



        [Fact]
        public async Task ShouldThowBussinessExceptionWhenPaymentNotificationNotVerify()
        {
            PaymentNotificationServiceModel postedModel = new PaymentNotificationServiceModel();

            _billQueriesMock.Setup(x => x.FindBulkBillByInvoiceNumberWithTenderInfoAsync(It.IsAny<string>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<BillInfo>(new BillDefaults().GetBillInfoDataForPaidBill());
                 });
            await Assert.ThrowsAsync<InvalidDataException>(async () => await _billAppService.SetBillPaid(postedModel));
        }

        [Fact]
        public async Task ShouldThowBussinessExceptionWhenBillIsNull()
        {
            PaymentNotificationServiceModel postedModel = new BillDefaults().GetPaymentNotificationServiceData();

            _billQueriesMock.Setup(x => x.FindBulkBillByInvoiceNumberWithTenderInfoAsync(It.IsAny<string>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<BillInfo>(new BillInfo());
                 });
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _billAppService.SetBillPaid(postedModel));
        }


        [Fact]
        public async Task ShouldSetBillPaidForInvitationSucess()
        {
            PaymentNotificationServiceModel postedModel = new BillDefaults().GetPaymentNotificationServiceData();
            BillInfo billInfo = new BillDefaults().GetBillInfoDataForInvitation();
            billInfo.Invitation.Test_AddTender(new TenderDefault().GetGeneralTender());

            _billQueriesMock.Setup(x => x.FindBulkBillByInvoiceNumberWithTenderInfoAsync(It.IsAny<string>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<BillInfo>(billInfo);
                 });

            _billQueriesMock.Setup(x => x.FindBillByInvoiceNumberWithTender(It.IsAny<string>()))
             .Returns(() =>
             {
                 return Task.FromResult<BillInfo>(new BillDefaults().GetBillInfoDataForInvitation());
             });


            var result = await _billAppService.SetBillPaid(postedModel);
            Assert.IsType<string>(result);
            Assert.NotNull(result);
        }

    }
}
