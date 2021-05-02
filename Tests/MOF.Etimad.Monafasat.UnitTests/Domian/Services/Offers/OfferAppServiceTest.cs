using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.LocalContent;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Offers
{
    public class OfferAppServiceTest
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
        public OfferAppServiceTest()
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
        public async Task Should_AddOffer()
        {
            Offer offer = new OfferDefaults().GetOfferDefaultsWithOfferId();

            _moqOfferQueries.Setup(o => o.CheckOfferAdding(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });
            await _sut.AddOffer(offer);
            _moqOfferCommands.Verify(c => c.CreateAsync(offer), Times.Once);
        }

        [Fact]
        public async Task Should_SendOffer_AndReturnOffer()
        {
            MockUser();

            _moqOfferQueries.Setup(o => o.GetOfferWithTenderDetailsById(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithOfferId());
                });
            _moqTenderQueries.Setup(o => o.FindTenderWithOffer(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });

            _moqOfferDomainService.Setup(d => d.IsValidToSend(It.IsAny<Offer>(), It.IsAny<Tender>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            _moqLocalContentConfigurationSettings.Setup(setting => setting.getLocalContentSettingsDate())
               .Returns(() => Task.FromResult(new ConfigurationSetting() { Id = 1, Date = new DateTime(2021, 4, 1), Description = "local content" }));

            _moqLocalContentProxy.Setup(localcontentProxy => localcontentProxy.GetBaselineScoreInquiry(It.IsAny<string>()))
                .Returns(() => Task.FromResult(new LocalContentServiceResponse<decimal?>() { isSuccess = false }));

            _moqLocalContentProxy.Setup(localcontentProxy => localcontentProxy.GetTargetPlanScoreInquiry(It.IsAny<string>(), It.IsAny<string>()))
               .Returns(() => Task.FromResult(new LocalContentServiceResponse<decimal?>() { content = (decimal)22.01, isSuccess = false }));

            _moqSMEASizeInqueryProxy.Setup(localcontentProxy => localcontentProxy.SMEASizeInquiry(It.IsAny<string>()))
              .Returns(() => Task.FromResult(new LocalContentViewModel() { isSMEA = false}));

            var result = await _sut.SendOffer(It.IsAny<int>(), It.IsAny<string>());
            Assert.NotNull(result);
            _moqOfferCommands.Verify(v => v.UpdateAsync(It.IsAny<Offer>()));

        }

        [Fact]
        public async Task Should_SendOffer_AndReturnOffer_AndSaveLocalContent()
        {
            MockUser();
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.Tender = new TenderDefault().GetGeneralTender();
            offer.Tender.CreateTenderLocalContent();
            _moqOfferQueries.Setup(o => o.GetOfferWithTenderDetailsById(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Offer>(offer);
                });
            _moqTenderQueries.Setup(o => o.FindTenderWithOffer(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });

            _moqOfferDomainService.Setup(d => d.IsValidToSend(It.IsAny<Offer>(), It.IsAny<Tender>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            _moqLocalContentConfigurationSettings.Setup(setting => setting.getLocalContentSettingsDate())
               .Returns(() => Task.FromResult(new ConfigurationSetting() { Id = 1, Date = new DateTime(2021, 4, 1), Description = "local content" }));

            _moqLocalContentProxy.Setup(localcontentProxy => localcontentProxy.GetBaselineScoreInquiry(It.IsAny<string>()))
                .Returns(() => Task.FromResult(new LocalContentServiceResponse<decimal?>() { content = (decimal)22.2, isSuccess = true }));

            _moqLocalContentProxy.Setup(localcontentProxy => localcontentProxy.GetTargetPlanScoreInquiry(It.IsAny<string>(), It.IsAny<string>()))
               .Returns(() => Task.FromResult(new LocalContentServiceResponse<decimal?>() { content = (decimal)22.01, isSuccess = true }));

            _moqSMEASizeInqueryProxy.Setup(smea => smea.SMEASizeInquiry(It.IsAny<string>()))
                .Returns(() => Task.FromResult(new LocalContentViewModel() { isSMEA = true }));

            var result = await _sut.SendOffer(It.IsAny<int>(), It.IsAny<string>());
            Assert.NotNull(result);
            _moqOfferCommands.Verify(v => v.UpdateAsync(It.IsAny<Offer>()));
            _moqLocalContentProxy.Verify(l => l.GetBaselineScoreInquiry(It.IsAny<string>()));
            _moqLocalContentProxy.Verify(l => l.GetTargetPlanScoreInquiry(It.IsAny<string>(), It.IsAny<string>()));
            _moqSMEASizeInqueryProxy.Verify(l => l.SMEASizeInquiry(It.IsAny<string>()));
        }

        [Fact]
        public async Task SendOffer_WithCrNotExistInMoneyMarketUpdateIsExistInMoneyMarketWithFalse()
        {
            MockUser();

            _moqOfferQueries.Setup(o => o.GetOfferWithTenderDetailsById(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithOfferId());
                });
            _moqTenderQueries.Setup(o => o.FindTenderWithOffer(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });

            _moqOfferDomainService.Setup(d => d.IsValidToSend(It.IsAny<Offer>(), It.IsAny<Tender>()))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();
            _moqNotificationAppService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            _moqLocalContentConfigurationSettings.Setup(setting => setting.getLocalContentSettingsDate())
               .Returns(() => Task.FromResult(new ConfigurationSetting() { Id = 1, Date = new DateTime(2021, 4, 1), Description = "local content" }));

            _moqLocalContentProxy.Setup(localcontentProxy => localcontentProxy.GetBaselineScoreInquiry(It.IsAny<string>()))
                .Returns(() => Task.FromResult(new LocalContentServiceResponse<decimal?>() { isSuccess = false }));

            _moqLocalContentProxy.Setup(localcontentProxy => localcontentProxy.GetTargetPlanScoreInquiry(It.IsAny<string>(), It.IsAny<string>()))
               .Returns(() => Task.FromResult(new LocalContentServiceResponse<decimal?>() { content = (decimal)22.01, isSuccess = false }));

            _moqSMEASizeInqueryProxy.Setup(localcontentProxy => localcontentProxy.SMEASizeInquiry(It.IsAny<string>()))
              .Returns(() => Task.FromResult(new LocalContentViewModel() { isSMEA = false }));

            var result = await _sut.SendOffer(It.IsAny<int>(), It.IsAny<string>());
            Assert.False(result.OfferlocalContentDetails.IsIncludedInMoneyMarket);
            Assert.Equal(DateTime.Now.Date, result.OfferlocalContentDetails.IsIncludedInMoneyMarketUpdatedDate.Date);

        }

        [Fact]
        public async Task Should_GeOfferByTenderIdAndOfferIdForOpening()
        {
            MockUser();

            _moqOfferQueries.Setup(o => o.GeOfferByTenderIdAndOfferIdForOpening(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<OfferModel>(new OfferDefaults().GetOfferModel()); });
            _moqOfferQueries.Setup(o => o.GeOfferByTenderIdAndOfferIdForChecking(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithQt()); });
            _moqQantityTemplatesProxy.Setup(o => o.GenerateTemplatesByFormIds(It.IsAny<List<long>>()))
                .Returns(() => { return Task.FromResult<ApiResponse<List<TemplateFormDTO>>>(new OfferDefaults().EmptyTemplateFormDTOsApiResponse()); });

            var result = await _sut.GeOfferByTenderIdAndOfferIdForOpening(It.IsAny<int>(), It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GeOfferByTenderId()
        {
            MockUser();

            _moqOfferQueries.Setup(o => o.GeOfferByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<OfferModel>(new OfferDefaults().GetOfferModel()); });

            var result = await _sut.GeOfferByTenderId(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GeOfferByTenderIdAndOfferIdForChecking()
        {
            MockUser();
            var offer = new OfferDefaults().GetOfferDefaultsWithQt();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(o => o.GeOfferByTenderIdAndOfferIdForChecking(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(offer); });
            
            _moqOfferQueries.Setup(o => o.GetLocalContentSettingsDate())
                .Returns(() => { return Task.FromResult(new ConfigurationSetting() { Id = 1, Date = new DateTime(2021, 4, 1), Description = "local content" }); });
            
            _moqQantityTemplatesProxy.Setup(o => o.GenerateTemplatesByFormIds(It.IsAny<List<long>>()))
                .Returns(() => { return Task.FromResult<ApiResponse<List<TemplateFormDTO>>>(new OfferDefaults().EmptyTemplateFormDTOsApiResponse()); });
            

            var result = await _sut.GeOfferByTenderIdAndOfferIdForChecking(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindOfferQuantityItemsById()
        {
            Offer offer = new OfferDefaults().GetOfferDefaultsWithQt();
            _moqQantityTemplatesProxy.Setup(o => o.GenerateTemplatesByFormIds(It.IsAny<List<long>>()))
                .Returns(() => { return Task.FromResult<ApiResponse<List<TemplateFormDTO>>>(new OfferDefaults().EmptyTemplateFormDTOsApiResponse()); });

            var result = await _sut.FindOfferQuantityItemsById(offer, It.IsAny<bool>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindApplyOfferQuantityItemsById()
        {

            _moqOfferQueries.Setup(q => q.GetOfferQuantityItemsForApplyOffer(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<QuantitiesTemplateModel>(It.IsAny<QuantitiesTemplateModel>()); });
            _moqQantityTemplatesProxy.Setup(p => p.GetMonafasatGOV(It.IsAny<FormConfigurationDTO>()))
            .Returns(() => { return Task.FromResult<ApiResponse<List<HtmlTemplateDto>>>(It.IsAny<ApiResponse<List<HtmlTemplateDto>>>()); });

            var result = await _sut.FindApplyOfferQuantityItemsById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferTableQuantityItems()
        {
            QuantityTableSearchCretriaModel searchCretriaModel = new QuantityTableSearchCretriaModel() { OfferId = 1, QuantityTableId = 1, IsReadOnly = false };
            Offer offer = new OfferDefaults().GetOfferDefaultsWithQt();

            _moqOfferQueries.Setup(q => q.GeOfferByTenderIdAndOfferIdForChecking(searchCretriaModel.OfferId))
                .Returns(() => { return Task.FromResult<Offer>(offer); });
            _moqOfferQueries.Setup(q => q.GetOfferTableQuantityItems(offer, searchCretriaModel.QuantityTableId))
                .Returns(() => { return 1; });
            _moqOfferQueries.Setup(q => q.GetSupplierQTableItemsForChecking(searchCretriaModel))
                .Returns(() => { return Task.FromResult<QueryResult<TenderQuantityItemDTO>>(new OfferDefaults().tenderQuantityItemDTOQueryResult()); });


            var result = await _sut.GetOfferTableQuantityItems(searchCretriaModel);
            _moqQantityTemplatesProxy.Verify(v => v.GetMonafasatSupplierForChecking(It.IsAny<FormConfigurationDTO>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_UpdateOfferCheckingStatus()
        {
            OfferCheckingDetailsModel detailsModel = new OfferCheckingDetailsModel()
            {
                TenderTypeId = (int)Enums.TenderType.NewTender,
                OfferAcceptanceStatusId = (int)Enums.OfferAcceptanceStatusId.AcceptedOffer,
                OfferTechnicalEvaluationStatusId = (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
            };
            Offer offer = new OfferDefaults().GetOfferDefaultsWithQt();
            List<TechnicianReportAttachment> technicianReportAttachments = new List<TechnicianReportAttachment>() { new TechnicianReportAttachment("name", "1", (int)Enums.AttachmentType.TechnicianReport) };
            _moqOfferQueries.Setup(s => s.FindOfferById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(offer); });
            _moqMapper.Setup(m => m.Map<List<TechnicianReportAttachment>>(It.IsAny<List<TechniciansReportAttachmentModel>>()))
                .Returns(() => { return technicianReportAttachments; });
            _moqOfferCommands.Setup(m => m.UpdateAsync(It.IsAny<Offer>()))
                .Returns(() => { return Task.FromResult<Offer>(offer); });

            var result = await _sut.UpdateOfferCheckingStatus(detailsModel);
            _moqOfferCommands.Verify(c => c.UpdateAsync(It.IsAny<Offer>()), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_AddExclusionReason()
        {
            ExclusionReasonAwardingModel exclusion = new ExclusionReasonAwardingModel() { ExclusionReason = "reason" };
            Offer offer = new OfferDefaults().GetOfferDefaultsWithQt();

            _moqOfferQueries.Setup(s => s.FindOfferById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(offer); });
            _moqOfferQueries.Setup(s => s.CanAddExclusionReasonIfTenderHasExtendOfferValidity(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<bool>(false); });

            await _sut.AddExclusionReason(exclusion);
            _moqOfferCommands.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_RemoveExclusionReason()
        {
            ExclusionReasonAwardingModel exclusion = new ExclusionReasonAwardingModel() { ExclusionReason = "reason" };
            Offer offer = new OfferDefaults().GetOfferDefaultsWithQt();

            _moqOfferQueries.Setup(s => s.FindOfferById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(offer); });

            await _sut.RemoveExclusionReason(exclusion);
            _moqOfferCommands.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_GetAllCombinedInvitationForSupplier()
        {
            _moqOfferQueries.Setup(s => s.GetAllCombinedInvitationForSupplier(It.IsAny<CombinedSearchCriteria>()))
                .Returns(() => { return Task.FromResult<QueryResult<CombinedListModel>>(new OfferDefaults().EmptyCombinedListModelsQueryResult()); });

            var result = await _sut.GetAllCombinedInvitationForSupplier(It.IsAny<CombinedSearchCriteria>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetOfferDetailsForFinancialChecking()
        {
            _moqOfferQueries.Setup(s => s.GetOfferDetailsForFinancialCheckingByOfferId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<OfferDetailsForCheckingModel>(new OfferDefaults().GetOfferDetailsForCheckingModel()); });
            _moqTenderQueries.Setup(s => s.FindTenderOfferDetailsByTenderIdForDirectPurchaseCheckStage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });

            await _sut.GetOfferDetailsForFinancialChecking(It.IsAny<string>(), It.IsAny<int>());
        }

        [Fact]
        public async Task Should_AddOfferBid()
        {
            Tender tender = new TenderDefault().GetGeneralTender();
            tender.AddBiddingRounds(new BiddingRound(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), (int)Enums.BiddingRoundStatus.Started, tender.TenderId));
            _moqTenderQueries.Setup(s => s.FindTenderForAddingBiddingOfferByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(tender); });
            _moqTenderQueries.Setup(s => s.FindTenderOffersForBiddingRound(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<Offer>>(new OfferDefaults().GetOfferList()); });
            _moqOfferDomainService.Setup(d => d.IsValidAddBidding(It.IsAny<Tender>(), It.IsAny<List<Offer>>(), It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<string>()))
                .Verifiable();

            await _sut.AddOfferBid(It.IsAny<string>(), It.IsAny<string>(), 5.00m, "1299659801");
            _moqTenderCommands.Verify(t => t.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task Should_AcceptCombinedInvitation()
        {
            MockUser();

            _moqOfferQueries.Setup(s => s.GetCombinedWithOfferAndTenderById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<OfferSolidarity>(new OfferDefaults().GetOfferSolidarityLeader()); });
            _moqOfferDomainService.Setup(d => d.IsVaildToAcceptOrRejectCombinedInvitation(It.IsAny<OfferSolidarity>()))
                .Verifiable();
            _moqNotificationAppService.Setup(n => n.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();

            await _sut.AcceptCombinedInvitation(It.IsAny<int>());
            _moqOfferCommands.Verify(c => c.UpdateCombinedAsync(It.IsAny<OfferSolidarity>()));
        }

        [Fact]
        public async Task Should_RejectCombinedInvitation()
        {
            MockUser();

            _moqOfferQueries.Setup(s => s.GetCombinedWithOfferAndTenderById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<OfferSolidarity>(new OfferDefaults().GetOfferSolidarityLeader()); });
            _moqOfferDomainService.Setup(d => d.IsVaildToAcceptOrRejectCombinedInvitation(It.IsAny<OfferSolidarity>()))
                .Verifiable();
            _moqNotificationAppService.Setup(n => n.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();

            await _sut.RejectCombinedInvitation(It.IsAny<int>());
            _moqOfferCommands.Verify(c => c.UpdateCombinedAsync(It.IsAny<OfferSolidarity>()));
        }

        [Fact]
        public async Task Should_GetSupplierAllFiles()
        {
            _moqOfferQueries.Setup(s => s.FindOfferByIdWithAttachementsModel(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<SupplierAttachmentPartialVModel>(new OfferDefaults().GetSupplierAttachmentPartialVModel()); });

            var result = await _sut.GetSupplierAllFiles(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_DeleteAttachementByIdAndsuplierCR()
        {
            _moqOfferQueries.Setup(s => s.FindSupplierAttachmentById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<SupplierAttachment>(new OfferDefaults().GetSupplierOriginalAttachment()); });
            _moqOfferQueries.Setup(s => s.FindOfferById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithQt()); });

            await _sut.DeleteAttachement(It.IsAny<int>(), "1299659801");
            _moqOfferCommands.Verify(c => c.UpdateAsync(It.IsAny<Offer>()), Times.Once);
            _moqOfferCommands.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_DeleteAttachementByReferenceNumber()
        {
            _moqOfferQueries.Setup(s => s.FindSupplierAttachmentById(It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<SupplierAttachment>(new OfferDefaults().GetSupplierOriginalAttachment()); });
            _moqOfferQueries.Setup(s => s.FindOfferById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithQt()); });

            await _sut.DeleteAttachement(It.IsAny<string>());
            _moqOfferCommands.Verify(c => c.UpdateAsync(It.IsAny<Offer>()), Times.Once);
            _moqOfferCommands.Verify(c => c.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_RemoveSolidaritySupplier()
        {
            MockUser();
            SolidarityRemoveInvitationModel solidarityRemoveInvitationModel = new SolidarityRemoveInvitationModel() { offerIdEncrypted = "" };
            Tender tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved);

            _moqOfferQueries.Setup(s => s.FindOfferById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Offer>(new OfferDefaults().GetOfferDefaultsWithQt()); });
            _moqTenderQueries.Setup(s => s.FindTenderInvitationsWithById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(tender); });
            _moqHttpContextAccessor.Setup(httpCon => httpCon.HttpContext.User.Claims).Returns(() =>
                new List<Claim>() { new Claim(IdentityConfigs.Claims.SelectedCR, "1299659801") });


            await _sut.RemoveSolidaritySupplier(solidarityRemoveInvitationModel);
            _moqOfferCommands.Verify(c => c.UpdateAsync(It.IsAny<Offer>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetOfferStatusForOfferSummary()
        {
            OfferSummaryStatusModel offerModel = new OfferSummaryStatusModel();

            _moqOfferQueries.Setup(s => s.FindOfferSummaryStatusModelById(It.IsAny<int>(), It.IsAny<string>()))
               .Returns(() => { return Task.FromResult<OfferSummaryStatusModel>(offerModel); });

            var result = await _sut.GetOfferStatusForOfferSummary(1, "1299659801");
            Assert.NotNull(result);
            Assert.IsType<OfferSummaryStatusModel>(result);
        }

        [Fact]
        public async Task Should_GetOfferStatusForOfferSummary_ReturnExceptionIfModelNull()
        {

            _moqOfferQueries.Setup(s => s.FindOfferSummaryStatusModelById(It.IsAny<int>(), It.IsAny<string>()))
               .Returns(() => { return Task.FromResult<OfferSummaryStatusModel>(null); });

           await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.GetOfferStatusForOfferSummary(1, "1299659801"));
        }

        [Fact]
        public async Task Should_GetOfferStatusForOfferSummary_ReturnExceptionIfOfferStatusRecieved()
        {

            OfferSummaryStatusModel offerModel = new OfferSummaryStatusModel()
            {
                StatusId = (int)Enums.OfferStatus.Received,
                CommercialNumber = "1299659801"
            }; 
            _moqOfferQueries.Setup(s => s.FindOfferSummaryStatusModelById(It.IsAny<int>(), It.IsAny<string>()))
               .Returns(() => { return Task.FromResult<OfferSummaryStatusModel>(offerModel); });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.GetOfferStatusForOfferSummary(1, "1299659801"));
        }
        [Fact]
        public async Task Should_GetOfferStatusForOfferSummary_ReturnExceptionIfOfferCancelled()
        {

            OfferSummaryStatusModel offerModel = new OfferSummaryStatusModel()
            {
                StatusId = (int)Enums.OfferStatus.Canceled
                
            }; 
            _moqOfferQueries.Setup(s => s.FindOfferSummaryStatusModelById(It.IsAny<int>(), It.IsAny<string>()))
               .Returns(() => { return Task.FromResult<OfferSummaryStatusModel>(offerModel); });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.GetOfferStatusForOfferSummary(1, "1299659801"));
        }



        [Fact]
        public async Task GetOfferLocalContentDetails_WithOfferid_ReturnValidObject()
        {
            #region Arrange
            _moqOfferQueries.Setup(offer => offer.GetOfferLocalContentDetails(It.IsAny<int>())).Returns(() => Task.FromResult(new OfferLocalContentDetailsModel() { IsIncludedInMoneyMarket = "نعم" }));
            #endregion

            var result = await _sut.GetOfferLocalContentDetails(It.IsAny<int>());
            
            Assert.NotNull(result);
        }
       [Fact]
        public async Task OfferCheckingDetailsForAwarding_WithOfferidAndTenderId_ReturnValidObject()
        {
            #region Arrange
            _moqOfferQueries.Setup(offer => offer.OfferCheckingDetailsForAwarding(It.IsAny<int>(),It.IsAny<int>()))
                .Returns(() => Task.FromResult(new OfferModel() { IsValidToApplyOfferLocalContentChanges = true }));

            _moqLocalContentConfigurationSettings.Setup(setting => setting.getLocalContentSettingsDate())
                .Returns(() => Task.FromResult(new ConfigurationSetting() { Id = 1, Date = new DateTime(2021, 4, 1), Description = "local content" }));
            #endregion

            var result = await _sut.OfferCheckingDetailsForAwarding(It.IsAny<int>(), It.IsAny<int>());
            
            Assert.NotNull(result);
        }



        [Fact]
        public async Task UpdateCorporationSize_WhenSM_ReturnYes()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqSMEASizeInqueryProxy.Setup(m => m.SMEASizeInquiry(It.IsAny<string>())).Returns(Task.FromResult(new ViewModel.LocalContent.LocalContentViewModel() { isSMEA = true }));
            #endregion
            
            var result = await _sut.UpdateCorporationSize(It.IsAny<int>());

            Assert.Equal("نعم",result);
        }

        [Fact]
        public async Task UpdateCorporationSize_WhenSM_SetSMCorporationWithTrue()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqSMEASizeInqueryProxy.Setup(m => m.SMEASizeInquiry(It.IsAny<string>())).Returns(Task.FromResult(new ViewModel.LocalContent.LocalContentViewModel() { isSMEA = true }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

             await _sut.UpdateCorporationSize(It.IsAny<int>());

            Assert.True(offer.OfferlocalContentDetails.IsSmallOrMediumCorporation);
        }

        [Fact]
        public async Task UpdateCorporationSize_WhenLarg_ReturnNo()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqSMEASizeInqueryProxy.Setup(m => m.SMEASizeInquiry(It.IsAny<string>())).Returns(Task.FromResult(new ViewModel.LocalContent.LocalContentViewModel() { isSMEA = false }));
            #endregion

            var result = await _sut.UpdateCorporationSize(It.IsAny<int>());

            Assert.Equal("لا", result);
        }

        [Fact]
        public async Task UpdateCorporationSize_WhenLarg_SetSMCorporationWithFalse()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqSMEASizeInqueryProxy.Setup(m => m.SMEASizeInquiry(It.IsAny<string>())).Returns(Task.FromResult(new ViewModel.LocalContent.LocalContentViewModel() { isSMEA = false }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            await _sut.UpdateCorporationSize(It.IsAny<int>());

            Assert.False(offer.OfferlocalContentDetails.IsSmallOrMediumCorporation);
        }


        [Fact]
        public async Task UpdateCorporationSize_WhenFailed_ThrowBusinessExcption()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqSMEASizeInqueryProxy.Setup(m => m.SMEASizeInquiry(It.IsAny<string>())).Returns(Task.FromResult(new ViewModel.LocalContent.LocalContentViewModel() { isSMEA = false, Status = "Error" }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            var result = await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.UpdateCorporationSize(It.IsAny<int>()));
            Assert.IsType<BusinessRuleException>(result);
        }

        [Theory]
        [InlineData(35)]
        public async Task UpdateLocalContentBaseLine_WhenSuccess_ReturnBaseLinePercentage(decimal baseLinePerce)
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.Tender = new TenderDefault().GetGeneralTender();
            offer.Tender.CreateTenderLocalContent();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqLocalContentProxy.Setup(m => m.GetBaselineScoreInquiry(It.IsAny<string>())).Returns(Task.FromResult(new LocalContentServiceResponse<decimal?>() { isSuccess = true, content = baseLinePerce }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            var result = await _sut.UpdateLocalContentBaseLine(It.IsAny<int>());

            Assert.Equal(baseLinePerce + " % ", result);
        }

        [Theory]
        [InlineData(35)]
        public async Task UpdateLocalContentBaseLine_WhenSuccess_SetOfferBaseLinePercentage(decimal baseLinePerce)
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.Tender = new TenderDefault().GetGeneralTender();
            offer.Tender.CreateTenderLocalContent();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqLocalContentProxy.Setup(m => m.GetBaselineScoreInquiry(It.IsAny<string>())).Returns(Task.FromResult(new LocalContentServiceResponse<decimal?>() {isSuccess = true , content = baseLinePerce }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

             await _sut.UpdateLocalContentBaseLine(It.IsAny<int>());

            Assert.Equal(baseLinePerce, offer.OfferlocalContentDetails.LocalContentPercentage);
        }

        [Fact]
        public async Task UpdateLocalContentBaseLine_WhenFailed_ThrowBusinessRuleException()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqLocalContentProxy.Setup(m => m.GetBaselineScoreInquiry(It.IsAny<string>())).Returns(Task.FromResult(new LocalContentServiceResponse<decimal?>() { isSuccess = false, content = null }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            var result = await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.UpdateLocalContentBaseLine(It.IsAny<int>()));

            Assert.IsType<BusinessRuleException>(result);
        }

        [Theory]
        [InlineData(35)]
        public async Task UpdateLocalContentDesiredPercentage_WhenSuccess_ReturnDesiredPercentage(decimal desiredPercent)
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            offer.Tender = new TenderDefault().GetGeneralTender();
            offer.Tender.CreateTenderLocalContent();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqLocalContentProxy.Setup(m => m.GetTargetPlanScoreInquiry(It.IsAny<string>(),It.IsAny<string>())).Returns(Task.FromResult(new LocalContentServiceResponse<decimal?>() { isSuccess = true, content = desiredPercent }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            var result = await _sut.UpdateLocalContentDesiredPercentage(It.IsAny<int>());

            Assert.Equal(desiredPercent + " % ", result);
        }

        [Theory]
        [InlineData(35)]
        public async Task UpdateLocalContentDesiredPercentage_WhenSuccess_SetOfferDesiredPercentage(decimal desiredPercent)
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            offer.Tender = new TenderDefault().GetGeneralTender();
            offer.Tender.CreateTenderLocalContent();

            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqLocalContentProxy.Setup(m => m.GetTargetPlanScoreInquiry(It.IsAny<string>(),It.IsAny<string>())).Returns(Task.FromResult(new LocalContentServiceResponse<decimal?>() { isSuccess = true, content = desiredPercent }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            await _sut.UpdateLocalContentDesiredPercentage(It.IsAny<int>());

            Assert.Equal(desiredPercent, offer.OfferlocalContentDetails.LocalContentDesiredPercentage);
        }

        [Fact]
        public async Task UpdateLocalContentDesiredPercentage_WhenFailed_ThrowBusinessRuleException()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            offer.Tender = new TenderDefault().GetGeneralTender();
            _moqOfferQueries.Setup(offer => offer.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqLocalContentProxy.Setup(m => m.GetTargetPlanScoreInquiry(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new LocalContentServiceResponse<decimal?>() { isSuccess = false, content = null }));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            var result = await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.UpdateLocalContentDesiredPercentage(It.IsAny<int>()));

            Assert.IsType<BusinessRuleException>(result);
        }

        [Fact]
        public async Task CalculateOfferFinancialPreferences_shouldReturnValue()
        {
            var offer1 = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer1.OfferlocalContentDetails = new OfferlocalContentDetails();
            offer1.OfferlocalContentDetails.SetCorporationSizeLarg();
            offer1.OfferlocalContentDetails.SetLocalContentBaseLinePercentage(20);
            offer1.OfferlocalContentDetails.SetLocalContentDesiredPercentage(10);
            offer1.SetFinalPrice(100);
            var offer2 = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer2.OfferlocalContentDetails = new OfferlocalContentDetails();
            offer2.OfferlocalContentDetails.SetCorporationSizeSmallOrMedium();
            offer2.OfferlocalContentDetails.SetLocalContentBaseLinePercentage(30);
            offer2.OfferlocalContentDetails.SetLocalContentDesiredPercentage(20);
            offer2.SetFinalPrice(100);

            _moqOfferQueries.Setup(offer => offer.GetIdenticalOffersWithLocalContentByTenderId(It.IsAny<int>())).Returns(Task.FromResult(new List<Offer>() { offer1, offer2 }));

            Tender tender = new TenderDefault().GetGeneralTender();
            TenderLocalContent tenderLocalContent = new TenderLocalContent();
            tenderLocalContent.UpdateTenderLocalContent(0, new List<int>() { 1, 2 }, false, 10, 10);
            tender.SetTenderLocalContent(tenderLocalContent);
            _moqTenderQueries.Setup(m => m.FindTenderWithLocalContentPreference(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(tender);
              });

            tender.CreateTenderQuantityTables(2, "t1", 15005);
            tender.CreateTenderQuantityTables(3, "t2", 15050);
            tender.TenderQuantityTables.First().Id = 2;
            tender.TenderQuantityTables.Last().Id = 3;
            var quantityTableItems = new List<TenderQuantityItemDTO>()
            {
                new TenderQuantityItemDTO()
                {
                    AlternativeValue = "0",
                    ColumnId = 1,
                    Id = 1,
                    ColumnName = "count",
                    IsDefault = true,
                    TableId= 2,
                    TableName = "t1",
                    IsNewTable = false,
                    IsTableDeleted = false,
                    ItemNumber = 1,
                    TemplateId = 3,
                    TenderFormHeaderId = 1,
                    TenderId = 1,
                    Value = "10"
                },
                new TenderQuantityItemDTO()
                {
                    AlternativeValue = "0",
                    ColumnId = 2,
                    Id = 1,
                    ColumnName = "price",
                    IsDefault = true,
                    TableId = 2,
                    TableName = "t1",
                    IsNewTable = false,
                    IsTableDeleted = false,
                    ItemNumber = 1,
                    TemplateId = 3,
                    TenderFormHeaderId = 1,
                    TenderId = 1,
                    Value = "100"
                },
                new TenderQuantityItemDTO()
                {
                    AlternativeValue = "0",
                    ColumnId = 3,
                    Id = 1,
                    ColumnName = "total",
                    IsDefault = true,
                    TableId = 2,
                    TableName = "t1",
                    IsNewTable = false,
                    IsTableDeleted = false,
                    ItemNumber = 1,
                    TemplateId = 3,
                    TenderFormHeaderId = 1,
                    TenderId = 1,
                    Value = ""
                }
            };

            var quantityTableItems2 = new List<TenderQuantityItemDTO>()
            {
                new TenderQuantityItemDTO()
                {
                    AlternativeValue = "0",
                    ColumnId = 4,
                    Id = 1,
                    ColumnName = "count",
                    IsDefault = true,
                    TableId= 3,
                    TableName = "t2",
                    IsNewTable = false,
                    IsTableDeleted = false,
                    ItemNumber = 1,
                    TemplateId = 22,
                    TenderFormHeaderId = 1,
                    TenderId = 1,
                    Value = "100"
                },
                new TenderQuantityItemDTO()
                {
                    AlternativeValue = "0",
                    ColumnId = 5,
                    Id = 1,
                    ColumnName = "price",
                    IsDefault = true,
                    TableId = 3,
                    TableName = "t2",
                    IsNewTable = false,
                    IsTableDeleted = false,
                    ItemNumber = 1,
                    TemplateId = 22,
                    TenderFormHeaderId = 1,
                    TenderId = 1,
                    Value = "50"
                },
                new TenderQuantityItemDTO()
                {
                    AlternativeValue = "0",
                    ColumnId = 6,
                    Id = 1,
                    ColumnName = "total",
                    IsDefault = true,
                    TableId = 3,
                    TableName = "t2",
                    IsNewTable = false,
                    IsTableDeleted = false,
                    ItemNumber = 1,
                    TemplateId = 22,
                    TenderFormHeaderId = 1,
                    TenderId = 1,
                    Value = ""
                }
            };

            tender.SaveTenderQuantityTableItems(quantityTableItems, "t1");
            tender.SaveTenderQuantityTableItems(quantityTableItems2, "t2");
            _moqTenderQueries.Setup(t => t.GetTenderWithQuantityTable(It.IsAny<int>())).Returns(() => { return Task.FromResult<Tender>(tender); });

            _moqQantityTemplatesProxy.Setup(t => t.GetCostColumnsIdByFormIdForNotSupply(It.IsAny<List<long>>()))
                .Returns(() =>
                {
                    return Task.FromResult(new List<ColumnDto>() {
                        new ColumnDto()
                        {
                            DataSource = "(##2##*##1##)",
                            Id = 3
                        }
                    });
                }
                );
            _moqOfferQueries.Setup(x => x.GetSupplierQTableItems(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult(quantityTableItems.Union(quantityTableItems2).ToList()); });
            await _sut.CalculateOfferFinancialPreferences(1);

            Assert.Equal(100, offer1.OfferlocalContentDetails.OfferPriceAfterSmallAndMediumCorporations);
        }



        [Fact]
        public async Task UpdateIsCorporatioIncludedInMoneyMarket_WithExistedInDB_ReturnTrue()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(o => o.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqOfferQueries.Setup(o => o.GetSupplierFromMonayMarketWithCr(It.IsAny<string>())).Returns(Task.FromResult(new MoneyMarketSuppliers()));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            var result = await _sut.UpdateIsCorporatioExistedInMoneyMarket(It.IsAny<int>());

            Assert.Equal("نعم",result);
        }
        [Fact]
        public async Task UpdateIsCorporatioIncludedInMoneyMarket_ThatNotExistedInDB_ReturnFalse()
        {
            #region Arrange
            var offer = new OfferDefaults().GetOfferDefaultsWithOfferId();
            offer.OfferlocalContentDetails = new OfferlocalContentDetails();
            _moqOfferQueries.Setup(o => o.GetOfferWithLocalContentDetailsById(It.IsAny<int>())).Returns(Task.FromResult(offer));
            _moqOfferCommands.Setup(comm => comm.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails)).Returns(Task.FromResult(offer.OfferlocalContentDetails));
            #endregion

            var result = await _sut.UpdateIsCorporatioExistedInMoneyMarket(It.IsAny<int>());

            Assert.Equal("لا", result);
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
