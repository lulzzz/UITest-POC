using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService
{
    public class TenderJobQueriesTest
    {
        private readonly TenderJobQueries _sut;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        public TenderJobQueriesTest()
        {
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _sut = new TenderJobQueries(InitialData.context, _rootConfigurationMock.Object);
        }

        [Fact]
        public async Task Should_GetAwardedSupplierQuantityTableItemsValue()
        {
            var result = await _sut.GetAwardedSupplierQuantityTableItemsValue(It.IsAny<List<int>>());
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(new int[] { 1020 })]
        public async Task Should_FillVendorProducts(IEnumerable<int> offerIds)
        {
            var result = await _sut.FillVendorProducts(offerIds.ToList(), It.IsAny<List<EmarketPlaceResponse>>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindFinishedBiddingRounds()
        {
            var result = await _sut.FindFinishedBiddingRounds();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindPendingBiddingRounds()
        {
            var result = await _sut.FindPendingBiddingRounds();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTendersToOpenOffers()
        {
            var result = await _sut.FindTendersToOpenOffers(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindAgenciesByAgencyCodes()
        {
            var result = await _sut.FindAgenciesByAgencyCodes(It.IsAny<List<string>>());
            Assert.NotNull(result);
        }
    }
}
