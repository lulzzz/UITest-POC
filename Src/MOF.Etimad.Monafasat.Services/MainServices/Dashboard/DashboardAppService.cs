using AutoMapper;
using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class DashboardAppService : IDashboardAppService
    {
        private readonly IDashboardQueries _dashboardQueries;
        private readonly ILookUpService _lookUpService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardAppService(IDashboardQueries dashboardQueries, ILookUpService lookUpService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardQueries = dashboardQueries;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _lookUpService = lookUpService;
        }

        #region Service Queries
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForDataEntry(int branchId, SearchCriteria criteria)
        {
            QueryResult<RejectTenderViewModel> tenderList = await _dashboardQueries.GetRejectedTendersForDataEntry(branchId, criteria);

            return tenderList;
        }
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfGrievence(string agencyCode, SearchCriteria criteria)
        {
            QueryResult<RejectTenderViewModel> tenderList = await _dashboardQueries.GetRejectedTendersOfGrievence(agencyCode, criteria);

            return tenderList;
        }
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedPreplaning(string agencyCode, SearchCriteria criteria)
        {
            QueryResult<RejectTenderViewModel> tenderList = await _dashboardQueries.GetRejectedPreplaning(agencyCode, criteria);

            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedActionOfGrievence(string agencyCode, SearchCriteria criteria)
        {
            QueryResult<ProcessNeedsActionViewModel> tenderList = await _dashboardQueries.GetTendersNeedActionOfGrievence(agencyCode, criteria);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetPrePlaningNeedApproval(string agencyCode, SearchCriteria criteria)
        {
            QueryResult<ProcessNeedsActionViewModel> tenderList = await _dashboardQueries.GetPrePlanningNeedApproval(agencyCode, criteria);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedManagerApproval(SearchCriteria criteria)
        {
            QueryResult<ProcessNeedsActionViewModel> tenderList = await _dashboardQueries.GetBlockNeedManagerApproval(criteria);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedSecretaryApproval(SearchCriteria criteria)
        {
            QueryResult<ProcessNeedsActionViewModel> tenderList = await _dashboardQueries.GetBlockNeedSecretaryApproval(criteria);
            return tenderList;
        }
        public async Task<QueryResult<RejectTenderViewModel>> GetBlockSecretaryRejected(SearchCriteria criteria)
        {
            QueryResult<RejectTenderViewModel> tenderList = await _dashboardQueries.GetBlockSecretaryRejected(criteria);
            return tenderList;
        }

        public async Task<int> GetRejectedEscalationsPaging(string agenctCode)
        {
            int count = await _dashboardQueries.GetRejectedEscalationsPaging(agenctCode);
            return count;
        }

        public async Task<int> GetRejectedTendersCountOfCheckAndAwardingStage(int committeeId)
        {
            int tenderList = await _dashboardQueries.GetRejectedTendersCountOfCheckAndAwardingStage(committeeId);
            return tenderList;
        }

        public async Task<int> GetRejectedTendersCountOfDirectPurchaseCheckAndAwardingStage(int committeeId)
        {
            int tenderList = await _dashboardQueries.GetRejectedTendersCountOfDirectPurchaseCheckAndAwardingStage(committeeId);
            return tenderList;
        }
        public async Task<int> GetTendersNeedApprovalForDirectPurchaseCountAsync(int committeeId)
        {
            int tenderList = await _dashboardQueries.GetTendersNeedApprovalForDirectPurchaseCountAsync(committeeId);
            return tenderList;
        }
        public async Task<int> GetRejectedTendersCountPrePlaning()
        {
            int tenderList = await _dashboardQueries.GetRejectedTendersCountPrePlaning();
            return tenderList;
        }

        public async Task<int> GetRejectedCountBlock()
        {
            int tenderList = await _dashboardQueries.GetRejectedCountBlock();
            return tenderList;
        }

        public async Task<int> GetPendingEnquiriesCount(int committeeId, int userId)
        {
            int tenderList = await _dashboardQueries.GetPendingEnquiriesCount(committeeId, userId);
            return tenderList;
        }
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForOpeningStage(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetRejectedTendersForOpeningStage(committeeId, criteria);

            return tenderList;
        }

        public async Task<int> GetRejectedTendersCountForDataEntry(int branchId)
        {
            return await _dashboardQueries.GetRejectedTendersCountForDataEntry(branchId);
        }
        public async Task<int> GetRejectedTendersCountForOpeningStage(int committeeId)
        {
            return await _dashboardQueries.GetRejectedTendersCountForOpeningStage(committeeId);
        }

        public async Task<int> GetTendersInvitationsCount(int branchId)
        {
            return await _dashboardQueries.GetTendersInvitationsCount(branchId);
        }

        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfDirectPurchase(int committeeId, SearchCriteria criteria)
        {
            QueryResult<RejectTenderViewModel> rejectTenders = await _dashboardQueries.GetRejectedTendersOfDirectPurchase(committeeId, criteria);
            return rejectTenders;

        }

        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckAndAwardingStage(int committeeId, SearchCriteria criteria)
        {
            QueryResult<RejectTenderViewModel> rejectTenders = await _dashboardQueries.GetRejectedTendersOfCheckAndAwardingStage(committeeId, criteria);
            return rejectTenders;

        }



        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckQualificationStage(int committeeId, SearchCriteria criteria)
        {
            QueryResult<RejectTenderViewModel> rejectTenders = await _dashboardQueries.GetRejectedTendersOfCheckQualificationStage(committeeId, criteria);
            return rejectTenders;

        }

        public async Task<QueryResult<RejectTenderViewModel>> AwardedRejectedTendersPagingAsync(int branchId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetRejectedTendersByStatusIdPagingAsync(branchId, (int)Enums.TenderStatus.OffersAwardedRejected, criteria);
            var tenderListModel = _mapper.Map<QueryResult<RejectTenderViewModel>>(tenderList);
            return tenderListModel;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetOpeningStageProcessNeedsActionPagingAsync(int branchId, SearchCriteria criteria)
        {
            QueryResult<ProcessNeedsActionViewModel> tenderList = await _dashboardQueries.GetOpeningStageProcessNeedsActionPagingAsync(branchId, criteria, (int)Enums.TenderStatus.OffersAwardedPending, (int)Enums.TenderStatus.OffersCheckedPending);
            return tenderList;
        }

        public async Task<QueryResult<JoiningRequestInvitationsViewModel>> JoiningRequestInvitationsPagingAsync(int branchId, SearchCriteria criteria)
        {
            var joiningRequestInvitations = await _dashboardQueries.GetTendersInvitationsPagingAsync(branchId, criteria);
            return joiningRequestInvitations;
        }

        public async Task<QueryResult<OpeningNotificationsViewModel>> OpeningNotificationsPagingAsync(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.OpeningNotificationsPagingAsync(committeeId, criteria);
            var tenderListModel = _mapper.Map<QueryResult<OpeningNotificationsViewModel>>(tenderList);
            return tenderListModel;
        }
        public async Task<QueryResult<BasicTenderModel>> GetPendingEnquiries(int committeeId, int userId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetPendingEnquiries(committeeId, userId, criteria);
            return tenderList;
        }
        public async Task<int> GetUnderEstablishedTendersCount(int branchId)
        {
            var tenderList = await _dashboardQueries.GetUnderEstablishedTendersCount(branchId);
            return tenderList;
        }
        public async Task<int> GetProcessNeedsApprovalByStatusPaging(int branchId, string agencyCode)
        {
            var tenderList = await _dashboardQueries.GetProcessNeedsApprovalByStatusPaging(branchId, agencyCode);
            return tenderList;
        }
        public async Task<int> GetProcessNeedsApprovalForEtimadOfficcer(int branchId, string agencyCode)
        {
            var tenderList = await _dashboardQueries.GetProcessNeedsApprovalForEtimadOfficcer(branchId, agencyCode);
            return tenderList;
        }
        public async Task<QueryResult<Tender>> GetDataEntryProcessNeedsActionPagingAsync(int branchId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetUnderEstablishedTendersAsync(branchId, criteria);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetAuditingProcessNeedsActionPagingAsync(int branchId, string agencyCode, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetProcessNeedsApprovalByStatusPaging(branchId, agencyCode, criteria);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetAuditingPreQualificationProcessNeedsActionPagingAsync(int branchId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetQualificationProcessNeedsApprovalByStatusPagingAsync(branchId, criteria);
            var tenderListModel = _mapper.Map<QueryResult<ProcessNeedsActionViewModel>>(tenderList);
            return tenderListModel;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetConfirmOpeningProcessNeedsActionPagingAsync(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetTendersWaitingForApproveOppeiningReport(committeeId, criteria);
            return tenderList;
        }
        public async Task<int> GetTendersWaitingForApproveOpeningReportCount(int committeeId)
        {
            var tenderList = await _dashboardQueries.GetTendersWaitingForApproveOpeningReportCount(committeeId);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingStageProcessNeedsActionPagingAsync(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetTendersWaitingForApproveCheckingReport(committeeId, criteria);
            var tenderListModel = _mapper.Map<QueryResult<ProcessNeedsActionViewModel>>(tenderList);
            return tenderListModel;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingAndAwardingStageProcessNeedsAction(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetCheckingAndAwardingStageProcessNeedsAction(committeeId, criteria);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetFinalAwardStageProcessNeedsAction(int branchId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetFinalAwardStageProcessNeedsAction(branchId, criteria);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetVROAwardStageProcessNeedsAction(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetVROAwardStageProcessNeedsAction(committeeId, criteria);
            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchasePaginAsync(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetTendersNeedApprovalForDirectPurchasePaginAsync(committeeId, criteria);
            return tenderList;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync(int committeeId, SearchCriteria criteria, string agencyCode)
        {
            return await _dashboardQueries.GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync(committeeId, criteria, agencyCode);
        }
        public async Task<int> GetTendersNeedApprovalForPreQualificationCommitteeManagerCount(int committeeId, string agencyCode)
        {
            return await _dashboardQueries.GetTendersNeedApprovalForPreQualificationCommitteeManagerCount(committeeId, agencyCode);
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync(committeeId, criteria);
            return tenderList;
        }


        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync(committeeId, criteria);
            return tenderList;
        }

        public async Task<int> GetCheckingAndAwardingStageProcessNeedsActionCount(int committeeId)
        {
            int count = await _dashboardQueries.GetCheckingAndAwardingStageProcessNeedsActionCount(committeeId);
            return count;
        }
        public async Task<int> GetTendersNeedActionOfGrievenceCount(string agencyCode)
        {
            int count = await _dashboardQueries.GetTendersNeedActionOfGrievenceCount(agencyCode);
            return count;
        }
        public async Task<int> GetRejectedTendersOfGrievenceCount(string agencyCode)
        {
            int count = await _dashboardQueries.GetTendersNeedActionOfGrievenceCount(agencyCode);
            return count;
        }
        public async Task<int> GetVROCheckingAndAwardingStageProcessNeedsActionCount(int committeeId)
        {
            int count = await _dashboardQueries.GetVROCheckingAndAwardingStageProcessNeedsActionCount(committeeId);
            return count;
        }
        public async Task<int> GetFinalAwardingStageProcessNeedsActionCount()
        {
            int count = await _dashboardQueries.GetFinalAwardingStageProcessNeedsActionCount();
            return count;
        }
        public async Task<int> GetPrePlaningNeedApprovalCount(string agencyCode)
        {
            int count = await _dashboardQueries.GetPrePlaningNeedApprovalCount(agencyCode);
            return count;
        }
        public async Task<int> GetPrePlaningRejectedCount(string agencyCode)
        {
            int count = await _dashboardQueries.GetPrePlaningRejectedCount(agencyCode);
            return count;
        }
        public async Task<int> GetManagerBlockNeedApprovalCount()
        {
            int count = await _dashboardQueries.GetManagerBlockNeedApprovalCount();
            return count;
        }
        public async Task<int> GetSecretaryBlockNeedApprovalCount()
        {
            int count = await _dashboardQueries.GetSecretaryBlockNeedApprovalCount();
            return count;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetAwardingProcessNeedsActionPagingAsync(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetTendersWaitingForApproveAwardingReport(committeeId, criteria);
            var tenderListModel = _mapper.Map<QueryResult<ProcessNeedsActionViewModel>>(tenderList);
            return tenderListModel;
        }

        public async Task<SalesSummaryViewModel> SalesSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            var resultList = await _dashboardQueries.SalesSummaryPagingAsync(criteria);
            return resultList;
        }
        public async Task<TendersSummaryViewModel> TendersSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            var resultList = await _dashboardQueries.TendersSummaryPagingAsync(criteria);
            return resultList;
        }
        public async Task<DirectInvitationsSummaryViewModel> DirectInvitationsSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            var resultList = await _dashboardQueries.DirectInvitationsSummaryPagingAsync(criteria);
            return resultList;
        }
        public async Task<string> SuppliersCountPagingAsync(DashboardSearchCriteria criteria)
        {
            var result = await _dashboardQueries.SuppliersCountPagingAsync(criteria);
            return result;
        }

        public async Task<List<LastTenPurshaseModel>> GetLastTenPurshase(int branchId, string agencyCode)
        {
            return await _dashboardQueries.GetLastTenPurshase(branchId, agencyCode);
        }
        public async Task<QueryResult<Enquiry>> GetSuppliersEnquiry(int branchId, SearchCriteria criteria)
        {
            return await _dashboardQueries.GetSuppliersEnquiry(branchId, criteria);
        }

        public async Task<int> GetLastTenPurshaseCount(int branchId, string agencyCode)
        {
            return await _dashboardQueries.GetLastTenPurshaseCount(branchId, agencyCode);
        }

        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForVROOpeningStage(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetRejectedTendersForVROOpeningStage(committeeId, criteria);

            return tenderList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetVROOpenCheckStageProcessNeedsAction(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _dashboardQueries.GetVROOpenCheckStageProcessNeedsAction(committeeId, criteria);
            return tenderList;
        }

        public async Task<int> GetRejectedTendersCountForVROOpeningCheckingStage(int committeeId)
        {
            int count = await _dashboardQueries.GetRejectedTendersCountForVROOpeningCheckingStage(committeeId);
            return count;
        }


        #region Unit 
        public async Task<QueryResult<TenderWaitingTheUnitActionViewModel>> GetTendersWaitingTheUnitActionAsync(SearchCriteria criteria)
        {
            return await _dashboardQueries.GetTendersWaitingTheUnitActionAsync(criteria);
        }
        #endregion

        #endregion
    }
}
