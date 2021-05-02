using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Offers
{
    public class OfferQueriesTest
    {
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        private readonly Mock<ILocalContentConfigurationSettings> _localContentConfigurationSettings;
        private readonly OfferQueries _sut;
        public OfferQueriesTest()
        {
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _localContentConfigurationSettings = new Mock<ILocalContentConfigurationSettings>();
            _sut = new OfferQueries(InitialData.context, _moqHttpContextAccessor.Object, _rootConfigurationMock.Object, _localContentConfigurationSettings.Object);
        }

        [Fact]
        public async Task Should_FindOffers()
        {
            OfferSearchCriteria offerSearchCriteria = new OfferSearchCriteria();

            var result = await _sut.FindOffers(offerSearchCriteria);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferWithQuantitiesTablesByOfferId()
        {
            var result = await _sut.GetOfferWithQuantitiesTablesByOfferId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindOfferDetailsById()
        {
            Mock<IAppDbContext> _moqDbContext = new Mock<IAppDbContext>();
            _moqDbContext.Setup(m => m.Offers)
                .Returns(() => { return InitialData.context.Offers; });
            var result = _sut.FindOfferDetailsById(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindOfferWithCombinedById()
        {
            var result = await _sut.FindOfferWithCombinedById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferWithTenderById()
        {
            var result = await _sut.GetOfferWithTenderById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetofferWithTenderAndSupplierQT()
        {
            var result = await _sut.GetofferWithTenderAndSupplierQT(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindOfferWithQTJSONId()
        {
            var result = await _sut.FindOfferWithQTJSONId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindOfferByIdWithAttaches()
        {
            var result = await _sut.FindOfferByIdWithAttaches(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindOfferByCombinedSupplierId()
        {
            var result = await _sut.FindOfferByCombinedSupplierId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetFailedSuppliersOnPostQualification()
        {
            var result = await _sut.GetFailedSuppliersOnPostQualification(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferById()
        {
            var result = await _sut.GetOfferById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindOfferByOfferId()
        {
            var result = await _sut.FindOfferByOfferId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferWithQTById()
        {
            var result = await _sut.GetOfferWithQTById(1);

            Assert.NotNull(result);
        }


        [Fact]
        public async Task Should_GetOfferWithQuantityTablesForApplyOfferById()
        {
            var result = await _sut.GetOfferWithQuantityTablesForApplyOfferById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferWithCombinedById()
        {
            var result = await _sut.GetOfferWithCombinedById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferByOfferId()
        {
            var result = await _sut.GetOfferByOfferId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GeOfferByTenderId()
        {
            var result = await _sut.GeOfferByTenderId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GeOfferByTenderIdAndOfferIdForChecking()
        {
            var result = await _sut.GeOfferByTenderIdAndOfferIdForChecking(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferForQuantityTableForAwarding()
        {
            var result = await _sut.GetOfferForQuantityTableForAwarding(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetCombinedOfferDetailForVRO()
        {
            var result = await _sut.GetCombinedOfferDetailForVRO(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetCombinedbyId()
        {
            var result = await _sut.GetCombinedbyId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetCombinedOfferDetail()
        {
            var result = await _sut.GetCombinedOfferDetail(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetIdenticalOffersByTenderId()
        {
            var result = await _sut.GetIdenticalOffersByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetReceivedAndIdenticalOffersByTenderId()
        {
            var result = await _sut.GetReceivedAndIdenticalOffersByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetNotIdenticalOffersByTenderId()
        {
            var result = await _sut.GetNotIdenticalOffersByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetFinancialAcceeptedOffersByTenderId()
        {
            var result = await _sut.GetFinancialAcceeptedOffersByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetFinancialRejectedOffersByTenderId()
        {
            var result = await _sut.GetFinancialRejectedOffersByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetReceivedOffersByTenderId()
        {
            var result = await _sut.GetReceivedOffersByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetNegotiationFirstStageSuppliersByTenderId()
        {
            var result = await _sut.GetNegotiationFirstStageSuppliersByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOffersForSecondNegotiationByTenderId()
        {
            var result = await _sut.GetOffersForSecondNegotiationByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOtherOfferSolidarity()
        {
            var result = await _sut.GetOtherOfferSolidarity(It.IsAny<int>(), It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetNotNegotiatedOffersForFirstStageNegotiationByTenderId()
        {
            var result = await _sut.GetNotNegotiatedOffersForFirstStageNegotiationByTenderId(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync()
        {
            var result = await _sut.GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(It.IsAny<int>(), It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetCombinedWithOfferandTenderById()
        {
            var result = await _sut.GetCombinedWithOfferandTenderById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetCombinedWithOfferAndTenderById()
        {
            var result = await _sut.GetCombinedWithOfferAndTenderById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferWithQuantityItems()
        {
            var result = await _sut.GetOfferWithQuantityItems(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferLocalContentDetailsByOfferId()
        {
            var result = await _sut.GetOfferLocalContentDetailsByOfferId(1);

            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task Should_GetOfferLocalContentDetailsWithOfferByOfferId()
        {
            var result = await _sut.GetOfferLocalContentDetailsWithOfferByOfferId(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetIdenticalOffersWithLocalContentByTenderId()
        {
            var result = await _sut.GetIdenticalOffersWithLocalContentByTenderId(1);

            Assert.NotNull(result);
        }
    }
}
