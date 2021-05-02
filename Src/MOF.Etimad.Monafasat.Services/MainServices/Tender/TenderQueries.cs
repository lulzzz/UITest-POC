using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using MOF.Etimad.Monafasat.ViewModel.DTO;
using MOF.Etimad.Monafasat.ViewModel.LCGDto;
using MOF.Etimad.Monafasat.ViewModel.Tender.LocalContent;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class TenderQueries : ITenderQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIDMAppService _idmAppService;
        private readonly RootConfigurations _rootConfiguration;
        private readonly ILocalContentConfigurationSettings _localContentConfigurationSettings;

        public TenderQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor, IIDMAppService idmAppService, IOptionsSnapshot<RootConfigurations> rootConfiguration, ILocalContentConfigurationSettings localContentConfigurationSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _idmAppService = idmAppService;
            _rootConfiguration = rootConfiguration.Value;
            _localContentConfigurationSettings = localContentConfigurationSettings;
        }

        bool IsInRole(string roleName)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(roleName);
        }

        public async Task<QueryResult<LinkTendersToCommitteeModel>> GetTendersToJoinCommitteeWithBranches(LinkTendersToCommitteeSearchCriteriaModel criteria)
        {

            var result = await _context.Tenders
.Where(x => x.IsActive == true && x.AgencyCode == criteria.SelectedAgencyCode && x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && x.TenderStatusId != (int)Enums.TenderStatus.Established
&& x.TenderStatusId != (int)Enums.TenderStatus.Pending && x.TenderStatusId != (int)Enums.TenderStatus.Rejected)
                       .WhereIf(criteria.BranchIdStringList != null, x => criteria.BranchIdStringList.Contains(x.BranchId))
.WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee, x => x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && x.DirectPurchaseCommitteeId == null)
.WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.PreQualificationCommittee, x => (x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification) && x.PreQualificationCommitteeId == null)
.WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee, x => x.VROCommitteeId == null && x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
.WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee, x => x.TechnicalOrganizationId == null && x.LastEnqueriesDate >= DateTime.Now.Date)
.WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee, x => x.TenderTypeId != (int)Enums.TenderType.FirstStageTender && x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification && x.TenderTypeId != (int)Enums.TenderType.Competition && x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects && x.OffersOpeningCommitteeId == null && (x.TenderStatusId == (int)Enums.TenderStatus.OffersOppening || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected || x.TenderStatusId == (int)Enums.TenderStatus.Approved))
.WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee, x => x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects && x.OffersCheckingCommitteeId == null
          && (x.TenderStatusId == (int)Enums.TenderStatus.OffersOppening || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected || x.TenderStatusId == (int)Enums.TenderStatus.Approved
          || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed || x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending || x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected))
.SortBy(criteria.Sort, criteria.SortDirection)
.Select(t => new LinkTendersToCommitteeModel
{
    TenderNumber = t.TenderNumber,
    AgencyCode = t.AgencyCode,
    BranchId = t.BranchId,
    AgencyName = t.Agency.NameArabic,
    BranchName = t.Branch.BranchName,
    TenderTypeId = t.TenderTypeId,
    LastEnqueriesDate = t.LastEnqueriesDate,
    LastOfferPresentationDate = t.LastOfferPresentationDate,
    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
    TechnicalOrganizationId = t.TechnicalOrganizationId,
    DirectPurchaseCommitteeId = t.DirectPurchaseCommitteeId,
    VROCommitteeId = t.VROCommitteeId,
    OffersOpeningDate = t.OffersOpeningDate,
    TenderStatusId = t.TenderStatusId,
    TenderId = t.TenderId,
    TenderName = t.TenderName,
    InvitationTypeName = t.InvitationType.NameAr,
    TenderTypeName = t.TenderType.NameAr,
    TenderStatusName = t.Status.NameAr,
    TechnicalOrganizationName = t.TechnicalOrganization.CommitteeName,
    OffersOpeningCommitteeName = t.OffersOpeningCommittee.CommitteeName,
    TenderIdString = Util.Encrypt(t.TenderId),
    TenderStatusIdString = (Util.Encrypt(t.TenderStatusId)),
    OffersCheckingCommitteeName = t.OffersCheckingCommittee.CommitteeName,
    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
}).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;

        }

        public async Task<List<Tender>> GetTendersToJoinCommittees(LinkTendersToCommitteeSearchCriteriaModel criteria)
        {
            var result = await _context.Tenders
            .Where(x => x.IsActive == true && x.AgencyCode == criteria.AgencyCode && x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification && x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && x.TenderStatusId != (int)Enums.TenderStatus.Established
             && x.TenderStatusId != (int)Enums.TenderStatus.Pending && x.TenderStatusId != (int)Enums.TenderStatus.Rejected)
            .WhereIf(criteria.BranchId != null, x => x.BranchId == criteria.BranchId)
            .WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee, x => x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && x.DirectPurchaseCommitteeId == null)
            .WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee, x => x.VROCommitteeId == null && x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            .WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee, x => x.TechnicalOrganizationId == null && x.LastEnqueriesDate <= DateTime.Now.Date)
            .WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.OpenOfferCommittee, x => x.TenderTypeId != (int)Enums.TenderType.FirstStageTender && x.TenderTypeId != (int)Enums.TenderType.Competition && x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase
            && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects && x.OffersOpeningCommitteeId == null
            && (x.TenderStatusId == (int)Enums.TenderStatus.OffersOppening || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending
                     || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected || x.TenderStatusId == (int)Enums.TenderStatus.Approved))
            .WhereIf(criteria.CommitteeTypeId == (int)Enums.CommitteeType.CheckOfferCommittee, x => x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects && x.OffersCheckingCommitteeId == null
                       && (x.TenderStatusId == (int)Enums.TenderStatus.OffersOppening || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected || x.TenderStatusId == (int)Enums.TenderStatus.Approved
                       || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed || x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending || x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected))
            .SortBy(criteria.Sort, criteria.SortDirection)
            .ToListAsync();
            return result;

        }

        public async Task<QueryResult<BasicTenderModel>> FindTenderForAppliedSuppliersReport(TenderSearchCriteria criteria)
        {
            var result = await _context.Tenders
            .Where(x => x.IsActive == true && x.SubmitionDate != null)
            .Where(x => x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification)
            .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
            .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
            .WhereIf(!String.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
            .WhereIf(criteria.TenderTypeId != 0, x => x.TenderTypeId == criteria.TenderTypeId)
            .WhereIf(!String.IsNullOrWhiteSpace(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
            .WhereIf((criteria.BranchId.HasValue && criteria.BranchId.Value != 0), x => x.BranchId == criteria.BranchId)
            .WhereIf(!String.IsNullOrEmpty(Convert.ToString(criteria.TenderStatusId)), x => x.TenderStatusId == criteria.TenderStatusId)
             .SortBy(criteria.Sort, criteria.SortDirection)
            .Select(t => new BasicTenderModel
            {
                OffersCount = t.Offers.Count(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)),
                TenderNumber = t.TenderNumber,
                TenderReferenceNumber = t.ReferenceNumber,
                AgencyCode = t.AgencyCode,
                BranchId = t.BranchId,
                AgencyName = t.Agency.NameArabic,
                BranchName = t.Branch.BranchName,
                TenderTypeId = t.TenderTypeId,
                LastEnqueriesDate = t.LastEnqueriesDate,
                LastOfferPresentationDate = t.LastOfferPresentationDate,
                OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                OffersOpeningDate = t.OffersOpeningDate,
                TenderStatusId = t.TenderStatusId,
                TenderId = t.TenderId,
                TenderName = t.TenderName,
                TenderTypeName = t.TenderType.NameAr,
                TenderStatusName = t.Status.NameAr,
                TenderIdString = Util.Encrypt(t.TenderId),
                TenderStatusIdString = (Util.Encrypt(t.TenderStatusId)),
                SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
            }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;

        }
        public async Task<Tender> FindTenderOfferDetailsByTenderIdForDirectPurchaseCheckStage(int tenderId, int userId)
        {
            var entities = await _context.Tenders
                .Where(x => x.TenderId == tenderId && x.DirectPurchaseCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRoleId == (int)Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee || c.UserRoleId == (int)Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee))).FirstOrDefaultAsync();
            return entities;
        }


        public async Task<BasicOfferTenderInfoModel> GetBasicOfferTenderInfoById(int id)
        {
            var entities = await _context.Tenders
                .Where(t => t.IsActive == true && t.TenderId == id)
                .Select(x => new BasicOfferTenderInfoModel
                {
                    TenderId = x.TenderId,
                    TenderName = x.TenderName,
                    TenderReferenceNumber = x.ReferenceNumber,
                    InitialGuaranteeAddress = x.InitialGuaranteeAddress,
                    InsideKSA = x.InsideKSA,
                    ExcuationLocation = x.InsideKSA == null ? "" : (x.InsideKSA == true ? Resources.TenderResources.DisplayInputs.InsideKSA : Resources.TenderResources.DisplayInputs.OutsideKSA),
                    TenderAreaNameList = x.InsideKSA == null ? null : (x.InsideKSA == true ? x.TenderAreas.Select(y => y.Area.NameAr).ToList() : x.TenderCountries.Select(y => y.Country.NameAr).ToList()),

                })
                .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<QueryResult<AllTendersViewModel>> GetAllTendersForIndexPage(TenderSearchCriteria criteria)
        {
            if (criteria.IsVro == true)
            {
                criteria.TenderTypeIds = new List<int>
                {
                    (int)Enums.TenderType.NationalTransformationProjects
                };
            }
            List<int> OneFileTenderStatusIds = GetOneFileTenderStatusIdsForOpenOffersReportHiding();
            List<int> TwoFileTenderStatusIds = GetTwoFileTenderStatusIdsForOpenOffersReportHiding();
            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established
                };
            }
            if (criteria.NotInStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.Approved,
                    (int)Enums.TenderStatus.OffersOppening,
                    (int)Enums.TenderStatus.OffersOppenedPending,
                    (int)Enums.TenderStatus.OffersOppenedConfirmed,
                    (int)Enums.TenderStatus.OffersOppenedRejected,
                    (int)Enums.TenderStatus.OffersCheckedPending,
                    (int)Enums.TenderStatus.OffersCheckedConfirmed,
                    (int)Enums.TenderStatus.OffersCheckedRejected,
                    (int)Enums.TenderStatus.OffersAwarding,
                    (int)Enums.TenderStatus.OffersAwardedPending,
                    (int)Enums.TenderStatus.OffersAwardedRejected,
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established,
                };
            }
            var tenders = await _context.Tenders
            .Include(t => t.Agency).Include(t => t.Branch)
            .Include(t => t.TenderType).Include(t => t.Status)

            .Where(x => x.IsActive == true && x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification)
            .WhereIf(criteria.UserId != 0, q => q.TenderHistories.OrderByDescending(x => x.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.Approved && x.UserId == criteria.UserId) != null)
            .WhereIf(!String.IsNullOrWhiteSpace(criteria.AgencyCode), x =>
                (criteria.IsVro.HasValue && criteria.IsVro.Value && x.CreatedByTypeId == (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO
                && !string.IsNullOrEmpty(criteria.AgencyCode) && x.Agency.VROOfficeCode == criteria.AgencyCode && x.TenderStatusId != (int)Enums.TenderStatus.Established && x.TenderStatusId
                != (int)Enums.TenderStatus.UnderEstablishing
                && x.TenderStatusId != (int)Enums.TenderStatus.Pending && x.TenderStatusId != (int)Enums.TenderStatus.Rejected)
                || (x.AgencyCode == criteria.AgencyCode))
            .WhereIf((criteria.BranchId.HasValue && criteria.BranchId.Value != 0), x => (criteria.IsVro.HasValue && criteria.IsVro.Value && x.CreatedByTypeId == (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO
                && !string.IsNullOrEmpty(criteria.AgencyCode) && x.Agency.VROOfficeCode == criteria.AgencyCode && x.TenderStatusId != (int)Enums.TenderStatus.Established && x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing
                && x.TenderStatusId != (int)Enums.TenderStatus.Pending && x.TenderStatusId != (int)Enums.TenderStatus.Rejected)
                || (x.BranchId == criteria.BranchId))
            .WhereIf(criteria.IsVro == true, x => x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && x.AgencyCode == criteria.AgencyCode)
            .WhereIf(criteria.UserRole == RoleNames.OffersPurchaseSecretary || criteria.UserRole == RoleNames.OffersPurchaseManager, x => x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
            .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Trim().Contains(criteria.TenderNumber.Trim()))
            .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Trim().Contains(criteria.ReferenceNumber.Trim()))
            .WhereIf(criteria.CreatedBy != null, x => x.CreatedBy.ToLower().Contains(criteria.CreatedBy.ToLower()))
            .WhereIf(criteria.ApprovedBy != null, x => x.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Approved && h.ActionId == (int)TenderActions.ApproveTender && h.CreatedBy.ToLower().Contains(criteria.ApprovedBy.ToLower())))
            .WhereIf(criteria.TenderTypeId != 0, x => x.TenderTypeId == criteria.TenderTypeId)
            .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderActivityName), x => x.TenderActivities.Any(a => a.Activity.NameAr.Contains(criteria.TenderActivityName)))
            .WhereIf(criteria.TenderAreasIds != null && criteria.TenderAreasIds.Count != 0, x => x.TenderAreas.Any(a => criteria.TenderAreasIds.Contains(a.AreaId)))
            .WhereIf(criteria.ConditionsBookletPrice != null && criteria.ConditionsBookletPrice != 0, x => x.ConditionsBookletPrice == criteria.ConditionsBookletPrice)
            .WhereIf(criteria.ConditionsBookletPrice == 0, x => x.ConditionsBookletPrice == criteria.ConditionsBookletPrice || x.ConditionsBookletPrice == null)
            .WhereIf(criteria.SubmitionDate != null && criteria.SubmitionDate != DateTime.MinValue, x => x.SubmitionDate >= criteria.SubmitionDate.Value.Date && x.SubmitionDate.Value.Date < criteria.SubmitionDate.Value.AddDays(1))
            .WhereIf(criteria.LastOfferPresentationDate != null && criteria.LastOfferPresentationDate != DateTime.MinValue, x => x.LastOfferPresentationDate >= criteria.LastOfferPresentationDate.Value.Date && x.LastOfferPresentationDate.Value.Date < criteria.LastOfferPresentationDate.Value.AddDays(1))
            .WhereIf(criteria.LastEnqueriesDate != null && criteria.LastEnqueriesDate != DateTime.MinValue, x => x.LastEnqueriesDate == criteria.LastEnqueriesDate.Value.Date)
            .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => criteria.TenderStatusIds.Contains(x.TenderStatusId))
            .WhereIf(criteria.TenderTypeIds != null && criteria.TenderTypeIds.Count != 0, x => criteria.TenderTypeIds.Contains(x.TenderTypeId))
            .WhereIf(!criteria.TenderStatusIds.Any() && criteria.NotInStatusId == 0 && criteria.TenderStatusId != null, x => x.TenderStatusId == criteria.TenderStatusId)
            .WhereIf(criteria.NotInStatusId > 0, x => x.TenderStatusId == (int)Enums.TenderStatus.Approved && x.LastOfferPresentationDate >= DateTime.Now)
            .WhereIf(!string.IsNullOrEmpty(criteria.FinancialYear), x => x.SubmitionDate.Value.Year == int.Parse(criteria.FinancialYear))
            .SortBy(criteria.Sort, criteria.SortDirection)
            .ToQueryResult(criteria.PageNumber, criteria.PageSize);

            var tenderIds = tenders.Items.Select(t => t.TenderId).ToList();
            var tenderChangeRequests = await _context.TenderChangeRequests.Where(c => c.IsActive == true && tenderIds.Contains(c.TenderId)).ToListAsync();
            var tenderOffers = await _context.Offers.Where(o => o.IsActive == true && tenderIds.Contains(o.TenderId))
                        .Where(o => (o.OfferStatusId == (int)Enums.OfferStatus.Received || o.OfferStatusId == (int)Enums.OfferStatus.Excluded)).ToListAsync();

            var itemsModel = tenders.Items.Select(t => new AllTendersViewModel
            {
                TenderNumber = t.TenderNumber,
                TenderReferenceNumber = t.ReferenceNumber,
                AgencyCode = t.AgencyCode,
                BranchId = t.BranchId,
                AgencyName = t.Agency.NameArabic,
                BranchName = t.Branch.BranchName,
                ConditionsBookletPrice = t.ConditionsBookletPrice,
                EstimatedValue = t.EstimatedValue,
                InsideKSA = t.InsideKSA,
                InvitationTypeId = t.InvitationTypeId,
                TenderTypeId = t.TenderTypeId,
                LastEnqueriesDate = t.LastEnqueriesDate,
                LastOfferPresentationDate = t.LastOfferPresentationDate,
                OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                TechnicalOrganizationId = t.TechnicalOrganizationId != null ? t.TechnicalOrganizationId.Value : 0,
                OffersOpeningDate = t.OffersOpeningDate,
                OffersCheckingDate = t.OffersCheckingDate,
                Purpose = t.Purpose,
                TenderStatusId = t.TenderStatusId,
                SupplierNeedSample = t.SupplierNeedSample,
                TenderId = t.TenderId,
                TenderName = t.TenderName,
                UserCommitteeId = criteria.SelectedCommitteeId,
                TenderTypeName = t.TenderType.NameAr,
                HasPreQualification = t.PreQualificationId.HasValue,
                UnitStatusId = t.TenderUnitStatusId,
                PreQualificationId = t.PreQualificationId,
                TenderStatusName = t.Status.NameAr,
                CreatedDate = t.CreatedAt,
                SubmitionDate = t.SubmitionDate,
                TenderIdString = Util.Encrypt(t.TenderId),
                TenderStatusIdString = (Util.Encrypt(t.TenderStatusId)),
                SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                OffersCheckingDateHijri = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                TenderChangeRequestIdsForAuditor = tenderChangeRequests.Where(c => c.TenderId == t.TenderId && (c.RequestedByRoleName == RoleNames.DataEntry || c.RequestedByRoleName == RoleNames.PurshaseSpecialist) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                TenderChangeRequestIdsForDataEntry = tenderChangeRequests.Where(c => c.TenderId == t.TenderId && (c.RequestedByRoleName == RoleNames.DataEntry || c.RequestedByRoleName == RoleNames.PurshaseSpecialist) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                ChangeRequestTypeId = tenderChangeRequests.Where(x => x.TenderId == t.TenderId).Select(x => x.ChangeRequestTypeId).FirstOrDefault(),
                QuantitiesTableStatus = tenderChangeRequests.Where(x => x.TenderId == t.TenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).OrderByDescending(x => x.CreatedAt).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                ExtendDatesStatus = tenderChangeRequests.Where(x => x.TenderId == t.TenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                AttachmentsStatus = tenderChangeRequests.Where(x => x.TenderId == t.TenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                CancelRequestStatus = tenderChangeRequests.Where(x => x.TenderId == t.TenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                ChangeRequestedBy = tenderChangeRequests.Where(x => x.TenderId == t.TenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                OffersCount = tenderOffers.Count(o => o.TenderId == t.TenderId),
                HasAcceptedOffers = tenderOffers.Any(o => o.TenderId == t.TenderId && o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer),
                HasSecondStage = tenders.Items.Any(a => t.TenderId == a.TenderFirstStageId),
                //HasSecondStage = _context.Tenders.Where(s => s.IsActive == true).Any(a => t.TenderId == a.TenderFirstStageId),
                CanRecreateFramWork = t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement && t.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed &&
                t.AgreementTypeId == (int)Enums.AgreementType.Opened && t.PreviousFramWorkId == null,
                OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                CanViewSuppliersReportFinancialSupervisor =
                (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile && !OneFileTenderStatusIds.Contains(t.TenderStatusId))
                ||
                (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles && TwoFileTenderStatusIds.Contains(t.TenderStatusId))
            });

            QueryResult<AllTendersViewModel> result = new QueryResult<AllTendersViewModel>(itemsModel, tenders.TotalCount, tenders.PageNumber, tenders.PageSize);

            return result;

        }

        public async Task<QueryResult<BasicTenderModel>> FindTenderBySearchCriteriaForIndexPage(TenderSearchCriteria criteria)
        {
            if (criteria.IsVro == true)
            {
                criteria.TenderTypeIds = new List<int>
                {
                    (int)Enums.TenderType.NationalTransformationProjects
                };
            }
            List<int> OneFileTenderStatusIds = GetOneFileTenderStatusIdsForOpenOffersReportHiding();
            List<int> TwoFileTenderStatusIds = GetTwoFileTenderStatusIdsForOpenOffersReportHiding();
            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established
                };
            }
            if (criteria.NotInStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.Approved,
                    (int)Enums.TenderStatus.OffersOppening,
                    (int)Enums.TenderStatus.OffersOppenedPending,
                    (int)Enums.TenderStatus.OffersOppenedConfirmed,
                    (int)Enums.TenderStatus.OffersOppenedRejected,
                    (int)Enums.TenderStatus.OffersCheckedPending,
                    (int)Enums.TenderStatus.OffersCheckedConfirmed,
                    (int)Enums.TenderStatus.OffersCheckedRejected,
                    (int)Enums.TenderStatus.OffersAwarding,
                    (int)Enums.TenderStatus.OffersAwardedPending,
                    (int)Enums.TenderStatus.OffersAwardedRejected,
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established,
                };
            }
            var items = await _context.Tenders
            .Include(t => t.Agency)
            .Include(t => t.Branch)
            .Include(t => t.InvitationType)
            .Include(t => t.TenderType)
            .Include(t => t.ChangeRequests)
            .Include(t => t.Status)
            .Include(t => t.TechnicalOrganization)
            .Include(t => t.OffersOpeningCommittee)
            .Include(t => t.OffersCheckingCommittee)
            .Include(t => t.Offers)
            .Include(t => t.Invitations)
            .Include(t => t.Enquiries).ThenInclude(t => t.EnquiryReplies)
            .Where(x => x.IsActive == true && x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification)
            .WhereIf(criteria.UserId != 0, q => q.TenderHistories.OrderByDescending(x => x.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.Approved && x.UserId == criteria.UserId) != null)
            .WhereIf(!String.IsNullOrWhiteSpace(criteria.AgencyCode), x =>
                (criteria.IsVro.HasValue && criteria.IsVro.Value && x.CreatedByTypeId == (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO
                && !string.IsNullOrEmpty(criteria.AgencyCode) && x.Agency.VROOfficeCode == criteria.AgencyCode && x.TenderStatusId != (int)Enums.TenderStatus.Established && x.TenderStatusId
                != (int)Enums.TenderStatus.UnderEstablishing
                && x.TenderStatusId != (int)Enums.TenderStatus.Pending && x.TenderStatusId != (int)Enums.TenderStatus.Rejected)
                || (x.AgencyCode == criteria.AgencyCode))
            .WhereIf((criteria.BranchId.HasValue && criteria.BranchId.Value != 0), x => (criteria.IsVro.HasValue && criteria.IsVro.Value && x.CreatedByTypeId == (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO
                && !string.IsNullOrEmpty(criteria.AgencyCode) && x.Agency.VROOfficeCode == criteria.AgencyCode && x.TenderStatusId != (int)Enums.TenderStatus.Established && x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing
                && x.TenderStatusId != (int)Enums.TenderStatus.Pending && x.TenderStatusId != (int)Enums.TenderStatus.Rejected)
                || (x.BranchId == criteria.BranchId))
            .WhereIf(criteria.IsVro == true, x => x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && x.AgencyCode == criteria.AgencyCode)
            .WhereIf(criteria.UserRole == RoleNames.OffersPurchaseSecretary || criteria.UserRole == RoleNames.OffersPurchaseManager, x => x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
            .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Trim().Contains(criteria.TenderNumber.Trim()))
            .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Trim().Contains(criteria.ReferenceNumber.Trim()))
            .WhereIf(criteria.CreatedBy != null, x => x.CreatedBy.ToLower().Contains(criteria.CreatedBy.ToLower()))
            .WhereIf(criteria.ApprovedBy != null, x => x.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Approved && h.ActionId == (int)TenderActions.ApproveTender && h.CreatedBy.ToLower().Contains(criteria.ApprovedBy.ToLower())))
            .WhereIf(criteria.TenderTypeId != 0, x => x.TenderTypeId == criteria.TenderTypeId)
            .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderActivityName), x => x.TenderActivities.Any(a => a.Activity.NameAr.Contains(criteria.TenderActivityName)))
            .WhereIf(criteria.TenderAreasIds != null && criteria.TenderAreasIds.Count != 0, x => x.TenderAreas.Any(a => criteria.TenderAreasIds.Contains(a.AreaId)))
            .WhereIf(criteria.ConditionsBookletPrice != null && criteria.ConditionsBookletPrice != 0, x => x.ConditionsBookletPrice == criteria.ConditionsBookletPrice)

            .WhereIf(criteria.ConditionsBookletPrice == 0, x => x.ConditionsBookletPrice == criteria.ConditionsBookletPrice || x.ConditionsBookletPrice == null)
            .WhereIf(criteria.SubmitionDate != null && criteria.SubmitionDate != DateTime.MinValue, x => x.SubmitionDate >= criteria.SubmitionDate.Value.Date && x.SubmitionDate.Value.Date < criteria.SubmitionDate.Value.AddDays(1))
            .WhereIf(criteria.LastOfferPresentationDate != null && criteria.LastOfferPresentationDate != DateTime.MinValue, x => x.LastOfferPresentationDate >= criteria.LastOfferPresentationDate.Value.Date && x.LastOfferPresentationDate.Value.Date < criteria.LastOfferPresentationDate.Value.AddDays(1))
            .WhereIf(criteria.LastEnqueriesDate != null && criteria.LastEnqueriesDate != DateTime.MinValue, x => x.LastEnqueriesDate == criteria.LastEnqueriesDate.Value.Date)
            .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => criteria.TenderStatusIds.Contains(x.TenderStatusId))
            .WhereIf(criteria.TenderTypeIds != null && criteria.TenderTypeIds.Count != 0, x => criteria.TenderTypeIds.Contains(x.TenderTypeId))
            .WhereIf(!criteria.TenderStatusIds.Any() && criteria.NotInStatusId == 0 && criteria.TenderStatusId != null, x => x.TenderStatusId == criteria.TenderStatusId)
            .WhereIf(criteria.NotInStatusId > 0, x => x.TenderStatusId == (int)Enums.TenderStatus.Approved && x.LastOfferPresentationDate >= DateTime.Now)
            .WhereIf(!string.IsNullOrEmpty(criteria.FinancialYear), x => x.SubmitionDate.Value.Year == int.Parse(criteria.FinancialYear))
            .SortBy(criteria.Sort, criteria.SortDirection)
            .ToQueryResult(criteria.PageNumber, criteria.PageSize);

            var itemsModel = items.Items.Select(t => new BasicTenderModel
            {
                TenderNumber = t.TenderNumber,
                TenderReferenceNumber = t.ReferenceNumber,
                AgencyCode = t.AgencyCode,
                BranchId = t.BranchId,
                AgencyName = t.Agency.NameArabic,
                BranchName = t.Branch.BranchName,
                ConditionsBookletPrice = t.ConditionsBookletPrice,
                EstimatedValue = t.EstimatedValue,
                InsideKSA = t.InsideKSA,
                InvitationTypeId = t.InvitationTypeId,
                TenderTypeId = t.TenderTypeId,
                LastEnqueriesDate = t.LastEnqueriesDate,
                LastOfferPresentationDate = t.LastOfferPresentationDate,
                OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                TechnicalOrganizationId = t.TechnicalOrganizationId != null ? t.TechnicalOrganizationId.Value : 0,
                OffersOpeningDate = t.OffersOpeningDate,
                OffersCheckingDate = t.OffersCheckingDate,
                Purpose = t.Purpose,
                TenderStatusId = t.TenderStatusId,
                SupplierNeedSample = t.SupplierNeedSample,
                TenderId = t.TenderId,
                TenderName = t.TenderName,
                UserCommitteeId = criteria.SelectedCommitteeId,
                InvitationTypeName = t.InvitationType?.NameAr,
                TenderTypeName = t.TenderType.NameAr,
                HasPreQualification = t.PreQualificationId.HasValue,
                UnitStatusId = t.TenderUnitStatusId,
                PreQualificationId = t.PreQualificationId,
                TenderChangeRequestIdsForAuditor = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.DataEntry || c.RequestedByRoleName == RoleNames.PurshaseSpecialist) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                TenderChangeRequestIdsForDataEntry = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.DataEntry || c.RequestedByRoleName == RoleNames.PurshaseSpecialist) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                ChangeRequestTypeId = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestTypeId).FirstOrDefault(),
                QuantitiesTableStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).OrderByDescending(x => x.CreatedAt).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                ExtendDatesStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                AttachmentsStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                ChangeRequestedBy = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                TenderStatusName = t.Status.NameAr,
                TechnicalOrganizationName = t.TechnicalOrganization?.CommitteeName,
                OffersOpeningCommitteeName = t.OffersOpeningCommittee?.CommitteeName,
                CreatedDate = t.CreatedAt,
                SubmitionDate = t.SubmitionDate,
                TenderIdString = Util.Encrypt(t.TenderId),
                TenderStatusIdString = (Util.Encrypt(t.TenderStatusId)),
                OffersCheckingCommitteeName = t.OffersCheckingCommittee?.CommitteeName,
                HasEnquiry = t.Enquiries.Any(e => e.TenderId == t.TenderId && e.IsActive == true),
                EnquirySendingDate = t.Enquiries.Select(e => e.CreatedAt).FirstOrDefault(),
                OffersCount = t.Offers.Count(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)),
                IsPurchased = t.ConditionsBooklets.Any(),
                SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                OffersCheckingDateHijri = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                PendingEnquiryReplyCount = t.Enquiries.Select(e => e.EnquiryReplies.Count(r => r.IsActive == true && r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Pending)).Count(),
                ApprovedEnquiryReplyCount = t.Enquiries.Select(e => e.EnquiryReplies.Count(r => r.IsActive == true && r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved)).Count(),
                AwardingValue = t.Offers.Select(x => x.TotalOfferAwardingValue).Sum() == 0 ? t.Offers.Select(p => p.PartialOfferAwardingValue).Sum() : t.Offers.Select(h => h.TotalOfferAwardingValue).Sum(),
                HasSecondStage = _context.Tenders.Where(s => s.IsActive == true).Any(a => t.TenderId == a.TenderFirstStageId),
                HasAcceptedOffers = t.Offers.Any(o => o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && o.IsActive == true),
                CanRecreateFramWork = t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement && t.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed &&
                t.AgreementTypeId == (int)Enums.AgreementType.Opened && t.PreviousFramWorkId == null,
                PerqualificationsTenderStatus = t.Invitations.Any(s => s.StatusId == (int)Enums.InvitationStatus.Rejected || s.StatusId == (int)Enums.InvitationStatus.Withdrawal),
                OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                canViewSuppliersReportFinancialSupervisor =
                (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile && !OneFileTenderStatusIds.Contains(t.TenderStatusId))
                ||
                (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles && TwoFileTenderStatusIds.Contains(t.TenderStatusId))
            });

            QueryResult<BasicTenderModel> result = new QueryResult<BasicTenderModel>(itemsModel, items.TotalCount, items.PageNumber, items.PageSize);

            return result;

        }

        public async Task<List<int>> FindTenderByAgencyCodeForPurchaseMethod(string agencyCode)
        {
            var result = await _context.Tenders.Where(t => t.AgencyCode == agencyCode && t.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
                 .Select(x => x.TenderTypeId).ToListAsync();
            return result;
        }

        public async Task<QueryResult<TenderMovement>> GetTenderMovementsFromHistoryByTenderId(SimpleTenderSearchCriteria criteria)
        {
            var result = await _context.TenderHistories
                .Where(x => x.TenderId == criteria.TenderId)
                 .OrderByDescending(d => d.CreatedAt)
                .Select(f => new TenderMovement
                {
                    Email = _context.UserProfiles.FirstOrDefault(d => d.Id == f.UserId).Email,
                    MovmentDate = f.CreatedAt,
                    UserName = f.CreatedBy,
                    StatusName = f.Status.NameAr,
                    ActionName = f.Action.NameAr
                }).ToQueryResult(criteria.PageNumber, (int)Enums.PageSize.twenty);
            return result;
        }

        public async Task<QueryResult<BasicTenderModel>> FindTenderBySearchCriteriaForUnderOperationsStage(TenderSearchCriteria criteria)
        {

            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established,

                };
            }
            var result = await _context.Tenders
                .Where(x => x.BranchId == criteria.BranchId)
                .Where(x => x.IsActive == true)
                .Where(x => x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification)
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
                .WhereIf((criteria.TenderTypeId != 0), x => x.TenderTypeId == criteria.TenderTypeId)
                .Where(x => criteria.TenderStatusIds.Contains(x.TenderStatusId))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new BasicTenderModel
                {
                    TenderNumber = t.TenderNumber,
                    TenderReferenceNumber = t.ReferenceNumber,
                    AgencyCode = t.AgencyCode,
                    BranchId = t.BranchId,
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    EstimatedValue = t.EstimatedValue,
                    InsideKSA = t.InsideKSA,
                    InvitationTypeId = t.InvitationTypeId,
                    TenderTypeId = t.TenderTypeId,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                    TechnicalOrganizationId = t.TechnicalOrganizationId != null ? t.TechnicalOrganizationId.Value : 0,
                    OffersOpeningDate = t.OffersOpeningDate,
                    Purpose = t.Purpose,
                    TenderStatusId = t.TenderStatusId,
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    InvitationTypeName = t.InvitationType.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    TenderChangeRequestIdsForAuditor = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.DataEntry || c.RequestedByRoleName == RoleNames.PurshaseSpecialist) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForDataEntry = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.DataEntry || c.RequestedByRoleName == RoleNames.PurshaseSpecialist) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.TenderChangeRequestId).ToList(),
                    ChangeRequestStatusIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatusId).ToList(),
                    ChangeRequestStatusNames = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatus.NameAr).ToList(),
                    ChangeRequestTypeId = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestTypeId).FirstOrDefault(),
                    QuantitiesTableStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).OrderByDescending(x => x.CreatedAt).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    ExtendDatesStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    AttachmentsStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).OrderByDescending(x => x.TenderChangeRequestId).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    ChangeRequestedBy = t.ChangeRequests.Where(x => x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).OrderByDescending(x => x.TenderChangeRequestId).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    TenderStatusName = t.Status.NameAr,
                    TechnicalOrganizationName = t.TechnicalOrganization.CommitteeName,
                    OffersOpeningCommitteeName = t.OffersOpeningCommittee.CommitteeName,
                    CreatedDate = t.CreatedAt,
                    SubmitionDate = t.SubmitionDate,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderStatusIdString = (Util.Encrypt(t.TenderStatusId)),
                    OffersCheckingCommitteeName = t.OffersCheckingCommittee.CommitteeName,
                    HasEnquiry = t.Enquiries.Any(e => e.TenderId == t.TenderId && e.IsActive == true),
                    EnquirySendingDate = t.Enquiries.Select(e => e.CreatedAt).FirstOrDefault(),
                    IsPurchased = t.ConditionsBooklets.Any(),
                    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    PendingEnquiryReplyCount = t.Enquiries.Select(e => e.EnquiryReplies.Count(r => r.IsActive == true && r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Pending)).Count(),
                    ApprovedEnquiryReplyCount = t.Enquiries.Select(e => e.EnquiryReplies.Count(r => r.IsActive == true && r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Approved)).Count(),
                    AwardingValue = t.Offers.Select(x => x.TotalOfferAwardingValue).Sum() == 0 ? t.Offers.Select(p => p.PartialOfferAwardingValue).Sum() : t.Offers.Select(h => h.TotalOfferAwardingValue).Sum()

                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindTenderBySearchCriteriaForOpeningStage(TenderSearchCriteria criteria)
        {
            //prepare new flow filters
            var isTwoFlowApplied = _rootConfiguration.OldFlow.isApplied;
            var TwoFlowDate = _rootConfiguration.OldFlow.EndDate.ToDateTime();
            List<int> newFlowStatuses = new List<int>
            {
                (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed,
                (int)Enums.TenderStatus.OffersOpenFinancialStage,
                (int)Enums.TenderStatus.OffersOpenFinancialStagePending,
                (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
            };

            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established
                };
            }

            var result = await _context.Tenders
           .Where(x => x.OffersOpeningCommitteeId == criteria.SelectedCommitteeId && x.AgencyCode == criteria.SelectedAgencyCode && x.IsActive == true)
           .Where(x => (x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification && x.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.FirstStageTender && x.TenderTypeId != (int)Enums.TenderType.Competition)
                   || x.ChangeRequests.Any(t => t.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && t.RequestedByRoleName == RoleNames.OffersOppeningSecretary))

           .WhereIf(criteria.UserRole == Enums.UserRole.NewMonafasat_OffersOpeningManager.ToString(), x => (x.TenderTypeId == (int)Enums.TenderType.CurrentTender && x.OffersOpeningDate <= DateTime.Now) || x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderStatusId != (int)Enums.TenderStatus.Approved)
                      .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
           .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
           .WhereIf(!String.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
           .WhereIf((criteria.TenderTypeId != 0), x => x.TenderTypeId == criteria.TenderTypeId)
           .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => isTwoFlowApplied && (x.SubmitionDate.Value >= TwoFlowDate)
           ? criteria.TenderStatusIds.Contains(x.TenderStatusId) || newFlowStatuses.Contains(x.TenderStatusId)
           : criteria.TenderStatusIds.Contains(x.TenderStatusId))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new TenderCheckingAndAwardingModel
                {
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    TenderStatusName = t.Status.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    TenderReferenceNumber = t.ReferenceNumber,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    AgencyCode = t.AgencyCode,
                    BranchId = t.BranchId,
                    CreatedBy = t.CreatedBy,
                    TenderChangeRequestIdsForOpeningSecretary = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersOppeningSecretary && (c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForOpeningManager = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersOppeningSecretary && (c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    ApprovedBy = t.TenderHistories.OrderByDescending(h => h.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.Approved && (h.ActionId == (int)Enums.TenderActions.ApproveTender || h.ActionId == (int)Enums.TenderActions.ApproveTenderByUnitManager)).CreatedBy,
                    ChangeRequestedBy = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && x.RequestedByRoleName == RoleNames.OffersOppeningSecretary).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && x.RequestedByRoleName == RoleNames.OffersOppeningSecretary).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    UserCommitteeType = t.OffersOpeningCommittee != null && criteria.UserId != null ? (t.OffersOpeningCommittee.CommitteeUsers.Any(c => c.UserProfileId == criteria.UserId && c.IsActive == true && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersOpeningManager) ? (int)Enums.UserCommitteeType.NewMonafasat_OffersOpeningManager : (int)Enums.UserCommitteeType.NewMonafasat_OffersOpeningSecretary) : (int)Enums.UserCommitteeType.None,
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                    SubmitionDate = t.SubmitionDate,
                    OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    OffersOpeningDate = t.OffersOpeningDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersCheckingDateHijri = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCount = t.Offers.Count(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)),
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;

        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindTenderBySearchCriteriaForCheckingStage(TenderSearchCriteria criteria)
        {
            //prepare new flow filters
            var isTwoFlowApplied = _rootConfiguration.OldFlow.isApplied;
            var TwoFlowDate = _rootConfiguration.OldFlow.EndDate.ToDateTime();
            List<int> oldFlowStatuses = new List<int>
            {
                (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed,
                (int)Enums.TenderStatus.OffersOpenFinancialStage
            };
            List<int> newFlowStatuses = new List<int>
            {
                (int)Enums.TenderStatus.OffersOpenFinancialStageApproved
            };
            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established
                };
            }
            var result = await _context.Tenders
             .Where(x => x.OffersCheckingCommitteeId == criteria.SelectedCommitteeId && x.AgencyCode == criteria.SelectedAgencyCode && x.IsActive == true)
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
             .WhereIf(criteria.TenderTypeId != 0, x => x.TenderTypeId == criteria.TenderTypeId)
             .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => (isTwoFlowApplied && (x.SubmitionDate.Value < TwoFlowDate)
             ? criteria.TenderStatusIds.Contains(x.TenderStatusId) || oldFlowStatuses.Contains(x.TenderStatusId)
             : criteria.TenderStatusIds.Contains(x.TenderStatusId)) || newFlowStatuses.Contains(x.TenderStatusId)
             || (x.TenderStatusId == (int)Enums.TenderStatus.Approved && x.OffersCheckingDate <= DateTime.Now && (x.TenderTypeId == (int)Enums.TenderType.Competition || x.TenderTypeId == (int)Enums.TenderType.FirstStageTender || x.TenderTypeId == (int)Enums.TenderType.ReverseBidding)))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new TenderCheckingAndAwardingModel
                {
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    TenderReferenceNumber = t.ReferenceNumber,
                    TenderStatusName = t.Status.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    AgencyCode = t.AgencyCode,
                    BranchId = t.BranchId,
                    CreatedBy = t.CreatedBy,
                    OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                    TenderChangeRequestIdsForCheckingSecretary = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForCheckingManager = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    ApprovedBy = t.TenderHistories.OrderByDescending(h => h.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.Approved && (h.ActionId == (int)Enums.TenderActions.ApproveTender || h.ActionId == (int)Enums.TenderActions.ApproveTenderByUnitManager)).CreatedBy,
                    ChangeRequestedBy = t.ChangeRequests.Where(x => x.IsActive == true && x.RequestedByRoleName == RoleNames.OffersCheckSecretary && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.RequestedByRoleName == RoleNames.OffersCheckSecretary && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    UserCommitteeType = t.OffersCheckingCommittee != null && criteria.UserId != null ? (t.OffersCheckingCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == criteria.UserId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckManager) ? (int)UserCommitteeType.NewMonafasat_OffersCheckManager : (int)UserCommitteeType.NewMonafasat_OffersCheckSecretary) : (int)UserCommitteeType.None,
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                    SubmitionDate = t.SubmitionDate,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    OffersOpeningDate = t.OffersOpeningDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersCheckingDateHijri = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCount = t.Offers.Count(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)),
                    CanOpenBiddingPage = (t.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed || t.TenderStatusId == (int)Enums.TenderStatus.Bidding || t.TenderStatusId == (int)Enums.TenderStatus.BiddingApproved) && t.TenderTypeId == (int)Enums.TenderType.ReverseBidding,
                    CanStartTechnicalEvaluation = (t.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed || t.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || t.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed || t.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalChecking)
                    || ((t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || t.TenderTypeId == (int)Enums.TenderType.FirstStageTender || t.TenderTypeId == (int)Enums.TenderType.Competition) && t.OffersCheckingDate <= DateTime.Now && t.TenderStatusId == (int)Enums.TenderStatus.Approved)
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<VROTendersDTO>> GetVROTendersCreatedByGovAgency(TenderSearchCriteria criteria)
        {
            var relatedVRO = _context.GovAgencies.Where(x => x.AgencyCode == criteria.SelectedAgencyCode && x.VROOfficeCodeRelated != null).Count();
            var result = await _context.Tenders
             .Where(x => x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && x.IsActive == true && relatedVRO > 0)
             .WhereIf(criteria.UserRole == RoleNames.DataEntry || criteria.UserRole == RoleNames.Auditer, x => x.VRORelatedBranch.BranchId == criteria.BranchId)
             .WhereIf(criteria.UserRole == RoleNames.MonafasatAdmin, x => x.VRORelatedBranch.AgencyCode == criteria.SelectedAgencyCode)
             .WhereIf(criteria.UserRole == RoleNames.MonafasatAccountManager, x => x.CreatedByTypeId == (int)Enums.TenderCreatedByType.AgenciesRelatedByVRO)
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new VROTendersDTO
                {
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    TenderStatusId = t.TenderStatusId,
                    TenderNumber = t.TenderNumber,
                    TenderReferenceNumber = t.ReferenceNumber,
                    TenderTypeId = t.TenderTypeId,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    AgencyCode = t.AgencyCode,
                    BranchId = t.BranchId,
                    OffersOpeningDate = t.OffersOpeningDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    SubmitionDate = t.SubmitionDate,
                    TenderStatusName = t.Status.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> GetTendersForCheckingDirectPuchaseStageAsync(TenderSearchCriteria criteria)
        {
            var currentDate = DateTime.Now;
            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established
                };
            }
            var result = await _context.Tenders
                .Where(x => (x.DirectPurchaseCommitteeId == criteria.SelectedCommitteeId || (x.IsLowBudgetDirectPurchase.HasValue && x.IsLowBudgetDirectPurchase.Value && x.DirectPurchaseMemberId == criteria.UserId)) &&
                x.AgencyCode == criteria.SelectedAgencyCode &&
                x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase &&
                currentDate >= x.OffersCheckingDate &&
                x.IsActive == true)
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
             .WhereIf((criteria.TenderTypeId == (int)Enums.TenderType.CurrentTender || criteria.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase), x => x.TenderTypeId == criteria.TenderTypeId)
             .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => criteria.TenderStatusIds.Contains(x.TenderStatusId))

                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new TenderCheckingAndAwardingModel
                {
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    TenderReferenceNumber = t.ReferenceNumber,
                    TenderStatusName = t.Status.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    AgencyCode = t.AgencyCode,
                    BranchId = t.BranchId,
                    CreatedBy = t.CreatedBy,
                    OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                    TenderChangeRequestIdsForCheckingSecretary = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForCheckingManager = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    ApprovedBy = t.TenderHistories.OrderByDescending(h => h.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.Approved && (h.ActionId == (int)Enums.TenderActions.ApproveTender || h.ActionId == (int)Enums.TenderActions.ApproveTenderByUnitManager)).CreatedBy,
                    ChangeRequestedBy = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    UserCommitteeType =

                    t.DirectPurchaseCommittee != null && criteria.UserId != null ?

                    (t.DirectPurchaseCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == criteria.UserId
                    && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee)
                    ? (int)Enums.UserCommitteeType.NewMonafasat_ManagerDirtectPurshasingCommittee
                    : (int)UserCommitteeType.NewMonafasat_SecretaryDirtectPurshasingCommittee)

                    : (int)UserCommitteeType.None,
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                    SubmitionDate = t.SubmitionDate,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    OffersOpeningDate = t.OffersOpeningDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersCheckingDateHijri = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCount = t.Offers.Count(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)),
                    ChangeRequestStatusIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatusId).ToList(),
                    CanStartOfferCheckingDate = DateTime.Now >= t.OffersCheckingDate,
                    IsLowBudgetDirectPurchase = t.IsLowBudgetDirectPurchase == true,
                    DirectPurchaseMemberId = t.DirectPurchaseMemberId,
                    IsLowBudgetAndAssignedMember = t.IsLowBudgetDirectPurchase == true && t.DirectPurchaseMemberId == criteria.UserId

                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<Tender> FindTenderForCheckingStatusByTenderId(int tenderId)
        {
            Tender tenderEntity = await _context.Tenders
                .Include(x => x.BiddingRounds)
                .Include(x => x.PostQualifications)
                .Include(x => x.Offers)
                .Include(x => x.Status)
                .Include(x => x.Invitations).Where(tender => tender.TenderId == tenderId).SingleOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<TenderUnitAssign> FindLastTenderUnitAssignsByTenderId(int tenderId)
        {
            TenderUnitAssign tenderUnitAssign = await _context.TenderUnitAssigns.Where(t => t.TenderId == tenderId && t.UnitSpecialistLevel != (int)Enums.UnitSpecialistLevel.UnitManager)
                .OrderByDescending(t => t.CreatedAt).FirstOrDefaultAsync();
            return tenderUnitAssign;
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindTenderBySearchCriteriaForAwardingStage(TenderSearchCriteria criteria)
        {
            var result = await _context.Tenders
                .Where(x => x.IsActive == true && x.AgencyCode == criteria.SelectedAgencyCode)
                .Where(x => x.OffersCheckingCommitteeId == criteria.SelectedCommitteeId || x.DirectPurchaseCommitteeId == criteria.SelectedCommitteeId || (x.IsLowBudgetDirectPurchase.HasValue && x.DirectPurchaseMemberId == criteria.UserId)
                    || x.VROCommitteeId == criteria.SelectedCommitteeId)
                .Where(x => x.TenderTypeId != (int)Enums.TenderType.FirstStageTender && x.TenderTypeId != (int)Enums.TenderType.PostQualification)
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
                .WhereIf((criteria.TenderTypeId != 0), x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(criteria.UserRole == RoleNames.OffersPurchaseSecretary || criteria.UserRole == RoleNames.OffersPurchaseManager, x => x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                .Where(x => (criteria.TenderStatusIds.Contains(x.TenderStatusId)
                    || (x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed && x.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile && (x.DirectPurchaseCommitteeId == criteria.SelectedCommitteeId || (x.IsLowBudgetDirectPurchase.HasValue && x.DirectPurchaseMemberId == criteria.UserId))))
                    && !(x.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed && x.TenderTypeId == (int)Enums.TenderType.ReverseBidding))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new TenderCheckingAndAwardingModel
                {
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    TenderStatusName = t.Status.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    AgencyCode = t.AgencyCode,
                    IsAllOffersRejected = t.Offers.All(o => o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer),
                    BranchId = t.BranchId,
                    CreatedBy = t.CreatedBy,
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                    SubmitionDate = t.SubmitionDate,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    OffersOpeningDate = t.OffersOpeningDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersCheckingDateHijri = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCount = t.Offers.Count(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)),
                    DoIHaveApprovedSideAction = (t.PostQualifications.Where(q => q.IsActive == true && q.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedConfirmed)
                                                 .Any(q => q.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && q.TenderStatusId != (int)Enums.TenderStatus.Established &&
                                                 q.TenderStatusId != (int)Enums.TenderStatus.Pending && q.TenderStatusId != (int)Enums.TenderStatus.Rejected)) ||
                                                 (t.AgencyCommunicationRequests.Where(q => q.IsActive == true && q.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
                                                 .Any(a => a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved)),
                    DoIHavePendingSideAction = (t.PostQualifications.Where(q => q.IsActive == true && q.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedConfirmed)
                                                 .Any(q => q.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && q.TenderStatusId != (int)Enums.TenderStatus.Established &&
                                                 q.TenderStatusId != (int)Enums.TenderStatus.Pending && q.TenderStatusId != (int)Enums.TenderStatus.Rejected)) ||
                                                 (t.AgencyCommunicationRequests.Where(q => q.IsActive == true && q.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
                                                 .Any(a => a.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Approved)),
                    CanStartingAwarding = (t.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed) &&
                                                (t.TenderTypeId != (int)Enums.TenderType.ReverseBidding || t.BiddingRounds.Count > 0),
                    EstimatedValue = t.EstimatedValue,
                    CanShowAwardingReport = t.Offers.Any(o => o.IsActive == true && (o.PartialOfferAwardingValue != null || o.TotalOfferAwardingValue != null)),
                    IsLowBudgetDirectPurchase = t.IsLowBudgetDirectPurchase,
                    DirectPurchaseMemberId = t.DirectPurchaseMemberId,
                    IsLowBudgetAndAssignedMember = t.IsLowBudgetDirectPurchase == true && t.DirectPurchaseMemberId == criteria.UserId
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            var tenderIds = result.Items.Select(i => i.TenderId).Distinct().ToList();
            var changeRequests = await _context.TenderChangeRequests.Where(c => c.IsActive == true && tenderIds.Contains(c.TenderId)).ToListAsync();

            var tenderHistoryList = await _context.TenderHistories.Where(history => tenderIds.Contains(history.TenderId))
                .Select(history => new TenderHistoryModel
                {
                    TenderId = history.TenderId,
                    TenderHistoryId = history.TenderHistoryId,
                    ApprovedBy = history.CreatedBy
                }).ToListAsync();

            foreach (var item in result.Items)
            {
                item.TenderHistoryModels = tenderHistoryList.Where(h => h.TenderId == item.TenderId).ToList();
                item.ApprovedBy = tenderHistoryList.Where(h => h.TenderId == item.TenderId).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().ApprovedBy;
                item.TenderChangeRequestIds = changeRequests.Where(c => c.TenderId == item.TenderId).Select(c => c.TenderChangeRequestId).ToList();
                item.ChangeRequestStatusIds = changeRequests.Where(c => c.TenderId == item.TenderId).Select(c => c.ChangeRequestStatusId).ToList();
                item.ChangeRequestTypeId = changeRequests.Where(c => c.TenderId == item.TenderId).Select(c => c.ChangeRequestTypeId).FirstOrDefault();
                item.ChangeRequestedBy = changeRequests.Where(c => c.TenderId == item.TenderId && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).OrderByDescending(c => c.CreatedAt).Select(s => s.RequestedByRoleName).FirstOrDefault();
                item.CancelRequestStatus = changeRequests.Where(c => c.TenderId == item.TenderId && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).OrderByDescending(c => c.CreatedAt).Select(s => s.ChangeRequestStatusId).FirstOrDefault();
                item.TenderChangeRequestIdsForCheckingSecretary = changeRequests.Where(c => c.TenderId == item.TenderId && c.RequestedByRoleName == RoleNames.OffersCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList();
                item.TenderChangeRequestIdsForCheckingManager = changeRequests.Where(c => c.TenderId == item.TenderId && c.RequestedByRoleName == RoleNames.OffersCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList();
                item.TenderChangeRequestIdsForPurchaseSecretary = changeRequests.Where(c => c.TenderId == item.TenderId && c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList();
                item.TenderChangeRequestIdsForPurchaseManager = changeRequests.Where(c => c.TenderId == item.TenderId && c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList();
                item.TenderChangeRequestIdsForVROSecretary = changeRequests.Where(c => c.TenderId == item.TenderId && c.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList();
                item.TenderChangeRequestIdsForVROManager = changeRequests.Where(c => c.TenderId == item.TenderId && c.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList();
            }
            return result;
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindTenderBySearchCriteriaForAwardingStageForApproveTenderAward(TenderSearchCriteria criteria)
        {
            var branchUser = await _context.BranchUsers
              .Where(u => u.IsActive == true && u.BranchId == criteria.BranchId && u.UserProfileId == criteria.UserId && u.EstimatedValueFrom.HasValue && u.EstimatedValueTo > 0)
              .FirstOrDefaultAsync();
            if (branchUser == null)
            {
                branchUser = new BranchUser();
            }
            var result = await _context.Tenders
                .Where(x => x.IsActive == true && x.BranchId == criteria.BranchId && branchUser.EstimatedValueFrom.HasValue && branchUser.EstimatedValueTo.HasValue && x.EstimatedValue >= branchUser.EstimatedValueFrom && x.EstimatedValue <= branchUser.EstimatedValueTo)

                .Where(x => x.TenderTypeId != (int)Enums.TenderType.FirstStageTender && x.TenderTypeId != (int)Enums.TenderType.PostQualification && x.TenderTypeId != (int)Enums.TenderType.PreQualification)
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
                .WhereIf((criteria.TenderTypeId != 0), x => x.TenderTypeId == criteria.TenderTypeId)
                .Where(x => (criteria.TenderStatusIds.Contains(x.TenderStatusId) || (x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed && x.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile && x.DirectPurchaseCommitteeId == criteria.SelectedCommitteeId))
                    && !(x.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed && x.TenderTypeId == (int)Enums.TenderType.ReverseBidding))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new TenderCheckingAndAwardingModel
                {
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    TenderStatusName = t.Status.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    AgencyCode = t.AgencyCode,
                    IsAllOffersRejected = t.Offers.All(o => o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer),
                    BranchId = t.BranchId,
                    CreatedBy = t.CreatedBy,
                    TenderChangeRequestIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.TenderChangeRequestId).ToList(),
                    ChangeRequestStatusIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatusId).ToList(),
                    ChangeRequestTypeId = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestTypeId).FirstOrDefault(),
                    TenderHistoryModels = t.TenderHistories.Select(x => new TenderHistoryModel { TenderHistoryId = x.TenderHistoryId, ApprovedBy = x.CreatedBy }).ToList(),
                    ChangeRequestedBy = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).OrderByDescending(c => c.CreatedAt).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).OrderByDescending(c => c.CreatedAt).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    TenderChangeRequestIdsForCheckingSecretary = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForCheckingManager = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForPurchaseSecretary = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForPurchaseManager = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForVROSecretary = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForVROManager = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                    SubmitionDate = t.SubmitionDate,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    OffersOpeningDate = t.OffersOpeningDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersCheckingDateHijri = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCount = t.Offers.Count(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)),
                    DoIHaveApprovedSideAction = (t.PostQualifications.Where(q => q.IsActive == true && q.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedConfirmed)
                                                 .Any(q => q.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && q.TenderStatusId != (int)Enums.TenderStatus.Established &&
                                                 q.TenderStatusId != (int)Enums.TenderStatus.Pending && q.TenderStatusId != (int)Enums.TenderStatus.Rejected)) ||
                                                 (t.AgencyCommunicationRequests.Where(q => q.IsActive == true && q.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
                                                 .Any(a => a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved)),
                    DoIHavePendingSideAction = (t.PostQualifications.Where(q => q.IsActive == true && q.TenderStatusId != (int)Enums.TenderStatus.OffersCheckedConfirmed)
                                                 .Any(q => q.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && q.TenderStatusId != (int)Enums.TenderStatus.Established &&
                                                 q.TenderStatusId != (int)Enums.TenderStatus.Pending && q.TenderStatusId != (int)Enums.TenderStatus.Rejected)) ||
                                                 (t.AgencyCommunicationRequests.Where(q => q.IsActive == true && q.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
                                                 .Any(a => a.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Approved)),
                    CanStartingAwarding = (t.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed) &&
                                                (t.TenderTypeId != (int)Enums.TenderType.ReverseBidding || t.BiddingRounds.Count > 0),
                    EstimatedValue = t.EstimatedValue,
                    CanShowAwardingReport = t.Offers.Any(o => o.IsActive == true && (o.PartialOfferAwardingValue != null || o.TotalOfferAwardingValue != null))
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            foreach (var item in result.Items)
            {
                item.ApprovedBy = item.TenderHistoryModels.OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().ApprovedBy;

            }
            return result;
        }

        public async Task<QueryResult<TenderCheckingAndAwardingModel>> FindAwardedTenderBySearchCriteria(TenderSearchCriteria criteria)
        {
            var result = await _context.Tenders.AsNoTracking()
                .Where(x => x.IsActive == true && x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
                .WhereIf(!string.IsNullOrEmpty(criteria.cr), x => x.Offers.Any(f =>
                   string.Equals(criteria.cr, f.CommericalRegisterNo) && f.CommericalRegisterNo.Equals(criteria.cr) && f.IsActive == true))
                   .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
                   .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                   .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
                   .WhereIf((criteria.TenderTypeId != 0 && criteria.TenderTypeId != (int)Enums.TenderType.DirectPurchaseTowTypes && criteria.TenderTypeId != (int)Enums.TenderType.GeneralTernderTwoTypes), x => x.TenderTypeId == criteria.TenderTypeId)
                   .WhereIf((criteria.TenderTypeId == (int)Enums.TenderType.DirectPurchaseTowTypes), x => x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                   .WhereIf((criteria.TenderTypeId == (int)Enums.TenderType.GeneralTernderTwoTypes), x => x.TenderTypeId == (int)Enums.TenderType.NewTender || x.TenderTypeId == (int)Enums.TenderType.CurrentTender)
                   .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new TenderCheckingAndAwardingModel
                {
                    AgencyCode = t.AgencyCode,
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName,
                    TenderNumber = t.TenderNumber,
                    TenderReferenceNumber = t.ReferenceNumber,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderStatusId = t.TenderStatusId,
                    TenderName = t.TenderName,
                    TenderTypeName = (t.TenderTypeId == (int)Enums.TenderType.CurrentTender || t.TenderTypeId == (int)Enums.TenderType.NewTender) ? "منافسة عامة" : (t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) ? "شراء مباشر" : t.TenderType.NameAr,
                    TenderStatusName = t.Status.NameAr,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    SubmitionDate = t.SubmitionDate,
                    OffersOpeningDate = t.OffersOpeningDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCount = t.Offers.Count(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)),
                    FinancialFees = ((t.TenderTypeId == (int)Enums.TenderType.CurrentTender || t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || t.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
                         : (t.Invitations.Any(s => s.IsActive == true)) ? t.TenderType.InvitationCost
                         : t.TenderType.BuyingCost)
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<TenderInformationModel>> GetAllTendersHasEnquiry(TenderSearchCriteria criteria)
        {
            var result = await _context.Tenders
                .Where(x => x.IsActive == true)
                .WhereIf(criteria.BranchId.HasValue && criteria.BranchId.Value != 0, x => x.BranchId == criteria.BranchId)
                .WhereIf(IsInRole(RoleNames.Auditer) || IsInRole(RoleNames.EtimadOfficer), x => x.BranchId == criteria.BranchId && x.Enquiries.Any())
                                .WhereIf(IsInRole(RoleNames.TechnicalCommitteeUser),
                x => (x.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == criteria.UserId && c.CommitteeId == _httpContextAccessor.HttpContext.User.SelectedCommittee() && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_TechnicalCommitteeUser && c.IsActive == true)
                || x.Enquiries.Any(e => e.JoinTechnicalCommittees.Any(j => j.CommitteeId == _httpContextAccessor.HttpContext.User.SelectedCommittee() && j.CommitteeId != x.TechnicalOrganizationId && j.IsActive == true)))
                 && x.Enquiries.Any())
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
                .WhereIf((criteria.TenderTypeId != 0), x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(criteria.EnquiryReplyStatus != null && criteria.EnquiryReplyStatus == (int)Enums.EnquiryReplyStatus.Pending, x => x.Enquiries.All(e => e.IsActive == true && e.EnquiryReplies.All(r => r.EnquiryReplyStatusId == (int)Enums.EnquiryReplyStatus.Pending)))

                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new TenderInformationModel
                {
                    EnquiriesCountForTechnical = t.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == criteria.UserId && t.TechnicalOrganizationId == _httpContextAccessor.HttpContext.User.SelectedCommittee() && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_TechnicalCommitteeUser && c.IsActive == true)
                    ? t.Enquiries.Count(e => e.TenderId == t.TenderId && e.IsActive == true) : t.Enquiries.Count(e => e.JoinTechnicalCommittees.Any(c => c.Committee.CommitteeUsers.Any(x => x.UserProfileId == criteria.UserId && x.IsActive == true && c.IsActive == true)) && t.IsActive == true),

                    EnquiriesCountForAuditor = t.Enquiries.Count(e => e.TenderId == t.TenderId && e.IsActive == true),
                    TenderNumber = t.TenderNumber,
                    TenderTypeId = t.TenderTypeId,
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

        public async Task<List<string>> FinancialYear()
        {
            var result = await _context.TenderHistories
                .Where(t => t.StatusId == (int)Enums.TenderStatus.Approved).Select(t => t.CreatedAt.Year.ToString())
                .Distinct()
                .ToListAsync();
            return result;
        }

        public async Task<List<FavouriteSupplierTender>> GetFavouriteTenderSuppliersToApplyOffer()
        {
            var result = await _context.FavouriteSupplierTenders
              .Include(t => t.Tender)
              .ThenInclude(t => t.Offers)
              .Where(a => !(a.Tender.Offers.Any(o => o.TenderId == a.TenderId && o.CommericalRegisterNo == a.SupplierCRNumber)))
              .Where(a => a.Tender.LastOfferPresentationDate.Value.Date.AddDays(-2) == DateTime.Now.Date)
              .Distinct().ToListAsync();
            return result;
        }

        public async Task<List<Tender>> GetRelatedTendersByActivities(int tenderId)
        {
            Tender tender = await FindTenderActivitiesByTenderId(tenderId);
            List<int> activitiesIDs = tender.TenderActivities.OrderBy(a => a.ActivityId).Select(a => a.ActivityId).Distinct().ToList();
            var result = (_context.Tenders.Include(x => x.Offers).Include(a => a.Status)
                .Include(x => x.TenderActivities)
                .Where(a => a.TenderActivities.Any(ta => activitiesIDs.Contains(ta.ActivityId)) && a.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed))
                .OrderByDescending(a => a.CreatedAt).Take(5).ToList();
            return result;
        }

        public async Task<Invitation> GetInvitation(string commericalRegisterNo, int tenderId)
        {
            var result = await _context.Invitations.Where(x => x.CommericalRegisterNo == commericalRegisterNo
            && x.TenderId == tenderId
            && x.IsActive == true).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Invitation> GetInvitationById(int invitationId)
        {
            var result = await _context.Invitations.Include(i => i.BillInfo).ThenInclude(d => d.BillPaymentDetails)
                .Include(t => t.Tender)
                .ThenInclude(s => s.TenderType)
                .Include(t => t.Tender)
                .ThenInclude(a => a.Agency)
                .Where(x => x.InvitationId == invitationId && x.IsActive == true).FirstOrDefaultAsync();
            return result;
        }
        public async Task<ConditionsBooklet> FindConditionsBookletForRePurchase(int tenderId, string CR)
        {
            var tenderEntity = await _context.ConditionsBooklets
              .Include(x => x.Supplier)
              .Include(x => x.BillInfo)
              .ThenInclude(x => x.BillPaymentDetails)
              .Where(t => t.TenderId == tenderId && t.CommericalRegisterNo == CR).FirstOrDefaultAsync();
            return tenderEntity;

        }

        public async Task<QueryResult<Invitation>> GetNewInvitationsByCRNo(TenderSearchCriteria tenderSearchCriteria)
        {
            var result = await _context.Invitations
                .Include(x => x.Tender)
                .ThenInclude(h => h.Agency)
                .Where(c => c.CommericalRegisterNo == tenderSearchCriteria.crNumber && c.StatusId == (int)InvitationStatus.New && c.Tender.IsActive == true && c.IsActive == true && c.InvitationTypeId == (int)InvitationRequestType.Invitation).ToQueryResult(tenderSearchCriteria.PageNumber, tenderSearchCriteria.PageSize);
            return result;
        }

        public async Task<QueryResult<Tender>> GetAllTenders(string cr)
        {
            var result = await _context.Tenders
                .Include(x => x.Invitations)
                .Include(x => x.ConditionsBooklets)
                .Where(q => q.IsActive == true
                && q.Invitations.Any(c => c.IsActive == true)
                && !(q.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase
                && q.InvitationTypeId == (int)Enums.InvitationType.Specific)
                && q.Invitations.Any(c => c.CommericalRegisterNo == cr))
                .ToQueryResult();
            return result;
        }

        public async Task<QueryResult<TenderInvitationDetailsModel>> GetAllUnsubscribedSupplierTenders(TenderSearchCriteria criteria)
        {

            var result = await _context.Tenders
                .Include(x => x.Invitations)
                .Include(x => x.Enquiries)
                .Include(x => x.TenderActivities)
                .Include(x => x.ConditionsBooklets)
                .Include(x => x.Status)
                .Include(x => x.Agency)
                .Where(q => q.IsActive == true
                && q.TenderStatusId != (int)Enums.TenderStatus.Established && q.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && q.TenderStatusId != (int)Enums.TenderStatus.Pending
                && (!(q.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && q.InvitationTypeId == (int)Enums.InvitationType.Specific)))
                .WhereIf((criteria.TenderTypeId == (int)Enums.TenderType.CurrentTender || criteria.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase), x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(criteria.AgencyCode != "", x => x.AgencyCode == criteria.AgencyCode)
                .WhereIf(criteria.TenderActivityId != null, x => x.TenderActivities.Any(a => a.Activity.ActivityId == criteria.TenderActivityId))
                .WhereIf(criteria.TenderAreasIds != null && criteria.TenderAreasIds.Count != 0, x => x.TenderAreas.Any(a => criteria.TenderAreasIds.Contains(a.AreaId)))
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.ConditionaBookletRange), x => x.ConditionsBookletPrice >= GetBookPrice(criteria.ConditionaBookletRange).start && x.ConditionsBookletPrice <= GetBookPrice(criteria.ConditionaBookletRange).end)
                .Select(x => new TenderInvitationDetailsModel
                {
                    TenderName = x.TenderName,
                    TenderNumber = x.TenderNumber,
                    TenderId = x.TenderId,
                    BranchId = x.BranchId,
                    AgencyCode = x.AgencyCode,
                    BranchName = x.Branch.BranchName,
                    AgencyName = x.Agency.NameArabic,
                    TenderStatusId = x.TenderStatusId,
                    TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                    TenderStatusName = x.Status.NameAr,
                    TenderIdString = Util.Encrypt(x.TenderId),
                    TenderTypeId = x.TenderTypeId,
                    TenderTypeName = x.TenderType.NameAr,
                    CondetionalBookletPrice = x.ConditionsBookletPrice,
                    CreatedAt = x.CreatedAt,
                    LastEnqueriesDate = x.LastEnqueriesDate,
                    LastOfferPresentationDate = x.LastOfferPresentationDate,
                    IsBlocked = false,
                    OffersOpeningDate = x.OffersOpeningDate,
                    LastEnqueriesDateHijri = x.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd"),
                    LastOfferPresentationDateHijri = x.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd"),
                    OffersOpeningDateHijri = x.OffersOpeningDate.HasValue ? x.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    InsideKSA = x.InsideKSA,
                    TenderActivityName = x.TenderActivities.Select(a => a.Activity.NameAr).FirstOrDefault(),
                    TenderActivityNameList = x.TenderActivities.Select(a => a.Activity.NameAr).ToList(),
                    InvitationTypeId = x.InvitationTypeId,
                    InvitationStatusId = x.Invitations.FirstOrDefault(c => c.CommericalRegisterNo == criteria.cr && c.IsActive == true) != null ? x.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == criteria.cr).StatusId : 0,
                    InvitationRequistTypeId = x.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == criteria.cr) != null ? x.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == criteria.cr).InvitationTypeId : 0,
                    IsPurchased = x.ConditionsBooklets.Any(b => b.CommericalRegisterNo == criteria.cr),
                    SubmitionDate = x.SubmitionDate,
                    IsFavouriteTender = x.FavouriteSupplierTenders.Any(f => string.Equals(f.SupplierCRNumber, criteria.cr) && f.IsActive == true),
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);


            return result;
        }

        public async Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierTenders(TenderSearchCriteria tenderSearch)
        {
            return await GetSupplierTenders_Test(tenderSearch);

            #region [OLD CODE REGION]

            DateTime currentDateTime = DateTime.Now;
            var myInvitations = await _context.Invitations.Where(f => f.IsActive == true && f.CommericalRegisterNo == tenderSearch.cr && f.StatusId == (int)Enums.InvitationStatus.Approved).ToListAsync();
            var myOffers = await _context.Offers.Where(o => o.CommericalRegisterNo == tenderSearch.cr && o.OfferStatusId != (int)OfferStatus.Canceled).ToListAsync();
            var myConditionsBooklets = await _context.ConditionsBooklets.Where(b => b.CommericalRegisterNo == tenderSearch.cr).ToListAsync();
            var mySolidarities = await _context.OfferSolidarities.Where(c => c.IsActive == true && c.CRNumber == tenderSearch.cr && c.StatusId != (int)SupplierSolidarityStatus.ToBeSent && c.StatusId != (int)SupplierSolidarityStatus.Rejected && c.Offer.OfferStatusId != (int)OfferStatus.Canceled).Select(d => new
            {
                TenderId = d.Offer.TenderId,
                offerId = d.OfferId,
                solidarityId = d.Id,
                offerStatusId = d.Offer.OfferStatusId,
                solidarityStatusId = d.StatusId,
                solidarityTypeId = d.SolidarityTypeId
            }).ToListAsync();
            var myBills = (await _context.BillInfos.Include(d => d.ConditionsBooklet).Where(c => c.ConditionsBooklet.CommericalRegisterNo == tenderSearch.cr && c.IsActive == true && c.BillStatusId ==
                  (int)Enums.BillStatus.Paid).ToListAsync()).Where(c => myConditionsBooklets.Any(d => d.ConditionsBookletId == c.ConditionsBookletID)).Select(d => new { TenderId = d.ConditionsBooklet.TenderId, billStatusId = d.BillStatusId }).ToList();
            var myFavoriteTenders = await _context.FavouriteSupplierTenders.Where(f => f.IsActive == true && f.SupplierCRNumber == tenderSearch.cr).ToListAsync();
            var tenderTypes = await _context.TenderTypes.Where(f => f.IsActive == true).ToListAsync();
            var allmyTenderIds = new List<int>();
            allmyTenderIds.AddRange(myInvitations.Select(d => d.TenderId).Distinct().ToList());
            allmyTenderIds.AddRange(myConditionsBooklets.Select(d => d.TenderId).Distinct().ToList());
            allmyTenderIds.AddRange(mySolidarities.Select(d => d.TenderId).Distinct().ToList());
            var result = await _context.Tenders
           .Where(x => x.TenderStatusId != (int)Enums.TenderStatus.Pending && x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && x.TenderStatusId != (int)Enums.TenderStatus.Established && x.TenderStatusId != (int)Enums.TenderStatus.Canceled)
           .Where(x => x.TenderStatusId != (int)Enums.TenderStatus.Rejected)
           .Where(x => x.IsActive == true)
             .Where(x => x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification)
                .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.AgencyCode), c => c.AgencyCode == tenderSearch.AgencyCode)
             .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.ReferenceNumber), c => c.ReferenceNumber == tenderSearch.ReferenceNumber)
            .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.TenderName), c => c.TenderName.ToUpper().Trim().Contains(tenderSearch.TenderName.ToUpper().Trim()))
            .Where(t => allmyTenderIds.Any(d => d == t.TenderId))
            .SortBy(tenderSearch.Sort, tenderSearch.SortDirection)
            .Select(x => new TenderInvitationDetailsModel
            {
                TenderName = x.TenderName,
                TenderNumber = x.TenderNumber,
                AgencyCode = x.AgencyCode,
                TenderId = x.TenderId,

                AgencyName = /*x.AgencyCode.Contains("VRO") ? x.Agency.NameArabic + (" - VRO") : */ x.Agency.NameArabic, //out
                TenderStatusId = x.TenderStatusId,
                TenderStatusName = x.Status.NameAr,
                TenderTypeId = x.TenderTypeId,
                TenderTypeName = /*(x.TenderTypeId == (int)Enums.TenderType.CurrentTender || x.TenderTypeId == (int)Enums.TenderType.NewTender) ? "منافسة عامة" : (x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) ? "شراء مباشر" :*/ x.TenderType.NameAr,
                CondetionalBookletPrice = x.ConditionsBookletPrice,
                CreatedAt = x.CreatedAt,
                HasQualification = x.PreQualificationId != null ? true : false,
                LastEnqueriesDate = x.LastEnqueriesDate,

                LastOfferPresentationDate = x.LastOfferPresentationDate,
                OffersOpeningDate = x.OffersOpeningDate,

                OffersCheckingDate = x.OffersCheckingDate,

                InsideKSA = x.InsideKSA,
                ReferenceNumber = x.ReferenceNumber,
                TechnicalOrganizationId = x.TechnicalOrganizationId,
                TenderActivityName = x.TenderActivities.Select(a => a.Activity.NameAr).FirstOrDefault(),
                TenderActivityNameList = x.TenderActivities.Select(a => a.Activity.NameAr).ToList(),
                InvitationTypeId = x.InvitationTypeId,

                SubmitionDate = x.SubmitionDate,
                IsFavouriteShow = false,
                OfferPresentationWayId = x.OfferPresentationWayId.HasValue ? x.OfferPresentationWayId.Value : 0
            })
            .ToQueryResult(tenderSearch.PageNumber, tenderSearch.PageSize);
            List<BiddingRound> BiddingRounds = null;
            var MytenderIds = result.Items.Select(m => m.TenderId).Distinct().ToList();
            if (result.Items.Any(f => f.TenderTypeId == (int)Enums.TenderType.ReverseBidding))
            {
                BiddingRounds = await _context.BiddingRounds.Where(f => f.IsActive == true && MytenderIds.Any(d => d == f.TenderId)).ToListAsync();
            }
            var tenderInv = await _context.Invitations.Where(f => f.IsActive == true && MytenderIds.Any(d => d == f.TenderId)).ToListAsync();
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            bool hasInv = false;
            bool hasOffer = false;
            bool istenderhasInvitations = false;
            foreach (var currentElement in result.Items)
            {
                hasInv = myInvitations.FirstOrDefault(c => c.IsActive == true && c.TenderId == currentElement.TenderId) != null;
                hasOffer = myOffers.Any(c => c.IsActive == true && c.TenderId == currentElement.TenderId);
                istenderhasInvitations = tenderInv.Any(d => d.TenderId == currentElement.TenderId);
                currentElement.IsTenderOwner = currentElement.AgencyCode == tenderSearch.cr ? true : false;
                currentElement.AgencyName = currentElement.AgencyCode.Contains("VRO") ? currentElement.AgencyName + (" - VRO") : currentElement.AgencyName;
                currentElement.TenderStatusIdString = Util.Encrypt(currentElement.TenderStatusId);
                currentElement.TenderIdString = Util.Encrypt(currentElement.TenderId);
                currentElement.TenderTypeName = (currentElement.TenderTypeId == (int)Enums.TenderType.CurrentTender || currentElement.TenderTypeId == (int)Enums.TenderType.NewTender) ? "منافسة عامة" : (currentElement.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || currentElement.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) ? "شراء مباشر" : currentElement.TenderTypeName;
                currentElement.LastEnqueriesDateHijri = currentElement.LastEnqueriesDate.HasValue ? currentElement.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
                currentElement.LastOfferPresentationDateHijri = currentElement.LastOfferPresentationDate.HasValue ? currentElement.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
                currentElement.OffersOpeningDateHijri = currentElement.OffersOpeningDate.HasValue ? currentElement.OffersOpeningDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
                currentElement.OffersCheckingDateHijri = currentElement.OffersCheckingDate.HasValue ? currentElement.OffersCheckingDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
                currentElement.OfferSolidarityId = mySolidarities.FirstOrDefault(d => d.TenderId == currentElement.TenderId) == null ? 0 : mySolidarities.FirstOrDefault(d => d.TenderId == currentElement.TenderId).offerId;
                currentElement.InvitationId = hasInv ? myInvitations.FirstOrDefault(c => c.IsActive == true && c.TenderId == currentElement.TenderId).InvitationId : 0;
                currentElement.OfferId = mySolidarities.FirstOrDefault(d => d.TenderId == currentElement.TenderId && d.solidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader) == null ? 0 : mySolidarities.FirstOrDefault(d => d.TenderId == currentElement.TenderId && d.solidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).offerId;
                currentElement.InvitationStatusId = myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId && c.IsActive == true && c.StatusId != (int)InvitationStatus.Withdrawal) != null ? myInvitations.FirstOrDefault(c => c.IsActive == true && c.StatusId != (int)InvitationStatus.Withdrawal).StatusId : 0;

                currentElement.InvitationRequistTypeId = hasInv ? myInvitations.FirstOrDefault(c => c.IsActive == true && c.TenderId == currentElement.TenderId).InvitationTypeId : 0;
                currentElement.IsPurchased = myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null ? true : false;
                currentElement.IsOfferWithSolidarity = hasOffer && myOffers.FirstOrDefault(c => c.IsActive == true && c.TenderId == currentElement.TenderId).IsSolidarity ? true : false;
                currentElement.IsFavouriteTender = myFavoriteTenders.Any(c => c.IsActive == true && c.TenderId == currentElement.TenderId);
                currentElement.SupplierHasOffer = myOffers.Any(c => c.OfferStatusId == (int)OfferStatus.Received && c.TenderId == currentElement.TenderId);
                currentElement.PaymentStatusId = myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null ? myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId).billStatusId : 0;
                currentElement.OfferStatusId = hasOffer ? myOffers.FirstOrDefault(c => c.IsActive == true && c.TenderId == currentElement.TenderId).OfferStatusId : 0;
                currentElement.IsAvailabletoApplyOffer =
                IsAvaialableToApplyOffer((Enums.TenderType)currentElement.TenderTypeId, (Enums.TenderStatus)currentElement.TenderStatusId,
                       currentElement.SupplierHasOffer, currentElement.LastOfferPresentationDate,
                        mySolidarities.Any(c => c.TenderId == currentElement.TenderId && c.offerStatusId == (int)Enums.OfferStatus.Received && c.solidarityStatusId == (int)Enums.SupplierSolidarityStatus.Approved), hasInv ? myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId).StatusId : 0,
                         currentElement.InvitationTypeId,
                         (myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null ?
                       myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId).billStatusId : 0));

                currentElement.IsAvailabletoBuy = IsAvaialableToPurchase((currentElement.PreQualificationId != null ? true : false),
                      false,
                      tenderSearch.cr == currentElement.AgencyCode,
                   (myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null ?
                       myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId).billStatusId : 0),
                      currentElement.TenderTypeId.Value,
                      currentElement.TenderStatusId,
                     currentElement.LastOfferPresentationDate,
                      (mySolidarities.Any(c => c.TenderId == currentElement.TenderId) && currentElement.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && currentElement.TenderTypeId != (int)Enums.TenderType.CurrentTender && currentElement.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects),
                      (myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId && c.IsActive == true && c.StatusId != (int)InvitationStatus.Rejected && c.StatusId != (int)InvitationStatus.Withdrawal) != null ? myInvitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.TenderId == currentElement.TenderId && c.StatusId != (int)InvitationStatus.Rejected && c.StatusId != (int)InvitationStatus.Withdrawal).StatusId : 0),
                      currentElement.InvitationTypeId);

                currentElement.FinancialFees = ((currentElement.TenderTypeId == (int)Enums.TenderType.CurrentTender || currentElement.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || currentElement.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
                        : istenderhasInvitations ? tenderTypes.FirstOrDefault(d => d.TenderTypeId == currentElement.TenderTypeId).InvitationCost
                        : tenderTypes.FirstOrDefault(d => d.TenderTypeId == currentElement.TenderTypeId).BuyingCost);


                currentElement.CanOpenBiddingPage = currentElement.TenderTypeId == (int)Enums.TenderType.ReverseBidding ?
                        BiddingRounds.Any(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Started && r.StartDate <= currentDateTime.AddMinutes(1) && currentDateTime < r.EndDate) && myOffers.Any(o => o.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer && o.TenderId == currentElement.TenderId) : false;
            };
            return result;

            #endregion [OLD REGION]
        }


        public async Task<QueryResult<TenderInvitationDetailsModel>> GetSupplierTenders_Test(TenderSearchCriteria tenderSearch)
        {
            DateTime currentDateTime = DateTime.Now;
            var result = await _context.Tenders
           .Where(x => x.TenderStatusId != (int)Enums.TenderStatus.Rejected)
           .Where(x => x.IsActive == true)
           .Where(x => x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification)
           .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.AgencyCode), c => c.AgencyCode == tenderSearch.AgencyCode)
           .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.ReferenceNumber), c => c.ReferenceNumber == tenderSearch.ReferenceNumber)
           .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.TenderName), c => c.TenderName.ToUpper().Trim().Contains(tenderSearch.TenderName.ToUpper().Trim()))
           .Where(d => (d.ConditionsBooklets.Any(b => b.CommericalRegisterNo == tenderSearch.cr)
             ||
             d.Invitations.Any(f => f.IsActive == true && f.CommericalRegisterNo == tenderSearch.cr && f.StatusId == (int)Enums.InvitationStatus.Approved)
            ||
            ((d.Offers.Any() && d.Offers.Any(o => o.Combined.Any(c => c.CRNumber == tenderSearch.cr && c.StatusId != (int)SupplierSolidarityStatus.ToBeSent && c.StatusId != (int)SupplierSolidarityStatus.Rejected) && o.OfferStatusId != (int)OfferStatus.Canceled))
                                )))

            .SortBy(tenderSearch.Sort, tenderSearch.SortDirection)
            .Select(x => new TenderInvitationDetailsModel
            {
                TenderName = x.TenderName,
                TenderNumber = x.TenderNumber,
                AgencyCode = x.AgencyCode,
                TenderId = x.TenderId,
                InvitationTypeId = x.InvitationTypeId,
                AgencyName = x.Agency.NameArabic,
                TenderStatusId = x.TenderStatusId,
                TenderStatusName = x.Status.NameAr,
                TenderTypeId = x.TenderTypeId,
                CondetionalBookletPrice = x.ConditionsBookletPrice,
                CreatedAt = x.CreatedAt,
                PreQualificationId = x.PreQualificationId,
                LastEnqueriesDate = x.LastEnqueriesDate,
                LastOfferPresentationDate = x.LastOfferPresentationDate,
                OffersOpeningDate = x.OffersOpeningDate,
                OffersCheckingDate = x.OffersCheckingDate,
                InsideKSA = x.InsideKSA,
                ReferenceNumber = x.ReferenceNumber,
                TechnicalOrganizationId = x.TechnicalOrganizationId,
                SubmitionDate = x.SubmitionDate,
                IsFavouriteShow = false,
                BuyingCost = x.TenderType.BuyingCost,
                InvitationCost = x.TenderType.InvitationCost,
                OfferPresentationWayId = x.OfferPresentationWayId.HasValue ? x.OfferPresentationWayId.Value : 0,
                TenderTypeName = x.TenderType.NameAr,
                IsJoinRequest = x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && x.InvitationTypeId == (int)Enums.InvitationType.Public && x.Invitations.Any(s => s.IsActive == true && s.InvitationTypeId == (int)Enums.InvitationRequestType.Request && s.CommericalRegisterNo == tenderSearch.cr && s.StatusId == (int)Enums.InvitationStatus.Approved)
            })
            .ToQueryResult(tenderSearch.PageNumber, tenderSearch.PageSize);
            #region [GET SUPPLIER DATA]

            List<BiddingRoundVM> BiddingRounds = new List<BiddingRoundVM>();
            var MytenderIds = result.Items.Select(m => m.TenderId).Distinct().ToList();
            var myInvitations = await _context.Invitations.Where(f => f.IsActive == true && MytenderIds.Any(d => d == f.TenderId) && f.CommericalRegisterNo == tenderSearch.cr && f.StatusId == (int)Enums.InvitationStatus.Approved).Select(d => new { d.TenderId, d.StatusId, d.InvitationTypeId, d.InvitationId }).ToListAsync();
            var myOffers = await _context.Offers.Where(o => o.IsActive == true && o.CommericalRegisterNo == tenderSearch.cr && MytenderIds.Any(d => d == o.TenderId) && o.OfferStatusId != (int)OfferStatus.Canceled).Select(s => new { s.TenderId, s.OfferStatusId, s.IsSolidarity, s.OfferTechnicalEvaluationStatusId, s.IsActive }).ToListAsync();
            var mySolidarities = await _context.OfferSolidarities.Include(d => d.Offer).Where(c => c.IsActive == true && c.Offer.IsActive == true && MytenderIds.Any(d => d == c.Offer.TenderId) && c.CRNumber == tenderSearch.cr && c.StatusId != (int)SupplierSolidarityStatus.ToBeSent && c.StatusId != (int)SupplierSolidarityStatus.Rejected && c.Offer.OfferStatusId != (int)OfferStatus.Canceled).Select(d => new
            {
                d.Offer.TenderId,
                offerId = d.OfferId,
                solidarityId = d.Id,
                offerStatusId = d.Offer.OfferStatusId,
                solidarityStatusId = d.StatusId,
                solidarityTypeId = d.SolidarityTypeId
            }).ToListAsync();
            var myBills = await _context.BillInfos.Include(d => d.ConditionsBooklet).Where(c => c.ConditionsBooklet.CommericalRegisterNo == tenderSearch.cr && MytenderIds.Any(d => d == c.ConditionsBooklet.TenderId) && c.IsActive == true && c.BillStatusId ==
                 (int)Enums.BillStatus.Paid).Select(d => new { TenderId = d.ConditionsBooklet.TenderId, billStatusId = d.BillStatusId }).ToListAsync();
            var myFavoriteTenders = await _context.FavouriteSupplierTenders.Where(f => f.IsActive == true && MytenderIds.Any(d => d == f.TenderId) && f.SupplierCRNumber == tenderSearch.cr).Select(d => d.TenderId).ToListAsync();
            if (result.Items.Any(f => f.TenderTypeId == (int)Enums.TenderType.ReverseBidding))
            {
                BiddingRounds = await _context.BiddingRounds.Where(f => f.IsActive == true && MytenderIds.Any(d => d == f.TenderId)).Select(d => new BiddingRoundVM { BiddingRoundStatusId = d.BiddingRoundStatusId, StartDate = d.StartDate, EndDate = d.EndDate }).ToListAsync();
            }
            var tenderInv = await _context.Invitations.Where(f => f.IsActive == true && MytenderIds.Any(d => d == f.TenderId)).Select(d => new { d.InvitationId, d.TenderId }).ToListAsync();

            #endregion
            #region [CONFIGURE OTHER DATA]
            bool hasInv = false;
            bool hasOffer = false;
            bool istenderhasInvitations = false;

            foreach (var currentElement in result.Items)
            {
                hasInv = myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null;
                hasOffer = myOffers.Any(c => c.IsActive == true && c.TenderId == currentElement.TenderId);
                istenderhasInvitations = tenderInv.Any(d => d.TenderId == currentElement.TenderId);
                currentElement.IsTenderOwner = currentElement.AgencyCode == tenderSearch.cr ? true : false;
                currentElement.AgencyName = currentElement.AgencyCode.Contains("VRO") ? currentElement.AgencyName + (" - VRO") : currentElement.AgencyName;
                currentElement.TenderStatusIdString = Util.Encrypt(currentElement.TenderStatusId);
                currentElement.TenderIdString = Util.Encrypt(currentElement.TenderId);
                currentElement.TenderTypeName = (currentElement.TenderTypeId == (int)Enums.TenderType.CurrentTender || currentElement.TenderTypeId == (int)Enums.TenderType.NewTender) ? "منافسة عامة" : (currentElement.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || currentElement.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) ? "شراء مباشر" : currentElement.TenderTypeName;
                currentElement.LastEnqueriesDateHijri = currentElement.LastEnqueriesDate.HasValue ? currentElement.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
                currentElement.LastOfferPresentationDateHijri = currentElement.LastOfferPresentationDate.HasValue ? currentElement.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
                currentElement.OffersOpeningDateHijri = currentElement.OffersOpeningDate.HasValue ? currentElement.OffersOpeningDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
                currentElement.OffersCheckingDateHijri = currentElement.OffersCheckingDate.HasValue ? currentElement.OffersCheckingDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
                currentElement.OfferSolidarityId = mySolidarities.FirstOrDefault(d => d.TenderId == currentElement.TenderId) == null ? 0 : mySolidarities.FirstOrDefault(d => d.TenderId == currentElement.TenderId).offerId;
                currentElement.InvitationId = hasInv ? myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId).InvitationId : 0;
                currentElement.OfferId = mySolidarities.FirstOrDefault(d => d.TenderId == currentElement.TenderId && d.solidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader) == null ? 0 : mySolidarities.FirstOrDefault(d => d.TenderId == currentElement.TenderId && d.solidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).offerId;
                currentElement.HasQualification = currentElement.PreQualificationId != null ? true : false;
                currentElement.InvitationStatusId = myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId && c.StatusId != (int)InvitationStatus.Withdrawal) != null ? myInvitations.FirstOrDefault(c => c.StatusId != (int)InvitationStatus.Withdrawal).StatusId : 0;
                currentElement.InvitationRequistTypeId = hasInv ? myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId).InvitationTypeId : 0;
                currentElement.IsPurchased = myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null ? true : false;

                currentElement.IsOfferWithSolidarity = hasOffer && myOffers.FirstOrDefault(c => c.IsActive == true && c.TenderId == currentElement.TenderId).IsSolidarity ? true : false;
                currentElement.IsFavouriteTender = myFavoriteTenders.Any(c => c == currentElement.TenderId);
                currentElement.SupplierHasOffer = myOffers.Any(c => c.OfferStatusId == (int)OfferStatus.Received && c.TenderId == currentElement.TenderId);

                currentElement.PaymentStatusId = myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null ? myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId).billStatusId : 0;
                currentElement.OfferStatusId = hasOffer ? myOffers.FirstOrDefault(c => c.IsActive == true && c.TenderId == currentElement.TenderId).OfferStatusId : 0;
                currentElement.IsAvailabletoApplyOffer =
                IsAvaialableToApplyOffer((Enums.TenderType)currentElement.TenderTypeId, (Enums.TenderStatus)currentElement.TenderStatusId,
                       currentElement.SupplierHasOffer, currentElement.LastOfferPresentationDate,
                        mySolidarities.Any(c => c.TenderId == currentElement.TenderId && c.offerStatusId == (int)Enums.OfferStatus.Received && c.solidarityStatusId == (int)Enums.SupplierSolidarityStatus.Approved), hasInv ? myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId).StatusId : 0,
                         currentElement.InvitationTypeId,
                         (myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null ?
                       myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId).billStatusId : 0));

                currentElement.IsAvailabletoBuy = IsAvaialableToPurchase((currentElement.PreQualificationId != null ? true : false),
                      false,
                      tenderSearch.cr == currentElement.AgencyCode,
                   (myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId) != null ?
                       myBills.FirstOrDefault(c => c.TenderId == currentElement.TenderId).billStatusId : 0),
                      currentElement.TenderTypeId.Value,
                      currentElement.TenderStatusId,
                     currentElement.LastOfferPresentationDate,
                      (mySolidarities.Any(c => c.TenderId == currentElement.TenderId) && currentElement.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && currentElement.TenderTypeId != (int)Enums.TenderType.CurrentTender && currentElement.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects),
                      (myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId && c.StatusId != (int)InvitationStatus.Rejected && c.StatusId != (int)InvitationStatus.Withdrawal) != null ? myInvitations.FirstOrDefault(c => c.TenderId == currentElement.TenderId && c.StatusId != (int)InvitationStatus.Rejected && c.StatusId != (int)InvitationStatus.Withdrawal).StatusId : 0),
                      currentElement.InvitationTypeId);
                currentElement.FinancialFees = ((currentElement.TenderTypeId == (int)Enums.TenderType.CurrentTender || currentElement.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || currentElement.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
                        : istenderhasInvitations ? currentElement.InvitationCost
                        : currentElement.BuyingCost);

                currentElement.CanOpenBiddingPage = currentElement.TenderTypeId == (int)Enums.TenderType.ReverseBidding ?
                        BiddingRounds.Any(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Started && r.StartDate <= currentDateTime.AddMinutes(1) && currentDateTime < r.EndDate) && myOffers.Any(o => o.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer && o.TenderId == currentElement.TenderId) : false;
            };

            #endregion
            return result;
        }


        public async Task<QueryResult<TenderInvitationDetailsModel>> GetAllSupplierTenders(TenderSearchCriteria tenderSearch, List<SupplierAgencyBlockModel> _allSupplierBlock)
        {
            decimal StartRange = 0;
            decimal endRang = 0;

            var allSupplierBlock = _allSupplierBlock.Select(d => d.AgencyCode).AsQueryable();
            if (!String.IsNullOrEmpty(tenderSearch.ConditionaBookletRange))
            {
                var x = GetBookPrice(tenderSearch.ConditionaBookletRange);
                StartRange = x.start;
                endRang = x.end;
            }

            var result = await _context.Tenders
                                    .Where(x => x.IsActive == true && x.SubmitionDate != null && x.TenderStatusId != (int)Enums.TenderStatus.Canceled)

                     .Where(x => x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification)

                .Where(d => d.Invitations.Any(f => f.IsActive == true && f.CommericalRegisterNo == tenderSearch.cr && (f.StatusId == (int)Enums.InvitationStatus.New || f.StatusId == (int)Enums.InvitationStatus.WaitingPayment)) || ((d.InvitationTypeId.Value == (int)Enums.InvitationType.Public && !d.Invitations.Any(s => s.IsActive == true && s.CommericalRegisterNo == tenderSearch.cr)) || d.InvitationTypeId == null))
                .Where(d => !((d.Offers.Count > 0 && d.Offers.Any(o => o.CommericalRegisterNo == tenderSearch.cr && o.OfferStatusId != (int)OfferStatus.Canceled))
                             || (d.ConditionsBooklets.Any(b => b.IsActive == true && b.CommericalRegisterNo == tenderSearch.cr))))


                .WhereIf(tenderSearch.OnlyGetFavouriteTenders, x => x.FavouriteSupplierTenders.Any(t => t.IsActive == true && t.SupplierCRNumber == tenderSearch.cr))
                      .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.TenderName), x => x.TenderName.ToUpper().Trim().Contains(tenderSearch.TenderName.ToUpper().Trim()))
                    .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.ReferenceNumber), x => x.ReferenceNumber == tenderSearch.ReferenceNumber)
                     .WhereIf((tenderSearch.TenderTypeId != 0 && tenderSearch.TenderTypeId != (int)Enums.TenderType.DirectPurchaseTowTypes && tenderSearch.TenderTypeId != (int)Enums.TenderType.GeneralTernderTwoTypes), x => x.TenderTypeId == tenderSearch.TenderTypeId)
                     .WhereIf((tenderSearch.TenderTypeId == (int)Enums.TenderType.DirectPurchaseTowTypes), x => x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                     .WhereIf((tenderSearch.TenderTypeId == (int)Enums.TenderType.GeneralTernderTwoTypes), x => x.TenderTypeId == (int)Enums.TenderType.NewTender || x.TenderTypeId == (int)Enums.TenderType.CurrentTender)
                     .WhereIf((tenderSearch.FromLastOfferPresentationDate != null && tenderSearch.FromLastOfferPresentationDate != DateTime.MinValue), x =>
                     x.LastOfferPresentationDate.Value.Date >= tenderSearch.FromLastOfferPresentationDate.Value)
                     .WhereIf((tenderSearch.ToLastOfferPresentationDate != null && tenderSearch.ToLastOfferPresentationDate != DateTime.MinValue), x => x.LastOfferPresentationDate.Value.Date <= tenderSearch.ToLastOfferPresentationDate.Value.Date)
                     .WhereIf(!string.IsNullOrEmpty(tenderSearch.AgencyCode), x => x.AgencyCode == tenderSearch.AgencyCode)
                     .WhereIf(tenderSearch.TenderActivityId != null, x => x.TenderActivities.Any(a => a.Activity.ActivityId == tenderSearch.TenderActivityId) || x.TenderActivities.Any(a => a.Activity.ParentID == tenderSearch.TenderActivityId))
                     .WhereIf(tenderSearch.TenderSubActivityId != null, x => x.TenderActivities.Any(a => a.Activity.ActivityId == tenderSearch.TenderSubActivityId))
                     .WhereIf(tenderSearch.TenderAreasIds != null && tenderSearch.TenderAreasIds.Count != 0, x => x.TenderAreas.Any(a => tenderSearch.TenderAreasIds.Contains(a.AreaId)))
                     .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.ConditionaBookletRange), x => (StartRange == 0 && endRang == 0 && (x.ConditionsBookletPrice == null)) || ((x.ConditionsBookletPrice + ((x.TenderTypeId == (int)Enums.TenderType.CurrentTender || x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
                         : (x.Invitations.Any(s => s.IsActive == true)) ? x.TenderType.InvitationCost
                         : x.TenderType.BuyingCost)) >= StartRange && (x.ConditionsBookletPrice + ((x.TenderTypeId == (int)Enums.TenderType.CurrentTender || x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
                         : (x.Invitations.Any(s => s.IsActive == true)) ? x.TenderType.InvitationCost
                         : x.TenderType.BuyingCost)) <= endRang))

                     .WhereIf(!string.IsNullOrWhiteSpace(tenderSearch.PublishDate), x => x.SubmitionDate >= DateTime.Now.AddDays(Convert.ToInt16(tenderSearch.PublishDate) * -1))
                     .WhereIf(tenderSearch.TenderCategory == (int)TenderCategory.ActiveTenders || tenderSearch.TenderCategory == null || tenderSearch.TenderCategory == 0,
                     x => x.TenderStatusId == (int)Enums.TenderStatus.Approved && x.LastOfferPresentationDate >= DateTime.Now
                     && (!x.ConditionsBooklets.Any(c => c.Supplier.SelectedCr == tenderSearch.cr)))
                     .WhereIf(tenderSearch.TenderCategory == (int)TenderCategory.EndedTenders,
                     x => x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && x.LastOfferPresentationDate <= DateTime.Now).SortBy(tenderSearch.Sort, tenderSearch.SortDirection)
                     .Select(x => new TenderInvitationDetailsModel
                     {
                         OfferId = x.Offers.Where(o => o.OfferStatusId == (int)OfferStatus.Received && o.IsActive == true && o.CommericalRegisterNo == tenderSearch.cr).Select(s => s.OfferId).FirstOrDefault(),
                         OfferSolidarityId = x.Offers.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr || (c.Combined.Any(o => (o is OfferSolidarity) && ((OfferSolidarity)o).CRNumber == tenderSearch.cr && ((OfferSolidarity)o).StatusId == ((int)Enums.SupplierSolidarityStatus.Approved)))) != null ? x.Offers.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr || (c.Combined.Any(o => (o is OfferSolidarity) && ((OfferSolidarity)o).CRNumber == tenderSearch.cr && ((OfferSolidarity)o).StatusId == ((int)Enums.SupplierSolidarityStatus.Approved)))).OfferId : 0,
                         TenderName = x.TenderName,
                         TenderNumber = x.TenderNumber,
                         ReferenceNumber = x.ReferenceNumber,
                         TenderId = x.TenderId,
                         BranchId = x.BranchId,
                         AgencyCode = x.AgencyCode,
                         BranchName = x.Branch.BranchName,
                         AgencyName = x.Agency.NameArabic,
                         TenderStatusId = x.TenderStatusId,
                         RejectionReason = x.TenderHistories.Where(h => h.StatusId == x.TenderStatusId).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().RejectionReason,
                         HasQualification = x.PreQualificationId != null ? true : false,
                         TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                         TenderStatusName = x.Status.NameAr,
                         TenderIdString = Util.Encrypt(x.TenderId),
                         TenderTypeId = x.TenderTypeId,
                         TenderTypeName = (x.TenderTypeId == (int)Enums.TenderType.CurrentTender || x.TenderTypeId == (int)Enums.TenderType.NewTender) ? "منافسة عامة" : (x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) ? "شراء مباشر" : x.TenderType.NameAr,
                         TechnicalOrganizationId = x.TechnicalOrganizationId,
                         CondetionalBookletPrice = x.ConditionsBookletPrice,
                         CreatedAt = x.CreatedAt,
                         FinancialFees = ((x.TenderTypeId == (int)Enums.TenderType.CurrentTender || x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
                         : (x.Invitations.Any(s => s.IsActive == true)) ? x.TenderType.InvitationCost
                         : x.TenderType.BuyingCost),
                         LastEnqueriesDate = x.LastEnqueriesDate,
                         LastOfferPresentationDate = x.LastOfferPresentationDate,
                         IsBlocked = (allSupplierBlock.Any() && allSupplierBlock.Any(b => b == x.AgencyCode || b == null)),
                         OffersOpeningDate = x.OffersOpeningDate,
                         LastEnqueriesDateHijri = x.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd"),
                         LastOfferPresentationDateHijri = x.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd"),
                         OffersOpeningDateHijri = x.OffersOpeningDate.HasValue ? x.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                         OffersCheckingDate = x.OffersCheckingDate,
                         OffersCheckingDateHijri = x.OffersCheckingDate.HasValue ? x.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                         InsideKSA = x.InsideKSA,
                         TenderActivityName = x.TenderActivities.Select(a => a.Activity.NameAr).FirstOrDefault(),
                         TenderActivityNameList = x.TenderActivities.Select(a => a.Activity.NameAr).ToList(),
                         InvitationTypeId = x.InvitationTypeId,
                         InvitationId = x.Invitations.Where(s => s.IsActive == true && s.StatusId != (int)Enums.InvitationStatus.Withdrawal).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr) != null ? x.Invitations.Where(s => s.IsActive == true && s.StatusId != (int)Enums.InvitationStatus.Withdrawal).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr).InvitationId : 0,
                         InvitationIdString = Util.Encrypt(x.Invitations.Where(s => s.IsActive == true && s.StatusId != (int)Enums.InvitationStatus.Withdrawal).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr) != null ? x.Invitations.Where(s => s.IsActive == true && s.StatusId != (int)Enums.InvitationStatus.Withdrawal).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr).InvitationId : 0),
                         InvitationStatusId = x.Invitations.FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr && c.IsActive == true && c.StatusId != (int)Enums.InvitationStatus.Withdrawal) != null ?
                         x.Invitations.Where(s => s.IsActive == true && s.StatusId != (int)Enums.InvitationStatus.Withdrawal).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr).StatusId : 0,
                         InvitationStatusString = Util.Encrypt(x.Invitations.FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr && c.IsActive == true && c.StatusId != (int)Enums.InvitationStatus.Withdrawal) != null ? x.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr).StatusId : 0),
                         InvitationRequistTypeId = x.Invitations.Where(s => s.IsActive == true && s.StatusId != (int)Enums.InvitationStatus.Withdrawal).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr /*&& c.IsActive == true*/) != null ? x.Invitations.Where(s => s.IsActive == true && s.StatusId != (int)Enums.InvitationStatus.Withdrawal).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr).InvitationTypeId : 0,
                         IsPurchased = x.ConditionsBooklets.Where(b => b.CommericalRegisterNo == tenderSearch.cr && b.IsActive == true && b.BillInfo != null && b.BillInfo.ExpiryDate > DateTime.Now).FirstOrDefault() == null ? false : true,
                         OfferPresentationWayId = x.OfferPresentationWayId.HasValue ? x.OfferPresentationWayId.Value : 0,
                         CanPurchase = (x.TenderTypeId == (int)Enums.TenderType.NewTender && (x.PreQualificationId == null || x.Invitations.Any(i => i.CommericalRegisterNo == tenderSearch.cr && i.IsActive == true && i.StatusId == (int)InvitationStatus.Approved)))
                         || ((x.TenderTypeId == (int)Enums.TenderType.LimitedTender || x.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || x.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
                         && (x.InvitationTypeId == (int)Enums.InvitationType.Public || x.Invitations.Any(i => i.CommericalRegisterNo == tenderSearch.cr && i.IsActive == true && i.StatusId == (int)InvitationStatus.Approved)))
                         || (x.TenderTypeId == (int)Enums.TenderType.FirstStageTender || x.TenderTypeId == (int)Enums.TenderType.CurrentTender),
                         IsTenderOwner = x.AgencyCode == tenderSearch.cr ? true : false,
                         SubmitionDate = x.SubmitionDate,
                         IsInvitedToSolidarity = x.Offers.Where(r => r.OfferStatusId == (int)Enums.OfferStatus.Received && r.IsActive == true && r.Combined.Any(o => (o is OfferSolidarity) && ((OfferSolidarity)o).CRNumber == tenderSearch.cr)).Any()
                         && x.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.CurrentTender && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects,
                         IsOfferWithSolidarity = x.Offers.Any(o => o.CommericalRegisterNo == tenderSearch.cr) ? x.Offers.FirstOrDefault(o => o.CommericalRegisterNo == tenderSearch.cr).IsSolidarity : false,
                         IsFavouriteTender = x.FavouriteSupplierTenders.Any(f => string.Equals(f.SupplierCRNumber, tenderSearch.cr) && f.IsActive == true),

                         SupplierHasOffer = x.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true).Any(o => o.CommericalRegisterNo == tenderSearch.cr),
                         PaymentStatusId = x.ConditionsBooklets.Where(b => b.CommericalRegisterNo == tenderSearch.cr).FirstOrDefault() != null ? x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == tenderSearch.cr).BillInfo != null ? x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == tenderSearch.cr).BillInfo.BillStatusId : 0 : 0,
                         OfferStatusId = x.Offers.Where(b => b.CommericalRegisterNo == tenderSearch.cr).FirstOrDefault() != null ? x.Offers.Where(b => b.CommericalRegisterNo == tenderSearch.cr).FirstOrDefault().OfferStatusId : 0,
                         IsAvailabletoApplyOffer = IsAvaialableToApplyOffer(
                        (Enums.TenderType)x.TenderTypeId,
                        (Enums.TenderStatus)x.TenderStatusId,
                         x.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true).Any(o => o.CommericalRegisterNo == tenderSearch.cr),
                         x.LastOfferPresentationDate.Value,
                        (x.Offers.Where(r => r.OfferStatusId == (int)Enums.OfferStatus.Received && r.IsActive == true && r.Combined.Any(o => (o is OfferSolidarity) && ((OfferSolidarity)o).CRNumber == tenderSearch.cr && ((OfferSolidarity)o).StatusId == ((int)Enums.SupplierSolidarityStatus.Approved))).Any()),
                    (x.Invitations.FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr && c.IsActive == true && c.StatusId != (int)Enums.InvitationStatus.Rejected && c.StatusId != (int)Enums.InvitationStatus.Withdrawal) != null ? x.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr && c.StatusId != (int)Enums.InvitationStatus.Rejected && c.StatusId != (int)Enums.InvitationStatus.Withdrawal).StatusId : 0),
                        x.InvitationTypeId,
                         (x.ConditionsBooklets.Where(b => b.CommericalRegisterNo == tenderSearch.cr).FirstOrDefault() != null ?
                   x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == tenderSearch.cr).BillInfo != null ?
                   x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == tenderSearch.cr).BillInfo.BillStatusId : 0 : 0)
                        ),
                         IsAvailabletoBuy = IsAvaialableToPurchase(
                        (x.PreQualificationId != null ? true : false),
                       (allSupplierBlock.Any() &&
                        allSupplierBlock.Any(b => b == x.AgencyCode || b == null)),
                         tenderSearch.cr == x.AgencyCode,
                    (x.ConditionsBooklets.Where(b => b.CommericalRegisterNo == tenderSearch.cr).FirstOrDefault() != null ? x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == tenderSearch.cr).BillInfo != null ? x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == tenderSearch.cr).BillInfo.BillStatusId : 0 : 0),
                    x.TenderTypeId,
                        x.TenderStatusId,
                         x.LastOfferPresentationDate,
                       (x.Offers.Where(r => r.OfferStatusId == (int)Enums.OfferStatus.Received && r.IsActive == true && r.Combined.Any(o => (o is OfferSolidarity) && ((OfferSolidarity)o).CRNumber == tenderSearch.cr && ((OfferSolidarity)o).StatusId == ((int)Enums.SupplierSolidarityStatus.Approved))).Any() && x.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.CurrentTender && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects),
                         (x.Invitations.FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr && c.IsActive == true && c.StatusId != (int)Enums.InvitationStatus.Rejected && c.StatusId != (int)Enums.InvitationStatus.Withdrawal) != null ? x.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == tenderSearch.cr && c.StatusId != (int)Enums.InvitationStatus.Rejected && c.StatusId != (int)Enums.InvitationStatus.Withdrawal).StatusId : 0),
                       x.InvitationTypeId
                        ),
                     }).ToQueryResult(tenderSearch.PageNumber, tenderSearch.PageSize);
            return result;
        }

        public async Task<QueryResult<AllTendersForVisitorModel>> GetAllTendersForVisitor(TenderSearchCriteria criteria)
        {
            decimal StartRange = 0;
            decimal endRang = 0;
            if (!string.IsNullOrEmpty(criteria.ConditionaBookletRange))
            {
                var x = GetBookPrice(criteria.ConditionaBookletRange);
                StartRange = x.start;
                endRang = x.end;
            }
            var result = await _context.Tenders
    .Where(q => q.IsActive == true && q.SubmitionDate != null)
    .Where(q => q.InvitationTypeId == (int)Enums.InvitationType.Public)
    .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
    .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
     .WhereIf((criteria.TenderTypeId != 0), x => x.TenderTypeId == criteria.TenderTypeId)
    .WhereIf((criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)
    .WhereIf((criteria.ReferenceNumber != null), x => x.ReferenceNumber == criteria.ReferenceNumber)
    .WhereIf((criteria.FromLastOfferPresentationDate != null && criteria.FromLastOfferPresentationDate != DateTime.MinValue), x => x.LastOfferPresentationDate >= criteria.FromLastOfferPresentationDate && x.LastOfferPresentationDate.Value.Date <= criteria.ToLastOfferPresentationDate.Value.Date)
    .WhereIf(criteria.TenderActivityId != null, x => x.TenderActivities.Any(a => a.Activity.ActivityId == criteria.TenderActivityId) || x.TenderActivities.Any(a => a.Activity.ParentID == criteria.TenderActivityId))
    .WhereIf(criteria.TenderSubActivityId != null, x => x.TenderActivities.Any(a => a.Activity.ActivityId == criteria.TenderSubActivityId))
    .WhereIf(criteria.TenderAreasIds != null && criteria.TenderAreasIds.Count != 0, x => x.TenderAreas.Any(a => criteria.TenderAreasIds.Contains(a.AreaId)))
    .WhereIf(!string.IsNullOrWhiteSpace(criteria.ConditionaBookletRange), x => (StartRange == 0 && endRang == 0 && (x.ConditionsBookletPrice == null)) || (x.ConditionsBookletPrice >= StartRange && x.ConditionsBookletPrice <= endRang))
    .WhereIf(!string.IsNullOrWhiteSpace(criteria.PublishDate), x => x.SubmitionDate >= DateTime.Now.AddDays(Convert.ToInt16(criteria.PublishDate) * -1))
    .WhereIf(criteria.TenderCategory == (int)TenderCategory.ActiveTenders || criteria.TenderCategory == null || criteria.TenderCategory == 0,
    x => x.TenderStatusId == (int)Enums.TenderStatus.Approved && x.LastOfferPresentationDate >= DateTime.Now).WhereIf(criteria.TenderCategory == (int)TenderCategory.EndedTenders,
    x => x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && x.LastOfferPresentationDate <= DateTime.Now).SortBy(criteria.Sort, criteria.SortDirection)
    .Select(x => new AllTendersForVisitorModel
    {
        TenderName = x.TenderName,
        TenderNumber = x.TenderNumber,
        ReferenceNumber = x.ReferenceNumber,
        TenderId = x.TenderId,
        BranchId = x.BranchId,
        AgencyCode = x.AgencyCode,
        BranchName = x.Branch.BranchName,
        AgencyName = x.Agency.NameArabic,
        TenderStatusId = x.TenderStatusId,
        TenderStatusName = x.Status.NameAr,
        TenderTypeId = x.TenderTypeId,
        TenderTypeName = x.TenderType.NameAr,
        TechnicalOrganizationId = x.TechnicalOrganizationId,
        CondetionalBookletPrice = x.ConditionsBookletPrice,
        CreatedAt = x.CreatedAt,
        LastEnqueriesDate = x.LastEnqueriesDate,
        LastOfferPresentationDate = x.LastOfferPresentationDate,
        OffersOpeningDate = x.OffersOpeningDate,
        InsideKSA = x.InsideKSA,
        TenderActivityName = x.TenderActivities.Select(a => a.Activity.NameAr).FirstOrDefault(),
        TenderActivityNameList = x.TenderActivities.Select(a => a.Activity.NameAr).ToList(),
        SubmitionDate = x.SubmitionDate,
        FinancialFees = ((x.TenderTypeId == (int)Enums.TenderType.CurrentTender || x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
             : (x.Invitations.Any(s => s.IsActive == true)) ? x.TenderType.InvitationCost
             : x.TenderType.BuyingCost)
    }).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            foreach (var item in result.Items)
            {
                item.TenderStatusIdString = Util.Encrypt(item.TenderStatusId);
                item.TenderIdString = Util.Encrypt(item.TenderId);
                item.LastEnqueriesDateHijri = item.LastEnqueriesDate.HasValue ? item.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
                item.LastOfferPresentationDateHijri = item.LastOfferPresentationDate.HasValue ? item.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
                item.OffersOpeningDateHijri = item.OffersOpeningDate.HasValue ? item.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
            }
            return result;
        }

        public BookletPriceRange GetBookPrice(string range)
        {
            BookletPriceRange obj = new BookletPriceRange(range);
            return obj;
        }

        public async Task<QueryResult<Invitation>> GetSuppliersJoiningRequestsByTenderId(int tenderId)
        {
            var result = await _context.Invitations
                .Include(x => x.Tender)
                .Include(x => x.Supplier)
                 .Include(x => x.InvitationStatus)
                .Where(c => c.TenderId == tenderId && c.InvitationTypeId == (int)InvitationRequestType.Request && c.StatusId == (int)InvitationStatus.New && c.IsActive == true).ToQueryResult();
            return result;
        }

        public async Task<Invitation> GetJoiningRequestByInvitationId(int invitationId)
        {
            var result = await _context.Invitations
                .Include(x => x.Tender)
                .Include(x => x.Supplier)
                .Where(c => c.InvitationId == invitationId
                && c.InvitationTypeId == (int)InvitationRequestType.Request
                && c.IsActive == true).FirstOrDefaultAsync();
            return result;
        }

        public async Task<QueryResult<TenderOpenOfferModel>> GetOffersToOpeningByTenderIdAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            var result = await _context.Offers
                           .Where(t => t.TenderId == tenderBasicSearchCriteria.TenderId && t.IsActive == true && t.OfferStatusId == (int)Enums.OfferStatus.Received)
                           .Select(offer => new TenderOpenOfferModel
                           {
                               CommericalRegisterName = offer.Supplier.SelectedCrName,
                               CommericalRegisterNo = offer.CommericalRegisterNo,
                               OfferId = offer.OfferId,
                               FinalPriceAfterDiscount = offer.FinalPriceAfterDiscount,
                               OfferStatusId = offer.OfferStatusId,
                               CombinedCount = offer.Combined.Count(),
                               CombinedIdString = offer.Combined.Count() == 1 ? Util.Encrypt(offer.Combined.Where(a => a.OfferId == offer.OfferId).Select(x => x.Id).FirstOrDefault()) : "",

                               InvitationDate = offer.Tender.Invitations.Count > 0 && offer.Tender.ConditionsBooklets.Count == 0 ?
                               (offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SubmissionDate.HasValue ?
                               offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).SubmissionDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "")
                               :
                               offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.PurchaseDate.HasValue ?
                               offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.PurchaseDate.Value.ToHijriDateWithFormat("dd/MM/yyyy")
                               : "",
                               InvitationStatus = offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo) != null ?
                               (offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased) :
                               offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo) != null ?
                               offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).InvitationStatus.NameAr :
                               Resources.TenderResources.DisplayInputs.NotPurchased,
                               MainActivity = string.Join(" , ", offer.Tender.TenderActivities.Select(s => s.Activity.NameAr).ToArray()),
                               OfferIdString = Util.Encrypt(offer.OfferId),
                               OfferPresentationWay = offer.Tender.OfferPresentationWayId.HasValue ? offer.Tender.OfferPresentationWayId : 0,
                               OfferStatusName = offer.Status.NameAr,
                               TenderStatusId = offer.Tender.TenderStatusId,
                               IsSolidarity = offer.IsSolidarity,
                               OfferPresentationType = offer.IsSolidarity ? Resources.OfferResources.DisplayInputs.WithSolidarity : Resources.OfferResources.DisplayInputs.WithoutSolidarity,
                               OpeningState = offer.Combined.Count(w => w.SupplierCombinedDetail == null) > 0 ? "لم يتم فتح العرض" : "تم فتح العرض",
                           }).ToQueryResult(tenderBasicSearchCriteria.PageNumber, tenderBasicSearchCriteria.PageSize);
            return result;
        }

        public async Task<QueryResult<Offer>> GetOffersByTenderIdAsync(int tenderId)
        {
            var result = await _context.Offers
                .Where(t => t.TenderId == tenderId && t.IsActive == true && t.OfferStatusId == (int)Enums.OfferStatus.Received)
                .Include(t => t.Status)
                .Include(t => t.Tender).ThenInclude(i => i.Invitations).ThenInclude(inv => inv.InvitationStatus)
                .Include(t => t.Tender).ThenInclude(i => i.InvitationType)
                .Include(t => t.Tender).ThenInclude(i => i.TenderType)
                .Include(t => t.Tender).ThenInclude(i => i.ConditionsBooklets).ThenInclude(i => i.BillInfo).ThenInclude(ii => ii.BillStatus)
                .Include(t => t.Tender).ThenInclude(t => t.TenderActivities)
                .ThenInclude(c => c.Activity)
                .Include(t => t.Supplier).ThenInclude(i => i.SupplierUserProfiles).ToQueryResult();
            return result;
        }

        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersByTenderIdForCheckingGridAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            var verid = await GetCurrentTenderActivityVersion(tenderBasicSearchCriteria.TenderId);
            var tender = await _context.Tenders.FirstOrDefaultAsync(d => d.TenderId == tenderBasicSearchCriteria.TenderId);
            var isActivityContainSupply =

                   tender.IsTenderContainsTawreedTables.HasValue && tender.IsTenderContainsTawreedTables.Value
                || await _context.TenderActivities.AnyAsync(a => a.TenderId == tenderBasicSearchCriteria.TenderId && a.IsActive == true &&
        (
        verid >= (int)Enums.ActivityVersions.Sprint7Activities
            ? a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralSupply)
            : a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials))

              );

            var result = await _context.Offers.Include(x => x.OfferlocalContentDetails)
               .Where(t => t.TenderId == tenderBasicSearchCriteria.TenderId && t.IsActive == true && t.OfferStatusId == (int)OfferStatus.Received)
               .Where(t => (t.OfferTechnicalEvaluationStatusId.HasValue && t.Tender.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles)
               && (t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking
               || t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending || t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved
               || t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage
               || t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending || t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
               || t.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved)
               ? t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
               : (t.OfferTechnicalEvaluationStatusId == null || t.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer || t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer))
               .SortBy(tenderBasicSearchCriteria.Sort, "ASC")
                       .Select(t => new TenderCheckOfferModel
                       {
                           OfferId = t.OfferId,
                           TechnicalEvaluationLevel = t.TechnicalEvaluationLevel,
                           FinancialEvaluationLevel = t.FinancialEvaluationLevel,
                           InvitationPurchaseDate = t.Tender.Invitations.Count > 0 && t.Tender.ConditionsBooklets.Count == 0 ?
                           t.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).SubmissionDate.Value :
                           t.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).BillInfo.PurchaseDate ?? DateTime.MinValue,
                           InvitationPurchaseDateString = t.Tender.Invitations.Count > 0 && t.Tender.ConditionsBooklets.Count == 0 ?
                           (t.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).SubmissionDate.HasValue ?
                           t.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).SubmissionDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "") :
                           t.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).BillInfo.PurchaseDate.HasValue ?
                           t.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).BillInfo.PurchaseDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                           CombinedCount = t.Combined.Count(),
                           InvitationPurchaseStatus = t.Tender.Invitations.Count > 0 && t.Tender.ConditionsBooklets.Count == 0 ?
                           t.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).InvitationStatus.NameAr :
                           t.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                           InvitationTypeName = t.Tender.Invitations.Any(i => i.CommericalRegisterNo == t.CommericalRegisterNo) ? t.Tender.Invitations.OrderByDescending(i => i.CreatedAt).FirstOrDefault(i => i.CommericalRegisterNo == t.CommericalRegisterNo).InvitationStatus.NameAr : "",
                           CommericalRegisterName = t.Supplier.SelectedCrName,
                           CommericalRegisterNo = t.CommericalRegisterNo,
                           OfferAcceptanceStatusId = t.OfferAcceptanceStatusId,
                           OfferTechnicalEvaluationStatusId = t.OfferTechnicalEvaluationStatusId,
                           OfferPrice = t.FinalPriceAfterDiscount,
                           OfferWeightAfterCalcNPA = t.OfferWeightAfterCalcNPA,
                           TenderAwardingType = t.Tender.TenderAwardingType,
                           CheckingDate = t.Tender.OffersCheckingDate,
                           OfferStatus = t.Status != null ? t.Status.NameAr : "",
                           LocalContentWeight = t.OfferlocalContentDetails.OfferPriceAfterLocalContent,
                           FinancialEvaluation = t.OfferlocalContentDetails.PricePreferancePercentage,
                           PriceAfterCalculateSMEA = t.OfferWeightAfterCalcNPA,
                           FinalPrice = t.OfferFinalPrice,
                           BaiscActivity = t.Tender.TenderActivities.Select(s => s.Activity.NameAr).FirstOrDefault(),
                           IsChecked = t.TotalOfferAwardingValue.HasValue && t.TotalOfferAwardingValue > 0 ||
                           t.PartialOfferAwardingValue.HasValue && t.PartialOfferAwardingValue > 0,
                           ContainSupply = isActivityContainSupply
                       })
                       .ToQueryResult(tenderBasicSearchCriteria.PageNumber, tenderBasicSearchCriteria.PageSize);
            return result;
        }

        public async Task<QueryResult<TenderCheckOfferModel>> GetVROOffersByTenderIdForCheckingGridAsync(TenderBasicSearchCriteria tendersearch)
        {
            var result = await _context.Offers
               .Where(t => t.TenderId == tendersearch.TenderId && t.IsActive == true && t.OfferStatusId == (int)Enums.OfferStatus.Received && (
                           t.Tender.Invitations.Any(x => x.IsActive == true && x.StatusId == (int)Enums.InvitationStatus.Approved && x.CommericalRegisterNo == t.CommericalRegisterNo) ||
                           t.Tender.ConditionsBooklets.Any(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo)))
                           .Where(t => (t.OfferTechnicalEvaluationStatusId.HasValue && t.OfferAcceptanceStatusId.HasValue
                           && (t.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved || t.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening
                           || t.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking || t.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                           || t.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected || t.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved))
                           ? t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && t.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer
                           : (t.OfferTechnicalEvaluationStatusId == null || t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer || t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer))
                           .OrderBy(x => x.FinalPriceAfterDiscount)
                       .Select(t => new TenderCheckOfferModel
                       {
                           OfferId = t.OfferId,
                           TechnicalEvaluationLevel = t.TechnicalEvaluationLevel,
                           FinancialEvaluationLevel = t.FinancialEvaluationLevel,
                           InvitationPurchaseDate = t.Tender.Invitations.Count > 0 && t.Tender.ConditionsBooklets.Count == 0 ?
                           t.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).SubmissionDate.Value :
                           t.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).BillInfo.PurchaseDate.HasValue ?
                           t.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).BillInfo.PurchaseDate.Value
                           : DateTime.MinValue,

                           InvitationPurchaseStatus = t.Tender.Invitations.Count > 0 && t.Tender.ConditionsBooklets.Count == 0 ?
                           t.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved).InvitationStatus.NameAr :
                           t.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,

                           InvitationTypeName = t.Tender.Invitations.Any(i => i.CommericalRegisterNo == t.CommericalRegisterNo) ? " / " + t.Tender.Invitations.FirstOrDefault(i => i.CommericalRegisterNo == t.CommericalRegisterNo).InvitationStatus.NameAr : "",
                           CommericalRegisterName = t.Supplier.SelectedCrName,
                           CommericalRegisterNo = t.CommericalRegisterNo,
                           OfferAcceptanceStatusId = t.OfferAcceptanceStatusId,
                           OfferTechnicalEvaluationStatusId = t.OfferTechnicalEvaluationStatusId,
                           OfferPrice = t.FinalPriceAfterDiscount,
                           TenderAwardingType = t.Tender.TenderAwardingType,
                           CheckingDate = t.Tender.OffersCheckingDate,
                           OfferStatus = t.Status != null ? t.Status.NameAr : "",
                           BaiscActivity = t.Tender.TenderActivities.Select(s => s.Activity.NameAr).FirstOrDefault(),
                           IsChecked = t.TotalOfferAwardingValue.HasValue && t.TotalOfferAwardingValue > 0 ||
                           t.PartialOfferAwardingValue.HasValue && t.PartialOfferAwardingValue > 0
                       })
                       .ToQueryResult(tendersearch.PageNumber, tendersearch.PageSize);
            return result;
        }

        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersByTenderIdForCheckAsync(int tenderId)
        {
            var result = await _context.Offers.Include(x => x.OfferlocalContentDetails)
               .Where(t => t.TenderId == tenderId && t.IsActive == true && t.OfferStatusId == (int)Enums.OfferStatus.Received)
                       .Select(offer => new TenderCheckOfferModel
                       {
                           CommericalRegisterName = offer.Supplier.SelectedCrName,
                           CommericalRegisterNo = offer.CommericalRegisterNo,
                           OfferId = offer.OfferId,
                           OfferPrice = offer.FinalPriceAfterDiscount,
                           InvitationTypeName = (offer.Tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || offer.Tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) ? (offer.Tender.TenderType.NameAr)
                                                     + (offer.Tender.Invitations.Any(i => i.CommericalRegisterNo == offer.CommericalRegisterNo) ? " / " + offer.Tender.Invitations.FirstOrDefault(i => i.CommericalRegisterNo == offer.CommericalRegisterNo).InvitationStatus.NameAr : "") : offer.Tender.TenderType.NameAr,
                           OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
                           OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId,
                           OfferStatusId = offer.OfferStatusId,
                           IsChecked = (offer.TotalOfferAwardingValue.HasValue && offer.TotalOfferAwardingValue > 0) ||
                                                     (offer.PartialOfferAwardingValue.HasValue && offer.PartialOfferAwardingValue > 0),
                           TotalOfferAwardingValue = offer.TotalOfferAwardingValue,
                           PartialOfferAwardingValue = offer.PartialOfferAwardingValue,
                           LocalContentWeight = offer.OfferlocalContentDetails.OfferPriceAfterLocalContent,
                           FinancialEvaluation = offer.OfferlocalContentDetails.PricePreferancePercentage,
                           PriceAfterCalculateSMEA = offer.OfferWeightAfterCalcNPA,
                           FinalPrice = offer.OfferFinalPrice,
                           TenderAwardingType = offer.Tender.TenderAwardingType,
                           InvitationPurchaseDate = offer.Tender.Invitations.FirstOrDefault(i => i.CommericalRegisterNo == offer.CommericalRegisterNo && i.IsActive == true && i.StatusId == (int)Enums.InvitationStatus.Approved).SubmissionDate,
                       })
                       .ToQueryResult();
            return result;
        }

        public async Task<QueryResult<TenderCheckOfferModel>> GetOffersByTenderIdForOpeningAwardingStage(TenderBasicSearchCriteria tenderBasicSearchCriteria)
        {
            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();
            var result = await _context.Offers
                .Include(o => o.Tender)
                .ThenInclude(oo => oo.TenderHistories)
               .Where(o => o.TenderId == tenderBasicSearchCriteria.TenderId && o.IsActive == true)
               .Select(t => new TenderCheckOfferModel
               {
                   OfferId = t.OfferId,
                   OfferStatusId = t.OfferStatusId,
                   TenderTypeId = t.Tender.TenderTypeId,
                   TechnicalEvaluationLevel = t.TechnicalEvaluationLevel,
                   FinancialEvaluationLevel = t.FinancialEvaluationLevel,
                   InvitationPurchaseStatus = t.Tender.Invitations.Any() && t.Tender.ConditionsBooklets.Count == 0 ?
                           t.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo).InvitationStatus.NameAr :
                           t.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == t.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                   CommericalRegisterName = t.Supplier.SelectedCrName,
                   CommericalRegisterNo = t.CommericalRegisterNo,
                   OfferAcceptanceStatusId = t.OfferAcceptanceStatusId,
                   OfferWeightAfterCalcNPA = t.OfferWeightAfterCalcNPA,
                   OfferTechnicalEvaluationStatusId = t.OfferTechnicalEvaluationStatusId,
                   OfferPrice = t.FinalPriceAfterDiscount,
                   isNewAwarding = t.Tender.IsNewAwarding(newAwardingReleaseDate)
               }).ToListAsync();
            var res = await result.OrderBy(r => r.OfferPrice)
            .Where(r => r.isNewAwarding ?
            (r.OfferStatusId == (int)Enums.OfferStatus.Received && r.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
               && r.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
               :
               (r.OfferStatusId == (int)Enums.OfferStatus.Received && r.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer))
                .ToQueryResult(tenderBasicSearchCriteria.PageNumber, tenderBasicSearchCriteria.PageSize);
            return res;
        }

        public async Task<Tender> FindTenderQuantityTablesById(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Tenders
                .Include(h => h.ChangeRequests)
                .ThenInclude(p => p.TenderQuantityTableChanges)
                .ThenInclude(kk => kk.QuantitiyItemsChangeJson)
                .Include(o => o.Offers).Include(s => s.Status)
                .Include(c => c.Attachments)
                .Where(t => t.IsActive == true && t.TenderId == tenderId).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> FindTenderForQuantitiesTableChangeRequests(int tenderId)
        {
            Tender tender = new Tender();
            var quantityTables = await _context.TenderQuantityTables.Include(t => t.Tender).Include(a => a.QuantitiyItemsJson).Where(q => q.IsActive == true && q.TenderId == tenderId && q.QuantitiyItemsJson != null && q.QuantitiyItemsJson.TenderQuantityTableItems != null/*&& q.QuantitiyItemsJson.TenderQuantityTableItems.Any()*/).ToListAsync();
            quantityTables.Where(a => a.QuantitiyItemsJson != null && !a.QuantitiyItemsJson.TenderQuantityTableItems.Any()).ToList().ForEach(a => quantityTables.Remove(a));
            if (quantityTables.Any())
                tender = quantityTables.FirstOrDefault().Tender;
            else
                tender = await _context.Tenders.Where(t => t.IsActive == true && t.TenderId == tenderId).FirstOrDefaultAsync();
            tender.TenderQuantityTables.Clear();
            tender.TenderQuantityTables.AddRange(quantityTables);
            var changeRequest = new TenderChangeRequest();
            var changeReq = await _context.TenderChangeRequests.Include(t => t.Tender)
                .Include(q => q.TenderQuantityTableChanges).ThenInclude(i => i.QuantitiyItemsChangeJson)
                .Where(a => a.TenderId == tenderId && a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).FirstOrDefaultAsync();



            changeReq.TenderQuantityTableChanges.Where(a => !(a.TableChangeStatusId == (int)Enums.QuantityTableChangeStatus.Remove || (a.QuantitiyItemsChangeJson != null && a.QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any(/*q => q.IsActive == true*/)))).ToList()
                                .ForEach(a => changeReq.TenderQuantityTableChanges.Remove(a));
            changeRequest = changeReq;
            tender.ChangeRequests.Add(changeRequest);

            var tenderDetails = await _context.Tenders
                .Where(t => t.IsActive == true && t.TenderId == tenderId)
                .Include(q => q.ConditionsBooklets)
                .ThenInclude(q => q.BillInfo)
                .Include(q => q.Invitations)
                .Include(a => a.Offers)
                .FirstOrDefaultAsync();
            tender.ConditionsBooklets.AddRange(tenderDetails.ConditionsBooklets);
            tender.Offers.AddRange(tenderDetails.Offers);
            tender.Invitations.AddRange(tenderDetails.Invitations);
            return tender;
        }


        public async Task<Tender> GetTenderWithQuantityTablesById(int tenderId)
        {
            var entities = await _context.Tenders.Include(a => a.TenderQuantityTables).ThenInclude(a => a.QuantitiyItemsJson)
                    .Where(t => t.IsActive == true && t.TenderId == tenderId)
                   .FirstOrDefaultAsync();

            return entities;
        }
        public async Task<Tender> GetTenderWithChangeRequest(int tenderId)
        {
            var entities = await _context.Tenders
                .Include(a => a.ChangeRequests)
                .Where(t => t.IsActive == true && t.TenderId == tenderId).AsTracking()
                .FirstOrDefaultAsync();
            return entities;
        }
        public async Task<Tender> GetTenderWithQuantityTable(int tenderId)
        {
            return await _context.Tenders.Include(t => t.TenderQuantityTables)
                .Where(t => t.TenderId == tenderId && (t.TenderQuantityTables.Where(tq => tq.IsActive == true).Any() || t.TenderQuantityTables.Count == 0)).FirstOrDefaultAsync();
        }
        public async Task<Tender> GetTenderWithFormQuantityItemsWithChanges(long tableId, bool isNewTable, int tenderId)
        {
            Tender tender = new Tender();
            if (tableId == 0)
                tender = await GetTenderWithChangeRequest(tenderId);
            else if (!isNewTable)
            {
                var quantityTable = await _context.TenderQuantityTables
                    //.Include(a => a.QuantitiyItemsJson)
                    .Include(a => a.Tender).ThenInclude(a => a.ChangeRequests).Where(s => s.Id == tableId).FirstOrDefaultAsync();
                var items = await _context.TenderQuantityTableItemsView.Where(i => i.QTableId == quantityTable.Id).ToListAsync();

                quantityTable.QuantitiyItemsJson = items.Any() ? new TenderQuantitiyItemsJson()
                {
                    Id = items.FirstOrDefault().QTableItemsId,
                    TenderQuantityTableId = items.FirstOrDefault().QTableId,
                    TenderQuantityTableItems = items.Select(item => new TenderQuantityTableItem()
                    {
                        Id = item.Id,
                        ActivityTemplateId = item.ActivityTemplateId,
                        ColumnId = item.ColumnId,
                        ColumnName = item.ColumnName,
                        ItemNumber = item.ItemNumber.Value,
                        TenderFormHeaderId = item.TenderFormHeaderId,
                        Value = item.Value
                    }).ToList()
                } : new TenderQuantitiyItemsJson()
                {
                    Id = _context.TenderQuantityTables.Include(a => a.QuantitiyItemsJson).FirstOrDefault(s => s.Id == tableId).QuantitiyItemsJson.Id,
                    TenderQuantityTableId = quantityTable.Id
                };

                if (quantityTable != null)
                    tender = quantityTable.Tender;
                var changeRequest = tender.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestStatusId == (int)QuantityTableChangeStatus.Add && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).FirstOrDefault();
                if (changeRequest != null)
                {
                    var quantityTableChanges = await _context.TenderQuantityTableChanges.Where(a => a.TenderChangeRequestId == changeRequest.TenderChangeRequestId && /*a.TenderQuantityTableItemChanges.Any(i => i.IsActive == true) &&*/ a.IsActive == true).ToListAsync();
                    quantityTableChanges.Where(a => a.QuantitiyItemsChangeJson != null && a.QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any()).ToList().ForEach(a => { changeRequest.TenderQuantityTableChanges.Add(a); });
                }
            }
            else
            {
                var quantityTableChange = await _context.TenderQuantityTableChanges.Include(a => a.QuantitiyItemsChangeJson).Include(a => a.ChangeRequest).ThenInclude(a => a.Tender).Where(a => a.IsActive == true && a.Id == tableId).ToListAsync();
                tender = quantityTableChange.FirstOrDefault().ChangeRequest.Tender;
            }
            return tender;
        }

        public async Task<Tender> GetTenderWithFormQuantityTablesAndActivitiesWithoutChanges(int tenderId)
        {
            Tender tender = new Tender();
            var quantityTables = await _context.TenderQuantityTables.Include(t => t.Tender)
                .Where(q => q.IsActive == true && q.TenderId == tenderId).ToListAsync();
            foreach (var table in quantityTables)
            {
                bool hasItems = _context.TenderQuantityTableItemsView.Where(i => i.QTableId == table.Id).Count() > 0;

                if (hasItems)
                {
                    var items = await _context.TenderQuantityTableItemsView.Where(i => i.QTableId == table.Id).ToListAsync();
                    table.QuantitiyItemsJson = new TenderQuantitiyItemsJson()
                    {
                        Id = items.FirstOrDefault().QTableItemsId,
                        TenderQuantityTableId = items.FirstOrDefault().QTableId,
                        TenderQuantityTableItems = items.Select(item => new TenderQuantityTableItem()
                        {
                            Id = item.Id,
                            ActivityTemplateId = item.ActivityTemplateId,
                            ColumnId = item.ColumnId,
                            ColumnName = item.ColumnName,
                            ItemNumber = item.ItemNumber.Value,
                            TenderFormHeaderId = item.TenderFormHeaderId,
                            Value = item.Value
                        }).ToList()
                    };
                }
            }
            quantityTables.Where(a => a.QuantitiyItemsJson == null || !a.QuantitiyItemsJson.TenderQuantityTableItems.Any()).ToList().ForEach(a => quantityTables.Remove(a));
            if (quantityTables.Count == 0)
                tender = await _context.Tenders.Where(a => a.IsActive == true && a.TenderId == tenderId).FirstOrDefaultAsync();
            else
            {
                tender = quantityTables.FirstOrDefault().Tender;
                tender.TenderQuantityTables.Clear();
                tender.TenderQuantityTables.AddRange(quantityTables);
            }
            var activities = await _context.TenderActivities.Include(a => a.Activity).ThenInclude(t => t.ActivityTemplateVersions).Where(a => a.IsActive == true && a.TenderId == tenderId).ToListAsync();
            activities.ForEach(a => tender.TenderActivities.Add(a));
            return tender;
        }

        public async Task<Tender> GetTenderWithFormQuantityTablesWithoutActivitiesAndChanges(int tenderId)
        {
            Tender tender = new Tender();
            var quantityTables = await _context.TenderQuantityTables.Include(t => t.Tender)
                .ThenInclude(x => x.TenderActivities).ThenInclude(o => o.Activity).ThenInclude(m => m.ActivityTemplateVersions)
                .Include(t => t.Tender).ThenInclude(v => v.TenderVersions).ThenInclude(c => c.Version)
                //.Include(a => a.QuantitiyItemsJson)
                .Where(q => q.IsActive == true && q.TenderId == tenderId)
                .ToListAsync();

            foreach (var table in quantityTables)
            {
                var items = await _context.TenderQuantityTableItemsView.Where(t => t.QTableId == table.Id).ToListAsync();

                if (items.Any())
                {
                    table.QuantitiyItemsJson = new TenderQuantitiyItemsJson()
                    {
                        Id = items.FirstOrDefault().QTableItemsId,
                        TenderQuantityTableId = table.Id,
                        TenderQuantityTableItems = items.Select(i => new TenderQuantityTableItem()
                        {
                            Id = i.Id,
                            ActivityTemplateId = i.ActivityTemplateId,
                            ColumnId = i.ColumnId,
                            ColumnName = i.ColumnName,
                            ItemNumber = i.ItemNumber.Value,
                            TenderFormHeaderId = i.TenderFormHeaderId,
                            Value = i.Value
                        }).ToList()
                    };
                }
            }

            quantityTables.Where(a => a.QuantitiyItemsJson == null).ToList().ForEach(a => quantityTables.Remove(a));
            if (quantityTables.Count == 0)
                tender = await _context.Tenders.Where(a => a.IsActive == true && a.TenderId == tenderId).FirstOrDefaultAsync();
            else
            {
                tender = quantityTables.FirstOrDefault().Tender;
                tender.TenderQuantityTables.Clear();
                tender.TenderQuantityTables.AddRange(quantityTables);
            }
            return tender;
        }

        public async Task<Tender> GetTenderWithFormQuantityTablesWithChanges(int tenderId)
        {
            Tender tender = new Tender();
            var quantityTables = await _context.TenderQuantityTables.Include(t => t.Tender).Include(a => a.QuantitiyItemsJson).Where(q => q.IsActive == true && q.TenderId == tenderId && q.QuantitiyItemsJson != null && q.QuantitiyItemsJson.TenderQuantityTableItems != null/*&& q.QuantitiyItemsJson.TenderQuantityTableItems.Any()*/).ToListAsync();
            quantityTables.Where(a => a.QuantitiyItemsJson != null && !a.QuantitiyItemsJson.TenderQuantityTableItems.Any()).ToList().ForEach(a => quantityTables.Remove(a));
            if (quantityTables.Count > 0)
                tender = quantityTables.FirstOrDefault().Tender;
            else
                tender = await _context.Tenders.Where(t => t.IsActive == true && t.TenderId == tenderId).FirstOrDefaultAsync();
            tender.TenderQuantityTables.Clear();
            tender.TenderQuantityTables.AddRange(quantityTables);
            var changeRequest = new TenderChangeRequest();
            var quantityTablesChange = await _context.TenderQuantityTableChanges.Include(a => a.ChangeRequest).Include(a => a.QuantitiyItemsChangeJson).Where(a => a.ChangeRequest.TenderId == tenderId && a.IsActive == true && a.ChangeRequest.IsActive == true).ToListAsync();
            quantityTablesChange.Where(a => !(a.TableChangeStatusId == (int)Enums.QuantityTableChangeStatus.Remove || a.QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any())).ToList()
                                .ForEach(a => quantityTablesChange.Remove(a));
            if (quantityTablesChange.Count > 0)
            {
                changeRequest = quantityTablesChange.FirstOrDefault().ChangeRequest;
                changeRequest.TenderQuantityTableChanges.Clear();
                changeRequest.TenderQuantityTableChanges.AddRange(quantityTablesChange);
            }
            else
                changeRequest = await _context.TenderChangeRequests.Where(a => a.IsActive == true && a.TenderId == tenderId && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)Enums.ChangeStatusType.Approved).FirstOrDefaultAsync();
            if (changeRequest != null && changeRequest.TenderChangeRequestId != 0)
                tender.ChangeRequests.Add(changeRequest);
            var activities = await _context.TenderActivities.Include(a => a.Activity).ThenInclude(t => t.ActivityTemplateVersions).Where(a => a.IsActive == true && a.TenderId == tenderId).ToListAsync();
            activities.ForEach(a => tender.TenderActivities.Add(a));
            return tender;
        }

        public async Task<TenderChangeRequest> GetChangeRequestWithFormQuantityTables(int tenderId)
        {
            var changeRequest = new TenderChangeRequest();
            var quantityTablesChange = await _context.TenderQuantityTableChanges.Include(a => a.ChangeRequest).ThenInclude(a => a.Tender)
                .Include(a => a.QuantitiyItemsChangeJson)
                .Where(a => a.ChangeRequest.TenderId == tenderId && a.IsActive == true && a.ChangeRequest.IsActive == true).ToListAsync();

            quantityTablesChange.Where(a => !(a.TableChangeStatusId == (int)Enums.QuantityTableChangeStatus.Remove || a.QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any())).ToList()
                                .ForEach(a => quantityTablesChange.Remove(a));
            if (quantityTablesChange.Any())
            {
                changeRequest = quantityTablesChange.FirstOrDefault().ChangeRequest;
                changeRequest.TenderQuantityTableChanges.Clear();
                changeRequest.TenderQuantityTableChanges.AddRange(quantityTablesChange);
            }
            else
                changeRequest = await _context.TenderChangeRequests.Include(a => a.Tender).Where(a => a.IsActive == true && a.TenderId == tenderId && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && a.ChangeRequestStatusId != (int)Enums.ChangeStatusType.Approved).FirstOrDefaultAsync();
            return changeRequest;
        }

        public async Task<QuantitiesTemplateModel> GetTenderQuantityItems(int tenderId)
        {
            var tender = await _context.Tenders
                .Include(t => t.TenderActivities).ThenInclude(a => a.Activity).ThenInclude(c => c.ActivityTemplateVersions)
                .Include(a => a.TenderQuantityTables)//.ThenInclude(a => a.QuantitiyItemsJson)
                .Where(t => t.IsActive == true && t.TenderId == tenderId).FirstOrDefaultAsync();
            int? activityVersionId = await GetCurrentTenderActivityVersion(tenderId);

            foreach (var table in tender.TenderQuantityTables)
            {
                var items = await _context.TenderQuantityTableItemsView.Where(t => t.QTableId == table.Id).ToListAsync();
                if (items.Any())
                    table.QuantitiyItemsJson = new TenderQuantitiyItemsJson()
                    {
                        Id = items.FirstOrDefault().QTableItemsId,
                        TenderQuantityTableId = table.Id,
                        TenderQuantityTableItems = items.Select(item => new TenderQuantityTableItem()
                        {
                            Id = item.Id,
                            ActivityTemplateId = item.ActivityTemplateId,
                            ColumnId = item.ColumnId,
                            ColumnName = item.ColumnName,
                            ItemNumber = item.ItemNumber.Value,
                            TenderFormHeaderId = item.TenderFormHeaderId,
                            Value = item.Value
                        }).ToList()
                    };
            }
            var entitiess = new QuantitiesTemplateModel()
            {
                TemplateYears = tender.TemplateYears,
                TenderID = tender.TenderId,
                //ActivityTemplates = x.TenderActivities.Select(a => a.Activity.ActivitytemplateID.Value).Distinct().ToList(),
                TenderCreatedByTypeId = tender.CreatedByTypeId,
                TenderIdString = Util.Encrypt(tender.TenderId),
                PreQualificationId = tender.PreQualificationId,
                InvitationTypeId = tender.InvitationTypeId,
                HasAlternativeOffer = tender.HasAlternativeOffer ?? false,
                TenderName = tender.TenderName,
                TenderStatusId = tender.TenderStatusId,
                ReferenceNumber = tender.ReferenceNumber,
                TenderNumber = tender.TenderNumber,
                TenderTypeId = tender.TenderTypeId,
                OfferPresentationWayId = tender.OfferPresentationWayId ?? 0,
                QuantitiesItems = tender.TenderQuantityTables.Where(a => a.IsActive == true && a.QuantitiyItemsJson != null).SelectMany(b => b.QuantitiyItemsJson.TenderQuantityTableItems).Select(q => new TenderQuantityItemDTO
                {
                    Id = q.Id,
                    TableId = tender.TenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.TenderQuantityTableItems.Any(i => i.Id == q.Id)).Id,
                    TableName = string.IsNullOrEmpty(tender.TenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.TenderQuantityTableItems.Any(i => i.Id == q.Id)).Name) ? "اسم الجدول" :
                    tender.TenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.TenderQuantityTableItems.Any(i => i.Id == q.Id)).Name,
                    ColumnId = q.ColumnId,
                    ColumnName = q.ColumnName,
                    ItemNumber = q.ItemNumber,
                    TenderId = tender.TenderId,
                    TenderFormHeaderId = q.TenderFormHeaderId,
                    //TemplateId = q.ActivityTemplateId,
                    TemplateId = tender.TenderActivities.SelectMany(a => a.Activity.ActivityTemplateVersions.Where(v => v.VersionId == activityVersionId).Select(v => v.TemplateId)).FirstOrDefault(),
                    Value = q.Value
                }).ToList()
            };

            entitiess.ActivityTemplates = await _context.TenderActivities.Where(t => t.IsActive == true && t.TenderId == tenderId)
                                                .SelectMany(a => a.Activity.ActivityTemplateVersions.Where(t => t.VersionId == activityVersionId).Select(t => t.TemplateId.Value))
                                                .Distinct()
                                                .ToListAsync();
            return entitiess;
        }

        public async Task<List<TenderQuantityItemDTO>> GetTenderTableQuantityItemsByTableId(int tableId)
        {
            var q = await _context.TenderQuantityTables.Include(a => a.QuantitiyItemsJson)
                .Where(t => t.IsActive == true && t.Id == tableId)
                .FirstOrDefaultAsync();
            var entitiess = new TenderQuantityTableDTO
            {
                TableId = q.Id,
                TableName = string.IsNullOrEmpty(q.Name) ? "اسم الجدول" : q.Name,
                TenderId = q.TenderId,
                QuantityItems = q.QuantitiyItemsJson != null ? q.QuantitiyItemsJson.TenderQuantityTableItems.Select(i => new TenderQuantityItemDTO
                {
                    Id = i.Id,
                    TableId = q.Id,
                    TableName = string.IsNullOrEmpty(q.Name) ? "اسم الجدول" : q.Name,
                    ColumnId = i.ColumnId,
                    ColumnName = i.ColumnName,
                    ItemNumber = i.ItemNumber,
                    TenderId = q.TenderId,
                    TenderFormHeaderId = i.TenderFormHeaderId,
                    TemplateId = i.ActivityTemplateId,
                    Value = i.Value
                }).ToList() : new List<TenderQuantityItemDTO>()
            };
            return entitiess.QuantityItems;
        }

        public async Task<List<TenderQuantityItemDTO>> GetTenderTableQuantityItemsForNewByTableId(int tableId)
        {
            var t = await _context.TenderQuantityTableChanges.Include(a => a.QuantitiyItemsChangeJson)
                                    .Where(t => t.IsActive == true && t.Id == tableId && t.ChangeRequest.IsActive == true).FirstOrDefaultAsync();
            var entitiess = t.QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Select(q => new TenderQuantityItemDTO()
            {
                Id = q.Id,
                TableId = t.Id,
                TableName = string.IsNullOrEmpty(t.Name) ? "اسم الجدول" : t.Name,
                ColumnId = q.ColumnId,
                ColumnName = q.ColumnName,
                ItemNumber = q.ItemNumber,
                TenderFormHeaderId = q.TenderFormHeaderId,
                TemplateId = q.ActivityTemplateId,
                Value = q.Value
            }).ToList();
            return entitiess;
        }

        public async Task<int> GetTenderTableQuantityItems(Tender tender, long tableId)
        {
            var result = await _context.SP_TendrQuantityTableCellCount.FromSqlRaw($"SP_TenderQuantityTableCellCount {tableId}").ToListAsync();
            return result.Count == 0 ? 0 : int.Parse(result.FirstOrDefault().ItemCount.ToString());
        }

        public async Task<int> GetTenderTableQuantityItemsChanges(Tender tender, long tableId)
        {
            if (!tender.ChangeRequests.Any(a => a.IsActive == true && a.ChangeRequestStatusId != (int)Enums.ChangeStatusType.Approved && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables) ||
                !tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestStatusId != (int)Enums.ChangeStatusType.Approved && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).TenderQuantityTableChanges.Any(a => a.Id == tableId) ||
                !tender.ChangeRequests.FirstOrDefault(a => a.IsActive == true && a.ChangeRequestStatusId != (int)Enums.ChangeStatusType.Approved && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).TenderQuantityTableChanges.FirstOrDefault(a => a.Id == tableId).QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any(t => tender.IsActive == true))
                return 0;

            var result = await _context.SP_TendrQuantityTableCellCount.FromSqlRaw($"SP_TenderQuantityTableChangeCellCount {tableId}").ToListAsync();
            return int.Parse(result.FirstOrDefault().ItemCount.ToString());
        }

        public async Task<QueryResult<TenderQuantityItemDTO>> GetTenderTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var quantityTable = await _context.TenderQuantityTables//.Include(a => a.QuantitiyItemsJson)
                .Where(t => t.TenderId == quantityTableSearchCretriaModel.TenderId && t.Id == quantityTableSearchCretriaModel.QuantityTableId).AsTracking()
                .FirstOrDefaultAsync();


            return await _context.TenderQuantityTableItemsView.Where(t => t.QTableId == quantityTable.Id).Select(i => new TenderQuantityItemDTO
            {
                Id = i.Id,
                TableId = quantityTable.Id,
                TableName = string.IsNullOrEmpty(quantityTable.Name) ? "اسم الجدول" : quantityTable.Name,
                ColumnId = i.ColumnId,
                ColumnName = i.ColumnName,
                ItemNumber = i.ItemNumber.Value,
                TenderId = quantityTable.TenderId,
                TenderFormHeaderId = i.TenderFormHeaderId,
                TemplateId = i.ActivityTemplateId,
                Value = i.Value
            }).OrderBy(a => a.ItemNumber)
            .ToQueryResult(quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * quantityTableSearchCretriaModel.CellsCount);
        }

        public async Task<QueryResult<TenderQuantityItemDTO>> GetTenderTableQuantityItemsChanges(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var t = await _context.TenderQuantityTableChanges.Include(a => a.QuantitiyItemsChangeJson).Include(a => a.ChangeRequest)
                   .Where(t => t.IsActive == true && t.ChangeRequest.TenderId == quantityTableSearchCretriaModel.TenderId && t.Id == quantityTableSearchCretriaModel.QuantityTableId).FirstOrDefaultAsync();
            var result = await t.QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Select(q => new TenderQuantityItemDTO
            {
                Id = q.Id,
                TableId = t.Id,
                TableName = string.IsNullOrEmpty(t.Name) ? "اسم الجدول" : t.Name,
                ColumnId = q.ColumnId,
                ColumnName = q.ColumnName,
                ItemNumber = q.ItemNumber,
                TenderId = t.ChangeRequest.TenderId,
                TenderFormHeaderId = q.TenderFormHeaderId,
                TemplateId = q.ActivityTemplateId,
                Value = q.Value,
                IsNewTable = t.TableChangeStatusId == (int)Enums.TableChangeStatus.Add,
                IsTableDeleted = t.TableChangeStatusId == (int)Enums.TableChangeStatus.Delete
            })
                        .OrderBy(a => a.ItemNumber)
                        .ToQueryResult(quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * quantityTableSearchCretriaModel.CellsCount);
            return result;
        }

        public async Task<Tender> FindTenderByInvitationId(int invitationId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(invitationId), invitationId.ToString());
            Tender tender = await _context.Tenders.Include(o => o.Offers)
                .Include(i => i.Invitations).ThenInclude(s => s.BillInfo).ThenInclude(d => d.BillPaymentDetails)
                .Where(x => x.Invitations.Any(i => i.InvitationId == invitationId && i.IsActive == true)).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<Invitation> FindInvitationByInvitationId(int invitationId)
        {
            Invitation invitation = await _context.Invitations.Where(x => x.InvitationId == invitationId).AsTracking().FirstOrDefaultAsync();
            return invitation;
        }

        public async Task<Tender> FindTenderByAgencyCode(string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(agencyCode), agencyCode.ToString());
            var tenderEntity = await _context.Tenders.Where(t => t.AgencyCode == agencyCode).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<Tender> FindTenderByTenderId(int tenderId)
        {
            var tenderEntity = await _context.Tenders.Where(t => t.TenderId == tenderId).AsTracking().FirstOrDefaultAsync();
            return tenderEntity;
        }
        public async Task<Tender> GetTenderWithDateAndAddressByTenderId(int tenderId)
        {
            var tenderEntity = await _context.Tenders.Include(t => t.TenderDates).Include(t => t.TenderAddress)
                .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<Tender> GetTenderByTenderIdWithInvitations(int tenderId)
        {
            var tenderEntity = await _context.Tenders.Include(x => x.Invitations).Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }


        public async Task<Tender> FindTenderWithInvitationsByTenderId(int tenderId)
        {
            var tenderEntity = await _context.Tenders
                .Include(x => x.Invitations)
                .Include(a => a.TenderAgreementAgencies)
                .Include(m => m.AgencyBudgetNumbers).AsTracking()
                .Include(u => u.UnRegisteredSuppliersInvitation).AsTracking()
                .Where(t => t.IsActive == true && t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<PurchaseModel> FindTenderWithConditionBookletByTenderIdAndCr(int tenderId, string cr, List<SupplierAgencyBlockModel> allSupplierBlock)
        {
            var purchaseModel = await _context.Tenders
                                        .Where(t => t.TenderId == tenderId)
                                        .Select(x => new PurchaseModel
                                        {
                                            IsTenderOwner = x.AgencyCode == cr ? true : false,
                                            IsAvailabletoApplyOffer = IsAvaialableToApplyOffer(
                        (Enums.TenderType)x.TenderTypeId,
                        (Enums.TenderStatus)x.TenderStatusId,
                         x.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true).Any(o => o.CommericalRegisterNo == cr),
                         x.LastOfferPresentationDate.Value,
                        (x.Offers.Where(r => r.OfferStatusId == (int)Enums.OfferStatus.Received && r.IsActive == true && r.Combined.Any(o => (o is OfferSolidarity) && ((OfferSolidarity)o).CRNumber == cr && ((OfferSolidarity)o).StatusId == ((int)Enums.SupplierSolidarityStatus.Approved))).Any()),
                    (x.Invitations.FirstOrDefault(c => c.CommericalRegisterNo == cr && c.IsActive == true && c.StatusId != (int)Enums.InvitationStatus.Rejected && c.StatusId != (int)Enums.InvitationStatus.Withdrawal) != null ? x.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == cr && c.StatusId != (int)Enums.InvitationStatus.Rejected && c.StatusId != (int)Enums.InvitationStatus.Withdrawal).StatusId : 0),
                        x.InvitationTypeId,
                         (x.ConditionsBooklets.Where(b => b.CommericalRegisterNo == cr).FirstOrDefault() != null ?
                   x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == cr).BillInfo != null ?
                   x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == cr).BillInfo.BillStatusId : 0 : 0)),
                                            IsBillExpired = x.ConditionsBooklets.Any() ? x.ConditionsBooklets.Any(c => c.IsActive == true && c.Tender.LastOfferPresentationDate > DateTime.Now && c.CommericalRegisterNo == cr && c.BillInfo.IsActive == true && c.BillInfo.BillStatusId == (int)Enums.BillStatus.SuccessUploaded && c.BillInfo.ExpiryDate < DateTime.Now) : false,

                                            IsAvailabletoBuy = IsAvaialableToPurchase(
                        (x.PreQualificationId != null ? true : false),
                         false,
                          cr == x.AgencyCode,
                    (x.ConditionsBooklets.Where(b => b.CommericalRegisterNo == cr).FirstOrDefault() != null ? x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == cr).BillInfo != null ? x.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == cr).BillInfo.BillStatusId : 0 : 0),
                    x.TenderTypeId,
                        x.TenderStatusId,
                         x.LastOfferPresentationDate,
                       (x.Offers.Where(r => r.OfferStatusId == (int)Enums.OfferStatus.Received && r.IsActive == true && r.Combined.Any(o => (o is OfferSolidarity) && ((OfferSolidarity)o).CRNumber == cr && ((OfferSolidarity)o).StatusId == ((int)Enums.SupplierSolidarityStatus.Approved))).Any() && x.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.CurrentTender && x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects),
                         (x.Invitations.FirstOrDefault(c => c.CommericalRegisterNo == cr && c.IsActive == true && c.StatusId != (int)Enums.InvitationStatus.Rejected && c.StatusId != (int)Enums.InvitationStatus.Withdrawal) != null ? x.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == cr && c.StatusId != (int)Enums.InvitationStatus.Rejected && c.StatusId != (int)Enums.InvitationStatus.Withdrawal).StatusId : 0),
                       x.InvitationTypeId
                        ),
                                            TenderName = x.TenderName,
                                            TenderNumber = x.TenderNumber,
                                            CommericalRegisterNo = cr,
                                            Price = x.ConditionsBookletPrice != null ? x.ConditionsBookletPrice.Value : 0,
                                            LastOfferPresentationDate = x.LastOfferPresentationDate,
                                            ConditionBookletModel = x.ConditionsBooklets.Where(c => c.CommericalRegisterNo == cr && c.IsActive == true && c.BillInfo != null).Select(c => new ConditionBookletModel
                                            {
                                                PaymentStatusId = c.BillInfo.BillStatusId,
                                                CommericalRegisterName = c.Supplier.SelectedCrName,
                                                ConditionsBookletId = c.ConditionsBookletId,
                                                SadadNumber = c.BillInfo.BillInvoiceNumber,
                                                PurchaseDate = c.BillInfo.PurchaseDate,
                                                BillAmount = c.BillInfo.AmountDue,
                                                PurchaseDateString = c.BillInfo.PurchaseDate.HasValue ? c.BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy hh:mm tt") : Resources.TenderResources.DisplayInputs.NotPaid,
                                                BillBookletFees = c.BillInfo.BillPaymentDetails.Where(b => b.FeesTypeId == (int)Enums.TenderFeesType.FinantialCostForInvitation || b.FeesTypeId == (int)Enums.TenderFeesType.FinantialCostForConditionalBooklet).FirstOrDefault() != null ?
                                                c.BillInfo.BillPaymentDetails.Where(b => b.FeesTypeId == (int)Enums.TenderFeesType.FinantialCostForInvitation || b.FeesTypeId == (int)Enums.TenderFeesType.FinantialCostForConditionalBooklet).FirstOrDefault().AmountDue : 0
                                            }
                                            ).FirstOrDefault(),
                                            TenderId = x.TenderId,
                                            HaveToPayConditionalBookletFees = (!x.Invitations.Any(i => i.IsActive == true && i.StatusId == (int)Enums.InvitationStatus.Approved && i.CommericalRegisterNo == cr) && x.TenderTypeId != (int)Enums.TenderType.CurrentTender),
                                            ConditionalBookletFees = x.TenderType.BuyingCost,
                                            TenderIdString = Util.Encrypt(x.TenderId),
                                            CanPurchase = (x.TenderTypeId == (int)Enums.TenderType.NewTender && (x.PreQualificationId == null || x.Invitations.Any(i => i.CommericalRegisterNo == cr && i.IsActive == true && i.StatusId == (int)InvitationStatus.Approved)))
                                                             || ((x.TenderTypeId == (int)Enums.TenderType.LimitedTender || x.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || x.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
                                                             && (x.InvitationTypeId == (int)Enums.InvitationType.Public || x.Invitations.Any(i => i.CommericalRegisterNo == cr && i.IsActive == true && i.StatusId == (int)InvitationStatus.Approved)))
                                                             || (x.TenderTypeId == (int)Enums.TenderType.FirstStageTender || x.TenderTypeId == (int)Enums.TenderType.CurrentTender)
                                                             && /*!allSupplierBlock.Any(b => b.AgencyCode == x.AgencyCode || b.AgencyCode == null) &&*/ x.AgencyCode != cr,
                                            IsPublicCompetition = x.TenderTypeId == (int)Enums.TenderType.Competition && x.InvitationTypeId == (int)Enums.InvitationType.Public
                                        }).FirstOrDefaultAsync();
            purchaseModel.CanSupplierBuyBook = purchaseModel.ConditionBookletModel == null;
            if (!purchaseModel.CanSupplierBuyBook)
            {
                purchaseModel.PaymentStatusId = purchaseModel.ConditionBookletModel.PaymentStatusId;
                purchaseModel.CommericalRegisterName = purchaseModel.ConditionBookletModel.CommericalRegisterName;
                purchaseModel.ConditionsBookletId = purchaseModel.ConditionBookletModel.ConditionsBookletId;
                purchaseModel.SadadNumber = purchaseModel.ConditionBookletModel.SadadNumber;
                purchaseModel.PurshaseDate = purchaseModel.ConditionBookletModel.PurchaseDate;
                purchaseModel.PurshaseDateString = purchaseModel.ConditionBookletModel.PurchaseDateString;
                purchaseModel.BillAmount = purchaseModel.ConditionBookletModel.BillAmount;
                purchaseModel.BillBookletFees = purchaseModel.ConditionBookletModel.BillBookletFees;

            }
            if (purchaseModel.PaymentStatusId == (int)Enums.BillStatus.New || purchaseModel.PaymentStatusId == (int)Enums.BillStatus.UnderProcess)
                purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.WaitingForSaddNumber;
            else if (purchaseModel.PaymentStatusId == (int)Enums.BillStatus.SuccessUploaded)
                purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.WaitingPayament + " رقم السداد : " + purchaseModel.SadadNumber;
            else if (purchaseModel.PaymentStatusId == (int)Enums.BillStatus.Paid)
            {
                if (purchaseModel.SadadNumber != null)
                {
                    purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.WaitingPayament + " رقم السداد : " + purchaseModel.SadadNumber;
                }
                else
                {
                    purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.Purchased;
                }
            }
            else if (purchaseModel.PaymentStatusId == 0)
                purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.SendDetailesByMessage;
            else purchaseModel.PaymentMessage = "";
            return purchaseModel;
        }

        public async Task<PurchaseModel> FindTenderForInvitationBillDetailsByTenderIdAndCr(int tenderId, string cr, List<SupplierAgencyBlockModel> allSupplierBlock)
        {
            var purchaseModel = await _context.Tenders
                                        .Where(t => t.IsActive == true && t.TenderId == tenderId)
                                        .Select(x => new PurchaseModel
                                        {
                                            IsTenderOwner = x.AgencyCode == cr ? true : false,
                                            TenderName = x.TenderName,
                                            TenderNumber = x.TenderNumber,
                                            CommericalRegisterNo = cr,
                                            Price = x.Invitations != null ? x.Invitations.Where(c => c.CommericalRegisterNo == cr && c.IsActive == true && c.BillInfo != null).FirstOrDefault().BillInfo.AmountDue : 0,
                                            LastOfferPresentationDate = x.LastOfferPresentationDate,
                                            IsBillExpired = x.Invitations.Any(i => i.StatusId == (int)Enums.InvitationStatus.WaitingPayment) ? x.Invitations.Any(c => c.IsActive == true && c.Tender.LastOfferPresentationDate > DateTime.Now && c.CommericalRegisterNo == cr && c.BillInfo.IsActive == true && c.BillInfo.BillStatusId == (int)Enums.BillStatus.SuccessUploaded && c.BillInfo.ExpiryDate < DateTime.Now) : false,
                                            ConditionBookletModel = x.Invitations.Where(c => c.CommericalRegisterNo == cr && c.IsActive == true && (c.StatusId == (int)Enums.InvitationStatus.Approved || c.StatusId == (int)Enums.InvitationStatus.WaitingPayment) && c.BillInfo != null).Select(c => new ConditionBookletModel
                                            {
                                                PaymentStatusId = c.BillInfo.BillStatusId,
                                                CommericalRegisterName = c.Supplier.SelectedCrName,
                                                ConditionsBookletId = 0,
                                                SadadNumber = c.BillInfo.BillInvoiceNumber,
                                                PurchaseDate = c.BillInfo.PurchaseDate,
                                                BillAmount = c.BillInfo.AmountDue,
                                                PurchaseDateString = c.BillInfo.PurchaseDate.HasValue ? c.BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy hh:mm tt") : Resources.TenderResources.DisplayInputs.NotPaid,
                                                BillBookletPrice = c.BillInfo.BillPaymentDetails.Where(b => b.FeesTypeId == (int)Enums.TenderFeesType.ConditionalBookletPrice).FirstOrDefault() != null ?
                                                 c.BillInfo.BillPaymentDetails.Where(b => b.FeesTypeId == (int)Enums.TenderFeesType.ConditionalBookletPrice).FirstOrDefault().AmountDue : 0,
                                                BillInvitationFees = c.BillInfo.BillPaymentDetails.Where(b => b.FeesTypeId == (int)Enums.TenderFeesType.FinantialCostForInvitation).FirstOrDefault() != null ?
                                                 c.BillInfo.BillPaymentDetails.Where(b => b.FeesTypeId == (int)Enums.TenderFeesType.FinantialCostForInvitation).FirstOrDefault().AmountDue : 0
                                            }
                                            ).FirstOrDefault(),
                                            TenderId = x.TenderId,
                                            TenderIdString = Util.Encrypt(x.TenderId),
                                        }).FirstOrDefaultAsync();
            purchaseModel.CanSupplierBuyBook = purchaseModel.ConditionBookletModel == null;
            if (!purchaseModel.CanSupplierBuyBook)
            {
                purchaseModel.PaymentStatusId = purchaseModel.ConditionBookletModel.PaymentStatusId;
                purchaseModel.CommericalRegisterName = purchaseModel.ConditionBookletModel.CommericalRegisterName;
                purchaseModel.ConditionsBookletId = purchaseModel.ConditionBookletModel.ConditionsBookletId;
                purchaseModel.SadadNumber = purchaseModel.ConditionBookletModel.SadadNumber;
                purchaseModel.PurshaseDate = purchaseModel.ConditionBookletModel.PurchaseDate;
                purchaseModel.PurshaseDateString = purchaseModel.ConditionBookletModel.PurchaseDateString;
                purchaseModel.BillBookletPrice = purchaseModel.ConditionBookletModel.BillBookletPrice;
                purchaseModel.BillInvitationFees = purchaseModel.ConditionBookletModel.BillInvitationFees;
            }
            if (purchaseModel.PaymentStatusId == (int)Enums.BillStatus.New || purchaseModel.PaymentStatusId == (int)Enums.BillStatus.UnderProcess)
                purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.WaitingForSaddNumber;
            else if (purchaseModel.PaymentStatusId == (int)Enums.BillStatus.SuccessUploaded)
                purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.WaitingPayament + " رقم السداد : " + purchaseModel.SadadNumber;
            else if (purchaseModel.PaymentStatusId == (int)Enums.BillStatus.Paid)
            {
                if (purchaseModel.SadadNumber != null)
                {
                    purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.WaitingPayament + " رقم السداد : " + purchaseModel.SadadNumber;
                }
                else
                {
                    purchaseModel.PaymentMessage = "تم سداد المقابل المالى للدعوة";
                }
            }
            else if (purchaseModel.PaymentStatusId == 0)
                purchaseModel.PaymentMessage = Resources.TenderResources.DisplayInputs.SendDetailesByMessage;
            else purchaseModel.PaymentMessage = "";
            return purchaseModel;
        }

        private static bool IsAvaialableToPurchase(bool HasQualification, bool isBlocked, bool IsTenderOwner, int PaymentStatusId, int tenderType, int enTenderStatus, DateTime? tenderLastOfferPresentationDate, bool IsInvitedToSolidrity, int invitationStatusId, int? invitationType)
        {
            if (!isBlocked && !IsTenderOwner && PaymentStatusId == 0 && enTenderStatus == (int)Enums.TenderStatus.Approved && tenderLastOfferPresentationDate >= DateTime.Now /*&& ! IsInvitedToSolidarity*/)
            {
                switch (tenderType)
                {
                    case (int)Enums.TenderType.CurrentTender:
                        return true;
                    case (int)Enums.TenderType.NewTender:

                        if (HasQualification)
                        {
                            if (invitationStatusId == (int)InvitationStatus.Approved)
                                return true;
                            else
                                return false;
                        }
                        else
                            return true;
                    case (int)Enums.TenderType.LimitedTender:
                        return (invitationStatusId == (int)InvitationStatus.Approved);
                    case (int)Enums.TenderType.ReverseBidding:
                    case (int)Enums.TenderType.FrameworkAgreement:
                    case (int)Enums.TenderType.FirstStageTender:
                        return ((invitationStatusId == (int)InvitationStatus.Approved/* && HasQualification*/) || invitationType == (int)Enums.InvitationType.Public);
                    case (int)Enums.TenderType.Competition:
                        return invitationType == (int)Enums.InvitationType.Public;
                    default:
                        return false;
                }
            }
            else
                return false;
        }

        private static bool IsAvaialableToApplyOffer(Enums.TenderType tenderType, Enums.TenderStatus enTenderStatus, bool SupplierHasOffer, DateTime? tenderLastOfferPresentationDate, bool IsInvitedToSolidrity, int invitationStatus, int? invitationType, int billStatus)
        {

            if (enTenderStatus == Enums.TenderStatus.Approved && !SupplierHasOffer && tenderLastOfferPresentationDate >= DateTime.Now && !IsInvitedToSolidrity)
            {
                switch (tenderType)
                {
                    case Enums.TenderType.CurrentTender:
                    case Enums.TenderType.NewTender:
                    case Enums.TenderType.LimitedTender:
                    case Enums.TenderType.ReverseBidding:
                    case Enums.TenderType.FrameworkAgreement:
                    case Enums.TenderType.FirstStageTender:
                        if (billStatus == (int)Enums.BillStatus.Paid)
                            return true;
                        else return false;
                    case Enums.TenderType.SecondStageTender:
                    case Enums.TenderType.NationalTransformationProjects:
                        if (invitationStatus == (int)Enums.InvitationStatus.Approved)
                            return true;
                        else return false;
                    case Enums.TenderType.CurrentDirectPurchase:
                    case Enums.TenderType.NewDirectPurchase:
                        if (invitationStatus == (int)Enums.InvitationStatus.Approved || billStatus == (int)Enums.BillStatus.Paid)
                            return true;
                        else return false;
                    case Enums.TenderType.Competition:
                        if (invitationType == (int)Enums.InvitationType.Public)
                            return billStatus == (int)Enums.BillStatus.Paid;
                        else if (invitationType == (int)Enums.InvitationType.Specific && invitationStatus == (int)Enums.InvitationStatus.Approved)
                            return true;
                        else return false;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<Tender> FindTenderInvitationsWithById(int tenderId)
        {
            var tenderEntity = await _context.Tenders.Include(y => y.Invitations).Include(x => x.Agency).Include(x => x.UnRegisteredSuppliersInvitation).Include(x => x.Branch).Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<bool> FindUnRegisteredSuppliersInvitationByTenderIDAndEmail(int tenderId, string email, string CrNumber)
        {
            var entities = await _context.UnRegisteredSuppliersInvitation.Where(t => t.TenderId == tenderId && t.Email == email && t.CrNumber == CrNumber).FirstOrDefaultAsync();
            if (entities == null)
                return true;
            else
                return false;
        }

        public async Task<Tender> FindTenderAndAgencyByTenderId(int tenderId)
        {
            var tenderEntity = await _context.Tenders.Include(t => t.Invitations).Include(t => t.TenderType).Include(t => t.Agency).Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<Tender> FindTenderForSendToApproval(int tenderId)
        {
            var tender = await _context.Tenders
                .Include(t => t.Agency).Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<Tender> FindTenderWithAreasById(int tenderId)
        {
            var tenderEntity = await _context.Tenders.Include(x => x.TenderAreas).Include(a => a.TenderCountries).Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<Tender> FindTenderWithConditionsTemplateById(int tenderId, int branchId)
        {
            var tender = await _context.Tenders
               .Include(x => x.TenderConditionsTemplate)
               .Include(t => t.Attachments)
               .Include(t => t.TenderLocalContent)
               .ThenInclude(t => t.LocalContentMechanism)
               .Where(t => t.TenderId == tenderId && t.IsActive == true && t.BranchId == branchId).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<Tender> FindTenderWithConditionsTemplateIntroductionById(int tenderId, int branchId)
        {
            var tenderEntity = await _context.Tenders.Include(x => x.TenderDates)
                .Include(x => x.TenderConditionsTemplate)
                .ThenInclude(t => t.TemplateCertificates)
                .Where(t => t.TenderId == tenderId && t.IsActive == true && t.BranchId == branchId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<List<Template>> FindTenderForLocalContentByTenderId(int tenderId, int branchId)
        {
            var tenderEntity = await _context.Tenders.Include(x => x.TenderActivities).Include(x => x.TenderVersions).ThenInclude(x => x.Version).Where(t => t.TenderId == tenderId && t.IsActive == true && t.BranchId == branchId).FirstOrDefaultAsync();
            List<int> activityIds = tenderEntity.TenderActivities.Select(x => x.ActivityId).ToList();
            int versionId = tenderEntity.TenderVersions.FirstOrDefault(x => x.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity).VersionId;
            var templates = await _context.ActivityVersions.Where(x => x.VersionId == versionId && activityIds.Contains(x.ActivityId)).Select(x => x.Template).ToListAsync();
            return templates;
        }


        public async Task<List<Template>> FindTemplatesby(int tenderId, int activityversionid, int branchId)
        {
            var tenderEntity = await _context.Tenders.Include(x => x.TenderActivities).Where(t => t.TenderId == tenderId && t.IsActive == true).FirstOrDefaultAsync();
            List<int> activityIds = tenderEntity.TenderActivities.Select(x => x.ActivityId).ToList();
            var templates = await _context.ActivityVersions.Where(x => x.VersionId == activityversionid && activityIds.Contains(x.ActivityId)).Select(x => x.Template).ToListAsync();
            return templates;
        }



        public async Task<Tender> FindTenderWithConditionsTemplateTechnicalOutputs(int tenderId, int branchId)
        {
            var tenderEntity = await _context.Tenders
                .Include(x => x.TenderConditionsTemplate)
                .ThenInclude(t => t.TenderConditionsTemplateTechnicalOutputs)
                .Where(t => t.TenderId == tenderId && t.IsActive == true && t.BranchId == branchId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<Tender> FindTenderOfferDetailsByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var tenderEntity = await _context.Tenders
                .Include(t => t.ChangeRequests)
                .Include(t => t.Status)
                .Include(t => t.InvitationType)
                .Include(t => t.ConditionsBookletDeliveryAddress)
                .Include(t => t.OffersOpeningAddress)
                .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<TenderDatesModel> FindTenderDatesByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var tender = await _context.Tenders.Include(t => t.TenderDates).AsTracking()
              .Where(t => t.TenderId == tenderId)
              .Select(t => new TenderDatesModel
              {
                  OffersOpeningAddressId = t.OffersOpeningAddressId,
                  TenderId = t.TenderId,
                  TenderIdString = Util.Encrypt(t.TenderId),
                  TenderStatusId = t.TenderStatusId,
                  TenderTypeId = t.TenderTypeId,
                  AgencyCode = t.AgencyCode,
                  BranchId = t.BranchId,
                  LastEnqueriesDate = t.LastEnqueriesDate,
                  LastOfferPresentationDate = t.LastOfferPresentationDate,
                  OffersOpeningDate = t.OffersOpeningDate,
                  OffersCheckingDate = t.OffersCheckingDate,
                  TenderName = t.TenderName,
                  ReferenceNumber = t.ReferenceNumber,
                  TenderNumber = t.TenderNumber,
                  PreQualificationId = t.PreQualificationId,
                  InvitationTypeId = t.InvitationTypeId,
                  InitialGuaranteePercentage = t.InitialGuaranteePercentage,
                  SupplierNeedSample = t.SupplierNeedSample,
                  SamplesDeliveryAddress = t.SamplesDeliveryAddress,
                  InitialGuaranteeAddress = t.InitialGuaranteeAddress,
                  BuildingName = t.BuildingName,
                  FloarNumber = t.FloarNumber,
                  DepartmentName = t.DepartmentName,
                  DeliveryDate = t.DeliveryDate,
                  AwardingStoppingPeriod = t.AwardingStoppingPeriod,
                  IsSampleAddresSameOffersAddress = (bool)t.TenderAddress.IsSampleAddresSameOffersAddress,
                  OfferDeliveryAddress = t.TenderAddress.OffersDeliveryAddress,
                  OfferBuildingName = t.TenderAddress.OfferBuildingName,
                  OfferFloarNumber = t.TenderAddress.OfferFloarNumber,
                  OfferDepartmentName = t.TenderAddress.OfferDepartmentName,
                  OffersDeliveryDate = t.TenderDates.OffersDeliveryDate,
                  QuantityTableVersionId = t.QuantityTableVersionId,
                  FinalGuaranteePercentage = t.AwardingDiscountPercentage,
                  AwardingExpectedDate = t.TenderDates.AwardingExpectedDate,
                  StartingBusinessOrServicesDate = t.TenderDates.StartingBusinessOrServicesDate,
                  VersionId = t.TenderVersions.Where(x => x.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity).Select(x => x.VersionId).FirstOrDefault()
              }).FirstOrDefaultAsync();

            tender.DeliveryTime = tender.DeliveryDate.HasValue ? tender.DeliveryDate.Value.ToString("HH:mm tt") : "";
            tender.OffersDeliveryTime = tender.OffersDeliveryDate.HasValue ? tender.OffersDeliveryDate.Value.ToString("HH:mm tt") : "";
            tender.OffersOpeningTime = tender.OffersOpeningDate.HasValue ? tender.OffersOpeningDate.Value.ToString("HH:mm tt") : "";
            tender.OffersCheckingTime = tender.OffersCheckingDate.HasValue ? tender.OffersCheckingDate.Value.ToString("HH:mm tt") : "";
            tender.LastOfferPresentationTime = tender.LastOfferPresentationDate.HasValue ? tender.LastOfferPresentationDate.Value.ToString("HH:mm tt") : "";
            if (tender.InitialGuaranteeAddress == null || tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tender.TenderTypeId == (int)Enums.TenderType.Competition || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                tender.NeedInitialGuarantee = false;
            }
            else
            {
                tender.NeedInitialGuarantee = true;
            }

            return tender;
        }



        public async Task<GetConditionTemplateModel> FindTenderConditionTemplateByTenderId(int tenderId, int branchId)
        {
            var tenderLocalContent = await _context.TenderLocalContents.Where(x => x.TenderId == tenderId).Include(m => m.LocalContentMechanism).FirstOrDefaultAsync();
            var result = await _context.Tenders.Include(t => t.TenderConditionsTemplate).AsTracking()
              .Where(t => t.TenderId == tenderId)
              .Select(t => new GetConditionTemplateModel
              {
                  TenderLocalContentId = tenderLocalContent != null ? tenderLocalContent.Id : 0,
                  MinimumBaseline = tenderLocalContent != null ? tenderLocalContent.MinimumBaseline : null,
                  MinimumPercentageLocalContentTarget = tenderLocalContent != null ? tenderLocalContent.MinimumPercentageLocalContentTarget : null,
                  IsApplyProvisionsMandatoryList = tenderLocalContent != null ? tenderLocalContent.IsApplyProvisionsMandatoryList : false,
                  StudyMinimumTargetFileNetReferenceId = tenderLocalContent != null ? tenderLocalContent.StudyMinimumTargetFileNetReferenceId : "",

                  SpecialConditions = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.SpecialConditions : "",
                  Attachments = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.Attachments : "",
                  HideTenderFragmentation = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.HideTenderFragmentation : false,
                  TenderFragmentation = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderFragmentation : "",
                  HideCerificatesIDs = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.HideCerificatesIDs : false,
                  TenderId = t.TenderId,
                  AgencyCode = t.AgencyCode,
                  EncryptedTenderId = Util.Encrypt(t.TenderId),
                  PreQualificationId = t.PreQualificationId,
                  TenderTypeId = t.TenderTypeId,
                  TenderCreatedByTypeId = t.CreatedByTypeId,
                  TenderStatusId = t.TenderStatusId,
                  InvitationTypeId = t.InvitationTypeId,
                  OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                  ReferenceNumber = t.ReferenceNumber,
                  TenderName = t.TenderName,
                  TenderNumber = t.TenderNumber,
                  PurchaseDate = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToString("dd/MM/yyyy") : "",
                  TenderConditionsTemplatePriceNumbers = t.ConditionsBookletPrice,
                  TenderConditionsTemplatePriceText = "",
                  LastSendQuestionsDate = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("dd/MM/yyyy") : "",
                  SendQuestionsDate = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("dd/MM/yyyy") : "",
                  LastApplyOffersDate = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy") : "",
                  LastApplyOffersTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("hh:mm tt") : "",
                  OpenOffersDate = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("dd/MM/yyyy") : "",
                  OpenOffersTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("hh:mm tt") : "",
                  OpenOffersLocation = t.OffersOpeningAddress.AddressName,
                  ConditionTemplateStageStatusId = t.ConditionTemplateStageStatusId,
                  TenderPurpose = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.Description : "",
                  AttachmentReferenceLst = t.Attachments.Where(a => a.AttachmentTypeId == (int)Enums.AttachmentType.TenderOtherFile).Select(at =>
                         "/Upload/GetFile/" + at.FileNetReferenceId + ":" + at.Name).ToList(),
                  AgencyDecalration = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgencyDecalration : "",
                  Description = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.Description : "",
                  AgentName = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentName : "",
                  AgentJob = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentJob : "",
                  AgentFax = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentFax : "",
                  AgentPhone = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentPhone : "",
                  AgentEmail = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentEmail : "",
                  FinancialOfferDocuments = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.FinancialOfferDocuments : "",
                  TechnicalOfferDocuments = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TechnicalOfferDocuments : "",
                  MaxTimeToSendQuestions = t.SubmitionDate.HasValue && t.LastEnqueriesDate.HasValue ? (t.LastEnqueriesDate.Value - t.SubmitionDate.Value).Days.ToString() : "(-)",
                  InitialGuaranteePercentage = t.InitialGuaranteePercentage ?? 0,
                  FinalGuaranteePercentage = t.AwardingDiscountPercentage ?? 0,
                  StoppingPeriod = t.AwardingStoppingPeriod ?? 0,
                  MaxTimeToAnswerQuestions = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.MaxTimeToAnswerQuestions : 0,
                  AlternativeEmailForCommuncation = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AlternativeEmailForCommuncation : "",
                  DocumentStyle = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.DocumentStyle : "",
                  AllowancePeriodToAddOffersIfNotAddedBeofre = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AllowancePeriodToAddOffersIfNotAddedBeofre : 0,
                  AllowedOfferSiningDays = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AllowedOfferSiningDays : 0,
                  DeliveryDate = t.DeliveryDate.HasValue ? t.DeliveryDate.Value.ToHijriDateWithFormat("dd/MM/yyyy hh:mm tt") : "",
                  SupplierNeedSample = t.SupplierNeedSample,
                  SamplesDeliveryAddress = (t.BuildingName != null ? Resources.TenderResources.DisplayInputs.BuildingName + ":" + t.BuildingName + ", " : "")
                        + (t.FloarNumber != null ? Resources.TenderResources.DisplayInputs.FloarNumber + ":" + t.FloarNumber + ", " : "")
                        + (t.DepartmentName != null ? Resources.TenderResources.DisplayInputs.DepartmentName + ":" + t.DepartmentName + ", " : ""),
                  BuildingName = t.BuildingName ?? "",
                  FloarNumber = t.FloarNumber ?? "",
                  DepartmentName = t.DepartmentName ?? "",
                  OfferBuildingName = t.TenderAddress.OfferBuildingName,
                  OfferDepartmentName = t.TenderAddress.OfferDepartmentName,
                  OfferFloarNumber = t.TenderAddress.OfferFloarNumber,
                  OffersDeliveryAddress = t.TenderAddress.OffersDeliveryAddress,
                  OffersDeliveryDate = t.TenderDates.OffersDeliveryDate.HasValue ? t.TenderDates.OffersDeliveryDate.Value.ToHijriDateWithFormat("dd/MM/yyyy hh:mm tt") : "",
                  ConfirimJoiningTheTender = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.ConfirimJoiningTheTender : "",
                  WritePrice = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.WritePrice : Resources.TenderResources.DisplayInputs.WritePriceDefaultValue,
                  HasAlternativeOffer = t.HasAlternativeOffer.HasValue && t.HasAlternativeOffer.Value,
                  ProjectsScope = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.ProjectsScope : "",
                  WorksProgram = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.WorksProgram : "",
                  WorkLocationDetails = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.WorkLocationDetails : "",
                  WorkforceSpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.WorkforceSpecifications : "",
                  MaterialsSpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.MaterialsSpecifications : "",
                  BasicInformation = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.BasicInformation : "" : "",
                  RequiredDcoumentationBefore = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.RequiredDcoumentationBefore : "" : "",
                  Tests = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.Tests : "" : "",
                  IntilizationAndStartWork = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.IntilizationAndStartWork : "" : "",
                  RequiredDcoumentationAfter = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.RequiredDcoumentationAfter : "" : "",
                  Trainging = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.Trainging : "" : "",
                  Guarantee = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.Guarantee : "" : "",
                  Maintanance = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.Maintanance : "" : "",
                  MachineGuarantee = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.MachineGuarantee : "" : "",
                  MachineMaintanance = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration != null ? t.TenderConditionsTemplate.TenderConditionsTemplateMaterialInofmration.MachineMaintanance : "" : "",
                  EquipmentsSpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.EquipmentsSpecifications : "",
                  ContractBasedOnPerformanceDetails = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.ContractBasedOnPerformanceDetails : "",
                  ServicesAndWorkImplementationsMethod = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.ServicesAndWorkImplementationsMethod : "",
                  QualitySpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.QualitySpecifications : "",
                  SafetySpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.SafetySpecifications : "",
                  ShowGeneralOnly = true,
                  ListOfSections = new List<int>(),
                  TemplateIds = new List<int?>(),
                  TenderConditionsTemplateId = t.TenderConditionsTemplateId,
                  AwardingExpectedDate = t.TenderDates.AwardingExpectedDate.HasValue ? t.TenderDates.AwardingExpectedDate.Value.ToString("dd/MM/yyyy") : "",
                  StartWorkingDate = t.TenderDates.StartingBusinessOrServicesDate.HasValue ? t.TenderDates.StartingBusinessOrServicesDate.Value.ToString("dd/MM/yyyy") : "",
                  ParticipationConfirmationLetterDateString = t.TenderDates.ParticipationConfirmationLetterDate.HasValue ? t.TenderDates.ParticipationConfirmationLetterDate.Value.ToString("dd/MM/yyyy") : "",
                  IsTenderContainsTawreedTables = t.IsTenderContainsTawreedTables ?? false,
                  EstimatedValue = t.EstimatedValue,
                  OffersEvaluationCriteria = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.OffersEvaluationCriteria : "",
              }).FirstOrDefaultAsync();
            var tenderActivities = await _context.TenderActivities.Where(x => x.IsActive == true && x.TenderId == tenderId).Include(a => a.Activity).ThenInclude(a => a.ActivityTemplateVersions).ThenInclude(a => a.Template).ThenInclude(a => a.ConditionTemplateActivities).ToListAsync();

            var activityTenderVersion = await _context.TenderVersions.FirstOrDefaultAsync(x => x.TenderId == tenderId && x.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity);
            result.VersionId = activityTenderVersion.VersionId;

            result.ShowGeneralOnly = tenderActivities.Where(a => a.IsActive == true).Select(a => a.Activity.ActivityTemplateVersions.Where(v => v.VersionId == activityTenderVersion.VersionId).Select(t => t.TemplateId)).Distinct().Count() > 1;
            result.ListOfSections = tenderActivities.Where(a => a.IsActive == true).SelectMany(a => a.Activity.ActivityTemplateVersions.SelectMany(r => r.Template.ConditionTemplateActivities)).Select(a => a.ConditionsTemplateSectionId).Distinct().ToList();
            result.TemplateIds = tenderActivities.Where(x => x.IsActive == true).SelectMany(a => a.Activity.ActivityTemplateVersions.Where(t => t.VersionId == result.VersionId).Select(t => t.TemplateId)).Distinct().ToList();
            result.TemplateName = result.TemplateIds.Count() > 1 ? "نموذج عام" : tenderActivities.Where(x => x.IsActive == true).SelectMany(a => a.Activity.ActivityTemplateVersions.Where(t => t.VersionId == result.VersionId).Select(t => t.Template.NameAr)).FirstOrDefault();
            result.IsNotTawreed = (result.IsTenderContainsTawreedTables.HasValue && !result.IsTenderContainsTawreedTables.Value && (!result.TemplateIds.Any(x => x == (int)Enums.ActivityTemplate.Tawreed) || !result.TemplateIds.Any(x => x == (int)Enums.ActivityTemplate.TawreedActivityVersion4)) && result.EstimatedValue > 50000000);
            result.showLocalContentTab = result.VersionId < (int)Enums.ActivityVersions.Sprint7Activities;
            result.IsTawreedActivity = ((result.IsTenderContainsTawreedTables.HasValue && result.IsTenderContainsTawreedTables.Value) || result.TemplateIds.Any(x => x == (int)Enums.ActivityTemplate.Tawreed) || result.TemplateIds.Any(x => x == (int)Enums.ActivityTemplate.TawreedActivityVersion4));

            result.TablesCount = await _context.TenderQuantityTables.CountAsync(x => x.TenderId == tenderId);
            var tenderTemplate = await _context.TenderConditionsTemplates.Include(c => c.TenderConditionsTemplateTechnicalOutputs)
               .Include(c => c.TenderConditionsTemplateTechnicalDelrations)
               .Include(c => c.TemplateCertificates).ThenInclude(cer => cer.Certificate).FirstOrDefaultAsync(x => x.TenderConditionsTemplateId == result.TenderConditionsTemplateId);
            result.TenderConditionsTemplateOutputs = tenderTemplate != null ? tenderTemplate.TenderConditionsTemplateTechnicalOutputs
                .Select(a => new TenderConditionsTemplateOutputsViewModel
                {
                    TenderConditionsTemplateTechnicalOutputId = a.TenderConditionsTemplateTechnicalOutputId,
                    OutputStage = a.OutputStage,
                    OutputName = a.OutputName,
                    OutputDescriptions = a.OutputDescriptions,
                    OutputDeliveryTime = a.OutputDeliveryTime
                }).ToList() : new List<TenderConditionsTemplateOutputsViewModel>();
            result.TechnicalDeclrations = tenderTemplate != null ? tenderTemplate.TenderConditionsTemplateTechnicalDelrations.Where(a => a.IsActive == true)
                 .Select(a => new TenderConditionsTemplateTechnicalDeclrationViewModel
                 {
                     TenderConditionsTemplateTechnicalDeclrationId = a.TenderConditionsTemplateTechnicalDeclrationId,
                     Term = a.Term,
                     Decleration = a.Decleration
                 }).ToList() : new List<TenderConditionsTemplateTechnicalDeclrationViewModel>();
            result.CerificatesIDs = tenderTemplate != null ? tenderTemplate.TemplateCertificates.Where(a => a.IsActive == true).Select(f => f.CerificateId).ToList() : new List<int>();
            result.LocalContentMechanismIDs = tenderLocalContent != null ? tenderLocalContent.LocalContentMechanism.Where(a => a.IsActive == true).Select(f => f.LocalContentMechanismPreferenceId).ToList() : new List<int>();
            result.Certificates = tenderTemplate != null ? tenderTemplate.TemplateCertificates.Where(a => a.IsActive == true).Select(f => new LookupModel { Id = f.CerificateId, Name = f.Certificate.NameAr }).ToList() : new List<LookupModel>();

            if (result.TemplateIds.Any())
            {
                result.ShowGeneralTemplates = ConditionsForGeneralTemplates(result.TemplateIds);
                result.ShowServicesTemplates = ConditionsForServicesTemplates(result.TemplateIds);
                result.ShowSectionNineAndTen = ShowSections(result.TemplateIds);
                result.IsShowLocalContentConditionSection = ShowLocalContentConditionSection(result.TemplateIds, result.EstimatedValue, result.LocalContentMechanismIDs);
            }

            return result;
        }


        private static bool ConditionsForGeneralTemplates(List<int?> templateIds)
        {

            return templateIds.Contains((int)Enums.ConditionsTemplateCategory.Ration) ||
                           templateIds.Contains((int)Enums.ConditionsTemplateCategory.MaintenanceAndOperation) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.RoadConstruction) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.BuildingConstruction) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.OperationAndMaintenanceMethods) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.Pharmaceutical) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.MedicalSupplies) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.GeneralSupply) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.InformationTechnology) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.GeneralModel) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.CleanlinessOfCities);
        }

        private static bool ConditionsForServicesTemplates(List<int?> templateIds)
        {

            return templateIds.Contains((int)Enums.ConditionsTemplateCategory.EngineeringServicesSupervision) ||
                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.EngineeringServicesDesign) ||
                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.ConsultingServices);
        }


        private bool ShowSections(List<int?> templateIds)
        {

            return templateIds.Contains((int)Enums.ConditionsTemplateCategory.Ration) ||
                           templateIds.Contains((int)Enums.ConditionsTemplateCategory.MaintenanceAndOperation) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.RoadConstruction) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.BuildingConstruction) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.OperationAndMaintenanceMethods) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.InformationTechnology) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.CleanlinessOfCities) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.EngineeringServicesSupervision) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.EngineeringServicesDesign) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.ConsultingServices) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.Pharmaceutical) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.GeneralSupply) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.GeneralModel) ||
                                     templateIds.Contains((int)Enums.ConditionsTemplateCategory.MedicalSupplies);
        }

        private bool ShowLocalContentConditionSection(List<int?> templateIds, decimal? EstimatedValue, List<int> localContentMechanismIDs)
        {
            return EstimatedValue >= 50000000
                || (localContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.LocalContentConditionsWeight) || localContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.MinimumBaseline)
                && (templateIds.Contains((int)Enums.ConditionsTemplateCategory.Ration) ||
                         templateIds.Contains((int)Enums.ConditionsTemplateCategory.MaintenanceAndOperation) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.RoadConstruction) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.BuildingConstruction) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.OperationAndMaintenanceMethods) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.InformationTechnology) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.GeneralModel) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.CleanlinessOfCities) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.EngineeringServicesSupervision) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.EngineeringServicesDesign) ||
                                   templateIds.Contains((int)Enums.ConditionsTemplateCategory.ConsultingServices)));

        }


        public async Task<GetConditionTemplateModel> FindConditionTemplate(int tenderId, int branchId)
        {
            var tenderLocalContent = await _context.TenderLocalContents.Where(x => x.TenderId == tenderId).Include(m => m.LocalContentMechanism).FirstOrDefaultAsync();

            var result = await _context.Tenders
              .Where(t => t.TenderId == tenderId)
              .Select(t => new GetConditionTemplateModel
              {
                  TenderLocalContentId = tenderLocalContent != null ? tenderLocalContent.Id : 0,
                  MinimumBaseline = tenderLocalContent != null ? tenderLocalContent.MinimumBaseline : null,
                  MinimumPercentageLocalContentTarget = tenderLocalContent != null ? tenderLocalContent.MinimumPercentageLocalContentTarget : null,
                  IsApplyProvisionsMandatoryList = tenderLocalContent != null ? tenderLocalContent.IsApplyProvisionsMandatoryList : true,
                  StudyMinimumTargetFileNetReferenceId = tenderLocalContent != null ? tenderLocalContent.StudyMinimumTargetFileNetReferenceId : "",
                  ParticipationConfirmationLetterDate = t.TenderDates.ParticipationConfirmationLetterDate,
                  OffersEvaluationCriteria = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.OffersEvaluationCriteria : "",
                  OffersChecking = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.OffersChecking : "",
                  BranchId = t.BranchId,
                  HideTenderFragmentation = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.HideTenderFragmentation : false,
                  TenderFragmentation = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TenderFragmentation : "",
                  HideCerificatesIDs = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.HideCerificatesIDs : false,
                  Description = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.Description : "",
                  AgentName = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentName : "",
                  AgentJob = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentJob : "",
                  AgentFax = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentFax : "",
                  AgentPhone = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentPhone : "",
                  AgentEmail = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgentEmail : "",
                  FinancialOfferDocuments = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.FinancialOfferDocuments : "",
                  TechnicalOfferDocuments = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.TechnicalOfferDocuments : "",
                  MaxTimeToAnswerQuestions = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.MaxTimeToAnswerQuestions : 0,
                  AlternativeEmailForCommuncation = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AlternativeEmailForCommuncation : "",
                  DocumentStyle = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.DocumentStyle : "",
                  ConfirimJoiningTheTender = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.ConfirimJoiningTheTender : "",
                  AllowancePeriodToAddOffersIfNotAddedBeofre = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AllowancePeriodToAddOffersIfNotAddedBeofre : 0,
                  AllowedOfferSiningDays = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AllowedOfferSiningDays : 0,
                  WritePrice = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.WritePrice : Resources.TenderResources.DisplayInputs.WritePriceDefaultValue,
                  SpecialConditions = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.SpecialConditions : "",
                  Attachments = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.Attachments : "",
                  WorkforceSpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.WorkforceSpecifications : "",
                  MaterialsSpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.MaterialsSpecifications : "",
                  EquipmentsSpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.EquipmentsSpecifications : "",
                  ServicesAndWorkImplementationsMethod = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.ServicesAndWorkImplementationsMethod : "",
                  QualitySpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.QualitySpecifications : "",
                  SafetySpecifications = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.SafetySpecifications : "",
                  AgencyDecalration = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.AgencyDecalration : "",
                  ProjectsScope = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.ProjectsScope : "",
                  WorksProgram = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.WorksProgram : "",
                  WorkLocationDetails = t.TenderConditionsTemplate != null ? t.TenderConditionsTemplate.WorkLocationDetails : "",
                  EncryptedTenderId = Util.Encrypt(t.TenderId),
                  PreQualificationId = t.PreQualificationId,
                  TenderTypeId = t.TenderTypeId,
                  TenderCreatedByTypeId = t.CreatedByTypeId,
                  TenderStatusId = t.TenderStatusId,
                  InvitationTypeId = t.InvitationTypeId,
                  OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                  ConditionTemplateStageStatusId = t.ConditionTemplateStageStatusId,
                  ReferenceNumber = t.ReferenceNumber,
                  TenderName = t.TenderName,
                  TenderNumber = t.TenderNumber,
                  QuantityTableVersionId = t.QuantityTableVersionId,
                  ShowGeneralOnly = true,
                  ListOfSections = new List<int>(),
                  TemplateIds = new List<int?>(),
                  TenderConditionsTemplateId = t.TenderConditionsTemplateId,
                  IsTenderContainsTawreedTables = t.IsTenderContainsTawreedTables ?? false,
                  EstimatedValue = t.EstimatedValue,
                  AttachmentReferenceLst = t.Attachments.Where(a => a.AttachmentTypeId == (int)Enums.AttachmentType.TenderOtherFile).Select(at => "/Upload/GetFile/" + at.FileNetReferenceId + ":" + at.Name).ToList(),
                  LocalContentAttachmentReferenceLst = t.Attachments.Where(a => a.AttachmentTypeId == (int)Enums.AttachmentType.LocalContent).Select(at => "/Upload/GetFile/" + at.FileNetReferenceId + ":" + at.Name).ToList(),

              }).FirstOrDefaultAsync();
            var tenderActivities = await _context.TenderActivities.Where(x => x.IsActive == true && x.TenderId == tenderId).Include(a => a.Activity).ThenInclude(a => a.ActivityTemplateVersions).ThenInclude(r => r.Template).ThenInclude(a => a.ConditionTemplateActivities).ToListAsync();
            var activityTenderVersion = await _context.TenderVersions.FirstOrDefaultAsync(x => x.TenderId == tenderId && x.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity);
            result.VersionId = activityTenderVersion.VersionId;

            result.ShowGeneralOnly = tenderActivities.Where(a => a.IsActive == true).Select(a => a.Activity.ActivityTemplateVersions.Where(v => v.VersionId == activityTenderVersion.VersionId).Select(t => t.TemplateId)).Distinct().Count() > 1;
            result.ListOfSections = tenderActivities.Where(a => a.IsActive == true).SelectMany(a => a.Activity.ActivityTemplateVersions.SelectMany(r => r.Template.ConditionTemplateActivities)).Select(a => a.ConditionsTemplateSectionId).Distinct().ToList();
            result.TemplateIds = tenderActivities.Where(x => x.IsActive == true).SelectMany(a => a.Activity.ActivityTemplateVersions.Where(t => t.VersionId == result.VersionId).Select(t => t.TemplateId)).Distinct().ToList();
            //result.TemplateIds = tenderActivities.Where(x => x.IsActive == true).SelectMany(a => a.Activity.ActivityTemplateVersions.Select(t => t.TemplateId)).Distinct().ToList();
            var tenderTemplate = await _context.TenderConditionsTemplates.Include(c => c.TenderConditionsTemplateTechnicalOutputs)
                .Include(c => c.TenderConditionsTemplateTechnicalDelrations)
                .Include(c => c.TemplateCertificates).FirstOrDefaultAsync(x => x.TenderConditionsTemplateId == result.TenderConditionsTemplateId);

            result.IsNotTawreed = (result.IsTenderContainsTawreedTables.HasValue && !result.IsTenderContainsTawreedTables.Value && (!result.TemplateIds.Any(x => x == (int)Enums.ActivityTemplate.Tawreed) || !result.TemplateIds.Any(x => x == (int)Enums.ActivityTemplate.TawreedActivityVersion4)) && result.EstimatedValue > 50000000);
            result.IsTawreedActivity = ((result.IsTenderContainsTawreedTables.HasValue && result.IsTenderContainsTawreedTables.Value) || result.TemplateIds.Any(x => x == (int)Enums.ActivityTemplate.Tawreed) || result.TemplateIds.Any(x => x == (int)Enums.ActivityTemplate.TawreedActivityVersion4));

            result.showLocalContentTab = result.VersionId < (int)Enums.ActivityVersions.Sprint7Activities;

            result.TenderConditionsTemplateOutputs = tenderTemplate != null ? tenderTemplate.TenderConditionsTemplateTechnicalOutputs
                 .Select(a => new TenderConditionsTemplateOutputsViewModel
                 {
                     TenderConditionsTemplateTechnicalOutputId = a.TenderConditionsTemplateTechnicalOutputId,
                     OutputStage = a.OutputStage,
                     OutputName = a.OutputName,
                     OutputDescriptions = a.OutputDescriptions,
                     OutputDeliveryTime = a.OutputDeliveryTime
                 }).ToList() : new List<TenderConditionsTemplateOutputsViewModel>();
            result.CerificatesIDs = tenderTemplate != null ? tenderTemplate.TemplateCertificates.Where(a => a.IsActive == true).Select(f => f.CerificateId).ToList() : new List<int>();
            result.LocalContentMechanismIDs = tenderLocalContent != null ? tenderLocalContent.LocalContentMechanism.Where(a => a.IsActive == true).Select(f => f.LocalContentMechanismPreferenceId).ToList() : new List<int>();
            result.TechnicalDeclrations = tenderTemplate != null ? tenderTemplate.TenderConditionsTemplateTechnicalDelrations.Where(a => a.IsActive == true)
                 .Select(a => new TenderConditionsTemplateTechnicalDeclrationViewModel
                 {
                     TenderConditionsTemplateTechnicalDeclrationId = a.TenderConditionsTemplateTechnicalDeclrationId,
                     Term = a.Term,
                     Decleration = a.Decleration
                 }).ToList() : new List<TenderConditionsTemplateTechnicalDeclrationViewModel>();
            return result;
        }

        public async Task<ConditionsBookletTemplateModel> GetTenderConditionsBookletTemplateDetails(int tenderId)
        {
            var entities = await _context.Tenders.AsTracking()
              .Where(t => t.TenderId == tenderId)
              .Select(t => new ConditionsBookletTemplateModel
              {
                  TenderBranchId = t.BranchId,
                  TenderName = t.TenderName,
                  MinistryName = "",
                  AgencyName = t.Agency.NameArabic,
                  AgencyLogo = "",
                  TednerNumber = t.ReferenceNumber,
                  SubmitionDate = t.SubmitionDate,
                  BookletPrice = t.ConditionsBookletPrice,
                  BookletPriceString = "",
                  EnqueriesDate = t.LastEnqueriesDate,
                  LastEnqueriesDate = t.LastEnqueriesDate,
                  LastOfferPresentationDate = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy") : "",
                  LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("HH:mm") : "",
                  OffersOpeningDate = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("dd/MM/yyyy") : "",
                  OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("HH:mm") : "",
                  OffersOpeningAddress = t.OffersOpeningAddress.AddressName,
                  AgreementDuration = (t.AgreementDays.HasValue ? t.AgreementDays.Value.ToString() : "0") + (t.AgreementMonthes.HasValue ? t.AgreementMonthes.Value.ToString() : "0") + "/" + (t.AgreementYears.HasValue ? t.AgreementYears.Value.ToString() : "0"),
                  Beneficiaries = "",
                  Description = t.TenderConditionsTemplate == null ? "" : t.TenderConditionsTemplate.Description,
                  AgentEmail = t.TenderConditionsTemplate == null ? "" : t.TenderConditionsTemplate.AgentEmail,
                  AgentFax = t.TenderConditionsTemplate == null ? "" : t.TenderConditionsTemplate.AgentFax,
                  AgentJob = t.TenderConditionsTemplate == null ? "" : t.TenderConditionsTemplate.AgentJob,
                  AgentName = t.TenderConditionsTemplate == null ? "" : t.TenderConditionsTemplate.AgentName,
                  AgentPhone = t.TenderConditionsTemplate == null ? "" : t.TenderConditionsTemplate.AgentPhone,
                  CerificatesIDs = t.TenderConditionsTemplate == null ? null : t.TenderConditionsTemplate.TemplateCertificates.Select(a => a.CerificateId).ToList(),
                  AgencyCode = t.AgencyCode
              }).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<TenderDatesModel> FindTenderDatesDetailsByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var model = await _context.Tenders
                .Include(t => t.ChangeRequests)
             .Where(t => t.TenderId == tenderId)
             .Select(t => new TenderDatesModel
             {
                 TenderName = t.TenderName,
                 TenderNumber = t.TenderNumber,
                 ReferenceNumber = t.ReferenceNumber,
                 TenderId = t.TenderId,
                 TenderStatusId = t.TenderStatusId,
                 TenderTypeId = t.TenderTypeId,
                 AgencyCode = t.AgencyCode,
                 VROOfficeCode = t.Agency.VROOfficeCode,
                 TenderCreatedByTypeId = t.CreatedByTypeId,
                 VRORelatedBranchAgencyCode = t.VRORelatedBranch.AgencyCode,
                 TenderIdString = Util.Encrypt(t.TenderId),
                 LastEnqueriesDate = t.LastEnqueriesDate,
                 //LastEnqueriesDateString = (t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                 //LastEnqueriesDateHijriString = (t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),

                 LastOfferPresentationDate = t.LastOfferPresentationDate,
                 //LastOfferPresentationDateString = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                 //LastOfferPresentationDateHijriString = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                 LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",

                 OffersOpeningDate = t.OffersOpeningDate,
                 //OffersOpeningDateString = (t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                 //OffersOpeningDateHijriString = (t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                 OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",

                 OffersCheckingDate = t.OffersCheckingDate,
                 //OffersCheckingDateString = (t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                 //OffersCheckingDateHijriString = (t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                 OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("hh:mm tt") : "",

                 ChangeRequestTypeId = t.ChangeRequests.Count(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates) > 0 ? t.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).FirstOrDefault().ChangeRequestTypeId : -1,
                 ChangeRequestStatusId = t.ChangeRequests.Count(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates) > 0 ? t.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).FirstOrDefault().ChangeRequestStatusId : -1,
                 TenderChangeRequestId = t.ChangeRequests.Count(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates) > 0 ? t.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).FirstOrDefault().TenderChangeRequestId : 0,

                 AwardingExpectedDate = t.TenderDates.AwardingExpectedDate,
                 StartingBusinessOrServicesDate = t.TenderDates.StartingBusinessOrServicesDate,
                 ParticipationConfirmationLetterDate = t.TenderDates.ParticipationConfirmationLetterDate,

                 SupplierNeedSample = t.SupplierNeedSample,
                 //DeliveryDate = t.DeliveryDate.HasValue ? t.DeliveryDate.Value.ToHijriDateWithFormat("dd/MM/yyyy hh:mm tt") : "",
                 DeliveryDate = t.DeliveryDate,
                 SamplesDeliveryAddress = Resources.TenderResources.DisplayInputs.SamplesDeliveryAddress + ":" + t.SamplesDeliveryAddress == null ? "" : t.SamplesDeliveryAddress
                + (t.BuildingName != null ? ", " + Resources.TenderResources.DisplayInputs.BuildingName + ":" + t.BuildingName : "")
                + (t.FloarNumber != null ? ", " + Resources.TenderResources.DisplayInputs.FloarNumber + ":" + t.FloarNumber : "")
                + (t.DepartmentName != null ? ", " + Resources.TenderResources.DisplayInputs.DepartmentName + ":" + t.DepartmentName : ""),

                 OffersOpeningAddress = t.OffersOpeningAddress.AddressName,
                 AwardingStoppingPeriod = t.AwardingStoppingPeriod,
                 IsSampleAddresSameOffersAddress = (bool)t.TenderAddress.IsSampleAddresSameOffersAddress,
                 OffersDeliveryDate = t.TenderDates.OffersDeliveryDate,
                 OfferDeliveryAddress = t.TenderAddress.OffersDeliveryAddress,
                 OfferBuildingName = t.TenderAddress.OfferBuildingName,
                 OfferFloarNumber = t.TenderAddress.OfferFloarNumber,
                 OfferDepartmentName = t.TenderAddress.OfferDepartmentName,
                 MaxTimeToAnswerQuestions = t.TenderConditionsTemplate.MaxTimeToAnswerQuestions

             }).FirstOrDefaultAsync();

            if (model.IsSampleAddresSameOffersAddress)
            {
                model.OfferDeliveryAddress = model.SamplesDeliveryAddress;
            }
            else
            {
                model.OfferDeliveryAddress = Resources.TenderResources.DisplayInputs.SamplesDeliveryAddress + ":" + model.OfferDeliveryAddress == null ? "" : model.OfferDeliveryAddress
                + (model.OfferBuildingName != null ? ", " + Resources.TenderResources.DisplayInputs.BuildingName + ":" + model.OfferBuildingName : "")
                + (model.OfferFloarNumber != null ? ", " + Resources.TenderResources.DisplayInputs.FloarNumber + ":" + model.OfferFloarNumber : "")
                + (model.OfferDepartmentName != null ? ", " + Resources.TenderResources.DisplayInputs.DepartmentName + ":" + model.OfferDepartmentName : "");
            }
            model.VersionId = await GetCurrentTenderActivityVersion(tenderId);

            if (model.VersionId >= (int)Enums.ActivityVersions.Sprint7Activities)
            {
                model.StartingSendingEnquiries = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.Approved).OrderByDescending(h => h.TenderHistoryId).Select(x => x.CreatedAt).FirstOrDefaultAsync();
            }
            return model;
        }

        #region Tender Details

        public async Task<TenderDetailsModel> GetMainTenderDetailsForSuppliers(int tenderId, string cr, List<SupplierAgencyBlockModel> _allBlockedSuppliers)
        {
            var allBlockedSuppliers = _allBlockedSuppliers.AsQueryable<SupplierAgencyBlockModel>();

            var tender = await _context.Tenders
            .Where(t => t.TenderId == tenderId)
            .Select(t => new TenderDetailsModel
            {
                GovAgencies = new List<GovAgencyModel>(),
                TenderStatusName = t.TenderStatusId == (int)Enums.TenderStatus.Established ? Resources.TenderResources.Messages.UnderEstablishing : t.Status.NameAr,
                LastOfferPresentationDate = t.LastOfferPresentationDate,
                LastEnqueriesDate = t.LastEnqueriesDate,
                TenderTypeId = t.TenderTypeId,
                Purpose = t.Purpose,
                ConditionsBookletPrice = t.ConditionsBookletPrice == null ? 0 : t.ConditionsBookletPrice,
                SupplierNeedSample = t.SupplierNeedSample,
                AgencyName = t.Agency != null ? t.Agency.NameArabic : "",
                TenderName = t.TenderName,
                TenderNumber = t.TenderNumber,
                TenderIdString = Util.Encrypt(t.TenderId),
                EstimatedValue = t.EstimatedValue,
                IsTenderOwner = t.AgencyCode == cr ? true : false,
                TenderStatusId = t.TenderStatusId,
                InvitationTypeName = t.InvitationType != null ? t.InvitationType.NameAr : "",
                TenderStatusIdString = Util.Encrypt(t.TenderStatusId),
                ReasonForLimitedTenderTypeName = t.ReasonForLimitedTenderType.Name,
                ReferenceNumber = t.ReferenceNumber,
                ReasonForPurchaseTenderTypeName = t.ReasonForPurchaseTenderType.Name,
                InvitationTypeId = t.InvitationTypeId,
                AgreementTypeName = t.AgreementTypeId.HasValue ? t.AgreementType.NameAr : "",
                AgreementMonthes = t.AgreementMonthes,
                AgreementYears = t.AgreementYears,
                AgreementDays = t.AgreementDays,
                NumberOfWinners = t.NumberOfWinners,
                BonusValue = t.BonusValue,
                PreQualificationName = t.PreQualificationId.HasValue ? t.PreQualification.TenderName : "",
                NeededInitialGurantee = !string.IsNullOrEmpty(t.InitialGuaranteeAddress) ? Resources.TenderResources.DisplayInputs.PrimaryGuarantee : Resources.TenderResources.DisplayInputs.NoGuarantee,
                InitialGuaranteeAddress = t.InitialGuaranteeAddress,
                OfferPresentationWay = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWay.Name : "",
                OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                AgencyCode = t.AgencyCode,
                IsFavouriteTender = t.FavouriteSupplierTenders.Any(f => f.SupplierCRNumber == cr && f.IsActive == true),
                IsPurchased = t.ConditionsBooklets.Any(c => cr != "" ? (c.CommericalRegisterNo == cr && c.IsActive == true && c.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid) : false),
                HasAlternativeOffer = t.HasAlternativeOffer ?? false,
                IsLinkedToAnnouncement = (t.TenderTypeId == (int)Enums.TenderType.LimitedTender && t.PreAnnouncementId != null) ? true : false,
                PreAnnouncementName = t.PreAnnouncement.AnnouncementName,
                PreAnnouncementId = t.PreAnnouncementId,
                TenderTypeName = t.TenderType.NameAr,
                TenderInvitationFees = t.TenderType.InvitationCost,
                TenderPurchaseFees = t.TenderType.BuyingCost,
                CanBuyBooklet = (t.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || t.TenderTypeId == (int)Enums.TenderType.Competition || t.ConditionsBookletPrice == 0) ? "false" : "true",
                FinalGuaranteePercentage = t.AwardingDiscountPercentage,
                CreatedDate = t.CreatedAt,
                TenderLocalContentId = t.TenderLocalContentId,
                LocalContentMechanismIds = new List<int>()
            }).FirstOrDefaultAsync();

            tender.VersionId = await GetCurrentTenderActivityVersion(tenderId);

            tender.RejectionReason = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == tender.TenderStatusId).OrderByDescending(h => h.TenderHistoryId).Select(x => x.RejectionReason).FirstOrDefaultAsync();

            if (tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement)
            {
                tender.GovAgencies = await _context.TenderAgreementAgencies.Where(x => x.TenderId == tenderId).Select(a => new GovAgencyModel { NameArabic = a.GovAgency.NameArabic }).ToListAsync();
            }

            var invitations = await _context.Invitations.Where(c => c.TenderId == tenderId && c.IsActive == true).Include(c => c.BillInfo).Include(c => c.InvitationStatus).ToListAsync();
            tender.SupplierInvitation = invitations.Where(i => !string.IsNullOrEmpty(cr) ? (i.CommericalRegisterNo == cr && i.StatusId != (int)Enums.InvitationStatus.Withdrawal) : i.IsActive == true)
               .Select(c => new SupplierInvitationModel
               {
                   InvitationIdString = Util.Encrypt(c.InvitationId),
                   InvitationStatusIdString = Util.Encrypt(c.StatusId),
                   InvitationTypeIdString = Util.Encrypt(c.InvitationTypeId),
                   InvitationTypeId = c.InvitationTypeId,
                   SadadNumber = c.BillInfo?.BillInvoiceNumber,
                   InvitationStatusName = c.InvitationStatus.NameAr
               }).FirstOrDefault();

            var isNewTender = tender.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && tender.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects;

            tender.ShowInvitationReceipt = isNewTender && invitations.Any(c => !string.IsNullOrEmpty(cr) ?

              (c.CommericalRegisterNo == cr && (c.StatusId == (int)Enums.InvitationStatus.Approved || c.StatusId == (int)Enums.InvitationStatus.WaitingPayment)
               && c.BillInfo != null ? c.BillInfo.ConditionsBookletID == null : false)

              : false) ? true : false;

            tender.CanPurchase = (tender.TenderTypeId == (int)Enums.TenderType.NewTender && (tender.PreQualificationId == null || invitations.Any(i => i.CommericalRegisterNo == cr && (i.StatusId == (int)InvitationStatus.Approved || i.StatusId == (int)InvitationStatus.WaitingPayment))))
                               || ((tender.TenderTypeId == (int)Enums.TenderType.LimitedTender || tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding || tender.TenderTypeId == (int)Enums.TenderType.SecondStageTender)
                               && (tender.InvitationTypeId == (int)Enums.InvitationType.Public || invitations.Any(i => i.CommericalRegisterNo == cr && (i.StatusId == (int)InvitationStatus.Approved || i.StatusId == (int)InvitationStatus.WaitingPayment))))
                               || (tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender)
                               || (tender.InvitationTypeId == (int)Enums.InvitationType.Public && tender.TenderTypeId == (int)Enums.TenderType.Competition);

            tender.FinancialFees = (tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
                         : (invitations.Any() ? tender.TenderInvitationFees
                         : tender.TenderPurchaseFees);

            var offers = await _context.Offers.Where(o => o.TenderId == tenderId && o.IsActive == true).Include(o => o.Combined).ToListAsync();

            tender.OfferStatusId = offers.Where(o => o.CommericalRegisterNo == cr).Select(o => o.OfferStatusId).FirstOrDefault();

            tender.SupplierHasOffer = offers.Any(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.CommericalRegisterNo == cr);
            tender.SupplierHasRecievedOrExcludedOffer = offers.Any(o => o.CommericalRegisterNo == cr && (o.OfferStatusId == (int)Enums.OfferStatus.Received || o.OfferStatusId == (int)Enums.OfferStatus.Excluded));

            tender.IsSupplierCombined = offers.Where(x => x.OfferStatusId == (int)Enums.OfferStatus.Received).SelectMany(s => s.Combined).Any(h => h.CRNumber == cr && h.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader && h.IsActive == true);
            if (!tender.IsOldTender)
            {
                tender.IsTenderNewWithLocalContent = await IsTenderHasLocalContent(tender.CreatedDate);
                if (tender.IsTenderNewWithLocalContent && tender.TenderLocalContentId != null)
                {
                    tender.LocalContentMechanismIds = await GetTenderLocalContentMechanism((int)tender.TenderLocalContentId);
                }
                tender.ConShowLocalContentValues = tender.IsTenderNewWithLocalContent && (tender.LocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.MinimumRequiredMechanismLocalContent) || tender.LocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.MechanismForWeighingLocalContentFinancialEvaluation));
            }
            return tender;

        }

        public async Task<TenderDetailsModel> GetMainTenderDetailsForVisitor(int tenderId)
        {
            var tender = await _context.Tenders
            .Where(t => t.TenderId == tenderId && t.IsActive == true)
            .Where(t => t.SubmitionDate != null && t.InvitationTypeId == (int)Enums.InvitationType.Public)
            .Select(t => new TenderDetailsModel
            {
                TenderStatusName = t.TenderStatusId == (int)Enums.TenderStatus.Established ? Resources.TenderResources.Messages.UnderEstablishing : t.Status.NameAr,
                LastOfferPresentationDate = t.LastOfferPresentationDate,
                LastEnqueriesDate = t.LastEnqueriesDate,
                TenderTypeId = t.TenderTypeId,
                Purpose = t.Purpose,
                ConditionsBookletPrice = t.ConditionsBookletPrice,
                SupplierNeedSample = t.SupplierNeedSample,
                AgencyName = t.Agency != null ? t.Agency.NameArabic : "",
                TenderName = t.TenderName,
                TenderNumber = t.TenderNumber,
                TenderIdString = Util.Encrypt(t.TenderId),
                TenderStatusId = t.TenderStatusId,
                EstimatedValue = t.EstimatedValue,
                InvitationTypeName = t.InvitationType != null ? t.InvitationType.NameAr : "",
                TenderStatusIdString = Util.Encrypt(t.TenderStatusId),
                ReasonForLimitedTenderTypeName = t.ReasonForLimitedTenderType.Name,
                ReferenceNumber = t.ReferenceNumber,
                ReasonForPurchaseTenderTypeName = t.ReasonForPurchaseTenderType.Name,
                InvitationTypeId = t.InvitationTypeId,
                AgreementTypeName = t.AgreementTypeId.HasValue ? t.AgreementType.NameAr : "",
                AgreementMonthes = t.AgreementMonthes,
                AgreementYears = t.AgreementYears,
                AgreementDays = t.AgreementDays,
                NumberOfWinners = t.NumberOfWinners,
                BonusValue = t.BonusValue,
                PreQualificationName = t.PreQualificationId.HasValue ? t.PreQualification.TenderName : "",
                TenderTypeName = t.TenderType.NameAr,
                NeededInitialGurantee = !string.IsNullOrEmpty(t.InitialGuaranteeAddress) ? Resources.TenderResources.DisplayInputs.PrimaryGuarantee : Resources.TenderResources.DisplayInputs.NoGuarantee,
                InitialGuaranteeAddress = t.InitialGuaranteeAddress,
                OfferPresentationWay = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWay.Name : "",
                OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                AgencyCode = t.AgencyCode,
                HasAlternativeOffer = t.HasAlternativeOffer ?? false,
                FinancialFees = ((t.TenderTypeId == (int)Enums.TenderType.CurrentTender || t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || t.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) ? 0
                         : (t.Invitations.Any(s => s.IsActive == true)) ? t.TenderType.InvitationCost
                         : t.TenderType.BuyingCost),
                FinalGuaranteePercentage = t.AwardingDiscountPercentage
            }).FirstOrDefaultAsync();

            tender.VersionId = await GetCurrentTenderActivityVersion(tenderId);

            return tender;
        }

        public async Task<TenderDetailsModel> GetMainTenderDetailsByTenderId(int tenderId, int branchId)
        {
            string unitAgencyCode = _rootConfiguration.UnitAgencyCodeConfiguration.UnitAgencyCode;

            var tender = await _context.Tenders
            .WhereIf(branchId != 0, t => t.BranchId == branchId)
            .Where(t => t.TenderId == tenderId)
            .Select(t => new TenderDetailsModel
            {
                GovAgencies = new List<GovAgencyModel>(),
                LastOfferPresentationDate = t.LastOfferPresentationDate,
                LastEnqueriesDate = t.LastEnqueriesDate,
                TenderTypeId = t.TenderTypeId,
                CreatedByTypeId = t.CreatedByTypeId,
                TenderId = t.TenderId,
                PreQualificationId = t.PreQualificationId,
                TechnicalOrganizationId = t.TechnicalOrganizationId,
                TenderIdString = Util.Encrypt(t.TenderId),
                TenderStatusIdString = Util.Encrypt(t.TenderStatusId),
                TenderTypeName = t.TenderType.NameAr,
                TenderTypeOtherSelectionReason = t.TenderTypeOtherSelectionReason,
                PreQualificationName = t.PreQualificationId.HasValue ? t.PreQualification.TenderName : "",
                InvitationTypeName = t.InvitationType != null ? t.InvitationType.NameAr : "",
                TechnicalOrganizationName = t.TechnicalOrganization.CommitteeName,
                OffersOpeningCommitteeName = t.OffersOpeningCommittee != null ? t.OffersOpeningCommittee.CommitteeName : "",
                OffersCheckingCommitteeName = t.OffersCheckingCommittee != null ? t.OffersCheckingCommittee.CommitteeName : "",
                DirectPurchaseCommitteeName = t.DirectPurchaseCommittee != null ? t.DirectPurchaseCommittee.CommitteeName : "",
                VROCommitteeName = t.VROCommittee != null ? t.VROCommittee.CommitteeName : "",
                CreatedBy = t.CreatedBy,
                TenderStatusName = t.TenderStatusId == (int)Enums.TenderStatus.Established ? Resources.TenderResources.Messages.UnderEstablishing : t.Status.NameAr,
                AgencyName = t.Agency != null ? t.Agency.NameArabic : "",
                TenderName = t.TenderName,
                TenderNumber = t.TenderNumber,
                EstimatedValue = t.EstimatedValue,
                Purpose = t.Purpose,
                DeliveryDate = t.DeliveryDate.HasValue ? t.DeliveryDate.Value.ToHijriDateWithFormat("dd/MM/yyyy hh:mm tt") : "",
                ConditionsBookletPrice = t.ConditionsBookletPrice,
                SupplierNeedSample = t.SupplierNeedSample,
                AgencyCode = t.AgencyCode,
                BranchId = t.BranchId,
                TenderStatusId = t.TenderStatusId,
                ReferenceNumber = t.ReferenceNumber,
                ReasonForLimitedTenderTypeId = t.ReasonForLimitedTenderTypeId,
                ReasonForLimitedTenderTypeName = t.ReasonForLimitedTenderType.Name,
                ReasonForPurchaseTenderTypeId = t.ReasonForPurchaseTenderTypeId,
                ReasonForPurchaseTenderTypeName = t.ReasonForPurchaseTenderType.Name,
                InvitationTypeId = t.InvitationTypeId,
                InvitationTypeIdString = t.InvitationType.NameAr,
                AgreementTypeName = t.AgreementTypeId.HasValue ? t.AgreementType.NameAr : "",
                AgreementMonthes = t.AgreementMonthes,
                AgreementYears = t.AgreementYears,
                AgreementDays = t.AgreementDays,
                NumberOfWinners = t.NumberOfWinners,
                BonusValue = t.BonusValue,
                CreatedByType = t.CreatedByTypeId,
                NeededInitialGurantee = !string.IsNullOrEmpty(t.InitialGuaranteeAddress) ? Resources.TenderResources.DisplayInputs.PrimaryGuarantee : Resources.TenderResources.DisplayInputs.NoGuarantee,
                InitialGuaranteeAddress = t.InitialGuaranteeAddress,
                OfferPresentationWay = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWay.Name : "",
                OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                IsUnitAgencyCheckNeeded = (t.EstimatedValue.HasValue &&
                t.EstimatedValue > (int)Enums.ConitionalBookletPriceList.MoreThanTwentyFiveMilion
                && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase
                && t.TenderTypeId != (int)Enums.TenderType.CurrentTender)
                || (t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement && t.AgencyCode != unitAgencyCode),
                HasAlternativeOffer = t.HasAlternativeOffer ?? false,
                IsLowBudgetPurchase = t.IsLowBudgetDirectPurchase,
                PurchaseMemberName = t.UserProfile.FullName,
                FinalGuaranteePercentage = t.AwardingDiscountPercentage
            }).FirstOrDefaultAsync();

            tender.VersionId = await GetCurrentTenderActivityVersion(tenderId);

            tender.RejectionReason = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == tender.TenderStatusId).OrderByDescending(h => h.TenderHistoryId).Select(x => x.RejectionReason).FirstOrDefaultAsync();

            if (tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement)
            {
                tender.GovAgencies = await _context.TenderAgreementAgencies.Where(x => x.TenderId == tenderId).Select(a => new GovAgencyModel { NameArabic = a.GovAgency.NameArabic }).ToListAsync();
            }
            return tender;
        }

        public async Task<TenderDetailsModel> GetMainTenderDetailsForUnit(int tenderId, string agencyCode)
        {
            string unitAgencyCode = _rootConfiguration.UnitAgencyCodeConfiguration.UnitAgencyCode;
            bool isUnitUserRole = _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitManagerUser) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitSpecialistLevel1) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitSpecialistLevel2);

            var localContentSettings = await _context.LocalContentSettings.FirstOrDefaultAsync();
            var tenderActivityId = await GetCurrentTenderActivityVersion(tenderId);

            var tender = await _context.Tenders
            .Where(t => t.TenderId == tenderId)
            .WhereIf(!string.IsNullOrEmpty(agencyCode), t => t.AgencyCode == agencyCode || t.Agency.VROOfficeCode == agencyCode || t.VRORelatedBranch.AgencyCode == agencyCode)
            .Select(t => new TenderDetailsModel
            {
                GovAgencies = new List<GovAgencyModel>(),
                LastOfferPresentationDate = t.LastOfferPresentationDate,
                LastEnqueriesDate = t.LastEnqueriesDate,
                TenderTypeId = t.TenderTypeId,
                CreatedByTypeId = t.CreatedByTypeId,
                TenderId = t.TenderId,
                PreQualificationId = t.PreQualificationId,
                InvitationTypeIdString = t.InvitationType == null ? "" : t.InvitationType.NameAr,
                TechnicalOrganizationId = t.TechnicalOrganizationId,
                TenderIdString = Util.Encrypt(t.TenderId),
                TenderStatusIdString = Util.Encrypt(t.TenderStatusId),
                TenderTypeName = t.TenderType.NameAr,
                TenderTypeOtherSelectionReason = t.TenderTypeOtherSelectionReason,
                PreQualificationName = t.PreQualificationId.HasValue ? t.PreQualification.TenderName : "",
                InvitationTypeName = t.InvitationType != null ? t.InvitationType.NameAr : "",
                HasPreQualification = t.PreQualificationId.HasValue ? Resources.SharedResources.DisplayInputs.Yes : Resources.SharedResources.DisplayInputs.No,
                TechnicalOrganizationName = t.TechnicalOrganization.CommitteeName,
                OffersOpeningCommitteeName = t.OffersOpeningCommittee != null ? t.OffersOpeningCommittee.CommitteeName : "",
                OffersCheckingCommitteeName = t.OffersCheckingCommittee != null ? t.OffersCheckingCommittee.CommitteeName : "",
                DirectPurchaseCommitteeName = t.DirectPurchaseCommittee != null ? t.DirectPurchaseCommittee.CommitteeName : "",
                VROCommitteeName = t.VROCommittee != null ? t.VROCommittee.CommitteeName : "",
                CreatedBy = t.CreatedBy,
                TenderStatusName = t.TenderStatusId == (int)Enums.TenderStatus.Established ? Resources.TenderResources.Messages.UnderEstablishing : t.Status.NameAr,
                AgencyName = t.Agency != null ? t.Agency.NameArabic : "",
                TenderName = t.TenderName,
                TenderNumber = t.TenderNumber,
                EstimatedValue = GetEstimatedValue(t.TenderTypeId, t.TenderStatusId, t.EstimatedValue, isUnitUserRole),
                TenderUnitStatusId = t.TenderUnitStatusId,
                Purpose = t.Purpose,
                ConditionsBookletPrice = t.ConditionsBookletPrice,
                SupplierNeedSample = t.SupplierNeedSample,
                DeliveryDate = t.DeliveryDate.HasValue ? t.DeliveryDate.Value.ToHijriDateWithFormat("dd/MM/yyyy hh:mm tt") : "",
                SamplesDeliveryAddress = Resources.TenderResources.DisplayInputs.SamplesDeliveryAddress + ":" + t.SamplesDeliveryAddress == null ? "" : t.SamplesDeliveryAddress
                + (t.BuildingName != null ? ", " + Resources.TenderResources.DisplayInputs.BuildingName + ":" + t.BuildingName : "")
                + (t.FloarNumber != null ? ", " + Resources.TenderResources.DisplayInputs.FloarNumber + ":" + t.FloarNumber : "")
                + (t.DepartmentName != null ? ", " + Resources.TenderResources.DisplayInputs.DepartmentName + ":" + t.DepartmentName : ""),
                OffersOpeningAddress = t.OffersOpeningAddress.AddressName,
                AgencyCode = t.AgencyCode,
                BranchId = t.BranchId,
                TenderStatusId = t.TenderStatusId,
                CanUnitDoAnyAction =
                (!t.TenderUnitAssigns.Any() && _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitSpecialistLevel1) && t.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview) ||
                    (t.TenderUnitStatusId != (int)Enums.TenderUnitStatus.WaitingManagerApprove && t.TenderUnitStatusId != (int)Enums.TenderUnitStatus.UnderManagerReviewing &&
                    (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitSpecialistLevel1) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitSpecialistLevel2)) &&
                    t.TenderUnitAssigns.Any() ? t.TenderUnitAssigns.Where(u => u.UnitSpecialistLevel != (int)Enums.UnitSpecialistLevel.UnitManager).OrderByDescending(a =>
                    a.TenderUnitAssignId).FirstOrDefault(a => a.IsActive == true && a.IsCurrentlyAssigned == true).UserProfileId == Convert.ToInt32(_httpContextAccessor.HttpContext.User.UserId()) : false) ||
                    (!t.TenderUnitAssigns.Any(u => u.UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitManager) && _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitManagerUser) && t.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove) ||
                    ((t.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove || t.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderManagerReviewing) && _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.UnitManagerUser) &&
                    t.TenderUnitAssigns.Any(u => u.UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitManager) ?
                    t.TenderUnitAssigns.Where(u => u.UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitManager).OrderByDescending(a =>
                    a.TenderUnitAssignId).FirstOrDefault(a => a.IsActive == true && a.IsCurrentlyAssigned == true).UserProfileId == Convert.ToInt32(_httpContextAccessor.HttpContext.User.UserId()) : false),
                IsUnitSecreteryAccepted = t.IsUnitSecreteryAccepted,
                UnitModificatationsFiles = t.Attachments.Where(a => a.IsActive == true && a.AttachmentTypeId == (int)Enums.AttachmentType.UnitModificationsAttachmentsToDataEntry)
                .Select(a => new TenderAttachmentModel
                {
                    TenderId = t.TenderId,
                    AttachmentTypeId = a.AttachmentTypeId,
                    Name = a.Name,
                    FileNetReferenceId = a.FileNetReferenceId,
                }).ToList(),
                IsUnitAgencyCheckNeeded = IsUnitAgencyCheckNeeded(t, unitAgencyCode),
                ReferenceNumber = t.ReferenceNumber,
                ReasonForLimitedTenderTypeId = t.ReasonForLimitedTenderTypeId,
                ReasonForLimitedTenderTypeName = t.ReasonForLimitedTenderType.Name,
                ReasonForPurchaseTenderTypeId = t.ReasonForPurchaseTenderTypeId,
                ReasonForPurchaseTenderTypeName = t.ReasonForPurchaseTenderType.Name,
                InvitationTypeId = t.InvitationTypeId,
                AgreementTypeName = t.AgreementTypeId.HasValue ? t.AgreementType.NameAr : "",
                AgreementMonthes = t.AgreementMonthes,
                AgreementYears = t.AgreementYears,
                AgreementDays = t.AgreementDays,
                NumberOfWinners = t.NumberOfWinners,
                BonusValue = t.BonusValue,
                NeededInitialGurantee = !string.IsNullOrEmpty(t.InitialGuaranteeAddress) ? Resources.TenderResources.DisplayInputs.PrimaryGuarantee : Resources.TenderResources.DisplayInputs.NoGuarantee,
                InitialGuaranteeAddress = t.InitialGuaranteeAddress,
                FinalGuaranteePercentage = t.AwardingDiscountPercentage,
                OfferPresentationWay = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWay.Name : "",
                OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                HasAlternativeOffer = t.HasAlternativeOffer ?? false,
                EstimateValue = t.EstimatedValue ?? 0,

                ContainsSupply = t.QuantityTableVersionId > (int)Enums.QuantityTableVersion.Version1 && (tenderActivityId >= (int)Enums.ActivityVersions.Sprint7Activities
                        ? t.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(v => v.TemplateId == (int)TenderActivityTamplate.GeneralSupply))
                        : t.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(v => v.TemplateId == (int)TenderActivityTamplate.GeneralMaterials))),

                IsTenderContainsTawreedTables = t.IsTenderContainsTawreedTables.HasValue && t.IsTenderContainsTawreedTables.Value,
                IsTenderTypeHasNPPrecentage = t.TenderTypeId != (int)Enums.TenderType.FirstStageTender && t.TenderTypeId != (int)Enums.TenderType.Competition && t.TenderTypeId != (int)Enums.TenderType.ReverseBidding
                && t.TenderTypeId != (int)Enums.TenderType.CurrentTender && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && t.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects,
                IsLinkedToAnnouncement = (t.TenderTypeId == (int)Enums.TenderType.LimitedTender && t.PreAnnouncementId != null) ? true : false,
                PreAnnouncementId = t.PreAnnouncementId,
                PreAnnouncementName = t.PreAnnouncement.AnnouncementName,
                IsLowBudgetPurchase = t.IsLowBudgetDirectPurchase,
                PurchaseMemberName = t.UserProfile.FullName,
                NationalProductRate = t.NationalProductPercentage.GetValueOrDefault(),
                //NationalProductPercentage = t.TenderLocalContent.NationalProductPercentage.GetValueOrDefault(),
                NationalProductPercentage = t.TenderLocalContentId != null ? (t.TenderLocalContent.NationalProductPercentage != null ? t.TenderLocalContent.NationalProductPercentage : localContentSettings.NationalProductPercentage) : t.NationalProductPercentage != null ? t.NationalProductPercentage : localContentSettings.NationalProductPercentage,

                MinimumBaseline = t.TenderLocalContent.MinimumBaseline,
                MinimumPercentageLocalContentTarget = t.TenderLocalContent.MinimumPercentageLocalContentTarget,
                //NationalProductPercentage = t.NationalProductPercentage != null ? t.NationalProductPercentage : t.TenderLocalContent.NationalProductPercentage,
                LocalContentMaximumPercentage = t.TenderLocalContent.LocalContentMaximumPercentage != null ? t.TenderLocalContent.LocalContentMaximumPercentage : localContentSettings.LocalContentMaximumPercentage,
                PriceWeightAfterAdjustment = t.TenderLocalContent.PriceWeightAfterAdjustment != null ? t.TenderLocalContent.PriceWeightAfterAdjustment : localContentSettings.PriceWeightAfterAdjustment,
                LocalContentWeightAndFinancialMarket = t.TenderLocalContent.LocalContentWeightAndFinancialMarket != null ? t.TenderLocalContent.LocalContentWeightAndFinancialMarket : localContentSettings.LocalContentWeightAndFinancialMarket,
                BaselineWeight = t.TenderLocalContent.BaselineWeight != null ? t.TenderLocalContent.BaselineWeight : 50,
                LocalContentTargetWeight = t.TenderLocalContent.LocalContentTargetWeight != null ? t.TenderLocalContent.LocalContentTargetWeight : 50,
                FinancialMarketListedWeight = t.TenderLocalContent.FinancialMarketListedWeight != null ? t.TenderLocalContent.FinancialMarketListedWeight : 5,
                TenderLocalContentId = t.TenderLocalContentId,
                CreatedDate = t.CreatedAt,
                LocalContentMechanismIds = new List<int>()

            }).FirstOrDefaultAsync();

            tender.VersionId = await GetCurrentTenderActivityVersion(tenderId);

            tender.IsTenderNewWithLocalContent = await IsTenderHasLocalContent(tender.CreatedDate);

            if (tender.IsTenderNewWithLocalContent && tender.TenderLocalContentId != null)
            {
                tender.LocalContentMechanismIds = await GetTenderLocalContentMechanism((int)tender.TenderLocalContentId);
            }

            tender.ConShowNationalProduct = tender.ContainsSupply || tender.IsTenderContainsTawreedTables || (tender.IsTenderNewWithLocalContent && tender.LocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.ThePricePreferenceMechanismNationalProduct));
            tender.ConShowLocalContentValues = tender.IsTenderNewWithLocalContent && (tender.LocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.MinimumRequiredMechanismLocalContent) || tender.LocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.MechanismForWeighingLocalContentFinancialEvaluation));

            if (tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement)
            {
                tender.GovAgencies = await _context.TenderAgreementAgencies.Where(x => x.TenderId == tenderId).Select(a => new GovAgencyModel { NameArabic = a.GovAgency.NameArabic }).ToListAsync();
            }
            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId).ToListAsync();
            tender.RejectionReason = tenderHistories.Where(h => h.StatusId == tender.TenderStatusId).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().RejectionReason;
            tender.ApprovedBy = tenderHistories.Where(h => (h.ActionId == (int)Enums.TenderActions.ApproveTender || h.ActionId == (int)Enums.TenderActions.ApproveTenderByUnitManager) && tender.TenderStatusId == (int)Enums.TenderStatus.Approved).OrderByDescending(h => h.CreatedAt).Select(h => h.CreatedBy).FirstOrDefault();
            tender.RejectedBy = tenderHistories.Where(h => tender.TenderStatusId == (int)Enums.TenderStatus.Rejected).OrderByDescending(h => h.CreatedAt).Select(h => h.CreatedBy).FirstOrDefault();

            var tenderUnitStatusesHistories = await _context.TenderUnitStatusesHistories.Where(h => h.TenderId == tenderId).OrderByDescending(h => h.TenderUnitStatusesHistoryId).Include(h => h.TenderUnitUpdateType).ToListAsync();
            if (tenderUnitStatusesHistories.Any())
            {
                tender.UnitRejectionReason = tenderUnitStatusesHistories.FirstOrDefault(u => !string.IsNullOrEmpty(u.Comment))?.Comment;
                tender.UnitModificationType = tenderUnitStatusesHistories.FirstOrDefault().TenderUnitUpdateType?.Name;
                tender.UnitNotes = tenderUnitStatusesHistories.FirstOrDefault().Comment;
            }
            return tender;
        }

        #endregion

        private static bool IsUnitAgencyCheckNeeded(Tender tender, string unitAgencyCode)
        {
            var isOldTender = tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender;
            var isMoreThanTwentyFiveMillion = tender.EstimatedValue.HasValue
                && tender.EstimatedValue > (int)Enums.ConitionalBookletPriceList.MoreThanTwentyFiveMilion;
            var isFrameworkTender = tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement
                && tender.AgencyCode != unitAgencyCode;

            return (isMoreThanTwentyFiveMillion && !isOldTender) || isFrameworkTender;
        }

        private static decimal? GetEstimatedValue(int tenderType, int tenderStatus, decimal? estimatedValue, bool isUnitUserRole)
        {
            if (
                (tenderType == (int)Enums.TenderType.CurrentDirectPurchase || tenderType == (int)Enums.TenderType.CurrentTender)
                &&
                (
                tenderStatus == (int)Enums.TenderStatus.OffersChecking
                || tenderStatus == (int)Enums.TenderStatus.OffersCheckedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersCheckedPending
                || tenderStatus == (int)Enums.TenderStatus.OffersCheckedRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedPending
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersAwarding
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedPending
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedRejected
                )
                )
            {
                return estimatedValue;
            }
            else if (isUnitUserRole ||
                (tenderType != (int)Enums.TenderType.CurrentDirectPurchase && tenderType != (int)Enums.TenderType.CurrentTender && tenderType != (int)Enums.TenderType.NationalTransformationProjects) &&
                (
                tenderStatus == (int)Enums.TenderStatus.OffersOpenFinancialStage
                || tenderStatus == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending
                || tenderStatus == (int)Enums.TenderStatus.OffersOppening
                || tenderStatus == (int)Enums.TenderStatus.OffersOppenedRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersOppenedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersOppenedPending
                || tenderStatus == (int)Enums.TenderStatus.DirectPurchaseOffersChecking
                || tenderStatus == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApproved
                || tenderStatus == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApprovePending
                || tenderStatus == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingPending
                || tenderStatus == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingRejected)
                || tenderStatus == (int)Enums.TenderStatus.OffersCheckedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersCheckedPending
                || tenderStatus == (int)Enums.TenderStatus.OffersCheckedRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedPending
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersAwarding
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedPending
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersTechnicalCheckingPending
                || tenderStatus == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending
                || tenderStatus == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved
                || tenderStatus == (int)Enums.TenderStatus.OffersFinancialChecking
                || tenderStatus == (int)Enums.TenderStatus.OffersChecking
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedPending
                || tenderStatus == (int)Enums.TenderStatus.OffersAwardedRejected
                || tenderStatus == (int)Enums.TenderStatus.OffersAwarding
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedConfirmed
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedPending
                || tenderStatus == (int)Enums.TenderStatus.OffersInitialAwardedRejected
                )
            {
                return estimatedValue;
            }
            else if ((tenderType == (int)Enums.TenderType.NationalTransformationProjects) &&
                (
                tenderStatus == (int)Enums.TenderStatus.VROFinancialCheckingOpening
                || tenderStatus == (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval
                || tenderStatus == (int)Enums.TenderStatus.VROOffersFinancialChecking
                || tenderStatus == (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved
                || tenderStatus == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                || tenderStatus == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected
                || tenderStatus == (int)Enums.TenderStatus.VROOffersTechnicalChecking
                || tenderStatus == (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved
                || tenderStatus == (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending
                || tenderStatus == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected
                )
                )
            {
                return estimatedValue;
            }
            else
            {
                return null;
            }
        }

        public async Task<TenderCommunicationRequestModel> GetCommunicationRequestsDetailsTenderId(int tenderId, string cr)
        {
            var userId = _httpContextAccessor.HttpContext.User.UserId();

            var tender = await _context.Tenders
                .Where(t => t.IsActive == true && t.TenderId == tenderId)
                  .Select(t => new TenderCommunicationRequestModel
                  {
                      CanAddPlaint = string.IsNullOrEmpty(cr) ? false :
                                  t.Offers.Where(o =>
                                  t.IsActive == true && t.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed && o.IsActive == true
                                  && !o.PlaintRequests.Any() && o.CommericalRegisterNo.Equals(cr)).FirstOrDefault() == null ? false
                                  :
                                  (
                                              (t.TenderHistories.OrderByDescending(a => a.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed) == null ? false : t.TenderHistories
                                              .OrderByDescending(a => a.TenderHistoryId)
                                              .FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).CreatedAt.Date.AddDays(t.AwardingStoppingPeriod.Value).Date
                                              >= DateTime.Now.Date && DateTime.Now.Date >= t.TenderHistories.OrderByDescending(a => a.TenderHistoryId)
                                              .FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).CreatedAt.Date)
                                              &&
                                              (!t.TenderAwardingHistories.Any() ? false : t.TenderAwardingHistories
                                              .OrderByDescending(a => a.AwardingIndex).Select(th => th.AwardingIndex).FirstOrDefault() == (int)Enums.AwardingHistoryIndex.One ||
                                              (!t.TenderAwardingHistories.Any() ? false : t.TenderAwardingHistories.OrderByDescending(a => a.AwardingIndex).Select(th => th.AwardingIndex).FirstOrDefault() == (int)Enums.AwardingHistoryIndex.Two
                                              && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && t.TenderTypeId != (int)Enums.TenderType.CurrentTender && t.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects
                                              && t.TenderAwardingHistories.Where(th => th.AwardingIndex == (int)Enums.AwardingHistoryIndex.One && th.CommercialRegisterationNumber == cr).FirstOrDefault() != null)
                                              )
                                  ),
                      LastOfferPresentationDate = t.LastOfferPresentationDate,
                      LastEnqueriesDate = t.LastEnqueriesDate,
                      TechnicalCommitteeId = t.TechnicalOrganizationId,
                      TenderIdString = Util.Encrypt(tenderId),
                      TenderStatusIdString = Util.Encrypt(t.TenderStatusId),
                      OfferCheckingDate = t.OffersCheckingDate,
                      OfferOpeningDate = t.OffersOpeningDate,
                      TenderTypeIdString = Util.Encrypt(t.TenderTypeId),
                      IsSupplierCombined = t.Offers.Where(x => x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received).SelectMany(s => s.Combined).Any(h => h.CRNumber == cr && h.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader && h.IsActive == true),
                      IsPurchased = t.ConditionsBooklets.Any(c => cr != "" ? (c.CommericalRegisterNo == cr && c.IsActive == true && c.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid) : false),
                      IsLowBudgetDirectPurchase = t.IsLowBudgetDirectPurchase,
                      DirectPurchaseMemberId = t.DirectPurchaseMemberId,
                      InvitationStatusId = t.Invitations.FirstOrDefault(c => c.CommericalRegisterNo == cr && c.IsActive == true) != null ? t.Invitations.Where(s => s.IsActive == true).FirstOrDefault(c => c.CommericalRegisterNo == cr).StatusId : 0,
                      InvitationTypeId = t.InvitationTypeId,
                      SupplierHasOffers = t.Offers.Any(r => r.IsActive == true && r.CommericalRegisterNo == cr && r.OfferStatusId == (int)Enums.OfferStatus.Received),
                      SupplierUploadQualificationDoc = t.QualificationSupplierData.Where(r => r.SupplierSelectedCr == cr && r.TenderId == tenderId).FirstOrDefault() != null ? true : false,
                      SupplierHasExtendRequest = t.AgencyCommunicationRequests.Any(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.SupplierOfferExtendDates && a.IsActive == true) ? t.AgencyCommunicationRequests.Any(x => x.SupplierExtendOfferDatesRequest.CR == cr && x.IsActive == true) : false,
                      TenderHasExtendOffersValidity = t.AgencyCommunicationRequests.Any(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy && a.IsActive == true
                      &&
                      ((a.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished
                      && (!a.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(s => s.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Rejected
                           || s.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Accepted))
                      && a.ExtendOffersValidity.NewOffersExpiryDate > DateTime.Now)
                      ||
                      (a.ExtendOffersValidity.NewOffersExpiryDate > DateTime.Now &&
                       !a.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(s => s.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Rejected
                           || s.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Accepted))
                       )),
                      IsUserHasAccessToLowBudgetTender = (t.IsLowBudgetDirectPurchase.HasValue && t.IsLowBudgetDirectPurchase == true && t.DirectPurchaseMemberId == userId)
                  }).FirstOrDefaultAsync();

            var firstNegotiation = await _context.NegotiationFirstStages.Where(t => t.AgencyCommunicationRequest.TenderId == tenderId).FirstOrDefaultAsync();
            tender.IsNewNegotiation = firstNegotiation != null ? firstNegotiation.IsNewNegotiation.HasValue : false;
            return tender;
        }

        public async Task<QueryResult<Tender>> FindTendersByLogedUser(string agencyCode)
        {
            var result = await _context.Tenders
                .Where(x => x.IsActive == true && x.AgencyCode == agencyCode
                && (x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && x.TenderStatusId != (int)Enums.TenderStatus.Established
                && x.TenderStatusId != (int)Enums.TenderStatus.Pending && x.TenderStatusId != (int)Enums.TenderStatus.Rejected))
                .Include(x => x.TenderType)
                .Include(x => x.InvitationType)
                .Include(x => x.Status)
                .OrderBy(x => x.TenderId)
                .ToQueryResult(1, 10000);
            return result;
        }

        public async Task<AllBasicTenderDataModel> GetBasicTenderDetailsById(int tenderId, string agencyCode)
        {


            DateTime now = DateTime.Now;
            return await _context.Tenders
                .WhereIf(agencyCode != "", t => t.AgencyCode == agencyCode || t.VRORelatedBranch.AgencyCode == agencyCode)
                .Where(t => t.TenderId == tenderId)
                .Select(t => new AllBasicTenderDataModel
                {
                    TenderTypeName = t.TenderType.NameAr,
                    TenderTypeId = t.TenderTypeId,
                    ReasonForLimitedTenderTypeName = t.ReasonForLimitedTenderType != null ? t.ReasonForLimitedTenderType.Name : "",
                    ReasonForPurchaseTenderTypeName = t.ReasonForPurchaseTenderType != null ? t.ReasonForPurchaseTenderType.Name : "",
                    TenderTypeOtherSelectionReason = t.TenderTypeOtherSelectionReason,
                    HasQualification = t.PreQualificationId.HasValue,
                    PreQualificationName = t.PreQualification != null ? t.PreQualification.TenderName : "",
                    InvitationTypeName = t.InvitationType != null ? t.InvitationType.NameAr : "",
                    TechnicalOrganizationName = t.TechnicalOrganization.CommitteeName,
                    OffersOpeningCommitteeName = t.OffersOpeningCommittee != null ? t.OffersOpeningCommittee.CommitteeName : "",
                    OffersCheckingCommitteeName = t.OffersCheckingCommittee != null ? t.OffersCheckingCommittee.CommitteeName : "",
                    DirectPurchaseCommitteeName = t.DirectPurchaseCommittee != null ? t.DirectPurchaseCommittee.CommitteeName : "",
                    CreatedBy = t.CreatedBy,
                    TenderStatusName = t.TenderStatusId == (int)Enums.TenderStatus.Established ? "تحت الإنشاء" : t.Status.NameAr,
                    TenderUnitStatusId = t.TenderUnitStatusId,
                    AgencyName = t.Agency != null ? t.Agency.NameArabic : "",
                    RemainingDays = t.LastOfferPresentationDate.HasValue && t.LastOfferPresentationDate > now ? (t.LastOfferPresentationDate.Value - now).Days : 0,
                    RemainingHours = t.LastOfferPresentationDate.HasValue && t.LastOfferPresentationDate > now ? (t.LastOfferPresentationDate.Value - now).Hours : 0,
                    RemainingMins = t.LastOfferPresentationDate.HasValue && t.LastOfferPresentationDate > now ? (t.LastOfferPresentationDate.Value - now).Minutes : 0,
                    ReferenceNumber = t.ReferenceNumber,
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    Purpose = t.Purpose,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    EstimatedValue = t.EstimatedValue ?? 0,
                    EstimatedValueString = new ConvertNumberToText(t.EstimatedValue ?? 0, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic(),
                    NeedInitialGuarantee = !string.IsNullOrEmpty(t.InitialGuaranteeAddress),
                    InitialGuaranteeAddress = t.InitialGuaranteeAddress,
                    DeliveryDate = t.DeliveryDate.HasValue ? t.DeliveryDate.Value.ToHijriDateWithFormat("dd/MM/yyyy hh:mm tt") : "",
                    SupplierNeedSample = t.SupplierNeedSample,
                    SamplesDeliveryAddress = Resources.TenderResources.DisplayInputs.SamplesDeliveryAddress + ":" + t.SamplesDeliveryAddress == null ? "" : t.SamplesDeliveryAddress
                + (t.BuildingName != null ? ", " + Resources.TenderResources.DisplayInputs.BuildingName + ":" + t.BuildingName : "")
                + (t.FloarNumber != null ? ", " + Resources.TenderResources.DisplayInputs.FloarNumber + ":" + t.FloarNumber : "")
                + (t.DepartmentName != null ? ", " + Resources.TenderResources.DisplayInputs.DepartmentName + ":" + t.DepartmentName : ""),
                    OffersOpeningAddress = t.OffersOpeningAddress.AddressName,
                    AgencyCode = t.AgencyCode,
                    TenderStatusId = t.TenderStatusId,
                    OfferPresentationWayName = t.OfferPresentationWay != null ? t.OfferPresentationWay.Name : ""
                }).FirstOrDefaultAsync();
        }

        public async Task<EditeCommitteesModel> GetTenderCommitteesByTenderId(int tenderId, int branchId)
        {
            var entities = await _context.Tenders
            .Where(t => t.IsActive == true && t.TenderId == tenderId /*&& t.BranchId == branchId*/)
            .Select(t => new EditeCommitteesModel
            {
                PreQualificationCommitteeId = t.PreQualificationCommitteeId,
                TenderName = t.TenderName,
                TenderNumber = t.TenderNumber,
                ReferenceNumber = t.ReferenceNumber,
                TenderStatusName = t.Status.NameAr,
                TenderTypeName = t.TenderType.NameAr,
                TenderStatusId = t.TenderStatusId,
                TechnicalOrganizationId = t.TechnicalOrganizationId,
                OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                VROCommitteeId = t.VROCommitteeId,
                DirectPurchaseCommitteeId = t.DirectPurchaseCommitteeId,
                TenderId = t.TenderId,
                TenderTypeId = t.TenderTypeId,
                BranchId = t.BranchId,
                TenderIdString = Util.Encrypt(t.TenderId),
                LastEnqueriesDate = t.LastEnqueriesDate,
                IsLowBudgetDirectPurchase = t.IsLowBudgetDirectPurchase,
                EstimatedValue = t.EstimatedValue,
                DirectPurchaseCommitteeMemberId = t.DirectPurchaseMemberId
            }).FirstOrDefaultAsync();
            return entities;
        }
        public async Task<Tender> GetTenderByIdWithoutAnyIncluds(int tenderId)
        {
            var entities = await _context.Tenders
            .Where(t => t.IsActive == true && t.TenderId == tenderId).FirstOrDefaultAsync();
            return entities;
        }
        public async Task<Tender> GetTenderByIdWitOffersAndBidingRounds(int tenderId)
        {
            var entities = await _context.Tenders
                .Include(t => t.Offers).Include(t => t.BiddingRounds)
            .Where(t => t.IsActive == true && t.TenderId == tenderId).FirstOrDefaultAsync();
            return entities;
        }


        public async Task<Tender> GetTenderByIdWithAwardingHistoy(int tenderId)
        {
            var tender = await _context.Tenders
                .Include(t => t.TenderAwardingHistories)
            .Where(t => t.IsActive == true && t.TenderId == tenderId)
            .FirstOrDefaultAsync();

            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwarding).ToListAsync();
            tender.SetTenderHistories(tenderHistories);
            return tender;
        }
        public async Task<Tender> GetTenderWithTypeAndInvitations(int tenderId)
        {
            var entities = await _context.Tenders.Include(x => x.TenderType).Include(x => x.Invitations).Include(x => x.Agency)
            .Where(t => t.IsActive == true && t.TenderId == tenderId).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> GetTenderByIdAndBranch(int tenderId, int branchId)
        {
            var entities = await _context.Tenders.Include(t => t.Offers).Include(f => f.Branch).Include(t => t.TenderType)
                .Include(a => a.TenderActivities)
                .Include(a => a.TenderAreas)
                .Include(a => a.TenderCountries)
                .Include(a => a.TenderConstructionWorks)
                .Include(a => a.TenderMaintenanceRunnigWorks)
                .Include(t => t.TenderConditionsTemplate)
                .ThenInclude(t => t.TenderConditionsTemplateMaterialInofmration)
                .Include(t => t.TenderConditionsTemplate)
                .ThenInclude(t => t.TemplateCertificates)
                .Include(t => t.TenderConditionsTemplate)
                .ThenInclude(t => t.TenderConditionsTemplateTechnicalDelrations)
                .Include(t => t.TenderConditionsTemplate)
                .ThenInclude(t => t.TenderConditionsTemplateTechnicalOutputs)
            .Where(t => t.IsActive == true && t.TenderId == tenderId && t.BranchId == branchId).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> GetTendersRelatedToFirstStage(int tenderId, int branchId)
        {
            var entities = await _context.Tenders
            .Where(t => t.IsActive == true && t.TenderFirstStageId == tenderId && t.BranchId == branchId).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<SecondStageBasicData> GetTenderByIdAndBranchForSecondStage(int tenderId, int branchId)
        {
            var entities = await _context.Tenders.Where(t => t.IsActive == true && t.TenderId == tenderId && t.BranchId == branchId)
                .Select(t => new SecondStageBasicData
                {
                    InitialGuaranteeAddress = t.InitialGuaranteeAddress,
                    InitialGuaranteePercentage = t.InitialGuaranteePercentage,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderStatusId = t.TenderStatusId,
                    TenderFirstStageId = t.TenderId,
                    TenderTypeId = t.TenderTypeId,
                    TenderTypeName = t.TenderType.NameAr,
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    ReferenceNumber = t.ReferenceNumber,
                    EstimatedValue = (decimal)t.EstimatedValue,
                    TechnicalOrganizationId = t.TechnicalOrganizationId ?? 0,
                    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                    Purpose = t.Purpose,
                    CreatedBy = t.CreatedBy,
                    BranchId = t.BranchId,
                    AgencyBudgetNumber = t.AgencyBudgetNumbers != null ? t.AgencyBudgetNumbers.Select(s => new AgencyBudgetNumberModel
                    {
                        TenderId = s.TenderId.Value,
                        ProjectName = s.ProjectName,
                        ProjectNumber = s.ProjectNumber,
                        IsGSF = (s.IsProject ?? false),
                        IsProject = (s.IsProject ?? false),
                        Cache = s.Cache,
                        Cost = s.Cost
                    }).ToList() : null,
                }).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<CreateTenderBasicDataModel> GetBasicTenderByIdAndBranch(int tenderId, int branchId)
        {
            string unitAgencyCode = _rootConfiguration.UnitAgencyCodeConfiguration.UnitAgencyCode;

            var tender = await _context.Tenders
            .Where(t => t.IsActive == true && t.TenderId == tenderId && t.BranchId == branchId)
            .Select(t => new CreateTenderBasicDataModel
            {
                TenderName = t.TenderName,
                BranchId = t.BranchId,
                TenderTypeId = t.TenderTypeId,
                TenderNumber = t.TenderNumber,
                ConditionsBookletPrice = t.ConditionsBookletPrice,
                InvitationTypeId = t.InvitationTypeId,
                TechnicalOrganizationId = t.TechnicalOrganizationId,
                OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                DirectPurchaseCommitteeId = t.DirectPurchaseCommitteeId,
                VROCommitteeId = t.VROCommitteeId,
                Purpose = t.Purpose,
                TenderStatusId = t.TenderStatusId,
                TenderStatusIdString = Util.Encrypt(t.TenderStatusId),
                TenderIdString = Util.Encrypt(t.TenderId),
                TenderId = t.TenderId,
                SpendingCategoryId = t.SpendingCategoryId,
                TenderTypeName = t.TenderType.NameAr,
                HasQualification = t.PreQualificationId != null ? true : false,
                ReasonForLimitedTenderTypeId = t.ReasonForLimitedTenderTypeId,
                ReasonForLimitedTenderTypeName = t.ReasonForLimitedTenderType.Name,
                ReasonForPurchaseTenderTypeId = t.ReasonForPurchaseTenderTypeId,
                ReasonForPurchaseTenderTypeName = t.ReasonForPurchaseTenderType.Name,
                TenderTypeOtherSelectionReason = t.TenderTypeOtherSelectionReason,
                CreatedBy = t.CreatedBy,
                ReferenceNumber = t.ReferenceNumber,
                OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                EstimatedValue = (decimal)t.EstimatedValue,
                PreQualificationId = t.PreQualificationId,
                AgreementMonthes = t.AgreementMonthes,
                AgreementTypeId = t.AgreementTypeId,
                AgreementYears = t.AgreementYears,
                AgreementDays = t.AgreementDays,
                TenderAgreementAgencyIDs = new List<string>(), // t.TenderAgreementAgencies.Select(tender => tender.AgencyCode).ToList(),
                NumberOfWinners = t.NumberOfWinners,
                BonusValue = t.BonusValue,
                IsVRO = t.Agency.IsVRO,
                IsExceptedAgency = t.Agency.IsExcepted,
                PurchaseMethodString = t.Agency.PurchaseMethods,
                PreviousFramWorkId = t.PreviousFramWorkId,
                SamplesDeliveryAddress = t.SamplesDeliveryAddress,
                SupplierNeedSample = t.SupplierNeedSample,
                QuantityTableVersionId = t.QuantityTableVersionId,
                IsUnitAgency = t.AgencyCode == unitAgencyCode ? true : false,
                AgencyBudgetNumbers = null,
                IsLinkedToAnnouncement = (t.TenderTypeId == (int)Enums.TenderType.LimitedTender && (t.PreAnnouncementId != null || t.AnnouncementTemplateId != null)) ? true : false,
                PreAnnouncementId = t.PreAnnouncementId,
                AnnouncementTemplateId = t.AnnouncementTemplateId != null ? t.AnnouncementTemplateId : 0,
                DirectPurchaseCommitteeMemberId = t.DirectPurchaseMemberId,
                IsLowBudgetDirectPurchase = t.IsLowBudgetDirectPurchase
            }).FirstOrDefaultAsync();
            if (tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement)
            {
                tender.TenderAgreementAgencyIDs = await _context.TenderAgreementAgencies.Where(x => x.TenderId == tenderId).Select(x => x.AgencyCode).ToListAsync();
            }
            return tender;
        }

        public async Task<Tender> GetTenderWithAreas(int tenderId, int branchId)
        {
            var entities = await _context.Tenders.Include(x => x.TenderAreas).Include(x => x.TenderCountries)
            .Where(t => t.IsActive == true && t.TenderId == tenderId /*&& t.BranchId == branchId*/).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<TenderInformationModel> GetTenderById(int tenderId)
        {
            var entities = await _context.Tenders
            .Where(t => t.IsActive == true && t.TenderId == tenderId)
            .Select(t => new TenderInformationModel
            {
                TenderName = t.TenderName,
                TenderTypeId = t.TenderTypeId,
                TenderNumber = t.TenderNumber,
                ReferenceNumber = t.ReferenceNumber,
                TechnicalOrganizationId = t.TechnicalOrganizationId,
                LastEnqueriesDate = t.LastEnqueriesDate,
                TenderTypeName = t.TenderType.NameAr,
                TenderIdString = Util.Encrypt(t.TenderId),
                TenderStatusIdString = Util.Encrypt(t.TenderStatusId)
            }).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> GetTenderAgencyCommunicationById(int tenderId)
        {
            var entities = await _context.Tenders.Include(t => t.AgencyCommunicationRequests).ThenInclude(f => f.Negotiations).Include(t => t.ChangeRequests)
                                  .Where(t => t.IsActive == true && t.TenderId == tenderId)
                                  .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> GetTenderAgencyCommunicationByIdForExtendDatesRequist(int tenderId)
        {
            var entities = await _context.Tenders
                .Include(t => t.AgencyCommunicationRequests)
                .Include(t => t.ChangeRequests)
                .Where(t => t.IsActive == true && t.TenderId == tenderId)
                .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> FindTenderDetailsByTenderId(int tenderId)
        {
            var entities = await _context.Tenders
            .Include(t => t.Invitations)
            .Include(t => t.ConditionsBooklets)
            .Include(t => t.Status)
            .Include(t => t.Agency)
            .Include(t => t.TenderType)
            .Include(t => t.InvitationType)
            .Include(t => t.OffersCheckingCommittee)
            .Include(t => t.OffersOpeningCommittee)
            .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<bool> SupplierExistsInInvitationsOrConditionsBookletsByTenderIdAndCR(int tenderId, string CR)
        {
            return await _context.Tenders
                .AnyAsync(t => t.TenderId == tenderId && t.IsActive == true
                && (t.ConditionsBooklets.Any(c => c.CommericalRegisterNo == CR && c.IsActive == true) || (t.Invitations.Any(i => i.TenderId == tenderId && i.IsActive == true && i.CommericalRegisterNo == CR && i.StatusId == (int)InvitationStatus.Approved)) || (t.Offers.Any(o => o.TenderId == tenderId && o.IsActive == true && o.CommericalRegisterNo == CR && o.OfferStatusId != (int)Enums.OfferStatus.Canceled))));
        }

        public async Task<bool> UserExistsInTechnicalCommitteeUsersByEnquiryIdAndUserId(int enquiryId, int userId)
        {
            var result = await _context.Enquiries
              .AnyAsync(e => e.EnquiryId == enquiryId && e.IsActive == true &&
              (e.Tender.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == userId && c.IsActive == true)));
            return result;
        }

        public async Task<bool> UserExistsInCommitteeUsersByEnquiryIdAndUserId(int enquiryId, int userId)
        {
            var result = await _context.Enquiries
               .AnyAsync(e => e.EnquiryId == enquiryId && e.IsActive == true &&
               (e.Tender.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == userId && c.IsActive == true) ||
               (e.JoinTechnicalCommittees.Any(i => i.Committee.CommitteeUsers.Any(c => c.UserProfileId == userId && c.IsActive == true) && i.IsActive == true))));
            return result;
        }

        public async Task<Tender> FindTenderInvitationOfferDetailsByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Tenders
                .Include(t => t.Offers)
                .Include(t => t.Invitations)
                .Include(t => t.ConditionsBooklets)
                .Include(t => t.TenderHistories)
                .Where(x => x.TenderId == tenderId).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> FindTenderInvitationByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Tenders
                .Include(t => t.Invitations)
                .Include(t => t.Branch)
                .Include(t => t.Agency)
                .Where(x => x.TenderId == tenderId).AsTracking().FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> FindTenderOfferDetailsByTenderIdDisplayOnly(int tenderId)
        {
            var entities = await _context.Tenders.FirstOrDefaultAsync();
            return entities;
        }

        public async Task<TenderOffersModel> GetTenderOfferDetailsForOppeningStage(int tenderId, int userId)
        {
            var oldflow = _rootConfiguration.OldFlow;
            var oldflowdate = _rootConfiguration.OldFlow.EndDate.ToDateTime();
            var tender = await _context.Tenders
                .Where(x => x.TenderId == tenderId)
                .Where(x =>
               (x.OffersOpeningCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && c.UserRole.Name == RoleNames.OffersOppeningManager)
                 &&
                 (
                 (x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.CurrentTender
                 &&
                 x.TenderStatusId == (int)Enums.TenderStatus.Approved
                 )
                 ||
                 (
                 x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedPending || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningPending

                 || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed
                 || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed
                 )

                 ))

                 ||
                  (x.OffersOpeningCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && c.UserRole.Name == RoleNames.OffersOppeningSecretary)
                 &&
                 (
                 (x.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && x.TenderTypeId != (int)Enums.TenderType.CurrentTender
                 &&
                 x.TenderStatusId == (int)Enums.TenderStatus.Approved
                 )
                 ||
                 (
                 x.TenderStatusId == (int)Enums.TenderStatus.OffersOppening || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppening
                 || x.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedRejected || x.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningRejected
                 || x.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
                )

                 ))
                )
                .Select(t => new TenderOffersModel
                {
                    CancelRequestId = t.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && c.RequestedByRoleName == RoleNames.OffersOppeningSecretary).OrderByDescending(c => c.TenderChangeRequestId).Select(c => c.TenderChangeRequestId).FirstOrDefault(),
                    TenderId = t.TenderId,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderName = t.TenderName,
                    InsideKSA = t.InsideKSA,
                    ExcutionPlace = t.InsideKSA != null ? (t.InsideKSA == true ? Resources.TenderResources.DisplayInputs.InsideKSA : Resources.TenderResources.DisplayInputs.OutsideKSA) : Resources.TenderResources.DisplayInputs.OutsideKSA,
                    TenderNumber = t.TenderNumber,
                    TenderRefrenceNumber = t.ReferenceNumber,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    EstimatedValue = t.EstimatedValue,
                    EstimatedValueText = t.EstimatedValue != null ? new ConvertNumberToText(t.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic() : "",
                    OffersOpeningDate = t.OffersOpeningDate,
                    IsTenderTechnicalChecked = t.Offers.Where(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received).All(o => o.OfferTechnicalEvaluationStatusId.HasValue),
                    IsTenderFinancialChecked = t.Offers.Where(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.OfferAcceptanceStatusId.HasValue),
                    OffersCount = t.Offers.Where(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)).Count(),
                    BuyersCount = t.ConditionsBooklets.Where(x => x.IsActive == true).Count() > 0 ?
                    (t.ConditionsBooklets.Where(x => x.IsActive == true && x.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid).Count()) :
                    (t.Invitations.Where(x => x.IsActive == true).Count() > 0 ?
                     t.Invitations.Where(x => x.IsActive == true && x.StatusId == (int)Enums.InvitationStatus.Approved).Count() : 0),
                    TenderAreaNameList = t.InsideKSA == true ? t.TenderAreas.Select(y => y.Area.NameAr).ToList() : t.TenderCountries.Select(y => y.Country.NameAr).ToList(),
                    InvitationsCount = t.Invitations.Where(inv => inv.StatusId == (int)Enums.InvitationStatus.Approved && inv.IsActive == true).Count(),
                    RejectionReason = t.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason,
                    IsValidToSubmit = t.Offers.Where(a => a.IsOpened != true && a.IsActive == true && a.OfferStatusId == (int)Enums.OfferStatus.Received).Count() == 0 ? true : false,
                    AgencyCode = t.AgencyCode,
                    OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                    CombinedCount = t.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true).SelectMany(s => s.Combined).Count(),
                    CombinedDetailsCount = t.Offers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true).SelectMany(s => s.Combined).Where(w => w.SupplierCombinedDetail != null).Count()
               ,
                    IsOldFlow = oldflow.isApplied ? t.IsOldFlow(oldflowdate) : false
                }).FirstOrDefaultAsync();
            tender.UnOpenCombinedSuppliersOffers = tender.CombinedCount - tender.CombinedDetailsCount;

            return tender;
        }

        public async Task<int> GetUnOpenOffersForCombinedSuppliers(int tenderId)
        {
            var Query = await _context.Offers.Include(s => s.Combined)
             .Where(x => x.TenderId == tenderId && x.OfferStatusId == (int)Enums.OfferStatus.Received)
           .SelectMany(s => s.Combined).Where(w => w.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).Select(d => d.Id).ToListAsync();
            var detailscount = await _context.SupplierCombinedDetails.Where(w => Query.ToList().Any(s => s == w.CombinedId)).CountAsync();
            return Query.Count - detailscount;
        }

        public async Task<TenderOffersModel> GetTenderOffersDetailsForOpenAwarding(int tenderId, int userId, string agencyCode)
        {
            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();
            var tender = await _context.Tenders
                .Include(t => t.TenderType).Include(p => p.TenderVersions).ThenInclude(f => f.Version)
                .Where(x => x.TenderId == tenderId && x.AgencyCode == agencyCode &&
                (x.OffersCheckingCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && c.UserRole.Name == RoleNames.OffersCheckSecretary) ||
                x.DirectPurchaseCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && c.UserRole.Name == RoleNames.OffersPurchaseSecretary) ||
                 (x.IsLowBudgetDirectPurchase == true && x.DirectPurchaseMemberId == userId) ||
                x.VROCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && c.UserRole.Name == RoleNames.OffersOpeningAndCheckSecretary))).FirstOrDefaultAsync();

            var tenderOffers = await _context.Offers.Where(o => o.TenderId == tenderId && o.IsActive == true).ToListAsync();
            var tenderInvitations = await _context.Invitations.Where(i => i.TenderId == tenderId && i.IsActive == true).ToListAsync();
            int ActivityVersionId = tender.ActivityVersionId;
            var conditionsBooklets = await _context.ConditionsBooklets.Where(c => c.IsActive == true && c.TenderId == tenderId).Include(c => c.BillInfo).ToListAsync();
            var isActivityContainSupply =
                (tender.IsTenderContainsTawreedTables.HasValue && tender.IsTenderContainsTawreedTables.Value)
                ||
(await _context.TenderActivities.AnyAsync(a => a.TenderId == tenderId && a.IsActive == true && (
           ActivityVersionId >= (int)Enums.ActivityVersions.Sprint7Activities
               ? a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralSupply)
               : a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials))

                ));
            //a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials));

            var result = new TenderOffersModel
            {
                TenderIdString = Util.Encrypt(tender.TenderId),
                TenderRefrenceNumber = tender.ReferenceNumber,
                TenderStatusId = tender.TenderStatusId,
                TenderTypeId = tender.TenderTypeId,
                TenderName = tender.TenderName,
                TenderTypeName = tender.TenderType.NameAr,
                TenderNumber = tender.TenderNumber,
                EstimatedValue = tender.EstimatedValue,
                IsLowBudgetDirectPurchase = tender.IsLowBudgetDirectPurchase,
                ContainSupply = tender.QuantityTableVersionId > (int)Enums.QuantityTableVersion.Version1 && isActivityContainSupply,
                NPCalcHaveBeenDone = tenderOffers.Any(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferWeightAfterCalcNPA != o.FinalPriceAfterDiscount),
                OffersCount = tenderOffers.Count(o => o.IsActive == true &&
                    tender.IsNewAwarding(newAwardingReleaseDate) ?
                (o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
                && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
                :
                (o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)),

                BuyersCount = conditionsBooklets.Any() ?
                    conditionsBooklets.Count(c => c.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid) :
                    (tenderInvitations.Any(x => x.IsActive == true) ?
                     tenderInvitations.Count(x => x.IsActive == true && x.StatusId == (int)Enums.InvitationStatus.Approved) : 0)
            };
            if (result.EstimatedValue != null)
            {
                ConvertNumberToText obj = new ConvertNumberToText(result.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                result.EstimatedValueText = obj.ConvertToArabic();
            }
            return result;
        }
        public async Task<TenderOffersModel> GetTenderAwardingDetailsByTenderId(int tenderId, int userId, string agencyCode)
        {

            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();
            var days = int.Parse(_rootConfiguration.AwardingConfiguration.QualifationsValidityDays);
            DateTime hayzNafazDate = _rootConfiguration.OldFlow.EndDate.ToDateTime();
            var user = _httpContextAccessor.HttpContext.User;

            var tender = await _context.Tenders
                .Include(t => t.ChangeRequests)
                .Include(t => t.TenderType).Include(t => t.Invitations)
                .Where(x => x.TenderId == tenderId && x.AgencyCode == agencyCode &&
              (x.OffersCheckingCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRole.Name == RoleNames.OffersCheckManager || c.UserRole.Name == RoleNames.OffersCheckSecretary)) ||
              x.DirectPurchaseCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRole.Name == RoleNames.OffersPurchaseManager || c.UserRole.Name == RoleNames.OffersPurchaseSecretary)) ||
              x.VROCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRole.Name == RoleNames.OffersOpeningAndCheckSecretary || c.UserRole.Name == RoleNames.OffersOpeningAndCheckManager)) ||
              (x.IsLowBudgetDirectPurchase == true && x.DirectPurchaseMemberId == userId)
              || user.IsInRole(RoleNames.ApproveTenderAward))).FirstOrDefaultAsync();

            var tenderOffers = await _context.Offers.Where(o => o.TenderId == tenderId && o.IsActive == true).ToListAsync();
            var conditionsBooklets = await _context.ConditionsBooklets.Where(c => c.IsActive == true && c.TenderId == tenderId).Include(c => c.BillInfo).ToListAsync();
            var activityid = await GetCurrentTenderActivityVersion(tenderId);
            var preQualification = await _context.Tenders.Where(q => q.TenderId == tender.PreQualificationId && q.IsActive == true).Include(q => q.QualificationFinalResults).FirstOrDefaultAsync();

            //  isActivityContainSupply = await _context.TenderActivities.AnyAsync(a => a.TenderId == tenderId && a.IsActive == true && a.Activity.ActivityTemplateVersions.Any(s => s.TemplateId == (int)TenderActivityTamplate.GeneralMaterials));
            var isActivityContainSupply =
                       (tender.IsTenderContainsTawreedTables.HasValue && tender.IsTenderContainsTawreedTables.Value)
                       ||
       (await _context.TenderActivities.AnyAsync(a => a.TenderId == tenderId && a.IsActive == true &&


                   (
                   activityid >= (int)Enums.ActivityVersions.Sprint7Activities
                       ? a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralSupply)
                       : a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials))

                       ));

            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwarding).ToListAsync();
            var preQualificationHistories = await _context.TenderHistories.Where(h => h.TenderId == tender.PreQualificationId).ToListAsync();
            var versionId = await GetCurrentTenderActivityVersion(tenderId);

            tender.SetTenderHistories(tenderHistories);

            if (preQualificationHistories.Any())
            {
                preQualification.SetTenderHistories(preQualificationHistories);
            }

            var result = new TenderOffersModel
            {
                VersionId = versionId,
                CancelRequestId = tender.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && (c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary || c.RequestedByRoleName == RoleNames.OffersCheckSecretary || c.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary)).OrderByDescending(c => c.TenderChangeRequestId).Select(c => c.TenderChangeRequestId).FirstOrDefault(),

                TenderIdString = Util.Encrypt(tender.TenderId),
                TenderRefrenceNumber = tender.ReferenceNumber,
                TenderStatusId = tender.TenderStatusId,
                TenderTypeId = tender.TenderTypeId,
                TenderId = tender.TenderId,
                TenderName = tender.TenderName,
                TenderNumber = tender.TenderNumber,
                EstimatedValue = tender.EstimatedValue,
                NumberOfWinners = tender.NumberOfWinners,
                BonusValue = tender.BonusValue,
                TenderAwardingType = tender.TenderAwardingType,
                HasGuarantee = tender.HasGuarantee,
                AwardingMonths = tender.AwardingMonths,
                AwardingDiscountPercentage = tender.AwardingDiscountPercentage,
                IsShowAwardingDiscountPercentage = (versionId < (int)Enums.ActivityVersions.Sprint7Activities && (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.Competition)),
                FinalGuaranteeDeliveryAddress = tender.FinalGuaranteeDeliveryAddress,
                AwardingReportFileNameId = tender.AwardingReportFileNameId,
                AwardingReportFileName = tender.AwardingReportFileName,
                IsOldTender = tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects,
                DirectPurchaseReasonId = tender.ReasonForPurchaseTenderTypeId,
                IsLowBudgetDirectPurchase = tender.IsLowBudgetDirectPurchase,
                DirectPurchaseMemberId = tender.DirectPurchaseMemberId,
                AwardingStoppingPeriod = GetAwardingStoppingPeriod(tender, versionId),

                postQualificationTenderIdString = Util.Encrypt(tender.PreQualificationId != null ? tender.PreQualificationId.Value : tender.TenderId),
                ContainSupply = tender.QuantityTableVersionId > (int)Enums.QuantityTableVersion.Version1 && isActivityContainSupply,

                IsPreqaulificationExpired = preQualification != null ? (preQualificationHistories.FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed) != null &&
                            (DateTime.Now - preQualificationHistories.FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed).CreatedAt).TotalDays > days ? true : false) : false,

                IsPasssPreqaulification = preQualification != null ? (preQualification.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded) ? true : false) : false,

                CanApproveInitially = tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase && user.UserId() == tender.DirectPurchaseMemberId && tender.IsLowBudgetDirectPurchase == true,
                CanInsertAwardingData = user.IsInRole(RoleNames.OffersCheckSecretary) || user.IsInRole(RoleNames.OffersPurchaseSecretary) || user.IsInRole(RoleNames.OffersOpeningAndCheckSecretary) || (user.IsInRole(RoleNames.OffersPurchaseManager) && user.UserId() == tender.DirectPurchaseMemberId && tender.IsLowBudgetDirectPurchase == true),
                IsTenderHasStopPeriod = ((tender.TenderTypeId == (int)Enums.TenderType.Competition || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) && tender.SubmitionDate < hayzNafazDate)
                                || (tender.TenderTypeId != (int)Enums.TenderType.CurrentTender && tender.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase && tender.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects
                                    && tender.TenderTypeId != (int)Enums.TenderType.Competition && tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase),

                IsTenderHasAwardingValue = tenderOffers.Any(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && (o.TotalOfferAwardingValue != null || o.PartialOfferAwardingValue != null)),
                NPCalcHaveBeenDone = tenderOffers.Any(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferWeightAfterCalcNPA != o.FinalPriceAfterDiscount),
                OffersCount = tenderOffers.Count(o =>
                              tender.IsNewAwarding(newAwardingReleaseDate)
                 ? (o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
                && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
                :
                (o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)),


                BuyersCount = conditionsBooklets.Any() ?
                    conditionsBooklets.Count(c => c.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid) :
                    (tender.Invitations.Any(i => i.IsActive == true) ?
                     tender.Invitations.Count(i => i.IsActive == true && i.StatusId == (int)Enums.InvitationStatus.Approved) : 0)
            };

            if (result.EstimatedValue != null)
            {
                ConvertNumberToText obj = new ConvertNumberToText(result.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                result.EstimatedValueText = obj.ConvertToArabic();
            }

            if (result.CanInsertAwardingData && (result.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected || result.TenderStatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected || result.TenderStatusId == (int)Enums.TenderStatus.BackForAwardingFromPlaint))
            {
                var tenderHistoryList = await _context.TenderHistories.Where(h => h.TenderId == tenderId).ToListAsync();
                result.RejectionReason = CheckTenderRejectReason(tender.TenderStatusId, tenderHistoryList);
            }
            return result;
        }

        private int? GetAwardingStoppingPeriod(Tender tender, int versionId)
        {
            int? _awardingStoppingPeriod = 0;
            if (versionId >= (int)Enums.ActivityVersions.Sprint7Activities)
            {
                _awardingStoppingPeriod = tender.AwardingStoppingPeriod;
            }
            else
            {
                _awardingStoppingPeriod = tender.AwardingStoppingPeriod > 0 ? tender.AwardingStoppingPeriod : 5;
            }
            return _awardingStoppingPeriod;
        }

        private static string CheckTenderRejectReason(int tenderStatusId, List<TenderHistory> tenderHistories)
        {
            switch (tenderStatusId)
            {
                case (int)Enums.TenderStatus.Established:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.Rejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.Rejected).RejectionReason;
                        break;
                    }
                case (int)Enums.TenderStatus.OffersOppening:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.OffersOppenedRejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersOppenedRejected).RejectionReason;
                        break;
                    }
                case (int)Enums.TenderStatus.OffersOppenedConfirmed:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected).RejectionReason;
                        break;
                    }
                case (int)Enums.TenderStatus.OffersAwardedRejected:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersAwardedRejected).RejectionReason;
                        break;
                    }
                case (int)Enums.TenderStatus.OffersInitialAwardedRejected:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersInitialAwardedRejected).RejectionReason;
                        break;
                    }
                case (int)Enums.TenderStatus.BackForAwardingFromPlaint:
                    {
                        if (tenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.BackForAwardingFromPlaint))
                            return tenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.BackForAwardingFromPlaint).RejectionReason;
                        break;
                    }
                default:
                    return "";
            }
            return "";
        }
        public async Task<Tender> FindTenderRelationsByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var tenderRelationsEntity = await _context.Tenders.AsTracking()
                    .Include(t => t.TenderActivities)
                    .Include(t => t.TenderConstructionWorks)
                    .Include(t => t.TenderMaintenanceRunnigWorks)
                    .Include(t => t.TenderCountries)
                    .Include(t => t.TenderAreas)
                    .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderRelationsEntity;
        }

        public async Task<TenderRelationsModel> FindTenderRelationsByTenderIdAfterUpdateDates(int tenderId)
        {
            var tenderActivitiesIds = await _context.TenderActivities.Where(x => x.TenderId == tenderId).Select(x => x.ActivityId).ToListAsync();
            var tenderConstructionWorksIds = await _context.TenderConstructionWorks.Where(x => x.TenderId == tenderId).Select(x => x.ConstructionWorkId).ToListAsync();
            var tenderMaintenanceRunnigWorksIds = await _context.TenderMentainanceRunnigWorks.Where(x => x.TenderId == tenderId).Select(x => x.MaintenanceRunningWorkId).ToListAsync();
            var tenderCountriesIds = await _context.TenderCountries.Where(x => x.TenderId == tenderId).Select(x => x.CountryId).ToListAsync();
            var tenderAreasIds = await _context.TenderAreas.Where(x => x.TenderId == tenderId).Select(x => x.AreaId).ToListAsync();

            var tenderRelationsModel = await _context.Tenders
                .Where(t => t.TenderId == tenderId)
                .Select(x => new TenderRelationsModel
                {
                    TenderAreaIDs = tenderAreasIds,
                    TenderCountriesIDs = tenderCountriesIds,
                    TenderActivitieIDs = tenderActivitiesIds,
                    TenderConstructionWorkIDs = tenderConstructionWorksIds,
                    TenderMentainanceRunnigWorkIDs = tenderMaintenanceRunnigWorksIds,
                }).FirstOrDefaultAsync();
            return tenderRelationsModel;
        }

        public async Task<Tender> FindTenderRelationsWithoutQuantityTables(int tenderId)
        {

            var tenderActivities = await _context.TenderActivities.Where(x => x.TenderId == tenderId).ToListAsync();
            var tenderConstructionWorks = await _context.TenderConstructionWorks.Where(x => x.TenderId == tenderId).ToListAsync();
            var tenderMaintenanceRunnigWorks = await _context.TenderMentainanceRunnigWorks.Where(x => x.TenderId == tenderId).ToListAsync();
            var tenderCountries = await _context.TenderCountries.Where(x => x.TenderId == tenderId).ToListAsync();
            var tenderAreas = await _context.TenderAreas.Where(x => x.TenderId == tenderId).ToListAsync();

            var tender = await _context.Tenders
                   .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();

            tender.SetTenderRelations(tenderActivities, tenderAreas, tenderCountries, tenderConstructionWorks, tenderMaintenanceRunnigWorks);
            return tender;
        }


        public async Task<TenderRelationsModel> FindTenderRelationsModelByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());

            var tenderActivitiesIds = await _context.TenderActivities.Where(x => x.TenderId == tenderId).Select(x => x.ActivityId).ToListAsync();
            var tenderConstructionWorksIds = await _context.TenderConstructionWorks.Where(x => x.TenderId == tenderId).Select(x => x.ConstructionWorkId).ToListAsync();
            var tenderMaintenanceRunnigWorksIds = await _context.TenderMentainanceRunnigWorks.Where(x => x.TenderId == tenderId).Select(x => x.MaintenanceRunningWorkId).ToListAsync();
            var tenderCountriesIds = await _context.TenderCountries.Where(x => x.TenderId == tenderId).Select(x => x.CountryId).ToListAsync();
            var tenderAreasIds = await _context.TenderAreas.Where(x => x.TenderId == tenderId).Select(x => x.AreaId).ToListAsync();

            var tenderRelationsEntity = await _context.Tenders
                    .Where(t => t.TenderId == tenderId)
                    .Select(t => new TenderRelationsModel
                    {
                        TenderName = t.TenderName,
                        TenderNumber = t.TenderNumber,
                        AgencyCode = t.AgencyCode,
                        TenderIdString = Util.Encrypt(t.TenderId),
                        TenderId = t.TenderId,
                        TenderTypeId = t.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed && t.TenderTypeId == (int)Enums.TenderType.FirstStageTender ? (int)Enums.TenderType.SecondStageTender : t.TenderTypeId,
                        InsideKSA = t.InsideKSA,
                        TenderAreaIDs = tenderAreasIds,
                        TenderCountriesIDs = tenderCountriesIds,
                        TenderActivitieIDs = tenderActivitiesIds,
                        TenderConstructionWorkIDs = tenderConstructionWorksIds,
                        TenderMentainanceRunnigWorkIDs = tenderMaintenanceRunnigWorksIds,
                        Details = t.Details,
                        ReferenceNumber = t.ReferenceNumber,
                        ActivityDescription = t.ActivityDescription,
                        PreQualificationId = t.PreQualificationId,
                        InvitationTypeId = t.InvitationTypeId,
                        OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                        TenderCreatedByTypeId = t.CreatedByTypeId,
                        TemplateYears = t.TemplateYears,
                        BranchId = t.BranchId,
                        QuantityTableVersionId = t.QuantityTableVersionId,
                        ActivityVersionId = t.TenderVersions.FirstOrDefault(tv => tv.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity).VersionId,
                        IsTawreed = t.IsTenderContainsTawreedTables.HasValue && t.IsTenderContainsTawreedTables.Value == true ? true : false,
                    }).FirstOrDefaultAsync();

            return tenderRelationsEntity;
        }

        private async Task<Tender> FindTenderActivitiesByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var tender = await _context.Tenders
                .Include(t => t.TenderActivities)
                .ThenInclude(tt => tt.Activity).ThenInclude(d => d.ActivityTemplateVersions)
                .Where(t => t.TenderId == tenderId && t.TenderActivities.Any(a => a.IsActive == true)).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<Tender> FindTenderForApproval(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var tender = await _context.Tenders
                .Include(x => x.TenderUnitAssigns)
                .Include(x => x.Agency)
                .ThenInclude(x => x.Branches)
                .Include(x => x.Invitations)
                .Include(x => x.UnRegisteredSuppliersInvitation)
                .Include(x => x.TenderActivities)
                .ThenInclude(x => x.Activity)
                .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<Tender> FindTenderForApprove(int tenderId)
        {
            var tender = await _context.Tenders
                .Include(t => t.Agency).Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();

            var unitAssign = await _context.TenderUnitAssigns.Where(i => i.TenderId == tenderId).ToListAsync();
            var invitations = await _context.Invitations.Where(c => c.TenderId == tenderId && c.IsActive == true && c.StatusId == (int)InvitationStatus.ToBeSent).ToListAsync();
            var unRegisteredInvitations = await _context.UnRegisteredSuppliersInvitation.Where(c => c.TenderId == tenderId && c.IsActive == true && c.InvitationStatusId == (int)InvitationStatus.ToBeSent).ToListAsync();
            tender.Invitations.AddRange(invitations);
            tender.UnRegisteredSuppliersInvitation.AddRange(unRegisteredInvitations);
            tender.TenderUnitAssigns.AddRange(unitAssign);
            //tender.SetTenderLocalContent();
            return tender;
        }

        public async Task<bool> IsFrameworkTenderCheckValid(Tender tender)
        {
            string unitAgencyCode = _rootConfiguration.UnitAgencyCodeConfiguration.UnitAgencyCode;
            var activities = await _context.TenderActivities.Where(a => a.TenderId == tender.TenderId && a.IsActive == true).Select(a => a.ActivityId).ToListAsync();
            var result = await _context.Tenders
                .AnyAsync(t => t.IsActive == true &&
                t.AgencyCode == unitAgencyCode &&
                t.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed &&
                t.TenderAgreementAgencies.Any(a => a.AgencyCode == tender.AgencyCode) &&
                t.TenderActivities.Any(a => activities.Contains(a.ActivityId)));
            return result;
        }

        public async Task<bool> IsTenderHasLocalContent(DateTime CreatedDate)
        {
            var isTenderHasLocalContent = await _context.ConfigurationSettings.AnyAsync(c => c.Date <= CreatedDate && c.Id == (int)Enums.ConfigurationSetting.LocalContent);
            return isTenderHasLocalContent;
        }
        public async Task<List<int>> GetTenderLocalContentMechanism(int tenderLocalContentId)
        {
            var localContentMechanism = await _context.LocalContentMechanism.Where(x => x.TenderLocalContentId == tenderLocalContentId).Select(x => x.LocalContentMechanismPreferenceId).ToListAsync();
            return localContentMechanism;
        }

        public async Task<TenderLocalContentValuesViewModel> GetTenderLocalContenetValuesByTenderId(int tenderId)
        {
            var localContentSettings = await _context.LocalContentSettings.FirstOrDefaultAsync();
            var tenderActivityId = await GetCurrentTenderActivityVersion(tenderId);

            var tender = await _context.Tenders
                .Where(t => t.TenderId == tenderId)
                .Select(x => new TenderLocalContentValuesViewModel
                {
                    IsTenderContainsTawreedTables = (bool)x.IsTenderContainsTawreedTables,
                    QuantityTableVersionId = x.QuantityTableVersionId,
                    ContainsSupply = tenderActivityId >= (int)Enums.ActivityVersions.Sprint7Activities
                        ? x.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralSupply))
                        : x.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials)),

                    MinimumBaseline = x.TenderLocalContent.MinimumBaseline,
                    MinimumPercentageLocalContentTarget = x.TenderLocalContent.MinimumPercentageLocalContentTarget,
                    NationalProductPercentage = x.TenderLocalContentId != null ? (x.TenderLocalContent.NationalProductPercentage != null ? x.TenderLocalContent.NationalProductPercentage : localContentSettings.NationalProductPercentage) : x.NationalProductPercentage != null ? x.NationalProductPercentage : localContentSettings.NationalProductPercentage,

                    LocalContentMaximumPercentage = x.TenderLocalContent.LocalContentMaximumPercentage != null ? x.TenderLocalContent.LocalContentMaximumPercentage : localContentSettings.LocalContentMaximumPercentage,
                    PriceWeightAfterAdjustment = x.TenderLocalContent.PriceWeightAfterAdjustment != null ? x.TenderLocalContent.PriceWeightAfterAdjustment : localContentSettings.PriceWeightAfterAdjustment,
                    LocalContentWeightAndFinancialMarket = x.TenderLocalContent.LocalContentWeightAndFinancialMarket != null ? x.TenderLocalContent.LocalContentWeightAndFinancialMarket : localContentSettings.LocalContentWeightAndFinancialMarket,
                    BaselineWeight = x.TenderLocalContent.BaselineWeight != null ? x.TenderLocalContent.BaselineWeight : 50,
                    LocalContentTargetWeight = x.TenderLocalContent.LocalContentTargetWeight != null ? x.TenderLocalContent.LocalContentTargetWeight : 50,
                    FinancialMarketListedWeight = x.TenderLocalContent.FinancialMarketListedWeight != null ? x.TenderLocalContent.FinancialMarketListedWeight : 5,
                    TenderLocalContentId = x.TenderLocalContentId,
                    CreatedDate = x.CreatedAt,
                    LocalContentMechanismIds = new List<int>()
                }).FirstOrDefaultAsync();
            if (!tender.IsOldTender)
            {
                tender.IsTenderNewWithLocalContent = await IsTenderHasLocalContent(tender.CreatedDate);
                if (tender.IsTenderNewWithLocalContent && tender.TenderLocalContentId != null)
                {
                    tender.LocalContentMechanismIds = await GetTenderLocalContentMechanism((int)tender.TenderLocalContentId);
                }

                tender.ConShowNationalProduct = tender.ContainsSupply || tender.IsTenderContainsTawreedTables || (tender.IsTenderNewWithLocalContent && tender.LocalContentMechanismIds.Contains((int)Enums.LocalContentMechanismPerfermance.ThePricePreferenceMechanismNationalProduct));

            }
            return tender;
        }
        public async Task<LocalContentDetailsViewModel> GetLocalContentDetailsForSupplierByTenderId(int tenderId)
        {
            var tender = await _context.Tenders
                .Where(t => t.TenderId == tenderId)
                .Select(x => new LocalContentDetailsViewModel
                {

                    MinimumBaseline = x.TenderLocalContent.MinimumBaseline,
                    MinimumPercentageLocalContentTarget = x.TenderLocalContent.MinimumPercentageLocalContentTarget,


                }).FirstOrDefaultAsync();

            return tender;
        }
        public async Task<TenderRelationsModel> FindRelationsDetailsByTenderId(int tenderId)
        {

            var tenderActivityId = await GetCurrentTenderActivityVersion(tenderId);
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var TenderActivities = await _context.TenderActivities.Where(e => e.IsActive == true && e.TenderId == tenderId).Select(c => new ActivityModel
            {
                ActivityId = c.Activity.ActivityId,
                Name = _httpContextAccessor.HttpContext.IsArabic() ? c.Activity.NameAr : c.Activity.NameEn,
                SubActivities = c.Activity.SubActivities.Select(s => new ActivityModel
                {
                    ActivityId = s.ActivityId,
                    Name = _httpContextAccessor.HttpContext.IsArabic() ? s.NameAr : s.NameEn
                }).ToList()
            }).ToListAsync();
            var TenderConstructionWorks = await _context.TenderConstructionWorks.Where(e => e.IsActive == true && e.TenderId == tenderId).Select(c => new ConstructionWorkModel
            {
                ConstructionWorkId = c.ConstructionWork.ConstructionWorkId,
                Name = _httpContextAccessor.HttpContext.IsArabic() ? c.ConstructionWork.NameAr : c.ConstructionWork.NameEn,
                SubWorks = c.ConstructionWork.SubWorks.Select(s => new ConstructionWorkModel
                {
                    ConstructionWorkId = s.ConstructionWorkId,
                    Name = _httpContextAccessor.HttpContext.IsArabic() ? s.NameAr : s.NameEn
                }).ToList()
            }).ToListAsync();
            var TenderMaintenanceRunnigWorks = await _context.TenderMentainanceRunnigWorks.Where(e => e.IsActive == true && e.TenderId == tenderId)
                .Select(c => new MaintenanceRunningWorkModel
                {
                    MaintenanceRunningWorkId = c.MaintenanceRunningWork.MaintenanceRunningWorkId,
                    Name = _httpContextAccessor.HttpContext.IsArabic() ? c.MaintenanceRunningWork.NameAr : c.MaintenanceRunningWork.NameEn
                }).ToListAsync();


            var localContentSettings = await _context.LocalContentSettings.FirstOrDefaultAsync();// .Get();

            var model = await _context.Tenders.Include(x => x.TenderLocalContent)
                .Where(t => t.TenderId == tenderId)
                .Select(x => new TenderRelationsModel
                {
                    TenderName = x.TenderName,
                    TenderNumber = x.TenderNumber,
                    InsideKSA = x.InsideKSA,
                    TenderTypeId = x.TenderTypeId,
                    TenderStatusId = x.TenderStatusId,
                    QuantityTableVersionId = x.QuantityTableVersionId,
                    ContainsSupply = x.QuantityTableVersionId > (int)Enums.QuantityTableVersion.Version1
                    && (x.IsTenderContainsTawreedTables.HasValue && x.IsTenderContainsTawreedTables.Value)
                    || (tenderActivityId >= (int)Enums.ActivityVersions.Sprint7Activities
                    ? x.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralSupply))
                    : x.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials))
                    ),
                    Details = x.Details,
                    NationalProductPercentageForUnit = x.NationalProductPercentage ?? 0,
                    IsTawreed = (bool)x.IsTenderContainsTawreedTables,
                    MinimumBaseline = x.TenderLocalContent.MinimumBaseline,
                    MinimumPercentageLocalContentTarget = x.TenderLocalContent.MinimumPercentageLocalContentTarget,
                    NationalProductPercentage = x.TenderLocalContentId != null ? (x.TenderLocalContent.NationalProductPercentage != null ? x.TenderLocalContent.NationalProductPercentage : localContentSettings.NationalProductPercentage) : x.NationalProductPercentage != null ? x.NationalProductPercentage : localContentSettings.NationalProductPercentage,
                    //NationalProductPercentageLocalContent = x.TenderLocalContent.NationalProductPercentage != null ? x.TenderLocalContent.NationalProductPercentage : localContentSettings.NationalProductPercentage,
                    LocalContentMaximumPercentage = x.TenderLocalContent.LocalContentMaximumPercentage != null ? x.TenderLocalContent.LocalContentMaximumPercentage : localContentSettings.LocalContentMaximumPercentage,
                    PriceWeightAfterAdjustment = x.TenderLocalContent.PriceWeightAfterAdjustment != null ? x.TenderLocalContent.PriceWeightAfterAdjustment : localContentSettings.PriceWeightAfterAdjustment,
                    LocalContentWeightAndFinancialMarket = x.TenderLocalContent.LocalContentWeightAndFinancialMarket != null ? x.TenderLocalContent.LocalContentWeightAndFinancialMarket : localContentSettings.LocalContentWeightAndFinancialMarket,
                    BaselineWeight = x.TenderLocalContent.BaselineWeight != null ? x.TenderLocalContent.BaselineWeight : 50,
                    LocalContentTargetWeight = x.TenderLocalContent.LocalContentTargetWeight != null ? x.TenderLocalContent.LocalContentTargetWeight : 50,
                    FinancialMarketListedWeight = x.TenderLocalContent.FinancialMarketListedWeight != null ? x.TenderLocalContent.FinancialMarketListedWeight : 5,
                    TenderLocalContentId = x.TenderLocalContentId,
                    CreatedDate = x.CreatedAt,
                    LocalContentMechanismIds = new List<int>()
                }).FirstOrDefaultAsync();

            model.ActivityVersionId = tenderActivityId;
            if (!model.IsOldTender)
            {
                model.IsTenderNewWithLocalContent = await IsTenderHasLocalContent(model.CreatedDate);
                if (model.IsTenderNewWithLocalContent && model.TenderLocalContentId != null)
                {
                    model.LocalContentMechanismIds = await GetTenderLocalContentMechanism((int)model.TenderLocalContentId);
                }
                //if (model.TenderLocalContentId != null)
                //{

                //    model.NationalProductPercentage = model.NationalProductPercentageLocalContent;

                //}
                //else
                //{
                //    model.NationalProductPercentage =
                //}
            }
            if (model.InsideKSA == true)
            {
                model.Areas = await _context.TenderAreas.Where(e => e.IsActive == true && e.TenderId == tenderId).Select(c => new LookupModel
                {
                    Id = c.Area.AreaId,
                    Name = c.Area.NameAr
                }).ToListAsync();
            }
            else
            {
                model.Countries = await _context.TenderCountries.Where(e => e.IsActive == true && e.TenderId == tenderId).Select(c => new LookupModel
                {
                    Id = c.Country.CountryId,
                    Name = _httpContextAccessor.HttpContext.IsArabic() ? c.Country.NameAr : c.Country.NameEn
                }).ToListAsync();
            }
            model.MaintenanceWorks = TenderMaintenanceRunnigWorks;
            model.Activities = TenderActivities;
            model.ConstructionWorks = TenderConstructionWorks;
            return model;
        }

        public async Task<Tender> FindTenderWithAttachments(int tenderId)
        {
            var entities = await _context.Tenders
                .Include(t => t.Attachments)
                .Where(t => t.TenderId == tenderId && t.IsActive == true)
                .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> FindTenderAttachmentsById(int tenderId, int branchId)
        {
            var entities = await _context.Tenders.Include(r => r.TenderVersions).ThenInclude(t => t.Version)
                .Include(c => c.ChangeRequests)
                .ThenInclude(a => a.AttachmentChanges)
                .Include(t => t.Attachments)
                .Include(t => t.Invitations)
                .Include(t => t.ConditionsBooklets)
                .ThenInclude(t => t.BillInfo)
                .Where(t => t.TenderId == tenderId)
                .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<AttachmentsModel> FindTenderAttachmentsModelById(int tenderId, int branchId)
        {
            var entities = await _context.Tenders
                    .WhereIf(branchId != 0, x => x.BranchId == branchId)
                    .Where(t => t.TenderId == tenderId)
                    .Select(t => new AttachmentsModel
                    {
                        BranchId = t.BranchId,
                        TenderName = t.TenderName,
                        TenderNumber = t.TenderNumber,
                        AgencyCode = t.AgencyCode,
                        TablesCount = t.TenderQuantityTables.Where(a => a.IsActive == true).Count(),
                        TenderID = t.TenderId,
                        TenderTypeId = t.TenderTypeId,
                        PreQualificationId = t.PreQualificationId,
                        TenderCreatedByTypeId = t.CreatedByTypeId,
                        InvitationTypeId = t.InvitationTypeId,
                        ReferenceNumber = t.ReferenceNumber,
                        TenderStatusId = t.TenderStatusId,
                        OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                        QuantityTableVersionId = t.QuantityTableVersionId,
                        TenderIDString = Util.Encrypt(t.TenderId),
                        BookletReferenceLst = t.Attachments.Where(a => a.AttachmentTypeId == (int)Enums.AttachmentType.TenderBookletAttachment).Select(at =>
                              "/Upload/GetFile/" + at.FileNetReferenceId + ":" + at.Name
                            ).ToList(),
                        AttachmentReferenceLst = t.Attachments.Where(a => a.AttachmentTypeId != (int)Enums.AttachmentType.TenderBookletAttachment).Select(at =>
                               "/Upload/GetFile/" + at.FileNetReferenceId + ":" + at.Name
                            ).ToList(),
                        BookletReference = "",
                        AttachmentReference = "",
                        AttachmentsChanges = t.ChangeRequests.Where(s => s.IsActive == true && s.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).Count() > 0 ?
                        t.ChangeRequests.Where(s => s.IsActive == true && s.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).FirstOrDefault().AttachmentChanges.Where(s => s.IsActive == true).Select(a => new TenderAttachmentsModelChanges
                        {
                            AttachmentId = a.TenderAttachmentId,
                            Name = a.Name,
                            FileNetReferenceId = a.FileNetReferenceId,
                            AttachmentTypeId = a.AttachmentTypeId,
                            DeletedAttachmentId = a.DeletedAttachmentId,
                        }).ToList() : new List<TenderAttachmentsModelChanges>()
                    })
                    .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> FindTenderAttachmentsStepById(int tenderId, int branchId)
        {
            var entities = await _context.Tenders
                    .Include(t => t.Attachments)
                    .WhereIf(branchId != 0, x => x.BranchId == branchId)
                    .Where(t => t.IsActive == true && t.TenderId == tenderId).AsTracking()
                    .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> FindTenderStatusByTenderId(int tenderId)
        {
            Tender tenderEntity = await _context.Tenders
                .Include(x => x.Status)
                .Include(x => x.ChangeRequests)
                .Include(x => x.TenderHistories)
                .ThenInclude(xx => xx.Action)
                .Where(tender => tender.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }
        public async Task<Tender> FindTenderForOpenCheckStageByTenderId(int tenderId)
        {
            Tender tenderEntity = await _context.Tenders
                .Include(x => x.Status)
                .Include(x => x.ChangeRequests)
                .Include(x => x.TenderHistories)
                .Where(tender => tender.TenderId == tenderId).SingleOrDefaultAsync();
            return tenderEntity;
        }
        public async Task<Tender> FindTenderFoAwardingStageByTenderId(int tenderId)
        {
            Tender tender = await _context.Tenders
                .Where(tender => tender.TenderId == tenderId).SingleOrDefaultAsync();
            return tender;
        }
        public async Task DeleteQTUsingStoredProcedure(int tenderId)
        {
            await _context.SP_DeleteTenderQuantityTableWithItem.FromSqlRaw($"SP_DeleteTenderQuantityTableWithItems {tenderId}").ToListAsync();
        }

        public async Task<Tender> FindTenderByIdForAwarding(int tenderId)
        {
            Tender tender = await _context.Tenders
               .Include(x => x.Offers)
               .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();

            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwarding).ToListAsync();
            tender.SetTenderHistories(tenderHistories);

            return tender;
        }
        public async Task<Tender> FindTenderForAwarding(int tenderId)
        {
            Tender tenderEntity = await _context.Tenders
                .Include(x => x.Offers)
                .Include(x => x.Status)
                .Include(x => x.ChangeRequests)
                .Include(x => x.PreQualification)
                .ThenInclude(x => x.QualificationFinalResults)
                .Include(x => x.PreQualification)
                .ThenInclude(x => x.TenderHistories)
                .Include(x => x.PostQualifications)
                .ThenInclude(x => x.TenderHistories)
                .Include(x => x.PostQualifications)
                .ThenInclude(x => x.QualificationFinalResults)
                .Where(tender => tender.TenderId == tenderId).SingleOrDefaultAsync();
            return tenderEntity;
        }
        public async Task<Tender> FindTenderWithStatusAndOffersByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tenderObject = await _context.Tenders
                .Include(a => a.AgencyCommunicationRequests)
                .Include(a => a.Agency)
                .Include(a => a.Branch)
                .Include(a => a.ChangeRequests)
                .Include(x => x.Status)
                .Include(x => x.Offers)

                .Include(t => t.TenderAreas)
                .Include(t => t.TenderCountries)
                .Where(tender => tender.TenderId == tenderId).SingleOrDefaultAsync();
            return tenderObject;
        }

        public async Task<Tender> FindTenderToApproveFinancialOpening(int tenderId)
        {
            Tender tenderObject = await _context.Tenders
               .Where(tender => tender.TenderId == tenderId).SingleOrDefaultAsync();
            return tenderObject;
        }
        public async Task<Tender> FindTenderForUpdateDates(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tenderObject = await _context.Tenders
                .Include(a => a.AgencyCommunicationRequests)
                .Include(a => a.Agency)
                .Include(a => a.Branch)
                .Include(a => a.ChangeRequests)
                .Where(tender => tender.TenderId == tenderId && tender.IsActive == true).SingleOrDefaultAsync();
            return tenderObject;
        }
        public async Task<Tender> FindTenderWithInvitationsBills(int tenderId)
        {
            Tender tenderObject = await _context.Tenders
                .Include(t => t.Invitations).ThenInclude(b => b.BillInfo)
                .Where(tender => tender.TenderId == tenderId && tender.IsActive == true).SingleOrDefaultAsync();
            return tenderObject;
        }
        public async Task<Tender> FindTenderWithConditionsBookletsBills(int tenderId)
        {
            Tender tenderObject = await _context.Tenders
                .Include(t => t.ConditionsBooklets).ThenInclude(b => b.BillInfo)
                .Where(tender => tender.TenderId == tenderId && tender.IsActive == true).SingleOrDefaultAsync();
            return tenderObject;
        }

        public async Task<Tender> FindTenderToApproveCancel(int tenderId)
        {
            Tender tenderObject = await _context.Tenders
                .Include(x => x.Offers)
                .Include(x => x.QualificationFinalResults)
                .Where(tender => tender.TenderId == tenderId).SingleOrDefaultAsync();
            return tenderObject;
        }

        public async Task<bool> IsTenderPurchasedBySupplier(int tenderId, string CR)
        {
            var result = await _context.ConditionsBooklets.Where(c => c.TenderId == tenderId && c.CommericalRegisterNo == CR).FirstOrDefaultAsync();
            return (result != null);
        }



        public async Task<List<Offer>> FindSuppliersOffersInAwardingStageByTenderId(int tenderId)
        {
            var offers = await _context.Offers.Where(x => x.TenderId == tenderId && x.IsActive == true
            && x.OfferStatusId == (int)Enums.OfferStatus.Received && x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)
            .Include(x => x.Supplier)
            .Include(x => x.OffersHistory)
            .ToListAsync();
            return offers;
        }

        public async Task<List<Offer>> GetFailedSuppliersOnFinancialEvaluation(int tenderId)
        {
            var offers = await _context.Offers.Include(o => o.OfferlocalContentDetails)
         .Where(x => x.TenderId == tenderId && x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received)
         .Where(o => o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.RejectedOffer)
         .ToListAsync();
            return offers;
        }
        public async Task<List<Offer>> GetFailedSuppliersOnTechnicalEvaluation(int tenderId)
        {
            var offers = await _context.Offers.Include(o => o.OfferlocalContentDetails)
            .Where(x => x.TenderId == tenderId && x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received)
            .Where(o => o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer)
            .ToListAsync();
            return offers;
        }

        public async Task<List<Offer>> FindNotAwardedSuppliersCauseExcludedReason(int tenderId)
        {
            var suppliersHasOffers = await _context.Offers
                .Where(x => x.TenderId == tenderId && x.IsActive == true && !String.IsNullOrEmpty(x.ExclusionReason)
                    && x.OfferStatusId == (int)Enums.OfferStatus.Received).Include(x => x.Supplier)
            .Include(x => x.OffersHistory).ToListAsync();
            return suppliersHasOffers;
        }
        public async Task<Tender> FindFavouriteSupplierTenderByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Tenders
                .Include(f => f.FavouriteSupplierTenders)
                .Where(t => t.TenderId == tenderId && t.IsActive == true).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<FavouriteSupplierTender> FindFavouriteSupplierTenderByTenderId(int tenderId, string cr)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.FavouriteSupplierTenders
                .Where(t => t.TenderId == tenderId && t.SupplierCRNumber == cr && t.IsActive == true).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> GetTenderForFramWorkReCreation(int tenderId)
        {
            var tender = await _context.Tenders.AsNoTracking()
                .Include(t => t.TenderAgreementAgencies)
                .Include(t => t.TenderAreas)
                .Include(t => t.TenderCountries)
                .Include(t => t.TenderActivities)
                .Include(t => t.TenderConstructionWorks)
                .Include(t => t.TenderMaintenanceRunnigWorks)
                .Include(t => t.Attachments)
                .Include(t => t.TenderConditionsTemplate)
                .ThenInclude(t => t.TenderConditionsTemplateMaterialInofmration)
                .Include(t => t.TenderConditionsTemplate)
                .ThenInclude(t => t.TemplateCertificates)
                .Include(t => t.TenderConditionsTemplate)
                .ThenInclude(t => t.TenderConditionsTemplateTechnicalDelrations)
                .Include(t => t.TenderConditionsTemplate)
                .ThenInclude(t => t.TenderConditionsTemplateTechnicalOutputs)
                .Where(t => t.TenderId == tenderId && t.IsActive == true).FirstOrDefaultAsync();

            tender.TenderQuantityTables.Clear();

            var qt = await _context.TenderQuantityTables.Include(i => i.QuantitiyItemsJson)
                .Where(x => x.IsActive == true && x.TenderId == tenderId).ToListAsync();

            tender.TenderQuantityTables.AddRange(qt);

            return tender;

        }

        public async Task<TenderAttachment> FindTenderAttachementByReferenceId(string referenceId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(referenceId), referenceId);
            var entity = await _context.TenderAttachments.Include(t => t.Tender)
                .Where(a => a.FileNetReferenceId == referenceId).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<TenderAttachmentChanges> FindTenderAttachementChangesByReferenceId(string referenceId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(referenceId), referenceId);
            var entity = await _context.AttachmentChanges
                .Where(a => a.FileNetReferenceId == referenceId).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<SupplierPreQualificationAttachment> GetQualificationAttachmentsByReferenceId(string referenceId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(referenceId), referenceId);
            var entity = await _context.SupplierPreQualificationAttachment
                .Where(a => a.IsActive == true && a.FileNetReferenceId == referenceId).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<List<TenderHistory>> FindTenderHistoriesByTenderId(int tenderId)
        {
            var tenderHistoryEntity = await _context.TenderHistories
                .Where(t => t.TenderId == tenderId)
                .OrderByDescending(t => t.TenderHistoryId).ToListAsync();
            return tenderHistoryEntity;
        }

        public async Task<InvitationStepModel> FindTenderDetailsByInvitationdAsync(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Tenders.Where(t => t.IsActive == true && t.TenderId == tenderId)
                .Select(t => new InvitationStepModel
                {
                    HasQualification = t.PreQualificationId != null ? true : false,
                    FirstStageId = t.TenderFirstStageId != null ? t.TenderFirstStageId : 0,
                    PreQualificationId = t.PreQualificationId != null ? t.PreQualificationId : 0,
                    HasAnnouncementTemplate = t.AnnouncementTemplateId != null && t.ReasonForLimitedTenderTypeId != (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers,
                    HasAnnouncementTemplateWithLimitedNumberReason = t.AnnouncementTemplateId != null && (t.ReasonForLimitedTenderTypeId == (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers || t.ReasonForPurchaseTenderTypeId == (int)Enums.ReasonForPurchaseTenderType.BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative),
                    AnnouncementTemplateId = t.AnnouncementTemplateId != null ? t.AnnouncementTemplateId : 0,
                    TenderId = t.TenderId,
                    TenderNumber = t.TenderNumber,
                    ReferenceNumber = t.ReferenceNumber,
                    TenderTypeName = t.TenderType.NameAr,
                    TenderName = t.TenderName,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    OffersCkeckingDate = t.OffersCheckingDate,
                    OffersOpeningDate = t.OffersOpeningDate,
                    AgencyName = t.Agency.NameArabic,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    TenderCreatedByTypeId = t.CreatedByTypeId,
                    InsideKSA = t.InsideKSA,
                    TenderAreas = t.TenderAreas.Select(a => new AreaModel
                    {
                        AreaId = a.AreaId,
                        NameAr = a.Area.NameAr,
                        NameEn = a.Area.NameEn
                    }).ToList()
                }).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<List<Invitation>> GetInvitation(List<string> commericalRegisterNos, int tenderId)
        {
            List<Invitation> invitations = new List<Invitation>();
            foreach (var item in commericalRegisterNos)
            {
                var invitation = await GetInvitation(item, tenderId);
                if (invitation != null)
                    invitations.Add(invitation);
            }
            return invitations;
        }

        public async Task<List<VacationsDate>> FindAllVacationDates()
        {
            List<VacationsDate> vacations = await _context.VacationsDates.ToListAsync();
            return vacations;
        }

        public async Task<List<AddressModel>> GetOfferOpeningAddresses(int branchId)
        {
            List<AddressModel> addresses = await _context.addresses.Where(a => a.AddressTypeId == (int)Enums.AddressType.OfferOpeningAddress && a.BranchId == branchId).Select(a => new AddressModel
            {
                AddressId = a.AddressId,
                AddressName = a.AddressName,
                AddressTypeId = a.AddressTypeId
            }).ToListAsync();
            return addresses;
        }

        public async Task<List<AddressModel>> GetAllAddresses()
        {
            List<AddressModel> addresses = await _context.addresses.Select(a => new AddressModel
            {
                AddressId = a.AddressId,
                AddressName = a.AddressName,
                AddressTypeId = a.AddressTypeId
            }).ToListAsync();
            return addresses;
        }

        public async Task<VerificationCode> FindVerificationCode(int userId, int typeId)
        {
            var entity = await _context.VerificationCode.Where(x => x.VerificationTypeId == typeId && x.UserId == userId).OrderByDescending(a => a.VerificationCodeId).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<bool> GetHasTendersByCommittee(int committeeId)
        {
            return await _context.Tenders
                .Where(x => x.TechnicalOrganizationId == committeeId || x.OffersOpeningCommitteeId == committeeId || x.OffersCheckingCommitteeId == committeeId
                || x.DirectPurchaseCommitteeId == committeeId)
                .AnyAsync();
        }

        public async Task<TenderDetialsReportModel> OpenTenderDetailsReport(int tenderId)
        {
            var result = await _context.Tenders
                 .Where(t => t.TenderId == tenderId && t.IsActive == true)
                 .Select(t => new TenderDetialsReportModel
                 {
                     TenderId = t.TenderId,
                     TenderNumber = t.TenderNumber,
                     TenderTypeId = t.TenderTypeId,
                     ReferenceNumber = t.ReferenceNumber,
                     TenderName = t.TenderName,
                     ConditionsBookletPrice = t.ConditionsBookletPrice,
                     AgnecyName = t.Agency.NameArabic,
                     TenderPurpose = t.Purpose,
                     ExcuationLocation = t.InsideKSA == true ? Resources.TenderResources.DisplayInputs.InsideKSA : Resources.TenderResources.DisplayInputs.OutsideKSA,
                     TenderConstructionWorks = t.TenderConstructionWorks.Select(w => w.ConstructionWork.NameAr).ToList(),
                     TenderMaintenanceRunnigWorks = t.TenderMaintenanceRunnigWorks.Select(m => m.MaintenanceRunningWork.NameAr).ToList(),
                     LastOfferApplyingDate = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "",
                     LastOfferApplyingDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "",
                     LastOpenOfferDate = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "لا يوجد",
                     LastOpenOfferDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "لا يوجد",
                     LastSupplierActionDate = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "لا يوجد",
                     LastSupplierActionDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "لا يوجد",
                     OpenOffersLocation = !string.IsNullOrEmpty(t.OffersOpeningAddress.AddressName) ? t.OffersOpeningAddress.AddressName : "لا يوجد",
                     SamplesDeliveryAddress = !string.IsNullOrEmpty(t.SamplesDeliveryAddress) ? t.SamplesDeliveryAddress : "لا يوجد",
                     OffersCheckingDateString = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "لا يوجد",
                     OffersCheckingDateHijriString = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "لا يوجد",
                     LastEnqueriesDateString = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "",
                     LastEnqueriesDateHijriString = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "",
                     LastOfferPresentationDateString = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "",
                     LastOfferPresentationDateHijriString = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "",
                     AwardingStoppingPeriod = t.AwardingStoppingPeriod,
                     AwardingExpectedDate = t.TenderDates.AwardingExpectedDate,
                     StartingBusinessOrServicesDate = t.TenderDates.StartingBusinessOrServicesDate
                 }).FirstOrDefaultAsync();

            result.VersionId = await GetCurrentTenderActivityVersion(tenderId);
            return result;
        }

        public async Task<TenderDetialsReportModel> OpenTenderDetailsReportForVisitor(int tenderId)
        {
            var result = await _context.Tenders
                 .Where(t => t.TenderId == tenderId && t.IsActive == true)
                .Where(t => t.SubmitionDate != null && t.InvitationTypeId == (int)Enums.InvitationType.Public)
                .Select(t => new TenderDetialsReportModel
                {
                    TenderId = t.TenderId,
                    TenderNumber = t.TenderNumber,
                    ReferenceNumber = t.ReferenceNumber,
                    TenderName = t.TenderName,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    AgnecyName = t.Agency.NameArabic,
                    TenderPurpose = t.Purpose,
                    ExcuationLocation = t.InsideKSA == true ? Resources.TenderResources.DisplayInputs.InsideKSA : Resources.TenderResources.DisplayInputs.OutsideKSA,
                    TenderConstructionWorks = t.TenderConstructionWorks.Select(w => w.ConstructionWork.NameAr).ToList(),
                    TenderMaintenanceRunnigWorks = t.TenderMaintenanceRunnigWorks.Select(m => m.MaintenanceRunningWork.NameAr).ToList(),
                    LastOfferApplyingDate = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "",
                    LastOfferApplyingDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "",
                    LastOpenOfferDate = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "لا يوجد",
                    LastOpenOfferDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "لا يوجد",
                    LastSupplierActionDate = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "لا يوجد",
                    LastSupplierActionDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "لا يوجد",
                    OpenOffersLocation = !string.IsNullOrEmpty(t.OffersOpeningAddress.AddressName) ? t.OffersOpeningAddress.AddressName : "لا يوجد",
                    SamplesDeliveryAddress = !string.IsNullOrEmpty(t.SamplesDeliveryAddress) ? t.SamplesDeliveryAddress : "لا يوجد",
                    OffersCheckingDateString = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "لا يوجد",
                    OffersCheckingDateHijriString = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "لا يوجد",
                    LastEnqueriesDateString = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "",
                    LastEnqueriesDateHijriString = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "",
                    LastOfferPresentationDateString = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) : "",
                    LastOfferPresentationDateHijriString = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("ar-SA")) : "",
                    AwardingStoppingPeriod = t.AwardingStoppingPeriod,
                    AwardingExpectedDate = t.TenderDates.AwardingExpectedDate,
                    StartingBusinessOrServicesDate = t.TenderDates.StartingBusinessOrServicesDate
                }).FirstOrDefaultAsync();
            result.VersionId = await GetCurrentTenderActivityVersion(tenderId);

            return result;
        }


        public async Task<AwardingReportModel> AwardingReport(int tenderId)
        {
            var result = await _context.Tenders.Include(x => x.Agency).Where(t => t.TenderId == tenderId && t.IsActive == true)
                .Select(t => new AwardingReportModel
                {
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    AgencyName = t.Agency.NameArabic,
                    AgencyCode = t.AgencyCode,
                    AwardingType = t.TenderAwardingType.Value ? Resources.TenderResources.DisplayInputs.FullAwarding : Resources.TenderResources.DisplayInputs.PartialAwarding,
                    OffersOpeningDate = t.OffersOpeningDate != null ? t.OffersOpeningDate.Value.ToString("dd/MM/yyyy") : null,
                    Suppliers = t.Offers.Where(x => x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received && x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(o => new Suppliers
                    {
                        CommericalRegisterName = o.Supplier.SelectedCrName.ToString(),
                        Amount = t.TenderAwardingType.Value ? o.TotalOfferAwardingValue.ToString() : o.PartialOfferAwardingValue.ToString(),
                        AwardingReasons = o.JustificationOfRecommendation
                    }).ToList(),
                }).FirstOrDefaultAsync();

            return result;

        }

        public async Task<OfferReportModel> OffersReport(int tenderId)
        {
            var query = await _context.Tenders.Include(t => t.Offers).Include(t => t.ConditionsBooklets).Include(t => t.Offers)
               .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            var result = new OfferReportModel
            {
                TenderName = query.TenderName,
                Agency = query.AgencyCode.ToString(),
                Companies = new List<Companies>()
            };
            foreach (var offer in query.Offers)
            {
                string buyingDate = "";
                var activeBooklets = query.ConditionsBooklets.Where(cb => cb.IsActive == true);
                if (activeBooklets.Any())
                {
                    var activeBooklet = activeBooklets.FirstOrDefault(b => b.CommericalRegisterNo == offer.CommericalRegisterNo);
                    var billInfo = await _context.BillInfos.Where(b => b.ConditionsBookletID == activeBooklet.ConditionsBookletId).OrderByDescending(b => b.CreatedAt).FirstOrDefaultAsync();
                    if (billInfo != null && billInfo.PurchaseDate.HasValue == true)
                    {
                        buyingDate = billInfo.PurchaseDate != null ? billInfo.PurchaseDate.Value.ToString("yyyy-MM-dd") : "";
                    }
                    else
                    {
                        buyingDate = activeBooklet.CreatedAt.ToString("yyyy-MM-dd");
                    }
                }
                else if (query.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
                {
                    var invitation = await _context.Invitations.Where(x => x.TenderId == tenderId && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.StatusId == (int)Enums.InvitationStatus.Approved && x.IsActive == true).OrderByDescending(x => x.SubmissionDate).FirstOrDefaultAsync();
                    buyingDate = invitation.SubmissionDate.HasValue ? invitation.SubmissionDate.Value.ToString("yyyy-MM-dd") : "";
                }
                var supplier = await _idmAppService.GetSupplierInfoByCR(offer.CommericalRegisterNo);
                var primaryAddress = supplier.Addresses.Any() ? supplier.Addresses.Where(o => o.IsPrimary == true).Any() ? supplier.Addresses.Where(o => o.IsPrimary == true).FirstOrDefault() : supplier.Addresses.FirstOrDefault() : null;
                result.Companies.Add(new Companies
                {
                    CommercialNumber = offer.CommericalRegisterNo,
                    BuyingDate = buyingDate,
                    CompanyName = supplier.supplierName,
                    Phone = !string.IsNullOrEmpty(supplier.Mobile) ? supplier.Mobile : (primaryAddress != null ? primaryAddress.PhoneNumber : ""),
                    Fax = !string.IsNullOrEmpty(supplier.Fax) ? supplier.Fax : (primaryAddress != null ? primaryAddress.FaxNumber : ""),
                    PostOffice = !string.IsNullOrEmpty(supplier.PostOffice) ? supplier.PostOffice : (primaryAddress != null ? primaryAddress.PostOffice : ""),
                    City = !string.IsNullOrEmpty(supplier.CityName) ? supplier.CityName : (primaryAddress != null ? primaryAddress?.City : ""),
                    PostalCode = !string.IsNullOrEmpty(supplier.PostalCode) ? supplier.PostalCode : (primaryAddress != null ? primaryAddress.PostalCode : "")
                });
            }
            return result;
        }

        public async Task<PrintTenderReceiptReportModel> PrintTenderReceiptReport(int tenderId, string CommericalRegisterNo)
        {
            var result = await _context.Tenders
                .Where(t => t.TenderId == tenderId)
                   .Select(tender => new PrintTenderReceiptReportModel
                   {
                       TenderName = tender.TenderName,
                       TenderId = tender.TenderId,
                       CommercialNumber = CommericalRegisterNo,
                       OfferId = tender.Offers.FirstOrDefault(offer => offer.CommericalRegisterNo == CommericalRegisterNo).OfferId,
                       PurshesingDate = tender.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == CommericalRegisterNo).BillInfo.PurchaseDate.HasValue ? tender.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == CommericalRegisterNo).BillInfo.PurchaseDate.Value.ToString("yyyy-MM-dd") : "",
                       BookletPrice = tender.ConditionsBooklets.FirstOrDefault(b => b.CommericalRegisterNo == CommericalRegisterNo).BillInfo.AmountDue
                   }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ConditionsBooklet> GetConditionBookletByCrAndTenderId(int tenderId, string cr)
        {
            var booklet = await _context.ConditionsBooklets.Include(s => s.BillInfo).ThenInclude(d => d.BillPaymentDetails)
                .Where(x => x.IsActive == true && x.TenderId == tenderId && x.CommericalRegisterNo == cr).FirstOrDefaultAsync();
            return booklet;

        }

        public async Task<CountAndCloseAppliedOffersModel> CountAndCloseAppliedOffers(int tenderId)
        {
            var result = await _context.Tenders
               .Where(t => t.TenderId == tenderId && t.IsActive == true)
                  .Select(tender => new CountAndCloseAppliedOffersModel
                  {
                      TenderId = tender.TenderId,
                      TenderNumber = tender.TenderNumber,
                      TenderReferenceNumber = tender.ReferenceNumber,
                      TenderName = tender.TenderName,
                      SubmitionDate = tender.SubmitionDate.HasValue ? tender.SubmitionDate.Value.ToString("yyyy", new CultureInfo("ar-SA")) : "",
                      AgencyName = tender.Agency.NameArabic,
                      TenderTypeId = (Enums.TenderType)tender.TenderTypeId,
                      OffersNumbers = tender.Offers.Where(o => o.IsActive == true).Count(),
                      InvetationsNumbers = tender.Invitations.Where(i => i.IsActive == true && i.InvitationTypeId == (int)InvitationRequestType.Invitation).Count(),
                      PurshaseNumbers = tender.ConditionsBooklets.Count,
                      InvitationedSuppliers = tender.Invitations.Count != 0 ? tender.Invitations.Where(a => tender.Offers.Any(o => o.CommericalRegisterNo == a.CommericalRegisterNo && o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received))
                      .Select(i => new CountAndCloseAppliedOffersModel.InvitationedSupplier
                      {
                          CommercialNumber = i.CommericalRegisterNo,
                          SupplierName = i.Supplier.SelectedCrName,
                          InvitaionStatus = i.InvitationStatus.NameAr,
                          InvitationAcceptanceDate = i.SubmissionDate.HasValue ? i.SubmissionDate.Value.ToString("yyyy-MM-dd") : "",
                          InvitationSendingDate = i.CreatedAt.ToString("yyyy-MM-dd")
                      }).ToList() : new List<CountAndCloseAppliedOffersModel.InvitationedSupplier>(),
                      PurshesSuppliers = tender.ConditionsBooklets.Count != 0 ? tender.ConditionsBooklets.Where(a => tender.Offers.Any(o => o.CommericalRegisterNo == a.CommericalRegisterNo && o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received))
                      .Select(b => new CountAndCloseAppliedOffersModel.PurshesSupplier
                      {
                          CommercialNumber = b.CommericalRegisterNo,
                          SupplierName = b.Supplier.SelectedCrName,
                          PurshaseDate = b.BillInfo.PurchaseDate.HasValue ? b.BillInfo.PurchaseDate.Value.ToString("yyyy-MM-dd") : "",
                          PurshaseStatus = b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased
                      }).ToList() : new List<CountAndCloseAppliedOffersModel.PurshesSupplier>()
                  }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Tender> FindTenderWithOffer(int tenderId)
        {
            return await _context.Tenders.Include(e => e.TenderActivities).ThenInclude(f => f.Activity)
                .Include(x => x.Offers).Include(x => x.Status)
                .Where(x => x.TenderId == tenderId).AsTracking().FirstOrDefaultAsync();
        }

        #region revisions


        public async Task<TenderChangeRequest> FindActiveTenderRevisionCancelByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var tenderRevisionCancel = await _context.TenderChangeRequests
                .Where(a => a.IsActive == true && a.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && a.TenderId == tenderId).OrderByDescending(a => a.TenderChangeRequestId)
                .FirstOrDefaultAsync();
            return tenderRevisionCancel;
        }

        public async Task<TenderCanelationModel> GetTenderDataForCanelation(int tenderId)
        {
            var user = _httpContextAccessor.HttpContext.User;


            var tenderCancelationss = await _context.Tenders
                .Include(d => d.TenderType)
                .Include(d => d.Status)
          .Where(a => a.IsActive == true && a.TenderId == tenderId).ToListAsync();
            var tenderCancelation = tenderCancelationss
                .Select(t => new TenderCanelationModel
                {
                    TenderId = tenderId,
                    TenderIdString = Util.Encrypt(tenderId),
                    TenderReferenceNumber = t.ReferenceNumber,
                    TenderNumber = t.TenderNumber,
                    TenderTypeId = t.TenderTypeId,
                    TenderName = t.TenderName,
                    ExcutionPlace = t.InsideKSA != null ? (t.InsideKSA == true ? Resources.TenderResources.DisplayInputs.InsideKSA : Resources.TenderResources.DisplayInputs.OutsideKSA) : Resources.TenderResources.DisplayInputs.OutsideKSA,
                    TenderTypeName = t.TenderType.NameAr,
                    TenderStatusName = t.Status.NameAr,
                    IsLowBudgetAndAssignedMember = t.IsLowBudgetDirectPurchase == true && t.DirectPurchaseMemberId == user.UserId(),
                    IsLowBudgetTender = t.IsLowBudgetDirectPurchase.HasValue && t.IsLowBudgetDirectPurchase == true,
                }).FirstOrDefault();


            var tenderChangeRequests = await _context.TenderChangeRequests
                .Include(c => c.SupplierViolators).ThenInclude(s => s.Supplier)
                .Where(c => c.TenderId == tenderId && c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).ToListAsync();

            tenderCancelation.TenderChangeRequestModel = tenderChangeRequests
                 .Select(c => new TenderChangeRequestModel
                 {
                     TenderChangeRequestId = c.TenderChangeRequestId,
                     ChangeRequestStatusId = c.ChangeRequestStatusId,
                     ChangeRequestTypeId = c.ChangeRequestTypeId,
                     RequestedByRoleName = c.RequestedByRoleName,
                     RejectionReason = c.RejectionReason,
                     TenderId = tenderId,
                     CancelationReasonId = c.CancelationReasonId,
                     SupplierViolatorCRs = c.SupplierViolators.Select(d => d.Supplier.SelectedCrName).ToList(),
                     CancelationReasonDescription = c.CancelationReasonDescription
                 }).FirstOrDefault();

            tenderCancelation.CanCreateCancelRequest = user.IsInRole(RoleNames.DataEntry) || user.IsInRole(RoleNames.PurshaseSpecialist) || user.IsInRole(RoleNames.OffersOppeningSecretary) || user.IsInRole(RoleNames.OffersOpeningAndCheckSecretary) || user.IsInRole(RoleNames.OffersCheckSecretary) || user.IsInRole(RoleNames.OffersPurchaseSecretary) || (user.IsInRole(RoleNames.OffersPurchaseManager) && tenderCancelation.IsLowBudgetAndAssignedMember);
            if (tenderCancelation.TenderChangeRequestModel != null)
            {
                tenderCancelation.CanAuditCancelRequest = (user.IsInRole(RoleHelper.GetHigherAuthorityByRoleName(tenderCancelation.TenderChangeRequestModel.RequestedByRoleName)) || tenderCancelation.IsLowBudgetAndAssignedMember) && tenderCancelation.TenderChangeRequestModel.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending;
            }
            return tenderCancelation;
        }

        public async Task<TenderDatesChange> FindActiveTenderRevisionDateForExtendDateApproval(int tenderId)
        {
            var entities = await _context.TenderRevisionDate
                .Include(a => a.ChangeRequest)
                .Where(a => a.ChangeRequest.TenderId == tenderId && a.ChangeRequest.IsActive == true && a.IsActive == true)
                .OrderByDescending(c => c.CreatedAt)
                .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<TenderRevisionDateModel> FindActiveTenderRevisionDateByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());


            var entities = await _context.TenderRevisionDate
                .Include(a => a.ChangeRequest)
              .Where(a => a.ChangeRequest.TenderId == tenderId && a.ChangeRequest.IsActive == true && a.IsActive == true)
              .OrderByDescending(c => c.CreatedAt)
              .Select(t => new TenderRevisionDateModel
              {
                  TenderId = tenderId,
                  TenderIdString = Util.Encrypt(tenderId),
                  LastEnqueriesDate = t.LastEnqueriesDate,
                  LastEnqueriesDateString = (t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                  LastEnqueriesDateHijriString = (t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),

                  LastOfferPresentationDate = t.LastOfferPresentationDate,
                  LastOfferPresentationDateString = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                  LastOfferPresentationDateHijriString = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                  LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",

                  OffersOpeningDate = t.OffersOpeningDate,
                  OffersOpeningDateString = (t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                  OffersOpeningDateHijriString = (t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                  OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",

                  OffersCheckingDate = t.OffersCheckingDate,
                  OffersCheckingDateString = (t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                  OffersCheckingDateHijriString = (t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                  OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("hh:mm tt") : ""
              }).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<TenderRevisionDateModel> FindRejectedTenderRevisionDateByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());


            var entities = await _context.TenderRevisionDate
                .Include(a => a.ChangeRequest)
              .Where(a => a.ChangeRequest.TenderId == tenderId && a.ChangeRequest.TenderChangeRequestId == a.TenderChangeRequestId && a.ChangeRequest.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected)
              .Select(t => new TenderRevisionDateModel
              {
                  TenderId = tenderId,
                  TenderIdString = Util.Encrypt(tenderId),
                  LastEnqueriesDate = t.LastEnqueriesDate,
                  LastEnqueriesDateString = (t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                  LastEnqueriesDateHijriString = (t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),

                  LastOfferPresentationDate = t.LastOfferPresentationDate,
                  LastOfferPresentationDateString = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                  LastOfferPresentationDateHijriString = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                  LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",

                  OffersOpeningDate = t.OffersOpeningDate,
                  OffersOpeningDateString = (t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                  OffersOpeningDateHijriString = (t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                  OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                  RejectionReason = t.ChangeRequest.RejectionReason
              }).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<QueryResult<TenderExtendDateModel>> FindTenderRevisionDateBySearchCriteria(TenderRevisionSearchCriteria criteria)
        {
            var result = await _context.TenderChangeRequests
             .Where(x => x.Tender.TenderTypeId != (int)Enums.TenderType.PreQualification && x.Tender.TenderTypeId != (int)Enums.TenderType.PostQualification && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates && x.Tender.IsActive == true)

            .WhereIf(!String.IsNullOrWhiteSpace(criteria.AgencyCode), x => x.Tender.AgencyCode == criteria.AgencyCode)
            .WhereIf((criteria.BranchId.HasValue && criteria.BranchId.Value != 0), x => x.Tender.BranchId == criteria.BranchId)
            .WhereIf(!(criteria.RequestDateFrom == null), x => x.CreatedAt >= criteria.RequestDateFrom.Value.Date)
            .WhereIf(!(criteria.RequestDateTo == null), x => x.CreatedAt <= criteria.RequestDateTo.Value.Date)
            .WhereIf(!(criteria.ResponseDateFrom == null), x => x.UpdatedAt >= criteria.ResponseDateFrom.Value.Date)
            .WhereIf(!(criteria.ResponseDateTo == null), x => x.UpdatedAt <= criteria.ResponseDateTo.Value.Date)
            .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.Tender.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
            .OrderBy(x => x.TenderChangeRequestId)
            .SortBy(criteria.Sort, criteria.SortDirection)
            .Select(x => new TenderExtendDateModel
            {
                TenderIdString = Util.Encrypt(x.Tender.TenderId),
                TenderName = x.Tender.TenderName,
                TenderNumber = x.Tender.TenderNumber,
                ReferenceNumber = x.Tender.ReferenceNumber,
                LastEnqueriesDate = x.Tender.LastEnqueriesDate,
                LastOfferPresentationDate = x.Tender.LastOfferPresentationDate,
                OffersOpeningDate = x.Tender.LastOfferPresentationDate,
                OffersCheckingDate = x.Tender.OffersCheckingDate,
                CreatedBy = x.CreatedBy,
                ChangeRequestStatusName = x.ChangeRequestStatus.NameAr

            })
            .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<TenderChangeRequest>> FindTenderRevisionCancelBySearchCriteria(TenderRevisionSearchCriteria criteria)
        {
            var result = await _context.TenderChangeRequests
            .Include(x => x.ChangeRequestStatus)
            .Include(x => x.Tender)
            .Where(x => x.Tender.AgencyCode == criteria.AgencyCode && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling)
            .WhereIf(!(criteria.RequestDateFrom == null), x => x.CreatedAt >= criteria.RequestDateFrom.Value.Date)
            .WhereIf(!(criteria.RequestDateTo == null), x => x.CreatedAt <= criteria.RequestDateTo.Value.Date)
            .WhereIf(!(criteria.ResponseDateFrom == null), x => x.UpdatedAt <= criteria.ResponseDateFrom.Value.Date)
            .WhereIf(!(criteria.ResponseDateTo == null), x => x.UpdatedAt <= criteria.ResponseDateTo.Value.Date)
            .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.Tender.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))

            .OrderBy(x => x.TenderChangeRequestId)
            .SortBy(criteria.Sort, criteria.SortDirection)
            .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        #endregion

        #region Subscriptions

        private async Task<bool> SupplierFailedInPostQualification(int tenderId, string cr)
        {

            var isSupplierFailedInPostQualification = await _context.Tenders
                .Where(t => t.PostQualificationTenderId == tenderId && t.IsActive == true)
                .Where(t => t.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed)
                 .AnyAsync(t => t.QualificationFinalResults.Any(q => q.IsActive == true && q.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed && q.SupplierSelectedCr == cr));

            return isSupplierFailedInPostQualification;
        }

        private async Task<bool> SupplierRejectExtendOfferValidity(int tenderId, string cr)
        {
            return await _context.ExtendOffersValiditySuppliers
                   .Where(e => e.Offer.TenderId == tenderId && e.IsActive == true && e.SupplierCR == cr)
                .Where(e => e.ExtendOffersValidity.AgencyCommunicationRequest.IsActive == true && e.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Rejected).AnyAsync();

        }

        private async Task<bool> SuppliersThatNotResponseToExtendOfferValidity(int tenderId, string cr)
        {
            return await _context.ExtendOffersValiditySuppliers
                .Where(e => e.Offer.TenderId == tenderId && e.IsActive == true && e.SupplierCR == cr)
                .Where(e => e.ExtendOffersValidity.AgencyCommunicationRequest.IsActive == true && e.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Sent && DateTime.Now.Date > e.ExtendOffersValidity.NewOffersExpiryDate).AnyAsync();
        }

        private async Task<bool> SuppliersThatAcceptInitialyExtendOfferValidity(int tenderId, string cr)
        {
            return await _context.ExtendOffersValiditySuppliers
                .Where(e => e.Offer.TenderId == tenderId && e.IsActive == true && e.SupplierCR == cr)
                .Where(e => e.ExtendOffersValidity.AgencyCommunicationRequest.IsActive == true && e.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.AcceptedInitially
                && DateTime.Now.Date > e.ExtendOffersValidity.NewOffersExpiryDate).AnyAsync();
        }

        public async Task<AwardingDetailsModel> GetAwardingInformationForSupplier(int tenderId, string cr)
        {
            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();

            var appliedOffers = await _context.Offers.Include(c => c.Supplier).Include(c => c.OfferlocalContentDetails)
                .Where(t => t.TenderId == tenderId && t.IsActive == true)
                .Where(t => t.OfferStatusId == (int)Enums.OfferStatus.Received || t.OfferStatusId == (int)Enums.OfferStatus.Excluded)
            .ToListAsync();

            var receivedOffers = appliedOffers.Where(o => o.OfferStatusId == (int)Enums.OfferStatus.Received).ToList();

            var tender = await _context.Tenders.Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwarding).ToListAsync();
            tender.SetTenderHistories(tenderHistories);
            receivedOffers.ForEach(o => o.Tender = tender);

            var offers = receivedOffers.Where(o => o.Tender.IsNewAwarding(newAwardingReleaseDate) ?
            (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
            && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer) :
            (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)).ToList();

            var isSupplierFailedInPostQualification = await SupplierFailedInPostQualification(tenderId, cr);
            var supplierRejectExtendOfferValidityResult = false;
            var suppliersThatNotResponseToExtendOfferValidityResult = false;
            var suppliersThatAcceptInitialyExtendOfferValidityResult = false;
            if (!isSupplierFailedInPostQualification)
            {
                supplierRejectExtendOfferValidityResult = await SupplierRejectExtendOfferValidity(tenderId, cr);
                suppliersThatNotResponseToExtendOfferValidityResult = await SuppliersThatNotResponseToExtendOfferValidity(tenderId, cr);
                suppliersThatAcceptInitialyExtendOfferValidityResult = await SuppliersThatAcceptInitialyExtendOfferValidity(tenderId, cr);
            }


            var result = new AwardingDetailsModel
            {
                ExclusionReason = !isSupplierFailedInPostQualification ? offers.Where(o => o.CommericalRegisterNo == cr && !string.IsNullOrEmpty(o.ExclusionReason)).Select(o => o.ExclusionReason).FirstOrDefault() : null,
                IsSupplierFailedInFinancialEvaluation = !isSupplierFailedInPostQualification && receivedOffers.Any(o => o.CommericalRegisterNo == cr && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.RejectedOffer),
                IsSupplierFailedInTechnicalEvaluation = !isSupplierFailedInPostQualification && receivedOffers.Any(o => o.CommericalRegisterNo == cr && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer),
                IsSupplierFailedInPostQualifacatoin = isSupplierFailedInPostQualification,
                IsSupplierRejectExtendOfferValidity = supplierRejectExtendOfferValidityResult,
                IsSupplierNotRespontExtendOfferValidity = suppliersThatNotResponseToExtendOfferValidityResult,
                IsSupplierNotAttcheFileInExtendOfferValidity = suppliersThatAcceptInitialyExtendOfferValidityResult,
                SupplierNames = appliedOffers.OrderBy(x => Guid.NewGuid()).Take(100).Select(x => x.Supplier.SelectedCrName).ToList(),
                OffersCount = appliedOffers.Count,
                AverageOffersValue = GetAverageOffersValue(offers),
                AwardedOffers = offers.Where(t => t.TotalOfferAwardingValue != null || t.PartialOfferAwardingValue != null)
                      .Select(o => new AwardedOffersDataModel
                      {
                          cr = o.CommericalRegisterNo,
                          SupplierName = o.Supplier.SelectedCrName,
                          OfferValue = o.TotalOfferAwardingValue == null ? o.PartialOfferAwardingValue : o.TotalOfferAwardingValue
                      }).ToList()
            };

            var isOldTender = tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
            if (!isOldTender)
            {
                var localContentSetting = await _localContentConfigurationSettings.getLocalContentSettingsDate();
                
                result.IsValidToApplyOfferLocalContent = tender.CreatedAt > localContentSetting.Date;

                if (result.IsValidToApplyOfferLocalContent)
                {
                    result.IsNotBindedToMandatoryList = receivedOffers.Any(o => o.CommericalRegisterNo == cr && !o.OfferlocalContentDetails.IsBindedToMandatoryList);
                    result.IsNotBindedToTheLowestBaseLine = receivedOffers.Any(o => o.CommericalRegisterNo == cr && !o.OfferlocalContentDetails.IsBindedToTheLowestBaseLine);
                    result.IsNotBindedToTheLowestLocalContent = receivedOffers.Any(o => o.CommericalRegisterNo == cr && !o.OfferlocalContentDetails.IsBindedToTheLowestLocalContent);
                }
            }
            if (result.AwardedOffers.Any())
            {
                result.IsAwardedSupplier = result.AwardedOffers.Any(x => x.cr == cr);
            }
            return result;
        }




        public async Task<CheckingResultsModel> GetCheckingResultsInformation(int tenderId)
        {
            var checkingResultsModel = new CheckingResultsModel();
            checkingResultsModel.SupplierNames = await _context.Offers.Where(o => o.IsActive == true && o.TenderId == tenderId)
                .Where(o => o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)
                .Select(o => o.Supplier.SelectedCrName).ToListAsync();
            return checkingResultsModel;
        }

        private decimal GetAverageOffersValue(List<Offer> offers)
        {
            var result = offers.Select(offer => offer.FinalPriceAfterDiscount).Average();

            return System.Math.Round(result ?? 0, 2);

        }


        #endregion

        #region Change Requestes

        public async Task<TenderChangeRequest> FindTenderExtendDatesRequests(int tenderId)
        {
            var result = await _context.TenderChangeRequests
                .Where(x => x.TenderId == tenderId && x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).FirstOrDefaultAsync();
            return result;
        }


        public async Task<TenderChangeRequest> GetChangeRequest(int tenderid, int changeTypeId)
        {
            var result = await _context.TenderChangeRequests
                .Include(t => t.CancelationReason)
                .Include(t => t.Tender)
                .ThenInclude(x => x.TenderQuantityTables)
                .ThenInclude(x => x.QuantitiyItemsJson)
                .Include(t => t.TenderQuantityTableChanges)
                .ThenInclude(t => t.QuantitiyItemsChangeJson)
                .Include(x => x.AttachmentChanges)
                .Include(x => x.RevisionDates).Where(x => x.IsActive == true && x.TenderId == tenderid && x.ChangeRequestTypeId == changeTypeId).OrderByDescending(d => d.TenderChangeRequestId).FirstOrDefaultAsync();
            return result;
        }
        public async Task<TenderChangeRequest> GetCancelChangeRequest(int tenderId)
        {
            var result = await _context.TenderChangeRequests
                .Include(t => t.CancelationReason)
                .Include(t => t.Tender)
                .Where(x => x.IsActive == true && x.TenderId == tenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling)
                .OrderByDescending(d => d.TenderChangeRequestId).FirstOrDefaultAsync();
            return result;
        }
        public async Task<TenderChangeRequest> GetPendingCancelChangeRequest(int tenderId, string requestedByRoleName)
        {
            var result = await _context.TenderChangeRequests
                .Where(x => x.IsActive == true && x.TenderId == tenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling)
                .Where(x => x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && x.RequestedByRoleName == requestedByRoleName)
                .OrderByDescending(d => d.TenderChangeRequestId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TenderChangeRequest> GetQTChangeRequest(int tenderid)
        {
            var result = await _context.TenderChangeRequests.Include(d => d.TenderQuantityTableChanges)
                 .Where(x => x.IsActive == true && x.TenderId == tenderid && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables && x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.New).FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<TenderQuantitiyItemsChangeJson>> GetQTItemsByTableIds(List<long> tableIds)
        {
            var result = await _context.TenderQuantityTableChanges.Where(x => x.IsActive == true && tableIds.Any(d => d == x.Id)).Select(d => d.QuantitiyItemsChangeJson)
                           .ToListAsync();
            return result;


        }
        public async Task<TenderChangeRequest> GetExtendDateChangeRequest(int tenderId)
        {
            var result = await _context.TenderChangeRequests
                .Include(t => t.Tender)
                .Include(x => x.RevisionDates)
                .Where(x => x.IsActive == true && x.TenderId == tenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates)
                .OrderByDescending(d => d.TenderChangeRequestId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TenderChangeRequest> GetExtendDateChangeRequestForRejection(int tenderId)
        {
            var result = await _context.TenderChangeRequests
                .Include(c => c.Tender)
                .Where(c => c.IsActive == true && c.TenderId == tenderId && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).OrderByDescending(d => d.TenderChangeRequestId).FirstOrDefaultAsync();
            return result;
        }
        public async Task<TenderChangeRequest> GetExtendDateChangeRequestForCancel(int tenderId)
        {
            var result = await _context.TenderChangeRequests
                .Include(c => c.Tender)
                .ThenInclude(t => t.AgencyCommunicationRequests)
                .Include(x => x.RevisionDates)
                .Where(c => c.IsActive == true && c.TenderId == tenderId && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).OrderByDescending(d => d.TenderChangeRequestId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TenderChangeRequest> GetAttachmentsChangeRequest(int tenderId)
        {
            var result = await _context.TenderChangeRequests
                .Include(t => t.Tender)
                .Where(x => x.IsActive == true && x.TenderId == tenderId && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments)
                .OrderByDescending(d => d.TenderChangeRequestId).FirstOrDefaultAsync();
            return result;
        }


        public async Task<TenderChangeRequest> GetNotPendingCancelChangeRequest(int tenderId)
        {
            var result = await _context.TenderChangeRequests.Include(a => a.Tender)
                .Where(x => x.IsActive == true && x.RequestedByRoleName == _httpContextAccessor.HttpContext.User.UserRole().ToString() && x.TenderId == tenderId && x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Tender> GetTenderWithExtendDatesChangeRequest(int tenderId)
        {
            var result = await _context.Tenders
                .Include(t => t.ChangeRequests)
                .Where(x => x.IsActive == true && x.TenderId == tenderId && x.ChangeRequests.Any(c => c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates && c.IsActive == true))
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Tender> GetTenderWithAttachmentsChangeRequests(int tenderId)
        {
            var entities = await _context.Tenders
                .Include(t => t.Attachments)
                .Include(t => t.ChangeRequests)
                .ThenInclude(c => c.AttachmentChanges)
                .Include(t => t.Offers)
                .ThenInclude(o => o.SupplierTenderQuantityTables)

                .Where(t => t.IsActive == true && t.TenderId == tenderId && t.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments))
                .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<Tender> GetTenderWithAttachmentsChangeRequestsForClosing(int tenderId)
        {
            var entities = await _context.Tenders
                .Include(t => t.ChangeRequests)
                .ThenInclude(c => c.AttachmentChanges)
                .Where(t => t.IsActive == true && t.TenderId == tenderId && t.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments))
                .FirstOrDefaultAsync();
            return entities;
        }
        public async Task<Tender> GetTenderWithAttachmentsChangeRequestsForRejection(int tenderId)
        {
            var entities = await _context.Tenders
                .Include(t => t.ChangeRequests)
                .Where(t => t.IsActive == true && t.TenderId == tenderId && t.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments))
                .FirstOrDefaultAsync();
            return entities;
        }
        public async Task<TenderAttachmentChanges> GetAttachmentById(int attachmentId)
        {
            var entity = await _context.AttachmentChanges.Where(x => x.IsActive == true && x.TenderAttachmentId == attachmentId).FirstOrDefaultAsync();
            return entity;
        }

        #endregion

        #region TenderNews

        public async Task<TenderNewsModel> GetTenderNewsByTenderId(int tenderId)
        {
            return await _context.Tenders
                .Where(r => r.TenderId == tenderId).Select(r => new TenderNewsModel
                {
                    TenderCreationDate = r.CreatedAt,
                    OfferOpeningDate = r.OffersOpeningDate != null ? (DateTime)r.OffersOpeningDate : DateTime.Now.Date,
                    TenderRevisionDate = r.ChangeRequests.Where(x => x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates && x.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Approved).Select(t => new TenderRevisionDateModel { LastEnqueriesDate = (DateTime)t.RevisionDates.FirstOrDefault().LastEnqueriesDate, OffersOpeningDate = (DateTime)t.RevisionDates.FirstOrDefault().OffersOpeningDate, LastOfferPresentationDate = (DateTime)t.RevisionDates.FirstOrDefault().LastOfferPresentationDate }).FirstOrDefault(),
                    OfferAwardingDate = r.TenderHistories.Any(n => n.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed) ? r.TenderHistories.Where(n => n.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).FirstOrDefault().CreatedAt.ToString("dd/MM/yyyy") : ""
                }).FirstOrDefaultAsync();
        }

        #endregion

        public async Task<List<string>> GetQualifiedSuppliers(int? preQualificationId)
        {

            var suppliers = await _context.QualificationFinalResult.Include(c => c.Tender).ThenInclude(h => h.Invitations).Include(s => s.Supplier)
                .Where(x => x.TenderId == preQualificationId
                && x.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded
                && x.IsActive == true
                && x.Tender.PreQualificationApplyDocuments.Any(s => s.IsActive == true && s.SupplierId == x.SupplierSelectedCr && s.StatusId == (int)Enums.QualificationStatus.Received))
                .Select(s => s.SupplierSelectedCr).ToListAsync();

            return suppliers;
        }
        public async Task<List<string>> GetSuppliersJoinedToAnnouncemet(int? preAnnouncementId)
        {

            var suppliersCR = await _context.AnnouncementJoinRequests
                .Where(x => x.AnnouncementId == preAnnouncementId && x.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent)
                .Select(x => x.Cr).ToListAsync();
            return suppliersCR;
        }
        public async Task<List<string>> GetAnnouncementSuppliersTemplateJoinedRequest(int? announcementTemplateId)
        {

            var suppliers = await _context.AnnouncementRequestSupplierTemplate
                 .Where(x => x.AnnouncementId == announcementTemplateId && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted &&
                  x.IsActive == true)
                 .Select(x => x.Cr).ToListAsync();
            return suppliers;
        }
        public async Task<Tender> GetTenderByIdForInvitations(int? tenderId)
        {
            var tender = await _context.Tenders.Include(c => c.Invitations).Include(c => c.UnRegisteredSuppliersInvitation)
                .Where(x => x.TenderId == tenderId &&
                 x.IsActive == true).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<NegotiationAgencyTenderModel> GetTendeBasicDataForNegotiation(int TenderId)
        {
            var Query = await _context.Tenders.Where(d => d.TenderId == TenderId
                        ).Select(d => new NegotiationAgencyTenderModel
                        {
                            committeeId = (d.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || d.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                            ? (d.DirectPurchaseCommitteeId.HasValue ? d.DirectPurchaseCommitteeId.Value : 0) : (d.OffersCheckingCommitteeId.HasValue ? d.OffersCheckingCommitteeId.Value : 0),
                            Amount = d.EstimatedValue,
                            AmountText = new ConvertNumberToText(d.EstimatedValue ?? 0, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic(),
                            ReferenceNumber = d.ReferenceNumber,
                            Tender = d.TenderName,
                            TenderAgencyNumber = d.TenderNumber,
                            TenderIdString = Util.Encrypt(d.TenderId),
                            tenderStatusId = d.TenderStatusId,
                            tenderTypeId = d.TenderTypeId,
                            AgencyName = d.Agency.NameArabic
                        }
            ).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<Tender> FindTenderWithAgenyRequestsAndNegotiations(int TenderId)
        {
            return await _context.Tenders.Include("AgencyCommunicationRequests.Negotiations").Where(r => r.TenderId == TenderId).FirstOrDefaultAsync();
        }

        #region Unit

        public async Task<Tender> FindTenderWithUnitHistoryById(int tenderId)
        {
            var tenderEntity = await _context.Tenders
                .Include(x => x.TenderUnitStatusesHistories)
                .Include(x => x.Attachments)
                .Include(x => x.Branch)
                .Include(x => x.Agency)
                .Include(x => x.TenderUnitAssigns)
                .Include(x => x.Invitations)
                .Where(t => t.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<QueryResult<BasicTenderModel>> FindTenderBySearchCriteriaForUnitStage(TenderSearchCriteria criteria)
        {


            var result = await _context.Tenders
                .Where(x => x.IsActive == true && x.TenderStatusId == (int)Enums.TenderStatus.UnitStaging && x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.PostQualification)
                .WhereIf(_httpContextAccessor.HttpContext.User.UserRole() == Enums.UserRole.NewMonafasat_UnitManager.ToString(), x => x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderManagerReviewing || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.RejectedByManager)

               .WhereIf(_httpContextAccessor.HttpContext.User.UserRole() == Enums.UserRole.NewMonafasat_UnitSpecialistLevel1.ToString(), x => x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderManagerReviewing || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingUnitSecretaryReview || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderUnitReviewLevelOne || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderReviewing || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.RejectedByManager
                        && (x.TenderUnitAssigns.FirstOrDefault(a => a.IsCurrentlyAssigned == true).UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelOne))
               .WhereIf(_httpContextAccessor.HttpContext.User.UserRole() == Enums.UserRole.NewMonafasat_UnitSpecialistLevel2.ToString(), x => x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.WaitingManagerApprove || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderManagerReviewing || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.TenderTransferdToLevelTwo || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.UnderUnitReviewLevelTwo || x.TenderUnitStatusId == (int)Enums.TenderUnitStatus.RejectedByManager
                && (x.TenderUnitAssigns.FirstOrDefault(a => a.IsCurrentlyAssigned == true).UnitSpecialistLevel == (int)Enums.UnitSpecialistLevel.UnitSpecialistLevelTwo))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.TenderNumber.Contains(criteria.ReferenceNumber))
                .WhereIf(criteria.TenderTypeId != 0, t => t.TenderTypeId == criteria.TenderTypeId)

                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new BasicTenderModel
                {
                    TenderNumber = t.TenderNumber,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    EstimatedValue = t.EstimatedValue,
                    InsideKSA = t.InsideKSA,
                    InvitationTypeId = t.InvitationTypeId,
                    TenderTypeId = t.TenderTypeId,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    OffersOpeningCommitteeId = t.OffersOpeningCommitteeId,
                    TechnicalOrganizationId = t.TechnicalOrganizationId != null ? t.TechnicalOrganizationId.Value : 0,
                    OffersOpeningDate = t.OffersOpeningDate,
                    Purpose = t.Purpose,
                    TenderStatusId = t.TenderStatusId,
                    SupplierNeedSample = t.SupplierNeedSample,

                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    InvitationTypeName = t.InvitationType.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    TenderChangeRequestIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.TenderChangeRequestId).ToList(),
                    ChangeRequestStatusIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatusId).ToList(),
                    ChangeRequestStatusNames = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatus.NameAr).ToList(),
                    QuantitiesTableStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    ExtendDatesStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    AttachmentsStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    ChangeRequestedBy = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    TenderStatusName = t.TenderUnitStatus.Name,
                    TechnicalOrganizationName = t.TechnicalOrganization.CommitteeName,
                    OffersOpeningCommitteeName = t.OffersOpeningCommittee.CommitteeName,
                    CreatedDate = t.CreatedAt,
                    AgencyName = t.Agency.NameArabic,
                    SubmitionDate = t.SubmitionDate,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderStatusIdString = (Util.Encrypt(t.TenderStatusId)),
                    OffersCheckingCommitteeName = t.OffersCheckingCommittee.CommitteeName,
                    IsPurchased = t.ConditionsBooklets.Any() ? true : false,
                    CreatedDateHijri = t.CreatedAt.ToHijriDateWithFormat("dd/MM/yyyy"),
                    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    AwardingValue = t.Offers.Select(x => x.TotalOfferAwardingValue).Sum() == 0 ? t.Offers.Select(p => p.PartialOfferAwardingValue).Sum() : t.Offers.Select(h => h.TotalOfferAwardingValue).Sum(),
                    UnitStatusId = t.TenderUnitStatusId,
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
              a.TenderUnitAssignId).FirstOrDefault(a => a.IsActive == true && a.IsCurrentlyAssigned == true).UserProfileId == Convert.ToInt32(_httpContextAccessor.HttpContext.User.UserId()) : false),
                })/*.Where(a => a.CanUnitDoAnyAction)*/

                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<List<TenderUnitStatusesHistory>> GetUnitStatusesHistoryByTenderId(int tenderId)
        {
            return await _context.TenderUnitStatusesHistories.Where(h => h.TenderId == tenderId).ToListAsync();
        }

        public async Task<List<string>> GetAcceptedSupplierInFirstStageTender(int? preQualificationId)
        {
            var suppliers = await _context.Offers
                .Where(x => x.TenderId == preQualificationId &&
                 x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer &&
                 x.IsActive == true).Select(x => x.CommericalRegisterNo).ToListAsync();
            return suppliers;
        }

        #endregion

        public async Task<ExtendOfferPresentationDatesModel> FindTenderRevisionDateForExtendOffersRequestByTenderId(int tenderId, int agencyRequestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(agencyRequestId), agencyRequestId.ToString());


            var entities = await _context.TenderRevisionDate
                .Include(a => a.ChangeRequest)
              .Where(a => a.ChangeRequest.TenderId == tenderId && a.ChangeRequest.IsActive == true && a.IsActive == true)
              .Select(t => new ExtendOfferPresentationDatesModel
              {
                  TenderStatusId = t.ChangeRequest.Tender.TenderStatusId,
                  TenderTypeId = t.ChangeRequest.Tender.TenderTypeId,
                  RevisionDateId = t.RevisionDateId,
                  AgencyId = t.ChangeRequest.Tender.AgencyCode,
                  OffersOpeningAddressId = t.ChangeRequest.Tender.OffersOpeningAddressId,
                  TenderId = tenderId,
                  TenderIdString = Util.Encrypt(tenderId),
                  LastEnqueriesDate = t.LastEnqueriesDate,
                  LastOfferPresentationDate = t.LastOfferPresentationDate,
                  LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                  OffersOpeningDate = t.OffersOpeningDate,
                  OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                  AgencyRequestId = agencyRequestId
              }).OrderByDescending(t => t.RevisionDateId).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<ExtendOfferPresentationDatesModel> FindTenderDatesForExtendDatesRequestByTenderId(int tenderId, int agencyRequestId)
        {


            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Check.ArgumentNotNullOrEmpty(nameof(agencyRequestId), agencyRequestId.ToString());
            var entities = await _context.Tenders
              .Include(t => t.Status)
              .Where(t => t.TenderId == tenderId)
              .Select(t => new ExtendOfferPresentationDatesModel
              {
                  TenderId = t.TenderId,
                  TenderStatusId = t.TenderStatusId,
                  TenderTypeId = t.TenderTypeId,
                  AgencyId = t.AgencyCode,
                  LastEnqueriesDate = t.LastEnqueriesDate,
                  LastOfferPresentationDate = t.LastOfferPresentationDate,
                  LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                  OffersOpeningDate = t.OffersOpeningDate,
                  OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                  OffersCheckingDate = t.OffersCheckingDate,
                  OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("hh:mm tt") : "",
                  AgencyRequestId = agencyRequestId
              }).FirstOrDefaultAsync();
            return entities;
        }


        #region Direct Purchase

        public async Task<TenderOffersModel> FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(int tenderId, int userId)
        {
            var tenderactivityid = await GetCurrentTenderActivityVersion(tenderId);
            DateTime LCPreferenceDate = (await _localContentConfigurationSettings.getLocalContentSettingsDate()).Date.Value;

            var entities = await _context.Tenders
                .Include(t => t.Offers)
                .Include(t => t.Invitations)
                .Include(t => t.ConditionsBooklets)
                .Include(t => t.TenderHistories)
                .Include(t => t.OffersCheckingCommittee)
                .ThenInclude(c => c.CommitteeUsers)
                .Where(x => x.TenderId == tenderId && (
                 x.IsLowBudgetDirectPurchase != true ? x.DirectPurchaseCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRoleId == (int)Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee || c.UserRoleId == (int)Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee)) : true))
                .Select(t => new TenderOffersModel
                {
                    CancelRequestId = t.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary).OrderByDescending(c => c.TenderChangeRequestId).Select(c => c.TenderChangeRequestId).FirstOrDefault(),
                    TenderId = t.TenderId,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderName = t.TenderName,
                    InsideKSA = t.InsideKSA,
                    ExcutionPlace = t.InsideKSA != null ? (t.InsideKSA == true ? Resources.TenderResources.DisplayInputs.InsideKSA : Resources.TenderResources.DisplayInputs.OutsideKSA) : Resources.TenderResources.DisplayInputs.OutsideKSA,
                    TenderNumber = t.TenderNumber,
                    TenderRefrenceNumber = t.ReferenceNumber,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    EstimatedValue = t.EstimatedValue,
                    CheckingDate = t.OffersCheckingDate,
                    OfferPresentationWayId = t.OfferPresentationWayId ?? (int)Enums.OfferPresentationWayId.OneFile,
                    EstimatedValueText = new ConvertNumberToText(t.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic(),
                    OffersOpeningDate = t.OffersOpeningDate != null ? t.OffersOpeningDate : null,
                    OffersCount = t.Offers.Where(x => (x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received) &&
                    x.Tender.Invitations.Any(i => i.IsActive == true && i.StatusId == (int)Enums.InvitationStatus.Approved && i.CommericalRegisterNo == x.CommericalRegisterNo))
                    .Where(x => (x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received)
                    && ((x.OfferTechnicalEvaluationStatusId.HasValue && x.Tender.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles)
                       && (x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking
                       || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved
                       || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage))
                       ? x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
                       : (x.OfferTechnicalEvaluationStatusId == null || x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer || x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer)).Count(),
                    BuyersCount = t.Invitations.Where(x => x.IsActive == true && x.StatusId == (int)Enums.InvitationStatus.Approved).Count() > 0 ?
                    t.Invitations.Where(x => x.IsActive == true && x.StatusId == (int)Enums.InvitationStatus.Approved).Count() : 0,
                    IsTenderTechnicalChecked = t.Offers.Any() ? (t.Offers.Where(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received).All(o => o.OfferTechnicalEvaluationStatusId.HasValue)) : false,
                    IsTenderFinancialChecked = t.Offers.Any() && (t.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.OfferAcceptanceStatusId.HasValue)),
                    isOffersPreferenceChecked = t.Offers.Any() && (t.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.OfferlocalContentDetails.OfferPriceAfterLocalContent.HasValue || o.OfferlocalContentDetails.OfferPriceAfterSmallAndMediumCorporations.HasValue)),
                    isApplyOfferPreference = t.IsValidToApplyOfferLocalContentChanges(LCPreferenceDate),
                    IsValidToGoToFinancailCheck = t.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.IsOfferFinancialDetailsEntered == true),
                    TenderAreaNameList = t.InsideKSA == true ? t.TenderAreas.Select(y => y.Area.NameAr).ToList() : t.TenderCountries.Select(y => y.Country.NameAr).ToList(),
                    InvitationsCount = t.Invitations.Where(inv => inv.StatusId == (int)Enums.InvitationStatus.Approved && inv.IsActive == true).Count(),
                    RejectionReason = t.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason,
                    AgencyCode = t.AgencyCode,
                    IsValidToCheck = t.Offers.Where(a => a.IsActive == true && a.OfferTechnicalEvaluationStatusId == null && a.OfferStatusId == (int)Enums.OfferStatus.Received).Count() == 0 ? true : false,
                    IsValidToCheckFinancial = t.Offers.Where(a => a.IsActive == true && a.OfferAcceptanceStatusId == null && a.OfferStatusId == (int)Enums.OfferStatus.Received).Count() == 0 ? true : false,
                    NPCalcHaveBeenDone = t.Offers.Any(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferWeightAfterCalcNPA != o.FinalPriceAfterDiscount),
                    showUnacceptedFinanciallyMessage = (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersPurchaseSecretary) && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase)
                    && ((t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile && t.TenderStatusId == (int)Enums.TenderStatus.OffersChecking)
                    || (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles && t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking)),
                    IsLowBudgetDirectPurchase = t.IsLowBudgetDirectPurchase == true,
                    DirectPurchaseMemberId = t.DirectPurchaseMemberId,
                    Containtawreed = t.IsTenderContainsTawreedTables.HasValue && t.IsTenderContainsTawreedTables.Value
                }).FirstOrDefaultAsync();

            entities.ContainSupply =
                (entities.Containtawreed.HasValue && entities.Containtawreed.Value)
                ||
(await _context.TenderActivities.AnyAsync(a => a.TenderId == tenderId && a.IsActive == true && (
         tenderactivityid >= (int)Enums.ActivityVersions.Sprint7Activities
             ? a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralSupply)
             : a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials))

             ));


            return entities;
        }

        #endregion Direct Purchase

        #region Bidding Round

        public async Task<Tender> FindBiddingRoundOffersByTenderId(int tenderId)
        {
            var result = await _context.Tenders
                .Include(t => t.TenderAreas)
                .ThenInclude(tt => tt.Area)
                .Include(t => t.TenderCountries)
                .ThenInclude(tt => tt.Country)
                .Include(t => t.Offers)
                .ThenInclude(t => t.Supplier)
                .Include(t => t.TenderHistories)
                .Include(t => t.BiddingRounds)
                .ThenInclude(t => t.BiddingRoundOffers)
                .ThenInclude(t => t.Offer)
                .ThenInclude(t => t.Supplier)
                .ThenInclude(t => t.SupplierUserProfiles)
                .Where(a => a.IsActive == true)
                .FirstOrDefaultAsync(t => t.IsActive == true && t.TenderId == tenderId);
            return result;
        }

        public async Task<Tender> FindTenderForAddingBiddingOfferByTenderId(int tenderId)
        {
            var result = await _context.Tenders
                .Include(t => t.TenderAreas)
                .ThenInclude(tt => tt.Area)
                .Include(t => t.TenderCountries)
                .ThenInclude(tt => tt.Country)
                .Include(t => t.TenderHistories)
                .Include(t => t.BiddingRounds)
                .ThenInclude(t => t.BiddingRoundOffers)
                .Where(a => a.IsActive == true)
                .FirstOrDefaultAsync(t => t.IsActive == true && t.TenderId == tenderId);
            return result;
        }

        public async Task<List<Offer>> FindTenderOffersForBiddingRound(int tenderId)
        {
            var result = await _context.Offers.Where(o => o.IsActive == true && o.TenderId == tenderId).Include(o => o.Supplier).ToListAsync();
            return result;
        }

        public async Task<Tender> FindBiddingRoundOffersByTenderIdForEndingBiddingRound(int tenderId)
        {
            var result = await _context.Tenders
                .Include(t => t.TenderAreas)
                .ThenInclude(tt => tt.Area)
                .Include(t => t.TenderCountries)
                .ThenInclude(tt => tt.Country)
                .Include(t => t.Offers)
                .ThenInclude(t => t.Supplier)
                .Include(t => t.TenderHistories)
                .Include(t => t.BiddingRounds)
                .ThenInclude(t => t.BiddingRoundOffers)
                .Where(a => a.IsActive == true)
                .FirstOrDefaultAsync(t => t.IsActive == true && t.TenderId == tenderId);
            return result;
        }

        #endregion

        #region VRO

        public async Task<QueryResult<VROTenderCheckingAndOpeningModel>> FindTendersForVROCheckingAndOpeningStageBySearchCriteria(TenderSearchCriteria criteria)
        {


            Calendar hijri = new UmAlQuraCalendar();

            var result = await _context.Tenders
             .Where(x => x.VROCommitteeId == criteria.SelectedCommitteeId && x.IsActive == true)
             .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
             .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
             .WhereIf(!String.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
                          .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => criteria.TenderStatusIds.Contains(x.TenderStatusId))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new VROTenderCheckingAndOpeningModel
                {
                    TenderId = t.TenderId,
                    TenderName = t.TenderName,
                    TenderReferenceNumber = t.ReferenceNumber,
                    TenderNumber = t.TenderNumber,
                    TenderStatusName = t.Status.NameAr,
                    TenderTypeName = t.TenderType.NameAr,
                    ConditionsBookletPrice = t.ConditionsBookletPrice,
                    AgencyCode = t.AgencyCode,
                    BranchId = t.BranchId,
                    CreatedBy = t.CreatedBy,
                    TenderChangeRequestIdsForCheckingSecretary = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && (c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForCheckingManager = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && (c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    ApprovedBy = t.TenderHistories.OrderByDescending(h => h.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.Approved && (h.ActionId == (int)Enums.TenderActions.ApproveTender || h.ActionId == (int)Enums.TenderActions.ApproveTenderByUnitManager)).CreatedBy,
                    ChangeRequestedBy = t.ChangeRequests.Where(x => x.IsActive == true && x.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    UserCommitteeType = t.VROCommittee != null && criteria.UserId != null ? (t.VROCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == criteria.UserId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersOpeningAndCheckManager) ? (int)Enums.UserCommitteeType.NewMonafasat_OffersCheckManager : (int)Enums.UserCommitteeType.NewMonafasat_OffersCheckSecretary) : (int)Enums.UserCommitteeType.None,
                    AgencyName = t.Agency.NameArabic,
                    BranchName = t.Branch.BranchName,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    OffersCheckingCommitteeId = t.VROCommitteeId,
                    OffersOpeningCommitteeId = t.VROCommitteeId,
                    SubmitionDate = t.SubmitionDate,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    OffersOpeningDate = t.OffersOpeningDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningDateHijri = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDateHijri = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToGregorianDate("hh:mm tt") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCount = t.Offers.Where(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded)).Count(),
                    CanStartTechnicalEvaluation = (t.TenderStatusId == (int)Enums.TenderStatus.Approved)
                     ? true : false
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<VROTenderOffersModel> FindVROTenderOfferDetailsByTenderIdForCheckStage(int tenderId, int userId)
        {
            var entities = await _context.Tenders
                .Where(x => x.TenderId == tenderId)
                .Select(t => new VROTenderOffersModel
                {
                    CancelRequestId = t.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending && c.RequestedByRoleName == RoleNames.OffersOpeningAndCheckSecretary).OrderByDescending(c => c.TenderChangeRequestId).Select(c => c.TenderChangeRequestId).FirstOrDefault(),
                    TenderId = t.TenderId,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderName = t.TenderName,
                    InsideKSA = t.InsideKSA,
                    ExcutionPlace = t.InsideKSA != null ? (t.InsideKSA == true ? Resources.TenderResources.DisplayInputs.InsideKSA : Resources.TenderResources.DisplayInputs.OutsideKSA) : Resources.TenderResources.DisplayInputs.OutsideKSA,
                    TenderNumber = t.TenderNumber,
                    TenderRefrenceNumber = t.ReferenceNumber,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    EstimatedValue = t.EstimatedValue,
                    isVRORelatedBranchCreated = t.VRORelatedBranchId.HasValue ? true : false,
                    OfferPresentationWayId = t.OfferPresentationWayId ?? (int)Enums.OfferPresentationWayId.OneFile,
                    EstimatedValueText = t.EstimatedValue.HasValue ? new ConvertNumberToText(t.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic() : "",
                    OffersOpeningDate = t.OffersOpeningDate != null ? t.OffersOpeningDate : null,
                    OffersCount = t.Offers
                    .Where(x => x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded))
                    .Where(x => x.Tender.Invitations.Any(i => i.IsActive == true && i.StatusId == (int)Enums.InvitationStatus.Approved && i.CommericalRegisterNo == x.CommericalRegisterNo) ||
                           x.Tender.ConditionsBooklets.Any(c => c.IsActive == true && c.CommericalRegisterNo == x.CommericalRegisterNo))
                    .Where(x => (x.OfferTechnicalEvaluationStatusId.HasValue && x.OfferAcceptanceStatusId.HasValue
                        && (x.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved || x.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening
                        || x.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking || x.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                        || x.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved))
                        ? x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && x.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer
                        : (x.OfferTechnicalEvaluationStatusId == null || x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer || x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer))
                    .Count(),
                    BuyersCount = t.ConditionsBooklets.Where(x => x.IsActive == true).Count(),
                    TenderAreaNameList = t.InsideKSA == true ? t.TenderAreas.Select(y => y.Area.NameAr).ToList() : t.TenderCountries.Select(y => y.Country.NameAr).ToList(),
                    InvitationsCount = t.Invitations.Where(inv => inv.StatusId == (int)Enums.InvitationStatus.Approved && inv.IsActive == true).Count(),
                    RejectionReason = t.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason,
                    AgencyCode = t.AgencyCode,
                    AreAllOffersChecked = t.Offers.Where(a => a.OfferTechnicalEvaluationStatusId == null && a.IsActive == true && a.OfferStatusId == (int)Enums.OfferStatus.Received).Count() == 0 ? true : false,
                    IsValidToGoToFinancailCheck = t.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
                    && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer).All(o => o.IsOfferFinancialDetailsEntered == true)
                }).FirstOrDefaultAsync();
            return entities;
        }

        #endregion VRO

        public async Task<QueryResult<NegotiationOnTenderModel>> GetAllTenderhasNegotiationbySearchCretriaForUnitUsers(NegotiationOnTenderSearchCriteriaModel criteria)
        {

            var result = await _context.Tenders
             .Where(x => x.IsActive == true)
             .WhereIf(criteria.TenderTypeId != 0, t => t.TenderTypeId == criteria.TenderTypeId)
             .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Trim().Contains(criteria.TenderName.ToUpper().Trim()))
             .WhereIf(!string.IsNullOrEmpty(criteria.ReferenceNumber), t => t.ReferenceNumber == criteria.ReferenceNumber)
             .WhereIf(!string.IsNullOrEmpty(criteria.TenderNumber), t => t.TenderNumber == criteria.TenderNumber)
             .Where(tender => tender.AgencyCommunicationRequests.Any(f => f.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation))
             .Where(t => t.AgencyCommunicationRequests.Any(f => f.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation) == true && t.AgencyCommunicationRequests.FirstOrDefault(f => f.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation).Negotiations.Any(f => f.NegotiationTypeId == (int)Enums.enNegotiationType.SecondStage && f.StatusId == (int)Enums.enNegotiationStatus.UnitSpecialestPendingApproved))
             .Select(
                t =>
                 new NegotiationOnTenderModel
                 {
                     tenderIdString = Util.Encrypt(t.TenderId),
                     tenderAddress = t.TenderName,
                     conditionalAmount = t.ConditionsBookletPrice ?? 0,
                     referenceNumber = t.ReferenceNumber,
                     tenderTypeName = t.TenderType.NameAr,
                     CreatedBy = t.CreatedBy,
                     agencyBranchName = t.Branch.BranchName,
                     agencyName = t.Agency.NameArabic,
                     negotiationIdString = GetId(t.AgencyCommunicationRequests.FirstOrDefault(f => f.IsActive == true && f.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation).Negotiations.FirstOrDefault(f => f.IsActive == true && f.NegotiationTypeId == (int)Enums.enNegotiationType.SecondStage && f.StatusId == (int)Enums.enNegotiationStatus.UnitSpecialestPendingApproved))
                 }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;

        }

        private static string GetId(Negotiation AGC)
        {
            if (AGC == null)
            {
                return "";
            }
            return Util.Encrypt(AGC.NegotiationId);
        }

        public async Task<List<SupplierTenderQuantityTableDTO>> getTenderQuantitytableItems(int tenderId)
        {
            var o = await _context.TenderQuantityTables
               .Where(d => d.TenderId == tenderId && d.IsActive == true).ToListAsync();
            var quantityTablesIds = o.Select(a => a.Id).ToList();

            var QTItems = await _context.TenderQuantityTableItemsView.Where(items => quantityTablesIds.Contains(items.QTableId)).ToListAsync();

            var tenderQuantityTableDTOs = o.Select(
                s => new SupplierTenderQuantityTableDTO()
                {
                    TableId = s.Id,
                    TableName = s.Name,
                    TableNumber = s.Id,
                    TenderId = s.TenderId,
                    SupplierQItems = QTItems.Where(t => t.QTableId == s.Id).Select(f => new SupplierTenderQuantityItemDTO()
                    {
                        TenderItemId = f.Id,
                        ColumnId = f.ColumnId,
                        ItemNumber = f.ItemNumber.Value,
                        Value = f.Value,
                        TenderFormHeaderId =
                        f.TenderFormHeaderId
                    }).ToList()
                }
                ).ToList();
            return tenderQuantityTableDTOs;

        }
        public async Task<Core.Entities.TenderType> GetTenderTypeById(int id)
        {
            return await _context.TenderTypes.Where(a => a.TenderTypeId == id).FirstOrDefaultAsync();
        }

        public async Task<List<TenderTypeWithAddedValueModel>> GetTenderTypesWithAddedValue()
        {


            return await _context.TenderTypes.Where(x => x.TenderTypeId != (int)Enums.TenderType.NationalTransformationProjects && x.TenderTypeId != (int)Enums.TenderType.PostQualification && x.TenderTypeId != (int)Enums.TenderType.PreQualification && x.TenderTypeId != (int)Enums.TenderType.CurrentTender && x.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase)
            .Select(s => new TenderTypeWithAddedValueModel()
            {
                Id = s.TenderTypeId,
                Name = s.NameAr,
                BuyingCost = s.BuyingCost,
                InvitationCost = s.InvitationCost
            }).ToListAsync();
        }

        public async Task<List<MandatoryListForExportModel>> GetAllMandatoryListForExport()
        {
            var result = await _context.MandatoryListProducts.Where(s => s.IsActive == true && s.MandatoryList.IsActive == true
            && (s.MandatoryList.StatusId == (int)Enums.MandatoryListStatus.Approved || s.MandatoryList.StatusId == (int)Enums.MandatoryListStatus.CancelRejected || s.MandatoryList.StatusId == (int)Enums.MandatoryListStatus.WaitingCancelApproval)).Select(x => new MandatoryListForExportModel
            {
                DivisionNameEn = x.MandatoryList.DivisionNameEn + " - " + x.MandatoryList.DivisionCode,
                DivisionNameAr = x.MandatoryList.DivisionNameAr + " - " + x.MandatoryList.DivisionCode,
                CSICode = x.CSICode,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                DescriptionAr = x.DescriptionAr,
                DescriptionEn = x.DescriptionEn,
                Price = x.PriceCelling,
            }).ToListAsync();
            return result;
        }

        public async Task<TenderDetailsForAppliedSuppliersReportModel> GetTenderDetailsForAppliedSuppliersReport(int tenderId)
        {
            var result = await _context.Tenders.Where(s => s.TenderId == tenderId && s.IsActive == true).Select(tender => new TenderDetailsForAppliedSuppliersReportModel
            {
                TenderId = tender.TenderId,
                TenderIdString = Util.Encrypt(tender.TenderId),
                TenderName = tender.TenderName,
                TenderNumber = tender.TenderNumber,
                TenderStatusName = tender.Status.NameAr,
                TenderTypeName = tender.TenderType.NameAr,
                ReferenceNumber = tender.ReferenceNumber,
                InvitationsCount = tender.Invitations.Count,
                PurchaseCount = tender.ConditionsBooklets.Count(c => c.IsActive == true && c.BillInfo != null && c.BillInfo.IsActive == true && c.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid),
                OffersCount = tender.Offers.Count(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received)
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Tender> GetTenderForLocalContent(int tenderId)
        {
            var tender = await _context.Tenders.Include(x => x.TenderLocalContent)
                 .FirstOrDefaultAsync(a => a.TenderId == tenderId);

            var tenderActivities = await _context.TenderActivities.Include(a => a.Activity).ThenInclude(x => x.ActivityTemplateVersions)
                .Where(a => a.IsActive == true && a.TenderId == tenderId).ToListAsync();
            tenderActivities.ForEach(a => tender.TenderActivities.Add(a));
            return tender;
        }

        public async Task<List<EmarketPlaceRequest>> GetAwardedSupplierQuantityTableItemsValue(List<int> offerIds)
        {
            var Columns = await _context.Offers.Where(o => o.IsActive == true && offerIds.Contains(o.OfferId)).SelectMany(f => f.SupplierTenderQuantityTables)
                .Select(x => new EmarketPlaceRequest
                {
                    FormId = x.TenderQuantityTable.FormId,
                    TableId = x.Id,
                }).ToListAsync();
            return Columns;
        }

        public async Task<SRMFrameworkAgreementManageModel> FillVendorProducts(List<int> offerIds, List<EmarketPlaceResponse> clotypes)
        {
            char[] spearator = { '|' };
            var s = await _context.Offers
                            .Include(a => a.Tender)
                            .ThenInclude(a => a.TenderAgreementAgencies)
                            .Where(o => o.IsActive == true && offerIds.Contains(o.OfferId)).FirstOrDefaultAsync();
            var tenderHistories = await _context.TenderHistories
                            .Where(h => h.Tender.Offers.Any(i => i.OfferId == s.OfferId)).ToListAsync();
            s.Tender.TenderHistories.AddRange(tenderHistories);
            var supplierTenderQuantityTables = await _context.SupplierTenderQuantityTables.Include(a => a.QuantitiyItemsJson).Include(a => a.TenderQuantityTable)
                            .Where(h => h.OfferId == s.OfferId).ToListAsync();
            s.SupplierTenderQuantityTables.AddRange(supplierTenderQuantityTables);
            var result = new SRMFrameworkAgreementManageModel
            {
                ReferenceNumber = s.Tender.ReferenceNumber,
                ContractName = s.Tender.TenderName,
                ContractType = s.Tender.AgreementTypeId == 0 ? SRMContractType.Open : SRMContractType.Close,
                ValidityInfo = new ValidityInfo
                {
                    NumOfDays = (int)s.Tender.AgreementDays,
                    NumOfMonths = (int)s.Tender.AgreementMonthes,
                    NumOfYears = (int)s.Tender.AgreementYears,
                },

                AgencyList = s.Tender.TenderAgreementAgencies.Select(a => a.AgencyCode).ToList(),
                AwardDt = DateTime.Now,
                CreatedBy = s.Tender.AgencyCode,
                TotalAmt = (decimal)s.FinalPriceAfterDiscount,//
                Note = s.JustificationOfRecommendation,// 
                Currency = "SAR",
                ValidFrom = s.Tender.TenderHistories.Where(h => h.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).OrderByDescending(h => h.TenderHistoryId).Select(h => h.CreatedAt).FirstOrDefault(),
                VendorsList =
                s.SupplierTenderQuantityTables.Select(w => new VendorList
                {
                    ProductList = CollectList(w.QuantitiyItemsJson.SupplierTenderQuantityTableItems, w.TenderQuantityTable.FormId, clotypes),
                    AwardedAmt = s.TotalOfferAwardingValue != null ? (decimal)s.TotalOfferAwardingValue : (decimal)s.PartialOfferAwardingValue,//
                    PurchaseCurrency = "SAR",
                    VendorId = s.CommericalRegisterNo
                }).ToList()
            };
            return result;
        }

        private static List<ProductList> CollectList(List<SupplierTenderQuantityTableItem> tableItems, long formId, List<EmarketPlaceResponse> clotypes)
        {
            ProductList product = new ProductList();
            List<ProductList> productLists = new List<ProductList>();
            var grouppedData = tableItems.GroupBy(d => d.ItemNumber).Select(d => new { rowNumber = d.Key, data = d });


            foreach (var datarow in grouppedData)
            {
                product.DeliveryDurationInfo = new DeliveryDurationInfo
                {
                    NumOfDays = 5,
                    NumOfMonths = 5,
                    NumOfYears = 3
                };
                var datarowObj = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.ItemId).ColumnId) != null;
                var datarowObjValue = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.ItemId).ColumnId).Select(d => d.Value).FirstOrDefault();

                product.ProductId = datarowObj == true ? datarowObjValue : "";
                product.ProductName = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Name).ColumnId) != null
                   ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Name).ColumnId).Select(d => d.Value).FirstOrDefault() : "";

                product.UnitPrice = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Price)?.ColumnId).Sum(s => decimal.Parse(string.IsNullOrEmpty(s.Value) ? "0" : s.Value));

                product.DiscountPercen = decimal.Parse(
                    datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.DiscountPercentage)?.ColumnId) != null
                    ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.DiscountPercentage)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "0");

                product.VATAmt = decimal.Parse(
                    datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.VAT)?.ColumnId) != null
                    ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.VAT)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "0");


                product.Quantity = decimal.Parse(
                    datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Quantity).ColumnId) != null
                    ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Quantity).ColumnId).Select(d => d.Value).FirstOrDefault() : "0");


                product.UnitOfMeasure = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Unit)?.ColumnId) != null
                   ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Unit)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "";

                product.Desc = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Description)?.ColumnId) != null
                   ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Description)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "";

                productLists.Add(product);
            }

            return productLists;
        }

        public async Task<TenderDetailsCheckingStageModel> GetTenderDetailsForCheckingStage(int tenderId, int userId, string agencyCode)
        {
            DateTime oldFlowEndTime = _rootConfiguration.OldFlow.EndDate.ToDateTime();
            DateTime LCPreferenceDate = (await _localContentConfigurationSettings.getLocalContentSettingsDate()).Date.Value;
            var tender = await FindTenderActivitiesByTenderId(tenderId);
            var verid = await GetCurrentTenderActivityVersion(tenderId);
            var result = await _context.Tenders.Where(x => x.TenderId == tenderId && x.AgencyCode == agencyCode &&
                (x.OffersCheckingCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRole.Name == RoleNames.OffersCheckManager || c.UserRole.Name == RoleNames.OffersCheckSecretary)) ||
                x.DirectPurchaseCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRole.Name == RoleNames.OffersPurchaseManager || c.UserRole.Name == RoleNames.OffersPurchaseSecretary)) ||
                x.VROCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRole.Name == RoleNames.OffersOpeningAndCheckSecretary || c.UserRole.Name == RoleNames.OffersOpeningAndCheckManager)) ||
                x.OffersOpeningCommittee.CommitteeUsers.Any(c => c.IsActive == true && c.UserProfileId == userId && (c.UserRole.Name == RoleNames.OffersOppeningSecretary || c.UserRole.Name == RoleNames.OffersOppeningManager))
                || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.ApproveTenderAward))).Select(t => new TenderDetailsCheckingStageModel
                {
                    CancelRequestId = t.ChangeRequests.Where(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending
                    && (
                    (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersOppeningManager) && c.RequestedByRoleName == RoleNames.OffersOppeningSecretary)
                    ||
                    (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckManager) && c.RequestedByRoleName == RoleNames.OffersCheckSecretary)
                    )
                    ).OrderByDescending(c => c.TenderChangeRequestId).Select(c => c.TenderChangeRequestId).FirstOrDefault(),
                    TenderIdString = Util.Encrypt(t.TenderId),
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    TenderRefrenceNumber = t.ReferenceNumber,
                    TenderStatusId = t.TenderStatusId,
                    TenderTypeId = t.TenderTypeId,
                    InsideKSA = t.InsideKSA,
                    EstimatedValue = t.EstimatedValue,
                    BiddingRoundStartDate = t.BiddingRounds.Any(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New) ? t.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New).StartDate : DateTime.Now,
                    BiddingRoundEndDate = t.BiddingRounds.Any(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New) ? t.BiddingRounds.FirstOrDefault(a => a.IsActive == true && a.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New).EndDate : DateTime.Now,
                    IsAllOffersNotIdenticallyMatched = t.Offers.All(a => a.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer),
                    RejectionReason = t.TenderHistories.OrderByDescending(t => t.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersCheckedRejected
                    || h.StatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected || h.StatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected
                    || h.StatusId == (int)Enums.TenderStatus.BiddingOffersCheckedRejected
                    || h.StatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected).RejectionReason,
                    CheckingDateSet = t.CheckingDateSet.HasValue && t.CheckingDateSet == true,
                    FinancialCheckingDateSet = t.FinancialCheckingDateSet.HasValue && t.FinancialCheckingDateSet == true,
                    IsTenderTechnicalChecked = t.Offers.Any() && (t.Offers.Where(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received).All(o => o.OfferTechnicalEvaluationStatusId.HasValue)),
                    IsTenderFinancialChecked = (t.Offers.Any() && t.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.OfferAcceptanceStatusId.HasValue)),
                    isOffersPreferenceChecked = t.Offers.Any() && (t.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.OfferlocalContentDetails.OfferPriceAfterLocalContent.HasValue || o.OfferlocalContentDetails.OfferPriceAfterSmallAndMediumCorporations.HasValue)),
                    isApplyOfferPreference = t.IsValidToApplyOfferLocalContentChanges(LCPreferenceDate),
                    //isApplySMEAPreference = t.TenderLocalContent.HighValueContractsAmmount <= t.EstimatedValue,
                    IsValidToGoToFinancailCheck = t.Offers.Where(o => o.IsActive == true && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).All(o => o.IsOfferFinancialDetailsEntered == true),
                    IsValidToCheck = t.Offers.Where(a => a.OfferTechnicalEvaluationStatusId == null && a.IsActive == true && a.OfferStatusId == (int)Enums.OfferStatus.Received).Count() == 0,
                    NPCalcHaveBeenDone = t.Offers.Any(o => o.IsActive == true && o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferWeightAfterCalcNPA != o.FinalPriceAfterDiscount),
                    OfferPresentationWayId = t.OfferPresentationWayId.HasValue ? t.OfferPresentationWayId.Value : 0,
                    OffersCount = t.Offers.Where(x => (x.IsActive == true && x.OfferStatusId == (int)Enums.OfferStatus.Received))
                                                 .Where(x => (x.OfferTechnicalEvaluationStatusId.HasValue && x.Tender.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles)
                                                    && (x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking
                                                    || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved
                                                    || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage
                                                    || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected
                                                    || x.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved)
                                                    ? x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
                                                    : (x.OfferTechnicalEvaluationStatusId == null || x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer || x.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer)).Count(),
                    BuyersCount = (t.Invitations.Count > 0 ? t.Invitations.Count(inv => inv.IsActive == true && inv.StatusId == (int)InvitationStatus.Approved) : 0) +
                    (t.TenderTypeId != (int)Enums.TenderType.LimitedTender ?
                    t.ConditionsBooklets.Count(c => c.IsActive == true && c.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid
                    && !(c.BillInfo.InvitationId.HasValue)) : 0),
                    isOldFlow = t.IsOldFlow(oldFlowEndTime),
                    canEnterOfferData = (t.CheckingDateSet.HasValue && (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile
                             || (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles &&
                             (t.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalChecking ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected ||
                             (t.FinancialCheckingDateSet.HasValue &&
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed ||
                             t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved)
                             ))
                             ))
                             ||
                             (t.FinancialCheckingDateSet.HasValue && t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles),
                    canViewOfferPrice = t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile || (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles &&
                        (t.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage ||
                        t.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStagePending ||
                        t.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved ||
                        t.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageRejected ||
                        t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking ||
                        t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending ||
                        t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved ||
                        t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected)),
                    showUnacceptedFinanciallyMessage = _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary) &&
                    (t.TenderTypeId != (int)Enums.TenderType.Competition && t.TenderTypeId != (int)Enums.TenderType.CurrentTender && t.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase
                    && t.TenderTypeId != (int)Enums.TenderType.FirstStageTender && t.TenderTypeId != (int)Enums.TenderType.ReverseBidding)
                    && ((t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile && t.TenderStatusId == (int)Enums.TenderStatus.OffersChecking)
                    || (t.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles && t.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking))
                }).FirstOrDefaultAsync();
            result.ContainSupply = tender.QuantityTableVersionId > (int)Enums.QuantityTableVersion.Version1 &&
                (tender.IsTenderContainsTawreedTables.HasValue && tender.IsTenderContainsTawreedTables.Value
|| (verid >= (int)Enums.ActivityVersions.Sprint7Activities
                ? tender.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralSupply))
                : tender.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials))

                ));
            return result;
        }

        public async Task<bool> IsSupplierFailedInPostqualification(int tenderId, string cr)
        {
            var result = await _context.PostQualificationSuppliersInvitations
                .Where(t => t.PostQualification.PostQualificationTenderId == tenderId && t.PostQualification.IsActive == true)
                .Where(t => t.CommercialNumber == cr && t.PostQualification.TenderStatusId != (int)Enums.TenderStatus.Canceled)
                .Where(t => t.PostQualification.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed && t.PostQualification.QualificationFinalResults.Any(f => f.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed && f.SupplierSelectedCr == cr))
                .AnyAsync();

            return result;
        }

        public async Task<bool> IsTenderHasActivePostqualification(int tenderId)
        {
            var result = await _context.Tenders
                .Where(t => t.PostQualificationTenderId == tenderId && t.IsActive == true)
                .Where(t => t.TenderStatusId != (int)Enums.TenderStatus.Canceled)
                .Where(t => (t.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed
                && !t.QualificationFinalResults.Any(f => f.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed ||
                                                         f.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded))
                || t.TenderStatusId != (int)Enums.TenderStatus.DocumentCheckConfirmed).AnyAsync();
            return result;
        }

        public async Task<Tender> FindTenderByAgencyAndReferenceNumberForContractLinking(string referenceNumber)
        {
            var result = await _context.Tenders.Include(d => d.TenderHistories).Include(e => e.AgencyCommunicationRequests).ThenInclude(ee => ee.PlaintRequests)
                         .Where(t => t.ReferenceNumber == referenceNumber && t.IsActive == true).FirstOrDefaultAsync();
            return result;
        }

        private List<int> GetOneFileTenderStatusIdsForOpenOffersReportHiding()
        {
            List<int> ids = new List<int>();
            ids.Add((int)Enums.TenderStatus.UnderEstablishing);
            ids.Add((int)Enums.TenderStatus.Established);
            ids.Add((int)Enums.TenderStatus.Canceled);
            ids.Add((int)Enums.TenderStatus.Rejected);
            ids.Add((int)Enums.TenderStatus.Approved);
            ids.Add((int)Enums.TenderStatus.OffersOppening);
            ids.Add((int)Enums.TenderStatus.OffersOppenedRejected);
            ids.Add((int)Enums.TenderStatus.OffersOppenedPending);
            return ids;
        }
        private List<int> GetTwoFileTenderStatusIdsForOpenOffersReportHiding()
        {
            List<int> ids = new List<int>();
            ids.Add((int)Enums.TenderStatus.UnderEstablishing);
            ids.Add((int)Enums.TenderStatus.Established);
            ids.Add((int)Enums.TenderStatus.Canceled);
            ids.Add((int)Enums.TenderStatus.Rejected);
            ids.Add((int)Enums.TenderStatus.Approved);
            ids.Add((int)Enums.TenderStatus.OffersOppening);
            ids.Add((int)Enums.TenderStatus.OffersOppenedRejected);
            ids.Add((int)Enums.TenderStatus.OffersOppenedPending);
            ids.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppening);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningPending);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningRejected);
            ids.Add((int)Enums.TenderStatus.OffersChecking);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalCheckingPending);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalCheckingRejected);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed);
            ids.Add((int)Enums.TenderStatus.OffersOpenFinancialStage);
            ids.Add((int)Enums.TenderStatus.OffersOpenFinancialStageApproved);
            ids.Add((int)Enums.TenderStatus.OffersOpenFinancialStagePending);
            ids.Add((int)Enums.TenderStatus.OffersOpenFinancialStageRejected);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningPending);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppeningRejected);
            ids.Add((int)Enums.TenderStatus.OffersTechnicalOppening);
            return ids;
        }


        #region LCG
        public async Task<TenderInfo> FindTenderInfo(string tenderReferenceId)
        {
            var result = await _context.Tenders
                 .Where(x => x.ReferenceNumber == tenderReferenceId)
                 .Select(x => new TenderInfo
                 {
                     ResponseStatusCode = 1,
                     ExpectedValue = x.EstimatedValue ?? 0,
                     GovernmentEntityId = x.AgencyCode,
                     MinThreshold = null,
                     OpenningBidsDate = x.OffersOpeningDate,
                     TenderName = x.TenderName,
                     TenderPurpose = x.Purpose,
                     TenderReferenceId = x.ReferenceNumber,
                     TenderSectorId = x.TenderActivities.Select(a => a.ActivityId).ToList()
                 }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<QueryResult<TenderInfo>> FindTenderList(string supplierId, int pageSize, int pageNumber)
        {

            var result = await _context.Tenders
                .Where(x => x.ConditionsBooklets.Any(c => c.CommericalRegisterNo == supplierId))
                .Select(x => new TenderInfo
                {
                    ExpectedValue = x.EstimatedValue ?? 0,
                    GovernmentEntityId = x.AgencyCode,
                    MinThreshold = null,
                    OpenningBidsDate = x.OffersOpeningDate,
                    TenderName = x.TenderName,
                    TenderPurpose = x.Purpose,
                    TenderReferenceId = x.ReferenceNumber,
                    TenderSectorId = x.TenderActivities.Select(a => a.ActivityId).ToList()
                }).ToQueryResult(pageNumber, pageSize);

            return result;
        }
        #endregion

        public async Task<TenderDates> GetTenderDatesByTenderId(int tenderId)
        {
            TenderDates tenderDates = await _context.TenderDates.Where(x => x.TenderId == tenderId).FirstOrDefaultAsync();
            return tenderDates;
        }


        public async Task<VersionHistory> GetCurrentActivityVersion()
        {
            var currentVersion = await _context.Versions.FirstOrDefaultAsync(version =>
                version.IsCurrentVersion && version.VersionTypeId == (int)Enums.VersionType.TenderActivity);

            return currentVersion;
        }

        public async Task<List<int>> GetTemplatesByActivityIdsAndVersionId(List<int> activityIds, int versionId)
        {
            var templateIds = await _context.ActivityVersions.
                Where(a => a.VersionId == versionId && activityIds.Contains(a.Activity.ActivityId))
                .Select(r => r.TemplateId.Value).Distinct().ToListAsync();
            return templateIds;
        }

        public async Task<int> GetCurrentTenderActivityVersion(int tenderId)
        {
            var activityVersionId = await _context.TenderVersions.
                Where(a => a.TenderId == tenderId && a.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity)
                .Select(a => a.VersionId).FirstOrDefaultAsync();

            return activityVersionId;
        }

        public long getLastItemNumber(string tenderId, long quantityTableId)
        {
            var LastItemNumber = _context.TenderQuantityTableItemsView.Where(x => x.QTableId == quantityTableId).Select(x => x.ItemNumber).Max();
            return LastItemNumber.HasValue ? LastItemNumber.Value : 0;
        }
        public async Task<List<long>> FindMandatoryListColumns(int tenderId, List<long> columnIds)
        {

            var tableids = await _context.Tenders.Include(d => d.TenderQuantityTables).Where(d => d.TenderId == tenderId).SelectMany(d => d.TenderQuantityTables).Select(f => f.Id).ToListAsync();
            var existItems = await _context.TenderQuantityTableItemsView.Where(x => tableids.Any(d => d == x.QTableId) && columnIds.Any(s => s == x.ColumnId)).Select(x => x.ColumnId).ToListAsync();
            return existItems;
        }

        public async Task<Tender> FindTenderWithLocalContentPreference(int tenderId)
        {
            return await _context.Tenders.Include(t => t.TenderLocalContent).FirstOrDefaultAsync(t => t.TenderId == tenderId);
        }
        public async Task<TenderLocalContent> GetTenderLocalContentByTenderId(int tenderId)
        {
            var result = await _context.TenderLocalContents.Where(x => x.TenderId == tenderId).FirstOrDefaultAsync();
            return result;
        }
    }
}