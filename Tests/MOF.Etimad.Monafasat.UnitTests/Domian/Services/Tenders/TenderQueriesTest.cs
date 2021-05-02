using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Tenders
{
    public class TenderQueriesTest
    {

        private readonly Mock<IIDMAppService> _IIDMAppService;
        private readonly Mock<IHttpContextAccessor> _IhttpContextAccessor;
        private readonly TenderQueries _sut;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        private readonly Mock<ILocalContentConfigurationSettings> _moqLocalContentConfigurationSettings;

        public TenderQueriesTest()
        {
            _IhttpContextAccessor = new Mock<IHttpContextAccessor>();
            _IIDMAppService = new Mock<IIDMAppService>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _moqLocalContentConfigurationSettings = new Mock<ILocalContentConfigurationSettings>();

            _sut = new TenderQueries(InitialData.context, _IhttpContextAccessor.Object, _IIDMAppService.Object, _rootConfigurationMock.Object, _moqLocalContentConfigurationSettings.Object);
        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldGetTenderRelationStepDetailsAsync(int tenderId)
        {
            var result = await _sut.FindTenderRelationsByTenderId(tenderId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetAllTendersForIndexPage()
        {
            TenderSearchCriteria tsc = new TenderSearchCriteria()
            {
                UserId = 0,
                TenderName = "Test Tender 1",
            };
            var result = await _sut.GetAllTendersForIndexPage(tsc);
            Assert.Single(result.Items);
            Assert.Equal((int)Enums.TenderStatus.Approved, result.Items.FirstOrDefault().TenderStatusId);
            Assert.Equal(DateTime.Now.AddDays(6).ToShortDateString(), result.Items.FirstOrDefault().OffersOpeningDate.Value.ToShortDateString());
        }

        [Fact]
        public async Task ShouldGetAllTendersForIndexPageForVRO()
        {
            TenderSearchCriteria tsc = new TenderSearchCriteria()
            {
                UserId = 0,
                IsVro = true,
                AgencyCode = "1"
            };
            var result = await _sut.GetAllTendersForIndexPage(tsc);
            Assert.Single(result.Items);
            Assert.Equal((int)Enums.TenderStatus.Approved, result.Items.FirstOrDefault().TenderStatusId);
            Assert.Equal(DateTime.Now.AddDays(6).ToShortDateString(), result.Items.FirstOrDefault().OffersOpeningDate.Value.ToShortDateString());
        }

        [Fact]
        public async Task ShouldGetAllTendersForIndexPageForUnderEstablishingTenders()
        {
            TenderSearchCriteria tsc = new TenderSearchCriteria()
            {
                UserId = 0,
                TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing,
                AgencyCode = "1"
            };
            var result = await _sut.GetAllTendersForIndexPage(tsc);
            Assert.Single(result.Items);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, result.Items.FirstOrDefault().TenderStatusId);
            Assert.Equal("Test Tender 3", result.Items.FirstOrDefault().TenderName);
        }

        [Fact]
        public async Task ShouldGetAllTendersForIndexPageForNotOffersAwardedConfirmed()
        {
            TenderSearchCriteria tsc = new TenderSearchCriteria()
            {
                UserId = 0,
                AgencyCode = "1",
                NotInStatusId = (int)Enums.TenderStatus.OffersAwardedConfirmed
            };
            var result = await _sut.GetAllTendersForIndexPage(tsc);
            Assert.Equal(2, result.Items.Count());
            Assert.Equal((int)Enums.TenderStatus.Approved, result.Items.LastOrDefault().TenderStatusId);
            Assert.Equal("Test Tender 1", result.Items.FirstOrDefault().TenderName);
        }

        [Fact]
        public async Task ShouldFindTenderBySearchCreiteria()
        {
            TenderSearchCriteria tsc = new TenderSearchCriteria()
            {
                UserId = 0,
                TenderName = "Test Tender 1",
            };
            var result = await _sut.FindTenderBySearchCriteriaForIndexPage(tsc);
            Assert.Single(result.Items);
            Assert.Equal((int)Enums.TenderStatus.Approved, result.Items.FirstOrDefault().TenderStatusId);
            Assert.Equal(DateTime.Now.AddDays(6).ToShortDateString(), result.Items.FirstOrDefault().OffersOpeningDate.Value.ToShortDateString());
        }

        [Fact]
        public async Task ShouldFindTenderBySearchCreiteriaForVRO()
        {
            TenderSearchCriteria tsc = new TenderSearchCriteria()
            {
                UserId = 0,
                IsVro = true,
                AgencyCode = "1"
            };
            var result = await _sut.FindTenderBySearchCriteriaForIndexPage(tsc);
            Assert.Single(result.Items);
            Assert.Equal((int)Enums.TenderStatus.Approved, result.Items.FirstOrDefault().TenderStatusId);
            Assert.Equal(DateTime.Now.AddDays(6).ToShortDateString(), result.Items.FirstOrDefault().OffersOpeningDate.Value.ToShortDateString());
        }

        [Fact]
        public async Task ShouldFindTenderBySearchCreiteriaUnderEstablishing()
        {
            TenderSearchCriteria tsc = new TenderSearchCriteria()
            {
                UserId = 0,
                TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing,
                AgencyCode = "1"
            };
            var result = await _sut.FindTenderBySearchCriteriaForIndexPage(tsc);
            Assert.Single(result.Items);
            Assert.Equal((int)Enums.TenderStatus.UnderEstablishing, result.Items.FirstOrDefault().TenderStatusId);
            Assert.Equal("Test Tender 3", result.Items.FirstOrDefault().TenderName);
        }

        [Fact]
        public async Task ShouldFindTenderBySearchCreiteriaNotOffersAwardedConfirmedg()
        {
            TenderSearchCriteria tsc = new TenderSearchCriteria()
            {
                UserId = 0,
                AgencyCode = "1",
                NotInStatusId = (int)Enums.TenderStatus.OffersAwardedConfirmed
            };
            var result = await _sut.FindTenderBySearchCriteriaForIndexPage(tsc);
            Assert.Equal(2, result.Items.Count());
            Assert.Equal((int)Enums.TenderStatus.Approved, result.Items.LastOrDefault().TenderStatusId);
            Assert.Equal("Test Tender 1", result.Items.FirstOrDefault().TenderName);
        }

        [Fact]
        public async Task ShouldGetLastItemNumber()
        {
            var result = _sut.getLastItemNumber("", 123);
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Should_GetTendersToJoinCommittees() 
        {
            LinkTendersToCommitteeSearchCriteriaModel searchCriteria = new LinkTendersToCommitteeSearchCriteriaModel();

            var result = await _sut.GetTendersToJoinCommittees(searchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderForCheckingStatusByTenderId()
        {
            var result = await _sut.FindTenderForCheckingStatusByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindLastTenderUnitAssignsByTenderId()
        {
            Mock<IAppDbContext> _moqDbContext = new Mock<IAppDbContext>();
            _moqDbContext.Setup(m => m.TenderUnitAssigns)
                .Returns(() => { return InitialData.context.TenderUnitAssigns; });
            var result = await _sut.FindLastTenderUnitAssignsByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderQuantityTablesById()
        {
            var result = await _sut.FindTenderQuantityTablesById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderWithQuantityTablesById()
        {
            var result = await _sut.GetTenderWithQuantityTablesById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderWithChangeRequest()
        {
            var result = await _sut.GetTenderWithChangeRequest(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderWithQuantityTable()
        {
            var result = await _sut.GetTenderWithQuantityTable(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderWithFormQuantityItemsWithChanges()
        {
            var result = await _sut.GetTenderWithFormQuantityItemsWithChanges(It.IsAny<long>(), It.IsAny<bool>(), 1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderByAgencyCode()
        {
            var result = await _sut.FindTenderByAgencyCode("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderByTenderIdWithInvitations()
        {
            var result = await _sut.GetTenderByTenderIdWithInvitations(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderWithInvitationsByTenderId()
        {
            var result = await _sut.FindTenderWithInvitationsByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderInvitationsWithById()
        {
            var result = await _sut.FindTenderInvitationsWithById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderAndAgencyByTenderId()
        {
            var result = await _sut.FindTenderAndAgencyByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderWithConditionsTemplateById()
        {
            var result = await _sut.FindTenderWithConditionsTemplateById(1, 1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderOfferDetailsByTenderId()
        {
            var result = await _sut.FindTenderOfferDetailsByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderWithTypeAndInvitations()
        {
            var result = await _sut.GetTenderWithTypeAndInvitations(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTendersRelatedToFirstStage()
        {
            var result = await _sut.GetTendersRelatedToFirstStage(50 , 1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderDetailsByTenderId()
        {
            var result = await _sut.FindTenderDetailsByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderInvitationOfferDetailsByTenderId()
        {
            var result = await _sut.FindTenderInvitationOfferDetailsByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderInvitationByTenderId()
        {
            var result = await _sut.FindTenderInvitationByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderOfferDetailsByTenderIdDisplayOnly()
        {
            var result = await _sut.FindTenderOfferDetailsByTenderIdDisplayOnly(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderWithAttachments()
        {
            var result = await _sut.FindTenderWithAttachments(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderAttachmentsById()
        {
            var result = await _sut.FindTenderAttachmentsById(1, 1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderStatusByTenderId()
        {
            var result = await _sut.FindTenderStatusByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderForAwarding()
        {
            var result = await _sut.FindTenderForAwarding(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderToApproveFinancialOpening()
        {
            var result = await _sut.FindTenderToApproveFinancialOpening(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderForUpdateDates()
        {
            var result = await _sut.FindTenderForUpdateDates(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindFavouriteSupplierTenderByTenderId()
        {
            var result = await _sut.FindFavouriteSupplierTenderByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderForFramWorkReCreation()
        {
            var result = await _sut.GetTenderForFramWorkReCreation(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderByIdForInvitations()
        {
            var result = await _sut.GetTenderByIdForInvitations(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderWithAgenyRequestsAndNegotiations()
        {
            var result = await _sut.FindTenderWithAgenyRequestsAndNegotiations(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderWithUnitHistoryById()
        {
            var result = await _sut.FindTenderWithUnitHistoryById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindBiddingRoundOffersByTenderId()
        {
            var result = await _sut.FindBiddingRoundOffersByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderForAddingBiddingOfferByTenderId()
        {
            var result = await _sut.FindTenderForAddingBiddingOfferByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindBiddingRoundOffersByTenderIdForEndingBiddingRound()
        {
            var result = await _sut.FindBiddingRoundOffersByTenderIdForEndingBiddingRound(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetForNationalProduct()
        {
            var result = await _sut.GetTenderForLocalContent(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderByAgencyAndReferenceNumberForContractLinking()
        {
            var result = await _sut.FindTenderByAgencyAndReferenceNumberForContractLinking(It.IsAny<string>());
            Assert.NotNull(result);
        }
    }
}
