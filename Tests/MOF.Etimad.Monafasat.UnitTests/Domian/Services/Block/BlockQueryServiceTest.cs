using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Block
{
    public class BlockQueryServiceTest
    {
        private readonly Mock<IBlockQueries> _moqBlockQueries;


        private readonly BlockQueries _sut;

        public BlockQueryServiceTest()
        {
            //Mock            
            _moqBlockQueries = new Mock<IBlockQueries>();

            // Arrange
            _sut = new BlockQueries(InitialData.context);
        }

        [Theory]
        [InlineData(6)]
        public async Task ShouldFindBlockByIdAsync(int supplierBlockId)
        {
            //============Return Result===========
            var result = await _sut.FindBlockByIdAsync(supplierBlockId);
            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }

        [Theory]
        [InlineData("Commertialreqistrationnumber1")]
        public async Task ShouldCheckifSupplierBlockedByCrNo(string supplierBlockId)
        {
            //============Return Result===========
            var result = await _sut.CheckifSupplierBlockedByCrNo(supplierBlockId);
            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData(4)]
        public async Task ShouldFindBlockById(int supplierBlockId)
        {
            //============Return Result===========
            var result = await _sut.FindBlockById(supplierBlockId);
            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlockModel>(result);
        }
        [Theory]
        [InlineData(1)]
        public async Task ShouldFindSupplierBlockById(int supplierBlockId)
        {
            //============Return Result===========
            var result = _sut.FindSupplierBlockById(supplierBlockId);
            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<SupplierBlock>(result);
        }
        [Theory]
        [InlineData("4")]
        public async Task ShouldGetAgencyById(string supplierBlockId)
        {
            //============Return Result===========
            var result = await _sut.GetAgencyById(supplierBlockId);
            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<GovAgency>(result);
        }

        [Fact]
        public async Task ShouldFindManagerBlockList()
        {
            //=== arrange=========
            var _blockObj = new BlockSearchCriteria();

            //============Return Result===========
            var result = await _sut.FindManagerBlockList(_blockObj);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }

        [Fact]
        public async Task ShouldFindBlockCommittee()
        {
            //=== arrange=========
            var _blockObj = new BlockCommitteeSearchCriteria();

            //============Return Result===========
            var result = await _sut.FindBlockCommittee(_blockObj);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<BlockCommitteeModel>>(result);
        }

        [Fact]
        public async Task ShouldFind()
        {
            //=== arrange=========
            var _blockObj = new BlockSearchCriteria();

            //============Return Result===========
            var result = await _sut.Find(_blockObj);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }

        [Fact]
        public async Task ShouldFindAdminBlockList()
        {
            //=== arrange=========
            var _blockObj = new BlockSearchCriteria()
            {
                CommercialRegistrationNo = "2",
                CommercialSupplierName = "SelectedCr2",
                AgencyCode = "1"
            };

            //============Return Result===========
            var result = await _sut.FindAdminBlockList(_blockObj);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }

        [Fact]
        public async Task ShouldFindSupplierBySearchCriteria()
        {
            //=== arrange=========
            var _blockObj = new BlockSearchCriteria();

            //============Return Result===========
            var result = await _sut.Find(_blockObj);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }

        [Fact]
        public async Task ShouldGetAllCurrentBlockedSuppliers()
        {

            //============Return Result===========
            var result = await _sut.GetAllCurrentBlockedSuppliers(null, new List<string>() { "Commertialreqistrationnumber1" });

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<List<SupplierAgencyBlockModel>>(result);
        }

        [Fact]
        public async Task ShouldGetAllBlockedSuppliers()
        {

            var CRs = new List<string>() { "1" };
            string agencyCode = "1";
            //============Return Result===========
            var result = await _sut.GetAllBlockedSuppliers(agencyCode, CRs);

            //====== Assert=====
            Assert.NotNull(result);
            Assert.IsType<List<SupplierAgencyBlockModel>>(result);
        }


    }
}
