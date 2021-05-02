using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ILookUpServiceQueries
    {
        Task<List<LookupModel>> FindAreas();
        Task<List<CountryModel>> FindCountries();
        Task<List<LookupModel>> FindCountriesLookup();
        Task<CountryModel> FindCountryById(int countryId);
        Task<List<ActivityModel>> FindActivities();
        Task<List<ActivityModel>> FindActivitiesWithVersion(int activityVersionId);
        Task<List<LookupModel>> GetAllTenderTypes();
        Task<List<LookupModel>> GetAgencyPurchaseMethodsModels(string agencyCode);
        Task<List<LookupModel>> GetAllPrePlanningStatus();
        Task<List<LookupModel>> GetAllBookletCertificates();

        Task<List<LookupModel>> GetAllLocalContentMechanismPreference();

        
        Task<List<LookupModel>> GetAllRoles();
        Task<List<LookupModel>> GetAllOperationCodesCategoies();
        Task<List<LookupModel>> GetAllLimitedTenderReasons();
        Task<List<LookupModel>> GetAllSpendingCategories();
        Task<List<LookupModel>> GetAllCancelationReasons();
        Task<List<LookupModel>> GetAllBuyerSuppliers(int tenderId);
        Task<List<LookupModel>> GetAllSuppliersHasPrequalifications(int preQualificationId);
        Task<List<LookupModel>> GetAllSuppliersHaveOffers(int tenderId);
        Task<List<LookupModel>> GetAllDirectPurchaseTenderReasons();
        Task<List<LookupModel>> GetAllPreQualifications(int tenderId, string agencyCode, int branchId);
        Task<List<LookupModel>> GetSuccededPreQualificationsForTenderCreation(int branchId);

        Task<List<LookupModel>> GetFinishedAnnouncementHasOneSupplier(int tenderId, string agencyCode, int branchId);
        Task<List<LookupModel>> GetFinishedAnnouncementForLimitedTender(int tenderId, string agencyCode, int branchId);
        Task<List<LookupModel>> GetAnnouncementSupplierTemplateForLimitedTender(int tenderId, string agencyCode, int selectedReasonId);
        Task<List<LookupModel>> GetAnnouncementSupplierTemplateForDirectPurchaseTender(string agencyCode);
        Task<List<LookupModel>> GetAllQuantityTableRowTypes();
        Task<List<LookupModel>> GetAllYearQuarters();
        Task<List<ActivityModel>> GetMainActivities();
        Task<List<LookupModel>> GetMainActivitiesByParentId(int? parentId = null);
        Task<List<ConstructionWorkModel>> FindConstructionWorks();
        Task<List<MaintenanceRunningWorkModel>> FindMentainanceWorks();
        Task<List<int>> FindActivitiesWithYears();
        Task<List<int>> FindActivitiesWithYearsWithVersion(int activityVersionId);
        Task<List<BankModel>> FindBanks();
        Task<List<LookupModel>> GetTenderStatusLookup();
        Task<List<LookupModel>> GetVROTenderStatusLookup();
        Task<List<LookupModel>> GetQualificationStatusLookup();
        Task<List<LookupModel>> GetAnnounementStatusLookup();
        Task<List<LookupModel>> GetApprovedTenderStatusLookup();
        Task<List<LookupModel>> FindTechnicalCommitteeByAgencyCode(string agencyCode, int userId);
        Task<List<LookupModel>> GetNegotiationTypes();
        Task<List<LookupModel>> GetReductionReasons();
        Task<List<LookupModel>> FindTechnicalCommitteeByAgencyCodeAndCommitteeId(string agencyCode, int committeeId);
        Task<List<Committee>> GetAllCommittesByAgency(string agencyCode);

        Task<List<LookupModel>> FindTechnicalCommitteeByBranchIdAndCommiteeId(int branchId, int committeeId);
        Task<Tender> FindTenderById(int tenderId);
        Task<List<LookupModel>> GetCities();
        Task<List<LookupModel>> GetBranchAddressTypes();
        Task<List<TenderTypeModel>> GetTenderTypes();
        Task<List<LookupModel>> GetAnnouncementStatusSupplierTemplatesLookup();

        #region Unit
        Task<List<LookupModel>> GetUnitModificationsTypes();
        #endregion
    }
}
