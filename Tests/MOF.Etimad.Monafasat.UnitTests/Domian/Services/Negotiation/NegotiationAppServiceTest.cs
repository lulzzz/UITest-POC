using AutoMapper;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Negotiation
{


    public class NegotiationAppServiceTest
    {
        private readonly Mock<IOfferQueries> _OfferQueries;
        private readonly Mock<IOfferCommands> _OfferCommands;
        private readonly Mock<INegotiationQueries> _NegotiationQueries;
        private readonly Mock<INegotiationCommands> _NegotiationCommands;
        private readonly Mock<IIDMQueries> _IDMQueries;
        private readonly Mock<ITenderQueries> _TenderQueries;
        private readonly Mock<ITenderAppService> _TenderAppService;
        private readonly Mock<INotificationAppService> _NotificationAppService;
        private readonly Mock<IHttpContextAccessor> _HttpContextAccessor;
        private readonly Mock<ITenderCommands> _TenderCommands;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IGenericCommandRepository> _GenericCommandRepository;
        private readonly Mock<INegotiationDomainService> _NegotiationDomainService;
        private readonly Mock<ICheckFundProxy> _CheckFundProxy;
        private readonly Mock<ISupplierQueries> _supplierQueries;
        private readonly Mock<IVerificationService> _verificationService;
        private readonly Mock<IQuantityTemplatesProxy> _qunatityTemplateProxy;
        private readonly NegotiationAppService _sut;

        public NegotiationAppServiceTest()
        {
            _OfferCommands = new Mock<IOfferCommands>();
            _OfferQueries = new Mock<IOfferQueries>();
            _NegotiationQueries = new Mock<INegotiationQueries>();
            _NegotiationCommands = new Mock<INegotiationCommands>();
            _IDMQueries = new Mock<IIDMQueries>();
            _TenderQueries = new Mock<ITenderQueries>();
            _TenderAppService = new Mock<ITenderAppService>();
            _NotificationAppService = new Mock<INotificationAppService>();
            _HttpContextAccessor = new Mock<IHttpContextAccessor>();
            _TenderCommands = new Mock<ITenderCommands>();
            _GenericCommandRepository = new Mock<IGenericCommandRepository>();
            _NegotiationDomainService = new Mock<INegotiationDomainService>();
            _CheckFundProxy = new Mock<ICheckFundProxy>();
            _mapper = new Mock<IMapper>();
            _supplierQueries = new Mock<ISupplierQueries>();
            _verificationService = new Mock<IVerificationService>();
            _qunatityTemplateProxy = new Mock<IQuantityTemplatesProxy>();
            _sut = new NegotiationAppService(_supplierQueries.Object, _OfferCommands.Object,
                _NegotiationQueries.Object,
                _NegotiationCommands.Object,
                _NegotiationDomainService.Object,
                _mapper.Object,
                _GenericCommandRepository.Object,
                _TenderCommands.Object,
                _TenderQueries.Object,
                _TenderAppService.Object,
                _NotificationAppService.Object,
                _verificationService.Object,
                _HttpContextAccessor.Object,
                _IDMQueries.Object,
                _OfferQueries.Object,
                _CheckFundProxy.Object,
                _qunatityTemplateProxy.Object);
        }

        [Theory]
        [InlineData("N5c2adXf574Qv*@@**DrsYtP1Q==", "N5c2adXf574Qv*@@**DrsYtP1Q==", "32332", "dsds")]
        public async Task SupplierShouldNotAggreeFirstNegotiationWhenStatusIsPassed(string negotiationId, string TenderId, string CR, string CRName)
        {
            // Arrange
            _OfferQueries.Setup(o => o.GetOfferForNegotiation(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaults());
            });
            _NegotiationQueries.Setup(o => o.GetNegotiationFirstStageWithAgency(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<NegotiationFirstStage>(new NegotiationDefaults().GetNegotiationFirstStageWithFaildStatus());
            });

            // Act
            var e = await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.AgreeOnFirstStageNegotiationBySupplier(negotiationId.ToString(), TenderId, CR, CRName));

            // Assert
            Assert.Equal("تم تغير حالة الطلب", e.Message);
        }

        [Theory]
        [InlineData("N5c2adXf574Qv*@@**DrsYtP1Q==", "N5c2adXf574Qv*@@**DrsYtP1Q==", "32332", "dsds")]
        public async Task SupplierShouldNotAggreeFirstNegotiationWhenReplyPeriodEnded(string negotiationId, string TenderId, string CR, string CRName)
        {
            // Arrange
            _OfferQueries.Setup(o => o.GetOfferForNegotiation(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaults());
            });
            _NegotiationQueries.Setup(o => o.GetNegotiationFirstStageWithAgency(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<NegotiationFirstStage>(new NegotiationDefaults().GetNegotiationFirstStageWithTimePassed());
            });

            // Act
            var e = await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.AgreeOnFirstStageNegotiationBySupplier(negotiationId.ToString(), TenderId, CR, CRName));

            // Assert
            Assert.Equal("إنتهى وقت الرد", e.Message);
        }

        [Theory]
        [InlineData(1, 1, "32332", "dsds")]
        public async Task SupplierCanAgreeFirstStageNegotiation(int negotiationId, int tenderId, string CR, string CRName)
        {
            // Arrange
            _OfferQueries.Setup(o => o.GetOfferForNegotiation(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithOfferId());
            });
            _NegotiationQueries.Setup(o => o.GetNegotiationFirstStageWithAgency(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<NegotiationFirstStage>(new NegotiationDefaults().GetNegotiationFirstStage());
            });

            // Act
            await _sut.AgreeOnFirstStageNegotiationBySupplier(Util.Encrypt(negotiationId), Util.Encrypt(tenderId), CR, CRName);

            // Assert
            _NegotiationCommands.Verify(x => x.UpdateNegotiationFirstStageAsync(It.IsAny<NegotiationFirstStage>()), Times.Once);
        }

        [Theory]
        [InlineData("N5c2adXf574Qv*@@**DrsYtP1Q==", "N5c2adXf574Qv*@@**DrsYtP1Q==")]
        public async Task SupplierShouldNotAggreeFirstNegotiationWithExtraDiscountWhenStatusIsPassed(string negotiationId, string TenderId)
        {
            // Arrange
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _HttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            _OfferQueries.Setup(o => o.GetOfferForNegotiation(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaults());
            });
            _NegotiationQueries.Setup(o => o.GetNegotiationFirstStageWithAgency(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<NegotiationFirstStage>(new NegotiationDefaults().GetNegotiationFirstStageWithFaildStatus());
            });
            AcceptNegotiationWithExtraDiscountModel extraDiscountModel = new AcceptNegotiationWithExtraDiscountModel()
            {
                TenderIdString = TenderId,
                NegotiationIdString = negotiationId,
                ExtraDiscountValue = 1000,
            };

            // Act
            var e = await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.AgreeOnNegotiationFirstStageWithExtraDiscount(extraDiscountModel));

            // Assert
            Assert.Equal("تم تغير حالة الطلب", e.Message);
        }

        [Theory]
        [InlineData("N5c2adXf574Qv*@@**DrsYtP1Q==", "N5c2adXf574Qv*@@**DrsYtP1Q==")]
        public async Task SupplierShouldNotAggreeFirstNegotiationWithExtraDiscountWhenReplyPeriodEnded(string negotiationId, string TenderId)
        {
            // Arrange
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _HttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            _OfferQueries.Setup(o => o.GetOfferForNegotiation(It.IsAny<int>(), It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaults());
            });
            _NegotiationQueries.Setup(o => o.GetNegotiationFirstStageWithAgency(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<NegotiationFirstStage>(new NegotiationDefaults().GetNegotiationFirstStageWithTimePassed());
            });
            AcceptNegotiationWithExtraDiscountModel extraDiscountModel = new AcceptNegotiationWithExtraDiscountModel()
            {
                TenderIdString = TenderId,
                NegotiationIdString = negotiationId,
                ExtraDiscountValue = 1000,
            };
            // Act
            var e = await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.AgreeOnNegotiationFirstStageWithExtraDiscount(extraDiscountModel));

            // Assert
            Assert.Equal("إنتهى وقت الرد", e.Message);
        }



        [Fact]
        public async Task ChangeCommunicationRequestStatus_Condition_Result()
        {
            #region Arrange

            _NegotiationQueries.Setup(que => que.FindWithAgencyRequestById(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWithTimePassed()));
            _NegotiationQueries.Setup(que => que.FindWithSuppliersById(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWithTimePassed()));

            _OfferQueries.Setup(que => que.GetTenderbyTenderIdAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new Tender()));



            NegotiationAgencyActionStatusModel negotiationActionStatus = new NegotiationAgencyActionStatusModel();

            #endregion

            await _sut.ChangeCommunicationRequestStatus(new NegotiationAgencyActionStatusModel());


            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task RejectNegotiationRequestFirstStage_WithRejectionReason_UpdateNegotiationStatusToBeRejected()
        {
            #region Arrange
            var negotiationFirstStage = new NegotiationDefaults().GetNegotiationFirstStage();
            _NegotiationQueries.Setup(que => que.FindWithSuppliersById(It.IsAny<int>()))
                .Returns(() => Task.FromResult(negotiationFirstStage));

            _NegotiationCommands.Setup(negCommand => negCommand.UpdateNegotiationFirstStageAsync(negotiationFirstStage))
                .Returns(Task.FromResult(negotiationFirstStage));
            NegotiationAgencyActionStatusModel negotiationActionStatus = new NegotiationAgencyActionStatusModel();
            #endregion

            var result = await _sut.RejectNegotiationRequestFirstStage(negotiationActionStatus);

            Assert.True(result);
        }

        [Theory]
        [InlineData(1, 1, "1010000154")]
        public async Task GetSupplierNegotiationPageInfo_WithIDAndCr_RetuenSuppierViewModel(int tenderId,
            int negotiationId, string cr)
        {
            #region Arrange
            NegotiationSupplierViewModel supplierOfferInfo = new NegotiationSupplierViewModel();
            _OfferQueries.Setup(que => que.GetOfferByTenderAndCR(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new OfferDefaults().GetOfferDefaultsWithOfferId()));

            _NegotiationQueries.Setup(que => que.FindSupplierOfferDetails(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationFirstSatgeSupplierOfferInfo()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWaitingSupplier()));

            _NegotiationQueries.Setup(que => que.FindTenderDetails(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new SupplierTenderMainInfo()));
            #endregion

            var result =
                await _sut.GetSupplierNegotiationPageInfo(Util.Encrypt(tenderId), Util.Encrypt(negotiationId), cr);

            Assert.IsType<NegotiationSupplierViewModel>(result);
        }

        [Theory]
        [InlineData(1, 1, "1010000154")]
        public async Task GetSupplierNegotiationPageInfo_WithSupplierAgrreStatus_ThrowsException(int tenderId,
            int negotiationId, string cr)
        {
            #region Arrange

            NegotiationSupplierViewModel supplierOfferInfo = new NegotiationSupplierViewModel();
            _OfferQueries.Setup(que => que.GetOfferByTenderAndCR(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new OfferDefaults().GetOfferDefaultsWithOfferId()));

            _NegotiationQueries.Setup(que => que.FindSupplierOfferDetails(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationFirstSatgeSupplierOfferInfo()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() =>
                    Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWithSupplierAgreeStatus()));

            _NegotiationQueries.Setup(que => que.FindTenderDetails(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new SupplierTenderMainInfo()));

            #endregion

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _sut.GetSupplierNegotiationPageInfo(Util.Encrypt(tenderId), Util.Encrypt(negotiationId), cr));

        }

        [Theory]
        [InlineData(1, 1, "1010000154")]
        public async Task GetSupplierNegotiationPageInfo_WithSupplierDisAgrreStatus_ThrowsException(int tenderId,
            int negotiationId, string cr)
        {
            #region Arrange

            NegotiationSupplierViewModel supplierOfferInfo = new NegotiationSupplierViewModel();
            _OfferQueries.Setup(que => que.GetOfferByTenderAndCR(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new OfferDefaults().GetOfferDefaultsWithOfferId()));

            _NegotiationQueries.Setup(que => que.FindSupplierOfferDetails(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationFirstSatgeSupplierOfferInfo()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() =>
                    Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWithSupplierDisAgreeStatus()));

            _NegotiationQueries.Setup(que => que.FindTenderDetails(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new SupplierTenderMainInfo()));

            #endregion

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _sut.GetSupplierNegotiationPageInfo(Util.Encrypt(tenderId), Util.Encrypt(negotiationId), cr));

        }


        [Theory]
        [InlineData(1, 1, "1010000154")]
        public async Task GetSupplierNegotiationFirstStageInfo_WithIDAndCr_RetuenSuppierViewModel(int tenderId,
            int negotiationId, string cr)
        {
            #region Arrange
            NegotiationSupplierViewModel supplierOfferInfo = new NegotiationSupplierViewModel();
            _OfferQueries.Setup(que => que.GetOfferByTenderAndCR(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new OfferDefaults().GetOfferDefaultsWithOfferId()));

            _NegotiationQueries.Setup(que => que.FindSupplierOfferDetailsForNegotiationFirstStage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationFirstSatgeSupplierOfferInfo()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWaitingSupplier()));

            _NegotiationQueries.Setup(que => que.FindTenderDetails(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new SupplierTenderMainInfo()));
            #endregion

            var result =
                await _sut.GetSupplierNegotiationFirstStageInfo(Util.Encrypt(tenderId), Util.Encrypt(negotiationId), cr);

            Assert.IsType<NegotiationSupplierViewModel>(result);
        }

        [Theory]
        [InlineData(1, 1, "1010000154")]
        public async Task GetSupplierNegotiationFirstStageInfo_WithSupplierAgrreStatus_ThrowsException(int tenderId,
            int negotiationId, string cr)
        {
            #region Arrange

            NegotiationSupplierViewModel supplierOfferInfo = new NegotiationSupplierViewModel();
            _OfferQueries.Setup(que => que.GetOfferByTenderAndCR(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new OfferDefaults().GetOfferDefaultsWithOfferId()));

            _NegotiationQueries.Setup(que => que.FindSupplierOfferDetailsForNegotiationFirstStage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationFirstSatgeSupplierOfferInfo()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() =>
                    Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWithSupplierAgreeStatus()));

            _NegotiationQueries.Setup(que => que.FindTenderDetails(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new SupplierTenderMainInfo()));

            #endregion

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _sut.GetSupplierNegotiationFirstStageInfo(Util.Encrypt(tenderId), Util.Encrypt(negotiationId), cr));

        }

        [Theory]
        [InlineData(1, 1, "1010000154")]
        public async Task GetSupplierNegotiationFirstStageInfo_WithSupplierDisAgrreStatus_ThrowsException(int tenderId,
            int negotiationId, string cr)
        {
            #region Arrange

            NegotiationSupplierViewModel supplierOfferInfo = new NegotiationSupplierViewModel();
            _OfferQueries.Setup(que => que.GetOfferByTenderAndCR(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new OfferDefaults().GetOfferDefaultsWithOfferId()));

            _NegotiationQueries.Setup(que => que.FindSupplierOfferDetailsForNegotiationFirstStage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => Task.FromResult(new NegotiationFirstSatgeSupplierOfferInfo()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() =>
                    Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWithSupplierDisAgreeStatus()));

            _NegotiationQueries.Setup(que => que.FindTenderDetails(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new SupplierTenderMainInfo()));

            #endregion

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _sut.GetSupplierNegotiationFirstStageInfo(Util.Encrypt(tenderId), Util.Encrypt(negotiationId), cr));

        }



        [Theory]
        [InlineData(1, 1, "1010000154", "Khaled ElSafy")]
        public async Task AgreeOnOfferNegotiationFirstStage_WithWrongStatus_ThrowsBusinessException(
            int negotiationId, int tenderId, string cr, string cRName)
        {
            #region Arrange

            _OfferQueries.Setup(que => que.GetOfferForNegotiation(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new OfferDefaults().GetOfferDefaultsWithOfferId()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() =>
                    Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWithSupplierDisAgreeStatus()));

            #endregion

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _sut.AgreeOnOfferNegotiationFirstStage(Util.Encrypt(negotiationId), Util.Encrypt(tenderId), cr,
                    cRName));
        }

        [Theory]
        [InlineData(1, 1, "1010000154", "Khaled ElSafy")]
        public async Task AgreeOnOfferNegotiationFirstStage_WithPassedTime_ThrowsBusinessException(
            int negotiationId, int tenderId, string cr, string cRName)
        {
            #region Arrange

            _OfferQueries.Setup(que => que.GetOfferForNegotiation(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new Offer()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() =>
                    Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStageWithTimePassed()));

            #endregion

            await Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _sut.AgreeOnOfferNegotiationFirstStage(Util.Encrypt(negotiationId), Util.Encrypt(tenderId), cr,
                    cRName));
            _NegotiationCommands.Verify(c=>c.SaveChanges(),Times.Once);
        }


        [Theory]
        [InlineData(1, 1, "1010000154", "Khaled ElSafy")]
        public async Task AgreeOnOfferNegotiationFirstStage_WithRightData_UpdateNegotiationStatusWithASupplierAggreed(
            int negotiationId, int tenderId, string cr, string cRName)
        {
            #region Arrange

            _OfferQueries.Setup(que => que.GetOfferForNegotiation(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult(new OfferDefaults().GetOfferDefaultsWithOfferId()));

            _NegotiationQueries.Setup(que => que.GetNegotiationFirstStageWithAgency(It.IsAny<int>()))
                .Returns(() =>
                    Task.FromResult(new NegotiationDefaults().GetNegotiationFirstStage()));

            #endregion

            await _sut.AgreeOnOfferNegotiationFirstStage(Util.Encrypt(negotiationId), Util.Encrypt(tenderId), cr,
                cRName);

            _NegotiationCommands.Verify(c => c.UpdateNegotiationFirstStageAsync(It.IsAny<NegotiationFirstStage>()),
                Times.Once);
        }

    }
}
