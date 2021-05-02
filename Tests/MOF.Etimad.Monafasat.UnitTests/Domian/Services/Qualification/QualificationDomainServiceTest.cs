using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Qualification
{
    public class QualificationDomainServiceTest
    {
        private readonly Mock<IAppDbContext> _moqContext;
        private readonly Mock<IQualificationQueries> _moqQualificationQueries;
        private readonly Mock<ICommunicationQueries> _moqCommunicationQueries;
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;
        private readonly Mock<IIDMQueries> _moqIDMQueries;
        private QualificationDomainService _Sut;

        public QualificationDomainServiceTest()
        {
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _moqCommunicationQueries = new Mock<ICommunicationQueries>();
            _moqContext = new Mock<IAppDbContext>();
            _moqIDMQueries = new Mock<IIDMQueries>();
            _moqQualificationQueries = new Mock<IQualificationQueries>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfigurationMock.Setup(x => x.Value)
                .Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForQualification());

            _Sut = new QualificationDomainService(_moqContext.Object, _moqQualificationQueries.Object,
                _moqHttpContextAccessor.Object, _moqCommunicationQueries.Object, _rootConfigurationMock.Object,
                _moqIDMQueries.Object);
        }

        [Fact]
        public async Task IsValidToCreate_WithOutTechnicalCommitee_ThrowsBusinessException()
        {
            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderAreaIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeManager)});

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () => await _Sut.IsValidToCreate(model));

            Assert.Equal(Resources.TenderResources.ErrorMessages.EnterTechnicalCommittee, e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreate_WithNullDates_ThrowsBusinessException()
        {
            #region Arrange

            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderCountriesIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true,
                PreQualificationCommitteeId = 0
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeSecretary)});

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () => await _Sut.IsValidToCreate(model));

            Assert.Equal(Resources.TenderResources.ErrorMessages.EnterAllQualificationDates, e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreate_WithEnquiryDateLessThanToday_ThrowsBusinessException()
        {
            #region Arrange

            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderAreaIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true,
                PreQualificationCommitteeId = 0,
                LastEnqueriesDate = DateTime.Now.AddDays(-2),
                LastOfferPresentationDate = DateTime.Now,
                LastOfferPresentationTime = "5:00",
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeSecretary)});

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () => await _Sut.IsValidToCreate(model));

            Assert.Equal(Resources.SharedResources.ErrorMessages.DateLessThanToday + "\n", e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreate_WithCheckingDateLessThanEnquiryDate_ThrowsBusinessException()
        {
            #region Arrange

            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderAreaIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true,
                PreQualificationCommitteeId = 0,
                LastEnqueriesDate = DateTime.Now.AddDays(2),
                LastOfferPresentationDate = DateTime.Now.AddDays(3),
                LastOfferPresentationTime = "5:00",
                OffersCheckingDate = DateTime.Now.AddDays(1)
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeSecretary)});

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () => await _Sut.IsValidToCreate(model));

            Assert.Equal(Resources.TenderResources.ErrorMessages.CheckingDateLessThanEnquiriesDate + "\n",
                e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreate_WithLastPresentationDateLessThanEnquiryDate_ThrowsBusinessException()
        {
            #region Arrange

            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderAreaIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true,
                PreQualificationCommitteeId = 0,
                LastEnqueriesDate = DateTime.Now.AddDays(2),
                LastOfferPresentationDate = DateTime.Now,
                LastOfferPresentationTime = "5:00",
                OffersCheckingDate = DateTime.Now.AddDays(4)
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeSecretary)});

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () => await _Sut.IsValidToCreate(model));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.InvalidLastEnqueriesDate, e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreatePostQualification_WithNullQualificationModel_ThrowsBusinessException()
        {

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToCreatePostQualification(null));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationNotExit, e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreatePostQualification_WithOutTechnicalCommitee_ThrowsBusinessException()
        {
            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderCountriesIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeManager)});

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToCreatePostQualification(model));

            Assert.Equal(Resources.TenderResources.ErrorMessages.EnterTechnicalCommittee, e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreatePostQualification_WithNullDates_ThrowsBusinessException()
        {
            #region Arrange

            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderAreaIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true,
                PreQualificationCommitteeId = 0
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeSecretary)});

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToCreatePostQualification(model));

            Assert.Equal(Resources.TenderResources.ErrorMessages.EnterAllQualificationDates, e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreatePostQualification_WithEnquiryDateLessThanToday_ThrowsBusinessException()
        {
            #region Arrange

            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderAreaIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true,
                PreQualificationCommitteeId = 0,
                LastEnqueriesDate = DateTime.Now.AddDays(-2),
                LastOfferPresentationDate = DateTime.Now,
                LastOfferPresentationTime = "5:00",
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeSecretary)});

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToCreatePostQualification(model));

            Assert.Equal(Resources.SharedResources.ErrorMessages.DateLessThanToday + "\n", e.Result.Message);
        }

        [Fact]
        public async Task IsValidToCreatePostQualification_WithCheckingDateLessThanEnquiryDate_ThrowsBusinessException()
        {
            #region Arrange

            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderAreaIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true,
                PreQualificationCommitteeId = 0,
                LastEnqueriesDate = DateTime.Now.AddDays(2),
                LastOfferPresentationDate = DateTime.Now,
                LastOfferPresentationTime = "5:00",
                OffersCheckingDate = DateTime.Now.AddDays(1)
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeSecretary)});

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToCreatePostQualification(model));

            Assert.Equal(Resources.TenderResources.ErrorMessages.CheckingDateLessThanEnquiriesDate + "\n",
                e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToCreatePostQualification_WithCheckingDateLessThanLastPresentationDate_ThrowsBusinessException()
        {
            #region Arrange

            PostQualificationApplyDocumentsModel model = new PostQualificationApplyDocumentsModel()
            {
                TenderStatusId = 0,
                TenderAreaIDs = new List<int>() {1},
                TechnicalOrganizationId = null,
                IsAgancyHasTechnicalCommittee = true,
                PreQualificationCommitteeId = 0,
                LastEnqueriesDate = DateTime.Now.AddDays(1),
                LastOfferPresentationDate = DateTime.Now.AddDays(3),
                LastOfferPresentationTime = "5:00",
                OffersCheckingDate = DateTime.Now.AddDays(2)
            };
            _moqHttpContextAccessor.Setup(con => con.HttpContext.User.Claims).Returns(() => new List<Claim>()
                {new Claim(IdentityConfigs.Claims.Role, RoleNames.PreQualificationCommitteeSecretary)});

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToCreatePostQualification(model));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.InvalidOffersCheckingDate, e.Result.Message);
        }

        [Fact]
        public async Task CheckMasterWeightForQualification_WithZeroTechnicalCapacity_ThrowsBusinessException()
        {
            PreQualificationSavingModel preQualificationSavingModel = new PreQualificationSavingModel()
                {TechnicalAdministrativeCapacity = 0};

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.CheckMasterWeightForQualification(preQualificationSavingModel));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.pleaseCheckItemWeight, e.Result.Message);
        }

        [Fact]
        public async Task CheckMasterWeightForQualification_WithZeroFinancialCapacity_ThrowsBusinessException()
        {
            PreQualificationSavingModel preQualificationSavingModel =
                new PreQualificationSavingModel() {FinancialCapacity = 0};

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.CheckMasterWeightForQualification(preQualificationSavingModel));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.pleaseCheckItemWeight, e.Result.Message);
        }

        [Fact]
        public async Task IsValidToSendQualificationForApprovement_WithAnyDateLessThanToday_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderDates(DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now, DateTime.Now, null, null,
                null, null,
                null, null, null,
                null, null);
            tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", 1221, Enums.TenderActions.ExtenedTenderDates);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToSendQualificationForApprovement(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationDateMustBelargeThanToday,
                e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToSendQualificationForApprovement_WithPresentationDateLessThanEnquiryDate_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderDates(DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), DateTime.Now,
                DateTime.Now.AddDays(3), null, null, null, null,
                null, null, null,
                null, null);
            tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", 1221, Enums.TenderActions.ExtenedTenderDates);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToSendQualificationForApprovement(tender));

            Assert.Equal("اخر موعد تقديم فحص التاهيل يجب أن يكون أكبر من  اخر موعد لاستلام استفسارات",
                e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToSendQualificationForApprovement_WithCheckingDateLessThanPresentationDate_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderDates(DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), DateTime.Now,
                DateTime.Now.AddDays(2), null, null, null, null,
                null, null, null,
                null, null);
            tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", 1221, Enums.TenderActions.ExtenedTenderDates);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToSendQualificationForApprovement(tender));

            Assert.Equal("تاريخ فحس التاهيل يجب أن يكون أكبر من اخر موعد لاستلام العروض", e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToSendQualificationForApprovementFromCommitteeManager_WithZeroCapcity_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.SetPointToPassToQualification(7, 0, 0, 1);
            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToSendQualificationForApprovementFromCommitteeManager(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.pleaseCheckItemWeight, e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToSendQualificationForApprovementFromCommitteeManager_WithWrongStatus_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.SetPointToPassToQualification(7, 40, 30, 1);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToSendQualificationForApprovementFromCommitteeManager(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.NotAllaowedToSendTenderForApprovement,
                e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToSendQualificationForApprovementFromCommitteeManager_WithAnyDateLessThanToday_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.SetPointToPassToQualification(7, 40, 30, 1);

            tender.UpdateTenderDates(DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now, DateTime.Now, null, null,
                null, null,
                null, null, null,
                null, null);
            tender.UpdateTenderStatus(Enums.TenderStatus.QualificationUnderEstablishingFromCommittee, "", 1221,
                Enums.TenderActions.ExtenedTenderDates);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToSendQualificationForApprovementFromCommitteeManager(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationDateMustBelargeThanToday,
                e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToSendQualificationForApprovementFromCommitteeManager_WithPresentationDateLessThanEnquiryDate_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.SetPointToPassToQualification(7, 40, 30, 1);

            tender.UpdateTenderDates(DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), DateTime.Now,
                DateTime.Now.AddDays(3), null, null, null, null,
                null, null, null,
                null, null);
            tender.UpdateTenderStatus(Enums.TenderStatus.QualificationUnderEstablishingFromCommittee, "", 1221,
                Enums.TenderActions.ExtenedTenderDates);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToSendQualificationForApprovementFromCommitteeManager(tender));

            Assert.Equal("اخر موعد تقديم فحص التاهيل يجب أن يكون أكبر من  اخر موعد لاستلام استفسارات",
                e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToSendQualificationForApprovementFromCommitteeManager_WithCheckingDateLessThanPresentationDate_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.SetPointToPassToQualification(7, 40, 30, 1);

            tender.UpdateTenderDates(DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), DateTime.Now,
                DateTime.Now.AddDays(2), null, null, null, null,
                null, null, null,
                null, null);
            tender.UpdateTenderStatus(Enums.TenderStatus.QualificationUnderEstablishingFromCommittee, "", 1221,
                Enums.TenderActions.ExtenedTenderDates);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToSendQualificationForApprovementFromCommitteeManager(tender));

            Assert.Equal("تاريخ فحس التاهيل يجب أن يكون أكبر من اخر موعد لاستلام العروض", e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToAcceptQualificationFromCommitteSecrtary_WithNoQualificationCommittee_ThrowsBusinessException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToAcceptQualificationFromCommitteSecrtary(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification,
                e.Result.Message);
        }

        [Fact]
        public async Task
            IsValidToAcceptQualificationFromCommitteSecrtary_WithNotAssinedQualificationCommittee_ThrowsBusinessException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdatePreQualificationCommittees(4, 1, 1, 1);
            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToAcceptQualificationFromCommitteSecrtary(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification,
                e.Result.Message);
        }

        [Theory]
        [InlineData(1)]
        public async Task
            CanAddPostqualificationIfTenderHasExtendOfferValidity_WithExsitedQualification_ThrowsBusinessException(
                int tenderId)
        {
            _moqCommunicationQueries
                .Setup(comm => comm.CanAddPostqualificationIfTenderHasExtendOfferValidity(It.IsAny<int>()))
                .ReturnsAsync(() => true);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.CanAddPostqualificationIfTenderHasExtendOfferValidity(tenderId));

            Assert.Equal("لايمكن اضافة تاهيل لاحق لأن هذه المنافسة عليها طلب تمديد سريان عروض سارى", e.Result.Message);
        }

        [Fact]
        public async Task IsValidToAcceptQualificationFromCommitteSecrtar_WithWrongStatus_ThrowsBusinessException()
        {
            #region Arrange

            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdatePreQualificationCommittees(4, 1, 1, 1);
            _moqHttpContextAccessor.Setup(context => context.HttpContext.User.Claims).Returns(() =>
                new List<Claim>()
                {
                    new Claim(IdentityConfigs.Claims.CommitteeId, "1"),
                    new Claim(IdentityConfigs.Claims.SelectedGovAgency, "AgencyName,022001000000")
                });

            #endregion

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToAcceptQualificationFromCommitteSecrtary(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification,
                e.Result.Message);
        }

        [Fact]
        public async Task IsValidToApproveQualification_WithWrongStatus_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToApproveQualification(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification,
                e.Result.Message);
        }

        [Fact]
        public async Task IsValidToApproveQualification_WithPassedDate_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderDates(DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now, DateTime.Now, null, null,
                null, null,
                null, null, null,
                null, null);
            tender.UpdateTenderStatus(Enums.TenderStatus.Pending, "", 1221, Enums.TenderActions.ExtenedTenderDates);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToApproveQualification(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.IsValidQualificationDateForApprovement,
                e.Result.Message);
        }

        [Theory]
        [InlineData("02200132000000")]
        public async Task
            IsValidToApprovePreQualificationFromCommitteeManager_WithWrongAgency_ThrowsAuthorizationException(
                string agencyCode)
        {
            var tender = new TenderDefault().GetGeneralTender();
            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToApprovePreQualificationFromCommitteeManager(tender, agencyCode));

        }

        [Theory]
        [InlineData("022001000000")]
        public async Task IsValidToApprovePreQualificationFromCommitteeManager_WithWrongStatus_ThrowsBusinessException(
            string agencyCode)
        {
            var tender = new TenderDefault().GetGeneralTender();

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToApprovePreQualificationFromCommitteeManager(tender, agencyCode));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.NotAllaowedToApproveQyalification,
                e.Result.Message);
        }

        [Theory]
        [InlineData("022001000000")]
        public async Task IsValidToApprovePreQualificationFromCommitteeManager_WithPassedDate_ThrowsBusinessException(
            string agencyCode)
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderDates(DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now, DateTime.Now, null, null,
                null, null,
                null, null, null,
                null, null);
            tender.UpdateTenderStatus(Enums.TenderStatus.PendingQualificationCommitteeManagerApproval, "", 1221,
                Enums.TenderActions.ExtenedTenderDates);

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToApprovePreQualificationFromCommitteeManager(tender, agencyCode));

            Assert.Equal(Resources.QualificationResources.Messages.CantApprovePresentaionDatePassed, e.Result.Message);
        }

        [Fact]
        public async Task IsValidToRejectQualification_WithWrongStatus_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToRejectQualification(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.NotAllaowedToRejectApproveQualification,
                e.Result.Message);
        }

        [Fact]
        public async Task IsValidToReopenQualification_WithWrongStatus_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();

            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.IsValidToReopenQualification(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.NotAllaowedToReopenQualification,
                e.Result.Message);
        }

        [Fact]
        public void IsValidToStartCheckingTender_WithNullObject_ThrowsBusinessException()
        {
            MockUser();

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToStartCheckingTender(null));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationNotExist, e.Message);
        }

        [Fact]
        public void IsValidToStartCheckingTender_WithNotAssignedcommittee_ThrowsBusinessException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();

            var e = Assert.Throws<AuthorizationException>(() =>
                _Sut.IsValidToStartCheckingTender(tender));
        }

        [Fact]
        public void IsValidToStartCheckingTender_WithWrongStatus_ThrowsBusinessException()
        {
            #region Arrange

            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdatePreQualificationCommittees(4, 1, 1, 1);
            var expectedMessage = Resources.QualificationResources.ErrorMessages.QualificationCheckingStatus +
                                  Resources.TenderResources.Messages.ResourceManager.GetString(
                                      nameof(Enums.TenderStatus.Approved));

            _moqHttpContextAccessor.Setup(context => context.HttpContext.User.Claims).Returns(() =>
                new List<Claim>() {new Claim(IdentityConfigs.Claims.CommitteeId, "1")});

            #endregion

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToStartCheckingTender(tender));

            Assert.Equal(expectedMessage, e.Message);
        }

        [Fact]
        public void IsValidToAccecssCheckingTender_WithNullObject_ThrowsBusinessException()
        {
            MockUser();

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToAccecssCheckingTender(null));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationNotExist, e.Message);
        }

        [Fact]
        public void IsValidToAccecssCheckingTender_WithNotAssignedcommittee_ThrowsAuthorizationException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();

            Assert.Throws<AuthorizationException>(() =>
                _Sut.IsValidToAccecssCheckingTender(tender));
        }

        [Fact]
        public void IsValidToAccecssPostQualificationCheckingTender_WithNullObject_ThrowsBusinessException()
        {

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToAccecssPostQualificationCheckingTender(null, new List<string>()));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationNotExist, e.Message);
        }

        [Fact]
        public void IsValidToAccecssPostQualificationCheckingTender_WithNotAssignedCommittee_AuthorizationException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.SetTenderType((int) Enums.TenderType.PostQualification);
            tender.UpdateCommittees(1, null, 1, 1, 1, 1, null, 1);

            Assert.Throws<AuthorizationException>(() =>
                _Sut.IsValidToAccecssPostQualificationCheckingTender(tender,
                    new List<string>() {RoleNames.OffersCheckManager, RoleNames.OffersPurchaseManager}));

        }

        [Fact]
        public void IsValidToReopenCheckTender_WithNullObject_ThrowsBusinessException()
        {

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToReopenCheckTender(null));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationNotExist, e.Message);
        }

        [Fact]
        public void IsValidToReopenCheckTender_WithNotAssignedCommittee_AuthorizationException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateCommittees(1, null, 1, 1, 1, 1, null, 1);

            Assert.Throws<AuthorizationException>(() =>
                _Sut.IsValidToReopenCheckTender(tender));

        }

        [Fact]
        public void IsValidToSendTenderToApproveChecking_WithNullObject_ThrowsBusinessException()
        {

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToSendTenderToApproveChecking(null, new List<string>()));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationNotExist, e.Message);
        }

        [Fact]
        public void IsValidToSendTenderToApproveChecking_WithNotAssignedCommittee_AuthorizationException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateCommittees(1, null, 1, 1, 1, 1, null, 1);

            Assert.Throws<AuthorizationException>(() =>
                _Sut.IsValidToSendTenderToApproveChecking(tender, new List<string>()));

        }

        [Fact]
        public void IsValidToApproveTenderChecking_WithNullObject_ThrowsBusinessException()
        {

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToApproveTenderChecking(null, new List<string>()));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationNotExist, e.Message);
        }

        [Fact]
        public void IsValidToApproveTenderChecking_WithNotAssignedCommittee_AuthorizationException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateCommittees(1, null, 1, 1, 1, 1, null, 1);

            Assert.Throws<AuthorizationException>(() =>
                _Sut.IsValidToApproveTenderChecking(tender, new List<string>()));

        }

        [Fact]
        public void IsValidToRejectCheckTender_WithNullObject_ThrowsBusinessException()
        {

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToRejectCheckTender(null, new List<string>()));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.QualificationNotExist, e.Message);
        }

        [Fact]
        public void IsValidToRejectCheckTender_WithNotAssignedCommittee_ThrowsAuthorizationException()
        {
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateCommittees(1, null, 1, 1, 1, 1, null, 1);

            Assert.Throws<AuthorizationException>(() =>
                _Sut.IsValidToRejectCheckTender(tender, new List<string>()));

        }

        [Fact]
        public void IsValidToRejectCheckTender_WithWrongStatus_ThrowsBusinessException()
        {
            #region Arrange

            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdatePreQualificationCommittees(4, 1, 1, 1);
            var expectedMessage = Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + " " +
                                  GetStatusName(Enums.TenderStatus.DocumentCheckPending);
            _moqHttpContextAccessor.Setup(x => x.HttpContext.User.Claims).Returns(() =>
            {
                return new Claim[1] {new Claim(IdentityConfigs.Claims.CommitteeId, "1")};
            });

            #endregion

            var e = Assert.Throws<BusinessRuleException>(() =>
                _Sut.IsValidToRejectCheckTender(tender, new List<string>()));

            Assert.Equal(expectedMessage, e.Message);
        }

        [Fact]
        public async Task CheckQualificationDateValidation_WithPassedDate_ThrowsBusinessException()
        {
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderDates(DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now, DateTime.Now.AddDays(5),
                null, null, null, null,
                null, null, null,
                null, null);
            var e = Assert.ThrowsAsync<BusinessRuleException>(async () =>
                await _Sut.CheckQualificationDateValidation(tender));

            Assert.Equal(Resources.QualificationResources.ErrorMessages.AfterDate, e.Result.Message);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetDirectPurchaseMemberProfile_WithUserId_ReturnUserObject(int userId)
        {
            _moqIDMQueries.Setup(idmQueries => idmQueries.FindUserProfileByIdWithoutIncludesAsync(It.IsAny<int>()))
                .Returns(() => Task.FromResult(new UserProfile()));

            var result = await _Sut.GetDirectPurchaseMemberProfile(userId);

            Assert.IsType<UserProfile>(result);
        }



        private string GetStatusName(Enums.TenderStatus tenderStatus)
        {
            var statusName = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(tenderStatus));
            return statusName;
        }

        private void MockUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() => { return idintity; });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        }



        [Fact]
        public void TestUserClaims()
        {
            _moqHttpContextAccessor.Setup(x => x.HttpContext.User.Claims).Returns(() =>
            {
                return new Claim[1] {new Claim(IdentityConfigs.Claims.CommitteeId, "1")};
            });
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateCommittees(1, null, 1, 1, 1, 1, null, 1);

            Assert.Throws<AuthorizationException>(() =>
                _Sut.IsValidToRejectCheckTender(tender, new List<string>()));

        }
    }
}
