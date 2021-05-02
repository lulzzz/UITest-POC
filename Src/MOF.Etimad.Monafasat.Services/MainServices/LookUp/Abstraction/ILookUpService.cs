using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.LocalContent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ILookUpService
    {
        Task<PrePlanningLookUpsModels> GetPrePlanningLookups();
        Task<List<LookupModel>> GetAllYearQuarters();
        Task<List<LookupModel>> GetAllPrePlanningStatus();
        Task<List<LookupModel>> GetAllTenderTypes();
        Task<List<LookupModel>> GetAgencyPurchaseMethodsModels(string agencyCode);
        Task<List<LookupModel>> GetAllDirectPurchaseTenderReasons();
        Task<List<LookupModel>> GetAllLimitedTenderReasons();
        Task<List<LookupModel>> GetAllSpendingCategories();
        Task<List<LookupModel>> GetAllCancelationReasons();
        Task<List<LookupModel>> GetAllBuyerSuppliers(int tenderId);
        Task<List<LookupModel>> GetAllSuppliersHasPrequalifications(int preQualificationId);
        Task<List<LookupModel>> GetAllSuppliersHaveOffers(int tenderId);
        Task<List<LookupModel>> GetAllPreQualifications(int tenderId, string agencyCode, int branchId);
        Task<List<LookupModel>> GetSuccededPreQualificationsForTenderCreation(int branchId);
        Task<List<LookupModel>> GetFinishedAnnouncementHasOneSupplier(int tenderId, string agencyCode, int branchId);
        Task<List<LookupModel>> GetFinishedAnnouncementForLimitedTender(int tenderId, string agencyCode, int branchId);
        Task<List<LookupModel>> GetAnnouncementSupplierTemplateForLimitedTender(int tenderId, string agencyCode, int selectedReasonId);
        Task<List<LookupModel>> GetAnnouncementSupplierTemplateForDirectPurchaseTender(string agencyCode);
        Task<List<LookupModel>> GetAllQuantityTableRowTypes();
        Task<RelationsStepModels> GetRelationsStepLookups(int activityVersionId);
        Task<List<LookupModel>> FindAreas();
        Task<List<CountryModel>> FindCountries();
        Task<List<LookupModel>> FindCountriesLookup();
        Task<CountryModel> FindCountryById(int countryId);
        Task<List<ActivityModel>> FindActivities();
        Task<List<ActivityModel>> GetMainActivities();
        Task<List<LookupModel>> GetMainActivitiesByParentId(int? ParentId = null);
        Task<List<LookupModel>> GetNegotiationTypes();
        Task<List<LookupModel>> GetReductionReasons();
        Task<List<ConstructionWorkModel>> FindConstructionWorks();
        Task<List<MaintenanceRunningWorkModel>> FindMaintenanceRunningWorks();

        Task<List<LookupModel>> GetTenderStatusLookup();
        Task<List<LookupModel>> GetVROTenderStatusLookup();
        Task<List<LookupModel>> GetQualificationStatusLookup();
        Task<List<LookupModel>> GetAnnounementStatusLookup();
        Task<List<LookupModel>> GetApprovedTenderStatusLookup();
        Task<List<LookupModel>> GetAllBookletCertificates();

        Task<List<LookupModel>> GetAllLocalContentMechanism();

        
        Task<List<LookupModel>> GetAllRoles();
        Task<List<LookupModel>> GetAllOperationCodesCategoies();
        Task<List<BankModel>> FindBanks();
        Task<CommitteesModel> GetAllCommittesByAgency(string agencyCode);
        Task<List<LookupModel>> FindTechnicalCommitteeByAgencyCode(string agencyCode, int userId);
        Task<List<LookupModel>> FindTechnicalCommitteeByBranchIdAndTender(int committeeId, int userId, string agencyCode, int tenderId);
        Task<List<LookupModel>> GetCities();
        Task<List<LookupModel>> GetBranchAddressTypes();
        Task<List<TenderTypeModel>> GetTenderTypes();
        Task<OfferOpeningLookups> GetOfferopeningLookups();

        Task<LocalContentViewModel> CheckEstablishmentType(string CR);
        Task<List<LookupModel>> GetAnnouncementStatusSupplierTemplatesLookup();

        #region Unit
        Task<List<LookupModel>> GetUnitModificationsTypes();
        #endregion
    }
}