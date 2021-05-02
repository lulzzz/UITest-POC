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
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class TenderApiControllerTest : BaseTestApiController
    {
        #region props
        private Claim[] _claims;
        private readonly ITenderAppService tenderService;
        private readonly AutoMapper.IMapper mapper;
        private readonly IBillAppService billAppService;

        private readonly IVerificationService verificationService;
        private readonly IIDMAppService iDMAppService;
        private readonly IAuthorizationService authorizationService;
        private readonly ISupplierService supplierService;
        private readonly IOfferAppService offerAppService;
        private readonly ILookUpService lookupAppService;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        private readonly INotificationAppService notificationAppService;
        private readonly IBlockAppService blockAppService;
        private readonly IMemoryCache memoryCache;
        private readonly IBranchAppService branchAppService;
        private readonly IAgencyBudgetService agencyBudgetService;
        private readonly IWathiqService wathiqService;
        private TenderController _tenderController;
        #endregion

        public TenderApiControllerTest()
        {

            var serviceProvider = services.BuildServiceProvider();
            tenderService = serviceProvider.GetService<ITenderAppService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VROTendersCreatedByGovAgencyModel, VROTendersCreatedByGovAgencyModel>();
                cfg.CreateMap<Core.Entities.Tender, TenderOffersStepModel>();
                cfg.CreateMap<Core.Entities.TenderAttachment, TenderOldAttachmentsModel>();
                cfg.CreateMap<Core.Entities.TenderAttachment, TenderAttachmentModel>();
                cfg.CreateMap<Core.Entities.TenderHistory, TenderHistoryModel>();
                cfg.CreateMap<Core.Entities.Tender, CreateTenderBasicDataModel>();
                cfg.CreateMap<Core.Entities.Tender, TenderInformationModel>();
                cfg.CreateMap<Core.Entities.Tender, TenderSamplesAddressModel>();
                cfg.CreateMap<Core.Entities.Tender, TenderAreasModel>();
                cfg.CreateMap<Core.Entities.TenderChangeRequest, TenderChangeRequestModel>();
                cfg.CreateMap<Core.Entities.Tender, EditeCommitteesModel>();
            });
            mapper = config.CreateMapper();
            billAppService = serviceProvider.GetService<IBillAppService>();
            iDMAppService = serviceProvider.GetService<IIDMAppService>();
            authorizationService = serviceProvider.GetService<IAuthorizationService>();
            supplierService = serviceProvider.GetService<ISupplierService>();
            offerAppService = serviceProvider.GetService<IOfferAppService>();
            lookupAppService = serviceProvider.GetService<ILookUpService>();
            blockAppService = serviceProvider.GetService<IBlockAppService>();
            branchAppService = serviceProvider.GetService<IBranchAppService>();
            memoryCache = serviceProvider.GetService<IMemoryCache>();
            notificationAppService = serviceProvider.GetService<INotificationAppService>();
            verificationService = serviceProvider.GetService<IVerificationService>();
            agencyBudgetService = serviceProvider.GetService<IAgencyBudgetService>();
            wathiqService = serviceProvider.GetService<IWathiqService>();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());

            _tenderController = new TenderController(tenderService, mapper, iDMAppService, supplierService, offerAppService, lookupAppService, blockAppService,
                     notificationAppService, verificationService,
                      agencyBudgetService, wathiqService, rootConfigurationsMock.Object);
        }

        [Fact]
        public async Task GetTendersBySearchCriteria_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.VRO).ToString()) };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { NotInStatusId = (int)TenderStatus.OffersAwardedConfirmed };

            // Act
            var result = await _tenderController.GetTendersBySearchCriteria(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetTendersByAgencyCode_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.VRO).ToString()) };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { NotInStatusId = (int)TenderStatus.OffersAwardedConfirmed };

            // Act
            var result = await _tenderController.GetTendersByAgencyCode(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task FindTenderByAgencyCodeForPurchaseMethod_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.VRO).ToString()) };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { NotInStatusId = (int)TenderStatus.OffersAwardedConfirmed };

            // Act
            var result = await _tenderController.FindTenderByAgencyCodeForPurchaseMethod(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllSupplierTenders_ReturnsData()
        {
            // Arrange
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString())
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { };

            // Act
            var result = await _tenderController.GetAllSupplierTenders(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetBasicOfferTenderInfoById_ReturnsData()
        {
            // Arrange
            int _id = 1;

            // Act
            var result = await _tenderController.GetBasicOfferTenderInfoById(_id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.TenderId, _id);
        }

        [Fact]
        public async Task GetTendersForAppliedSuppliersReport_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "0") };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { NotInStatusId = (int)TenderStatus.OffersAwardedConfirmed };

            // Act
            var result = await _tenderController.GetTendersForAppliedSuppliersReport(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetTendersForCheckingDirectPuchaseStageAsync_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
                new Claim(IdentityConfigs.Claims.CommitteeId, "8")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { };

            // Act
            var result = await _tenderController.GetTendersForCheckingDirectPuchaseStageAsync(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetTendersToJoinCommittees_ReturnsData()
        {
            // Arrange
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.SelectedCR, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "7")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var linkTendersToCommitteeSearchCriteriaModel = new LinkTendersToCommitteeSearchCriteriaModel() { };

            // Act
            var result = await _tenderController.GetTendersToJoinCommittees(linkTendersToCommitteeSearchCriteriaModel);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetTendersForUnderOperationsStageIndex_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
                new Claim(IdentityConfigs.Claims.CommitteeId, "8")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { };

            // Act
            var result = await _tenderController.GetTendersForUnderOperationsStageIndex(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetTendersForOpeningStageIndex_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
                new Claim(IdentityConfigs.Claims.CommitteeId, "8")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { };

            // Act
            var result = await _tenderController.GetTendersForOpeningStageIndex(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetSupplierInfoByCR_ReturnsData()
        {
            // Arrange
            var _supplierFullDataViewModel = new SupplierFullDataViewModel()
            {
                supplierNumber = "1010000154",
                Email = "h@h.com",
                IsicActivity = new List<ViewModel.IsicActivityApiModel>() {
                 new ViewModel.IsicActivityApiModel(){  IsicActivityID = 331509 , Description = "أعمال إصلاح و صيانة و إعادة بناء قاطرات وعربات السكك الحديدية"},
                 new ViewModel.IsicActivityApiModel(){ IsicActivityID = 531001 , Description = "أنشطة البريد العادي"}
                 },
                MainActivity = "البيع بالجملة والتجزئة لاطارات السيارات وتوابعها",
                MainActivityDescription = "test122",
                Mobile = "0567821380",
                SupplierType = 1,
                capital = "0",
                supplierName = "شركة خالد عبدالله الصافي",
                YasserInfo = new ViewModel.YasserApiModel()
                {
                    InvestmentLicenseNumber = "999",
                    MembershipCityCode = "101",
                    MembershipCityName = "الرياض",
                    OfficeFacilityNumberInMinistryOfLabor = "1",
                    SequenceFacilityNumberInMinistryOfLabor = "248621",
                    SocialSecuritySubscriptionNumber = "502612832"
                }
            };
            // Act
            var result = await _tenderController.GetSupplierInfoByCR(_supplierFullDataViewModel.supplierNumber);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_supplierFullDataViewModel.supplierNumber, result.supplierNumber);
            Assert.Equal(_supplierFullDataViewModel.Email, result.Email);
            Assert.Equal(_supplierFullDataViewModel.MainActivity, result.MainActivity);
            Assert.Equal(_supplierFullDataViewModel.MainActivityDescription, result.MainActivityDescription);
            Assert.Equal(_supplierFullDataViewModel.Mobile, result.Mobile);
            Assert.Equal(_supplierFullDataViewModel.SupplierType, result.SupplierType);
            Assert.Equal(_supplierFullDataViewModel.capital, result.capital);
            Assert.Equal(_supplierFullDataViewModel.supplierName, result.supplierName);

            Assert.NotNull(result.Addresses);
            Assert.NotEmpty(result.Addresses);

            Assert.NotNull(result.IsicActivity);
            Assert.NotEmpty(result.IsicActivity);
            Assert.Equal(_supplierFullDataViewModel.IsicActivity[0].IsicActivityID, result.IsicActivity[0].IsicActivityID);
            Assert.Equal(_supplierFullDataViewModel.IsicActivity[0].Description, result.IsicActivity[0].Description);
            Assert.Equal(_supplierFullDataViewModel.IsicActivity[1].IsicActivityID, result.IsicActivity[1].IsicActivityID);
            Assert.Equal(_supplierFullDataViewModel.IsicActivity[1].Description, result.IsicActivity[1].Description);

            Assert.NotNull(result.YasserInfo);
            Assert.Equal(_supplierFullDataViewModel.YasserInfo.InvestmentLicenseNumber, result.YasserInfo.InvestmentLicenseNumber);
            Assert.Equal(_supplierFullDataViewModel.YasserInfo.MembershipCityCode, result.YasserInfo.MembershipCityCode);
            Assert.Equal(_supplierFullDataViewModel.YasserInfo.MembershipCityName, result.YasserInfo.MembershipCityName);
            Assert.Equal(_supplierFullDataViewModel.YasserInfo.OfficeFacilityNumberInMinistryOfLabor, result.YasserInfo.OfficeFacilityNumberInMinistryOfLabor);
            Assert.Equal(_supplierFullDataViewModel.YasserInfo.SequenceFacilityNumberInMinistryOfLabor, result.YasserInfo.SequenceFacilityNumberInMinistryOfLabor);
            Assert.Equal(_supplierFullDataViewModel.YasserInfo.SocialSecuritySubscriptionNumber, result.YasserInfo.SocialSecuritySubscriptionNumber);
        }

        [Fact]
        public async Task GetTendersForCheckingStageIndex_ReturnsData()
        {
            // Arrange
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "8"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "7"),
                new Claim(IdentityConfigs.Claims.CommitteeId, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "9")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { };

            // Act
            var result = await _tenderController.GetTendersForCheckingStageIndex(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetVROTendersCreatedByGovAgency_ReturnsData()
        {
            // Arrange
            _claims = new Claim[5] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "8"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "019001000000"),
                new Claim(IdentityConfigs.Claims.BranchId, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.Role, "12"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { };

            // Act
            var result = await _tenderController.GetVROTendersCreatedByGovAgency(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetTendersForAwardingStageIndex_ReturnsData()
        {
            // Arrange
            _claims = new Claim[5] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "8"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "7"),
                new Claim(IdentityConfigs.Claims.BranchId, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "9"),
                new Claim(IdentityConfigs.Claims.Role, "12"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { };

            // Act
            var result = await _tenderController.GetTendersForAwardingStageIndex(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        //[Fact]
        //public async Task RejectInitialAwardTenderOffer_ReturnsData()
        //{
        //    // Arrange
        //    var rejectionModel = new RejectionModel() { };

        //    // Act
        //    await _tenderController.RejectInitialAwardTenderOffer(rejectionModel);

        //    //Assert
        //}

        [Fact]
        public async Task GetAllTendersHasEnquiry_ReturnsData()
        {
            // Arrange
            _claims = new Claim[5] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "8"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "7"),
                new Claim(IdentityConfigs.Claims.BranchId, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "9"),
                new Claim(IdentityConfigs.Claims.Role, "12"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            var tenderSearchCriteria = new TenderSearchCriteria() { };

            // Act
            var result = await _tenderController.GetAllTendersHasEnquiry(tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task GetFinancialYear_ReturnsData()
        {
            // Arrange

            // Act
            var result = await _tenderController.GetFinancialYear();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetStatus_ReturnsData()
        {
            // Arrange
            _claims = new Claim[5] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "8"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "7"),
                new Claim(IdentityConfigs.Claims.BranchId, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "9"),
                new Claim(IdentityConfigs.Claims.Role, "12"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            // Act
            var result = await _tenderController.GetStatus();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetApprovedStatuses_ReturnsData()
        {
            // Arrange

            // Act
            var result = await _tenderController.GetApprovedStatuses();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByTenderIdInvitationDetails_ReturnsData()
        {
            // Arrange
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.Vendor, "7")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            // Act
            var result = await _tenderController.GetByTenderIdInvitationDetails(5);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task EmarketPlace_ReturnsData()
        {
            // Arrange
            List<int> _iDs = new List<int>() { 1, 2 };

            // Act
            var result = await _tenderController.EmarketPlace(_iDs);

            //Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task IsTenderPurchasedBySupplier_ReturnsData()
        {
            // Arrange
            int _tenderId = 2;
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.Vendor, "7")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            // Act
            var result = await _tenderController.IsTenderPurchasedBySupplier(_tenderId);

            //Assert
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task FindAllVacationDates_ReturnsData()
        {
            // Arrange

            // Act
            var result = await _tenderController.FindAllVacationDates();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBasicTenderDetailsById_ReturnsData()
        {
            // Arrange
            var _tenderIdString = Util.Encrypt(3);
            // Arrange
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "8"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "7"),
                new Claim(IdentityConfigs.Claims.CommitteeId, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "9")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetBasicTenderDetailsById(_tenderIdString);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMainTenderDetailsById_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "4") };
            _tenderController = _tenderController.WithIdentity(_claims);
            var _tenderIdString = Util.Encrypt(1);
            // Act
            var result = await _tenderController.GetMainTenderDetailsById(_tenderIdString);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMainTenderDetailsForUnit_ReturnsData()
        {
            // Arrange
            var _tenderDetailsModel = new TenderDetailsModel()
            {
                TenderId = 1,
                TenderIdString = Util.Encrypt(1),
                AgencyCode = "022001000000",
                BranchId = 4,
                AgencyName = "وزارة الصحة - الديوان العام",
                TenderStatusIdString = "6qPa1DWumsGG0KjPQhbCXA==",
                CreatedBy = "مدخل  بيانات مصطفي ابراهيم | 1083471993",
                HasPreQualification = "No",
                InvitationTypeIdString = "عامة",
                InvitationTypeName = "عامة",
                OfferPresentationWayId = 1,
                OffersOpeningCommitteeName = "",
                OffersCheckingCommitteeName = ""
            };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, _tenderDetailsModel.BranchId.ToString()),
                new Claim(IdentityConfigs.Claims.SelectedCR, _tenderDetailsModel.AgencyCode),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetMainTenderDetailsForUnit(_tenderDetailsModel.TenderIdString);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_tenderDetailsModel.TenderId, result.TenderId);
            Assert.Equal(_tenderDetailsModel.TenderIdString, result.TenderIdString);
            Assert.Equal(_tenderDetailsModel.AgencyCode, result.AgencyCode);
            Assert.Equal(_tenderDetailsModel.BranchId, result.BranchId);
            Assert.Equal(_tenderDetailsModel.AgencyName, result.AgencyName);
            Assert.Equal(_tenderDetailsModel.TenderStatusIdString, result.TenderStatusIdString);
            Assert.Equal(_tenderDetailsModel.CreatedBy, result.CreatedBy);
            Assert.Equal(_tenderDetailsModel.HasPreQualification, result.HasPreQualification);
            Assert.Equal(_tenderDetailsModel.InvitationTypeIdString, result.InvitationTypeIdString);
            Assert.Equal(_tenderDetailsModel.InvitationTypeName, result.InvitationTypeName);
            Assert.Equal(_tenderDetailsModel.OfferPresentationWayId, result.OfferPresentationWayId);
            Assert.Equal(_tenderDetailsModel.OffersOpeningCommitteeName, result.OffersOpeningCommitteeName);
            Assert.Equal(_tenderDetailsModel.OffersCheckingCommitteeName, result.OffersCheckingCommitteeName);
        }

        [Fact]
        public async Task GetMainTenderDetailsForSuppliers_ReturnsData()
        {
            // Arrange
            var _tenderIdString = Util.Encrypt(3);
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString())
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetMainTenderDetailsForSuppliers(_tenderIdString);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMainTenderDetailsForVisitor_ReturnsData()
        {
            var _tenderIdString = Util.Encrypt(1);
            var _expectedTenderDetailsModel = new TenderDetailsModel()
            {
                TenderIdString = "uIIHdMXuI66Ey 98x66jkg==",
                AgencyCode = "022001000000",
                ReferenceNumber = "191139001001",
                BonusValue = null,
                ConditionsBookletPrice = 0,
                EstimatedValue = 70000,
                FinancialFees = 200,
                InvitationTypeId = 1,
                NeededInitialGurantee = "No Guarantee",
                LastOfferPresentationDate = DateTime.Parse("10/8/2021 2:50:00 PM"),
                NumberOfWinners = null
            };

            var _actualResult = await _tenderController.GetMainTenderDetailsForVisitor(_tenderIdString);

            Assert.NotNull(_actualResult);
            Assert.Equal(_expectedTenderDetailsModel.TenderIdString, _actualResult.TenderIdString);
            Assert.Equal(_expectedTenderDetailsModel.AgencyCode, _actualResult.AgencyCode);
            Assert.Equal(_expectedTenderDetailsModel.ReferenceNumber, _actualResult.ReferenceNumber);
            Assert.Equal(_expectedTenderDetailsModel.BonusValue, _actualResult.BonusValue);
            Assert.Equal(_expectedTenderDetailsModel.ConditionsBookletPrice, _actualResult.ConditionsBookletPrice);
            Assert.Equal(_expectedTenderDetailsModel.EstimatedValue, _actualResult.EstimatedValue);
            Assert.Equal(_expectedTenderDetailsModel.FinancialFees, _actualResult.FinancialFees);
            Assert.Equal(_expectedTenderDetailsModel.InvitationTypeId, _actualResult.InvitationTypeId);
            Assert.Equal(_expectedTenderDetailsModel.NumberOfWinners, _actualResult.NumberOfWinners);
            Assert.Equal(_expectedTenderDetailsModel.NeededInitialGurantee, _actualResult.NeededInitialGurantee);
            Assert.Equal(_expectedTenderDetailsModel.LastOfferPresentationDate, _actualResult.LastOfferPresentationDate);
            Assert.False(_actualResult.BrancHasNoCommittees);
        }

        [Fact]
        public async Task GetTendersByLogedUser_ReturnsData()
        {
            // Arrange
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString())
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetTendersByLogedUser();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBasicTenderDataById_ReturnsData()
        {
            var _expectedTenderId = 1;
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await _tenderController.GetBasicTenderDataById(_expectedTenderId);

            Assert.NotNull(_actualResult);
            Assert.Equal(_expectedTenderId, _actualResult.TenderId);
        }

        [Fact]
        public async Task GetBasicTenderDataById_ReturnsData_IsUnitAgency()
        {
            var _expectedTenderId = 0;
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await _tenderController.GetBasicTenderDataById(_expectedTenderId);

            Assert.NotNull(_actualResult);
            Assert.True(_actualResult.IsUnitAgency);
            Assert.Equal(_expectedTenderId, _actualResult.TenderId);
        }

        [Fact]
        public async Task GetBasicTenderLookups_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "7")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetBasicTenderLookups(5);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderTypeLookups_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1") ,
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "60179"),
            };

            _tenderController = _tenderController.WithIdentity(_claims);
            var result = await _tenderController.GetTenderTypeLookups();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetTenderAgreementAgenciesLookup_ReturnsData()
        {
            // Arrange
            // Act
            var result = await _tenderController.GetTenderAgreementAgenciesLookup();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetSuccededPreQualifications_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "4") };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetSuccededPreQualifications();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetFinishedAnnouncementHasOneSupplier_ReturnsData()
        {
            // Arrange
            string _tenderId = Util.Encrypt(3);
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "2")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetFinishedAnnouncementHasOneSupplier(_tenderId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetFinishedAnnouncementForLimitedTender_ReturnsData()
        {
            // Arrange
            string _tenderId = Util.Encrypt(3);
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "2")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetFinishedAnnouncementForLimitedTender(_tenderId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCommitteeByCommitteeTypeOnBranch_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "2")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetCommitteeByCommitteeTypeOnBranch();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetVROAndTechnicalCommittees_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "2")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetVROAndTechnicalCommittees();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTechincalAndDirectPurchaseCommittees_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "2")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetTechincalAndDirectPurchaseCommittees();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBasicTenderSecondStageDataById_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "2")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetBasicTenderSecondStageDataById(2);

            //Assert
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task CreateSeoncdStageBasic_ReturnsData()
        //{
        //    // Arrange
        //    var secondStageBasicData = new SecondStageBasicData() { TenderFirstStageId = 1, AgencyBudgetNumber = new List<ViewModel.AgencyBudget.AgencyBudgetNumberModel>() { } };
        //    _claims = new Claim[3] {
        //        new Claim(IdentityConfigs.Claims.BranchId, "4"),
        //        new Claim(IdentityConfigs.Claims.UserCategory, "8"),
        //        new Claim(IdentityConfigs.Claims.Vendor, "2")
        //    };

        //    _tenderController = _tenderController.WithIdentity(_claims);
        //    //var identity = new ClaimsIdentity(_claims, "BasicAuthentication");
        //    //_tenderController.ControllerContext.HttpContext.User.AddIdentity(identity);
        //    //tenderService
        //    // Act
        //    var result = await _tenderController.CreateSeoncdStageBasic(secondStageBasicData);

        //    //Assert
        //    Assert.NotNull(result);
        //}

        [Fact]
        public async Task GetTenderByIdForEnquiry_ReturnsData()
        {
            // Arrange
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString())
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetTenderByIdForEnquiry(2);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderByIdForJoinigRequests_ReturnsData()
        {
            // Arrange

            // Act
            var result = await _tenderController.GetTenderByIdForJoinigRequests(2);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderByIdToApplyOffer_ReturnsData()
        {
            var _expectedTenderInformationModel = new TenderInformationModel()
            {
                TenderId = 1,
                TenderStatusId = 4,
                OffersOpeningDate = DateTime.Parse("2020-08-17T13:34:19.7502321")
            };
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetTenderByIdToApplyOffer(_expectedTenderInformationModel.TenderId);

            Assert.NotNull(result);
            Assert.Equal(_expectedTenderInformationModel.TenderId, result.TenderId);
            Assert.Equal(_expectedTenderInformationModel.TenderStatusId, result.TenderStatusId);
            Assert.Equal(_expectedTenderInformationModel.OffersOpeningDate, result.OffersOpeningDate);
            Assert.True(result.InsideKSA);
        }

        [Fact]
        public async Task Add_ReturnsData()
        {
            // Arrange
            var createTenderBasicDataModel = new CreateTenderBasicDataModel()
            {
                TenderName = "TName",
                TenderTypeId = (int)TenderType.FirstStageTender
            };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "022001000000")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.Add(createTenderBasicDataModel);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateVROTenderByRelatedAgency_ReturnsData()
        {
            // Arrange
            var _tenderName = "Test-TenderName" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Minute + DateTime.Now.Millisecond;
            var createTenderBasicDataModel = new CreateTenderBasicDataModel()
            {
                TenderName = _tenderName,
                EstimatedValue = 70000,
                OfferPresentationWayId = 1,
                TenderTypeId = 1
            };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "022001000000")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.CreateVROTenderByRelatedAgency(createTenderBasicDataModel);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateVROTenderByRelatedAgency_ReturnsData()
        {
            // Arrange
            var _tenderName = "Test-TenderName" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Minute + DateTime.Now.Millisecond;
            var createTenderBasicDataModel = new CreateTenderBasicDataModel()
            {
                TenderId = 22664,
                TenderName = _tenderName,
                EstimatedValue = 70000,
                OfferPresentationWayId = 1,
                TenderTypeId = 1
            };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "022001000000")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.CreateVROTenderByRelatedAgency(createTenderBasicDataModel);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderCommitteesByTenderId_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.UserCategory, "8"),
                new Claim(IdentityConfigs.Claims.Vendor, "2")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetTenderCommitteesByTenderId(2);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task EditCommittees_ReturnsData()
        {
            // Arrange
            var editeCommitteesModel = new EditeCommitteesModel()
            {
                TenderId = 1
            };

            // Act
            var result = await _tenderController.EditCommittees(editeCommitteesModel);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderSamplesAddress_ReturnsData()
        {
            var _tenderId = 1;
            var _sampleDeliveryAddress = "delivery_address11";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetTenderSamplesAddress(_tenderId);

            Assert.NotNull(result);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_sampleDeliveryAddress, result.SamplesDeliveryAddress);

        }

        [Fact]
        public async Task EditSamplesDeliveryAddress_ReturnsData()
        {
            var _tenderId = 1;
            var _sampleDeliveryAddress = "delivery_address11";

            var result = await _tenderController.EditSamplesDeliveryAddress(_tenderId, _sampleDeliveryAddress);

            Assert.NotNull(result);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_sampleDeliveryAddress, result.SamplesDeliveryAddress);
        }

        [Fact]
        public async Task GetTenderToEditAreas_ReturnsData()
        {
            var _tenderId = 1;
            var _tenderNumber = "4";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetTenderToEditAreas(_tenderId);

            Assert.NotNull(result);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_tenderNumber, result.TenderNumber);
        }

        [Fact]
        public async Task EditAreas_ReturnsData()
        {
            var tenderAreasModel = new TenderAreasModel() { TenderId = 1, TenderNumber = "4", TenderAreaIDs = new List<int>() { 2 } };

            var result = await _tenderController.EditAreas(tenderAreasModel);

            Assert.NotNull(result);
            Assert.Equal(tenderAreasModel.TenderId, result.TenderId);
            Assert.Equal(tenderAreasModel.TenderNumber, result.TenderNumber);
        }

        [Fact]
        public async Task EditCountries_ReturnsData()
        {
            var tenderAreasModel = new TenderAreasModel() { TenderId = 1, TenderNumber = "4", TenderCountriesIDs = new List<int>() { 9 } };

            var result = await _tenderController.EditCountries(tenderAreasModel);

            Assert.NotNull(result);
            Assert.Equal(tenderAreasModel.TenderId, result.TenderId);
            Assert.Equal(tenderAreasModel.TenderNumber, result.TenderNumber);
        }

        [Fact]
        public async Task GetPurchaseModelByTenderId_ReturnsData()
        {
            // Arrange
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.Email, "mail-"),
                new Claim(IdentityConfigs.Claims.Mobile, "123")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetPurchaseModelByTenderId(5);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task _ReturnsData()
        {
            // Arrange
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.Email, "mail-"),
                new Claim(IdentityConfigs.Claims.Mobile, "123")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetInvitationBillDetailsModelByTenderId(5);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCommunicationRequestsDetailsTenderId_ReturnsData()
        {
            // Arrange
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, _agencyCode) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetCommunicationRequestsDetailsTenderId(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderOffersByIdAsync_ReturnsData()
        {
            // Arrange

            // Act
            var result = await _tenderController.GetTenderOffersByIdAsync(4);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderOfferDetailsForOppeningStage_ThrowsException()
        {
            var _tenderId = 1;
            var _agencyCode = "022001000000";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1") ,
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "15107"),
            };

            _tenderController = _tenderController.WithIdentity(_claims);

            await Assert.ThrowsAsync<NullReferenceException>(() => _tenderController.GetTenderOfferDetailsForOppeningStage(_tenderId));
        }

        [Fact]
        public async Task GetTenderOfferDetailsByTenderIdForCheckStage_ReturnsData()
        {
            var _tenderId = 54;
            var _agencyCode = "022001000000";
            var _tenderNumber = "624323";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1") ,
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "15107"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetTenderOfferDetailsByTenderIdForCheckStage(_tenderId);

            Assert.NotNull(result);
            Assert.Equal(_tenderNumber, result.TenderNumber);
            Assert.Equal(Util.Encrypt(_tenderId), result.TenderIdString);
            Assert.True(result.InsideKSA);
            Assert.True(result.IsValidToCheck);
            Assert.True(result.IsValidToGoToFinancailCheck);
            Assert.True(result.isOldFlow);
        }

        //[Fact]
        //public async Task GetTenderOffersDetailsForOpenAwarding_ReturnsData()
        //{
        //    var _tenderId = 1;
        //    var _tenderNumber = "624323";
        //    _claims = new Claim[3] {
        //        new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1") ,
        //        new Claim(IdentityConfigs.Claims.SelectedCR, "022001000000"),
        //        new Claim(IdentityConfigs.Claims.NameIdentifier, "15107"),
        //    };
        //    _tenderController = _tenderController.WithIdentity(_claims);

        //    var result = await _tenderController.GetTenderOffersDetailsForOpenAwarding(_tenderId);

        //    Assert.NotNull(result);
        //    Assert.Equal(Util.Encrypt(_tenderId), result.TenderIdString);
        //    Assert.Equal(_tenderNumber, result.TenderNumber);
        //}

        [Fact]
        public async Task GetTenderOfferDetailsByTenderIdForAwarding_ReturnsData()
        {
            var _tenderId = 54;
            var _agencyCode = "022001000000";
            var _tenderNumber = "624323";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1") ,
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "15107"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetTenderOfferDetailsByTenderIdForAwarding(_tenderId);

            Assert.NotNull(result);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_tenderNumber, result.TenderNumber);
            Assert.False(result.IsOldTender);
        }

        [Fact]
        public async Task GetOffersForOpenByTenderIdAsync_ReturnsData()
        {
            // Arrange
            var tenderBasicSearchCriteria = new TenderBasicSearchCriteria() { TenderIdString = Util.Encrypt(6) };
            // Act
            var result = await _tenderController.GetOffersForOpenByTenderIdAsync(tenderBasicSearchCriteria);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUnOpenOffersForCombinedSuppliers_ReturnsData()
        {
            // Arrange

            // Act
            var result = await _tenderController.GetUnOpenOffersForCombinedSuppliers(8);

            //Assert
            Assert.True(result == 0);
        }

        [Fact]
        public async Task GetOffersForCheckByTenderIdAsync_ReturnsData()
        {
            // Arrange
            var tenderBasicSearchCriteria = new TenderBasicSearchCriteria() { TenderIdString = Util.Encrypt(6) };
            // Act
            var result = await _tenderController.GetOffersForCheckByTenderIdAsync(tenderBasicSearchCriteria);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetVROOffersForCheckByTenderIdAsync_ReturnsData()
        {
            // Arrange
            // Act
            var result = await _tenderController.GetVROOffersForCheckByTenderIdAsync(new TenderBasicSearchCriteria {TenderId = 7 });

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task MoveTenderToFinancialOffersChecking_ThrowsException()
        {
            // Arrange
            string _tenderIdString = Util.Encrypt(364);

            await Assert.ThrowsAsync<BusinessRuleException>(() => _tenderController.MoveTenderToFinancialOffersChecking(_tenderIdString));
        }

        [Fact]
        public async Task GetDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage_ReturnsData()
        {
            var _expextedTendeOffersModel = new TenderOffersModel()
            {
                TenderId = 54,
                TenderRefrenceNumber = "191139060001",
                TenderNumber = "624323",
                AgencyCode = "022001000000",
                EstimatedValue = 800,
                ExcutionPlace = "Inside KSA",
                InsideKSA = true,
                IsTenderFinancialChecked = true,
                IsTenderTechnicalChecked = true,
                TenderIdString = "5nDvXCYNEdkAz VWoEarIg=="
            };
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "15107"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var _actualResult = await _tenderController.GetDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(_expextedTendeOffersModel.TenderId);

            //Assert
            Assert.NotNull(_actualResult);
            Assert.Equal(_expextedTendeOffersModel.TenderId, _actualResult.TenderId);
            Assert.Equal(_expextedTendeOffersModel.TenderRefrenceNumber, _actualResult.TenderRefrenceNumber);
            Assert.Equal(_expextedTendeOffersModel.TenderNumber, _actualResult.TenderNumber);
            Assert.Equal(_expextedTendeOffersModel.AgencyCode, _actualResult.AgencyCode);
            Assert.Equal(_expextedTendeOffersModel.EstimatedValue, _actualResult.EstimatedValue);
            Assert.Equal(_expextedTendeOffersModel.ExcutionPlace, _actualResult.ExcutionPlace);
            Assert.Equal(_expextedTendeOffersModel.TenderIdString, _actualResult.TenderIdString);
            Assert.True(_actualResult.InsideKSA);
            Assert.False(_actualResult.IsTenderFinancialChecked);
            Assert.False(_actualResult.IsTenderTechnicalChecked);
            Assert.False(_actualResult.HasCheckingDate);
            Assert.False(_actualResult.IsValidToGoToFinancailCheck);
            Assert.False(_actualResult.IsValidToSubmit);
        }

        [Fact]
        public async Task GetOffersForCheckByDirectPurchaseTenderId_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetOffersForCheckByDirectPurchaseTenderId(5);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CheckTenderOffersForDirectPurchasePagingAsync_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.CheckTenderOffersForDirectPurchasePagingAsync(5);

            //Assert
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task IsSupplierPassedForTenderAwarding_ReturnsData()
        //{
        //    var _agencyCode = "022001000000";
        //    _claims = new Claim[3] {
        //        new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1") ,
        //        new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
        //        new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
        //    };

        //    _tenderController = _tenderController.WithIdentity(_claims);

        //    var result = await _tenderController.IsSupplierPassedForTenderAwarding(1, "cr");

        //    Assert.True(result > 0);
        //}

        [Fact]
        public async Task RemoveOfferAwardingValue_ReturnsData()
        {
            // Arrange
            // Act
            var result = await _tenderController.RemoveOfferAwardingValue(5);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CheckSendTenderToApproveAwarding_ReturnsData()
        {
            // Arrange

            var sendToAwardingModel = new SendToAwardingModel()
            {
                EncryptedTenderId = Util.Encrypt(9),
                CRs = new List<string>() { "", "" }
            };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.CheckSendTenderToApproveAwarding(sendToAwardingModel);

            //Assert
            Assert.True(result > 0);
        }

        [Fact]
        public async Task GetOffersForAwardingStageByTenderId_ReturnsData()
        {
            // Arrange
            var tenderBasicSearchCriteria = new TenderBasicSearchCriteria() { TenderIdString = Util.Encrypt(1) };
            // Act
            var result = await _tenderController.GetOffersForAwardingStageByTenderId(tenderBasicSearchCriteria);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetDetailsById_ReturnsData()
        {
            var _expectedTenderOffersStepModel = new TenderOffersStepModel()
            {
                TenderId = 1,
                TenderStatusId = 4,
                TenderTypeId = 1,
                LastOfferPresentationDate = DateTime.Parse("10/8/2021 2:50:00 PM"),
                OffersOpeningAddressId = 1
            };
            var _agencyCode = "022001000000";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await _tenderController.GetDetailsById(_expectedTenderOffersStepModel.TenderId);

            Assert.NotNull(_actualResult);
            Assert.Equal(_expectedTenderOffersStepModel.TenderId, _actualResult.TenderId);
            Assert.Equal(_expectedTenderOffersStepModel.TenderStatusId, _actualResult.TenderStatusId);
            Assert.Equal(_expectedTenderOffersStepModel.TenderTypeId, _actualResult.TenderTypeId);
            Assert.Equal(_expectedTenderOffersStepModel.LastOfferPresentationDate, _actualResult.LastOfferPresentationDate);
            Assert.Equal(_expectedTenderOffersStepModel.OffersOpeningAddressId, _actualResult.OffersOpeningAddressId);
            Assert.False(_actualResult.SupplierNeedSample);
        }

        [Fact]
        public async Task GetDetailsById_Throws_UnHandledAccessException_IfTenderNotFound()
        {
            var _tenderId = 0;
            var _expectedMessage = Resources.TenderResources.ErrorMessages.FillTenderBasicInformation;
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await Assert.ThrowsAsync<UnHandledAccessException>(() => _tenderController.GetDetailsById(_tenderId));

            Assert.NotNull(_expectedMessage);
            Assert.Equal(_expectedMessage, _actualResult.Message);
        }

        [Fact]
        public async Task GetDetailsById_Throws_UnHandledAccessException_BasedOnAgencyCode()
        {
            var _expectedMessage = Resources.SharedResources.ErrorMessages.YouHaveNoAccess;
            var _tenderId = 1;
            var _expectedAgencyCode = "aa";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _expectedAgencyCode)
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await Assert.ThrowsAsync<UnHandledAccessException>(() => _tenderController.GetDetailsById(_tenderId));

            Assert.Equal(_expectedMessage, _actualResult.Message);
        }

        [Fact]
        public async Task GetTenderDatesByTenderId_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "4") };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetTenderDatesByTenderId(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetOfferDetailsById_ReturnsData()
        {
            // Arrange
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetOfferDetailsById(4);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindTenderByAgencyCode_ReturnsData()
        {
            var _agencyCode = "022001000000";
            var _expectedModel = new TenderOffersStepModel()
            {
                TenderId = 1,
                TenderTypeId = 1,
                OffersOpeningAddressId = 1,
                SupplierNeedSample = false,
                TenderStatusId = 4,
                ChangeRequestTypeId = 0
            };
            var _actualModel = await _tenderController.FindTenderByAgencyCode(_agencyCode);

            Assert.NotNull(_actualModel);
            Assert.Equal(_expectedModel.TenderId, _actualModel.TenderId);
            Assert.Equal(_expectedModel.TenderTypeId, _actualModel.TenderTypeId);
            Assert.Equal(_expectedModel.TenderStatusId, _actualModel.TenderStatusId);
            Assert.Equal(_expectedModel.OffersOpeningAddressId, _actualModel.OffersOpeningAddressId);
            Assert.Equal(_expectedModel.SupplierNeedSample, _actualModel.SupplierNeedSample);
            Assert.Equal(_expectedModel.ChangeRequestTypeId, _actualModel.ChangeRequestTypeId);
        }

        [Fact]
        public async Task GetRelationsById_ReturnsData()
        {
            // Arrange
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "4") };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetRelationsById(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAttachmentsByTenderId_ReturnsData()
        {
            // Arrange
            var _expecTenderId = 1;
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "4") };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var _actResult = await _tenderController.GetAttachmentsByTenderId(_expecTenderId);

            //Assert
            Assert.NotNull(_actResult);
            Assert.Equal(_expecTenderId, Util.Decrypt(_actResult.STenderId));
        }

        //[Fact]
        //public async Task ProcessAttachments_ReturnsData()
        //{
        //    // Arrange
        //    _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "4") };
        //    _tenderController = _tenderController.WithIdentity(_claims);
        //    // Act
        //    var result = await _tenderController.ProcessAttachments(new Core.Entities.Tender(1,1,"",1),true);

        //    //Assert
        //    Assert.NotNull(result);
        //}

        [Fact]
        public async Task GetAttachmentsStepByTenderId_ReturnsData()
        {
            var _expectedTenderId = 2;
            var _agencyCode = "022001000000";
            var _expectedTenderAttachmentId = 1;
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await _tenderController.GetAttachmentsStepByTenderId(_expectedTenderId);

            Assert.NotNull(_actualResult);
            Assert.NotEmpty(_actualResult);
            Assert.Equal(_expectedTenderId, _actualResult[0].TenderId);
            Assert.Equal(_expectedTenderAttachmentId, _actualResult[0].TenderAttachmentId);
        }

        [Fact]
        public async Task GetAttachmentsStepByTenderId_Throws_UnHandledAccessException()
        {
            var _expecTenderId = 1;
            var _expectedMessage = Resources.SharedResources.ErrorMessages.YouHaveNoAccess;
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "4") };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await Assert.ThrowsAsync<UnHandledAccessException>(() => _tenderController.GetAttachmentsStepByTenderId(_expecTenderId));

            Assert.Equal(_expectedMessage, _actualResult.Message);
        }

        [Fact]
        public async Task GetAttachmentsEditStepByTenderId_ReturnsData()
        {
            var _expectedTenderId = 1;
            var _expectedAgencyCode = "022001000000";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _expectedAgencyCode)
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await _tenderController.GetAttachmentsEditStepByTenderId(_expectedTenderId);

            Assert.NotNull(_actualResult);
            Assert.Equal(_expectedTenderId, _actualResult.TenderID);
            Assert.Equal(_expectedTenderId, Util.Decrypt(_actualResult.TenderIDString));
            Assert.Equal(_expectedAgencyCode, _actualResult.AgencyCode);
        }

        [Fact]
        public async Task GetAttachmentsEditStepByTenderId_Throws_UnHandledAccessException_Youhavenoaccess()
        {
            var _tenderId = 18;
            var _expectedMessage = Resources.SharedResources.ErrorMessages.YouHaveNoAccess;
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await Assert.ThrowsAsync<UnHandledAccessException>(() => _tenderController.GetAttachmentsEditStepByTenderId(_tenderId));

            Assert.Equal(_expectedMessage, _actualResult.Message);
        }

        [Fact]
        public async Task GetAttachmentsEditStepByTenderId_Throws_UnHandledAccessException_BasedOnAgencyCode()
        {
            var _expectedMessage = Resources.SharedResources.ErrorMessages.YouHaveNoAccess;
            var _tenderId = 1;
            var _expectedAgencyCode = "aa";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _expectedAgencyCode)
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await Assert.ThrowsAsync<UnHandledAccessException>(() => _tenderController.GetAttachmentsEditStepByTenderId(_tenderId));

            Assert.Equal(_expectedMessage, _actualResult.Message);
        }

        [Fact]
        public async Task GetRelationsDetailsByTenderId_ReturnsData()
        {
            // Arrange

            // Act
            var result = await _tenderController.GetRelationsDetailsByTenderId(4);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderHistoryByUserIdAndTenderIdAndStatusId_ReturnsData()
        {
            var _expectedTenderIdString = Util.Encrypt(1);
            var _expectedTenderStatusId = 4;

            var _actualResult = await _tenderController.GetTenderHistoryByUserIdAndTenderIdAndStatusId(_expectedTenderIdString, _expectedTenderStatusId);

            Assert.NotNull(_actualResult);
            Assert.Equal(Util.Decrypt(_expectedTenderIdString), _actualResult.TenderId);
        }

        [Fact]
        public async Task GetTenderQuantityTablesById_ReturnsData()
        {
            var _tenderId = 1;

            var result = await _tenderController.GetTenderQuantityTablesById(_tenderId);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task SendInvitations_ReturnsData()
        {
            // Arrange
            var invitationCrModels = new List<InvitationCrModel>() {
            new InvitationCrModel(){  TenderId = 1}
            };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.Vendor, "123") ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "1"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.SendInvitations(invitationCrModels);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SendRequestToApplayForTender_ReturnsData()
        {
            var basicTenderModel = new BasicTenderModel() { TenderId = 1 };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, "1010018340") ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString())
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.SendRequestToApplayForTender(basicTenderModel);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateTenderDates_ReturnsData()
        {
            var _expectedTenderDatesModel = new TenderDatesModel()
            {
                TenderId = 293,
                TenderNumber = "213214",
                ReferenceNumber = "191139352001",
                AgencyCode = "022001000000",
                LastEnqueriesDate = DateTime.Now.AddMonths(1),
                LastOfferPresentationDate = DateTime.Now.AddMonths(2),
                OffersOpeningDate = DateTime.Now.AddMonths(3),

            };
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "13")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await _tenderController.UpdateTenderDates(_expectedTenderDatesModel);

            Assert.NotNull(_actualResult);
            Assert.Equal(_expectedTenderDatesModel.TenderId, _actualResult.TenderId);
            Assert.Equal(_expectedTenderDatesModel.TenderNumber, _actualResult.TenderNumber);
            Assert.Equal(_expectedTenderDatesModel.ReferenceNumber, _actualResult.ReferenceNumber);
            Assert.Equal(_expectedTenderDatesModel.AgencyCode, _actualResult.AgencyCode);
            Assert.True(_actualResult.InsideKSA);
        }

        [Fact]
        public async Task GetTenderDatesDetailsById_ReturnsData()
        {
            var _tenderId = 1;
            var _agencyCode = "022001000000";
            var _tenderNumber = "4";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, _agencyCode),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetTenderDatesDetailsById(_tenderId);

            Assert.NotNull(result);
            Assert.Equal(_agencyCode, result.AgencyCode);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_tenderNumber, result.TenderNumber);
        }

        [Fact]
        public async Task GetTenderExtendDatesByTenderId_ReturnsData()
        {
            var _tenderId = 1;
            var _agencyCode = "022001000000";
            var _tenderNumber = "4";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetTenderExtendDatesByTenderId(_tenderId);

            Assert.NotNull(result);
            Assert.Equal(_agencyCode, result.AgencyCode);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_tenderNumber, result.TenderNumber);
        }

        [Fact]
        public async Task SaveTenderAttachments_ReturnsData()
        {
            // Arrange
            var attachements = new List<TenderAttachmentModel>() {
             new TenderAttachmentModel(){  TenderId = 1,Name = "__" ,AttachmentTypeId = 1}
            };
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.Vendor, "123") ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.NameIdentifier, "7"),
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.SaveTenderAttachments(attachements);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderConditionsTemplateById_ReturnsData()
        {
            var _tenderIdString = Util.Encrypt(1);
            var _agencyCode = "022001000000";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetTenderConditionsTemplateById(_tenderIdString);

            Assert.NotNull(result);
            Assert.Equal(_agencyCode, result.AgencyCode);
            Assert.Equal(_tenderIdString, result.EncryptedTenderId);
            Assert.NotEmpty(result.ListOfSections);
            Assert.NotEmpty(result.TemplateIds);
        }

        [Fact]
        public async Task GetConditionTemplate_ReturnsData()
        {
            var _tenderId = 1;
            var _tenderIdString = Util.Encrypt(_tenderId);
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.BranchId, "4")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await _tenderController.GetConditionTemplate(_tenderIdString);

            Assert.NotNull(_actualResult);
            Assert.Equal(_tenderId, Util.Decrypt(_actualResult.EncryptedTenderId));
        }

        [Fact]
        public async Task GetConditionTemplate_Throws_BusinessRuleException()
        {
            var _tenderId = 1;
            var _tenderIdString = Util.Encrypt(_tenderId);
            var _expectedMessage = Resources.SharedResources.ErrorMessages.YouHaveNoAccess;
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "0") };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await Assert.ThrowsAsync<BusinessRuleException>(() => _tenderController.GetConditionTemplate(_tenderIdString));

            Assert.NotNull(_actualResult);
            Assert.Equal(_expectedMessage, _actualResult.Message);
        }

        [Fact]
        public async Task GetAllAddresses_ReturnsData()
        {
            // Arrange
            // Act
            var result = await _tenderController.GetAllAddresses();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetOfferOpeningAddresses_ReturnsData()
        {
            var _expectedOpenningAddresses = new AddressModelDefault().BuildAddressModelList();
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.BranchId, "4") };
            _tenderController = _tenderController.WithIdentity(_claims);

            var _actualResult = await _tenderController.GetOfferOpeningAddresses();

            Assert.NotNull(_actualResult);
            Assert.NotEmpty(_actualResult);
            Assert.Equal(_expectedOpenningAddresses[0].AddressId, _actualResult[0].AddressId);
            Assert.Equal(_expectedOpenningAddresses[0].AddressName, _actualResult[0].AddressName);
            Assert.Equal(_expectedOpenningAddresses[0].AddressTypeId, _actualResult[0].AddressTypeId);
        }

        [Fact]
        public async Task GetOfferOpeningAddressesByBranchId_ReturnsData()
        {
            // Arrange
            var _branchId = 4;
            // Act
            var result = await _tenderController.GetOfferOpeningAddressesByBranchId(_branchId);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task PurshaseTender_ReturnsData()
        {
            // Arrange
            var _tenderId = 1;
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.Vendor, "123") ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
                new Claim(IdentityConfigs.Claims.Email, "mail-"),
                new Claim(IdentityConfigs.Claims.Mobile, "123")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.PurshaseTender(_tenderId);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSupplierFavouritTendersList_ReturnsData()
        {
            // Arrange
            var _tenderSearchCriteria = new TenderSearchCriteria();
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetSupplierFavouritTendersList(_tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindAwardedTenderBySearchCriteria_ReturnsData()
        {
            // Arrange
            var _tenderSearchCriteria = new TenderSearchCriteria();
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.FindAwardedTenderBySearchCriteria(_tenderSearchCriteria);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteTenderFromSupplierTendersFavouriteList_ReturnsData()
        {
            // Arrange
            var _tenderId = 3;
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.DeleteTenderFromSupplierTendersFavouriteList(_tenderId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddTenderToSupplierTendersFavouriteList_ReturnsData()
        {
            // Arrange
            var _tenderId = 3;
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, ((int)IDMUserCategory.GovVendor).ToString()) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.AddTenderToSupplierTendersFavouriteList(_tenderId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckifCurrentSupplierBlocked_Returns_True()
        {
            // Arrange
            var _commericalRegisterNo = "1011020232";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, _commericalRegisterNo) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.CheckifCurrentSupplierBlocked();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckifCurrentSupplierBlocked_Returns_False()
        {
            // Arrange
            var _commericalRegisterNo = "000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.Vendor, _commericalRegisterNo) ,
                new Claim(IdentityConfigs.Claims.UserCategory, ((int)IDMUserCategory.GovVendor).ToString()),
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.CheckifCurrentSupplierBlocked();

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllSuppliers_ReturnsData()
        {
            // Arrange
            var supplierSearchCretriaModel = new SupplierSearchCretriaModel();
            // Act
            var result = await _tenderController.GetAllSuppliers(supplierSearchCretriaModel);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task NotificationStatusReport_ReturnsData()
        {
            // Arrange
            var tenderNotificationSearchCriteria = new TenderNotificationSearchCriteria() { tenderId = Util.Encrypt(3) };
            // Act
            var result = await _tenderController.NotificationStatusReport(tenderNotificationSearchCriteria);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSupplierByCr_ReturnsData()
        {
            // Arrange
            var _commericalRegisterNo = "";
            // Act
            var result = await _tenderController.GetSupplierByCr(_commericalRegisterNo);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAgencyBranchFavouriteLists_ReturnsData()
        {
            // Arrange
            var _favouriteSupplierListModel = new FavouriteSupplierListModel();
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.SelectedCR, "8"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "7")
            };
            _tenderController = _tenderController.WithIdentity(_claims);
            // Act
            var result = await _tenderController.GetAgencyBranchFavouriteLists(_favouriteSupplierListModel);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteFavouriteSupplierList_ReturnsData()
        {
            var _favouriteSupplierListModel = new FavouriteSupplierListModel() { AgencyCode = "022001000000", Name = "Name123", FavouriteSupplierListId = 1 };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "022001000000"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.DeleteFavouriteSupplierList(_favouriteSupplierListModel);

            Assert.True(result);
        }

        [Fact]
        public async Task AddFavouriteSupplierList_ReturnsData()
        {
            var _agencyCode = "022001000000";
            var _name = "Test_FavouriteSupplierName_" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Minute + DateTime.Now.Millisecond;
            var _favouriteSupplierListModel = new FavouriteSupplierListModel() { AgencyCode = _agencyCode, Name = _name, FavouriteSupplierListId = 1 };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.AddFavouriteSupplierList(_favouriteSupplierListModel);

            Assert.NotNull(result);
            Assert.Equal(_favouriteSupplierListModel.Name, result.Name);
            Assert.Equal(_favouriteSupplierListModel.AgencyCode, result.AgencyCode);
        }
        [Fact]
        public async Task DeleteFavouriteSupplierListToTransfer_ReturnsData()
        {
            var _agencyCode = "VRO000007";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "8"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.DeleteFavouriteSupplierListToTransfer(5);

            Assert.True(result);
        }

        [Fact]
        public async Task GetFavouriteSuppliersByListId_ReturnsData()
        {
            var _agencyCode = "022001000000";
            var _favouriteSupplierListModel = new SupplierSearchCretriaModel() { AgencyCode = _agencyCode, BranchId = 4, FavouriteSupplierListId = 1 };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.GetFavouriteSuppliersByListId(_favouriteSupplierListModel);

            Assert.NotNull(result);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
        }

        [Fact]
        public async Task AddSupplierToFavouriteSuppliersListAsync_ReturnsData()
        {
            var _agencyCode = "022001000000";
            var _favouriteSupplierListModel = new SupplierSearchCretriaModel() { CommericalRegisterNo = "0917155336", AgencyCode = _agencyCode, BranchId = 1, FavouriteSupplierListId = 1 };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.AddSupplierToFavouriteSuppliersListAsync(_favouriteSupplierListModel);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSupplierFromFavouriteList_ReturnsData()
        {
            var _agencyCode = "022001000000";
            var _favouriteSupplierListModel = new SupplierSearchCretriaModel() { CommericalRegisterNo = "0917155336", AgencyCode = _agencyCode, BranchId = 1, FavouriteSupplierListId = 1 };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "4"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1")
            };
            _tenderController = _tenderController.WithIdentity(_claims);

            var result = await _tenderController.DeleteSupplierFromFavouriteList(_favouriteSupplierListModel);

            Assert.True(result);
        }

        [Fact]
        public async Task OpenTenderDetailsReportForVisitor_ReturnsData()
        {
            var _tenderId = 1;
            var _tenderDetailsReportModel = new TenderDetialsReportModel()
            {
                TenderId = 1,
                TenderNumber = "4",
                ReferenceNumber = "191139001001",
                AgnecyName = "وزارة الصحة - الديوان العام",
                TenderName = "PerformanceTesting14112019",
                LastSupplierActionDateHijri = "1443-03-01",
                LastSupplierActionDate = "2021-10-07",
                LastOfferApplyingDateHijri = "1443-03-02",
                LastOfferApplyingDate = "2021-10-08",
                LastOpenOfferDateHijri = "1441-12-27",
                LastOpenOfferDate = "2020-08-17",
                ApplyingOffersLocation = null,
                OpenOffersLocation = "test",
                SamplesDeliveryAddress = "delivery_address11",
                ExcuationLocation = "Inside KSA",
                ConditionsBookletPrice = 0,
                LastEnqueriesDateString = "2021-10-07",
                LastEnqueriesDateHijriString = "1443-03-01",
                LastOfferPresentationDateString = "2021-10-08",
                LastOfferPresentationDateHijriString = "1443-03-02",
                OffersCheckingDateString = "لا يوجد",
                OffersCheckingDateHijriString = "لا يوجد"
            };

            var result = await _tenderController.OpenTenderDetailsReportForVisitor(_tenderId);
            Assert.NotNull(result);
            Assert.NotEmpty(result.TenderConstructionWorks);
            Assert.Equal(_tenderDetailsReportModel.TenderId, result.TenderId);
            Assert.Equal(_tenderDetailsReportModel.AgnecyName, result.AgnecyName);
            Assert.Equal(_tenderDetailsReportModel.ApplyingOffersLocation, result.ApplyingOffersLocation);
            Assert.Equal(_tenderDetailsReportModel.ConditionsBookletPrice, result.ConditionsBookletPrice);
            Assert.Equal(_tenderDetailsReportModel.ExcuationLocation, result.ExcuationLocation);
            Assert.Equal(_tenderDetailsReportModel.LastEnqueriesDateHijriString, result.LastEnqueriesDateHijriString);
            Assert.Equal(_tenderDetailsReportModel.LastEnqueriesDateString, result.LastEnqueriesDateString);
            Assert.Equal(_tenderDetailsReportModel.LastOfferApplyingDate, result.LastOfferApplyingDate);
            Assert.Equal(_tenderDetailsReportModel.LastOfferApplyingDateHijri, result.LastOfferApplyingDateHijri);
            Assert.Equal(_tenderDetailsReportModel.LastOfferPresentationDateHijriString, result.LastOfferPresentationDateHijriString);
            Assert.Equal(_tenderDetailsReportModel.LastOfferPresentationDateString, result.LastOfferPresentationDateString);
            Assert.Equal(_tenderDetailsReportModel.LastOpenOfferDate, result.LastOpenOfferDate);
            Assert.Equal(_tenderDetailsReportModel.LastOpenOfferDateHijri, result.LastOpenOfferDateHijri);
            Assert.Equal(_tenderDetailsReportModel.LastSupplierActionDate, result.LastSupplierActionDate);
            Assert.Equal(_tenderDetailsReportModel.LastSupplierActionDateHijri, result.LastSupplierActionDateHijri);
            Assert.Equal(_tenderDetailsReportModel.OffersCheckingDateHijriString, result.OffersCheckingDateHijriString);
            Assert.Equal(_tenderDetailsReportModel.OffersCheckingDateString, result.OffersCheckingDateString);
            Assert.Equal(_tenderDetailsReportModel.OpenOffersLocation, result.OpenOffersLocation);
            Assert.Equal(_tenderDetailsReportModel.ReferenceNumber, result.ReferenceNumber);
            Assert.Equal(_tenderDetailsReportModel.SamplesDeliveryAddress, result.SamplesDeliveryAddress);
            Assert.Equal(_tenderDetailsReportModel.TenderName, result.TenderName);
            Assert.Equal(_tenderDetailsReportModel.TenderNumber, result.TenderNumber);
        }

        [Fact]
        public async Task OpenTenderDetailsReport_ReturnsData()
        {
            var _tenderId = 18;
            var _tenderDetailsReportModel = new TenderDetialsReportModel()
            {
                TenderId = 18,
                TenderNumber = "4",
                ReferenceNumber = "191139018001",
                AgnecyName = "وزارة الصحة - الديوان العام",
                TenderName = "VRO by GOV 4",
                LastSupplierActionDateHijri = "1441-03-20",
                LastSupplierActionDate = "2019-11-17",
                LastOfferApplyingDateHijri = "1441-03-21",
                LastOfferApplyingDate = "2019-11-18",
                LastOpenOfferDateHijri = "1441-04-18",
                LastOpenOfferDate = "2019-12-15",
                ApplyingOffersLocation = null,
                OpenOffersLocation = "Test Location",
                SamplesDeliveryAddress = "عنوان تسليم عينات للمورد عنوان تسليم عينات للمورد عنوان تسليم عينات للمورد عنوان تسليم عينات للمورد",
                ExcuationLocation = "Outside KSA",
                ConditionsBookletPrice = 0,
                LastEnqueriesDateString = "2019-11-17",
                LastEnqueriesDateHijriString = "1441-03-20",
                LastOfferPresentationDateString = "2019-11-18",
                LastOfferPresentationDateHijriString = "1441-03-21",
                OffersCheckingDateString = "لا يوجد",
                OffersCheckingDateHijriString = "لا يوجد"
            };

            var result = await _tenderController.OpenTenderDetailsReport(_tenderId);
            Assert.NotNull(result);
            Assert.NotEmpty(result.TenderConstructionWorks);
            Assert.Equal(_tenderDetailsReportModel.TenderId, result.TenderId);
            Assert.Equal(_tenderDetailsReportModel.AgnecyName, result.AgnecyName);
            Assert.Equal(_tenderDetailsReportModel.ApplyingOffersLocation, result.ApplyingOffersLocation);
            Assert.Equal(_tenderDetailsReportModel.ConditionsBookletPrice, result.ConditionsBookletPrice);
            Assert.Equal(_tenderDetailsReportModel.ExcuationLocation, result.ExcuationLocation);
            Assert.Equal(_tenderDetailsReportModel.LastEnqueriesDateHijriString, result.LastEnqueriesDateHijriString);
            Assert.Equal(_tenderDetailsReportModel.LastEnqueriesDateString, result.LastEnqueriesDateString);
            Assert.Equal(_tenderDetailsReportModel.LastOfferApplyingDate, result.LastOfferApplyingDate);
            Assert.Equal(_tenderDetailsReportModel.LastOfferApplyingDateHijri, result.LastOfferApplyingDateHijri);
            Assert.Equal(_tenderDetailsReportModel.LastOfferPresentationDateHijriString, result.LastOfferPresentationDateHijriString);
            Assert.Equal(_tenderDetailsReportModel.LastOfferPresentationDateString, result.LastOfferPresentationDateString);
            Assert.Equal(_tenderDetailsReportModel.LastOpenOfferDate, result.LastOpenOfferDate);
            Assert.Equal(_tenderDetailsReportModel.LastOpenOfferDateHijri, result.LastOpenOfferDateHijri);
            Assert.Equal(_tenderDetailsReportModel.LastSupplierActionDate, result.LastSupplierActionDate);
            Assert.Equal(_tenderDetailsReportModel.LastSupplierActionDateHijri, result.LastSupplierActionDateHijri);
            Assert.Equal(_tenderDetailsReportModel.OffersCheckingDateHijriString, result.OffersCheckingDateHijriString);
            Assert.Equal(_tenderDetailsReportModel.OffersCheckingDateString, result.OffersCheckingDateString);
            Assert.Equal(_tenderDetailsReportModel.OpenOffersLocation, result.OpenOffersLocation);
            Assert.Equal(_tenderDetailsReportModel.ReferenceNumber, result.ReferenceNumber);
            Assert.Equal(_tenderDetailsReportModel.SamplesDeliveryAddress, result.SamplesDeliveryAddress);
            Assert.Equal(_tenderDetailsReportModel.TenderName, result.TenderName);
            Assert.Equal(_tenderDetailsReportModel.TenderNumber, result.TenderNumber);
        }

        [Fact]
        public async Task GetTenderMovementsByTenderId_ReturnsData()
        {
            var _tenderDetailsReportModel = new SimpleTenderSearchCriteria()
            {
                TenderId = 1
            };

            var result = await _tenderController.GetTenderMovementsByTenderId(_tenderDetailsReportModel);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.TotalCount > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task AwardingReport_ReturnsData()
        {
            var _tenderId = 18;
            var _awardingReportModel = new AwardingReportModel()
            {
                TenderId = 18,
                TenderName = "VRO by GOV 4",
                AgencyName = "وزارة الصحة - الديوان العام",
                OffersOpeningDate = "15/12/2019",
                AwardingType = "Full Awarding",
            };

            var result = await _tenderController.AwardingReport(_tenderId);
            Assert.NotNull(result);
            Assert.Equal(_awardingReportModel.TenderId, result.TenderId);
            Assert.Equal(_awardingReportModel.TenderName, result.TenderName);
            Assert.Equal(_awardingReportModel.AgencyName, result.AgencyName);
            Assert.Equal(_awardingReportModel.OffersOpeningDate, result.OffersOpeningDate);
            Assert.Equal(_awardingReportModel.AwardingType, result.AwardingType);
        }

        [Fact]
        public async Task OffersReport_ReturnsData()
        {
            var _tenderId = 18;
            var _offerReportModel = new OfferReportModel()
            {
                TenderName = "VRO by GOV 4",
                Agency = "VRO000007"
            };

            var result = await _tenderController.OffersReport(_tenderId);

            Assert.NotNull(result);
            Assert.Equal(_offerReportModel.TenderName, result.TenderName);
            Assert.Equal(_offerReportModel.Agency, result.Agency);
        }

        [Fact]
        public async Task CountAndCloseAppliedOffers_ReturnsData()
        {
            var _tenderId = 18;
            var _countAndCloseAppliedOffersModel = new CountAndCloseAppliedOffersModel()
            {
                TenderId = 18,
                TenderNumber = "4",
                TenderName = "VRO by GOV 4",
                TenderTypeId = TenderType.NationalTransformationProjects,
                OffersNumbers = 0,
                InvetationsNumbers = 5,
                PurshaseNumbers = 0,
                SubmitionDate = "1441",
                AgencyName = "وزارة الصحة - الديوان العام"
            };

            var result = await _tenderController.CountAndCloseAppliedOffers(_tenderId);
            Assert.NotNull(result);
            Assert.Equal(_countAndCloseAppliedOffersModel.TenderId, result.TenderId);
            Assert.Equal(_countAndCloseAppliedOffersModel.TenderNumber, result.TenderNumber);
            Assert.Equal(_countAndCloseAppliedOffersModel.TenderName, result.TenderName);
            Assert.Equal(_countAndCloseAppliedOffersModel.TenderTypeId, result.TenderTypeId);
            Assert.Equal(_countAndCloseAppliedOffersModel.OffersNumbers, result.OffersNumbers);
            Assert.Equal(_countAndCloseAppliedOffersModel.InvetationsNumbers, result.InvetationsNumbers);
            Assert.Equal(_countAndCloseAppliedOffersModel.PurshaseNumbers, result.PurshaseNumbers);
            Assert.Equal(_countAndCloseAppliedOffersModel.SubmitionDate, result.SubmitionDate);
            Assert.Equal(_countAndCloseAppliedOffersModel.AgencyName, result.AgencyName);
        }

        [Fact]
        public async Task GetCheckingResultsInformation_ReturnsData()
        {
            var _tenderId = 1;
            var result = await _tenderController.GetCheckingResultsInformation(_tenderId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTenderRevisionCancelByTenderId_ReturnsData()
        {
            var _tenderId = 14;
            var _tenderChangeRequestModel = new TenderChangeRequestModel()
            {
                TenderChangeRequestId = 20,
                TenderId = 14,
                ChangeRequestStatusId = 4,
                ChangeRequestTypeId = 4,
                RequestedByRoleName = "NewMonafasat_OffersOpeningAndCheckSecretary"
            };
            var result = await _tenderController.GetTenderRevisionCancelByTenderId(_tenderId);
            Assert.NotNull(result);
            Assert.Equal(_tenderChangeRequestModel.TenderChangeRequestId, result.TenderChangeRequestId);
            Assert.Equal(_tenderChangeRequestModel.TenderId, result.TenderId);
            Assert.Equal(_tenderChangeRequestModel.ChangeRequestStatusId, result.ChangeRequestStatusId);
            Assert.Equal(_tenderChangeRequestModel.ChangeRequestTypeId, result.ChangeRequestTypeId);
            Assert.Equal(_tenderChangeRequestModel.RequestedByRoleName, result.RequestedByRoleName);
            Assert.Equal(_tenderChangeRequestModel.CancelationReasonDescription, result.CancelationReasonDescription);
        }

        [Fact]
        public async Task GetTenderDataForCanelation_ReturnsData()
        {
            var _tenderId = 1;

            var result = await _tenderController.GetTenderDataForCanelation(_tenderId);

            Assert.NotNull(result);
            Assert.Equal(_tenderId, result.TenderId);
        }
    }
}