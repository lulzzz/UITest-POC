using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Negotiation;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class CommunicationRequestApiControllerTest : BaseTestApiController
    {
        #region props

        private Claim[] _claims;
        private readonly AutoMapper.IMapper mapper;
        private readonly IOptionsSnapshot<MOF.Etimad.Monafasat.SharedKernal.RootConfigurations> _mockRootConfiguration;
        private readonly INegotiationAppService negotiationAppService;
        private readonly ICommunicationAppService communicationAppServiceAppService;
        private readonly ICommunicationQueries communicationQueries;
        private readonly ICommunicationCommands communicationCommands;
        private readonly IIDMQueries iDMQueries;
        private readonly ITenderQueries tenderQueries;
        private readonly ITenderAppService tenderService;
        private readonly ITenderCommands tenderCommands;
        private readonly IGenericCommandRepository genericCommandRepository;
        private readonly IVerificationService verificationService;
        private readonly IAgencyBudgetService agencyBudgetService;

        private CommunicationRequestController _communicationRequestController;

        #endregion

        public CommunicationRequestApiControllerTest()
        {

            var serviceProvider = services.BuildServiceProvider();
            communicationAppServiceAppService = serviceProvider.GetService<ICommunicationAppService>();
            negotiationAppService = serviceProvider.GetService<INegotiationAppService>();

            //Configure mapping just for this test
            var config = new MapperConfiguration(cfg =>
            {

                cfg.ValidateInlineMaps = false;
            });
            mapper = config.CreateMapper();

            _mockRootConfiguration = MockHelper.CreateIOptionSnapshotMock(new MOF.Etimad.Monafasat.SharedKernal.RootConfigurations());
            communicationQueries = new Mock<ICommunicationQueries>().Object;
            communicationCommands = new Mock<ICommunicationCommands>().Object;
            iDMQueries = new Mock<IIDMQueries>().Object;
            tenderQueries = new Mock<ITenderQueries>().Object;
            tenderService = new Mock<ITenderAppService>().Object;
            tenderCommands = new Mock<ITenderCommands>().Object;
            genericCommandRepository = new Mock<IGenericCommandRepository>().Object;
            verificationService = new Mock<IVerificationService>().Object;
            agencyBudgetService = new Mock<IAgencyBudgetService>().Object;
            _communicationRequestController = new CommunicationRequestController(negotiationAppService, communicationAppServiceAppService, verificationService, _mockRootConfiguration);
        }

        [Fact]
        public async Task GetSuppliersCommunicationRequestsGridTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var requestModel = new SimpleTenderSearchCriteria() { TenderId = 22176 };

            var response = await _communicationRequestController.GetSuppliersCommunicationRequestsGrid(requestModel);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
        }

        [Fact]
        public async Task GetAgencyCommunicationRequestsGridTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var requestModel = new SimpleTenderSearchCriteria() { TenderId = 222 };

            var response = await _communicationRequestController.GetAgencyCommunicationRequestsGrid(requestModel);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetExtendOffersValidityTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int agencyRequestId = 10;

            var response = await _communicationRequestController.GetExtendOffersValidity(agencyRequestId, null);

            Assert.NotNull(response);
            Assert.Equal("WbZkPyJ5NZU7TWNTs*@@**tY0Q==", response.AgencyRequestIdString);
            Assert.Equal("kjy5F jbpuplAQo2x57sLQ==", response.TenderIdString);
            Assert.Equal(2, response.ExtendOffersValiditySavingModel.ExtendOffersValidityId);

        }

        [Fact]
        public async Task GetOfferToExtendbyIdTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int offerId = 270;

            var response = await _communicationRequestController.GetOfferToExtendbyId(offerId);

            Assert.NotNull(response);
            Assert.Equal("XY8BqDyHXwE5YGLx5hr7wg==", response.OfferIdString);
            Assert.NotEmpty(response.offerAttachmentModels);
            Assert.False(string.IsNullOrEmpty(response.offerAttachmentModels[0].FileNetId));

        }

        [Fact]
        public async Task GetOfferFilesByIdTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int offerId = 270;

            var response = await _communicationRequestController.GetOfferFilesById(offerId);

            Assert.NotNull(response);
            Assert.NotEmpty(response.offerAttachments);
            Assert.NotEmpty(response.CombinedSuppliers);
            Assert.Equal("XY8BqDyHXwE5YGLx5hr7wg==", response.offerIdString);
            Assert.False(string.IsNullOrEmpty(response.offerAttachments[0].FileNetId));
        }

        [Fact]
        public async Task GetOfferDetailsTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int combinedSupplierId = 3033;

            var response = await _communicationRequestController.GetOfferDetails(combinedSupplierId);

            Assert.NotNull(response);
            Assert.Equal("sYecQeaQa3ZHhAXYfV 3CQ==", response.CombinDetailsIDString);
            Assert.Equal("NTNOJMDh4FBECCMDiTd1ig==", response.OfferIdString);
            Assert.Equal("bVj5L8OAwnIml584TIapaA==", response.TenderIDString);
            Assert.Equal(3033, response.CombinedId);
            Assert.Equal(1827, response.CombinedDetailId);
        }

        [Fact]
        public async Task GetCombinedSuppliersTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int offerId = 270;

            var response = await _communicationRequestController.GetCombinedSuppliers(offerId);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
            Assert.Equal(290, response.Items.First().CombinedId);
            Assert.Equal(270, response.Items.First().OfferId);
        }

        [Fact]
        public async Task GetTenderOfferIDsByOfferIDTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int offerId = 270;

            var response = await _communicationRequestController.GetTenderOfferIDsByOfferID(offerId);

            Assert.NotNull(response);
            Assert.Equal("XY8BqDyHXwE5YGLx5hr7wg==", response.OfferIdString);
            Assert.Equal("cT*@@**helR1dZBtzDuzSro Tg==", response.TenderIdString);
            Assert.True(response.IsOwner);
        }

        [Fact]
        public async Task AddExtendOffersValidityRequestTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var model = new ExtendOffersValiditySavingModel()
            {
                TenderIdString = Util.Encrypt(22006),
                OffersDuration = 10,
                ReplyReceivingDurationDays = 5,
                ExtendOffersReason = "reason",
                ReplyReceivingDurationTime = "3:00"
            };

            var response = await _communicationRequestController.AddExtendOffersValidityRequest(model);

            Assert.NotNull(response);
            Assert.False(string.IsNullOrEmpty(response));
        }

        [Fact]
        public async Task GetSupplierExtendOffersValidityForSupplierTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "4030231924") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int supplierExtendOffersValidtyId = 259;

            var response = await _communicationRequestController.GetSupplierExtendOffersValidityForSupplier(supplierExtendOffersValidtyId);

            Assert.NotNull(response);
            Assert.Equal("aisTH*@@**QvHYZUPl*@@**y62XHvQ==", response.TenderIdString);
            Assert.Equal("5nDvXCYNEdkAz VWoEarIg==", response.SupplierExtendOffersValidityId);
            Assert.Equal("Ue522dEaQCJ6kNd*@@**TVhXsw==", response.ExtendOffersValidityIdString);
            Assert.Equal("200239018970", response.TenderReferenceNumber);
            Assert.Equal(10, response.OffersValidityDays);
        }

        [Fact]
        public async Task AcceptExtendOffersValidityBySupplierTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "4030231924,4030231924") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int extendOffersValidtyId = 47;

            await _communicationRequestController.AcceptExtendOffersValidityBySupplier(extendOffersValidtyId);
        }

        [Fact]
        public async Task AddInitialGuaranteeAttachmentToOfferAsyncTest()
        {
            _claims = new Claim[3] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794,2050038794") ,
            new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString())};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int supplierExtendOffersValidityId = 39;
            var model = new ExtendOffersValidityAttachementViewModel()
            {
                AttachmentTypeId = 22,
                FileNetReferenceId = "idd_2CE19608-84B1-C336-8521-6875EB300000030257",
                Name = "EmployeeReport 1.pdf"
            };


            await _communicationRequestController.AddInitialGuaranteeAttachmentToOfferAsync(model, supplierExtendOffersValidityId);
        }

        [Fact]
        public async Task RejectExtendOffersValidityBySupplierTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794,2050038794") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int extendOffersValidityId = 40;

            await _communicationRequestController.RejectExtendOffersValidityBySupplier(extendOffersValidityId);
        }

        [Fact]
        public async Task CancelSupplierExtendOfferValidityTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            int extendOffersValidityId = 42;

            await _communicationRequestController.CancelSupplierExtendOfferValidity(extendOffersValidityId);
        }

        [Fact]
        public async Task GetSupplierNegotiationTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2251037375") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string tenderId = Util.Encrypt(41);
            string negotionionId = Util.Encrypt(5);

            var result = await _communicationRequestController.GetSupplierNegotiation(tenderId, negotionionId);

            Assert.NotNull(result);
            Assert.NotNull(result.supplierOfferInfo);
            Assert.Equal("1oelC87d85gM*@@**Rvo6rQ0 Q==", result.supplierOfferInfo.NegotiationIdString);
            Assert.Equal("wBOxIvOWe2vmh4GMJgq0Wg==", result.supplierOfferInfo.TenderIdString);
            Assert.NotNull(result.SupplierTenderMainInfo);
            Assert.Equal("191139044001", result.SupplierTenderMainInfo.ReferenceNumber);
            Assert.Equal("532424", result.SupplierTenderMainInfo.TenderNumber);
        }

        [Fact]
        public async Task CheckIfHasPendingRequestsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "4030123849") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await _communicationRequestController.CheckIfHasPendingRequests();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("lgnv1e*@@**M6awMF3EyIDycTQ==", result[0].TenderIdString);
            Assert.Equal("pM4eDOZ56IAIEdmR 5I98w==", result[0].RequestIdString);
            Assert.Equal("bVsCNF5IbNTntSiwwt76Hw==", result[0].NegotiationIdString);
            Assert.Equal("ifjK8HP3U0T1w9GYK6pVew==", result[0].AgencyRequestTypeIdString);
        }

        [Fact]
        public async Task AgreeOnOfferNegotiationFirstStageTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "1862748696,1862748696") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string tenderId = Util.Encrypt(19807);
            string negotionionId = Util.Encrypt(255);

            var result = await Record.ExceptionAsync(() => _communicationRequestController.AgreeOnOfferNegotiationFirstStage(tenderId, negotionionId));
            Assert.Contains("The instance of entity type 'Tender' cannot be tracked because another instance with the key value '{TenderId: 19807}' is already being tracked", result.Message);
        }

        [Fact]
        public async Task DisAgreeOfferAfterNegotiationFirstStageTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "1862748696,1862748696") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string tenderId = Util.Encrypt(800);
            string negotionionId = Util.Encrypt(40);

            var result = await _communicationRequestController.DisAgreeOfferAfterNegotiationFirstStage(tenderId, negotionionId);

            Assert.True(result);
        }

        [Fact]
        public async Task GetNegotiationDataTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string tenderId = Util.Encrypt(41);
            string negotionionId = Util.Encrypt(5);

            var result = await _communicationRequestController.GetNegotiationData(tenderId, negotionionId);

            Assert.NotNull(result);
            Assert.NotNull(result.NegotiationFirstStageModel);
            Assert.Equal("1oelC87d85gM*@@**Rvo6rQ0 Q==", result.NegotiationFirstStageModel.NegotiationIdString);
            Assert.Equal("wBOxIvOWe2vmh4GMJgq0Wg==", result.NegotiationFirstStageModel.TenderIdString);
            Assert.NotNull(result.NegotiationTenderModel);
            Assert.Equal("191139044001", result.NegotiationTenderModel.ReferenceNumber);
        }

        [Fact]
        public async Task IsAllowedToApplySecondStageNegotiationTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string tenderId = Util.Encrypt(50);

            var result = await _communicationRequestController.isAllowedToApplySecondStageNegotiation(tenderId);

            Assert.False(result);
        }

        [Fact]
        public async Task PreviewFirstStageNegotiationOffersTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string tenderId = Util.Encrypt(504);

            var result = await _communicationRequestController.PreviewFirstStageNegotiationOffers(tenderId, 5);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("MJ3LXtpiljSzpaNnHXVIZw==", result[0].offerIdString);
            Assert.Equal("aYznpWtlz9PpOj76wrqMMg==", result[0].tenderIdString);
        }

        [Fact]
        public async Task GetOffersListForFirstStageNegotiationTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string tenderId = Util.Encrypt(504);
            var model = new NegotiationOffersSearchModel()
            {
                TenderIdString = tenderId,
                DiscountValue = 5,
            };

            var result = await _communicationRequestController.GetOffersListForFirstStageNegotiation(model);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.Equal("MJ3LXtpiljSzpaNnHXVIZw==", result.Items.First().offerIdString);
            Assert.Equal("aYznpWtlz9PpOj76wrqMMg==", result.Items.First().tenderIdString);
        }

        [Fact]
        public async Task PreviewFirstStageNegotiationOffers_Second_Test()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string tenderId = Util.Encrypt(504);
            var model = new NegotiationAgencyFirstStageEditModel()
            {
                TenderIdString = tenderId,
                ReductionPercent = 5,
            };

            var result = await _communicationRequestController.PreviewFirstStageNegotiationOffers(model);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("MJ3LXtpiljSzpaNnHXVIZw==", result[0].offerIdString);
            Assert.Equal("aYznpWtlz9PpOj76wrqMMg==", result[0].tenderIdString);
        }

        [Fact]
        public async Task GetNegotiationFirstStageDatabyIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string negotionionId = Util.Encrypt(5);

            var result = await _communicationRequestController.GetNegotiationFirstStageDatabyId(negotionionId);

            Assert.NotNull(result);
            Assert.Equal("1oelC87d85gM*@@**Rvo6rQ0 Q==", result.NegotiationIdString);
            Assert.Equal("wBOxIvOWe2vmh4GMJgq0Wg==", result.TenderIdString);
            Assert.NotNull(result.negotiationFirstStageViewModel);
        }

        [Fact]
        public async Task CreateNegotiationRequestTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410"), new Claim(IdentityConfigs.Claims.CommitteeId, "13") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var model = new StartNegotiationModel()
            {
                TenderIdString = Util.Encrypt(800),
                enNegotiationTypeId = (int)Enums.enNegotiationType.FirstStage
            };

            //Not Allowed Scenario
            var result = await Record.ExceptionAsync(() => _communicationRequestController.CreateNegotiationRequest(model));
            Assert.Contains("غير مسموح  ", result.Message);
        }

        [Fact]
        public async Task GetSecondNegotiationTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string negotionionId = Util.Encrypt(6);

            var result = await _communicationRequestController.GetSecondNegotiation(negotionionId);

            Assert.NotNull(result);
            Assert.Equal("wBOxIvOWe2vmh4GMJgq0Wg==", result.TenderIdString);
            Assert.Equal("o3xboUiN2UVtnSOlUYJF2Q==", result.NegotiationIdString);
        }

        //Invalid object name 'Tender.TenderQuantitiyItemsJson'.
        [Fact]
        public async Task GetSecondNegotiation_NEWTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string negotionionId = Util.Encrypt(6);

            var result = await _communicationRequestController.GetSecondNegotiation_NEW(negotionionId);

            Assert.NotNull(result);
            Assert.Equal("wBOxIvOWe2vmh4GMJgq0Wg==", result.TenderIdString);
            Assert.Equal("o3xboUiN2UVtnSOlUYJF2Q==", result.NegotiationIdString);
        }

        [Fact]
        public async Task GetSecondStageNegotiationTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string negotionionId = Util.Encrypt(6);

            var result = await _communicationRequestController.GetSecondStageNegotiation(negotionionId);

            Assert.NotNull(result);
            Assert.Equal("wBOxIvOWe2vmh4GMJgq0Wg==", result.TenderIdString);
            Assert.Equal("o3xboUiN2UVtnSOlUYJF2Q==", result.NegotiationIdString);
        }

        [Fact]
        public async Task CreateFirstStageNegotiationTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string TenderIdString = Util.Encrypt(507);
            var model = new NegotiationAgencyFirstStageEditModel()
            {
                TenderIdString = TenderIdString,
                Days = 5,
                Hours = 16,
                NegotiationReasonId = 1,
                ReductionLetterrefId = "1",
                Attachment = new NegotiationAttachmentViewModel()
                {
                    AttachmentTypeId = 22,
                    FileNetReferenceId = "idd_2CE19608-84B1-C336-8521-6875EB300000030257",
                    Name = "EmployeeReport 1.pdf"
                },
                StatusId = 3
            };

            var result = await _communicationRequestController.CreateFirstStageNegotiation(model);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateFirstStageNegotiationTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string TenderIdString = Util.Encrypt(41);
            string negotationIdString = Util.Encrypt(5);

            var model = new NegotiationAgencyFirstStageEditModel()
            {
                NegotiationIdString = negotationIdString,
                TenderIdString = TenderIdString,
                Days = 5,
                Hours = 14,
                NegotiationReasonId = 1,
                ReductionLetterrefId = "1",
                Attachment = new NegotiationAttachmentViewModel()
                {
                    AttachmentTypeId = 22,
                    FileNetReferenceId = "idd_2CE19608-84B1-C336-8521-6875EB300000030257",
                    Name = "EmployeeReport 1.pdf"
                },
                StatusId = 3,
                ActionType = enSubmitActionType.SaveAndSend
            };

            var result = await _communicationRequestController.UpdateFirstStageNegotiation(model);
            Assert.True(result);
        }

        //Invalid object name 'CommunicationRequest.NegotiationQuantityItemJson'.
        //[Fact]
        //public async Task SaveSecondStageNegotaionTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
        //    string TenderIdString = Util.Encrypt(539);
        //    string negotationIdString = Util.Encrypt(25);

        //    var model = new NegotiationAgencySecondStageEditModel()
        //    {
        //        NegotiationId = negotationIdString,
        //        NegotiationReasonId = 1,
        //        NegotiationQuantityTableModels = new List<NegotiationQuantityTableModel>()
        //    };

        //    var result = await _communicationRequestController.SaveSecondStageNegotaion(model);
        //    Assert.NotNull(result);
        //}

        [Fact]
        public async Task ChangeNegotiationFirstStageStatusTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string negotationIdString = Util.Encrypt(43);

            var model = new NegotiationAgencyActionStatusModel()
            {
                NegotiationIdString = negotationIdString,
                RejectionReason = "reason",
                Status = enNegotiationStatus.UnitSpecialistReject,
                VerificationCode = "0"
            };

            var result = await _communicationRequestController.ChangeNegotiationFirstStageStatus(model);
            Assert.True(result);
        }

        //couldn't find navigation for: 'negotiationQuantitiestable'
        //[Fact]
        //public async Task SendToApproveNegotiationSecondStageByCheckManagerAsyncTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
        //    string TenderIdString = Util.Encrypt(539);
        //    string negotationIdString = Util.Encrypt(43);

        //    var model = new NegotiationAgencyActionStatusModel()
        //    {
        //        NegotiationIdString = negotationIdString,
        //        RejectionReason = "reason",
        //        Status = enNegotiationStatus.UnitSpecialistReject,
        //        VerificationCode = "0"
        //    };

        //     await _communicationRequestController.SendToApproveNegotiationSecondStageByCheckManagerAsync(22,5,7);
        //}

        //_httpContextAccessor.HttpContext.User.UserAgency() 
        //[Fact]
        //public async Task ApproveNegotiationSecondStageByCheckManagerAsyncTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
        //    string TenderIdString = Util.Encrypt(539);
        //    string negotationIdString = Util.Encrypt(43);

        //     await _communicationRequestController.ApproveNegotiationSecondStageByCheckManagerAsync(18,"0");
        //}

        //couldn't find navigation for: 'negotiationQuantitiestable'
        //[Fact]
        //public async Task ApproveNegotiationSecondStageByUnitSecretaryAsyncTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
        //    string TenderIdString = Util.Encrypt(539);
        //    string negotationIdString = Util.Encrypt(43);

        //    await _communicationRequestController.ApproveNegotiationSecondStageByUnitSecretaryAsync(18, "0");
        //}

        //Invalid object name 'CommunicationRequest.NegotiationQuantityItemJson'.
        //[Fact]
        //public async Task
        //DeleteNegotiationTableTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
        //    string TenderIdString = Util.Encrypt(539);
        //    string negotationIdString = Util.Encrypt(43);

        //    await _communicationRequestController.DeleteNegotiationTable("1");
        //}

        //Invalid object name 'CommunicationRequest.NegotiationQuantityItemJson'.
        //[Fact]
        //public async Task GetNegotiationTableQuantityTablesTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

        //    var result = await _communicationRequestController.GetNegotiationTableQuantityTables(20);
        //    Assert.NotNull(result);
        //    Assert.NotEmpty(result.tableFormHtmls);
        //    Assert.NotNull(result.offerStatusModel);
        //    Assert.Equal("FNQ6MSiMe 3MXGW2izPfbA==", result.offerStatusModel.offerIdString);
        //}

        [Fact]
        public async Task RejectNegotiationSecondStageByUnitSecretaryAsyncTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var model = new RejectNegotiation()
            {
                NegotiationId = 293,
                RejectionReason = "reason"
            };

            await _communicationRequestController.RejectNegotiationSecondStageByUnitSecretaryAsync(model);

        }

        //_httpContextAccessor.HttpContext.User.UserAgency()
        //[Fact]
        //public async Task EditNegotiationInfoAsyncTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);


        //    await _communicationRequestController.EditNegotiationInfoAsync(20);

        //}

        //Invalid object name 'CommunicationRequest.NegotiationQuantityItemJson'.
        [Fact]
        public async Task ApproveNegotiationSecondStageBySupplierAsyncTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            await _communicationRequestController.ApproveNegotiationSecondStageBySupplierAsync(296);

        }

        //Invalid object name 'CommunicationRequest.NegotiationQuantityItemJson'.
        [Fact]
        public async Task RejectNegotiationSecondStageBySupplierAsyncTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            await _communicationRequestController.RejectNegotiationSecondStageBySupplierAsync(298);

        }

        [Fact]
        public async Task ReopenSecondNegotiationTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string negotionId = Util.Encrypt(290);

            await _communicationRequestController.ReopenSecondNegotiation(negotionId);

        }

        //Invalid object name 'CommunicationRequest.NegotiationQuantityItemJson'.
        //[Fact]
        //public async Task ResetSecondNegotiationTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050016410,2050016410") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
        //    string negotionId = Util.Encrypt(29);

        //    await _communicationRequestController.ResetSecondNegotiation(negotionId);

        //}


        #region Plaint

        [Fact]
        public async Task AddPlaintRequestTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "1862748696,1862748696") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var model = new TenderPlaintCommunicationRequestModel()
            {
                EncryptedTenderId = Util.Encrypt(1342),
                EncryptedOfferId = Util.Encrypt(546),
                PlaintReason = "plaint reason test",
                CanEscalate = true
                //enNegotiationTypeId = (int)Enums.enNegotiationType.FirstStage
            };

            var result = await _communicationRequestController.AddPlaintRequest(model);

            Assert.NotNull(result);
            Assert.Equal("21fR0RZXdJhRwhI9EK3c4g==", result.EncryptedOfferId);
            Assert.Equal("seysc5s smD49xlNpWQqGA==", result.EncryptedTenderId);

        }

        [Fact]
        public async Task FindTenderForPlaintRequestByIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2251037375,2251037375") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string EncryptedTenderId = Util.Encrypt(1342);


            var result = await _communicationRequestController.FindTenderForPlaintRequestById(EncryptedTenderId);

            Assert.NotNull(result);
            Assert.Equal("seysc5s smD49xlNpWQqGA==", result.TenderIdString);
            Assert.Equal("191239000679", result.ReferenceNumber);
            Assert.Equal("1", result.TenderNumber);
        }

        [Fact]
        public async Task FindTenderForEscalationRequestByIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2251037375,2251037375") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string EncryptedTenderId = Util.Encrypt(1342);


            var result = await _communicationRequestController.FindTenderForEscalationRequestById(EncryptedTenderId);

            Assert.NotNull(result);
            Assert.Equal("seysc5s smD49xlNpWQqGA==", result.TenderIdString);
            Assert.Equal("191239000679", result.ReferenceNumber);
            Assert.Equal("191239000679", result.TenderNumber);
        }

        [Fact]
        public async Task FindSupplierPlaintRequestByTenderIdAndCRTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string EncryptedTenderId = Util.Encrypt(583);


            var result = await Record.ExceptionAsync(() => _communicationRequestController.FindSupplierPlaintRequestByTenderIdAndCR(EncryptedTenderId));
            Assert.Contains("You already have a plaint request on this competition", result.Message);
        }

        [Fact]
        public async Task FindPlaintRequestByIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string EncryptedPlaintId = Util.Encrypt(42);


            var result = await _communicationRequestController.FindPlaintRequestById(EncryptedPlaintId);

            Assert.NotNull(result);
            Assert.Equal("5rqZTFSiu9HS*@@**cEvRKF7aw==", result.EncryptedAgencyCommunicationRequestId);
            Assert.Equal("M Q16tmUTsR6D HtNA2oyw==", result.PlainRequestId);
            Assert.Equal("AOu515zaJF JppqjFkvIow==", result.TenderId);
        }

        [Fact]
        public async Task FindEscalatedPlaintRequestByIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            string EncryptedPlaintId = Util.Encrypt(78);


            var result = await _communicationRequestController.FindEscalatedPlaintRequestById(EncryptedPlaintId);

            Assert.NotNull(result);
            Assert.Equal("tNIPRjbhmSeJBQqMZ8EV8w==", result.EncryptedAgencyCommunicationRequestId);
            Assert.Equal("u*@@**GHc4i8mWskj4vbFUa7Uw==", result.PlainRequestId);
            Assert.Equal("3eCzsufTBWU OOlDmgNbkg==", result.TenderId);
        }

        [Fact]
        public async Task GetEscalatedTendersTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var model = new EscalationSearchCriteria();


            var result = await _communicationRequestController.GetEscalatedTenders(model);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task FindTenderPlaintRequestsByTenderIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var model = new PlaintSearchCriteria() { TenderId = 667, AgencyRequestId = Util.Encrypt(226) };


            var result = await _communicationRequestController.FindTenderPlaintRequestsByTenderId(model);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task FindTenderEscalatedPlaintRequestsByTenderIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var model = new PlaintSearchCriteria() { TenderId = 20606, AgencyRequestId = Util.Encrypt(639) };


            var result = await _communicationRequestController.FindTenderEscalatedPlaintRequestsByTenderId(model);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.Equal("3eCzsufTBWU OOlDmgNbkg==", result.Items.First().EncryptedTenderId);
            Assert.Equal("u*@@**GHc4i8mWskj4vbFUa7Uw==", result.Items.First().PlainRequestId);
        }

        [Fact]
        public async Task FindTenderPlaintCommunicationByTenderIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await _communicationRequestController.FindTenderPlaintCommunicationByTenderId(209);

            Assert.NotNull(result);
            Assert.Equal("IoDEh2LPcxSWX6uKRZU*@@**sg==", result.EncryptedCommunicationRequestId);
            Assert.True(result.HasPlaints);
            Assert.Equal("9b3uZdZLx*@@**V2VH5moylAMg==", result.EncryptedTenderId);
            Assert.Equal(209, result.CommunicationRequestId);

        }

        [Fact]
        public async Task FindTenderPlaintEscalationsByTenderIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await _communicationRequestController.FindTenderPlaintEscalationsByTenderId(209);

            Assert.NotNull(result);
            Assert.Equal("IoDEh2LPcxSWX6uKRZU*@@**sg==", result.EncryptedCommunicationRequestId);
            Assert.False(result.HasPlaints);
            Assert.Equal("9b3uZdZLx*@@**V2VH5moylAMg==", result.EncryptedTenderId);
            Assert.Equal(209, result.CommunicationRequestId);

        }

        //[Fact]
        //public async Task CreateSupplierExtendOfferDatesRequestTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

        //await _communicationRequestController.CreateSupplierExtendOfferDatesRequest(255,"extend reason",DateTime.Now.AddDays(1));

        //}

        [Fact]
        public async Task ApproveSupplierExtendOfferDatesRequestTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await _communicationRequestController.ApproveSupplierExtendOfferDatesRequest(212);
            Assert.NotNull(result);
            Assert.Equal("OqA70gAogZ6LTyA*@@**HwBkVg==", result.TenderIdString);
        }

        [Fact]
        public async Task RejectSupplierExtendOfferDatesRequestTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            await _communicationRequestController.RejectSupplierExtendOfferDatesRequest(213);

        }

        [Fact]
        public async Task FindSupplierExtendOfferDatesAgencyRequestByIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await _communicationRequestController.FindSupplierExtendOfferDatesAgencyRequestById(220);
            Assert.NotNull(result);
            Assert.Equal("OqA70gAogZ6LTyA*@@**HwBkVg==", result.TenderIdString);
            Assert.Equal(220, result.AgencyRequestId);
        }

        [Fact]
        public async Task FindSupplierExtendOfferDatesAgencyRequestsByIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await _communicationRequestController.FindSupplierExtendOfferDatesAgencyRequestsById(226);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        //Invalid object name 'Tender.TenderQuantitiyItemsJson'.
        //[Fact]
        //public async Task GetTenderExtendDatesByTenderIdTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "5900037276,5900037276") };
        //    _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

        //    var result = await _communicationRequestController.GetTenderExtendDatesByTenderId(457,201);
        //    Assert.NotNull(result);
        //}

        [Fact]
        public async Task UpdateTenderExtendDatesTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "019001000000,019001000000"),
            new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager)};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);
            var model = new ExtendOfferPresentationDatesModel()
            {
                TenderId = 91,
                TenderTypeId = (int)Enums.TenderType.PreQualification,
                LastEnqueriesDate = Convert.ToDateTime("2021-04-14 00:00:00.0000000"),
                LastOfferPresentationDate = Convert.ToDateTime("2021-06-20 00:00:00.0000000"),
                OffersOpeningDate = Convert.ToDateTime("2021-10-16 00:00:00.0000000")

            };


            await _communicationRequestController.UpdateTenderExtendDates(model);

        }

        [Fact]
        public async Task RejectPlaintCommunicationRequestTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "030001000000,030001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await Record.ExceptionAsync(() => _communicationRequestController.RejectPlaintCommunicationRequest(Util.Encrypt(230), "reject reason"));
            //NOT IsValidToManagerToChecklEscalation
            Assert.Contains("Unexpected Error", result.Message);
        }

        [Fact]
        public async Task ApprovePlaintCommunicationRequestTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await Record.ExceptionAsync(() => _communicationRequestController.ApprovePlaintCommunicationRequest(Util.Encrypt(8), "54687"));
            //NOT IsValidToManagerToChecklEscalation
            Assert.Contains("Unexpected Error", result.Message);
        }

        [Fact]
        public async Task RejectPlaintTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await Record.ExceptionAsync(() => _communicationRequestController.RejectPlaint(Util.Encrypt(25), "reject reason"));
            //Not IsValidToSendPlaintDecission
            Assert.Contains("Unexpected Error", result.Message);
        }

        [Fact]
        public async Task AcceptPlaintTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            await _communicationRequestController.AcceptPlaint(Util.Encrypt(288), 1, "");
        }

        [Fact]
        public async Task AcceptEscalationCommunicationRequestTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            await _communicationRequestController.AcceptEscalationCommunicationRequest(240, 1, "");
        }

        [Fact]
        public async Task ApproveEscalationCommunicationRequestTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await Record.ExceptionAsync(() => _communicationRequestController.ApproveEscalationCommunicationRequest(241, "54687"));
            //NOT IsValidToManagerToChecklEscalation
            Assert.Contains("Unexpected Error", result.Message);
        }

        [Fact]
        public async Task RejectEscalationCommunicationRequestApprovalTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await Record.ExceptionAsync(() => _communicationRequestController.RejectEscalationCommunicationRequestApproval(261, "reject reason"));
            //NOT IsValidToManagerToChecklEscalation
            Assert.Contains("Unexpected Error", result.Message);
        }

        [Fact]
        public async Task RejectEscalationCommunicationRequestTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            await _communicationRequestController.RejectEscalationCommunicationRequest(20, "reject reason");
        }

        [Fact]
        public async Task FindSupplierPlaintDetailsPlaintIdTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
             new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var result = await _communicationRequestController.FindSupplierPlaintDetailsPlaintId(226);

            Assert.NotNull(result);
            Assert.Equal("WBVLqOVQqx kowjJ2f0s3Q==", result.EncryptedTenderId);
            Assert.Equal("V7oI5RKL7QRbpNZMtDPiJw==", result.EncryptedOfferId);
            Assert.Equal("RoEqXHXTu27mST*@@**J9blOXQ==", result.EncryptedPlaintRequestId);
        }

        [Fact]
        public async Task SavePlaintNotesTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "041003000000,041003000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var model = new PlaintNotesModel() { Notes = "plaint notes", plaintId = Util.Encrypt(200) };

            var result = await _communicationRequestController.SavePlaintNotes(model);
            Assert.NotNull(result);
            Assert.Equal("plaint notes", result.Notes);
            Assert.Equal("GzwAYkLjxrFTI39cfjy5Pw==", result.plaintId);
        }

        [Fact]
        public async Task SaveEscalationNotesTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000" ) ,
            new Claim(IdentityConfigs.Claims.CommitteeId, "2")};
            _communicationRequestController = _communicationRequestController.WithIdentity(_claims);

            var model = new PlaintRequestModel() { Notes = "escalation notes", EncryptedPlaintId = Util.Encrypt(43) };

            var result = await _communicationRequestController.SaveEscalationNotes(model);
            Assert.NotNull(result);
            Assert.Equal("escalation notes", result.Notes);
            Assert.Equal("d9wmdwODjb600Iq8E*@@**wWdA==", result.EncryptedPlaintId);

        }


        #endregion
    }
}
