using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.AgencyDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService
{
    public class TenderJobAppServiceTests
    {
        private readonly Mock<IGenericCommandRepository> _genericCommandRepository;
        private readonly Mock<ITenderJobQueries> _tenderJobQueires;
        private readonly Mock<IIDMJobQueries> _idmJobQueries;
        private readonly Mock<IQuantityTemplatesProxy> _templatesProxy;
        private readonly Mock<ISRMFrameworkAgreementManageProxy> _sRMFrameworkAgreementManageProxy;
        private readonly Mock<INotificationJobAppService> _notificationJobAppService;
        public readonly TenderAppJobService _sut;
        public TenderJobAppServiceTests()
        {
            _genericCommandRepository = new Mock<IGenericCommandRepository>();
            _tenderJobQueires = new Mock<ITenderJobQueries>();
            _idmJobQueries = new Mock<IIDMJobQueries>();
            _templatesProxy = new Mock<IQuantityTemplatesProxy>();
            _sRMFrameworkAgreementManageProxy = new Mock<ISRMFrameworkAgreementManageProxy>();
            _notificationJobAppService = new Mock<INotificationJobAppService>();
            _sut = new TenderAppJobService(_idmJobQueries.Object, _templatesProxy.Object, _tenderJobQueires.Object, _genericCommandRepository.Object,
                _sRMFrameworkAgreementManageProxy.Object, _notificationJobAppService.Object);
        }

        [Fact]
        public async Task Should_CheckBiddingTenderRounds_ReturnTrue()
        {

            _tenderJobQueires.Setup(x => x.FindFinishedBiddingRounds()).Returns(() =>
            {
                return Task.FromResult<List<BiddingRound>>(new TenderBiddingRoundDefaults().GetBiddingRoundsList());
            });

            _tenderJobQueires.Setup(x => x.FindPendingBiddingRounds()).Returns(() =>
            {
                return Task.FromResult<List<BiddingRound>>(new TenderBiddingRoundDefaults().GetBiddingRoundsList());
            });

            var result = await _sut.CheckBiddingTenderRounds();
            Assert.True(result);
        }

        [Fact]
        public async Task Should_SendNotificatoinAfterFinishTendersStoppingPeriod_Return_True()
        {

            _tenderJobQueires.Setup(x => x.GetAllFinishedStoppingAwardingPeriodTenders()).Returns(() =>
            {
                return Task.FromResult<List<Tender>>(new TenderDefault().GetGenerlTenders());
            });


            List<GovAgency> govAgencies = new AgencyDefaults().GetGovAgencies();


            _tenderJobQueires.Setup(x => x.FindAgenciesByAgencyCodes(It.IsAny<List<string>>())).Returns(() =>
            {
                return Task.FromResult<List<GovAgency>>(govAgencies);
            });

            await _sut.SendNotificatoinAfterFinishTendersStoppingPeriod();
            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_SendNotificatoinAfterFinishTendersStoppingPeriod_DirectPurchase_Return_True()
        {

            _tenderJobQueires.Setup(x => x.GetAllFinishedStoppingAwardingPeriodTenders()).Returns(() =>
            {
                return Task.FromResult<List<Tender>>(new TenderDefault().GetDirectPurchaseTenders());
            });


            List<GovAgency> govAgencies = new AgencyDefaults().GetGovAgencies();


            _tenderJobQueires.Setup(x => x.FindAgenciesByAgencyCodes(It.IsAny<List<string>>())).Returns(() =>
            {
                return Task.FromResult<List<GovAgency>>(govAgencies);
            });

            await _sut.SendNotificatoinAfterFinishTendersStoppingPeriod();
            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_GetTenderOffersForOpening_ReturnTrue()
        {

            _tenderJobQueires.Setup(x => x.FindTendersToOpenOffers(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<List<Tender>>(new TenderDefault().GetGenerlTenders());
            });


            List<GovAgency> govAgencies = new AgencyDefaults().GetGovAgencies();


            _tenderJobQueires.Setup(x => x.FindAgenciesByAgencyCodes(It.IsAny<List<string>>())).Returns(() =>
            {
                return Task.FromResult<List<GovAgency>>(govAgencies);
            });

            var result = await _sut.GetTenderOffersForOpening(0);
            Assert.True(result);
        }

        [Fact]
        public async Task Should_GetTenderOffersForChecking_ReturnTrue()
        {

            _tenderJobQueires.Setup(x => x.FindTendersToCheckOffers()).Returns(() =>
            {
                return Task.FromResult<List<Tender>>(new TenderDefault().GetDirectPurchaseTenders());
            });


            List<GovAgency> govAgencies = new AgencyDefaults().GetGovAgencies();


            _tenderJobQueires.Setup(x => x.FindAgenciesByAgencyCodes(It.IsAny<List<string>>())).Returns(() =>
            {
                return Task.FromResult<List<GovAgency>>(govAgencies);
            });

            var result = await _sut.GetTenderOffersForChecking();
            Assert.True(result);
        }


        [Fact]
        public async Task Should_GetTenderOffersForChecking_LowBudget_ReturnTrue()
        {

            _tenderJobQueires.Setup(x => x.FindTendersToCheckOffers()).Returns(() =>
            {
                return Task.FromResult<List<Tender>>(new TenderDefault().GetDirectPurchaseLowBudgetTenders());
            });

            List<GovAgency> govAgencies = new AgencyDefaults().GetGovAgencies();
            _tenderJobQueires.Setup(x => x.FindAgenciesByAgencyCodes(It.IsAny<List<string>>())).Returns(() =>
            {
                return Task.FromResult<List<GovAgency>>(govAgencies);
            });

            List<UserProfile> userProfiles = new AgencyDefaults().GetUserProfiles();
            _idmJobQueries.Setup(x => x.FindUsersProfileByIdAsync(It.IsAny<List<int?>>())).Returns(() =>
            {
                return Task.FromResult<List<UserProfile>>(userProfiles);
            });

            var result = await _sut.GetTenderOffersForChecking();
            Assert.True(result);
        }


        [Fact]
        public async Task Should_SendFinalAwardedTendersToEmarketPlace_Return_True()
        {
            Tender tender = new TenderDefault().GetGeneralTender();
            tender.SetTenderReceivedOffers(new OfferDefaults().GetOfferList());

            List<Tender> tenders = new List<Tender>() { tender };
            _tenderJobQueires.Setup(x => x.GetAllFinalAwardedTendersForEmarketPlace()).Returns(() =>
            {
                return Task.FromResult<List<Tender>>(tenders);
            });

            EmarketPlaceRequest emarketPlaceRequest = new EmarketPlaceRequest() { FormId = 1, TableId = 1 };

            List<EmarketPlaceRequest> emarketPlaceRequests = new List<EmarketPlaceRequest>() { emarketPlaceRequest };
            _tenderJobQueires.Setup(x => x.GetAwardedSupplierQuantityTableItemsValue(It.IsAny<List<int>>())).Returns(() =>
            {
                return Task.FromResult<List<EmarketPlaceRequest>>(emarketPlaceRequests);
            });

            EmarketPlaceResponse emarketPlaceResponse = new EmarketPlaceResponse() { FormId = 1, ColumnId = 1, ColumnTypeId = (int)Enums.ColumnValueEnum.Description };
            List<EmarketPlaceResponse> emarketPlaceResponses = new List<EmarketPlaceResponse>() { emarketPlaceResponse };
            ApiResponse<List<EmarketPlaceResponse>> apiResponse = new ApiResponse<List<EmarketPlaceResponse>>() { Data = emarketPlaceResponses };

            _templatesProxy.Setup(x => x.GetEmarketPlace(It.IsAny<List<EmarketPlaceRequest>>())).Returns(() =>
            {
                return Task.FromResult<ApiResponse<List<EmarketPlaceResponse>>>(apiResponse);
            });

            SRMFrameworkAgreementManageModel sRMFramework = new SRMFrameworkAgreementManageModel();
            _tenderJobQueires.Setup(x => x.FillVendorProducts(It.IsAny<List<int>>(), It.IsAny<List<EmarketPlaceResponse>>())).Returns(() =>
            {
                return Task.FromResult<SRMFrameworkAgreementManageModel>(sRMFramework);
            });

            _sRMFrameworkAgreementManageProxy.Setup(x => x.SRMFrameworkAgreementManage(It.IsAny<SRMFrameworkAgreementManageModel>())).Returns(() =>
            {
                return Task.FromResult<bool>(true);
            });

            await _sut.SendFinalAwardedTendersToEmarketPlace();
            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_SendFinalAwardedTendersToEmarketPlace_Return_False()
        {
            Tender tender = new TenderDefault().GetGeneralTender();
            tender.SetTenderReceivedOffers(new OfferDefaults().GetOfferList());

            List<Tender> tenders = new List<Tender>() { tender };
            _tenderJobQueires.Setup(x => x.GetAllFinalAwardedTendersForEmarketPlace()).Returns(() =>
            {
                return Task.FromResult<List<Tender>>(tenders);
            });

            EmarketPlaceRequest emarketPlaceRequest = new EmarketPlaceRequest() { FormId = 1, TableId = 1 };

            List<EmarketPlaceRequest> emarketPlaceRequests = new List<EmarketPlaceRequest>() { emarketPlaceRequest };
            _tenderJobQueires.Setup(x => x.GetAwardedSupplierQuantityTableItemsValue(It.IsAny<List<int>>())).Returns(() =>
            {
                return Task.FromResult<List<EmarketPlaceRequest>>(emarketPlaceRequests);
            });

            ApiResponse<List<EmarketPlaceResponse>> apiResponse = new ApiResponse<List<EmarketPlaceResponse>>();

            _templatesProxy.Setup(x => x.GetEmarketPlace(It.IsAny<List<EmarketPlaceRequest>>())).Returns(() =>
            {
                return Task.FromResult<ApiResponse<List<EmarketPlaceResponse>>>(apiResponse);
            });

            await _sut.SendFinalAwardedTendersToEmarketPlace();
            _genericCommandRepository.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
