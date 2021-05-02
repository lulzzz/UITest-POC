using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.TestsBuilders.BillDefaults;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Bill
{
    public class BillCommandTest
    {
        private readonly Mock<IBillCommand> _moqBillCommand;
        private readonly IBillCommand _sut;

        public BillCommandTest()
        {
            _moqBillCommand = new Mock<IBillCommand>();
            _sut = new BillCommand(InitialData.context);
        }

        [Fact]
        public async Task ShouldUpdateBillAsyncSuccess()
        {
            _moqBillCommand.Setup(x => x.UpdateBillAsync(It.IsAny<BillInfo>()))
                       .Returns((BillInfo obj) =>
                       {
                           return Task.FromResult(obj);
                       });

            BillInfo billInfo = new BillDefaults().GetBillInfoData();
            var result = await _moqBillCommand.Object.UpdateBillAsync(billInfo);
            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }

        [Fact]
        public async Task ShouldUpdateWithoutSaveSuccess()
        {
            _moqBillCommand.Setup(x => x.UpdateWithoutSave(It.IsAny<BillInfo>()))
                       .Returns((BillInfo obj) =>
                       {
                           return Task.FromResult(obj);
                       });

            BillInfo billInfo = new BillDefaults().GetBillInfoData();
            var result = await _moqBillCommand.Object.UpdateWithoutSave(billInfo);
            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }

        [Fact]
        public async Task ShouldDeleteBillAsyncSuccess()
        {
            _moqBillCommand.Setup(x => x.DeleteBillAsync(It.IsAny<BillInfo>()))
                       .Returns((BillInfo obj) =>
                       {
                           return Task.FromResult(obj);
                       });

            BillInfo billInfo = new BillDefaults().GetBillInfoData();
            var result = await _moqBillCommand.Object.DeleteBillAsync(billInfo);
            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }

        [Fact]
        public async Task ShouldDeleteWithoutSaveSuccess()
        {
            _moqBillCommand.Setup(x => x.DeleteWithoutSave(It.IsAny<BillInfo>()))
                       .Returns((BillInfo obj) =>
                       {
                           return Task.FromResult(obj);
                       });

            BillInfo billInfo = new BillDefaults().GetBillInfoData();
            var result = await _moqBillCommand.Object.DeleteWithoutSave(billInfo);
            Assert.NotNull(result);
            Assert.IsType<BillInfo>(result);
        }
    }
}
