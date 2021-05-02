using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class DashboardQueries : IDashboardQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<QueryResult<Tender>> GetRejectedTendersByStatusIdPagingAsync(int branchId, int? statusId, SearchCriteria criteria)
        {
            var tenderList = await _context.Tenders
                .Include(x => x.Status)
                .Include(x => x.TenderHistories).Include(r => r.ChangeRequests)
                .Where(x => x.IsActive == true && x.BranchId == branchId)
                .WhereIf(statusId != null, x => x.TenderStatusId == statusId)
                .OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return tenderList;
        }

        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfGrievence(string agencyCode, SearchCriteria criteria)
        {
            var tenderList = _context.AgencyCommunicationRequests.Where(w =>
                         w.IsActive == true
                                                  && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                         && w.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                        .OrderBy(x => x.SupplierExtendOfferDatesRequestId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.Tender.TenderName,
                           TenderNumber = x.Tender.TenderNumber,
                           ReferenceNumber = x.Tender.ReferenceNumber,
                           TenderStatusName = x.EscalationStatus.Name,
                           TenderStatusId = x.Tender.TenderStatusId,
                           TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.AgencyRequestId),
                           RejectionReason = x.EscalationRejectionReason,
                           ChangeType = Resources.CommunicationRequest.DisplayInputs.EscalatePlaint,
                           ChangeRequestTypeId = 90
                       }
                 ).Page(criteria.PageNumber, criteria.PageSize);
            int count = await _context.AgencyCommunicationRequests.Where(w =>
                         w.IsActive == true
                                                  && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                         && w.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected).CountAsync();

            QueryResult<RejectTenderViewModel> queryResult = new QueryResult<RejectTenderViewModel>(await tenderList.ToListAsync(), count, criteria.PageNumber, criteria.PageSize);

            return queryResult;
        }

        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedPreplaning(string agencyCode, SearchCriteria criteria)
        {
            var tenderList = _context.PrePlannings.Where(w =>
                         w.IsActive == true && w.AgencyCode == agencyCode

                         && (w.StatusId == (int)Enums.PrePlanningStatus.Rejected || (w.StatusId == (int)Enums.PrePlanningStatus.Approved) && !w.IsDeleteRequested && !string.IsNullOrEmpty(w.DeleteRejectionReason)))
                        .OrderBy(x => x.PrePlanningId)
                       .Select(r => new RejectTenderViewModel
                       {
                           ProjectName = r.ProjectName,
                           EncyptedPrePlanningId = Util.Encrypt(r.PrePlanningId),
                           InsideKSA = r.InsideKSA,
                           Duration = r.Duration,
                           YearQuarterId = r.YearQuarterId,
                           YearQuarterName = r.YearQuarter.NameAr,
                           ChangeRequestTypeId = 95,
                           DurationInDays = r.DurationInDays,
                           DurationInMonths = r.DurationInMonths,
                           DurationInYears = r.DurationInYears,
                           PrePlaningStatusId = r.StatusId,
                           RejectionReason = r.RejectionReason,
                           DeleteRejectionReason = r.DeleteRejectionReason,
                           IsDeleteRequested = r.IsDeleteRequested
                       }
                 ).Page(criteria.PageNumber, criteria.PageSize);
            int count = await _context.PrePlannings.Where(w =>
                          w.IsActive == true && w.AgencyCode == agencyCode
                         && (w.StatusId == (int)Enums.PrePlanningStatus.Rejected || (w.StatusId == (int)Enums.PrePlanningStatus.Approved) && !w.IsDeleteRequested && !string.IsNullOrEmpty(w.DeleteRejectionReason))).CountAsync();

            QueryResult<RejectTenderViewModel> queryResult = new QueryResult<RejectTenderViewModel>(await tenderList.ToListAsync(), count, criteria.PageNumber, criteria.PageSize);
            return queryResult;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedActionOfGrievence(string agencyCode, SearchCriteria criteria)
        {
            var tenderList = _context.AgencyCommunicationRequests.Where(w => w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                     && (w.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending)).Select(r => new ProcessNeedsActionViewModel()
                     {
                         TenderName = r.Tender.TenderName,
                         TenderTypeId = r.Tender.TenderTypeId,
                         TenderIdString = Util.Encrypt(r.AgencyRequestId),
                         TenderNumber = r.Tender.TenderNumber,
                         TenderReferenceNumber = r.Tender.ReferenceNumber,
                         TenderStatusId = r.Tender.TenderStatusId,
                         LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                         LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                         TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                         OffersOpeningDate = r.Tender.OffersOpeningDate,
                         AcceptanceTypeName = Resources.CommunicationRequest.DisplayInputs.EscalatePlaint,
                         ChangeRequestTypeId = 90,
                         ChangeRequestIdString = Util.Encrypt(r.AgencyRequestTypeId)
                     }).Page(criteria.PageNumber, criteria.PageSize);
            int count = await _context.AgencyCommunicationRequests.Where(w => w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                     && (w.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending)).CountAsync();

            QueryResult<ProcessNeedsActionViewModel> queryResult = new QueryResult<ProcessNeedsActionViewModel>(await tenderList.ToListAsync(), count, criteria.PageNumber, criteria.PageSize);

            return queryResult;
        }

        public async Task<int> GetTendersNeedActionOfGrievenceCount(string agencyCode)
        {
            int count = await _context.AgencyCommunicationRequests.CountAsync(w => w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                       && (w.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending));
            return count;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetPrePlanningNeedApproval(string agencyCode, SearchCriteria criteria)
        {
            var prePlanningList = await _context.PrePlannings.Where(w => w.IsActive == true && w.AgencyCode == agencyCode && (w.StatusId == (int)Enums.PrePlanningStatus.Pending || w.IsDeleteRequested == true)).Select(r => new ProcessNeedsActionViewModel()
            {
                ProjectName = r.ProjectName,
                EncyptedPrePlanningId = Util.Encrypt(r.PrePlanningId),
                InsideKSA = r.InsideKSA,
                Duration = r.Duration,
                DurationInDays = r.DurationInDays,
                DurationInMonths = r.DurationInMonths,
                DurationInYears = r.DurationInYears,
                YearQuarterId = r.YearQuarterId,
                YearQuarterName = r.YearQuarter.NameAr,
                IsDeleteRequested = r.IsDeleteRequested,
                ChangeRequestTypeId = 95,
                PrePlaningStatusId = r.StatusId
            }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return prePlanningList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedManagerApproval(SearchCriteria criteria)
        {

            var mangerBlockList = await _context.SupplierBlock.Where(w => w.IsActive == true && (w.BlockStatusId == (int)Enums.BlockStatus.ApprovedSecertary)).Select(r => new ProcessNeedsActionViewModel()
            {
                TenderIdString = Util.Encrypt(r.SupplierBlockId),

                CommercialRegistrationNo = r.CommercialRegistrationNo,
                CommercialSupplierName = r.CommercialSupplierName,
                BlockReason = r.SecretaryBlockReason,
                ChangeRequestTypeId = 96
            }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return mangerBlockList;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetBlockNeedSecretaryApproval(SearchCriteria criteria)
        {
            var blockList = await _context.SupplierBlock.Where(w => w.IsActive == true && (w.BlockStatusId == (int)Enums.BlockStatus.NewAdmin)).Select(r => new ProcessNeedsActionViewModel()
            {
                TenderIdString = Util.Encrypt(r.SupplierBlockId),
                CommercialRegistrationNo = r.CommercialRegistrationNo,
                CommercialSupplierName = r.CommercialSupplierName,
                BlockReason = r.BlockStatusId == (int)Enums.BlockStatus.NewAdmin ? r.BlockDetails : r.SecretaryBlockReason,
                ChangeRequestTypeId = 97
            }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return blockList;
        }


        public async Task<QueryResult<RejectTenderViewModel>> GetBlockSecretaryRejected(SearchCriteria criteria)
        {
            var blockList = await _context.SupplierBlock.Where(w => w.IsActive == true && (w.BlockStatusId == (int)Enums.BlockStatus.RejectedSecertary || w.BlockStatusId == (int)Enums.BlockStatus.RejectedManager)).Select(r => new RejectTenderViewModel()
            {
                TenderIdString = Util.Encrypt(r.SupplierBlockId),
                TenderNumber = r.CommercialRegistrationNo,
                TenderName = r.CommercialSupplierName,
                RejectionReason = r.BlockDetails,
                ChangeRequestTypeId = 103
            }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return blockList;
        }



        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfDirectPurchase(int committeeId, SearchCriteria criteria)
        {
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            var q1 = await _context.Tenders
                       .Where(x => x.IsActive == true && x.DirectPurchaseCommitteeId == committeeId || (x.IsLowBudgetDirectPurchase.HasValue && x.IsLowBudgetDirectPurchase.Value && x.DirectPurchaseMemberId.Value == _httpContextAccessor.HttpContext.User.UserId()))
                       .Where(x => (x.TenderTypeId == (int)Enums.TenderType.PostQualification &&
                       (x.TenderStatusId == (int)Enums.TenderStatus.Rejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckRejected)) ||
                       (x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase &&
                       (x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected ||
                          x.TenderStatusId == (int)Enums.TenderStatus.BackForCheckingFromPlaint ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected)))
                       .SortBy(criteria.Sort, criteria.SortDirection)
                       .OrderBy(x => x.TenderId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.TenderName,
                           TenderNumber = x.TenderNumber,
                           ReferenceNumber = x.ReferenceNumber,
                           TenderStatusName = x.Status.NameAr,
                           TenderStatusId = x.TenderStatusId,
                           TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           TenderTypeId = x.TenderTypeId,
                           ChangeType = Resources.TenderResources.DisplayInputs.ApprovalRejection,
                           RejectionReason = x.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason,
                       }).ToListAsync();
            var q2 = await _context.AgencyCommunicationRequests.Where(w =>
                        w.IsActive == true && w.Tender.DirectPurchaseCommitteeId == committeeId
                        && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                        && w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                      .OrderBy(x => x.SupplierExtendOfferDatesRequestId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.Tender.TenderName,
                           TenderNumber = x.Tender.TenderNumber,
                           ReferenceNumber = x.Tender.ReferenceNumber,
                           TenderStatusName = x.Status.Name,
                           TenderStatusId = x.Status.Id,
                           TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           AgencyRequestTypeId = x.AgencyRequestTypeId,
                           RejectionReason = x.RejectionReason,
                           ChangeType = x.AgencyRequestType.Name,
                           AgencyRequestIdString = Util.Encrypt(x.AgencyRequestId),
                           ChangeRequestTypeId = 83
                       }).ToListAsync();
            var q3 = await _context.TenderChangeRequests
                       .Where(x => x.IsActive == true && x.Tender.DirectPurchaseCommitteeId == committeeId)
                       .Where(x => x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && x.RequestedByRoleName == RoleNames.OffersPurchaseSecretary && x.IsActive == true)
                       .SortBy(criteria.Sort, criteria.SortDirection)
                       .OrderBy(x => x.TenderChangeRequestId)
                        .Select(x => new RejectTenderViewModel
                        {
                            TenderName = x.Tender.TenderName,
                            TenderNumber = x.Tender.TenderNumber,
                            ReferenceNumber = x.Tender.ReferenceNumber,
                            TenderStatusName = x.Tender.Status.NameAr,
                            TenderStatusId = x.Tender.TenderStatusId,
                            TenderTypeId = x.Tender.TenderTypeId,
                            TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                            TenderIdString = Util.Encrypt(x.TenderId),
                            RejectionReason = x.RejectionReason,
                            ChangeType = x.ChangeRequestType.NameAr,
                            ChangeRequestTypeId = x.ChangeRequestTypeId,
                            ChangeRequestStatuesId = x.ChangeRequestStatusId
                        })
                        .ToListAsync();
            var q4 = await _context.AgencyCommunicationRequests.Where(w =>
                         w.IsActive == true && w.Tender.DirectPurchaseCommitteeId == committeeId
                         && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
                         && w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                      .OrderBy(x => x.SupplierExtendOfferDatesRequestId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.Tender.TenderName,
                           TenderNumber = x.Tender.TenderNumber,
                           ReferenceNumber = x.Tender.ReferenceNumber,
                           TenderStatusName = x.Status.Name,
                           TenderStatusId = x.Status.Id,
                           TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           AgencyRequestTypeId = x.AgencyRequestTypeId,
                           RejectionReason = x.RejectionReason,
                           ChangeType = x.AgencyRequestType.Name,
                           AgencyRequestIdString = Util.Encrypt(x.AgencyRequestId),
                           ChangeRequestTypeId = 80
                       }).ToListAsync();
            var q6 = await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
                          Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerReject && w.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId)
                          .Select(r => new RejectTenderViewModel()
                          {
                              RejectionReason = r.RejectionReason,
                              TenderName = r.AgencyCommunicationRequest.Tender.TenderName,
                              TenderTypeId = 99,
                              TenderIdString = Util.Encrypt(r.AgencyCommunicationRequest.TenderId),
                              TenderNumber = r.AgencyCommunicationRequest.Tender.TenderNumber,
                              ReferenceNumber = r.AgencyCommunicationRequest.Tender.ReferenceNumber,
                              TenderStatusId = r.AgencyCommunicationRequest.Tender.TenderStatusId,
                              TenderStatusIdString = Util.Encrypt(r.AgencyCommunicationRequest.Tender.TenderStatusId),
                              ChangeRequestTypeId = r.NegotiationTypeId,
                              ChangeType = Resources.SharedResources.DisplayInputs.FirstStageNegotiation,
                              operationType = Enums.OperationsNeedsApproval.NegotiationFirstStage,
                              ItemIdString = Util.Encrypt(r.NegotiationId),
                              TenderStatusName = r.NegotiationStatus.Name
                          }).ToListAsync();
            var q5 = await _context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
                          Where(w =>
                         (w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerReject || w.StatusId == (int)Enums.enNegotiationStatus.UnitSpecialistReject)
                          && (w.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId || (w.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && w.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId == userId)))
                          .Select(r => new RejectTenderViewModel()
                          {
                              RejectionReason = r.RejectionReason,
                              TenderName = r.AgencyCommunicationRequest.Tender.TenderName,
                              TenderTypeId = 99,
                              TenderIdString = Util.Encrypt(r.AgencyCommunicationRequest.TenderId),
                              TenderNumber = r.AgencyCommunicationRequest.Tender.TenderNumber,
                              ReferenceNumber = r.AgencyCommunicationRequest.Tender.ReferenceNumber,
                              TenderStatusId = r.AgencyCommunicationRequest.Tender.TenderStatusId,
                              TenderStatusIdString = Util.Encrypt(r.AgencyCommunicationRequest.Tender.TenderStatusId),
                              ChangeRequestTypeId = r.NegotiationTypeId,
                              ChangeType = Resources.SharedResources.DisplayInputs.SecondStageNegotiation,
                              operationType = Enums.OperationsNeedsApproval.Negotiation,
                              ItemIdString = Util.Encrypt(r.NegotiationId),
                              TenderStatusName = r.NegotiationStatus.Name
                          }).ToListAsync();
            var data = await q1.Concat(q2).Concat(q3).Concat(q4).Concat(q5).Concat(q6).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return data;
        }


        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckQualificationStage(int committeeId, SearchCriteria criteria)
        {
            var tenders = await _context.Tenders
                       .Where(x => x.IsActive == true && x.PreQualificationCommitteeId == committeeId)
                       .Where(x => (x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification) && (x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckRejected || x.TenderStatusId == (int)Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager))
                       .SortBy(criteria.Sort, criteria.SortDirection)
                       .OrderBy(x => x.TenderId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.TenderName,
                           TenderNumber = x.TenderNumber,
                           ReferenceNumber = x.ReferenceNumber,
                           TenderStatusName = x.Status.NameAr,
                           TenderStatusId = x.TenderStatusId,
                           TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           TenderTypeId = x.TenderTypeId,
                           ChangeType = Resources.TenderResources.DisplayInputs.ApprovalRejection,
                           RejectionReason = x.TenderHistories.Where(h => h.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected || h.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected || x.TenderStatusId == (int)Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager || h.StatusId == (int)Enums.TenderStatus.DocumentCheckRejected || h.StatusId == (int)Enums.TenderStatus.Rejected && h.IsActive == true).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().RejectionReason
                       }).ToListAsync();

            var changeRequests = await _context.TenderChangeRequests
                  .Where(x => x.IsActive == true && x.Tender.PreQualificationCommitteeId == committeeId)
                  .Where(x => x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && x.RequestedByRoleName == RoleNames.PreQualificationCommitteeSecretary && x.IsActive == true)
                  .SortBy(criteria.Sort, criteria.SortDirection)
                  .OrderBy(x => x.TenderChangeRequestId)
                   .Select(x => new RejectTenderViewModel
                   {
                       TenderName = x.Tender.TenderName,
                       TenderNumber = x.Tender.TenderNumber,
                       ReferenceNumber = x.Tender.ReferenceNumber,
                       TenderStatusName = x.ChangeRequestStatus.NameAr,
                       TenderStatusId = x.Tender.TenderStatusId,
                       TenderTypeId = x.Tender.TenderTypeId,
                       TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                       TenderIdString = Util.Encrypt(x.TenderId),
                       RejectionReason = x.RejectionReason,
                       ChangeType = x.ChangeRequestType.NameAr,
                       ChangeRequestTypeId = x.ChangeRequestTypeId,
                       ChangeRequestStatuesId = x.ChangeRequestStatusId

                   }).ToListAsync();
            var communicationRequests = await _context.AgencyCommunicationRequests.Where(w =>
                         w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId &&
                         w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                         && w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                      .OrderBy(x => x.SupplierExtendOfferDatesRequestId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.Tender.TenderName,
                           TenderNumber = x.Tender.TenderNumber,
                           ReferenceNumber = x.Tender.ReferenceNumber,
                           TenderStatusName = x.Tender.Status.NameAr,
                           TenderStatusId = x.Tender.TenderStatusId,
                           TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           RejectionReason = x.RejectionReason,
                           ChangeType = x.AgencyRequestType.Name,
                           ChangeRequestTypeId = 80
                       }).ToListAsync();

            var data = await tenders.Concat(changeRequests).Concat(communicationRequests).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return data;
        }

        public async Task<int> GetFinalAwardingStageProcessNeedsActionCount()
        {
            int branchId = _httpContextAccessor.HttpContext.User.UserBranch();
            var branchUser = await _context.BranchUsers
               .Where(u => u.IsActive == true && u.EstimatedValueFrom.HasValue && u.EstimatedValueTo > 0 && u.BranchId == branchId && u.UserProfileId == _httpContextAccessor.HttpContext.User.UserId())
               .FirstOrDefaultAsync();
            if (branchUser == null)
            {
                branchUser = new BranchUser();
            }
            var tendersCount = await _context.Tenders.Where(x => x.IsActive == true && x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed)
                        .Where(x => x.BranchId == branchId && branchUser.EstimatedValueFrom.HasValue && branchUser.EstimatedValueTo.HasValue && x.EstimatedValue >= branchUser.EstimatedValueFrom && x.EstimatedValue <= branchUser.EstimatedValueTo)
                        .CountAsync();


            return tendersCount;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetFinalAwardStageProcessNeedsAction(int branchId, SearchCriteria criteria)
        {
            var branchUser = await _context.BranchUsers
                .Where(u => u.IsActive == true && u.EstimatedValueFrom.HasValue && u.EstimatedValueTo > 0 && u.BranchId == branchId && u.UserProfileId == _httpContextAccessor.HttpContext.User.UserId())
                .FirstOrDefaultAsync();
            if (branchUser == null)
            {
                return new QueryResult<ProcessNeedsActionViewModel>(null, 0, criteria.PageNumber, criteria.PageSize);
            }
            var teners = await _context.Tenders
                        .Where(x => x.IsActive == true && x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed)
                        .Where(x => x.BranchId == branchId && branchUser.EstimatedValueFrom.HasValue && branchUser.EstimatedValueTo.HasValue && x.EstimatedValue >= branchUser.EstimatedValueFrom && x.EstimatedValue <= branchUser.EstimatedValueTo)
                        .OrderBy(r => r.CreatedAt)
            .Select(a => new ProcessNeedsActionViewModel()
            {
                TenderName = a.TenderName,
                TenderIdString = Util.Encrypt(a.TenderId),
                TenderNumber = a.TenderNumber,
                TenderReferenceNumber = a.ReferenceNumber,
                TenderTypeId = a.TenderTypeId,
                TenderStatusId = a.TenderStatusId,
                LastEnqueriesDate = a.LastEnqueriesDate,
                LastOfferPresentationDate = a.LastOfferPresentationDate,
                TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
                OffersOpeningDate = a.OffersOpeningDate,
                AcceptanceTypeName = a.Status.NameAr,
                BranchId = a.BranchId,
                EstimatedValue = a.EstimatedValue
            }).ToQueryResult();
            return teners;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetVROAwardStageProcessNeedsAction(int committeeId, SearchCriteria criteria)
        {
            var q = _context.Tenders
                        .Where(x => x.IsActive == true &&
                        x.OffersCheckingCommitteeId == committeeId &&
                        (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending ||
                        x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
                        ))
                        .OrderBy(r => r.CreatedAt)
            .Select(a => new ProcessNeedsActionViewModel()
            {
                TenderName = a.TenderName,
                TenderIdString = Util.Encrypt(a.TenderId),
                TenderNumber = a.TenderNumber,
                TenderReferenceNumber = a.ReferenceNumber,
                TenderTypeId = a.TenderTypeId,
                TenderStatusId = a.TenderStatusId,
                LastEnqueriesDate = a.LastEnqueriesDate,
                LastOfferPresentationDate = a.LastOfferPresentationDate,
                TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
                OffersOpeningDate = a.OffersOpeningDate,
                AcceptanceTypeName = a.Status.NameAr,

            });

            var count = _context.Tenders
                      .Where(x => x.IsActive == true && x.OffersCheckingCommitteeId == committeeId)
                              .Where(x => x.IsActive == true &&
                        (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending ||
                        x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
                        )).Count();

            QueryResult<ProcessNeedsActionViewModel> queryResult = new QueryResult<ProcessNeedsActionViewModel>(await q.ToListAsync(), count, criteria.PageNumber, criteria.PageSize);
            return queryResult;
        }




        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchasePaginAsync(int committeeId, SearchCriteria criteria)
        {
            var q1 = await _context.Tenders
                      .Where(x => x.IsActive == true &&
                      x.DirectPurchaseCommitteeId == committeeId &&
                      ((x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase &&
                      (x.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApprovePending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending ||
                      //x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending)) ||
                      (x.TenderTypeId == (int)Enums.TenderType.PostQualification &&
                      (x.TenderStatusId == (int)Enums.TenderStatus.Pending || x.TenderStatusId == (int)Enums.TenderStatus.Approved || x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckPending))))
                      .OrderBy(r => r.CreatedAt)
          .Select(a => new ProcessNeedsActionViewModel()
          {
              TenderName = a.TenderName,
              TenderIdString = Util.Encrypt(a.TenderId),
              TenderNumber = a.TenderNumber,
              TenderReferenceNumber = a.ReferenceNumber,
              TenderTypeId = a.TenderTypeId,
              TenderStatusId = a.TenderStatusId,
              LastEnqueriesDate = a.LastEnqueriesDate,
              LastOfferPresentationDate = a.LastOfferPresentationDate,
              TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
              OffersOpeningDate = a.OffersOpeningDate,
              AcceptanceTypeName = a.Status.NameAr,
          }).ToListAsync();
            var q2 = await _context.AgencyCommunicationRequests.Where(w =>
                          w.IsActive == true && w.Tender.DirectPurchaseCommitteeId == committeeId &&
                          w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
                          && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending)).Select(r => new ProcessNeedsActionViewModel()
                          {
                              TenderName = r.Tender.TenderName,
                              TenderTypeId = r.Tender.TenderTypeId,
                              TenderIdString = Util.Encrypt(r.Tender.TenderId),
                              TenderNumber = r.Tender.TenderNumber,
                              TenderReferenceNumber = r.Tender.ReferenceNumber,
                              TenderStatusId = r.Tender.TenderStatusId,
                              LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                              LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                              TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                              OffersOpeningDate = r.Tender.OffersOpeningDate,
                              AcceptanceTypeName = Resources.CommunicationRequest.DisplayInputs.ExtendOfferValidity,
                              OperationsNeedsApprovalType = (int)OperationsNeedsApproval.ExtendOffersValidity,
                              ExtendOffersValidityIdString = Util.Encrypt(r.ExtendOffersValidity.ExtendOffersValidityId),
                              ChangeRequestIdString = Util.Encrypt(r.AgencyRequestId)
                          }).ToListAsync();
            var q3 = await _context.AgencyCommunicationRequests.Where(w =>
                           w.IsActive == true && w.Tender.DirectPurchaseCommitteeId == committeeId &&
                           w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                           && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending)).Select(r => new ProcessNeedsActionViewModel()
                           {
                               TenderName = r.Tender.TenderName,
                               TenderTypeId = r.Tender.TenderTypeId,
                               TenderIdString = Util.Encrypt(r.Tender.TenderId),
                               TenderNumber = r.Tender.TenderNumber,
                               TenderReferenceNumber = r.Tender.ReferenceNumber,
                               TenderStatusId = r.Tender.TenderStatusId,
                               LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                               LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                               TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                               OffersOpeningDate = r.Tender.OffersOpeningDate,
                               AcceptanceTypeName = Resources.CommunicationRequest.DisplayInputs.Plaint,
                               OperationsNeedsApprovalType = (int)OperationsNeedsApproval.PlaintRequest,
                               ChangeRequestIdString = Util.Encrypt(r.AgencyRequestId)
                           }).ToListAsync();
            var q4 = await _context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
                          Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerPendingApprove && w.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId).Select(r => new ProcessNeedsActionViewModel()
                          {
                              TenderName = r.AgencyCommunicationRequest.Tender.TenderName,
                              TenderTypeId = 99,
                              TenderIdString = Util.Encrypt(r.AgencyCommunicationRequest.TenderId),
                              TenderNumber = r.AgencyCommunicationRequest.Tender.TenderNumber,
                              TenderReferenceNumber = r.AgencyCommunicationRequest.Tender.ReferenceNumber,
                              TenderStatusId = r.AgencyCommunicationRequest.Tender.TenderStatusId,
                              LastEnqueriesDate = r.AgencyCommunicationRequest.Tender.LastEnqueriesDate,
                              LastOfferPresentationDate = r.AgencyCommunicationRequest.Tender.LastOfferPresentationDate,
                              TenderStatusIdString = Util.Encrypt(r.AgencyCommunicationRequest.Tender.TenderStatusId),
                              OffersOpeningDate = r.AgencyCommunicationRequest.Tender.OffersOpeningDate,
                              AcceptanceTypeName = Resources.SharedResources.DisplayInputs.SecondStageNegotiation,
                              ChangeRequestTypeId = r.NegotiationTypeId,
                              OperationsNeedsApprovalType = (int)OperationsNeedsApproval.Negotiation,
                              ChangeRequestIdString = Util.Encrypt(r.NegotiationId)

                          }).ToListAsync();
            var q5 = await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
                          Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerPendingApprove && w.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId).Select(r => new ProcessNeedsActionViewModel()
                          {
                              TenderName = r.AgencyCommunicationRequest.Tender.TenderName,
                              TenderTypeId = 99,
                              TenderIdString = Util.Encrypt(r.AgencyCommunicationRequest.TenderId),
                              TenderNumber = r.AgencyCommunicationRequest.Tender.TenderNumber,
                              TenderReferenceNumber = r.AgencyCommunicationRequest.Tender.ReferenceNumber,
                              TenderStatusId = r.AgencyCommunicationRequest.Tender.TenderStatusId,
                              LastEnqueriesDate = r.AgencyCommunicationRequest.Tender.LastEnqueriesDate,
                              LastOfferPresentationDate = r.AgencyCommunicationRequest.Tender.LastOfferPresentationDate,
                              TenderStatusIdString = Util.Encrypt(r.AgencyCommunicationRequest.Tender.TenderStatusId),
                              OffersOpeningDate = r.AgencyCommunicationRequest.Tender.OffersOpeningDate,
                              AcceptanceTypeName = Resources.SharedResources.DisplayInputs.FirstStageNegotiation,
                              ChangeRequestTypeId = r.NegotiationTypeId,
                              OperationsNeedsApprovalType = (int)OperationsNeedsApproval.NegotiationFirstStage,
                              ChangeRequestIdString = Util.Encrypt(r.NegotiationId)

                          }).ToListAsync();
            var q6 = await
                   _context.TenderChangeRequests
                      .Where(x =>
                      x.IsActive == true &&
                      x.RequestedByRoleName == RoleNames.OffersPurchaseSecretary &&
                      x.Tender.DirectPurchaseCommitteeId == committeeId && x.ChangeRequestStatusId == (int)ChangeStatusType.Pending)
                      .OrderBy(r => r.CreatedAt)
          .Select(a => new ProcessNeedsActionViewModel()
          {
              TenderName = a.Tender.TenderName,
              TenderIdString = Util.Encrypt(a.TenderId),
              TenderNumber = a.Tender.TenderNumber,
              TenderReferenceNumber = a.Tender.ReferenceNumber,
              TenderTypeId = a.Tender.TenderTypeId,
              TenderStatusId = a.Tender.TenderStatusId,
              LastEnqueriesDate = a.Tender.LastEnqueriesDate,
              LastOfferPresentationDate = a.Tender.LastOfferPresentationDate,
              TenderStatusIdString = Util.Encrypt(a.Tender.TenderStatusId),
              ChangeRequestTypeId = a.ChangeRequestTypeId,
              OffersOpeningDate = a.Tender.OffersOpeningDate,
              AcceptanceTypeName = a.ChangeRequestType.NameAr,
          }).ToListAsync();


            var data = await q1.Concat(q2).Concat(q3).Concat(q4).Concat(q5).Concat(q6).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            return data;
        }



        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync(int committeeId, SearchCriteria criteria, string agencyCode)
        {

            var q1 = await _context.TenderChangeRequests.Where(x => x.IsActive == true && x.Tender.AgencyCode == agencyCode && x.Tender.PreQualificationCommitteeId == committeeId &&
            (x.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || x.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
           && x.ChangeRequestStatusId == (int)ChangeStatusType.Pending && x.RequestedByRoleName == RoleNames.PreQualificationCommitteeSecretary)
                            .Select(r => new ProcessNeedsActionViewModel()
                            {
                                TenderName = r.Tender.TenderName,
                                TenderTypeId = r.Tender.TenderTypeId,
                                TenderIdString = Util.Encrypt(r.TenderId),
                                TenderNumber = r.Tender.TenderNumber,
                                TenderReferenceNumber = r.Tender.ReferenceNumber,
                                TenderStatusId = r.Tender.TenderStatusId,
                                LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                                LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                                TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                                OffersOpeningDate = r.Tender.OffersOpeningDate,
                                AcceptanceTypeName = r.ChangeRequestType.NameAr,
                                ChangeRequestTypeId = r.ChangeRequestTypeId
                            }).ToListAsync();
            var q2 = await _context.Tenders
                      .Where(x =>
                      x.IsActive == true &&
                      x.AgencyCode == agencyCode &&
                      x.PreQualificationCommitteeId == committeeId &&
                             (x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification) && (
                      x.TenderStatusId ==
                      (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApprovePending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.PendingQualificationCommitteeManagerApproval))
                      .OrderBy(r => r.CreatedAt)
          .Select(a => new ProcessNeedsActionViewModel()
          {
              TenderName = a.TenderName,
              TenderIdString = Util.Encrypt(a.TenderId),
              TenderNumber = a.TenderNumber,
              TenderReferenceNumber = a.ReferenceNumber,
              TenderTypeId = a.TenderTypeId,
              TenderStatusId = a.TenderStatusId,
              LastEnqueriesDate = a.LastEnqueriesDate,
              LastOfferPresentationDate = a.LastOfferPresentationDate,
              TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
              OffersOpeningDate = a.OffersOpeningDate,
              AcceptanceTypeName = a.Status.NameAr,
          }).ToListAsync();








            var result = await q1.Concat(q2).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            return result;
        }
        public async Task<int> GetTendersNeedApprovalForPreQualificationCommitteeManagerCount(int committeeId, string agencyCode)
        {
            int count = await _context.Tenders.CountAsync(x =>
x.IsActive == true &&
x.AgencyCode == agencyCode &&
x.PreQualificationCommitteeId == committeeId &&
(x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification)
&& (x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckPending || x.TenderStatusId == (int)Enums.TenderStatus.PendingQualificationCommitteeManagerApproval));


            count += await _context.TenderChangeRequests.CountAsync(x => x.IsActive == true && x.Tender.AgencyCode == agencyCode && x.Tender.PreQualificationCommitteeId == committeeId &&
             (x.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || x.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
            && x.ChangeRequestStatusId == (int)ChangeStatusType.Pending && x.RequestedByRoleName == RoleNames.PreQualificationCommitteeSecretary);

            return count;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync(int committeeId, SearchCriteria criteria)
        {
            var result = await _context.Tenders
                      .Where(x =>
                      x.IsActive == true &&
                      x.DirectPurchaseCommitteeId == committeeId &&
                      x.TenderStatusId ==
                      (int)Enums.TenderStatus.DirectPurchaseOffersCheckingRejected ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected)
                      .OrderBy(r => r.CreatedAt)
          .Select(a => new ProcessNeedsActionViewModel()
          {
              TenderName = a.TenderName,
              TenderIdString = Util.Encrypt(a.TenderId),
              TenderNumber = a.TenderNumber,
              TenderReferenceNumber = a.ReferenceNumber,
              TenderTypeId = a.TenderTypeId,
              TenderStatusId = a.TenderStatusId,
              LastEnqueriesDate = a.LastEnqueriesDate,
              LastOfferPresentationDate = a.LastOfferPresentationDate,
              TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
              OffersOpeningDate = a.OffersOpeningDate,
              AcceptanceTypeName = a.Status.NameAr,
              RejectionReason = a.TenderHistories.OrderByDescending(j => j.TenderHistoryId).FirstOrDefault().RejectionReason
          }).ToQueryResult(criteria.PageNumber, criteria.PageSize);



            return result;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync(int committeeId, SearchCriteria criteria)
        {

            var q1 = await _context.Tenders
                      .Where(x =>
                      x.IsActive == true &&
                      x.OffersCheckingCommitteeId == committeeId &&
                      x.TenderStatusId ==
                      (int)Enums.TenderStatus.OffersCheckedPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.Pending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending)
                      .OrderBy(r => r.CreatedAt)
          .Select(a => new ProcessNeedsActionViewModel()
          {
              TenderName = a.TenderName,
              TenderIdString = Util.Encrypt(a.TenderId),
              TenderNumber = a.TenderNumber,
              TenderReferenceNumber = a.ReferenceNumber,
              TenderTypeId = a.TenderTypeId,
              TenderStatusId = a.TenderStatusId,
              LastEnqueriesDate = a.LastEnqueriesDate,
              LastOfferPresentationDate = a.LastOfferPresentationDate,
              TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
              OffersOpeningDate = a.OffersOpeningDate,
              AcceptanceTypeName = a.Status.NameAr,
          }).ToListAsync();
            var q2 = await _context.TenderChangeRequests.Where(x => x.IsActive == true && x.Tender.OffersCheckingCommitteeId == committeeId && (
            x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected)
            || (x.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification && x.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved)
            && (x.RequestedByRoleName == RoleNames.OffersCheckSecretary && x.Tender.OffersCheckingCommitteeId == committeeId)
            && x.ChangeRequestStatusId == (int)ChangeStatusType.Pending)
                          .Select(r => new ProcessNeedsActionViewModel()
                          {
                              TenderName = r.Tender.TenderName,
                              TenderTypeId = r.Tender.TenderTypeId,
                              TenderIdString = Util.Encrypt(r.TenderId),
                              TenderNumber = r.Tender.TenderNumber,
                              TenderReferenceNumber = r.Tender.ReferenceNumber,
                              TenderStatusId = r.Tender.TenderStatusId,
                              LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                              LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                              TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                              OffersOpeningDate = r.Tender.OffersOpeningDate,
                              AcceptanceTypeName = r.ChangeRequestType.NameAr,
                              ChangeRequestTypeId = r.ChangeRequestTypeId
                          }).ToListAsync();




            var queryResult = await q1.Concat(q2).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return queryResult;
        }






        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForDataEntry(int branchId, SearchCriteria criteria)
        {

            var q1 = await _context.Tenders
                      .Where(x => x.IsActive == true && x.BranchId == branchId)
                      .Where(x => x.TenderStatusId == (int)Enums.TenderStatus.ReturnedFromUnitToAgencyForEdit || x.TenderStatusId == (int)Enums.TenderStatus.Rejected || x.TenderStatusId == (int)Enums.TenderStatus.RejectedFromUnit)
                      .SortBy(criteria.Sort, criteria.SortDirection)
                      .OrderBy(x => x.TenderId)
                      .Select(x => new RejectTenderViewModel
                      {
                          TenderName = x.TenderName,
                          TenderNumber = x.TenderNumber,
                          ReferenceNumber = x.ReferenceNumber,
                          TenderStatusName = x.Status.NameAr,
                          TenderStatusId = x.TenderStatusId,
                          TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                          TenderIdString = Util.Encrypt(x.TenderId),
                          RejectionReason = x.TenderHistories.Where(h => h.StatusId == (int)Enums.TenderStatus.Rejected && h.IsActive == true).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().RejectionReason,
                          ChangeType = x.TenderType.NameAr,
                          TenderTypeId = x.TenderTypeId
                      }).ToListAsync();

            var q2 = await
                _context.TenderChangeRequests
                      .Where(x => x.IsActive == true && x.Tender.BranchId == branchId)
                      .Where(x => x.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved && x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && (x.RequestedByRoleName == RoleNames.DataEntry || x.RequestedByRoleName == RoleNames.PurshaseSpecialist))
                      .SortBy(criteria.Sort, criteria.SortDirection)
                      .OrderBy(x => x.TenderChangeRequestId)
                      .Select(x => new RejectTenderViewModel
                      {
                          TenderName = x.Tender.TenderName,
                          TenderNumber = x.Tender.TenderNumber,
                          ReferenceNumber = x.Tender.ReferenceNumber,
                          TenderStatusName = x.Tender.Status.NameAr,
                          TenderStatusId = x.Tender.TenderStatusId,
                          TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                          TenderIdString = Util.Encrypt(x.TenderId),
                          RejectionReason = x.RejectionReason,
                          ChangeType = x.ChangeRequestType.NameAr,
                          ChangeRequestTypeId = x.ChangeRequestTypeId,
                          ChangeRequestStatuesId = x.ChangeRequestStatusId,
                          TenderTypeId = x.Tender.TenderTypeId
                      })
                 .ToListAsync();

            var q3 = await _context.Announcements
                    .Where(x => x.IsActive == true && x.BranchId == branchId)
                    .Where(x => x.StatusId == (int)Enums.AnnouncementStatus.Rejected)
                    .SortBy(criteria.Sort, criteria.SortDirection)
                    .OrderBy(x => x.AnnouncementId)
                    .Select(x => new RejectTenderViewModel
                    {
                        TenderName = x.AnnouncementName,
                        ReferenceNumber = x.ReferenceNumber,
                        TenderStatusName = x.AnnouncementStatus.Name,
                        AnnouncementStatusId = x.StatusId,
                        TenderStatusIdString = Util.Encrypt(x.StatusId),
                        TenderIdString = Util.Encrypt(x.AnnouncementId),
                        RejectionReason = x.AnnouncementHistories.Where(h => h.StatusId == (int)Enums.AnnouncementStatus.Rejected && h.IsActive == true).OrderByDescending(h => h.Id).FirstOrDefault().RejectionReason,
                        ChangeType = "إعلان المنافسة",
                        //TenderTypeId = 20
                    }).ToListAsync();



            var queryResult = await q1.Concat(q2).Concat(q3).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return queryResult;

        }



        public async Task<QueryResult<JoiningRequestInvitationsViewModel>> GetTendersInvitationsPagingAsync(int branchId, SearchCriteria criteria)
        {
            var tendersJoiningInvitations = await _context.Tenders
                .Where(x => x.IsActive == true && x.BranchId == branchId && x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && x.Invitations.Any(i => i.InvitationTypeId == (int)InvitationRequestType.Request && i.StatusId == (int)InvitationStatus.New))
                .OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(x => new JoiningRequestInvitationsViewModel
                {
                    TenderIdString = Util.Encrypt(x.TenderId),
                    TenderName = x.TenderName,
                    TenderNumber = x.TenderNumber,
                    TenderStatusId = x.TenderStatusId,
                    TenderTypeId = x.TenderTypeId,
                    InvitationTypeId = x.InvitationTypeId,
                    JoiningRequestInvitationNumber = x.Invitations.Where(i => i.IsActive == true && i.InvitationTypeId == (int)InvitationRequestType.Request).Count()
                })
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return tendersJoiningInvitations;
        }

        public async Task<QueryResult<Tender>> OpeningNotificationsPagingAsync(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _context.Tenders
                .Include(x => x.Invitations)
                .Where(x => x.IsActive == true && (x.TenderStatusId == (int)Enums.TenderStatus.Approved && (x.OffersOpeningDate >= DateTime.Now || x.OffersOpeningDate >= DateTime.Now.Date.AddDays(-7))))
                .Where(x => x.OffersOpeningCommitteeId == committeeId)
                .OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return tenderList;
        }

        public async Task<QueryResult<BasicTenderModel>> GetPendingEnquiries(int committeeId, int userId, SearchCriteria criteria)
        {
            var result = await _context.Tenders
                .Where(x => x.IsActive == true)
                .Where(x => (x.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == userId && c.CommitteeId == committeeId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_TechnicalCommitteeUser && c.IsActive == true)
               || x.Enquiries.Any(e => e.JoinTechnicalCommittees.Any(j => j.CommitteeId == committeeId && j.IsActive == true)))
                && x.Enquiries.Count() > 0)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new BasicTenderModel
                {
                    EnquiriesCountForTechnical = t.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == userId && t.TechnicalOrganizationId == committeeId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_TechnicalCommitteeUser && c.IsActive == true) ? t.Enquiries.Count(e => e.TenderId == t.TenderId && e.IsActive == true) : t.Enquiries.Where(e => e.JoinTechnicalCommittees.Any(c => c.Committee.CommitteeUsers.Any(x => x.UserProfileId == userId && x.IsActive == true && c.IsActive == true)) && t.IsActive == true).Count(),


                    TenderNumber = t.TenderNumber,
                    TenderReferenceNumber = t.ReferenceNumber,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    OffersOpeningDate = t.OffersOpeningDate,
                    TenderName = t.TenderName,
                    TenderTypeName = t.TenderType.NameAr,
                    TenderStatusName = t.Status.NameAr,
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : ""
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }


        #region Open Stage Actions


        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForOpeningStage(int committeeId, SearchCriteria criteria)
        {
            var q1 = await _context.Tenders
                       .Where(x => x.IsActive == true && x.OffersOpeningCommitteeId == committeeId && (x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected
                       || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected
                       || x.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected))

                       .SortBy(criteria.Sort, criteria.SortDirection)
                       .OrderBy(x => x.TenderId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.TenderName,
                           TenderNumber = x.TenderNumber,
                           ReferenceNumber = x.ReferenceNumber,
                           TenderStatusName = x.Status.NameAr,
                           TenderStatusId = x.TenderStatusId,
                           ChangeType = Resources.TenderResources.DisplayInputs.ApprovalRejection,
                           TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           RejectionReason = x.TenderHistories.Where(h => (h.StatusId == (int)Enums.TenderStatus.OffersOppenedRejected

                              || h.StatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected
                            || h.StatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
                           ) && h.IsActive == true).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().RejectionReason
                       }).ToListAsync();
            var q2 = await _context.TenderChangeRequests
                      .Where(x => x.IsActive == true && x.Tender.OffersOpeningCommitteeId == committeeId)
                      .Where(x => (x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppening
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected

                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppening
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed

                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
                      || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
                      )
                           && x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && x.RequestedByRoleName == RoleNames.OffersOppeningSecretary && x.IsActive == true)
                      .SortBy(criteria.Sort, criteria.SortDirection)
                      .OrderBy(x => x.TenderChangeRequestId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.Tender.TenderName,
                           TenderNumber = x.Tender.TenderNumber,
                           ReferenceNumber = x.Tender.ReferenceNumber,
                           TenderStatusName = x.Tender.Status.NameAr,
                           TenderStatusId = x.Tender.TenderStatusId,
                           TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           RejectionReason = x.RejectionReason,
                           ChangeType = x.ChangeRequestType.NameAr,
                           ChangeRequestTypeId = x.ChangeRequestTypeId
                       }).ToListAsync();


            var queryResult = await q1.Concat(q2).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return queryResult;

        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetTendersWaitingForApproveOppeiningReport(int committeeId, SearchCriteria criteria)
        {

            var q1 = await _context.Tenders.Where(x => x.IsActive == true && x.OffersOpeningCommitteeId == committeeId && (x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending

            || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending || x.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending))
                      .OrderBy(r => r.CreatedAt).Select(a => new ProcessNeedsActionViewModel()
                      {
                          TenderName = a.TenderName,
                          TenderIdString = Util.Encrypt(a.TenderId),
                          TenderNumber = a.TenderNumber,
                          TenderReferenceNumber = a.ReferenceNumber,
                          TenderStatusId = a.TenderStatusId,
                          LastOfferPresentationDate = a.LastOfferPresentationDate,
                          TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
                          OffersOpeningDate = a.OffersOpeningDate,
                          AcceptanceTypeName = a.Status.NameAr,

                      }).ToListAsync();
            var q2 = await _context.TenderChangeRequests.Where(r => (r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppening
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected

            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppening
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected

            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
            || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed

            ) && r.RequestedByRoleName == RoleNames.OffersOppeningSecretary && r.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && r.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && r.IsActive == true && r.Tender.OffersOpeningCommitteeId == committeeId)
                                    .OrderBy(r => r.CreatedAt).Select(r => new ProcessNeedsActionViewModel()
                                    {
                                        TenderName = r.Tender.TenderName,
                                        TenderIdString = Util.Encrypt(r.TenderId),
                                        TenderNumber = r.Tender.TenderNumber,
                                        TenderStatusId = r.Tender.TenderStatusId,
                                        TenderReferenceNumber = r.Tender.ReferenceNumber,
                                        LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                                        TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                                        OffersOpeningDate = r.Tender.OffersOpeningDate,
                                        AcceptanceTypeName = r.ChangeRequestType.NameAr,
                                        ChangeRequestTypeId = r.ChangeRequestTypeId
                                    }).ToListAsync();
            var queryResult = await q1.Concat(q2).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return queryResult;

        }


        public async Task<int> GetTendersWaitingForApproveOpeningReportCount(int committeeId)
        {

            var count = await _context.Tenders.CountAsync(x => x.IsActive == true && x.OffersOpeningCommitteeId == committeeId && (x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending


          || x.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending
          || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending

            ));
            count += await _context.TenderChangeRequests.CountAsync(r => r.IsActive == true
            && r.Tender.OffersOpeningCommitteeId == committeeId && r.RequestedByRoleName == RoleNames.OffersOppeningSecretary && r.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending
                && (r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppening
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected

                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppening
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed

                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
                || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
                ));
            return count;
        }

        public async Task<int> GetRejectedTendersCountForOpeningStage(int committeeId)
        {
            var count = await _context.Tenders.CountAsync(x => x.IsActive == true && x.OffersOpeningCommitteeId == committeeId && (x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected
            || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected || x.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected

            ));
            count += await _context.TenderChangeRequests.CountAsync(x => x.IsActive == true && x.Tender.OffersOpeningCommitteeId == committeeId && x.RequestedByRoleName == RoleNames.OffersOppeningSecretary && x.ChangeRequestStatusId == (int)ChangeStatusType.Rejected
            && (x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppening
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppening
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
            ));
            return count;
        }

        #endregion

        public async Task<QueryResult<Tender>> GetTendersWaitingForApproveCheckingReport(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _context.Tenders
                .Include(x => x.TenderType)
        .Where(x => x.IsActive == true && x.OffersCheckingCommitteeId == committeeId && x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending)
                  .OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return tenderList;
        }

        public async Task<QueryResult<Tender>> GetTendersWaitingForApproveAwardingReport(int committeeId, SearchCriteria criteria)
        {
            var tenderList = await _context.Tenders
                .Include(x => x.TenderType)
                .Where(x => x.IsActive == true && x.OffersCheckingCommitteeId == committeeId && x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending)
                  .OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return tenderList;
        }

        public async Task<int> GetProcessNeedsApprovalByStatusPaging(int branchId, string agencyCode)
        {
            int count = await _context.Tenders.CountAsync(a => (a.TenderStatusId == (int)Enums.TenderStatus.Pending
                && a.BranchId == branchId
                && a.TenderTypeId != (Int32)Enums.TenderType.PostQualification)
                ||
                (a.TenderStatusId == (int)Enums.TenderStatus.PendingVROAuditerApprove
                && a.TenderTypeId != (int)Enums.TenderType.PostQualification
                && a.Agency.VROOfficeCode == agencyCode));

            count += await _context.TenderChangeRequests.CountAsync(r => r.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved
                     && r.Tender.BranchId == branchId
                     && r.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending
                     && (r.RequestedByRoleName == RoleNames.DataEntry || r.RequestedByRoleName == RoleNames.PurshaseSpecialist)
                     && r.IsActive == true);

            count += await _context.Announcements
             .CountAsync(a => (a.StatusId == (int)Enums.AnnouncementStatus.Pending
             && a.BranchId == branchId
             && a.AgencyCode == agencyCode));

            return count;
        }
        public async Task<int> GetProcessNeedsApprovalForEtimadOfficcer(int branchId, string agencyCode)
        {
            int count = await _context.Tenders.CountAsync(a => (a.TenderStatusId == (int)Enums.TenderStatus.Pending && a.BranchId == branchId && a.TenderTypeId != (Int32)Enums.TenderType.PostQualification) || (a.TenderStatusId == (int)Enums.TenderStatus.PendingVROAuditerApprove && a.TenderTypeId != (int)Enums.TenderType.PostQualification && a.Agency.VROOfficeCode == agencyCode))
            + await _context.TenderChangeRequests.CountAsync(r => r.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved
                     && r.Tender.BranchId == branchId && r.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && (r.RequestedByRoleName == RoleNames.DataEntry || r.RequestedByRoleName == RoleNames.PurshaseSpecialist) && r.IsActive == true);
            return count;
        }
        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetProcessNeedsApprovalByStatusPaging(int branchId, string agencyCode, SearchCriteria criteria)
        {
            var q1 = await _context.Tenders
                   .Where(a => (a.TenderStatusId == (int)Enums.TenderStatus.Pending
                   && a.BranchId == branchId
                   && a.TenderTypeId != (Int32)Enums.TenderType.PostQualification)
                   ||
                   (a.TenderStatusId == (int)Enums.TenderStatus.PendingVROAuditerApprove
                   && a.TenderTypeId != (int)Enums.TenderType.PostQualification
                   && a.Agency.VROOfficeCode == agencyCode))
                   .OrderByDescending(r => r.CreatedAt).Select(a => new ProcessNeedsActionViewModel()
                   {
                       TenderName = a.TenderName,
                       TenderIdString = Util.Encrypt(a.TenderId),
                       TenderNumber = a.TenderNumber,
                       TenderStatusId = a.TenderStatusId,
                       LastEnqueriesDate = a.LastEnqueriesDate,
                       LastOfferPresentationDate = a.LastOfferPresentationDate,
                       TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
                       OffersOpeningDate = a.OffersOpeningDate,
                       AcceptanceTypeName = a.Status.NameAr,
                       TenderTypeId = a.TenderTypeId,
                       TenderReferenceNumber = a.ReferenceNumber,
                       CreatedAt = a.CreatedAt

                   }).ToListAsync();



            var q2 = await
                _context.TenderChangeRequests
                     .Where(r => r.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved
                     && r.Tender.BranchId == branchId
                     && r.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending
                     && (r.RequestedByRoleName == RoleNames.DataEntry || r.RequestedByRoleName == RoleNames.PurshaseSpecialist)
                     && r.IsActive == true)
                     .OrderByDescending(r => r.CreatedAt).Select(r => new ProcessNeedsActionViewModel()
                     {
                         TenderName = r.Tender.TenderName,
                         TenderIdString = Util.Encrypt(r.TenderId),
                         TenderReferenceNumber = r.Tender.ReferenceNumber,
                         TenderNumber = r.Tender.TenderNumber,
                         TenderStatusId = r.Tender.TenderStatusId,
                         LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                         LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                         TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                         OffersOpeningDate = r.Tender.OffersOpeningDate,
                         AcceptanceTypeName = r.ChangeRequestType.NameAr,
                         ChangeRequestTypeId = r.ChangeRequestTypeId,
                         TenderTypeId = r.Tender.TenderTypeId,
                         CreatedAt = r.Tender.CreatedAt
                     }).ToListAsync();

            var q3 = await _context.Announcements
                   .Where(a => (a.StatusId == (int)Enums.AnnouncementStatus.Pending
                   && a.BranchId == branchId
                   && a.AgencyCode == agencyCode))
                   .OrderByDescending(r => r.CreatedAt).Select(a => new ProcessNeedsActionViewModel()
                   {
                       TenderName = a.AnnouncementName,
                       TenderIdString = Util.Encrypt(a.AnnouncementId),
                       TenderStatusId = a.StatusId,
                       LastOfferPresentationDate = a.PublishedDate.Value.AddDays(a.AnnouncementPeriod),
                       TenderStatusIdString = Util.Encrypt(a.StatusId),
                       AcceptanceTypeName = a.AnnouncementStatus.Name,
                       TenderTypeId = a.TenderTypeId,
                       TenderReferenceNumber = a.ReferenceNumber,
                       CreatedAt = a.CreatedAt,
                       IsAnnouncement = true
                   }).ToListAsync();

            var finaldata = await (q1.Concat(q2).Concat(q3)).OrderByDescending(x => x.CreatedAt).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return finaldata;
        }


        public async Task<QueryResult<Tender>> GetQualificationProcessNeedsApprovalByStatusPagingAsync(int branchId, SearchCriteria criteria)
        {
            var tenderList = await _context.Tenders
                .Include(x => x.TenderType).Include(r => r.ChangeRequests)
                .Where(x => x.IsActive == true && x.BranchId == branchId)
                .Where(s => s.TenderTypeId == (int)Enums.TenderType.PreQualification)
                .Where(x => x.TenderStatusId == (int)Enums.TenderStatus.Pending)
                .Where(w => w.IsActive == true)
                .OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return tenderList;
        }

        public async Task<QueryResult<Tender>> GetUnderEstablishedTendersAsync(int branchId, SearchCriteria criteria)
        {
            var tenderList = await _context.Tenders
                .Include(x => x.TenderType)
                .Where(x => x.IsActive == true && x.BranchId == branchId)
                .Where(x => x.TenderStatusId == (int)Enums.TenderStatus.Established || x.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
                .OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return tenderList;
        }

        public async Task<SalesSummaryViewModel> SalesSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            SalesSummaryViewModel resultModel = new SalesSummaryViewModel();
            DateTime startdate = GetStartDate(criteria.Duration);
            var resultList = await _context.Tenders
                .Include(a => a.Branch)
                .Include(x => x.Agency)
                .Include(x => x.ConditionsBooklets)
                .ThenInclude(x => x.BillInfo)
                .Where(x => x.IsActive == true && (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed))
                .WhereIf(!string.IsNullOrEmpty(criteria.Duration), x => x.CreatedAt >= startdate || x.UpdatedAt >= startdate)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode).OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToListAsync();

            var groupedByResult = await resultList
            .GroupBy(a => a.Branch)
            .Select(a => new SalesListViewModel()
            {
                AgencyName = a.FirstOrDefault().Branch.BranchName,
                Count = a.Count(),
                Price = a.Select(c => c.ConditionsBooklets.Select(b => b.BillInfo.AmountDue).Sum()).Sum()
            })
            .ToQueryResult(criteria.PageNumber, criteria.PageSize);


            resultModel.Sales = groupedByResult;
            resultModel.Total = groupedByResult.TotalCount;
            resultModel.PriceTotal = resultList.Select(x => x.ConditionsBooklets.Select(c => c.BillInfo.AmountDue).Sum()).Sum();
            return resultModel;

        }

        public async Task<TendersSummaryViewModel> TendersSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            TendersSummaryViewModel resultModel = new TendersSummaryViewModel();
            DateTime startdate = GetStartDate(criteria.Duration);
            var resultList = await _context.Tenders
                    .Include(a => a.Branch)
                .Where(x => x.IsActive == true && (x.TenderStatusId != (int)Enums.TenderStatus.Rejected))
                .WhereIf(!string.IsNullOrEmpty(criteria.Duration), x => x.CreatedAt >= startdate || x.UpdatedAt >= startdate)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode).OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection).ToListAsync();


            resultModel.Tenders = await resultList.GroupBy(x => x.Branch)
                    .Select(g => new TendersListViewModel { AgencyName = g.FirstOrDefault().Branch.BranchName, Count = g.Count() })
                    .ToQueryResult(criteria.PageNumber, criteria.PageSize);

            resultModel.Total = resultList.Count();
            return resultModel;
        }

        public async Task<DirectInvitationsSummaryViewModel> DirectInvitationsSummaryPagingAsync(DashboardSearchCriteria criteria)
        {
            DirectInvitationsSummaryViewModel resultModel = new DirectInvitationsSummaryViewModel();
            DateTime startdate = GetStartDate(criteria.Duration);
            var resultList = await _context.Invitations
                .Include(x => x.Tender)
                .ThenInclude(x => x.Agency)
                .Where(x => x.IsActive == true && (x.StatusId == (int)InvitationStatus.Approved))
                .WhereIf(!string.IsNullOrEmpty(criteria.Duration), x => x.CreatedAt >= startdate || x.UpdatedAt >= startdate)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.Tender.AgencyCode == criteria.AgencyCode).OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection).ToListAsync();

            resultModel.DirectInvitations = (await resultList.GroupBy(x => x.Tender.Agency.NameArabic)
            .Select(g => new DirectInvitationsListViewModel
            {
                AgencyName = g.Key,
                Count = g.Count(i => i.InvitationTypeId == (int)Enums.InvitationType.Public),
                InvitedSuppliers = g.Count(i => i.InvitationTypeId == (int)Enums.InvitationType.Specific)
            })
            .ToQueryResult(criteria.PageNumber, criteria.PageSize));

            resultModel.Total = resultList.Count();
            return resultModel;
        }

        public async Task<string> SuppliersCountPagingAsync(DashboardSearchCriteria criteria)
        {
            var reslut = await _context.Tenders
                .Where(x => x.IsActive == true && (x.TenderStatusId == (int)Enums.TenderStatus.Approved && x.OffersOpeningDate >= DateTime.Now))
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
                .OrderBy(x => x.TenderId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .CountAsync();
            return reslut.ToString();
        }

        public async Task<List<LastTenPurshaseModel>> GetLastTenPurshase(int branchId, string agencyCode)
        {
            var result = await _context.ConditionsBooklets.Include(b => b.BillInfo)
                .Where(x => x.IsActive == true && x.BillInfo.BillStatusId == (int)BillStatus.Paid)
                .WhereIf(agencyCode != "" && branchId == 0, x => x.Tender.AgencyCode == agencyCode)
                .WhereIf(branchId != 0, x => x.Tender.BranchId == branchId)
                .Select(t => new LastTenPurshaseModel
                {
                    SupplierName = t.Supplier.SelectedCrName,
                    TenderName = t.Tender.TenderName,
                    PurshaseDate = t.BillInfo.PurchaseDate ?? null,
                    PurshaseTime = t.BillInfo.PurchaseDate.HasValue ? t.BillInfo.PurchaseDate.Value.ToShortTimeString() : null,
                    CommericalRegisterNo = t.CommericalRegisterNo

                }).OrderByDescending(x => x.PurshaseDate).Take(10).ToListAsync();
            return result;
        }

        public async Task<QueryResult<Enquiry>> GetSuppliersEnquiry(int branchId, SearchCriteria criteria)
        {
            var result = await _context.Enquiries
                .Include(x => x.Tender)
                .Include(x => x.Supplier)
                .Where(x => x.IsActive == true && x.Tender.BranchId == branchId).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        private DateTime GetStartDate(string Duration)
        {
            var startdate = DateTime.Now;
            if (!string.IsNullOrEmpty(Duration))
            {
                if (Duration == "day")
                    startdate = startdate.AddDays(-(int)Durations.OneDay);
                else if (Duration == "month")
                    startdate = startdate.AddMonths(-(int)Durations.OneMonth);
                else if (Duration == "sixmonths")
                    startdate = startdate.AddMonths(-(int)Durations.SixMonth);
            }
            return startdate;
        }



        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetOpeningStageProcessNeedsActionPagingAsync(int branchId, SearchCriteria criteria, params int[] statues)
        {
            var tenderList = await _context.Tenders
               .Where(x => x.IsActive == true && x.BranchId == branchId && statues.Contains(x.TenderStatusId))
               .OrderBy(x => x.TenderId)
               .SortBy(criteria.Sort, criteria.SortDirection)
               .Select(x => new ProcessNeedsActionViewModel
               {
                   AcceptanceTypeName = x.Status.NameAr,
                   LastEnqueriesDate = x.LastEnqueriesDate,
                   LastOfferPresentationDate = x.LastOfferPresentationDate,
                   OffersOpeningDate = x.OffersOpeningDate,
                   TenderName = x.TenderName,
                   TenderNumber = x.TenderNumber,
                   TenderIdString = Util.Encrypt(x.TenderId),
                   TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                   TenderStatusId = x.TenderStatusId,
                   TenderReferenceNumber = x.ReferenceNumber
               }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return tenderList;
        }
        public async Task<int> GetRejectedTendersCountForDataEntry(int branchId)
        {

            int count = await _context.Tenders
                      .CountAsync(x => (x.IsActive == true && x.BranchId == branchId)
                      && (x.TenderStatusId == (int)Enums.TenderStatus.ReturnedFromUnitToAgencyForEdit || x.TenderStatusId == (int)Enums.TenderStatus.Rejected || x.TenderStatusId == (int)Enums.TenderStatus.RejectedFromUnit));

            count += await _context.TenderChangeRequests
                      .CountAsync(x => x.IsActive == true && x.Tender.BranchId == branchId
                      && (x.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved && x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && (x.RequestedByRoleName == RoleNames.DataEntry || x.RequestedByRoleName == RoleNames.PurshaseSpecialist)));

            count += await _context.Announcements
            .CountAsync(x => x.IsActive == true && x.BranchId == branchId
            && x.StatusId == (int)Enums.AnnouncementStatus.Rejected);

            return count;
        }

        public async Task<int> GetTendersInvitationsCount(int branchId)
        {
            var count = await _context.Tenders
                .Where(x => x.IsActive == true && x.BranchId == branchId && x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
                .Where(x => x.Invitations.Any(i => i.InvitationTypeId == (int)InvitationRequestType.Request && i.StatusId == (int)InvitationStatus.New))
                .CountAsync();
            return count;
        }

        public async Task<int> GetLastTenPurshaseCount(int branchId, string agencyCode)
        {
            var count = await _context.ConditionsBooklets.Include(b => b.BillInfo)
                .Where(x => x.IsActive == true && x.BillInfo.BillStatusId == (int)BillStatus.Paid)
                .WhereIf(agencyCode != "" && branchId == 0, x => x.Tender.AgencyCode == agencyCode)
                .WhereIf(branchId != 0, x => x.Tender.BranchId == branchId)
                .CountAsync();
            return count;
        }

        public async Task<int> GetUnderEstablishedTendersCount(int branchId)
        {
            int count = await _context.Tenders

                .Where(x => x.IsActive == true && x.BranchId == branchId)
                .Where(x => x.TenderStatusId == (int)Enums.TenderStatus.Established || x.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
                .CountAsync();
            return count;
        }


        public async Task<int> GetRejectedEscalationsPaging(string agencyCode)
        {
            int count = await _context.AgencyCommunicationRequests.Where(w =>
                        w.IsActive == true
                                                && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                        && w.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected).CountAsync();
            return count;
        }

        #region Check Stage Actions

        public async Task<int> GetCheckingAndAwardingStageProcessNeedsActionCount(int committeeId)
        {
            var count = await _context.Tenders
                        .Where(x => x.IsActive == true && x.OffersCheckingCommitteeId == committeeId)
                        .Where(x => (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending)
                        || (x.TenderTypeId == (int)Enums.TenderType.PostQualification && x.TenderStatusId == (int)Enums.TenderStatus.Pending))
                        .OrderBy(r => r.CreatedAt).CountAsync();

            count += await _context.TenderChangeRequests.CountAsync(x => x.IsActive == true
            && x.Tender.OffersCheckingCommitteeId == committeeId &&
            ((x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalChecking
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected)
             || (x.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification && x.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved))
             && x.RequestedByRoleName == RoleNames.OffersCheckSecretary && x.Tender.OffersCheckingCommitteeId == committeeId
             && x.ChangeRequestStatusId == (int)ChangeStatusType.Pending);
            count += await _context.NegotiationFirstStages
                          .CountAsync(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerPendingApprove
                           && w.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId
);
            count += await _context.NegotiationSecondStages.
                          CountAsync(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerPendingApprove && w.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId);
            count += await _context.AgencyCommunicationRequests.CountAsync(w =>
                            w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId &&
                            w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                            && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending));
            count += await _context.AgencyCommunicationRequests.CountAsync(w =>
                            w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId &&
                            w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
                            && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending));

            return count;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetCheckingAndAwardingStageProcessNeedsAction(int committeeId, SearchCriteria criteria)
        {
            var q1 = await _context.Tenders
                        .Where(x => x.IsActive == true && x.OffersCheckingCommitteeId == committeeId)
                        .Where(x => (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedPending
                        || x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending)
                        || (x.TenderTypeId == (int)Enums.TenderType.PostQualification && x.TenderStatusId == (int)Enums.TenderStatus.Pending))
                        .OrderBy(r => r.CreatedAt)
            .Select(a => new ProcessNeedsActionViewModel()
            {
                TenderName = a.TenderName,
                TenderIdString = Util.Encrypt(a.TenderId),
                TenderNumber = a.TenderNumber,
                TenderReferenceNumber = a.ReferenceNumber,
                TenderTypeId = a.TenderTypeId,
                TenderStatusId = a.TenderStatusId,
                LastEnqueriesDate = a.LastEnqueriesDate,
                LastOfferPresentationDate = a.LastOfferPresentationDate,
                TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
                OffersOpeningDate = a.OffersOpeningDate,
                AcceptanceTypeName = a.Status.NameAr,
            }).ToListAsync();
            var q2 = await _context.TenderChangeRequests.Where(x => x.IsActive == true
            && x.Tender.OffersCheckingCommitteeId == committeeId
            && ((x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalChecking
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected)
            || (x.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification && x.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved))
            && x.RequestedByRoleName == RoleNames.OffersCheckSecretary && x.Tender.OffersCheckingCommitteeId == committeeId
            && x.ChangeRequestStatusId == (int)ChangeStatusType.Pending)
                          .Select(r => new ProcessNeedsActionViewModel()
                          {

                              TenderName = r.Tender.TenderName,
                              TenderTypeId = r.Tender.TenderTypeId,
                              TenderIdString = Util.Encrypt(r.TenderId),
                              TenderNumber = r.Tender.TenderNumber,
                              TenderReferenceNumber = r.Tender.ReferenceNumber,
                              TenderStatusId = r.Tender.TenderStatusId,
                              LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                              LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                              TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                              OffersOpeningDate = r.Tender.OffersOpeningDate,
                              AcceptanceTypeName = r.ChangeRequestType.NameAr,
                              ChangeRequestTypeId = r.ChangeRequestTypeId
                          }).ToListAsync();
            var q3 = await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender)
            .Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerPendingApprove
             && w.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId)
            .Select(r => new ProcessNeedsActionViewModel()
            {
                TenderName = r.AgencyCommunicationRequest.Tender.TenderName,
                TenderTypeId = 99,
                TenderIdString = Util.Encrypt(r.AgencyCommunicationRequest.TenderId),
                TenderNumber = r.AgencyCommunicationRequest.Tender.TenderNumber,
                TenderReferenceNumber = r.AgencyCommunicationRequest.Tender.ReferenceNumber,
                TenderStatusId = r.AgencyCommunicationRequest.Tender.TenderStatusId,
                LastEnqueriesDate = r.AgencyCommunicationRequest.Tender.LastEnqueriesDate,
                LastOfferPresentationDate = r.AgencyCommunicationRequest.Tender.LastOfferPresentationDate,
                TenderStatusIdString = Util.Encrypt(r.AgencyCommunicationRequest.Tender.TenderStatusId),
                OffersOpeningDate = r.AgencyCommunicationRequest.Tender.OffersOpeningDate,
                AcceptanceTypeName = Resources.CommunicationRequest.DisplayInputs.NegotiationFirstStage,
                ChangeRequestTypeId = r.NegotiationTypeId,
                OperationsNeedsApprovalType = (int)OperationsNeedsApproval.NegotiationFirstStage,
                ChangeRequestIdString = Util.Encrypt(r.NegotiationId),
                IsNewNegotiation = r.IsNewNegotiation
            }).ToListAsync();
            var q4 = await _context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
            Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerPendingApprove && w.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId).Select(r => new ProcessNeedsActionViewModel()
            {
                TenderName = r.AgencyCommunicationRequest.Tender.TenderName,
                TenderTypeId = 99,
                TenderIdString = Util.Encrypt(r.AgencyCommunicationRequest.TenderId),
                TenderNumber = r.AgencyCommunicationRequest.Tender.TenderNumber,
                TenderReferenceNumber = r.AgencyCommunicationRequest.Tender.ReferenceNumber,
                TenderStatusId = r.AgencyCommunicationRequest.Tender.TenderStatusId,
                LastEnqueriesDate = r.AgencyCommunicationRequest.Tender.LastEnqueriesDate,
                LastOfferPresentationDate = r.AgencyCommunicationRequest.Tender.LastOfferPresentationDate,
                TenderStatusIdString = Util.Encrypt(r.AgencyCommunicationRequest.Tender.TenderStatusId),
                OffersOpeningDate = r.AgencyCommunicationRequest.Tender.OffersOpeningDate,
                AcceptanceTypeName = Resources.SharedResources.DisplayInputs.SecondStageNegotiation,
                ChangeRequestTypeId = r.NegotiationTypeId,
                OperationsNeedsApprovalType = (int)OperationsNeedsApproval.Negotiation,
                ChangeRequestIdString = Util.Encrypt(r.NegotiationId)
            }).ToListAsync();
            var q5 = await _context.AgencyCommunicationRequests.Where(w =>
             w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId &&
             w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
             && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending)).Select(r => new ProcessNeedsActionViewModel()
             {
                 TenderName = r.Tender.TenderName,
                 TenderTypeId = r.Tender.TenderTypeId,
                 TenderIdString = Util.Encrypt(r.Tender.TenderId),
                 TenderNumber = r.Tender.TenderNumber,
                 TenderReferenceNumber = r.Tender.ReferenceNumber,
                 TenderStatusId = r.Tender.TenderStatusId,
                 LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                 LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                 TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                 OffersOpeningDate = r.Tender.OffersOpeningDate,
                 AcceptanceTypeName = Resources.CommunicationRequest.DisplayInputs.Plaint,
                 OperationsNeedsApprovalType = (int)OperationsNeedsApproval.PlaintRequest,
                 ChangeRequestIdString = Util.Encrypt(r.AgencyRequestId)
             }).ToListAsync();



            var q6 = await _context.AgencyCommunicationRequests.Where(w =>
             w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId &&
             w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
             && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending)).Select(r => new ProcessNeedsActionViewModel()
             {
                 TenderName = r.Tender.TenderName,
                 TenderTypeId = r.Tender.TenderTypeId,
                 TenderIdString = Util.Encrypt(r.Tender.TenderId),
                 TenderNumber = r.Tender.TenderNumber,
                 TenderReferenceNumber = r.Tender.ReferenceNumber,
                 TenderStatusId = r.Tender.TenderStatusId,
                 LastEnqueriesDate = r.Tender.LastEnqueriesDate,
                 LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                 TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                 OffersOpeningDate = r.Tender.OffersOpeningDate,
                 AcceptanceTypeName = Resources.CommunicationRequest.DisplayInputs.ExtendOfferValidity,
                 OperationsNeedsApprovalType = (int)OperationsNeedsApproval.ExtendOffersValidity,
                 ExtendOffersValidityIdString = Util.Encrypt(r.ExtendOffersValidity != null ? r.ExtendOffersValidity.ExtendOffersValidityId : 0),
                 ChangeRequestIdString = Util.Encrypt(r.AgencyRequestId)
             }).ToListAsync();

            var queryResult = await q1.Concat(q2).Concat(q3).Concat(q4).Concat(q5).Concat(q6).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return queryResult;

        }

        public async Task<int> GetRejectedTendersCountOfCheckAndAwardingStage(int committeeId)
        {
            var count = await _context.Tenders.Where(x => x.IsActive == true && x.OffersCheckingCommitteeId == committeeId)
                       .Where(x => (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected) ||
                       x.TenderStatusId == (int)Enums.TenderStatus.BackForAwardingFromPlaint ||
                       x.TenderStatusId == (int)Enums.TenderStatus.BackForCheckingFromPlaint ||
                       (x.TenderTypeId == (int)Enums.TenderType.PostQualification && (x.TenderStatusId == (int)Enums.TenderStatus.Rejected || x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckRejected)) ||
                       x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckRejected).CountAsync();
            count += await _context.TenderChangeRequests.CountAsync(x => x.IsActive == true && x.Tender.OffersCheckingCommitteeId == committeeId && x.RequestedByRoleName == RoleNames.OffersCheckSecretary && x.ChangeRequestStatusId == (int)ChangeStatusType.Rejected
         && (x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalChecking
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected));
            count += await _context.AgencyCommunicationRequests.Where(w =>
                          w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId &&
                          w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                          && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)).CountAsync();
            count += await _context.AgencyCommunicationRequests.Where(w =>
                         w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId
                         && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
                         && w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected).CountAsync();
            count += await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender)
                          .Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerReject
                           && w.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId).CountAsync();
            count += await _context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
                          Where(w => (w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerReject || w.StatusId == (int)Enums.enNegotiationStatus.UnitSpecialistReject) && w.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId).CountAsync();
            return count;
        }

        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersOfCheckAndAwardingStage(int committeeId, SearchCriteria criteria)
        {
            var q1 = await _context.Tenders
                       .Where(x => x.IsActive == true && x.OffersCheckingCommitteeId == committeeId)
                       .Where(x => (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected) ||
                       x.TenderStatusId == (int)Enums.TenderStatus.BackForAwardingFromPlaint ||
                        x.TenderStatusId == (int)Enums.TenderStatus.BackForCheckingFromPlaint ||
                       (x.TenderTypeId == (int)Enums.TenderType.PostQualification && x.TenderStatusId == (int)Enums.TenderStatus.Rejected)) /*Prequalification*/

                       .SortBy(criteria.Sort, criteria.SortDirection)
                       .OrderBy(x => x.TenderId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.TenderName,
                           TenderNumber = x.TenderNumber,
                           ReferenceNumber = x.ReferenceNumber,
                           TenderStatusName = x.Status.NameAr,
                           TenderStatusId = x.TenderStatusId,
                           TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           TenderTypeId = x.TenderTypeId,
                           ChangeType = /*Resources.TenderResources.DisplayInputs.ApprovalRejection,*/x.TenderType.NameAr,
                           RejectionReason = x.TenderHistories.Where(h => h.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected || h.StatusId == (int)Enums.TenderStatus.DocumentCheckRejected || h.StatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || h.StatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected || h.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected || h.StatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected || h.StatusId == (int)Enums.TenderStatus.Rejected && h.IsActive == true).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().RejectionReason
                       }).ToListAsync();
            var q2 = await _context.AgencyCommunicationRequests.Where(w =>
                        w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId
                        && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
                        && w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                      .OrderBy(x => x.SupplierExtendOfferDatesRequestId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.Tender.TenderName,
                           TenderNumber = x.Tender.TenderNumber,
                           ReferenceNumber = x.Tender.ReferenceNumber,
                           TenderStatusName = x.Status.Name,
                           TenderStatusId = x.Status.Id,
                           TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           AgencyRequestTypeId = x.AgencyRequestTypeId,
                           RejectionReason = x.RejectionReason,
                           ChangeType = x.AgencyRequestType.Name,
                           AgencyRequestIdString = Util.Encrypt(x.AgencyRequestId),
                           ChangeRequestTypeId = 80
                       }).ToListAsync();

            var q3 = await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender)
                          .Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerReject
                           && w.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId)
                          .Select(r => new RejectTenderViewModel()
                          {
                              RejectionReason = r.RejectionReason,
                              TenderName = r.AgencyCommunicationRequest.Tender.TenderName,
                              TenderTypeId = 99,
                              TenderIdString = Util.Encrypt(r.AgencyCommunicationRequest.TenderId),
                              TenderNumber = r.AgencyCommunicationRequest.Tender.TenderNumber,
                              ReferenceNumber = r.AgencyCommunicationRequest.Tender.ReferenceNumber,
                              TenderStatusId = r.AgencyCommunicationRequest.Tender.TenderStatusId,
                              TenderStatusIdString = Util.Encrypt(r.AgencyCommunicationRequest.Tender.TenderStatusId),
                              ChangeRequestTypeId = r.NegotiationTypeId,
                              ChangeType = Resources.SharedResources.DisplayInputs.FirstStageNegotiation,
                              operationType = Enums.OperationsNeedsApproval.NegotiationFirstStage,
                              ItemIdString = Util.Encrypt(r.NegotiationId),
                              TenderStatusName = r.NegotiationStatus.Name,
                              IsNewNegotiation = r.IsNewNegotiation
                          }).ToListAsync();

            var q4 = await _context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
                          Where(w => (w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerReject || w.StatusId == (int)Enums.enNegotiationStatus.UnitSpecialistReject) && w.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId)

                          .Select(r => new RejectTenderViewModel()
                          {
                              RejectionReason = r.RejectionReason,
                              TenderName = r.AgencyCommunicationRequest.Tender.TenderName,
                              TenderTypeId = 99,
                              TenderIdString = Util.Encrypt(r.AgencyCommunicationRequest.TenderId),
                              TenderNumber = r.AgencyCommunicationRequest.Tender.TenderNumber,
                              ReferenceNumber = r.AgencyCommunicationRequest.Tender.ReferenceNumber,
                              TenderStatusId = r.AgencyCommunicationRequest.Tender.TenderStatusId,
                              TenderStatusIdString = Util.Encrypt(r.AgencyCommunicationRequest.Tender.TenderStatusId),
                              ChangeRequestTypeId = r.NegotiationTypeId,
                              ChangeType = Resources.SharedResources.DisplayInputs.SecondStageNegotiation,
                              operationType = Enums.OperationsNeedsApproval.Negotiation,
                              ItemIdString = Util.Encrypt(r.NegotiationId),
                              TenderStatusName = r.NegotiationStatus.Name
                          }).ToListAsync();

            var q5 = await _context.AgencyCommunicationRequests.Where(w =>
                        w.IsActive == true && w.Tender.OffersCheckingCommitteeId == committeeId
                        && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                        && w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                      .OrderByDescending(x => x.UpdatedAt)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.Tender.TenderName,
                           TenderNumber = x.Tender.TenderNumber,
                           ReferenceNumber = x.Tender.ReferenceNumber,
                           TenderStatusName = x.Status.Name,
                           TenderStatusId = x.Status.Id,
                           TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           AgencyRequestTypeId = x.AgencyRequestTypeId,
                           RejectionReason = x.RejectionReason,
                           ChangeType = x.AgencyRequestType.Name,
                           AgencyRequestIdString = Util.Encrypt(x.AgencyRequestId)
                       }).ToListAsync();

            var q6 = await
                 _context.TenderChangeRequests
                       .Where(x => x.IsActive == true && x.Tender.OffersCheckingCommitteeId == committeeId)
                       .Where(x => (x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalChecking
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved

            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed
            || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected)
                                   && x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && x.RequestedByRoleName == RoleNames.OffersCheckSecretary)
                       .SortBy(criteria.Sort, criteria.SortDirection)
                       .OrderBy(x => x.TenderChangeRequestId)
                        .Select(x => new RejectTenderViewModel
                        {
                            TenderName = x.Tender.TenderName,
                            TenderNumber = x.Tender.TenderNumber,
                            ReferenceNumber = x.Tender.ReferenceNumber,
                            TenderStatusName = x.Tender.Status.NameAr,
                            TenderStatusId = x.Tender.TenderStatusId,
                            TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                            TenderIdString = Util.Encrypt(x.TenderId),
                            RejectionReason = x.RejectionReason,
                            ChangeType = x.ChangeRequestType.NameAr,
                            ChangeRequestTypeId = x.ChangeRequestTypeId,
                            ChangeRequestStatuesId = x.ChangeRequestStatusId
                        }).ToListAsync();
            var queryResult = q1.Concat(q2).Concat(q3).Concat(q4).Concat(q5).Concat(q6).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return await queryResult;
        }

        public async Task<int> GetRejectedTendersCountOfDirectPurchaseCheckAndAwardingStage(int committeeId)
        {
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            int count = await _context.Tenders
                       .Where(x => x.IsActive == true && x.DirectPurchaseCommitteeId == committeeId || (x.IsLowBudgetDirectPurchase.HasValue && x.IsLowBudgetDirectPurchase.Value && x.DirectPurchaseMemberId.Value == userId))
                       .Where(x => (x.TenderTypeId == (int)Enums.TenderType.PostQualification &&
                       (x.TenderStatusId == (int)Enums.TenderStatus.Rejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckRejected)) ||
                       (x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase &&
                       (x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected ||
                       x.TenderStatusId == (int)Enums.TenderStatus.BackForCheckingFromPlaint ||
                       x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected)))
                       .CountAsync();

            count += await _context.AgencyCommunicationRequests.Where(w =>
                        w.IsActive == true && w.Tender.DirectPurchaseCommitteeId == committeeId
                        && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                        && w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                       .CountAsync();

            count += await _context.TenderChangeRequests
                       .Where(x => x.IsActive == true && x.Tender.DirectPurchaseCommitteeId == committeeId)
                       .Where(x => x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && x.RequestedByRoleName == RoleNames.OffersPurchaseSecretary && x.IsActive == true)
                        .CountAsync();

            count += await _context.AgencyCommunicationRequests.Where(w =>
                         w.IsActive == true && w.Tender.DirectPurchaseCommitteeId == committeeId
                         && w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
                         && w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                       .CountAsync();

            count += await _context.NegotiationFirstStages.
                          Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerReject && w.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId)
                          .CountAsync();

            count += await _context.NegotiationSecondStages.
                          Where(w =>
                          (w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerReject || w.StatusId == (int)Enums.enNegotiationStatus.UnitSpecialistReject)
                          && (w.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId || (w.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && w.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId == userId)))
                          .CountAsync();
            return count;
        }

        public async Task<int> GetTendersNeedApprovalForDirectPurchaseCountAsync(int committeeId)
        {
            int count = await _context.Tenders
                      .CountAsync(x => x.IsActive == true && x.DirectPurchaseCommitteeId == committeeId &&
                      ((x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase &&
                      (x.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApprovePending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending ||
                      x.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedPending)) ||
                      (x.TenderTypeId == (int)Enums.TenderType.PostQualification &&
                      (x.TenderStatusId == (int)Enums.TenderStatus.Pending || x.TenderStatusId == (int)Enums.TenderStatus.Approved || x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckPending))));
            count += await _context.AgencyCommunicationRequests.CountAsync(w =>
                                w.IsActive == true && w.Tender.DirectPurchaseCommitteeId == committeeId &&
                                w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy
                                && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending));
            count += await _context.AgencyCommunicationRequests.CountAsync(w =>
                           w.IsActive == true && w.Tender.DirectPurchaseCommitteeId == committeeId &&
                           w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                           && (w.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending));
            count += await (_context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
                          Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerPendingApprove && w.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId).
                          CountAsync());
            count += await (_context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(f => f.Tender).
                          Where(w => w.StatusId == (int)Enums.enNegotiationStatus.CheckManagerPendingApprove && w.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId).CountAsync());
            count += await _context.TenderChangeRequests
                          .CountAsync(x =>
                      x.IsActive == true &&
                      x.RequestedByRoleName == RoleNames.OffersPurchaseSecretary &&
                      x.Tender.DirectPurchaseCommitteeId == committeeId && x.ChangeRequestStatusId == (int)ChangeStatusType.Pending);
            return count;
        }

        #endregion

        public async Task<int> GetPrePlaningNeedApprovalCount(string agencyCode)
        {
            var count = await _context.PrePlannings.CountAsync(x => x.IsActive == true && x.AgencyCode == agencyCode && (x.StatusId == (int)Enums.PrePlanningStatus.Pending || x.IsDeleteRequested));
            return count;
        }

        public async Task<int> GetPrePlaningRejectedCount(string agencyCode)
        {
            var count = await _context.PrePlannings.CountAsync(x => x.IsActive == true && x.AgencyCode == agencyCode && (x.StatusId == (int)Enums.PrePlanningStatus.Rejected || (x.StatusId == (int)Enums.PrePlanningStatus.Approved) && !x.IsDeleteRequested && !string.IsNullOrEmpty(x.DeleteRejectionReason)));
            return count;
        }

        public async Task<int> GetManagerBlockNeedApprovalCount()
        {
            var count = await _context.SupplierBlock.CountAsync(x => x.IsActive == true && (x.BlockStatusId == (int)Enums.BlockStatus.ApprovedSecertary));
            return count;
        }
        public async Task<int> GetSecretaryBlockNeedApprovalCount()
        {
            var count = await _context.SupplierBlock.CountAsync(x => x.IsActive == true && x.BlockStatusId == (int)Enums.BlockStatus.NewAdmin);
            return count;
        }

        public async Task<int> GetRejectedTendersCountPrePlaning()
        {
            var count = await _context.PrePlannings.CountAsync(x => x.IsActive == true && x.StatusId == (int)Enums.PrePlanningStatus.Rejected);
            return count;


        }
        public async Task<int> GetRejectedCountBlock()
        {
            var count = await _context.SupplierBlock.CountAsync(x => x.IsActive == true && (x.BlockStatusId == (int)Enums.BlockStatus.RejectedSecertary || x.BlockStatusId == (int)Enums.BlockStatus.RejectedManager));
            return count;
        }

        public async Task<int> GetPendingEnquiriesCount(int committeeId, int userId)
        {
            var count = await _context.Tenders.Where(x => (x.IsActive == true && x.TechnicalOrganization.CommitteeUsers
            .Any(c => c.UserProfileId == userId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_TechnicalCommitteeUser && c.CommitteeId == committeeId && c.IsActive == true)
            || x.Enquiries.Any(e => e.JoinTechnicalCommittees.Any(j => j.CommitteeId == committeeId && j.IsActive == true)))
            && x.Enquiries.Count() > 0).CountAsync();
            return count;
        }

        #region VRO Operations

        public async Task<int> GetVROCheckingAndAwardingStageProcessNeedsActionCount(int committeeId)
        {
            var count = await _context.Tenders.CountAsync(x => x.IsActive == true && x.VROCommitteeId == committeeId &&
            (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending || x.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
            || x.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending));

            count += await _context.TenderChangeRequests.CountAsync(r => (
                    r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalChecking
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected)
                     && r.IsActive == true && r.Tender.VROCommitteeId == committeeId && r.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && r.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && r.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending);
            return count;
        }

        public async Task<QueryResult<ProcessNeedsActionViewModel>> GetVROOpenCheckStageProcessNeedsAction(int committeeId, SearchCriteria criteria)
        {
            var q1 = await _context.Tenders
                        .Where(x => x.IsActive == true && x.VROCommitteeId == committeeId &&
                        (x.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending || x.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                         || x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending)).OrderBy(r => r.CreatedAt)
            .Select(a => new ProcessNeedsActionViewModel()
            {
                TenderName = a.TenderName,
                TenderIdString = Util.Encrypt(a.TenderId),
                TenderNumber = a.TenderNumber,
                TenderReferenceNumber = a.ReferenceNumber,
                TenderTypeId = a.TenderTypeId,
                TenderStatusId = a.TenderStatusId,
                LastEnqueriesDate = a.LastEnqueriesDate,
                LastOfferPresentationDate = a.LastOfferPresentationDate,
                TenderStatusIdString = Util.Encrypt(a.TenderStatusId),
                OffersOpeningDate = a.OffersOpeningDate,
                AcceptanceTypeName = a.Status.NameAr,
            }).ToListAsync();
            var q2 = await _context.TenderChangeRequests.Where(r => (
                    r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalChecking
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected)
            && r.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && r.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling &&
            r.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && r.IsActive == true && r.Tender.VROCommitteeId == committeeId)
                           .OrderBy(r => r.CreatedAt).Select(r => new ProcessNeedsActionViewModel()
                           {
                               TenderName = r.Tender.TenderName,
                               TenderIdString = Util.Encrypt(r.TenderId),
                               TenderNumber = r.Tender.TenderNumber,
                               TenderStatusId = r.Tender.TenderStatusId,
                               TenderReferenceNumber = r.Tender.ReferenceNumber,
                               LastOfferPresentationDate = r.Tender.LastOfferPresentationDate,
                               TenderStatusIdString = Util.Encrypt(r.Tender.TenderStatusId),
                               OffersOpeningDate = r.Tender.OffersOpeningDate,
                               AcceptanceTypeName = r.ChangeRequestType.NameAr,
                               ChangeRequestTypeId = r.ChangeRequestTypeId
                           }).ToListAsync();
            var queryResult = await q1.Concat(q2).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return queryResult;

        }

        public async Task<int> GetRejectedTendersCountForVROOpeningCheckingStage(int committeeId)
        {
            var count = await _context.Tenders.CountAsync(x => x.IsActive == true && x.VROCommitteeId == committeeId &&
            (x.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected || x.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected || x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected));

            count += await _context.TenderChangeRequests.CountAsync(r => r.IsActive == true && r.Tender.VROCommitteeId == committeeId && r.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && r.ChangeRequestStatusId == (int)ChangeStatusType.Rejected && r.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling
            && (r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalChecking
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected)
            );
            return count;
        }

        public async Task<QueryResult<RejectTenderViewModel>> GetRejectedTendersForVROOpeningStage(int committeeId, SearchCriteria criteria)
        {
            var q1 = await _context.Tenders
                       .Where(x => x.IsActive == true && x.VROCommitteeId == committeeId &&
                       (x.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected || x.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected || x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected))
                       .SortBy(criteria.Sort, criteria.SortDirection)
                       .OrderBy(x => x.TenderId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.TenderName,
                           TenderNumber = x.TenderNumber,
                           ReferenceNumber = x.ReferenceNumber,
                           TenderStatusName = x.Status.NameAr,
                           ChangeType = x.TenderType.NameAr,
                           TenderStatusId = x.TenderStatusId,
                           TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           RejectionReason = x.TenderHistories.Where(h => (h.StatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected || h.StatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected || h.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected) && h.IsActive == true).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().RejectionReason
                       }).ToListAsync();
            var q2 = await _context.TenderChangeRequests
                      .Where(r => r.IsActive == true && r.Tender.VROCommitteeId == committeeId && r.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && r.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && r.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling
                      && (r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalChecking
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed
                    || r.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected))
                      .SortBy(criteria.Sort, criteria.SortDirection)
                      .OrderBy(x => x.TenderChangeRequestId)
                       .Select(x => new RejectTenderViewModel
                       {
                           TenderName = x.Tender.TenderName,
                           TenderNumber = x.Tender.TenderNumber,
                           ReferenceNumber = x.Tender.ReferenceNumber,
                           TenderStatusName = x.Tender.Status.NameAr,
                           TenderStatusId = x.Tender.TenderStatusId,
                           TenderStatusIdString = Util.Encrypt(x.Tender.TenderStatusId),
                           TenderIdString = Util.Encrypt(x.TenderId),
                           RejectionReason = x.RejectionReason,
                           ChangeType = x.ChangeRequestType.NameAr,
                           ChangeRequestTypeId = x.ChangeRequestTypeId
                       }).ToListAsync();

            var queryResult = await q1.Concat(q2).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return queryResult;
        }

        #endregion

        #region Unit 
        public async Task<QueryResult<TenderWaitingTheUnitActionViewModel>> GetTendersWaitingTheUnitActionAsync(SearchCriteria criteria)
        {
            var result = await _context.Tenders.Include(a => a.TenderUnitAssigns)
                .WhereIf(_httpContextAccessor.HttpContext.User.UserRole() == Enums.UserRole.NewMonafasat_UnitManager.ToString(), x => x.IsActive == true && x.TenderStatusId == (int)Enums.TenderStatus.UnitStaging && (x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderManagerReviewing))
                .WhereIf(_httpContextAccessor.HttpContext.User.UserRole() == Enums.UserRole.NewMonafasat_UnitSpecialistLevel1.ToString(), x => x.IsActive == true
                && x.TenderStatusId == (int)Enums.TenderStatus.UnitStaging
                        && (x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderReviewing || (x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.RejectedByManager
                        && (x.TenderUnitAssigns.FirstOrDefault(a => a.IsCurrentlyAssigned == true).UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelOne)
                        ))
                        || (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding && x.AgencyCommunicationRequests.Any(d => d.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation && d.Negotiations.Any(f => f.StatusId == (int)enNegotiationStatus.UnitSpecialestPendingApproved))))

                .WhereIf(_httpContextAccessor.HttpContext.User.UserRole() == Enums.UserRole.NewMonafasat_UnitSpecialistLevel2.ToString(), x => x.IsActive == true && (x.TenderStatusId == (int)Enums.TenderStatus.UnitStaging && (x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.TenderTransferdToLevelTwo || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo || (x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.RejectedByManager
                && (x.TenderUnitAssigns.FirstOrDefault(a => a.IsCurrentlyAssigned == true).UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelTwo)
                ))) || (x.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
                && x.AgencyCommunicationRequests.Any(d => d.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation && d.Negotiations.Any(f => f.StatusId == (int)enNegotiationStatus.UnitSpecialestPendingApproved))))

                .Select(
                t => new TenderWaitingTheUnitActionViewModel
                {
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    ReferenceNumber = t.ReferenceNumber,
                    RejectionReasonFromUnitManager = t.TenderUnitStatusesHistories.OrderByDescending(u => u.TenderUnitStatusesHistoryId).FirstOrDefault(u => u.TenderUnitStatusId == (int)Enums.TenderUnitStatus.RejectedByManager && t.TenderStatusId == (int)Enums.TenderStatus.UnitStaging).Comment,
                    TenderUnitStatusId = t.TenderUnitStatusId,
                    Status = t.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding ? Resources.TenderResources.DisplayInputs.PendingUnit : t.TenderUnitStatus.Name,
                    OperationType = t.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding ? Resources.CommunicationRequest.DisplayInputs.Negotiate : t.TenderType.NameAr,
                    TenderStatusId = t.TenderStatusId,
                    RequestId = (t.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
                    && t.AgencyCommunicationRequests.Any(d => d.IsActive == true && d.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation && d.Negotiations.Any(f => f.IsActive == true && f.StatusId == (int)enNegotiationStatus.UnitSpecialestPendingApproved))) ?
                    Util.Encrypt(t.AgencyCommunicationRequests.FirstOrDefault(d =>
                   d.IsActive == true && d.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation
                    && d.Negotiations.Any(f => f.IsActive == true && f.StatusId == (int)enNegotiationStatus.UnitSpecialestPendingApproved)).Negotiations.FirstOrDefault(f => f.StatusId == (int)enNegotiationStatus.UnitSpecialestPendingApproved).NegotiationId) : "",
                    CanUnitDoAnyAction =
                    (t.TenderUnitAssigns.Count == 0 && _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitSpecialistLevel1) && t.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview) ||
                    (t.TenderUnitStatusId != (int)Enums.TenderUnitStatus.WaitingManagerApprove && t.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderManagerReviewing &&
                    (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitSpecialistLevel1) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitSpecialistLevel2)) &&
                    t.TenderUnitAssigns.Count > 0 ? t.TenderUnitAssigns.Where(u => u.UnitSpecialistLevel != (int)Enums.UnitSpecialistLevel.UnitManager).OrderByDescending(a =>
                    a.TenderUnitAssignId).FirstOrDefault(a => a.IsActive == true && a.IsCurrentlyAssigned == true).UserProfileId == Convert.ToInt32(_httpContextAccessor.HttpContext.User.UserId()) : false) ||
                    (t.TenderUnitAssigns.Where(u => u.UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitManager).Count() == 0 && _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitManagerUser) && t.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove) ||
                    ((t.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove || t.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderManagerReviewing) && _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitManagerUser) &&
                    t.TenderUnitAssigns.Where(u => u.UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitManager).Count() > 0 ?
                    t.TenderUnitAssigns.Where(u => u.UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitManager).OrderByDescending(a =>
                    a.TenderUnitAssignId).FirstOrDefault(a => a.IsActive == true && a.IsCurrentlyAssigned == true).UserProfileId == Convert.ToInt32(_httpContextAccessor.HttpContext.User.UserId()) : false) || (
                    t.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
                    && t.AgencyCommunicationRequests.Any(d => d.IsActive == true && d.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation && d.Negotiations.Any(f => f.IsActive == true && f.StatusId == (int)enNegotiationStatus.UnitSpecialestPendingApproved)))
                })
                .Where(a => a.CanUnitDoAnyAction)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }
        #endregion
    }
}
