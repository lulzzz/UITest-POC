using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.TestsBuilders.BillDefaults;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Bill
{
    public class BillArchiveCommandTest
    {
        private readonly Mock<IBillArchiveCommand> _moqBillArchiveCommand;
        private readonly IBillArchiveCommand _sut;

        public BillArchiveCommandTest()
        {
            _moqBillArchiveCommand = new Mock<IBillArchiveCommand>();
            _sut = new BillArchiveCommand(InitialData.context);
        }

        [Fact]
        public async Task ShouldCreateBillArchiveSuccess()
        {
            _moqBillArchiveCommand.Setup(x => x.CreateAsync(It.IsAny<BillArchive>()))
                       .Returns((BillArchive obj) =>
                       {
                           return Task.FromResult(obj);
                       });

            BillArchive billArchive = new BillDefaults().GetBillArchiveData();
            var result = await _moqBillArchiveCommand.Object.CreateAsync(billArchive);
            Assert.NotNull(result);
            Assert.IsType<BillArchive>(result);
        }

        [Fact]
        public async Task ShouldCreateBillArchiveWithoutSaveSuccess()
        {
            _moqBillArchiveCommand.Setup(x => x.CreateWithoutSave(It.IsAny<BillArchive>()))
                       .Returns((BillArchive obj) =>
                       {
                           return Task.FromResult(obj);
                       });

            BillArchive billArchive = new BillDefaults().GetBillArchiveData();
            var result = await _moqBillArchiveCommand.Object.CreateWithoutSave(billArchive);
            Assert.NotNull(result);
            Assert.IsType<BillArchive>(result);
        }
    }
}
