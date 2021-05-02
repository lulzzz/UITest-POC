using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.AgencyDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults.ChangeRequestDefault;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using MOF.Etimad.Monafasat.ViewModel.Tender;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Tenders
{
    public class TenderAppServiceUnitTest
    {
        #region Mock

        private readonly Mock<INotificationAppService> _NotificationService;
        private readonly Mock<ISupplierQueries> _ISupplierQueries;
        private readonly Mock<ISupplierCommands> _ISupplierCommands;
        private readonly Mock<IIDMQueries> _IIDMQueries;
        private readonly Mock<IIDMAppService> _IIDMAppService;
        private readonly Mock<ICommitteeQueries> _ICommitteeQueries;
        private readonly Mock<IBlockAppService> _IBlockAppService;
        private readonly Mock<IMemoryCache> _IMemoryCache;
        private readonly Mock<IBranchAppService> _IBranchAppService;
        private readonly Mock<IBillAppService> _IBillAppService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IVerificationService> _moqVerificationService;
        private readonly Mock<ILogger<TenderAppService>> _loggerMock;
        private readonly Mock<ISRMFrameworkAgreementManageProxy> _moqSRMFrameworkAgreementManageProxy;
        private readonly Mock<ILocalContentSettingsAppService> _moqLocalContentSettingsAppService;

        private readonly TenderAppService _sut;
        private readonly AppDbContext _moqAppDbContext;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<ITenderQueries> _moqTenderQueries;
        private readonly Mock<ITenderCommands> _moqTenderCommand;
        private readonly Mock<ITenderDomainService> _moqTenderDomainService;

        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;

        private readonly Mock<IQuantityTemplatesProxy> _moqQuantityTableProxy;
        private readonly Mock<IOfferQueries> _moqOfferQueries;
        private readonly Mock<ICommunicationQueries> _moqCommunicationQueries;
        private readonly Mock<IBillCommand> _moqBillCommand;
        private readonly Mock<INegotiationQueries> _moqNegotiationQueries;
        private readonly Mock<ILocalContentConfigurationSettings> _moqLocalContentConfigurationSettings;

        const int TENDER_ID = 1;
        const int STOPING_PERIOD = 5;
        const string AGENCY_CODE = "022001000000";
        const string REJECTION_REASON = "Rejection reason";

        const string VERIFICATION_CODE = "123";


        public TenderAppServiceUnitTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB")
                .Options;
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqTenderQueries = new Mock<ITenderQueries>();
            _moqTenderCommand = new Mock<ITenderCommands>();
            _moqTenderDomainService = new Mock<ITenderDomainService>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _moqQuantityTableProxy = new Mock<IQuantityTemplatesProxy>();
            _IIDMAppService = new Mock<IIDMAppService>();
            _mapper = new Mock<IMapper>();
            _NotificationService = new Mock<INotificationAppService>();
            _ISupplierCommands = new Mock<ISupplierCommands>();
            _ISupplierQueries = new Mock<ISupplierQueries>();
            _IIDMQueries = new Mock<IIDMQueries>();
            _moqVerificationService = new Mock<IVerificationService>();
            _ICommitteeQueries = new Mock<ICommitteeQueries>();
            _IBlockAppService = new Mock<IBlockAppService>();
            _IMemoryCache = new Mock<IMemoryCache>();
            _IBranchAppService = new Mock<IBranchAppService>();
            _IBillAppService = new Mock<IBillAppService>();
            _loggerMock = new Mock<ILogger<TenderAppService>>();

            _moqSRMFrameworkAgreementManageProxy = new Mock<ISRMFrameworkAgreementManageProxy>();
            _moqLocalContentSettingsAppService = new Mock<ILocalContentSettingsAppService>();
            _moqOfferQueries = new Mock<IOfferQueries>();
            _moqCommunicationQueries = new Mock<ICommunicationQueries>();
            _moqBillCommand = new Mock<IBillCommand>();
            _moqNegotiationQueries = new Mock<INegotiationQueries>();
            _moqLocalContentConfigurationSettings = new Mock<ILocalContentConfigurationSettings>();

            _rootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForTender());

            _sut = new TenderAppService(_moqQuantityTableProxy.Object, _NotificationService.Object, _moqTenderQueries.Object,
               _moqTenderCommand.Object, _moqTenderDomainService.Object,
                _mapper.Object,
                _IBillAppService.Object,
                _httpContext.Object,
                _IIDMQueries.Object,
                _ICommitteeQueries.Object,
                _ISupplierQueries.Object,
                _IBlockAppService.Object,
                _IIDMAppService.Object,
                _ISupplierCommands.Object,
                _moqCommandRepository.Object,
                _IMemoryCache.Object,
                _IIDMAppService.Object,
                _IBranchAppService.Object,
                _moqVerificationService.Object,
                _loggerMock.Object,
                _rootConfigurationMock.Object,
                _moqBillCommand.Object,
                _moqLocalContentSettingsAppService.Object,
                _moqSRMFrameworkAgreementManageProxy.Object,
                _moqOfferQueries.Object,
                _moqCommunicationQueries.Object,
                _moqLocalContentConfigurationSettings.Object);
        }
        #endregion Mock

        [Fact]
        public async Task Should_GetAllUnitUsersByRoleName()
        {
            List<EmployeeIntegrationModel> employeeIntegrationModel = new List<EmployeeIntegrationModel>()
            {
                new EmployeeIntegrationModel(){ fullName = "name", mobileNumber = "055555555", email = "a@a.a", nationalId = "1111111111"}
            };
            _IIDMAppService.Setup(i => i.GetEmployeeDetailsByRoleName(It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<List<EmployeeIntegrationModel>>(employeeIntegrationModel); });

            var result = await _sut.GetAllUnitUsersByRoleName(It.IsAny<string>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_OpenTenderByUnitSecretaryAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToOpenUnitStageByUnitSecretary(It.IsAny<Tender>()))
                .Verifiable();

            await _sut.OpenTenderByUnitSecretaryAsync(It.IsAny<string>());
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.CreateAsync(It.IsAny<TenderUnitAssign>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_SendTenderByUnitSecretaryForModificationAsync()
        {
            MoqUser();
            ReturnTenderToDataEntryFromUnitFormodificationsModel modificationModel = new ReturnTenderToDataEntryFromUnitFormodificationsModel()
            {
                notes = "s",
                files = new List<ExtendOffersValidityAttachementViewModel>()
            };
            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToReturnTenderToDataEntryForEdit(It.IsAny<Tender>()))
                .Verifiable();
            _NotificationService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _NotificationService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sut.SendTenderByUnitSecretaryForModificationAsync(modificationModel);
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_SendToLevelTwoFromUnitSecretaryLevelOneAsync()
        {
            MoqUser();
            ReturnTenderToDataEntryFromUnitFormodificationsModel modificationModel = new ReturnTenderToDataEntryFromUnitFormodificationsModel()
            {
                notes = "s",
                files = new List<ExtendOffersValidityAttachementViewModel>()
            };
            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToSendToLevelTwoFromUnitSecretaryLevelOne(It.IsAny<Tender>(), It.IsAny<string>()))
                .Verifiable();
            _IIDMAppService.Setup(i => i.FindUserProfileByUserNameAsync(It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<UserProfile>(new AgencyDefaults().GetUserProfileData()); });
            _NotificationService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sut.SendToLevelTwoFromUnitSecretaryLevelOneAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.CreateAsync(It.IsAny<TenderUnitAssign>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_ApproveTenderByUnitSecretaryAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToUpdateApproveTenderByUnitSecretary(It.IsAny<Tender>()))
                .Verifiable();

            await _sut.ApproveTenderByUnitSecretaryAsync(It.IsAny<string>(), It.IsAny<bool>());
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task Should_RejectTenderByUnitSecretaryAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToUpdateRejectTenderByUnitSecretary(It.IsAny<Tender>(), It.IsAny<string>()))
                .Verifiable();

            await _sut.RejectTenderByUnitSecretaryAsync(It.IsAny<string>(), "comment");
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task Should_SendToApproveFromUnitSecretaryToUnitMangerAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToSendToApproveByUnitManager(It.IsAny<Tender>()))
                .Verifiable();
            _NotificationService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sut.SendToApproveFromUnitSecretaryToUnitMangerAsync(It.IsAny<string>());
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task Should_ReOpenTenderByUnitSecertaryAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToReOpenTenderByUnitSecretary(It.IsAny<Tender>()))
                .Verifiable();

            await _sut.ReOpenTenderByUnitSecertaryAsync(It.IsAny<string>());
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.CreateAsync(It.IsAny<TenderUnitAssign>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_OpenTenderByUnitSecretaryLevelTwoAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToOpenUnitStageByUnitSecretaryLevelTwo(It.IsAny<Tender>()))
                .Verifiable();

            await _sut.OpenTenderByUnitSecretaryLevelTwoAsync(It.IsAny<string>());
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_SendTenderByUnitSecretaryLevelTwoForModificationAsync()
        {
            MoqUser();
            ReturnTenderToDataEntryFromUnitFormodificationsModel modificationModel = new ReturnTenderToDataEntryFromUnitFormodificationsModel()
            {
                notes = "s",
                files = new List<ExtendOffersValidityAttachementViewModel>()
            };
            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToReturnTenderToDataEntryForEditLevelTwo(It.IsAny<Tender>()))
                .Verifiable();
            _NotificationService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _NotificationService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sut.SendTenderByUnitSecretaryLevelTwoForModificationAsync(modificationModel);
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_ApproveTenderByUnitSecretaryLevelTwoAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToUpdateApproveTenderByUnitSecretaryLevelTwo(It.IsAny<Tender>()))
                .Verifiable();

            await _sut.ApproveTenderByUnitSecretaryLevelTwoAsync(It.IsAny<string>());
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task Should_SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToSendToApproveFromLevelToByUnitManager(It.IsAny<Tender>()))
                .Verifiable();
            _NotificationService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sut.SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync(It.IsAny<string>());
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task Should_RejectTenderByUnitSecretaryLevelTwoAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToUpdateRejectTenderByUnitSecretaryLevelTwo(It.IsAny<Tender>(), It.IsAny<string>()))
                .Verifiable();

            await _sut.RejectTenderByUnitSecretaryLevelTwoAsync(It.IsAny<string>(), "comment");
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task Should_ReOpenTenderByUnitSecertaryLevelAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToReOpenTenderByUnitSecretary(It.IsAny<Tender>()))
                .Verifiable();

            await _sut.ReOpenTenderByUnitSecertaryLevelAsync(It.IsAny<string>());
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.CreateAsync(It.IsAny<TenderUnitAssign>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task Should_ReviewTenderByUnitManager()
        {
            MoqUser();

            _moqTenderQueries.Setup(i => i.FindTenderWithUnitHistoryById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToReviewTenderByUnitManager(It.IsAny<Tender>()))
                .Verifiable();

            await _sut.ReviewTenderByUnitManager(It.IsAny<string>());
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()));
            _moqCommandRepository.Verify(g => g.CreateAsync(It.IsAny<TenderUnitAssign>()));
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }



        private void MoqUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var usernum = new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(usernum);
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() => { return idintity; });
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
        }
    }
}
