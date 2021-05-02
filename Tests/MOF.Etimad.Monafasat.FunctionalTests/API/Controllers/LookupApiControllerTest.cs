using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.LocalContent;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class LookupApiControllerTest : BaseTestApiController
    {
        private Claim[] _claims;
        private readonly ILookUpService lookUpService;
        private LookupController _lookupController;
        private readonly IMapper mapper;
        private readonly IIDMAppService iDMAppService;
        private readonly IBranchAppService branchAppService;
        private readonly IMemoryCache memoryCache;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        public LookupApiControllerTest()
        {
            var serviceProvider = services.BuildServiceProvider();
            lookUpService = serviceProvider.GetService<ILookUpService>();
            var config = new MapperConfiguration(cfg =>
            {

                //cfg.CreateMap<SupplierSpecificationModel, SupplierSpecificationAttachment>();
                cfg.ValidateInlineMaps = false;
            });
            mapper = config.CreateMapper();
            iDMAppService = serviceProvider.GetService<IIDMAppService>();
            branchAppService = serviceProvider.GetService<IBranchAppService>();
            memoryCache = serviceProvider.GetService<IMemoryCache>();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            _lookupController = new LookupController(lookUpService, mapper, iDMAppService, branchAppService, memoryCache, rootConfigurationsMock.Object);
        }

        [Theory]
        [InlineData(3)]
        public async Task GetRelationsStepLookups_ReturnsData(int versionId)
        {
            var result = await _lookupController.GetRelationsStepLookups(versionId);
            Assert.NotNull(result);
            Assert.IsType<RelationsStepModels>(result);
            Assert.NotEmpty(result.Activities);
            Assert.NotEmpty(result.ActivitiesWithYears);
            Assert.NotEmpty(result.Areas);
            Assert.NotEmpty(result.Countries);
            Assert.NotEmpty(result.ConstructionWorks);
            Assert.NotEmpty(result.RunningWorks);
        }

        [Fact]
        public async Task GetOfferOppeningLookups_ReturnsData()
        {
            var result = await _lookupController.GetOfferOppeningLookups();

            Assert.NotNull(result);
            Assert.IsType<OfferOpeningLookups>(result);
            Assert.NotEmpty(result.Banks);
            Assert.NotEmpty(result.ConstructionWorks);
            Assert.NotEmpty(result.RunningWorks);
        }

        [Fact]
        public async Task GetAllTenderTypes_ReturnsData()
        {
            var result = await _lookupController.GetAllTenderTypes();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAgencyPurchaseMethods_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetAgencyPurchaseMethods();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllDirectPurchaseTenderReasons_ReturnsData()
        {
            var result = await _lookupController.GetAllDirectPurchaseTenderReasons();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllLimitedTenderReasons_ReturnsData()
        {
            var result = await _lookupController.GetAllLimitedTenderReasons();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllSpendingCategories_ReturnsData()
        {
            var result = await _lookupController.GetAllSpendingCategories();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllCancelationReasons_ReturnsData()
        {
            var result = await _lookupController.GetAllCancelationReasons();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllBuyerSuppliers_ReturnsData()
        {
            var _tenderId = 1;
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.Role, RoleNames.AdminAndAccountManagerPolicy)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetAllBuyerSuppliers(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllSuppliersHaveOffers_ReturnsData()
        {
            var _tenderId = 1;

            var result = await _lookupController.GetAllSuppliersHaveOffers(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllSuppliersHasPrequalifications_ReturnsData()
        {
            var _tenderId = 4;

            var result = await _lookupController.GetAllSuppliersHasPrequalifications(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllPreQualifications_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId, "1"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetAllPreQualifications();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
        }

        [Fact]
        public async Task GetAllQuantityTableRowTypes_ReturnsData()
        {
            var result = await _lookupController.GetAllQuantityTableRowTypes();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllQuantityTableRowType_ReturnsData()
        {
            var result = await _lookupController.GetAllQuantityTableRowType();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAreas_ReturnsData()
        {
            var result = await _lookupController.GetAreas();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAnnounementStatus_ReturnsData()
        {
            var result = await _lookupController.GetAnnounementStatus();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAreasforPreQualification_ReturnsData()
        {
            var result = await _lookupController.GetAreasforPreQualification();

            Assert.NotNull(result);
            Assert.IsType<List<AreaModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCountries_ReturnsData()
        {
            var result = await _lookupController.GetCountries();

            Assert.NotNull(result);
            Assert.IsType<List<CountryModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCountryById_ReturnsData()
        {
            var _countryId = 1;
            var result = await _lookupController.GetCountryById(_countryId);

            Assert.NotNull(result);
            Assert.IsType<CountryModel>(result);
            Assert.Equal(_countryId, result.CountryId);
            Assert.False(result.IsGolf);
        }

        [Fact]
        public async Task GetActivities_ReturnsData()
        {
            var result = await _lookupController.GetActivities();

            Assert.NotNull(result);
            Assert.IsType<List<ActivityModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetMainActivities_ReturnsData()
        {
            var result = await _lookupController.GetMainActivities();

            Assert.NotNull(result);
            Assert.IsType<List<ActivityModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetMainActivitiesByParentId_ReturnsData()
        {
            var result = await _lookupController.GetMainActivitiesByParentId();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetNegotiationTypes_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetNegotiationTypes();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
        }

        [Fact]
        public async Task GetReductionReasons_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetReductionReasons();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
        }

        [Fact]
        public async Task GetConstructionWorks_ReturnsData()
        {
            var result = await _lookupController.GetConstructionWorks();

            Assert.NotNull(result);
            Assert.IsType<List<ConstructionWorkModel>>(result);
        }

        [Fact]
        public async Task GetMaintenanceRunningWorks_ReturnsData()
        {
            var result = await _lookupController.GetMaintenanceRunningWorks();

            Assert.NotNull(result);
            Assert.IsType<List<MaintenanceRunningWorkModel>>(result);
        }

        [Fact]
        public async Task GetBanks_ReturnsData()
        {
            var result = await _lookupController.GetBanks();

            Assert.NotNull(result);
            Assert.IsType<List<BankModel>>(result);
        }

        [Fact]
        public async Task GetAllCommittesByAgency_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetAllCommittesByAgency();

            Assert.NotNull(result);
            Assert.IsType<CommitteesModel>(result);
            Assert.NotEmpty(result.CheckCommittees);
            Assert.NotEmpty(result.OpenCommittees);
            Assert.NotEmpty(result.preQualificationCommittees);
            Assert.NotEmpty(result.purchaseCommittees);
            Assert.NotEmpty(result.TechnicalCommittees);
            Assert.Empty(result.VROCommittee);
        }

        [Fact]
        public async Task GetTechnicalCommitteeList_ReturnsData()
        {
            var _agencyCode = "022001000000";
            var _userId = "1";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.NameIdentifier , _userId),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetTechnicalCommitteeList();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetTechnicalCommitteeListOnBranchTender_ReturnsData()
        {
            var _tenderId = 4;
            var _agencyCode = "022001000000";
            var _userId = "1";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.NameIdentifier , _userId),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetTechnicalCommitteeListOnBranchTender(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllAgenceies_ReturnsData()
        {
            var result = await _lookupController.GetAllAgenceies();

            Assert.NotNull(result);
            Assert.IsType<List<GovAgencyModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllBranchesByAgencyCode_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetAllBranchesByAgencyCode(_agencyCode);

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllBranches_ReturnsData()
        {
            var result = await _lookupController.GetAllBranches();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllBranchesNotAssignedToCommittee_ReturnsData()
        {
            var _committeeId = 1;
            var _committeType = 1;
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetAllBranchesNotAssignedToCommittee(_committeeId, _committeType);

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllDataEntryUsers_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetAllDataEntryUsers();

            Assert.NotNull(result);
            Assert.IsType<List<UserLookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllAuditorUsers_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetAllAuditorUsers();

            Assert.NotNull(result);
            Assert.IsType<List<UserLookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetUserBasedOnlistOfRoleIds_ReturnsData()
        {
            var _lstOfRoles = new List<int>() { };
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetUserBasedOnlistOfRoleIds(_lstOfRoles);

            Assert.NotNull(result);
            Assert.IsType<List<UserLookupModel>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUsersByRoleName_ReturnsData()
        {
            var _roleName = "";
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetUsersByRoleName(_roleName);

            Assert.NotNull(result);
            Assert.IsType<List<UserLookupModel>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCities_ReturnsData()
        {
            var result = await _lookupController.GetCities();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetBranchAddressTypes_ReturnsData()
        {
            var result = await _lookupController.GetBranchAddressTypes();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetBranchAddressTypesAndCommitteesByAgency_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetBranchAddressTypesAndCommitteesByAgency();

            Assert.NotNull(result);
            Assert.IsType<BranchModel>(result);
            Assert.NotEmpty(result.BranchAddressTypes);
        }

        [Fact]
        public async Task GetUnitModificationsTypes_ReturnsData()
        {
            var _agencyCode = "022001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency, "1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _lookupController = _lookupController.WithIdentity(_claims);

            var result = await _lookupController.GetUnitModificationsTypes();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetGovAgencies_ReturnsData()
        {

            var result = await _lookupController.GetGovAgencies();

            Assert.NotNull(result);
            Assert.IsType<List<GovAgencyModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetTenderTypes_ReturnsData()
        {

            var result = await _lookupController.GetTenderTypes();

            Assert.NotNull(result);
            Assert.IsType<List<TenderTypeModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CheckEstablishmentType_ReturnsData()
        {
            var _cR = "";
            var result = await _lookupController.CheckEstablishmentType(_cR);

            Assert.NotNull(result);
            Assert.IsType<LocalContentViewModel>(result);
        }

        [Fact]
        public async Task GetYearQuarters_ReturnsData()
        {

            var result = await _lookupController.GetYearQuarters();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetPrePlanningLookups_ReturnsData()
        {
            var result = await _lookupController.GetPrePlanningLookups();

            Assert.NotNull(result);
            Assert.IsType<PrePlanningLookUpsModels>(result);
        }

        [Fact]
        public async Task GetAllPrePlanningStatus_ReturnsData()
        {

            var result = await _lookupController.GetAllPrePlanningStatus();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetBookletCertificates_ReturnsData()
        {

            var result = await _lookupController.GetBookletCertificates();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetUserRoles_ReturnsData()
        {

            var result = await _lookupController.GetUserRoles();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetNotificationCategories_ReturnsData()
        {

            var result = await _lookupController.GetNotificationCategories();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }
    }
}
