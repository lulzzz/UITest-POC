using AutoMapper;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.LocalContent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class LookUpService : ILookUpService
    {
        private readonly ILookUpServiceQueries _lookupServiceQueries;

        private readonly IMapper _mapper;
        private readonly IGenericCommandRepository _genericrepository;
        private readonly IIDMAppService _idmAppService;
        private readonly ISMEASizeInquiryProxy _sMEASizeInquiryProxy;

        public LookUpService(ILookUpServiceQueries lookupServiceQueries, IMapper mapper, IGenericCommandRepository genericrepository, IIDMAppService idmAppService, ISMEASizeInquiryProxy sMEASizeInquiryProxy)
        {
            _mapper = mapper;
            _lookupServiceQueries = lookupServiceQueries;
            _genericrepository = genericrepository;
            _idmAppService = idmAppService;
            _sMEASizeInquiryProxy = sMEASizeInquiryProxy;
        }

        #region Service Query======================================================

        public async Task<List<CountryModel>> FindCountries()
        {
            return await _lookupServiceQueries.FindCountries();
        }
        public async Task<List<LookupModel>> FindCountriesLookup()
        {
            return await _lookupServiceQueries.FindCountriesLookup();
        }
        public async Task<CountryModel> FindCountryById(int countryId)
        {
            return await _lookupServiceQueries.FindCountryById(countryId);
        }

        public async Task<List<LookupModel>> GetAllTenderTypes()
        {

            return await _lookupServiceQueries.GetAllTenderTypes();
        }

        public async Task<List<LookupModel>> GetAgencyPurchaseMethodsModels(string agencyCode)
        {

            return await _lookupServiceQueries.GetAgencyPurchaseMethodsModels(agencyCode);
        }
        public async Task<List<LookupModel>> GetAllDirectPurchaseTenderReasons()
        {

            return await _lookupServiceQueries.GetAllDirectPurchaseTenderReasons();
        }
        public async Task<List<LookupModel>> GetAllLimitedTenderReasons()
        {

            return await _lookupServiceQueries.GetAllLimitedTenderReasons();
        }
        public async Task<List<LookupModel>> GetAllSpendingCategories()
        {
            return await _lookupServiceQueries.GetAllSpendingCategories();
        }
        public async Task<List<LookupModel>> GetAllCancelationReasons()
        {
            return await _lookupServiceQueries.GetAllCancelationReasons();
        }
        public async Task<List<LookupModel>> GetAllBuyerSuppliers(int tenderId)
        {
            return await _lookupServiceQueries.GetAllBuyerSuppliers(tenderId);
        }
        public async Task<List<LookupModel>> GetAllSuppliersHasPrequalifications(int preQualificationId)
        {
            return await _lookupServiceQueries.GetAllSuppliersHasPrequalifications(preQualificationId);
        }
        public async Task<List<LookupModel>> GetAllSuppliersHaveOffers(int tenderId)
        {
            return await _lookupServiceQueries.GetAllSuppliersHaveOffers(tenderId);
        }

        public async Task<List<LookupModel>> GetAllPreQualifications(int tenderId, string agencyCode, int branchId)
        {
            return await _lookupServiceQueries.GetAllPreQualifications(tenderId, agencyCode, branchId);
        }

        public async Task<List<LookupModel>> GetSuccededPreQualificationsForTenderCreation(int branchId)
        {
            return await _lookupServiceQueries.GetSuccededPreQualificationsForTenderCreation(branchId);
        }
        public async Task<List<LookupModel>> GetFinishedAnnouncementHasOneSupplier(int tenderId, string agencyCode, int branchId)
        {
            return await _lookupServiceQueries.GetFinishedAnnouncementHasOneSupplier(tenderId, agencyCode, branchId);
        }
        public async Task<List<LookupModel>> GetFinishedAnnouncementForLimitedTender(int tenderId, string agencyCode, int branchId)
        {
            return await _lookupServiceQueries.GetFinishedAnnouncementForLimitedTender(tenderId, agencyCode, branchId);
        }
        public async Task<List<LookupModel>> GetAnnouncementSupplierTemplateForLimitedTender(int tenderId, string agencyCode, int selectedReasonId)
        {
            return await _lookupServiceQueries.GetAnnouncementSupplierTemplateForLimitedTender(tenderId, agencyCode, selectedReasonId);
        }
        public async Task<List<LookupModel>> GetAnnouncementSupplierTemplateForDirectPurchaseTender(string agencyCode)
        {
            return await _lookupServiceQueries.GetAnnouncementSupplierTemplateForDirectPurchaseTender(agencyCode);
        }

        public async Task<List<LookupModel>> GetAllQuantityTableRowTypes()
        {
            return await _lookupServiceQueries.GetAllQuantityTableRowTypes();
        }
        public async Task<List<LookupModel>> FindAreas()
        {
            return await _lookupServiceQueries.FindAreas();
        }
        public delegate T Del<T>();
        public async Task<RelationsStepModels> GetRelationsStepLookups(int activityVersionId)
        {
            RelationsStepModels model = new RelationsStepModels();

            model.Areas = await FindAreas();
            model.Countries = await FindCountriesLookup();
            model.Activities = await FindActivitiesWithVersion(activityVersionId);
            model.RunningWorks = await FindMaintenanceRunningWorks();
            model.ConstructionWorks = await FindConstructionWorks(); 
            model.ActivitiesWithYears = await FindActivitiesWithYearsWithVersion(activityVersionId); return model;
        }

        public async Task<OfferOpeningLookups> GetOfferopeningLookups()
        {
            OfferOpeningLookups model = new OfferOpeningLookups();
            model.Banks = await FindBanks();
            model.RunningWorks = await FindMaintenanceRunningWorks();
            model.ConstructionWorks = await FindConstructionWorks();
            return model;
        }

        public async Task<PrePlanningLookUpsModels> GetPrePlanningLookups()
        {
            PrePlanningLookUpsModels model = new PrePlanningLookUpsModels();
            model.ProjectTypes = await FindActivities();
            model.Statuses = await GetAllPrePlanningStatus();
            model.YearQuarters = await GetAllYearQuarters();
            model.Agecies = await _idmAppService.GetALlAgencies();
            model.Areas = await _lookupServiceQueries.FindAreas();
            model.Countries = await _lookupServiceQueries.FindCountries();
            return model;
        }

        public async Task<List<BankModel>> FindBanks()
        {
            return await _lookupServiceQueries.FindBanks();
        }

        public async Task<List<ConstructionWorkModel>> FindConstructionWorks()
        {
            return await _lookupServiceQueries.FindConstructionWorks();
        }
        public async Task<List<int>> FindActivitiesWithYears()
        {
            return await _lookupServiceQueries.FindActivitiesWithYears();
        }
        public async Task<List<int>> FindActivitiesWithYearsWithVersion(int activityVersionId)
        {
            return await _lookupServiceQueries.FindActivitiesWithYearsWithVersion(activityVersionId);
        }

        public async Task<List<MaintenanceRunningWorkModel>> FindMaintenanceRunningWorks()
        {

            return await _lookupServiceQueries.FindMentainanceWorks();
        }


        public async Task<List<ActivityModel>> FindActivities()
        {
            return await _lookupServiceQueries.FindActivities();
        }

        public async Task<List<ActivityModel>> FindActivitiesWithVersion(int activityVersionId)
        {
            return await _lookupServiceQueries.FindActivitiesWithVersion(activityVersionId);
        }

        public async Task<List<ActivityModel>> GetMainActivities()
        {
            return await _lookupServiceQueries.GetMainActivities();
        }
        public async Task<List<LookupModel>> GetMainActivitiesByParentId(int? ParentId = null)
        {
            return await _lookupServiceQueries.GetMainActivitiesByParentId(ParentId);
        }

        #endregion

        public async Task<List<LookupModel>> GetTenderStatusLookup()
        {
            return await _lookupServiceQueries.GetTenderStatusLookup();
        }
        public async Task<List<LookupModel>> GetVROTenderStatusLookup()
        {
            return await _lookupServiceQueries.GetVROTenderStatusLookup();
        }
        public async Task<List<LookupModel>> GetAllYearQuarters()
        {
            return await _lookupServiceQueries.GetAllYearQuarters();
        }

        public async Task<List<LookupModel>> GetAllPrePlanningStatus()
        {
            return await _lookupServiceQueries.GetAllPrePlanningStatus();
        }
        public async Task<List<LookupModel>> GetAllBookletCertificates()
        {
            return await _lookupServiceQueries.GetAllBookletCertificates();
        }

        public async Task<List<LookupModel>> GetAllLocalContentMechanism()
        {
            return await _lookupServiceQueries.GetAllLocalContentMechanismPreference();
        }

        
        public async Task<List<LookupModel>> GetAllRoles()
        {
            return await _lookupServiceQueries.GetAllRoles();
        }
        public async Task<List<LookupModel>> GetAllOperationCodesCategoies()
        {
            return await _lookupServiceQueries.GetAllOperationCodesCategoies();
        }
        public async Task<List<LookupModel>> GetQualificationStatusLookup()
        {
            return await _lookupServiceQueries.GetQualificationStatusLookup();
        }
        public async Task<List<LookupModel>> GetAnnounementStatusLookup()
        {
            return await _lookupServiceQueries.GetAnnounementStatusLookup();
        }
        public async Task<List<LookupModel>> GetAnnouncementStatusSupplierTemplatesLookup()
        {
            return await _lookupServiceQueries.GetAnnouncementStatusSupplierTemplatesLookup();
        }
        public async Task<List<LookupModel>> GetApprovedTenderStatusLookup()
        {
            return await _lookupServiceQueries.GetApprovedTenderStatusLookup();
        }

        public async Task<List<LookupModel>> FindTechnicalCommitteeByAgencyCode(string agencyCode, int userId)
        {
            return await _lookupServiceQueries.FindTechnicalCommitteeByAgencyCode(agencyCode, userId);
        }
        public async Task<List<LookupModel>> GetReductionReasons()
        {
            return await _lookupServiceQueries.GetReductionReasons();
        }
        public async Task<List<LookupModel>> GetNegotiationTypes()
        {

            return await _lookupServiceQueries.GetNegotiationTypes();
        }
        public async Task<CommitteesModel> GetAllCommittesByAgency(string agencyCode)
        {
            var CommitteesModel = new CommitteesModel();
            var allCommittees = await _lookupServiceQueries.GetAllCommittesByAgency(agencyCode);
            CommitteesModel.TechnicalCommittees = allCommittees.Where(d => d.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee && d.IsActive == true).Select(d => new LookupModel { Id = d.CommitteeId, Name = d.CommitteeName }).ToList();
            CommitteesModel.CheckCommittees = allCommittees.Where(d => d.CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee && d.IsActive == true).Select(d => new LookupModel { Id = d.CommitteeId, Name = d.CommitteeName }).ToList();
            CommitteesModel.OpenCommittees = allCommittees.Where(d => d.CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee && d.IsActive == true).Select(d => new LookupModel { Id = d.CommitteeId, Name = d.CommitteeName }).ToList();

            CommitteesModel.purchaseCommittees = allCommittees.Where(d => d.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee && d.IsActive == true).Select(d => new LookupModel { Id = d.CommitteeId, Name = d.CommitteeName }).ToList();
            CommitteesModel.preQualificationCommittees = allCommittees.Where(d => d.CommitteeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee && d.IsActive == true).Select(d => new LookupModel { Id = d.CommitteeId, Name = d.CommitteeName }).ToList();
            CommitteesModel.VROCommittee = allCommittees.Where(d => d.CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee && d.IsActive == true).Select(d => new LookupModel { Id = d.CommitteeId, Name = d.CommitteeName }).ToList();
            return CommitteesModel;
        }

        public async Task<List<LookupModel>> FindTechnicalCommitteeByBranchIdAndTender(int committeeId, int userId, string agencyCode, int tenderId)
        {
            Tender tender = await _lookupServiceQueries.FindTenderById(tenderId);
            List<LookupModel> lookupModels = await _lookupServiceQueries.FindTechnicalCommitteeByBranchIdAndCommiteeId(tender.BranchId, committeeId);
            if (lookupModels.Count == 0)
            {
                lookupModels = await _lookupServiceQueries.FindTechnicalCommitteeByAgencyCodeAndCommitteeId(agencyCode, committeeId);
            }
            return lookupModels;
        }


        public async Task<List<LookupModel>> GetCities()
        {
            return await _lookupServiceQueries.GetCities();
        }
        public async Task<List<LookupModel>> GetBranchAddressTypes()
        {
            var lst = await _lookupServiceQueries.GetBranchAddressTypes();
            return lst.Where(s => s.Id != (int)Enums.BranchAddressType.MainBranchAddress).ToList();
        }
        public async Task<List<TenderTypeModel>> GetTenderTypes()
        {
            return await _lookupServiceQueries.GetTenderTypes();
        }

        public async Task<LocalContentViewModel> CheckEstablishmentType(string CR)
        {
            return await _sMEASizeInquiryProxy.SMEASizeInquiry(CR);
        }


        #region Unit
        public async Task<List<LookupModel>> GetUnitModificationsTypes()
        {
            return await _lookupServiceQueries.GetUnitModificationsTypes();
        }
        #endregion
    }
}
