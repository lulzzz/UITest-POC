using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Bill
{
    public class BillQueriesServiceTest
    {
        private readonly Mock<IBillQueries> _moqBillQueries;
        private readonly IBillQueries _sut;

        public BillQueriesServiceTest()
        {
            _moqBillQueries = new Mock<IBillQueries>();
            _sut = new BillQueries(InitialData.context);
        }

        [Theory]
        [InlineData("93991132000")]
        public async Task ShouldFindBulkBillByInvoiceNumberWithTenderInfoAsyncSuccess(string invoiceNumbers)
        {

            var result = await _sut.FindBulkBillByInvoiceNumberWithTenderInfoAsync(invoiceNumbers);
            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }

        [Theory]
        [InlineData("93991132000")]
        public async Task ShouldFindBillByInvoiceNumberWithTenderSuccess(string invoiceNumbers)
        {
            var result = await _sut.FindBillByInvoiceNumberWithTender(invoiceNumbers);
            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }

        [Theory]
        [InlineData("93991132000")]
        public async Task ShouldFindBillByInvoiceNumberWithoutIncludesForRolloutTeamSuccess(string invoiceNumbers)
        {
            var result = await _sut.FindBillByInvoiceNumberWithoutIncludesForRolloutTeam(invoiceNumbers);
            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }


        [Theory]
        [InlineData("93991132000", "0410010000000002000")]
        public async Task ShouldFindBillByInvoiceNumberAndAgencyCodeSuccess(string invoiceNumbers, string agencyCode)
        {
            var result = await _sut.FindBillByInvoiceNumberAndAgencyCode(invoiceNumbers, agencyCode);
            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }
    }
}
