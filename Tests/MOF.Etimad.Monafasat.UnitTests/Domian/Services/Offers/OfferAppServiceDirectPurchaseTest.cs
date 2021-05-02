using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using Moq;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Offers
{
    public class OfferAppServiceDirectPurchaseTest
    {
        #region Mock
        private readonly Mock<ISupplierQueries> _moqSupplierQueries;
        private readonly Mock<ISupplierCommands> _moqSupplierCommands;
        private readonly Mock<IQuantityTemplatesProxy> _moqQantityTemplatesProxy;
        private readonly Mock<ILocalContentProxy> _moqLocalContentProxy;
        private readonly Mock<ISMEASizeInquiryProxy> _moqSMEASizeInqueryProxy;
        private readonly Mock<IIDMAppService> _moqIdmAppService;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _moqConfiguration;
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly Mock<IBlockAppService> _moqBlockAppService;
        private readonly Mock<IOfferQueries> _moqOfferQueries;
        private readonly Mock<IOfferCommands> _moqOfferCommands;
        private readonly Mock<ITenderQueries> _moqTenderQueries;
        private readonly Mock<ITenderAppService> _moqTenderAppService;
        private readonly Mock<ITenderCommands> _moqTenderCommands;
        private readonly Mock<IMapper> _moqMapper;
        private readonly Mock<INotificationAppService> _moqNotificationAppService;
        private readonly Mock<IOfferDomainService> _moqOfferDomainService;
        private readonly Mock<ILocalContentConfigurationSettings> _moqLocalContentConfigurationSettings;
        private readonly Mock<ILocalContentPreferenceService> _moqLocalContentPreferenceService;

        private readonly OfferAppService _sut;
        public OfferAppServiceDirectPurchaseTest()
        {
            _moqSupplierQueries = new Mock<ISupplierQueries>();
            _moqSupplierCommands = new Mock<ISupplierCommands>();
            _moqQantityTemplatesProxy = new Mock<IQuantityTemplatesProxy>();
            _moqIdmAppService = new Mock<IIDMAppService>();
            _moqConfiguration = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _moqConfiguration.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsFor());
            _moqBlockAppService = new Mock<IBlockAppService>();
            _moqOfferQueries = new Mock<IOfferQueries>();
            _moqOfferCommands = new Mock<IOfferCommands>();
            _moqTenderQueries = new Mock<ITenderQueries>();
            _moqTenderAppService = new Mock<ITenderAppService>();
            _moqTenderCommands = new Mock<ITenderCommands>();
            _moqMapper = new Mock<IMapper>();
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _moqNotificationAppService = new Mock<INotificationAppService>();
            _moqOfferDomainService = new Mock<IOfferDomainService>();
            _moqLocalContentProxy = new Mock<ILocalContentProxy>();
            _moqSMEASizeInqueryProxy = new Mock<ISMEASizeInquiryProxy>();
            _moqLocalContentConfigurationSettings = new Mock<ILocalContentConfigurationSettings>();
            _moqLocalContentPreferenceService = new Mock<ILocalContentPreferenceService>();
        _sut = new OfferAppService(_moqOfferQueries.Object, _moqOfferCommands.Object, _moqOfferDomainService.Object, _moqMapper.Object, _moqNotificationAppService.Object, _moqTenderCommands.Object,
                _moqTenderQueries.Object, _moqTenderAppService.Object, _moqHttpContextAccessor.Object, _moqSupplierQueries.Object, _moqSupplierCommands.Object, _moqIdmAppService.Object,
                _moqQantityTemplatesProxy.Object, _moqBlockAppService.Object, _moqConfiguration.Object, _moqLocalContentProxy.Object, _moqSMEASizeInqueryProxy.Object, 
                _moqLocalContentConfigurationSettings.Object, _moqLocalContentPreferenceService.Object);
        }
        #endregion Mock

        [Fact]
        public async Task Should_SaveOneFileDirectPurchaseChecking()
        {
            MockUser();
            Offer offer = new OfferDefaults().GetOfferDefaultsWithQt();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            ConfigurationSetting configurationSetting = new ConfigurationSetting() { Date = DateTime.Now };

            CheckOfferResultModel checkOfferResultModel = new CheckOfferResultModel()
            {
                TechnicalOfferStatusId = (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
            };
            _moqOfferQueries.Setup(q => q.GetLocalContentSettingsDate())
                .Returns(() => { return Task.FromResult<ConfigurationSetting>(configurationSetting); });
            _moqOfferQueries.Setup(q => q.GetOfferForDirectPurchaseChekingById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(offer); });
            _moqQantityTemplatesProxy.Setup(p => p.GetTotalCostForChecking(It.IsAny<FormConfigurationDTO>()))
                .Returns(() => { return Task.FromResult<ApiResponse<TotalCostDTO>>(new OfferDefaults().TotalCostDTOApiResponse()); });

            await _sut.SaveOneFileDirectPurchaseChecking(checkOfferResultModel);
            _moqOfferCommands.Verify(c => c.UpdateAsync(It.IsAny<Offer>()), Times.Once);
        }

        [Fact]
        public async Task Should_SaveDirectPurchaseAttachmentsChecking()
        {
            CheckOfferResultModel checkOfferResultModel = new CheckOfferResultModel() 
            {
                TechnicalOfferStatusId = (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
            };
            _moqOfferQueries.Setup(q => q.GetOfferForDirectPurchaseChekingById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithQt()); });

            await _sut.SaveTwoFilesTechnicalDirectPurchaseChecking(checkOfferResultModel);
            _moqOfferCommands.Verify(c => c.UpdateAsync(It.IsAny<Offer>()), Times.Once);
        }

        [Fact]
        public async Task Should_SaveTwoFilesFinancialDirectPurchaseChecking()
        {
            Offer offer = new OfferDefaults().GetOfferDefaultsWithQt();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            ConfigurationSetting configurationSetting = new ConfigurationSetting() { Date = DateTime.Now };

            CheckOfferResultModel checkOfferResultModel = new CheckOfferResultModel()
            {
                TechnicalOfferStatusId = (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
            };
            _moqOfferQueries.Setup(q => q.GetLocalContentSettingsDate())
                .Returns(() => { return Task.FromResult<ConfigurationSetting>(configurationSetting); });
            _moqOfferQueries.Setup(q => q.GetOfferForDirectPurchaseChekingById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(offer); });
            _moqQantityTemplatesProxy.Setup(p => p.GetTotalCostForChecking(It.IsAny<FormConfigurationDTO>()))
                .Returns(() => { return Task.FromResult<ApiResponse<TotalCostDTO>>(new OfferDefaults().TotalCostDTOApiResponse()); });

            await _sut.SaveTwoFilesFinancialDirectPurchaseChecking(checkOfferResultModel);
            _moqOfferCommands.Verify(c => c.UpdateAsync(It.IsAny<Offer>()), Times.Once);
        }

        private void MockUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        }
    }
}
