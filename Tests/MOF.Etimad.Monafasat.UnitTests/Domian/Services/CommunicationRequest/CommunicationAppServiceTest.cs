using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.CommunicationRequestDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.CommunicationRequest
{
    public class CommunicationAppServiceTest
    {
        private readonly Mock<IQuantityTemplatesProxy> _moqQuantityTableProxy;
        private readonly Mock<INegotiationQueries> _moqNegotiationQueries;
        private readonly Mock<INegotiationCommands> _moqNegotiationCommands;
        private readonly Mock<IOfferQueries> _moqOfferQueries;
        private readonly Mock<IOfferAppService> _moqOfferAppService;
        private readonly Mock<IOfferCommands> _moqOfferCommands;
        private readonly Mock<ICommunicationQueries> _moqCommunicationQueries;
        private readonly Mock<ICommunicationCommands> _moqCommunicationCommands;
        private readonly Mock<IIDMQueries> _moqIIDMQueries;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<ITenderQueries> _moqTenderQueries;
        private readonly Mock<ITenderCommands> _moqTenderCommand;
        private readonly Mock<ITenderDomainService> _moqTenderDomainService;
        private readonly AppDbContext _moqAppDbContext;
        private readonly Mock<IIDMAppService> _moqIIDMAppService;
        private readonly Mock<IHttpContextAccessor> _moqHttpContext;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _moqRootConfigurationMock;
        private readonly Mock<IMapper> _moqMapper;
        private readonly Mock<INotificationAppService> _moqNotificationAppService;
        private readonly Mock<ITenderAppService> _moqTenderAppService;
        private readonly Mock<ICommunicationDomainService> _moqCommunicationDomainService;
        private readonly CommunicationAppService _sutCommunicationService;

        const string _agencyCode = "022001000000";
        const string _Cr = "1299659801";
        public CommunicationAppServiceTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
              .UseInMemoryDatabase(databaseName: "TestingDB")
              .Options;
            _moqHttpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _moqHttpContext.Object);
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqTenderQueries = new Mock<ITenderQueries>();
            _moqTenderCommand = new Mock<ITenderCommands>();
            _moqTenderDomainService = new Mock<ITenderDomainService>();
            _moqRootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _moqQuantityTableProxy = new Mock<IQuantityTemplatesProxy>();
            _moqIIDMAppService = new Mock<IIDMAppService>();
            _moqMapper = new Mock<IMapper>();
            _moqNotificationAppService = new Mock<INotificationAppService>();
            _moqIIDMQueries = new Mock<IIDMQueries>();
            _moqCommunicationDomainService = new Mock<ICommunicationDomainService>();
            _moqTenderAppService = new Mock<ITenderAppService>();
            _moqNegotiationQueries = new Mock<INegotiationQueries>();
            _moqNegotiationCommands = new Mock<INegotiationCommands>();
            _moqOfferQueries = new Mock<IOfferQueries>();
            _moqOfferAppService = new Mock<IOfferAppService>();
            _moqOfferCommands = new Mock<IOfferCommands>();
            _moqCommunicationQueries = new Mock<ICommunicationQueries>();
            _moqCommunicationCommands = new Mock<ICommunicationCommands>();

            _moqRootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForOfferTimesConfiguration());


            _sutCommunicationService = new CommunicationAppService(_moqNegotiationCommands.Object, _moqNegotiationQueries.Object, _moqCommunicationQueries.Object, _moqCommunicationCommands.Object,
                _moqCommunicationDomainService.Object, _moqMapper.Object, _moqCommandRepository.Object, _moqTenderCommand.Object, _moqTenderQueries.Object, _moqTenderAppService.Object,
                _moqHttpContext.Object, _moqIIDMQueries.Object, _moqOfferQueries.Object, _moqOfferCommands.Object, _moqTenderDomainService.Object, _moqNotificationAppService.Object, _moqIIDMAppService.Object
                , _moqRootConfigurationMock.Object, _moqQuantityTableProxy.Object);
        }

        [Fact]
        public void ShouldConstructCommunicationAppService()
        {
            Assert.NotNull(_sutCommunicationService);
            Assert.IsType<CommunicationAppService>(_sutCommunicationService);
        }


        [Theory]
        [InlineData(1076, 1, 60179)]
        public async Task ShouldUserAccessExtendOffersValiditySuccessWhenTenderIsLowBudgetDirectPurchase(int agencyRequestId, int tenderId, int userId)
        {
            _moqTenderAppService.Setup(m => m.FindTenderByTenderId(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Tender>(new Tender() { IsLowBudgetDirectPurchase = true, DirectPurchaseMemberId = 60179 });
            });
            var result = await _sutCommunicationService.GetExtendOffersValidity(agencyRequestId, tenderId, userId);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1076, 0, 60179)]
        public async Task ShouldUserAccessExtendOffersValiditySuccessWhenTenderIsNotLowBudgetDirectPurchase(int agencyRequestId, int tenderId, int userId)
        {
            Tender tender = new TenderDefault().GetGeneralTender();
            _moqCommunicationQueries.Setup(m => m.GetTenderByAgencyRequestId(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Tender>(tender);
            });
            var result = await _sutCommunicationService.GetExtendOffersValidity(agencyRequestId, tenderId, userId);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1076, 1, 60179)]
        public async Task ShouldThrowUnHandledAccessExceptionWhenTenderIsNotLowBudgetDirectPurchaseAndPurchaseCommiteeIsNull(int agencyRequestId, int tenderId, int userId)
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            _moqTenderAppService.Setup(m => m.FindTenderByTenderId(It.IsAny<int>()))
          .Returns(() =>
          {
              return Task.FromResult<Tender>(new Tender() { IsLowBudgetDirectPurchase = null });
          });
            await Assert.ThrowsAsync<UnHandledAccessException>(() => _sutCommunicationService.GetExtendOffersValidity(agencyRequestId, tenderId, userId));
        }

        [Fact]
        public async Task Should_FindTenderForPlaintRequestById()
        {
            _moqCommunicationQueries.Setup(q => q.FindTenderForPlaintRequestById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<TenderPlaintDatailsModel>(new CommunicationRequestDefault().GetTenderPlaintDatailsModel()); });
            var result = await _sutCommunicationService.FindTenderForPlaintRequestById(It.IsAny<int>(), It.IsAny<string>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetEscalatedTenders()
        {
            _moqCommunicationQueries.Setup(q => q.GetEscalatedTenders(It.IsAny<EscalationSearchCriteria>()))
                .Returns(() => { return Task.FromResult<QueryResult<EscalatedTendersModel>>(new CommunicationRequestDefault().GetEscalatedTendersModelQueryResult()); });
            var result = await _sutCommunicationService.GetEscalatedTenders(It.IsAny<EscalationSearchCriteria>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_EscalatePlaint()
        {
            PlaintRequest plaintRequest = new CommunicationRequestDefault().GetPlaintRequest();
            plaintRequest.AgencyCommunicationRequest.AddTender(new TenderDefault().GetGeneralTender());

            _moqCommunicationQueries.Setup(q => q.FindPlaintRequestToEscalateById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<PlaintRequest>(plaintRequest); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForUsersByRoleName(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<MainNotificationTemplateModel>()
                , It.IsAny<List<int>>(), It.IsAny<string>(), It.IsAny<int>()))
                .Verifiable();

             await _sutCommunicationService.EscalatePlaint(Util.Encrypt(1), "1", "attachment", _Cr);
            _moqCommunicationCommands.Verify(c => c.UpdatePlaintRequestAsync(It.IsAny<PlaintRequest>()), Times.Once);
        }

        [Fact]
        public async Task Should_FindTenderEscalatedPlaintRequestsByTenderId()
        {
            _moqCommunicationQueries.Setup(q => q.FindEscalatedTenderPlaintRequestsByTenderId(It.IsAny<PlaintSearchCriteria>()))
                .Returns(() => { return Task.FromResult<QueryResult<TenderPlaintCheckingModel>>(new CommunicationRequestDefault().GetTenderPlaintCheckingModelQueryResult()); });
            var result = await _sutCommunicationService.FindTenderEscalatedPlaintRequestsByTenderId(It.IsAny<PlaintSearchCriteria>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderPlaintRequestsByTenderId()
        {
            _moqCommunicationQueries.Setup(q => q.FindTenderPlaintRequestsByTenderId(It.IsAny<PlaintSearchCriteria>()))
                .Returns(() => { return Task.FromResult<QueryResult<TenderPlaintCheckingModel>>(new CommunicationRequestDefault().GetTenderPlaintCheckingModelQueryResult()); });
            var result = await _sutCommunicationService.FindTenderPlaintRequestsByTenderId(It.IsAny<PlaintSearchCriteria>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_SavePlaintNotes()
        {
            PlaintNotesModel plaintNotesModel = new PlaintNotesModel() { plaintId = Util.Encrypt(1), Notes = "notes" };

            _moqCommunicationQueries.Setup(q => q.FindPlaintById(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<PlaintRequest>(new CommunicationRequestDefault().GetPlaintRequest()); });

            var result = await _sutCommunicationService.SavePlaintNotes(plaintNotesModel, "1", 1);
            _moqCommunicationCommands.Verify(c => c.UpdatePlaintRequestAsync(It.IsAny<PlaintRequest>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_CreateCommunicationRequest()
        {
            TenderPlaintCommunicationRequestModel model = new TenderPlaintCommunicationRequestModel() { EncryptedTenderId = Util.Encrypt(1) };

            _moqCommunicationQueries.Setup(t => t.FindTenderWithPlaintRequestByTenderId(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToCreatePlain(It.IsAny<Tender>(), It.IsAny<string>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            var result = await _sutCommunicationService.CreateCommunicationRequest(model, It.IsAny<string>());
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_RejectPlaint()
        {
            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationPalintRequestsByRequestId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderPLaintCommunicationModel>(new CommunicationRequestDefault().GetTenderPLaintCommunicationModel()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToSendPlaintDecission(It.IsAny<TenderPLaintCommunicationModel>()))
                .Verifiable();
            _moqCommunicationQueries.Setup(t => t.FindCommunicationRequestByIdForPlaint(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            var result = await _sutCommunicationService.RejectPlaint(Util.Encrypt(1), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>());
            _moqCommunicationCommands.Verify(g => g.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_AcceptPlaint()
        {
            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationPalintRequestsByRequestId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderPLaintCommunicationModel>(new CommunicationRequestDefault().GetTenderPLaintCommunicationModel()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToSendPlaintDecission(It.IsAny<TenderPLaintCommunicationModel>()))
                .Verifiable();
            _moqCommunicationQueries.Setup(t => t.FindCommunicationRequestByIdForPlaint(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            var result = await _sutCommunicationService.AcceptPlaint(Util.Encrypt(1), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>());
            _moqCommunicationCommands.Verify(g => g.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_RejectPlaintCommunicationRequest()
        {
            _moqCommunicationQueries.Setup(t => t.FindCommunicationRequestByIdForRejectPlaint(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationPalintRequestsById(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(() => { return Task.FromResult<TenderPLaintCommunicationModel>(new CommunicationRequestDefault().GetTenderPLaintCommunicationModel()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToManagerToChecklPlaint(It.IsAny<TenderPLaintCommunicationModel>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqCommunicationCommands.Setup(t => t.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });

            var result = await _sutCommunicationService.RejectPlaintCommunicationRequest(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());
            _moqCommunicationCommands.Verify(g => g.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_ApprovePlaintCommunicationRequest()
        {
            MockUser();

            _moqTenderQueries.Setup(v => v.FindVerificationCode(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<VerificationCode>(new VerificationCode("1", 1, 1, 1)); });
            _moqTenderDomainService.Setup(d => d.IsValidVerificationCode(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationPalintRequestsById(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(() => { return Task.FromResult<TenderPLaintCommunicationModel>(new CommunicationRequestDefault().GetTenderPLaintCommunicationModel()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToManagerToChecklPlaint(It.IsAny<TenderPLaintCommunicationModel>()))
                .Verifiable();
            _moqCommunicationQueries.Setup(t => t.FindCommunicationRequestByIdForApprovePlaint(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqCommunicationQueries.Setup(t => t.GetCommunicationRequestWithNegotiationAndExtendOffersValdityByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<AgencyCommunicationRequest>>(new CommunicationRequestDefault().GetAgencyCommunicationRequests()); });
            _moqCommunicationDomainService.Setup(t => t.GetPostqualificationOnTenderForCancel(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<Tender>>(new QualificationDefault().GetPostQualifications()); });
            _moqOfferQueries.Setup(t => t.GetNegotiationFirstStageSuppliersByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<NegotiationFirstStageSupplier>>(new NegotiationDefaults().GetNegotiationFirstStageSupplier()); });
            _moqCommunicationQueries.Setup(t => t.GetAllPlaintRequestsByRequestId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<PlaintRequest>>(new CommunicationRequestDefault().GetPlaintRequests()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();

            await _sutCommunicationService.ApprovePlaintCommunicationRequest(1, "1", It.IsAny<string>(), It.IsAny<int>(), 1);
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_FindPlaintById()
        {
            _moqCommunicationQueries.Setup(q => q.FindPlaintById(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<PlaintRequest>(new CommunicationRequestDefault().GetPlaintRequest()); });
            var result = await _sutCommunicationService.FindPlaintById(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindEscalationByPlaintId()
        {
            _moqCommunicationQueries.Setup(q => q.FindEscalationByPlaintId(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<EscalationRequest>(new CommunicationRequestDefault().GetEscalationRequest()); });
            var result = await _sutCommunicationService.FindEscalationByPlaintId(It.IsAny<int>(), It.IsAny<string>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_SaveEscalationNotes()
        {
            PlaintRequestModel plaintNotesModel = new PlaintRequestModel() { PlainRequestId = Util.Encrypt(1), EscalationNotes = "notes" };

            _moqCommunicationQueries.Setup(q => q.FindEscalationByPlaintId(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<EscalationRequest>(new CommunicationRequestDefault().GetEscalationRequest()); });

            var result = await _sutCommunicationService.SaveEscalationNotes(plaintNotesModel, "1");
            _moqCommunicationCommands.Verify(c => c.UpdateEscalationRequestAsync(It.IsAny<EscalationRequest>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_AcceptEscalationCommunicationRequest()
        {
            _moqCommunicationQueries.Setup(q => q.FindEscalationRequestById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForUsersByRoleName(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<MainNotificationTemplateModel>()
                , It.IsAny<List<int>>(), It.IsAny<string>(), It.IsAny<int>()))
                .Verifiable();
            _moqCommunicationCommands.Setup(q => q.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });

            var result = await _sutCommunicationService.AcceptEscalationCommunicationRequest(It.IsAny<int>(), (int)Enums.TenderPlaintRequestProcedure.Other, "details");
            _moqCommunicationCommands.Verify(c => c.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_RejectEscalationCommunicationRequest()
        {
            _moqCommunicationQueries.Setup(q => q.FindEscalationRequestById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForUsersByRoleName(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<MainNotificationTemplateModel>()
                , It.IsAny<List<int>>(), It.IsAny<string>(), It.IsAny<int>()))
                .Verifiable();
            _moqCommunicationCommands.Setup(q => q.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });

            var result = await _sutCommunicationService.RejectEscalationCommunicationRequest(It.IsAny<int>(), "reason");
            _moqCommunicationCommands.Verify(c => c.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_ApproveEscalationCommunicationRequest()
        {
            MockUser();

            _moqTenderQueries.Setup(v => v.FindVerificationCode(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<VerificationCode>(new VerificationCode("1", 1, 1, 1)); });
            _moqTenderDomainService.Setup(d => d.IsValidVerificationCode(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationEscalatedPalintsByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderEscalatedPLaintModel>(new CommunicationRequestDefault().GetTenderEscalatedPLaintModel()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToManagerToChecklPlaint(It.IsAny<TenderPLaintCommunicationModel>()))
                .Verifiable();
            _moqCommunicationQueries.Setup(t => t.GetAgencyCommunicationRequestForApproval(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqCommunicationQueries.Setup(t => t.GetCommunicationRequestWithNegotiationAndExtendOffersValdityByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<AgencyCommunicationRequest>>(new CommunicationRequestDefault().GetAgencyCommunicationRequests()); });
            _moqCommunicationDomainService.Setup(t => t.GetPostqualificationOnTenderForCancel(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<Tender>>(new QualificationDefault().GetPostQualifications()); });
            _moqOfferQueries.Setup(t => t.GetNegotiationFirstStageSuppliersByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<NegotiationFirstStageSupplier>>(new NegotiationDefaults().GetNegotiationFirstStageSupplier()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();

            await _sutCommunicationService.ApproveEscalationCommunicationRequest(1, "1", It.IsAny<string>(), It.IsAny<int>(), 1);
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_RejectEscalationCommunicationRequestApproval()
        {
            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationEscalatedPalintsByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderEscalatedPLaintModel>(new CommunicationRequestDefault().GetTenderEscalatedPLaintModel()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToManagerToChecklEscalation(It.IsAny<TenderEscalatedPLaintModel>()))
                .Verifiable();
            _moqCommunicationQueries.Setup(t => t.GetAgencyCommunicationRequestForReject(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqCommunicationCommands.Setup(t => t.UpdateAgencytRequestAsync(It.IsAny<AgencyCommunicationRequest>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });

            var result = await _sutCommunicationService.RejectEscalationCommunicationRequestApproval(It.IsAny<int>(), It.IsAny<string>());
            _moqCommunicationCommands.Verify(g => g.UpdateAgencytRequestAsync(It.IsAny<AgencyCommunicationRequest>()));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_isAllowedToApplySecondStageNegotiation()
        {
            _moqCommunicationQueries.Setup(q => q.IsFirstStageNegotiationExist(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<bool>(true); });
            var result = await _sutCommunicationService.isAllowedToApplySecondStageNegotiation(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_CreateSupplierExtendOfferDatesAgencyRequest()
        {
            ExtendOffersDateRequestModel requestModel = new ExtendOffersDateRequestModel()
            {
                TenderId = 1,
                RequestReason = "d",
                RequestedDate = DateTime.Now,
                Cr = _Cr
            };
            _moqOfferQueries.Setup(t => t.GetTenderbyTenderIdAsync(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqCommunicationQueries.Setup(t => t.FindCommunicationRequestByTenderIdAndCr(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<SupplierExtendOfferDatesRequest>(new CommunicationRequestDefault().GetSupplierExtendOfferDatesRequest()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToCreateRequest(It.IsAny<SupplierExtendOfferDatesRequest>(), It.IsAny<DateTime>(), It.IsAny<Tender>()))
                .Verifiable();
            _moqCommunicationCommands.Setup(t => t.CreateAsync(It.IsAny<AgencyCommunicationRequest>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqCommunicationCommands.Setup(t => t.CreateSupplierExtendOfferDatesRequestAsync(It.IsAny<SupplierExtendOfferDatesRequest>()))
                .Returns(() => { return Task.FromResult<SupplierExtendOfferDatesRequest>(new CommunicationRequestDefault().GetSupplierExtendOfferDatesRequest()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            
            await _sutCommunicationService.CreateSupplierExtendOfferDatesAgencyRequest(requestModel);
            _moqCommunicationCommands.Verify(g => g.UpdateAgencytRequestAsync(It.IsAny<AgencyCommunicationRequest>()));
        }

        [Fact]
        public async Task Should_ApproveSupplierExtendOfferDatesRequest()
        {
            _moqCommunicationQueries.Setup(t => t.GetAgencyCommunicationRequestForSupplierExtendDatesRequest(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToApproveSupplierExtendOfferDatesRequest(It.IsAny<AgencyCommunicationRequest>()))
                .Verifiable();
            _moqCommunicationCommands.Setup(t => t.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()))
               .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });

            var result = await _sutCommunicationService.ApproveSupplierExtendOfferDatesRequest(It.IsAny<int>());
            _moqCommunicationCommands.Verify(g => g.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_RejectSupplierExtendOfferDatesRequest()
        {
            AgencyCommunicationRequest agencyCommunicationRequest = new CommunicationRequestDefault().GetAgencyCommunicationRequestById();
            agencyCommunicationRequest.SetSupplierExtendOfferDatesForUnitTest(new CommunicationRequestDefault().GetSupplierExtendOfferDatesRequest());

            _moqCommunicationQueries.Setup(t => t.GetAgencyCommunicationRequestForSupplierExtendDatesRequest(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(agencyCommunicationRequest); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToRejectSupplierExtendOfferDatesRequest(It.IsAny<AgencyCommunicationRequest>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();
            _moqCommunicationCommands.Setup(t => t.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()))
               .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });

            var result = await _sutCommunicationService.RejectSupplierExtendOfferDatesRequest(It.IsAny<int>());
            _moqCommunicationCommands.Verify(g => g.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()));
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindSupplierExtendOfferDatesAgencyRequestRequestById()
        {
            _moqCommunicationQueries.Setup(q => q.FindSupplierExtendOfferDatesRequestById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<SupplierExtendOfferDatesAgencyRequestModel>(new CommunicationRequestDefault().GetSupplierExtendOfferDatesAgencyRequestModel()); });
            var result = await _sutCommunicationService.FindSupplierExtendOfferDatesAgencyRequestRequestById(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindSupplierExtendOfferDatesRequestsByTenderId()
        {
            _moqCommunicationQueries.Setup(q => q.FindSupplierExtendOfferDatesRequestsByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<SupplierExtendOfferDatesAgencyRequestModel>>(new CommunicationRequestDefault().GetSupplierExtendOfferDatesAgencyRequestModels()); });
            var result = await _sutCommunicationService.FindSupplierExtendOfferDatesRequestsByTenderId(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetCommunicationRequestByRequestId()
        {
            _moqCommunicationQueries.Setup(q => q.GetAgencyCommunicationRequestById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            var result = await _sutCommunicationService.GetCommunicationRequestByRequestId(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_SendToApproveExtendOffersRequest()
        {

            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationRequestByIdForSendToApproval(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(new CommunicationRequestDefault().GetAgencyCommunicationRequestById()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqHttpContext.Setup(a => a.HttpContext.User.Claims)
               .Returns(() => { return new List<Claim> { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "AgencyName," + _agencyCode) }; });


            await _sutCommunicationService.SendToApproveExtendOffersRequest(It.IsAny<int>());
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<AgencyCommunicationRequest>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_ApproveExtendOffersRequestAsync()
        {
           // MockUser();
            AgencyCommunicationRequest agencyCommunicationRequest = new CommunicationRequestDefault().GetAgencyCommunicationRequestById();
            agencyCommunicationRequest.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, "");

            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationRequestByIdForApproval(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(agencyCommunicationRequest); });
            _moqCommunicationQueries.Setup(t => t.GetOffersToSendExtendOfferValidityByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<Offer>>(new OfferDefaults().GetOfferList()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqHttpContext.Setup(a => a.HttpContext.User.Claims)
                .Returns(() => { return new List<Claim> { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "AgencyName,"+ _agencyCode) }; });

            await _sutCommunicationService.ApproveExtendOffersRequestAsync(It.IsAny<int>());
            _moqCommunicationCommands.Verify(g => g.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()));
        }

        [Fact]
        public async Task Should_RejectExtendOffersRequest()
        {
            MockUser();
            AgencyCommunicationRequest agencyCommunicationRequest = new CommunicationRequestDefault().GetAgencyCommunicationRequestById();
            agencyCommunicationRequest.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, "");

            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationRequestById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(agencyCommunicationRequest); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToRejectExtendOffersRequest(It.IsAny<AgencyCommunicationRequest>()))
                .Verifiable();

            await _sutCommunicationService.RejectExtendOffersRequest(It.IsAny<int>(), It.IsAny<string>());
            _moqCommunicationCommands.Verify(g => g.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()));
        }

        [Fact]
        public async Task Should_DeleteExtendOffersRequestAsync()
        {
            MockUser();
            AgencyCommunicationRequest agencyCommunicationRequest = new CommunicationRequestDefault().GetAgencyCommunicationRequestById();
            agencyCommunicationRequest.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, "");

            _moqCommunicationQueries.Setup(t => t.FindAgencyCommunicationRequestByIdForDelete(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<AgencyCommunicationRequest>(agencyCommunicationRequest); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToDeleteExtendOffersRequest(It.IsAny<AgencyCommunicationRequest>()))
                .Verifiable();

            await _sutCommunicationService.DeleteExtendOffersRequestAsync(It.IsAny<int>());
            _moqCommunicationCommands.Verify(g => g.UpdateAsync(It.IsAny<AgencyCommunicationRequest>()));
        }

        [Fact]
        public async Task Should_AcceptExtendOffersValidityBySupplier()
        {
            MockUser();
            AgencyCommunicationRequest agencyCommunicationRequest = new CommunicationRequestDefault().GetAgencyCommunicationRequestById();
            agencyCommunicationRequest.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, "");

            _moqCommunicationQueries.Setup(t => t.GetExtendOffersValiditySupplierById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<ExtendOffersValiditySupplier>(new CommunicationRequestDefault().GetExtendOffersValiditySupplierById()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToAcceptExtendOffersValidityBySupplier(It.IsAny<ExtendOffersValiditySupplier>()))
                .Verifiable();
            _moqOfferQueries.Setup(t => t.GetTenderbyTenderIdAsync(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderWithCommittees()); });
            _moqNotificationAppService.Setup(o => o.SendNotificationByUserId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sutCommunicationService.AcceptExtendOffersValidityBySupplier(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>());
            _moqCommunicationCommands.Verify(g => g.UpdateExtendOffersValiditySupplier(It.IsAny<ExtendOffersValiditySupplier>()));
        }

        [Fact]
        public async Task Should_RejectExtendOffersValidityBySupplier()
        {
            MockUser();

            _moqCommunicationQueries.Setup(t => t.GetExtendOffersValiditySupplierById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<ExtendOffersValiditySupplier>(new CommunicationRequestDefault().GetExtendOffersValiditySupplierById()); });
            _moqCommunicationDomainService.Setup(d => d.IsValidToRejectExtendOffersValidityBySupplier(It.IsAny<ExtendOffersValiditySupplier>()))
                .Verifiable();
            _moqCommunicationCommands.Setup(g => g.UpdateExtendOffersValiditySupplier(It.IsAny<ExtendOffersValiditySupplier>()))
                .Returns(() => { return Task.FromResult<ExtendOffersValiditySupplier>(new CommunicationRequestDefault().GetExtendOffersValiditySupplierById()); });
            _moqOfferQueries.Setup(t => t.FindOfferWithQuantityTableByTenderIdAndCR(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithQt()); });
            _moqOfferQueries.Setup(t => t.GetTenderbyTenderIdAsync(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderWithCommittees()); });

            _moqNotificationAppService.Setup(o => o.SendNotificationByUserId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sutCommunicationService.RejectExtendOffersValidityBySupplier(It.IsAny<int>(), _Cr, It.IsAny<string>());
            _moqCommunicationCommands.Verify(g => g.UpdateExtendOffersValiditySupplier(It.IsAny<ExtendOffersValiditySupplier>()));
            _moqOfferCommands.Verify(g => g.UpdateAsync(It.IsAny<Offer>()));
        }

        [Fact]
        public async Task Should_CancelSupplierExtendOfferValidity()
        {
            _moqCommunicationQueries.Setup(t => t.GetExtendOffersValiditySupplierById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<ExtendOffersValiditySupplier>(new CommunicationRequestDefault().GetExtendOffersValiditySupplierById()); });

            await _sutCommunicationService.CancelSupplierExtendOfferValidity(It.IsAny<int>(), It.IsAny<string>());
            _moqCommunicationCommands.Verify(g => g.UpdateExtendOffersValiditySupplier(It.IsAny<ExtendOffersValiditySupplier>()));
        }

        [Fact]
        public async Task Should_GetSuppliersCommunicationRequestsGridAsync()
        {
            MockUser();
            SimpleTenderSearchCriteria simpleTenderSearchCriteria = new SimpleTenderSearchCriteria();

            _moqCommunicationQueries.Setup(q => q.GetSuppliersCommunicationRequestsGridAsync(It.IsAny<SimpleTenderSearchCriteria>()))
                .Returns(() => { return Task.FromResult<QueryResult<CommunicationRequestGrid>>(new CommunicationRequestDefault().GetCommunicationRequestGridQueryResult()); });

            var result = await _sutCommunicationService.GetSuppliersCommunicationRequestsGridAsync(simpleTenderSearchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAgencyCommunicationRequestsGridAsync()
        {
            MockUser();
            SimpleTenderSearchCriteria simpleTenderSearchCriteria = new SimpleTenderSearchCriteria();

            _moqCommunicationQueries.Setup(q => q.GetAgencyCommunicationRequestsGridAsync(It.IsAny<SimpleTenderSearchCriteria>()))
                .Returns(() => { return Task.FromResult<QueryResult<CommunicationRequestGrid>>(new CommunicationRequestDefault().GetCommunicationRequestGridQueryResult()); });

            var result = await _sutCommunicationService.GetAgencyCommunicationRequestsGridAsync(simpleTenderSearchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindActiveTenderRevisionDateByTenderId()
        {
            _moqTenderQueries.Setup(q => q.FindActiveTenderRevisionDateByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderRevisionDateModel>(new CommunicationRequestDefault().GetTenderRevisionDateModel()); });

            var result = await _sutCommunicationService.FindActiveTenderRevisionDateByTenderId(It.IsAny<int>());
            Assert.NotNull(result);
        }

        private void MockUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var selectedCr = new Claim(IdentityConfigs.Claims.SelectedCR, "1299659801");
            var selectedAgency = new Claim(IdentityConfigs.Claims.SelectedGovAgency, _agencyCode);
            
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            idintity.AddClaim(selectedCr);
            idintity.AddClaim(selectedAgency);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
        }
    }
}
