using AutoMapper;
using MOF.Etimad.Monafasat.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.MandatoryList
{
    public  class MandatoryListQueriesTest
    {
        private readonly Mock<IMapper> _mapper;
        private readonly MandatoryListQueries _sut;

        public MandatoryListQueriesTest()
        {
            _mapper = new Mock<IMapper>();
            _sut = new MandatoryListQueries(InitialData.context, _mapper.Object);
        }

        [Fact]
        public async Task ShouldFindMandatoryListSuccess()
        {
            var result = await _sut.Find(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMandatoryListDetailsForApprovalSuccess()
        {
            var result = await _sut.GetMandatoryListDetailsForApproval(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMandatoryListDetailsSuccess()
        {
            var result = await _sut.GetMandatoryListDetails(1);
            Assert.NotNull(result);
        }
        

        [Fact]
        public async Task ShouldGetAllForQuantityTableSuccess()
        {
            var result = await _sut.GetAllForQuantityTable();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetMandatoryListCSICodeInfoSuccess()
        {
            var result = await _sut.GetMandatoryListCSICodeInfo("1010");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetValidMandatoryListCodeForQauntityTableExcelSuccess()
        {
            List<string> CSICodes = new List<string>() { "1010"};
            var result = await _sut.GetValidMandatoryListCodeForQauntityTableExcel(CSICodes);
            Assert.NotNull(result);
        }

    }
}
