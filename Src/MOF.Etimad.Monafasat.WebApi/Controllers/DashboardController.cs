using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : BaseController
    {

        private readonly IDashboardAppService _dashboardService;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardAppService dashboardService, IMapper mapper, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _dashboardService = dashboardService;
            _mapper = mapper;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetRejectedTendersForDataEntry")]
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForDataEntry(SearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            var result = await _dashboardService.GetRejectedTendersForDataEntry(branchId, criteria);
            return result;
        }
        [Authorize(RoleNames.DashboardProcessNeedsActionPolicy)]
        [HttpGet]
        [Route("GetProcessesNeedActionOfGrievence")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetProcessesNeedActionOfGrievence(SearchCriteria criteria)
        {
            string agencyCode = User.UserAgency();
            var result = await _dashboardService.GetTendersNeedActionOfGrievence(agencyCode, criteria);
            return result;
        }


        [Authorize(RoleNames.DashboardProcessNeedsActionPolicy)]
        [HttpGet]
        [Route("GetPrePlaningNeedApproval")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetPrePlaningNeedApproval(SearchCriteria criteria)
        {
            string agencyCode = User.UserAgency();
            var result = await _dashboardService.GetPrePlaningNeedApproval(agencyCode, criteria);
            return result;
        }

        [Authorize(RoleNames.DashboardProcessNeedsActionPolicy)]
        [HttpGet]
        [Route("GetBlockNeedManagerApproval")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedManagerApproval(SearchCriteria criteria)
        {
            var result = await _dashboardService.GetBlockNeedManagerApproval(criteria);
            return result;
        }

        [Authorize(RoleNames.DashboardProcessNeedsActionPolicy)]
        [HttpGet]
        [Route("GetBlockNeedSecretaryApproval")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedSecretaryApproval(SearchCriteria criteria)
        {
            var result = await _dashboardService.GetBlockNeedSecretaryApproval(criteria);
            return result;
        }

        [Authorize(RoleNames.DashboardRejectedTendersPolicy)]
        [HttpGet]
        [Route("GetBlockNeedSecretaryRejected")]
        public async Task<QueryResult<RejectTenderViewModel>> GetBlockNeedSecretaryRejected(SearchCriteria criteria)
        {
            var result = await _dashboardService.GetBlockSecretaryRejected(criteria);
            return result;
        }


        [Authorize(RoleNames.DashboardRejectedTendersPolicy)]
        [HttpGet]
        [Route("GetRejectedTendersOfGrievence")]
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfGrievence(SearchCriteria criteria)
        {
            string agencyCode = User.UserAgency();
            var result = await _dashboardService.GetRejectedTendersOfGrievence(agencyCode, criteria);
            return result;
        }
        [Authorize(RoleNames.DashboardRejectedTendersPolicy)]
        [HttpGet]
        [Route("GetRejectedPreplaning")]
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedPreplaning(SearchCriteria criteria)
        {
            string agencyCode = User.UserAgency();
            var result = await _dashboardService.GetRejectedPreplaning(agencyCode, criteria);
            return result;
        }
        [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
        [HttpGet]
        [Route("GetRejectedTendersForOpeningStage")]
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForOpeningStage(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetRejectedTendersForOpeningStage(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpGet]
        [Route("GetRejectedTendersOfCheckAndAwardingStage")]
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckAndAwardingStage(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetRejectedTendersOfCheckAndAwardingStage(committeeId, criteria);
            return result;
        }


        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpGet]
        [Route("GetRejectedTendersOfDirectPurchase")]
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfDirectPurchase(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetRejectedTendersOfDirectPurchase(committeeId, criteria);
            return result;
        }

        //[Authorize(RoleNames.PreQualificationCommitteeSecretary)]
        [HttpGet]
        [Route("GetRejectedTendersOfCheckQualificationStage")]
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckQualificationStage(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetRejectedTendersOfCheckQualificationStage(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpGet]
        [Route("AwardedRejectedTendersPagingAsync")]
        public async Task<QueryResult<RejectTenderViewModel>> AwardedRejectedTendersPagingAsync(SearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            var result = await _dashboardService.AwardedRejectedTendersPagingAsync(branchId, criteria);
            return result;
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpGet]
        [Route("GetOpeningStageProcessNeedsActionPagingAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetOpeningStageProcessNeedsActionPagingAsync(SearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            var result = await _dashboardService.GetOpeningStageProcessNeedsActionPagingAsync(branchId, criteria);
            return result;
        }

        [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
        [HttpGet]
        [Route("JoiningRequestInvitationsPagingAsync")]
        public async Task<QueryResult<JoiningRequestInvitationsViewModel>> JoiningRequestInvitationsPagingAsync(SearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            var result = await _dashboardService.JoiningRequestInvitationsPagingAsync(branchId, criteria);
            return result;
        }

        [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("OpeningNotificationsPagingAsync")]
        public async Task<QueryResult<OpeningNotificationsViewModel>> OpeningNotificationsPagingAsync(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.OpeningNotificationsPagingAsync(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.TechnicalCommitteeUserPolicy)]
        [HttpGet]
        [Route("GetPendingEnquiries")]
        public async Task<QueryResult<BasicTenderModel>> GetPendingEnquiries(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            int userId = User.UserId();
            var result = await _dashboardService.GetPendingEnquiries(committeeId, userId, criteria);
            return result;
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpGet]
        [Route("GetAuditingProcessNeedsActionPagingAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetAuditingProcessNeedsActionPagingAsync(SearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            string agencyCode = User.UserAgency();
            var result = await _dashboardService.GetAuditingProcessNeedsActionPagingAsync(branchId, agencyCode, criteria);
            return result;
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpGet]
        [Route("GetAuditingPreQualificationProcessNeedsActionPagingAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetAuditingPreQualificationProcessNeedsActionPagingAsync(SearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            var result = await _dashboardService.GetAuditingPreQualificationProcessNeedsActionPagingAsync(branchId, criteria);
            return result;
        }

        [Authorize(RoleNames.DataEntryPolicy)]
        [HttpGet]
        [Route("GetDataEntrygProcessNeedsActionPagingAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetDataEntrygProcessNeedsActionPagingAsync(SearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            QueryResult<Tender> tenderList = await _dashboardService.GetDataEntryProcessNeedsActionPagingAsync(branchId, criteria);
            QueryResult<ProcessNeedsActionViewModel> tenderListModel = _mapper.Map<QueryResult<ProcessNeedsActionViewModel>>(tenderList);
            return tenderListModel;
        }

        [Authorize(RoleNames.OffersOppeningManagerPolicy)]
        [HttpGet]
        [Route("GetConfirmOpeningProcessNeedsActionPagingAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetConfirmOpeningProcessNeedsActionPagingAsync(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetConfirmOpeningProcessNeedsActionPagingAsync(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpGet]
        [Route("GetCheckingStageProcessNeedsActionPagingAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingStageProcessNeedsActionPagingAsync(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetCheckingStageProcessNeedsActionPagingAsync(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpGet]
        [Route("GetCheckingAndAwardingStageProcessNeedsAction")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingAndAwardingStageProcessNeedsAction(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetCheckingAndAwardingStageProcessNeedsAction(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpGet]
        [Route("GetFinalAwardStageProcessNeedsAction")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetFinalAwardStageProcessNeedsAction(SearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            var result = await _dashboardService.GetFinalAwardStageProcessNeedsAction(branchId, criteria);
            return result;
        }

        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpGet]
        [Route("GetVROAwardStageProcessNeedsAction")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetVROAwardStageProcessNeedsAction(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetVROAwardStageProcessNeedsAction(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
        [HttpGet]
        [Route("GetTendersNeedApprovalForDirectPurchasePaginAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchasePaginAsync(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetTendersNeedApprovalForDirectPurchasePaginAsync(committeeId, criteria);
            return result;
        }

        //[Authorize(RoleNames.PreQualificationCommitteeManager)]
        [HttpGet]
        [Route("GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync(committeeId, criteria, User.UserAgency());
            return result;
        }


        [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
        [HttpGet]
        [Route("GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync(committeeId, criteria);
            return result;
        }


        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpGet]
        [Route("GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync(committeeId, criteria);
            return result;
        }


        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpGet]
        [Route("GetAwardingProcessNeedsActionPagingAsync")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetAwardingProcessNeedsActionPagingAsync(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetAwardingProcessNeedsActionPagingAsync(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
        [HttpGet]
        [Route("SalesSummaryPagingAsync")]
        public async Task<SalesSummaryViewModel> SalesSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            criteria.AgencyCode = User.UserAgency();
            var result = await _dashboardService.SalesSummaryPagingAsync(criteria);
            return result;
        }

        [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
        [HttpGet]
        [Route("TendersSummaryPagingAsync")]
        public async Task<TendersSummaryViewModel> TendersSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            criteria.AgencyCode = User.UserAgency();
            var result = await _dashboardService.TendersSummaryPagingAsync(criteria);
            return result;
        }

        [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
        [HttpGet]
        [Route("DirectInvitationsSummaryPagingAsync")]
        public async Task<DirectInvitationsSummaryViewModel> DirectInvitationsSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            criteria.AgencyCode = User.UserAgency();
            var result = await _dashboardService.DirectInvitationsSummaryPagingAsync(criteria);
            return result;
        }

        [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
        [HttpGet]
        [Route("SuppliersCountPagingAsync")]
        public async Task<string> SuppliersCountPagingAsync(DashboardSearchCriteria criteria)
        {
            criteria.AgencyCode = User.UserAgency();
            var result = await _dashboardService.SuppliersCountPagingAsync(criteria);
            return result;
        }

        [Authorize(RoleNames.DashboardPolicy)]
        [HttpGet]
        [Route("GetLastTenPurshase")]
        public async Task<List<LastTenPurshaseModel>> GetLastTenPurshase()
        {
            string agencyCode = User.UserAgency();
            int branchId = User.UserBranch();
            var LastTenPurshaseModel = await _dashboardService.GetLastTenPurshase(branchId, agencyCode);
            return LastTenPurshaseModel;
        }

        [Authorize(RoleNames.AuditerPolicy)]
        [HttpGet]
        [Route("GetSuppliersEnquiry")]
        public async Task<QueryResult<SupplierEnquiryModel>> GetSuppliersEnquiry(DashboardSearchCriteria criteria)
        {
            int branchId = User.UserBranch();
            var enquires = await _dashboardService.GetSuppliersEnquiry(branchId, criteria);
            var result = _mapper.Map<QueryResult<SupplierEnquiryModel>>(enquires);
            return result;
        }


        [Authorize(RoleNames.DashboardPolicy)]
        [HttpGet]
        [Route("GetDashBoardCountsAsync")]
        public async Task<DashBoardTotalCount> GetDashBoardCountsAsync()
        {
            string agencyCode = User.UserAgency();
            DashBoardTotalCount model = new DashBoardTotalCount();
            int branchId = User.UserBranch();
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.PurshaseSpecialist))
            {
                model.processNeedAction = await _dashboardService.GetUnderEstablishedTendersCount(branchId);
            }
            else if (User.IsInRole(RoleNames.Auditer))
            {
                model.processNeedAction = await _dashboardService.GetProcessNeedsApprovalByStatusPaging(branchId, agencyCode);
            }
            else if (User.IsInRole(RoleNames.PreQualificationCommitteeManager))
            {
                int committeeId = User.SelectedCommittee();
                model.processNeedAction = await _dashboardService.GetTendersNeedApprovalForPreQualificationCommitteeManagerCount(committeeId, agencyCode);
            }
            else if (User.IsInRole(RoleNames.EtimadOfficer))
            {
                model.processNeedAction = await _dashboardService.GetProcessNeedsApprovalForEtimadOfficcer(branchId, agencyCode);
            }
            else if (User.IsInRole(RoleNames.OffersOppeningManager))
            {
                int committeeId = User.SelectedCommittee();
                model.processNeedAction = await _dashboardService.GetTendersWaitingForApproveOpeningReportCount(committeeId);
            }
            else if (User.IsInRole(RoleNames.OffersCheckManager))
            {
                int committeeId = User.SelectedCommittee();
                model.processNeedAction = await _dashboardService.GetCheckingAndAwardingStageProcessNeedsActionCount(committeeId);
            }
            else if (User.IsInRole(RoleNames.OffersOpeningAndCheckManager))
            {
                int committeeId = User.SelectedCommittee();
                model.processNeedAction = await _dashboardService.GetVROCheckingAndAwardingStageProcessNeedsActionCount(committeeId);
            }
            else if (User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary))
            {
                int committeeId = User.SelectedCommittee();
                model.RejectedTendersCount = await _dashboardService.GetRejectedTendersCountForVROOpeningCheckingStage(committeeId);
            }
            else if (User.IsInRole(RoleNames.ApproveTenderAward))
            {
                model.processNeedAction = await _dashboardService.GetFinalAwardingStageProcessNeedsActionCount();
            }
            else if (User.IsInRole(RoleNames.PrePlanningAuditor))
            {
                model.processNeedAction = await _dashboardService.GetPrePlaningNeedApprovalCount(agencyCode);
            }
            else if (User.IsInRole(RoleNames.PrePlanningCreator))
            {
                model.RejectedTendersCount = await _dashboardService.GetPrePlaningRejectedCount(agencyCode);
            }
            else if (User.IsInRole(RoleNames.MonafasatBlackListCommittee))
            {
                model.processNeedAction = await _dashboardService.GetManagerBlockNeedApprovalCount();
            }
            else if (User.IsInRole(RoleNames.MonafasatBlockListSecritary))
            {
                model.processNeedAction = await _dashboardService.GetSecretaryBlockNeedApprovalCount();
            }
            //////////////////////////////////////
            model.TenderInvitationCount = await _dashboardService.GetTendersInvitationsCount(branchId);

            model.LastTenPurshaseCount = await _dashboardService.GetLastTenPurshaseCount(branchId, agencyCode);
            ////////
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.PurshaseSpecialist))
            {
                model.RejectedTendersCount = await _dashboardService.GetRejectedTendersCountForDataEntry(branchId);
            }
            else if (User.IsInRole(RoleNames.OffersOppeningSecretary))
            {
                int committeeId = User.SelectedCommittee();
                model.RejectedTendersCount = await _dashboardService.GetRejectedTendersCountForOpeningStage(committeeId);
            }
            else if (User.IsInRole(RoleNames.OffersCheckSecretary))
            {
                int committeeId = User.SelectedCommittee();
                model.RejectedTendersCount = await _dashboardService.GetRejectedTendersCountOfCheckAndAwardingStage(committeeId);
            }
            else if (User.IsInRole(RoleNames.OffersPurchaseSecretary) || User.IsInRole(RoleNames.OffersPurchaseManager))
            {
                int committeeId = User.SelectedCommittee();
                model.RejectedTendersCount = await _dashboardService.GetRejectedTendersCountOfDirectPurchaseCheckAndAwardingStage(committeeId);
            }
            else if (User.IsInRole(RoleNames.MonafasatBlockListSecritary))
            {
                model.RejectedTendersCount = await _dashboardService.GetRejectedCountBlock();
            }

            else if (User.IsInRole(RoleNames.SecretaryGrievanceCommittee))
            {
                model.RejectedTendersCount = await _dashboardService.GetRejectedTendersOfGrievenceCount(User.UserAgency());
            }
            else if (User.IsInRole(RoleNames.TechnicalCommitteeUser))
            {
                int committeeId = User.SelectedCommittee();
                int userId = User.UserId();
                model.PendingEnquiriesCount = await _dashboardService.GetPendingEnquiriesCount(committeeId, userId);
            }
            if (User.IsInRole(RoleNames.SecretaryGrievanceCommittee))
            {
                model.RejectedTendersCount = await _dashboardService.GetRejectedEscalationsPaging(User.UserAgency());
            }
            if (User.IsInRole(RoleNames.ManagerGrievanceCommittee))
            {
                model.processNeedAction = await _dashboardService.GetTendersNeedActionOfGrievenceCount(User.UserAgency());
            }
            if (User.IsInRole(RoleNames.OffersPurchaseManager))
            {
                int committeeId = User.SelectedCommittee();
                model.processNeedAction = await _dashboardService.GetTendersNeedApprovalForDirectPurchaseCountAsync(committeeId);
            }
            return model;
        }

        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetRejectedTendersForVROOpeningStage")]
        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForVROOpeningStage(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetRejectedTendersForVROOpeningStage(committeeId, criteria);
            return result;
        }

        [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
        [HttpGet]
        [Route("GetVROOpenCheckStageProcessNeedsAction")]
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetVROOpenCheckStageProcessNeedsAction(SearchCriteria criteria)
        {
            int committeeId = User.SelectedCommittee();
            var result = await _dashboardService.GetVROOpenCheckStageProcessNeedsAction(committeeId, criteria);
            return result;
        }

        #region Unit

        [HttpGet]
        [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
        [Route("TendersWaitingTheUnitActionAsync")]
        public async Task<QueryResult<TenderWaitingTheUnitActionViewModel>> TendersWaitingTheUnitActionAsync(DashboardSearchCriteria criteria)
        {
            var result = await _dashboardService.GetTendersWaitingTheUnitActionAsync(criteria);
            return result;
        }

        #endregion

    }

}
