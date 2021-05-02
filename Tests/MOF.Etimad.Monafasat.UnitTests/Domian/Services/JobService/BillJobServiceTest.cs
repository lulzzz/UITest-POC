using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.TestsBuilders.BillDefaults;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService
{
    public class BillJobServiceTest
    {
        private readonly BillDefaults billDefaults = new BillDefaults();

        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<IBillingProxy> _billProxyMock;
        private readonly Mock<IBillJobQueries> _billQueriesMock;
        private readonly BillJobAppService _billAppService;

        public BillJobServiceTest()
        {

            _billProxyMock = new Mock<IBillingProxy>();
            _billQueriesMock = new Mock<IBillJobQueries>();
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _billAppService = new BillJobAppService(_billProxyMock.Object, _moqCommandRepository.Object, _billQueriesMock.Object);
        }

        [Fact]
        public async Task ShouldUpdateBillThroughTahseelSuccess()
        {
            _billQueriesMock.Setup(x => x.GetAllBillInfos())
                     .Returns(() =>
                     {
                         return Task.FromResult<List<BillInfo>>(new BillDefaults().GetBillsInfoData());
                     });
            _billQueriesMock.Setup(x => x.FindBillByInvoiceNumberWithoutIncludes(It.IsAny<string>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<BillInfo>(new BillDefaults().GetBillInfoData());
                     });
            var result = await _billAppService.UpdateBillThroughTahseel();
            Assert.IsType<bool>(result);
        }
    }
}

