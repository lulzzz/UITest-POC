using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{

    public class QualificationApiControllertest : BaseTestApiController
    {
        #region props

        private Claim[] _claims;

        private readonly IQualificationAppService qualificationAppService;
        private readonly ISupplierQualificationDocumentAppService supplierqualificationService;
        private readonly AutoMapper.IMapper mapper;
        private readonly IVerificationService verificationService;
        private readonly IIDMAppService iDMAppService;
        private readonly IAuthorizationService authorizationService;
        private readonly ISupplierService supplierService;
        private readonly IOfferAppService offerAppService;
        private readonly ILookUpService lookupAppService;
        private readonly ITenderAppService tenderService;
        private readonly ISupplierQualificationDocumentDomainService supplierQualificationDocumentDomainService;
        private readonly IOptionsSnapshot<MOF.Etimad.Monafasat.SharedKernal.RootConfigurations> _mockRootConfiguration;
        private readonly IMemoryCache memoryCache;
        private readonly IBranchAppService branchAppService;


        private QualificationController _qualificationController;
        private LookupController _lookupController;

        #endregion

        public QualificationApiControllertest()
        {

            var serviceProvider = services.BuildServiceProvider();

            tenderService = serviceProvider.GetService<ITenderAppService>();
            qualificationAppService = serviceProvider.GetService<IQualificationAppService>();
            iDMAppService = serviceProvider.GetService<IIDMAppService>();
            lookupAppService = serviceProvider.GetService<ILookUpService>();
            supplierqualificationService = serviceProvider.GetService<ISupplierQualificationDocumentAppService>();
            verificationService = serviceProvider.GetService<IVerificationService>();

            //Configure mapping just for this test
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PreQualificationBasicDetailsModel, PreQualificationBasicDetailsModel>();
                cfg.CreateMap<MOF.Etimad.Monafasat.Core.Entities.Tender, PreQualificationSavingModel>();
                cfg.ValidateInlineMaps = false;
            });
            mapper = config.CreateMapper();

            _mockRootConfiguration = MockHelper.CreateIOptionSnapshotMock(new MOF.Etimad.Monafasat.SharedKernal.RootConfigurations());
            //verificationService = new Mock<IVerificationService>().Object;
            authorizationService = new Mock<IAuthorizationService>().Object;
            supplierService = new Mock<ISupplierService>().Object;
            offerAppService = new Mock<IOfferAppService>().Object;
            supplierQualificationDocumentDomainService = new Mock<ISupplierQualificationDocumentDomainService>().Object;
            memoryCache = new Mock<IMemoryCache>().Object;
            branchAppService = new Mock<IBranchAppService>().Object;

            _qualificationController = new QualificationController(supplierqualificationService, qualificationAppService, mapper, verificationService,
                iDMAppService, authorizationService, supplierService, offerAppService, lookupAppService, tenderService, supplierQualificationDocumentDomainService, _mockRootConfiguration);

            _lookupController = new LookupController(lookupAppService, mapper, iDMAppService, branchAppService, memoryCache, _mockRootConfiguration);
        }

        [Fact]
        public async Task OpenTenderDetailsReport()
        {
            int tenderId = 295;

            var result = await _qualificationController.OpenTenderDetailsReport(tenderId);

            Assert.NotNull(result);
            Assert.Equal(result.TenderId, tenderId);
            Assert.False(string.IsNullOrEmpty(result.ReferenceNumber));
        }

        [Fact]
        public async Task IndexPagingAsyncTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var requestModel = new PreQualificationSearchCriteriaModel() { };

            var response = await _qualificationController.GetPreQualificationsBySearchCriteria(requestModel);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
        }

        [Fact]
        public async Task GetStatusAsyncTest()
        {

            var response = await _qualificationController.GetStatus();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetAreasAsyncTest()
        {
            var response = await _lookupController.GetAreas();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetAllDataEntryUsersAsyncTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _lookupController = _lookupController.WithIdentity(_claims);
            List<int> ids = new List<int>() { 1, 2, 3 };

            var response = await _lookupController.GetUserBasedOnlistOfRoleIds(ids);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetAllAgenciesAsyncTest()
        {
            var response = await _lookupController.GetAllAgenceies();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetSupplierProjectbySearchCriteriaTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var requestModel = new PreQualificationSearchCriteriaModel() { TenderIdStr = "kNWBZYjHyEW2O9Jt5g3wjw==", SupplierCr = "5900037276" };

            var response = await _qualificationController.GetSupplierProjectbySearchCriteria(requestModel);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
        }

        [Fact]
        public async Task PreQualificationDetailsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 295;

            var result = await _qualificationController.GetPrequalificationDetailsData(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.tenderId, qualificationId);
        }

        [Fact]
        public async Task GetQualificationsListForVisitorTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.supplier) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var requestModel = new PreQualificationSearchCriteriaModel() { };

            var result = await _qualificationController.GetQualificationsListForVisitor(requestModel);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task GetPreQualificationForCheckingStageIndexTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "1") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var requestModel = new PreQualificationSearchCriteriaModel() { };

            var result = await _qualificationController.GetPreQualificationForCheckingStageIndex(requestModel);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task GetPreQualificationForUnderOperationsStageIndexTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "1") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var requestModel = new PreQualificationSearchCriteriaModel() { };

            var result = await _qualificationController.GetPreQualificationForUnderOperationsStageIndex(requestModel);

            Assert.NotNull(result);
        }

        [SkippableFact(typeof(AuthorizationException))]
        public async Task GetPreQualificationDetailsForCheckingTest()
        {
            int tenderid = 220;
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.CommitteeId, CommitteeType.TechincalCommittee.ToString()) };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await Assert.ThrowsAsync<AuthorizationException>(() => _qualificationController.GetPreQualificationDetailsForChecking(tenderid));
        }

        [Fact]
        public async Task GetPreQualificationsRequestsForCheckingAsyncTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            string tenderIdString = "kNWBZYjHyEW2O9Jt5g3wjw==";
            string supplierId = "2050038794";
            var requestModel = new QualificationIdWithSearchCriteria() { TenderIdString = tenderIdString };

            var result = await _qualificationController.GetPreQualificationsRequestsForChecking(requestModel);

            Assert.NotNull(result);
            Assert.Contains(result.Items, t => t.SupplierId == supplierId);
        }


        [Fact]
        public async Task AddQualificationToSupplierFavouriteListAsyncTest()
        {

            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int tenderid = 653;

            var result = await _qualificationController.AddQualificationToSupplierFavouriteList(tenderid);

            Assert.True(result);
        }

        [Fact]
        public async Task PreQualificationApprovalTest()
        {

            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int tenderid = 653;

            var result = await _qualificationController.GetPreQualificationForApproval(tenderid);

            Assert.NotNull(result);
            Assert.Equal(result.PreQualificationId, tenderid);
        }

        [Fact]
        public async Task GetsupplierPreQualificationDocumentByIdTest()
        {
            int SupplierPreQualificationDocumentId = 220;
            string SupplierId = "2050038794";

            var result = await _qualificationController.GetsupplierPreQualificationDocumentById(SupplierPreQualificationDocumentId, SupplierId);

            Assert.NotNull(result);
            Assert.Equal(result.PreQualificationId, SupplierPreQualificationDocumentId);
            Assert.Equal(result.SupplierId, SupplierId);
        }


        [Fact]
        public async Task GetPreQualificationDetailsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 295;
            string referenceNumber = "191139000437";

            var result = await _qualificationController.GetPreQualificationDetails(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.ReferenceNumber, referenceNumber);
        }

        [Fact]
        public async Task GetPreQualificationDetailsForSupplierTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 295;

            var result = await _qualificationController.GetPreQualificationDetailsForSupplier(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.Id, qualificationId);
        }

        [Fact]
        public async Task CheckSupplierDocumentsTest()
        {

            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager),
            new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154")};
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 477;

            var result = await _qualificationController.CheckSupplierDocuments(qualificationId, true);
            Assert.NotNull(result);
            Assert.NotEmpty(result.lstQualificationSupplierDataYearlyViewModel);
            Assert.NotEmpty(result.lstQualificationSupplierFinancialDataModel);
            Assert.NotEmpty(result.lstQualificationSupplierTechDataModel);
            Assert.Equal("Z4HRE9Pu*@@***@@**HHBr5sNz2C7A==", result.PreQualificationIdString);
            Assert.Equal("1010000154", result.SupplierId);
        }

        [Fact]
        public async Task QualificationOffersRegistryReportTest()
        {
            int qualificationId = 295;

            var result = await _qualificationController.QualificationOffersRegistryReport(qualificationId);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSupplierDocumentsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int preQualificationId = 220;

            var result = await _qualificationController.GetSupplierDocuments(preQualificationId);

            Assert.NotNull(result);
            Assert.Equal(preQualificationId, result.PreQualificationId);
            Assert.Equal("2050038794", result.SupplierId);
            Assert.Equal(88, result.SupplierPreQualificationDocumentId);
        }

        [Fact]
        public async Task GetSupplierDataReviewModelTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAccountManager) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 208;
            string SupplierId = "142154293000206";

            var result = await _qualificationController.GetSupplierDataReviewModel(qualificationId, SupplierId);

            Assert.NotNull(result);
            Assert.Equal(result.QualificationTypeId, (int)Enums.PreQualificationType.Small);
            Assert.Equal(result.SupplierCR, SupplierId);
        }

        [Fact]
        public async Task GetSupplierIDMInfoTest()
        {
            string SupplierId = "142154293000206";

            var result = await _qualificationController.GetSupplierIDMInfo(SupplierId);

            Assert.NotNull(result);
            Assert.Equal(result.supplierNumber, SupplierId);
            Assert.NotEmpty(result.attachments);
            Assert.Equal(result.SupplierType, (int)Enums.SupplierType.GovVendor);
        }

        [Fact]
        public async Task GetPrequalificationStatusTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 208;

            var result = await _qualificationController.GetPrequalificationStatus(qualificationId);

            Assert.Equal(result, (int)Enums.TenderStatus.DocumentChecking);
        }

        [Fact]
        public async Task GetSmallConfigQualificationDetailsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 457;
            decimal cashMax = 5;
            decimal cashMin = 2;
            decimal financialCapacity = 50;

            var result = await _qualificationController.GetSmallConfigQualificationDetails(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.CashRateMax, cashMax);
            Assert.Equal(result.CashRateMin, cashMin);
            Assert.Equal(result.FinancialCapacity, financialCapacity);

        }

        [Fact]
        public async Task GetLargeConfigQualificationDetailsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 457;
            decimal cashMax = 5;
            decimal cashMin = 2;
            decimal financialCapacity = 50;

            var result = await _qualificationController.GetLargeConfigQualificationDetails(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.CashRateMax, cashMax);
            Assert.Equal(result.CashRateMin, cashMin);
            Assert.Equal(result.FinancialCapacity, financialCapacity);

        }

        [Fact]
        public async Task GetSmallQualificationSupplierDetailsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 457;
            decimal cashMax = 5;
            decimal cashMin = 2;
            decimal financialCapacity = 50;

            var result = await _qualificationController.GetSmallQualificationSupplierDetails(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.CashRateMax, cashMax);
            Assert.Equal(result.CashRateMin, cashMin);
            Assert.Equal(result.FinancialCapacity, financialCapacity);

        }

        [Fact]
        public async Task GetMediumQualificationSupplierDetailsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 457;
            decimal cashMax = 5;
            decimal cashMin = 2;
            decimal financialCapacity = 50;

            var result = await _qualificationController.GetMediumQualificationSupplierDetails(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.CashRateMax, cashMax);
            Assert.Equal(result.CashRateMin, cashMin);
            Assert.Equal(result.FinancialCapacity, financialCapacity);

        }




        [Fact]
        public async Task GetMediumConfigQualificationDetailsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 457;
            decimal cashMax = 5;
            decimal cashMin = 2;
            decimal financialCapacity = 50;
            decimal technicalAdministrativeCapacity = 50;
            decimal tenderPointsToPass = 5;
            decimal totalEmployeeCountMax = 5;
            decimal totalEmployeeCountMin = 2;
            decimal totalEmployeeCountWeight = 50;
            decimal totalEmployeePercentageMax = 5;


            var result = await _qualificationController.GetMediumConfigQualificationDetails(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.CashRateMax, cashMax);
            Assert.Equal(result.CashRateMin, cashMin);
            Assert.Equal(result.FinancialCapacity, financialCapacity);
            Assert.Equal(result.TechnicalAdministrativeCapacity, technicalAdministrativeCapacity);
            Assert.Equal(result.TenderPointsToPass, tenderPointsToPass);
            Assert.Equal(result.TotalEmployeeCountMax, totalEmployeeCountMax);
            Assert.Equal(result.TotalEmployeeCountMin, totalEmployeeCountMin);
            Assert.Equal(result.TotalEmployeeCountWeight, totalEmployeeCountWeight);
            Assert.Equal(result.TotalEmployeePercentageMax, totalEmployeePercentageMax);

        }

        [Fact]
        public async Task GetLargeQualificationSupplierDetailsTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 457;
            decimal cashMax = 5;
            decimal cashMin = 2;
            decimal financialCapacity = 50;
            decimal technicalAdministrativeCapacity = 50;
            decimal tenderPointsToPass = 5;
            decimal totalEmployeeCountMax = 5;
            decimal totalEmployeeCountMin = 2;
            decimal totalEmployeeCountWeight = 50;
            decimal totalEmployeePercentageMax = 5;


            var result = await _qualificationController.GetLargeQualificationSupplierDetails(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(result.CashRateMax, cashMax);
            Assert.Equal(result.CashRateMin, cashMin);
            Assert.Equal(result.FinancialCapacity, financialCapacity);
            Assert.Equal(result.TechnicalAdministrativeCapacity, technicalAdministrativeCapacity);
            Assert.Equal(result.TenderPointsToPass, tenderPointsToPass);
            Assert.Equal(result.TotalEmployeeCountMax, totalEmployeeCountMax);
            Assert.Equal(result.TotalEmployeeCountMin, totalEmployeeCountMin);
            Assert.Equal(result.TotalEmployeeCountWeight, totalEmployeeCountWeight);
            Assert.Equal(result.TotalEmployeePercentageMax, totalEmployeePercentageMax);

        }

        [Fact]
        public async Task CreatePreQualifcationTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            PreQualificationSavingModel model = new PreQualificationSavingModel()
            {
                ActivityDescription = "وصف النشاط",
                AttachmentReference = ",idd_2CE19608-84B1-C336-8521-6875EB300000030257:EmployeeReport 1.pdf",
                Attachments = new List<TenderAttachmentModel>() {
                    new TenderAttachmentModel() {
                        AttachmentTypeId = 16,
                        FileNetReferenceId = "idd_2CE19608-84B1-C336-8521-6875EB300000030257",
                        Name = "EmployeeReport 1.pdf"
                    }
                },
                Details = "التفاصيل",
                IsAgancyHasTechnicalCommittee = true,
                LastEnqueriesDate = DateTime.Now.AddDays(10),
                OffersCheckingDate = DateTime.Now.AddDays(25),
                LastOfferPresentationDate = DateTime.Now.AddDays(10),
                LastOfferPresentationTime = DateTime.Now.TimeOfDay.ToString(),
                OffersCheckingTime = DateTime.Now.TimeOfDay.ToString(),
                PreQualificationCommitteeId = 7,
                TechnicalOrganizationId = 3,
                TenderActivitieIDs = new List<int>() { 201 },
                TenderAreaIDs = new List<int>() { 1 },
                TenderConstructionWorkIDs = new List<int>() { 17 },
                TenderMentainanceRunnigWorkIDs = new List<int>() { 7 },
                TenderName = "Test Qualification API" + new Random().Next(1, 5000).ToString()

            };


            var result = await _qualificationController.CreatePreQualifcation(model);

            Assert.NotNull(result);
            Assert.Equal("022001000000", result.AgencyCode);
            Assert.NotEmpty(result.Attachments);
            Assert.Equal("idd_2CE19608-84B1-C336-8521-6875EB300000030257", result.Attachments[0].FileNetReferenceId);
            Assert.Equal(7, result.PreQualificationCommitteeId);
            Assert.True(result.InsideKSA);
            Assert.Equal(3, result.TechnicalOrganizationId);
            Assert.Equal(2, result.TenderStatusId);
            Assert.Equal(3, result.TenderTypeId);
            Assert.Equal(1, result.branchId);
            Assert.Contains("Test Qualification API", result.TenderName);
        }

        [Fact]
        public async Task GetAttachmentsByPreQSupIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int Id = 147;

            var result = await _qualificationController.GetAttachmentsByPreQSupId(Id);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(Id, result[0].SupPreQAttachmentId);
            Assert.Equal(134, result[0].AttachmentId);
            Assert.Equal("idd_4E298D2A-2A89-C1A4-85B3-7115FB800000", result[0].FileNetReferenceId);
        }

        [Fact]
        public async Task FindCheckedPreQualificationsBySearchCriteriaTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "1") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var agencyCode = "022001000000";
            var requestModel = new PreQualificationSearchCriteriaModel() { AgencyCode = agencyCode };

            var result = await _qualificationController.FindCheckedPreQualificationsBySearchCriteria(requestModel);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.Equal(agencyCode, result.Items.First().AgencyCode);
            Assert.Equal(agencyCode, result.Items.Last().AgencyCode);
        }

        [Fact]
        public async Task GetSupplierQualificationsListTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010274503") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var requestModel = new PreQualificationSearchCriteriaModel() { };
            var agencyCode = "022001000000";

            var result = await _qualificationController.GetSupplierQualificationsList(requestModel);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.Equal(agencyCode, result.Items.First().AgencyCode);
            Assert.Equal(agencyCode, result.Items.Last().AgencyCode);
        }

        [Fact]
        public async Task GetQualifiacrionDatesByQualificationIdTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 457;
            var agencyCode = "022001000000";
            var referenceNumber = "191240000749";

            var result = await _qualificationController.GetQualifiacrionDatesByQualificationId(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(referenceNumber, result.ReferenceNumber);
            Assert.Equal(qualificationId, result.TenderId);
            Assert.Equal(agencyCode, result.AgencyCode);

        }

        //[Fact]
        //public async Task CreateVerificationCodeTest()
        //{
        //    _claims = new Claim[4] { new Claim(IdentityConfigs.Claims.NameIdentifier, "2050038794"),
        //    new Claim(IdentityConfigs.Claims.Mobile, "561982341") , new Claim(IdentityConfigs.Claims.Email, "afarouk.ps@gmail.com"),
        //     new Claim(IdentityConfigs.Claims.Role, RoleNames.Auditer )
        //    };
        //    _qualificationController = _qualificationController.WithIdentity(_claims);
        //    int qualificationId = 457;

        //    await _qualificationController.CreateVerificationCode(qualificationId);

        //}

        [Fact]
        public async Task GetStatusFblagTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.Role, RoleNames.ApprovePlaintDataPolicy) };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var response = await _qualificationController.GetStatusFlag();
            Assert.True(response);
        }

        [Fact]
        public async Task GetPreQualificationEditingDataTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            int qualificationId = 457;
            var agencyCode = "022001000000";
            var referenceNumber = "191240000749";

            var result = await _qualificationController.GetPreQualificationEditingData(qualificationId);

            Assert.NotNull(result);
            Assert.Equal(agencyCode, result.AgencyCode);
            Assert.NotEmpty(result.TenderAreaIDs);
            Assert.Equal(referenceNumber, result.ReferenceNumber);

        }

        [Fact]
        public async Task SaveDraftTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            PreQualificationSavingModel model = new PreQualificationSavingModel()
            {
                ActivityDescription = "وصف النشاط",
                AttachmentReference = ",idd_2CE19608-84B1-C336-8521-6875EB300000030257:EmployeeReport 1.pdf",
                Attachments = new List<TenderAttachmentModel>() {
                    new TenderAttachmentModel() {
                        AttachmentTypeId = 16,
                        FileNetReferenceId = "idd_2CE19608-84B1-C336-8521-6875EB300000030257",
                        Name = "EmployeeReport 1.pdf"
                    }
                },
                Details = "التفاصيل",
                IsAgancyHasTechnicalCommittee = true,
                LastEnqueriesDate = DateTime.Now.AddDays(10),
                OffersCheckingDate = DateTime.Now.AddDays(25),
                LastOfferPresentationDate = DateTime.Now.AddDays(10),
                LastOfferPresentationTime = DateTime.Now.TimeOfDay.ToString(),
                OffersCheckingTime = DateTime.Now.TimeOfDay.ToString(),
                PreQualificationCommitteeId = 7,
                TechnicalOrganizationId = 3,
                TenderActivitieIDs = new List<int>() { 201 },
                TenderAreaIDs = new List<int>() { 1 },
                TenderConstructionWorkIDs = new List<int>() { 17 },
                TenderMentainanceRunnigWorkIDs = new List<int>() { 7 },
                TenderName = "Test Qualification API" + new Random().Next(1, 5000).ToString()

            };


            var result = await _qualificationController.SaveDraft(model);

            Assert.NotNull(result);
            Assert.Equal("022001000000", result.AgencyCode);
            Assert.NotEmpty(result.Attachments);
            Assert.Equal("idd_2CE19608-84B1-C336-8521-6875EB300000030257", result.Attachments[0].FileNetReferenceId);
            Assert.Equal(7, result.PreQualificationCommitteeId);
            Assert.True(result.InsideKSA);
            Assert.Equal(3, result.TechnicalOrganizationId);
            Assert.Equal((int)TenderStatus.UnderEstablishing, result.TenderStatusId);
            Assert.Equal((int)TenderType.PreQualification, result.TenderTypeId);
            Assert.Equal(1, result.branchId);
            Assert.Contains("Test Qualification API", result.TenderName);
        }

        [Fact]
        public async Task GetPrequalificationTechnicalExaminationTest()
        {
            var PreQualificationId = 457;

            var result = await _qualificationController.GetPrequalificationTechnicalExamination(PreQualificationId);

            Assert.NotNull(result);
            Assert.Equal("191240000749", result.QualificationNumber);
            Assert.Equal(PreQualificationId, result.PrequalificationId);
            Assert.NotEmpty(result.SupplierList);
        }

        [Fact]
        public async Task getcountriesyncTest()
        {
            var result = await _qualificationController.getcountriesync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAcceptedPreQualificationDocumentsTest()
        {
            var PreQualificationId = 457;

            var result = await _qualificationController.GetAcceptedPreQualificationDocuments(PreQualificationId);

            Assert.NotNull(result);
            Assert.NotEmpty(result.SupplierNames);
        }

        [Fact]
        public async Task GetPostQualificationDataTest()
        {
            var PreQualificationId = 457;

            var result = await _qualificationController.GetPostQualificationData("2050038794", PreQualificationId);

            Assert.NotNull(result);
            Assert.Equal("022001000000", result.AgencyCode);
            Assert.Equal("191240000749", result.ReferenceNumber);
            Assert.NotEmpty(result.TenderAreaIDs);
            Assert.Equal(1, result.PreQualificationCommitteeId);
            Assert.True(result.InsideKSA);
        }

        [Fact]
        public async Task GetQualificationDataToEditPostQualificationTest()
        {
            var tenderId = Util.Encrypt(629);

            var result = await _qualificationController.GetQualificationDataToEditPostQualification(tenderId);

            Assert.NotNull(result);
            Assert.Equal("022001000000", result.AgencyCode);
            Assert.Equal("200640001130", result.ReferenceNumber);
            Assert.NotEmpty(result.TenderAreaIDs);
            Assert.NotEmpty(result.CommercialNumbers);
            Assert.Equal(1, result.PreQualificationCommitteeId);
            Assert.Equal(5, result.TechnicalOrganizationId);
            Assert.True(result.InsideKSA);
            Assert.True(result.HasOldPostQualification);

        }

        [Fact]
        public async Task GetPostQualificationForApprovalTest()
        {
            var qualificationId = 629;

            var result = await _qualificationController.GetPostQualificationForApproval(qualificationId);

            Assert.NotNull(result);
            Assert.Equal("pcl8hH88Cvm8Vt0ktvML7A==", result.PreQualificationIdString);
            Assert.Equal((int)PreQualificationType.Small, result.PreQualificationTypeId);
            Assert.Equal(qualificationId, result.PreQualificationId);

        }

        //[Fact]
        //public async Task GetPostQualificationSuppierDocumentTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
        //    _qualificationController = _qualificationController.WithIdentity(_claims);
        //    var postQualificationId = 403;

        //    var result = await _qualificationController.GetPostQualificationSuppierDocument(postQualificationId);

        //    Assert.NotNull(result);
        //    Assert.Equal("7XpkkAtnH6xeiKykuZcsDg==", result.PreQualificationIdString);

        //}

        [Fact]
        public async Task ReopenPostQualificationCheckingAsync()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "2050038794") };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            var result = await _qualificationController.ReopenPostQualificationChecking(203);

            Assert.NotNull(result);
            Assert.Equal("022001000000", result.AgencyCode);
            Assert.Equal("555", result.TenderNumber);
            Assert.Equal(5, result.TechnicalOrganizationId);
            Assert.Equal(1, result.BranchId);
            Assert.True(result.InsideKSA);

        }

        [Fact]
        public async Task SendPreQualificationToApproveTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            int id = Util.Decrypt("S5w4nQxXBKMJK*@@**UCTg3n0A==");
            await _qualificationController.SendPreQualificationToApprove(id);
        }

        [Fact]
        public async Task SendQualificationToCommitteeApproveTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await _qualificationController.SendQualificationToCommitteeApprove(700);
        }

        [Fact]
        public async Task ApprovePreQualificationFromCommitteeManagerTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await _qualificationController.ApprovePreQualificationFromCommitteeManager(701, "0");
        }

        [Fact]
        public async Task ApprvePreQualificationTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await _qualificationController.ApprvePreQualification(702, "0");
        }

        [Fact]
        public async Task RejectApprvePreQualificationTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            RejectionModel model = new RejectionModel() { TenderId = 703, RejectionReason = "reason" };

            await _qualificationController.RejectApprvePreQualification(model);
        }

        [Fact]
        public async Task RejectApprovePreQualificationFromCommitteeManagerTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            RejectionModel model = new RejectionModel() { TenderId = 704, RejectionReason = "reason" };

            await _qualificationController.RejectApprovePreQualificationFromCommitteeManager(model);
        }

        [Fact]
        public async Task ReopenPreQualificationTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await _qualificationController.ReopenPreQualification(705);
        }

        [Fact]
        public async Task ReopenPreQualificationFromCommitteeSecrtaryTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await _qualificationController.ReopenPreQualificationFromCommitteeSecrtary(706);
        }

        [Fact]
        public async Task EditPreQualificationCommittesTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            EditeCommitteesModel model = new EditeCommitteesModel() { TenderId = 707, TechnicalOrganizationId = 1, PreQualificationCommitteeId = 1 };

            var result = await _qualificationController.EditPreQualificationCommittes(model);
            Assert.NotNull(result);
            Assert.Equal("201040001213", result.ReferenceNumber);
            Assert.Equal((int)TenderType.PreQualification, result.TenderTypeId);
            Assert.Equal(707, result.TenderId);
            Assert.Equal(1, result.BranchId);

        }

        [Fact]
        public async Task ChooseQualificationResultTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            ChooseQualificationResultModel model = new ChooseQualificationResultModel() { QualificationId = 629, FailingReason = "reason", FinalResultStatusId = (int)Enums.QualificationResultLookup.Failed, SupplierId = "1010000154" };

            await _qualificationController.ChooseQualificationResult(model);
        }

        [Fact]
        public async Task getQualificationDataToCreatePostQualificationTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);
            string id = Util.Encrypt(670);

            var result = await _qualificationController.getQualificationDataToCreatePostQualification("1010171916", id, "");
            Assert.NotNull(result);
            Assert.Equal("022001000000", result.AgencyCode);
            Assert.True(result.InsideKSA);
            Assert.Equal(7, result.PreQualificationCommitteeId);
            Assert.Equal("201040001176", result.ReferenceNumber);
            Assert.NotEmpty(result.TenderAreaIDs);
            Assert.NotEmpty(result.TenderActivitieIDs);
            Assert.NotEmpty(result.TenderMentainanceRunnigWorkIDs);
        }

        [Fact]
        public async Task SendPostQualificationToApproveTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await _qualificationController.SendPostQualificationToApprove(670);
        }

        [Fact]
        public async Task ApprvePostQualificationTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await _qualificationController.ApprvePostQualification(671, "0");
        }

        [Fact]
        public async Task RejectApprvePostQualificationTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            RejectionModel model = new RejectionModel() { TenderId = 672, RejectionReason = "reason" };

            await _qualificationController.RejectApprvePostQualification(model);
        }

        [Fact]
        public async Task ReopenPostQualificationTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            await _qualificationController.ReopenPostQualification(673);
        }

        //[Fact]
        //public async Task ApplyPostQualificationDocsforSupplierTest()
        //{
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154") };
        //    _qualificationController = _qualificationController.WithIdentity(_claims);

        //    PreQualificationApplyDocumentsModel model = new PreQualificationApplyDocumentsModel()
        //    {
        //        PreQualificationIdString = Util.Encrypt(677),
        //        QualificationTypeId = 3,
        //        TenderId = 674,
        //        RejectionReason = "reason",
        //        preQualificationSupplierAttachmentModels = new List<PreQualificationSupplierAttachmentModel>()
        //        {
        //            new PreQualificationSupplierAttachmentModel()
        //            {
        //                SupPreQAttachmentId = 147,
        //                AttachmentId = 134,
        //                FileNetReferenceId = "idd_4E298D2A-2A89-C1A4-85B3-7115FB800000",
        //                FileName = "1680174_Information Security Awareness - Refresher Course_Completion_Certificate.pdf"
        //            }
        //        }

        //    };

        //    var result = await _qualificationController.ApplyPostQualificationDocsforSupplier(model);
        //    Assert.NotNull(result);
        //}

        [Fact]
        public async Task ReopenPostQualificationCheckingTest()
        {
            _claims = new Claim[2] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "1")
            };
            _qualificationController = _qualificationController.WithIdentity(_claims);

            var result = await _qualificationController.ReopenPostQualificationChecking(687);

            Assert.NotNull(result);
            Assert.Equal("022001000000", result.AgencyCode);
            Assert.Equal(1, result.BranchId);
            Assert.Equal(3, result.TechnicalOrganizationId);
        }

    }
}
