using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IDashboardAppService
    {
        #region Queries
        Task<int> GetRejectedEscalationsPaging(string agenctCode);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedActionOfGrievence(string agencyCode, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetPrePlaningNeedApproval(string agencyCode, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedManagerApproval(SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedSecretaryApproval(SearchCriteria criteria);

        Task<QueryResult<RejectTenderViewModel>> GetBlockSecretaryRejected(SearchCriteria criteria);

        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfGrievence(string agencyCode, SearchCriteria criteria);
        Task<int> GetTendersNeedActionOfGrievenceCount(string agencyCode);
        Task<int> GetRejectedTendersOfGrievenceCount(string agencyCode);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedPreplaning(string agencyCode, SearchCriteria criteria);
        Task<int> GetRejectedTendersCountOfCheckAndAwardingStage(int committeeId);
        Task<int> GetRejectedTendersCountOfDirectPurchaseCheckAndAwardingStage(int committeeId);
        Task<int> GetRejectedTendersCountPrePlaning();
        Task<int> GetRejectedCountBlock();
        Task<int> GetTendersNeedApprovalForDirectPurchaseCountAsync(int committeeId);
        Task<int> GetPendingEnquiriesCount(int committeeId, int userId);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForDataEntry(int branchId, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForOpeningStage(int committeeId, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckAndAwardingStage(int committeeId, SearchCriteria criteria);

        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync(int committeeId, SearchCriteria criteria);

        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfDirectPurchase(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetOpeningStageProcessNeedsActionPagingAsync(int branchId, SearchCriteria criteria);
        Task<QueryResult<RejectTenderViewModel>> AwardedRejectedTendersPagingAsync(int branchId, SearchCriteria criteria);
        Task<QueryResult<JoiningRequestInvitationsViewModel>> JoiningRequestInvitationsPagingAsync(int branchId, SearchCriteria criteria);
        Task<QueryResult<OpeningNotificationsViewModel>> OpeningNotificationsPagingAsync(int committeeId, SearchCriteria criteria);
        Task<QueryResult<BasicTenderModel>> GetPendingEnquiries(int committeeId, int userId, SearchCriteria criteria);
        Task<QueryResult<Tender>> GetDataEntryProcessNeedsActionPagingAsync(int branchId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetAuditingProcessNeedsActionPagingAsync(int branchId, string agencyCode, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetAuditingPreQualificationProcessNeedsActionPagingAsync(int branchId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetConfirmOpeningProcessNeedsActionPagingAsync(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingStageProcessNeedsActionPagingAsync(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingAndAwardingStageProcessNeedsAction(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetFinalAwardStageProcessNeedsAction(int branchId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetVROAwardStageProcessNeedsAction(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchasePaginAsync(int committeeId, SearchCriteria criteria);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync(int committeeId, SearchCriteria criteria, string agencyCode);
        Task<int> GetTendersNeedApprovalForPreQualificationCommitteeManagerCount(int committeeId, string agencyCode);
        Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync(int committeeId, SearchCriteria criteria);
        Task<int> GetCheckingAndAwardingStageProcessNeedsActionCount(int committeeId);
        Task<int> GetVROCheckingAndAwardingStageProcessNeedsActionCount(int committeeId);
        Task<int> GetFinalAwardingStageProcessNeedsActionCount();
        Task<QueryResult<ProcessNeedsActionViewModel>> GetAwardingProcessNeedsActionPagingAsync(int committeeId, SearchCriteria criteria);
        Task<List<LastTenPurshaseModel>> GetLastTenPurshase(int branchId, string agencyCode);
        Task<QueryResult<Enquiry>> GetSuppliersEnquiry(int branchId, SearchCriteria criteria);
        Task<SalesSummaryViewModel> SalesSummaryPagingAsync(DashboardSearchCriteria criteria);
        Task<TendersSummaryViewModel> TendersSummaryPagingAsync(DashboardSearchCriteria criteria);
        Task<DirectInvitationsSummaryViewModel> DirectInvitationsSummaryPagingAsync(DashboardSearchCriteria criteria);
        Task<string> SuppliersCountPagingAsync(DashboardSearchCriteria criteria);
        Task<int> GetTendersInvitationsCount(int branchId);
        Task<int> GetLastTenPurshaseCount(int branchId, string agencyCode);
        Task<int> GetUnderEstablishedTendersCount(int branchId);
        Task<int> GetProcessNeedsApprovalByStatusPaging(int branchId, string agencyCode);
        Task<int> GetProcessNeedsApprovalForEtimadOfficcer(int branchId, string agencyCode);
        Task<int> GetTendersWaitingForApproveOpeningReportCount(int committeeId);
        Task<int> GetRejectedTendersCountForOpeningStage(int committeeId);
        Task<int> GetRejectedTendersCountForDataEntry(int branchId);
        Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckQualificationStage(int committeeId, SearchCriteria criteria);
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
        #endregion
    }
}