using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IReportsService
    {
        Task<List<TenderValueToTypesModel>> GetTendersForTenderToTypesReport(TenderValueToTypeSearchCriteria searchCriteria);
        Task<List<string>> FinancialYear();
        Task<Dictionary<int, string>> GetTendersName();
        Task<Dictionary<int, string>> GetTenderStatuses();
        Task<QueryResult<MessagesModel>> GetMessagesStatusReportAsync(SearchCriteria searchCriteria);

        Task<QueryResult<QualificationModel>> GetQualificationReport(TenderValueToTypeSearchCriteria searchCriteria);
        Task<List<TenderValueToTypesModel>> GetTendersCountForTenderToTypesReport(TenderValueToTypeSearchCriteria searchCriteria);
        Task<List<TenderValueToTypesModel>> GetTenderForTenderToAwardedSuppliersReport(TenderValueToTypeSearchCriteria searchCriteria);
        Task<List<TenderValueToTypesModel>> ReportTendersToActivity(TenderValueToTypeSearchCriteria searchCriteria);
        Task<QueryResult<DirectPurchaseModel>> GetDirectPurchaseReport(TenderValueToTypeSearchCriteria searchCriteria);
        Task<QueryResult<AmountOfSavingsViewModel>> GetAmountOfSavingsAsync(TenderValueToTypeSearchCriteria searchCriteria);
        Task<TotalAmountOfSavingsViewModel> GetTotalAmountOfSavingsAsync(TenderValueToTypeSearchCriteria searchCriteria);


        Task<List<MostTendersActivitiesModel>> ReportGetMostTendersActivitiesAsync(TenderValueToTypeSearchCriteria criteria);
        Task<List<QualificationCountModel>> QualificationCountAsync(TenderValueToTypeSearchCriteria searchCriteria);
        Task<QueryResult<QualificationCountModel>> QualificationCountListAsync(TenderValueToTypeSearchCriteria searchCriteria);



        Task<List<MostTendersActivitiesModel>> ReportGetMostSuppliersActivitiesAsync(TenderValueToTypeSearchCriteria criteria);
        Task<List<MostSuppliersHaveTendersModel>> ReportGetMostSuppliersHaveTendersAsync(TenderValueToTypeSearchCriteria criteria);
        Task<List<TendersSalesModel>> ReportGetTendersSalesAsync(TenderValueToTypeSearchCriteria criteria);
        Task<List<TendersPublishingModel>> ReportGetPublishedTendersAsync(TenderValueToTypeSearchCriteria criteria);
        Task<List<TenderCountsModel>> ReportGetTendersCountReportAsync(TenderSearchCriteria criteria);
        Task<QueryResult<Tender>> GetTendersReportList(TenderSearchCriteria criteria);
        Task<QueryResult<TenderCountsModel>> GetTendersCountReportList(TenderSearchCriteria criteria);
        int GetTendersCountReportsCount(TenderSearchCriteria criteria);
        Task<QueryResult<PurchaseModel>> GetTendersPurshasesReportList(TenderSearchCriteria criteria);
        Task<decimal> GetTenderPurshasesReportTotalAmount(TenderSearchCriteria criteria);

        Task<QueryResult<MostSuppliersHaveTendersModel>> ReportGetMostSuppliersHaveTendersListAsync(TenderValueToTypeSearchCriteria criteria);
        Task<QueryResult<NotifyModel>> GetDailyNotificationsList(NotifySearchCriteria criteria);
        Task<QueryResult<TendersSummryReportViewModel>> GetTendersSummaryAsync(TenderValueToTypeSearchCriteria searchCriteria);
        Task<TendersSummryReportCountsViewModel> GetTendersSummaryCountsAsync(TenderValueToTypeSearchCriteria searchCriteria);
        Task<QueryResult<AgencyFileViewModel>> GetAgencyFileAsync(TenderValueToTypeSearchCriteria searchCriteria);
         
        Task<AgencyTenderStatisticsModel> ReportGetAgencyStatisticsAsync(TenderValueToTypeSearchCriteria criteria);
        Task<QueryResult<Tender>> FindSuppliersPurshaseReport(TenderSearchCriteria criteria);
        Task<List<DirectInvitationsReportModel>> DirectInvitationsReport(TenderValueToTypeSearchCriteria criteria);

        #region TenderMonthlySalesReceipteReport
        Task<TenderSalesMonthlyCountsPerMonth> GetTenderSalesMonthlyCountsPerMonth(TenderValueToTypeSearchCriteria searchCriteria);
        Task<QueryResult<TenderSalesMonthlyRecipetReportPerAgency>> GetTenderSalesMonthlyRecipetReportPerAgency(TenderValueToTypeSearchCriteria searchCriteria);
        Task<TenderSalesMonthlyCountsPerMonth> GetAllTenderSalesMonthlyCountsPerMonth(TenderValueToTypeSearchCriteria searchCriteria);
        Task<QueryResult<TenderSalesMonthlyTenderDetails>> GetTenderSalesMonthlyTenderDetails(TenderValueToTypeSearchCriteria searchCriteria);
        Task<QueryResult<PerformanceReportModel>> GetPerformanceReportAsync(TenderValueToTypeSearchCriteria searchCriteria);

        Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendAgencyAsync(TenderValueToTypeSearchCriteria searchCriteria);
        Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendCategoryAsync(TenderValueToTypeSearchCriteria searchCriteria);

        Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendActivitiesAsync(TenderValueToTypeSearchCriteria criteria);
        #endregion TenderMonthlySalesReceipteReport
    }
}
