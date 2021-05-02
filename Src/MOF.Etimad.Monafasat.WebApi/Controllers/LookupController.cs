using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.LocalContent;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("Api/[controller]")]
    public class LookupController : BaseController
    {
        private readonly ILookUpService lookupService;
        private readonly IIDMAppService _iDMAppService;
        private readonly IBranchAppService _branchAppService;
        private readonly IMapper mapper;
        private readonly IMemoryCache _cache;

        public LookupController(ILookUpService lookupService, IMapper mapper, IIDMAppService iDMAppService, IBranchAppService branchAppService, IMemoryCache cache, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            this.lookupService = lookupService;
            this.mapper = mapper;
            _iDMAppService = iDMAppService;
            _branchAppService = branchAppService;
            _cache = cache;
        }

        [AllowAnonymous]
        [HttpGet("GetRelationsStepLookups/{activityVersionId}")]
        public async Task<RelationsStepModels> GetRelationsStepLookups(int activityVersionId)
        {
            return await lookupService.GetRelationsStepLookups(activityVersionId);
        }
        [AllowAnonymous]
        [HttpGet("GetOfferOppeningLookups")]
        public async Task<OfferOpeningLookups> GetOfferOppeningLookups()
        {
            return await lookupService.GetOfferopeningLookups();
        }
        [HttpGet("GetAllTenderTypes")]
        public async Task<List<LookupModel>> GetAllTenderTypes()
        {
            return await lookupService.GetAllTenderTypes();
        }
        [HttpGet("GetAgencyPurchaseMethods")]
        public async Task<List<LookupModel>> GetAgencyPurchaseMethods()
        {
            string agencyCode = User.UserAgency();
            var result = await lookupService.GetAgencyPurchaseMethodsModels(agencyCode);
            return result;
        }
        [HttpGet("GetAllDirectPurchaseTenderReasons")]
        public async Task<List<LookupModel>> GetAllDirectPurchaseTenderReasons()
        {
            return await lookupService.GetAllDirectPurchaseTenderReasons();
        }

        [HttpGet("GetAllLimitedTenderReasons")]
        public async Task<List<LookupModel>> GetAllLimitedTenderReasons()
        {
            return await lookupService.GetAllLimitedTenderReasons();
        }

        [HttpGet("GetAllSpendingCategories")]
        public async Task<List<LookupModel>> GetAllSpendingCategories()
        {
            return await lookupService.GetAllSpendingCategories();
        }

        [HttpGet("GetAllCancelationReasons")]
        public async Task<List<LookupModel>> GetAllCancelationReasons()
        {
            return await lookupService.GetAllCancelationReasons();
        }

        [HttpGet("GetAllBuyerSuppliers/{tenderId}")]
        public async Task<List<LookupModel>> GetAllBuyerSuppliers(int tenderId)
        {
            List<LookupModel> result = new List<LookupModel>();
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer))
                result = await lookupService.GetAllBuyerSuppliers(tenderId);
            else
                result = await lookupService.GetAllSuppliersHaveOffers(tenderId);
            return result;
        }

        [HttpGet("GetAllSuppliersHaveOffers/{tenderId}")]
        public async Task<List<LookupModel>> GetAllSuppliersHaveOffers(int tenderId)
        {
            var result = await lookupService.GetAllSuppliersHaveOffers(tenderId);
            return result;
        }

        [HttpGet("GetAllSuppliersHasPrequalifications/{tenderId}")]
        public async Task<List<LookupModel>> GetAllSuppliersHasPrequalifications(int tenderId)
        {
            var result = await lookupService.GetAllSuppliersHasPrequalifications(tenderId);
            return result;
        }


        [HttpGet("GetAllPreQualifications")]
        public async Task<List<LookupModel>> GetAllPreQualifications()
        {
            string agencyCode = User.UserAgency();
            int branchId = User.UserBranch();
            var result = await lookupService.GetAllPreQualifications(0, agencyCode, branchId);
            return result;
        }


        [HttpGet("GetAllQuantityTableRowTypes")]
        public async Task<List<LookupModel>> GetAllQuantityTableRowTypes()
        {
            var result = await lookupService.GetAllQuantityTableRowTypes();
            return result;
        }
        [HttpGet("GetAllQuantityTableRowType")]
        public async Task<List<LookupModel>> GetAllQuantityTableRowType()
        {
            var result = await lookupService.GetAllQuantityTableRowTypes();
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetAreas")]
        public async Task<List<LookupModel>> GetAreas()
        {
            return await lookupService.FindAreas();
        }

        [Authorize(RoleNames.GetAllAgencyAnnouncementPolicy)]
        [HttpGet]
        [Route("GetAnnounementStatus")]
        public async Task<List<LookupModel>> GetAnnounementStatus()
        {
            var tenderStatusList = await lookupService.GetAnnounementStatusLookup();
            return tenderStatusList;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAnnouncementStatusSupplierTemplatesLookup")]
        public async Task<List<LookupModel>> GetAnnouncementStatusSupplierTemplatesLookup()
        {
            var announcementStatusList = await lookupService.GetAnnouncementStatusSupplierTemplatesLookup();
            return announcementStatusList;
        }

        [AllowAnonymous]
        [HttpGet("GetAreasforPreQualification")]
        public async Task<List<AreaModel>> GetAreasforPreQualification()
        {
            return (await lookupService.FindAreas()).Select(d => new AreaModel { AreaId = d.Id, NameAr = d.Name }).ToList();
        }

        [AllowAnonymous]
        [HttpGet("GetCountries")]
        public async Task<List<CountryModel>> GetCountries()
        {
            return await lookupService.FindCountries();

        }

        [AllowAnonymous]
        [HttpGet("GetCountryById")]
        public async Task<CountryModel> GetCountryById(int countryId)
        {
            return await lookupService.FindCountryById(countryId);
        }

        [AllowAnonymous]
        [HttpGet("GetActivities")]
        public async Task<List<ActivityModel>> GetActivities()
        {
            return await lookupService.FindActivities();
        }

        [AllowAnonymous]
        [HttpGet("GetMainActivities")]
        public async Task<List<ActivityModel>> GetMainActivities()
        {
            return await lookupService.GetMainActivities();
        }
        [AllowAnonymous]
        [HttpGet("GetMainActivitiesByParentId/{parentId}")]
        public async Task<List<LookupModel>> GetMainActivitiesByParentId(int? parentId = 0)
        {
            return await lookupService.GetMainActivitiesByParentId(parentId);
        }
        [HttpGet("GetNegotiationTypes")]
        public async Task<List<LookupModel>> GetNegotiationTypes()
        {
            var result = await lookupService.GetNegotiationTypes();
            return result;
        }
        [HttpGet("GetReductionReasons")]
        public async Task<List<LookupModel>> GetReductionReasons()
        {
            var result = await lookupService.GetReductionReasons();
            return result;
        }
        [AllowAnonymous]
        [HttpGet("GetConstructionWorks")]
        public async Task<List<ConstructionWorkModel>> GetConstructionWorks()
        {
            return await lookupService.FindConstructionWorks();
        }
        [AllowAnonymous]
        [HttpGet("GetMaintenanceRunningWorks")]
        public async Task<List<MaintenanceRunningWorkModel>> GetMaintenanceRunningWorks()
        {
            return await lookupService.FindMaintenanceRunningWorks();
        }

        [AllowAnonymous]
        [HttpGet("GetBanks")]
        public async Task<List<BankModel>> GetBanks()
        {
            return await lookupService.FindBanks();
        }
        [HttpGet("GetAllCommittesByAgency")]
        public async Task<CommitteesModel> GetAllCommittesByAgency()
        {
            string agencyCode = User.UserAgency();
            return await lookupService.GetAllCommittesByAgency(agencyCode);
        }
        [HttpGet("GetTechnicalCommitteeList")]
        public async Task<List<LookupModel>> GetTechnicalCommitteeList()
        {
            string agencyCode = User.UserAgency();
            int userId = User.UserId();
            return await lookupService.FindTechnicalCommitteeByAgencyCode(agencyCode, userId);
        }

        [HttpGet("GetTechnicalCommitteeListOnBranchTender/{tenderId}")]
        public async Task<List<LookupModel>> GetTechnicalCommitteeListOnBranchTender(int tenderId)
        {
            int userId = User.UserId();
            string agencyCode = User.UserAgency();
            List<LookupModel> committeeList = await lookupService.FindTechnicalCommitteeByBranchIdAndTender(User.SelectedCommittee(), userId, agencyCode, tenderId);
            return committeeList;
        }

        [AllowAnonymous]
        [HttpGet("GetALlAgencies")]
        public async Task<List<GovAgencyModel>> GetAllAgenceies()
        {
            return await _iDMAppService.GetALlAgencies();
        }
        [AllowAnonymous]
        [HttpGet("GetAllBranchesByAgencyCode/{agency}")]
        public async Task<List<LookupModel>> GetAllBranchesByAgencyCode(string agency)
        {
            string agencyCode = string.Empty;
            if (!User.IsInRole(RoleNames.CustomerService) && !User.IsInRole(RoleNames.MonafasatAccountManager) && !User.IsInRole(RoleNames.FinancialSupervisor))
            {
                agencyCode = !string.IsNullOrEmpty(agency) ? agency : User.UserAgency();
            }
            else
            {
                agencyCode = agency;
            }
            List<LookupModel> branches = await _branchAppService.GetAllBranchesByAgencyCode(agencyCode);
            return branches;
        }
        [AllowAnonymous]
        [HttpGet("GetAllBranches")]
        public async Task<List<LookupModel>> GetAllBranches()
        {
            List<LookupModel> branches = await _branchAppService.GetAllBranchesByAgencyCode("");
            return branches;
        }

        [AllowAnonymous]
        [HttpGet("GetAllBranchesNotAssignedToCommittee/{committeeId}/{committeeType}")]
        public async Task<List<LookupModel>> GetAllBranchesNotAssignedToCommittee(int committeeId, int committeeType)
        {
            string agencyCode = User.UserAgency();
            List<LookupModel> branches = await _branchAppService.GetAllBranchesNotAssignedToCommittee(committeeId, committeeType, agencyCode);
            return branches;
        }

        [HttpGet("GetAllDataEntryUsers")]
        public async Task<List<UserLookupModel>> GetAllDataEntryUsers()
        {
            var AgencyCode = User.UserAgency();
            if (User.UserIsVRO())
            {
                return await _iDMAppService.GetAllDataEntryAndAuditorUsers(RoleNames.PurshaseSpecialist, AgencyCode);
            }
            else
            {
                return await _iDMAppService.GetAllDataEntryAndAuditorUsers(RoleNames.DataEntry, AgencyCode);
            }

        }


        [HttpGet("GetAllAuditorUsers")]
        public async Task<List<UserLookupModel>> GetAllAuditorUsers()
        {
            var AgencyCode = User.UserAgency();

            if (User.UserIsVRO())
            {
                return await _iDMAppService.GetAllDataEntryAndAuditorUsers(RoleNames.EtimadOfficer, AgencyCode);
            }
            else
            {
                return await _iDMAppService.GetAllDataEntryAndAuditorUsers(RoleNames.Auditer, AgencyCode);
            }

        }


        [HttpGet("GetUserBasedOnlistOfRoleIds")]
        public async Task<List<UserLookupModel>> GetUserBasedOnlistOfRoleIds(List<int> lstOfRoles)
        {
            var AgencyCode = User.UserAgency();
            return await _iDMAppService.GetUserBasedOnlistOfRoleIds(lstOfRoles, AgencyCode);
        }


        [HttpGet("GetUsersByRoleName/{roleName}")]
        public async Task<List<UserLookupModel>> GetUsersByRoleName(string roleName)
        {
            string agencyCode = User.UserAgency();
            var tenderStatusList = await _iDMAppService.FindUsersByRoleNameAndAgencyCodeForCommitteeAssignAsync(roleName, agencyCode);
            return tenderStatusList;
        }

        [HttpGet("GetUsersNotAssignedToCommitteeByRoleName/{roleName}/{committeeId}")]
        public async Task<List<UserLookupModel>> GetUsersNotAssignedToCommitteeByRoleName(string roleName, int committeeId)
        {
            UsersSearchCriteriaModel criteria = new UsersSearchCriteriaModel();
            criteria = GetUserAgencyTypeAndIdWithFlags(criteria);
            var usersForCommittee = await _iDMAppService.FindUsersByRoleNameAndAgencyCodeNotAssignedToCommitteeAssignAsync(roleName, criteria.AgencyId, committeeId, (AgencyType)criteria.AgencyType);
            return usersForCommittee;
        }

        [HttpGet("GetUsersbyCommitteeTypeId/{committeeTypeId}/{committeeId}")]
        public async Task<List<UserLookupModel>> GetUsersbyCommitteeTypeId(int committeeTypeId, int committeeId)
        {
            UsersSearchCriteriaModel userSearchCriteriaModel = new UsersSearchCriteriaModel();
            userSearchCriteriaModel = GetUserAgencyTypeAndIdWithFlags(userSearchCriteriaModel);
            var usersForCommitteeTypeId = await _iDMAppService.GetUsersbyCommitteeTypeId(userSearchCriteriaModel.AgencyId, committeeId, committeeTypeId, "", "", userSearchCriteriaModel.AgencyType);
            return usersForCommitteeTypeId;
        }

        private UsersSearchCriteriaModel GetUserAgencyTypeAndIdWithFlags(UsersSearchCriteriaModel userSearchCriteriaModel)
        {
            if (User.IsInRole(RoleNames.MonafasatAdmin))
            {
                userSearchCriteriaModel.isGovVendor = User.UserIsGovVendor();
                userSearchCriteriaModel.isSemiGovAgency = User.UserIsSemiGovAgency();
                userSearchCriteriaModel.AgencyType = userSearchCriteriaModel.isSemiGovAgency ? (int)AgencyType.SemiGovAgency : (userSearchCriteriaModel.isGovVendor ? (int)AgencyType.GovVendor : (int)AgencyType.Agency);
                userSearchCriteriaModel.AgencyId = User.UserAgency();
            }
            else
            {
                userSearchCriteriaModel.AgencyType = (int)AgencyType.None;
            }
            return userSearchCriteriaModel;
        }


        [HttpGet]
        [Route("GetCities")]
        public async Task<List<LookupModel>> GetCities()
        {
            return await lookupService.GetCities();
        }

        [HttpGet("GetBranchAddressTypes")]
        public async Task<List<LookupModel>> GetBranchAddressTypes()
        {
            return await lookupService.GetBranchAddressTypes();
        }
        [HttpGet("GetBranchAddressTypesAndCommitteesByAgency")]
        public async Task<BranchModel> GetBranchAddressTypesAndCommitteesByAgency()
        {
            string agencyCode = User.UserAgency();
            var committees = await lookupService.GetAllCommittesByAgency(agencyCode);
            var branchTypes = await lookupService.GetBranchAddressTypes();
            var branchModel = new BranchModel { BranchAddressTypes = branchTypes, Committiees = committees };
            return branchModel;
        }

        [HttpGet("GetUnitModificationsTypes")]
        public async Task<List<LookupModel>> GetUnitModificationsTypes()
        {
            var result = await lookupService.GetUnitModificationsTypes();
            return result;
        }

        [HttpGet]
        [Route("GetGovAgencies")]
        public async Task<List<GovAgencyModel>> GetGovAgencies()
        {
            return await _iDMAppService.GetALlAgencies();
        }

        [HttpGet]
        [Route("GetTenderTypes")]
        public async Task<List<TenderTypeModel>> GetTenderTypes()
        {
            return await lookupService.GetTenderTypes();
        }

        [AllowAnonymous]
        [HttpGet("CheckEstablishmentType/{CR}")]
        public async Task<LocalContentViewModel> CheckEstablishmentType(string CR)
        {
            return await lookupService.CheckEstablishmentType(CR);
        }

        [AllowAnonymous]
        [HttpGet("GetYearQuarters")]
        public async Task<List<LookupModel>> GetYearQuarters()
        {
            return await lookupService.GetAllYearQuarters();
        }

        [AllowAnonymous]
        [HttpGet("GetPrePlanningLookups")]
        public async Task<PrePlanningLookUpsModels> GetPrePlanningLookups()
        {
            return await lookupService.GetPrePlanningLookups();
        }
        [AllowAnonymous]
        [HttpGet("GetAllPrePlanningStatus")]
        public async Task<List<LookupModel>> GetAllPrePlanningStatus()
        {
            return await lookupService.GetAllPrePlanningStatus();
        }

        [AllowAnonymous]
        [HttpGet("GetBookletCertificates")]
        public async Task<List<LookupModel>> GetBookletCertificates()
        {
            return await lookupService.GetAllBookletCertificates();
        }

        [AllowAnonymous]
        [HttpGet("GetLocalContentMechanism")]
        public async Task<List<LookupModel>> GetLocalContentMechanism()
        {
            return await lookupService.GetAllLocalContentMechanism();
        }


        [AllowAnonymous]
        [HttpGet("GetUserRoles")]
        public async Task<List<LookupModel>> GetUserRoles()
        {
            List<LookupModel> list = await lookupService.GetAllRoles();
            return list;
        }

        [AllowAnonymous]
        [HttpGet("GetNotificationCategories")]
        public async Task<List<LookupModel>> GetNotificationCategories()
        {
            List<LookupModel> list = await lookupService.GetAllOperationCodesCategoies();
            return list;
        }
    }
}
