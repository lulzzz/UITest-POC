using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IDashboardQueries
    {
        Task<QueryResult<Tender>> OpeningNotificationsPagingAsync(int branchId, SearchCriteria criteria);
        Task<QueryResult<BasicTenderModel>> GetPendingEnquiries(int committeeId, int userId, SearchCriteria criteria);
        Task<QueryResult<JoiningRequestInvitationsViewModel>> GetTendersInvitationsPagingAsync(int branchId, SearchCriteria criteria);
        Task<QueryResult<Tender>> GetRejectedTendersByStatusIdPagingAsync(int branchId, int? statusId, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForDataEntry(int branchId, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckAndAwardingStage(int committeeId, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckQualificationStage(int committeeId, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfDirectPurchase(int committeeId, SearchCriteria criteria);

        Task<QueryResult<Tender>> GetUnderEstablishedTendersAsync(int branchId, SearchCriteria criteria);
        Task<SalesSummaryViewModel> SalesSummaryPagingAsync(DashboardSearchCriteria criteria);
        Task<TendersSummaryViewModel> TendersSummaryPagingAsync(DashboardSearchCriteria criteria);
        Task<DirectInvitationsSummaryViewModel> DirectInvitationsSummaryPagingAsync(DashboardSearchCriteria criteria);
        Task<string> SuppliersCountPagingAsync(DashboardSearchCriteria criteria);
        Task<List<LastTenPurshaseModel>> GetLastTenPurshase(int branchId, string agencyCode);
        Task<QueryResult<Enquiry>> GetSuppliersEnquiry(int branchId, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForOpeningStage(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetOpeningStageProcessNeedsActionPagingAsync(int branchId, SearchCriteria criteria, params int[] statues);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingAndAwardingStageProcessNeedsAction(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetFinalAwardStageProcessNeedsAction(int branchId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetVROAwardStageProcessNeedsAction(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchasePaginAsync(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync(int committeeId, SearchCriteria criteria);

        Task<QueryResult<Tender>> GetQualificationProcessNeedsApprovalByStatusPagingAsync(int branchId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetProcessNeedsApprovalByStatusPaging(int branchId, string agencyCode, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersWaitingForApproveOppeiningReport(int committeeId, SearchCriteria criteria);
        Task<QueryResult<Tender>> GetTendersWaitingForApproveCheckingReport(int committeeId, SearchCriteria criteria);
        Task<QueryResult<Tender>> GetTendersWaitingForApproveAwardingReport(int committeeId, SearchCriteria criteria);
        Task<int> GetRejectedTendersCountForDataEntry(int branchId);
        Task<int> GetRejectedTendersCountForOpeningStage(int committeeId);
        Task<int> GetRejectedTendersCountOfCheckAndAwardingStage(int committeeId);
        Task<int> GetRejectedTendersCountOfDirectPurchaseCheckAndAwardingStage(int committeeId);
        Task<int> GetTendersNeedApprovalForDirectPurchaseCountAsync(int committeeId);
        Task<int> GetRejectedTendersCountPrePlaning();
        Task<int> GetRejectedCountBlock();
        Task<int> GetPendingEnquiriesCount(int committeeId, int userId);
        Task<int> GetTendersInvitationsCount(int branchId);
        Task<int> GetLastTenPurshaseCount(int branchId, string agencyCode);
        Task<int> GetUnderEstablishedTendersCount(int branchId);
        Task<int> GetProcessNeedsApprovalByStatusPaging(int branchId, string agencyCode);
        Task<int> GetProcessNeedsApprovalForEtimadOfficcer(int branchId, string agencyCode);
        Task<int> GetRejectedEscalationsPaging(string agencyCode);
        Task<int> GetTendersWaitingForApproveOpeningReportCount(int committeeId);
        Task<int> GetCheckingAndAwardingStageProcessNeedsActionCount(int committeeId);
        Task<int> GetVROCheckingAndAwardingStageProcessNeedsActionCount(int committeeId);
        Task<int> GetFinalAwardingStageProcessNeedsActionCount();
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfGrievence(string agencyCode, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedPreplaning(string agencyCode, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedActionOfGrievence(string agencyCode, SearchCriteria criteria);
        Task<int> GetTendersNeedActionOfGrievenceCount(string agencyCode);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetPrePlanningNeedApproval(string agencyCode, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedManagerApproval(SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedSecretaryApproval(SearchCriteria criteria);

        Task<QueryResult<RejectTenderViewModel>> GetBlockSecretaryRejected(SearchCriteria criteria);

        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync(int committeeId, SearchCriteria criteria, string agencyCode);
        Task<int> GetTendersNeedApprovalForPreQualificationCommitteeManagerCount(int committeeId, string agencyCode);
        Task<int> GetPrePlaningNeedApprovalCount(string agencyCode);
        Task<int> GetPrePlaningRejectedCount(string agencyCode);
        Task<int> GetManagerBlockNeedApprovalCount();
        Task<int> GetSecretaryBlockNeedApprovalCount();

        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForVROOpeningStage(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetVROOpenCheckStageProcessNeedsAction(int committeeId, SearchCriteria criteria);

        Task<int> GetRejectedTendersCountForVROOpeningCheckingStage(int committeeId);
        #region Unit
        Task<QueryResult<TenderWaitingTheUnitActionViewModel>> GetTendersWaitingTheUnitActionAsync(SearchCriteria criteria);
        #endregion
    }

}
