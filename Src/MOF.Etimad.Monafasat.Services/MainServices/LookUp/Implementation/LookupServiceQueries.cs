using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class LookupServiceQueries : ILookUpServiceQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RootConfigurations _rootConfiguration;

        public LookupServiceQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _rootConfiguration = rootConfiguration.Value;

        }

        public async Task<List<LookupModel>> FindAreas()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.Areas
                              .Select(a => new LookupModel
                              {
                                  Id = a.AreaId,
                                  Name = isArabic ? a.NameAr : a.NameEn
                              }).ToListAsync();
            return result;
        }
        public async Task<List<CountryModel>> FindCountries()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.Countries
                .Select(c => new CountryModel
                {
                    CountryId = c.CountryId,
                    Name = isArabic ? c.NameAr : c.NameEn,
                    IsGolf = c.IsGolf ?? false
                }).ToListAsync();
            return result;
        }
        public async Task<List<LookupModel>> FindCountriesLookup()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.Countries
                .Select(c => new LookupModel
                {
                    Id = c.CountryId,
                    Name = isArabic ? c.NameAr : c.NameEn
                }).ToListAsync();
            return result;
        }
        public async Task<CountryModel> FindCountryById(int countryId)
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.Countries.Where(c => c.CountryId == countryId)
                .Select(c => new CountryModel
                {
                    CountryId = c.CountryId,
                    Name = isArabic ? c.NameAr : c.NameEn,
                    IsGolf = c.IsGolf ?? false
                }).FirstAsync();
            return result;
        }
        public async Task<List<BankModel>> FindBanks()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.Banks.Select(c => new BankModel
            {
                BankId = c.BankId,
                Name = isArabic ? c.NameAr : c.NameEn
            }).ToListAsync();
            return result;
        }
        public async Task<List<LookupModel>> GetAllTenderTypes()
        {
            var statusList = await _context.TenderTypes
                .Where(x => x.TenderTypeId != (int)Enums.TenderType.PreQualification)
                .Where(x => x.TenderTypeId != (int)Enums.TenderType.PostQualification)
                .Where(x => x.TenderTypeId != (int)Enums.TenderType.SecondStageTender)
                  .Select(x => new LookupModel { Id = x.TenderTypeId, Name = x.NameAr }).ToListAsync();
            return statusList;
        }

        public async Task<List<LookupModel>> GetAgencyPurchaseMethodsModels(string agencyCode)
        {
            string agencyPurchaseMethods = _context.GovAgencies.Where(a => a.AgencyCode == agencyCode).Select(a => a.PurchaseMethods).FirstOrDefault();
            List<int> SelectedTypes;
            if (!string.IsNullOrEmpty(agencyPurchaseMethods))
            {
                SelectedTypes = agencyPurchaseMethods.Split(',').Where(a => int.Parse(a) != (int)Enums.TenderType.SecondStageTender && int.Parse(a) != (int)Enums.TenderType.PreQualification && int.Parse(a) != (int)Enums.TenderType.PostQualification).AsEnumerable().Select(r => int.Parse(r)).ToList();
            }
            else
            {
                SelectedTypes = new List<int>() { (int)Enums.TenderType.CurrentTender, (int)Enums.TenderType.CurrentDirectPurchase, (int)Enums.TenderType.NationalTransformationProjects };
            }
            List<LookupModel> SelectedTenderTypes = await _context.TenderTypes.Where(t => SelectedTypes.Contains(t.TenderTypeId))
            .Select(d => new LookupModel
            {
                Id = d.TenderTypeId,
                Name = d.NameAr
            }).ToListAsync();
            return SelectedTenderTypes;
        }
        public async Task<List<LookupModel>> GetAllYearQuarters()
        {
            var statusList = await _context.YearQuarters.Select(x => new LookupModel { Id = x.YearQuarterId, Name = x.NameAr }).ToListAsync();
            return statusList;
        }

        public async Task<List<LookupModel>> GetAllPrePlanningStatus()
        {
            var statusList = await _context.PrePlanningStatuses.Select(x => new LookupModel { Id = x.StatusId, Name = x.NameAr }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetAllBookletCertificates()
        {
            var statusList = await _context.VendorCertificates.Select(x => new LookupModel { Id = x.VendorCertificateId, Name = x.NameAr }).ToListAsync();
            return statusList;
        }

        public async Task<List<LookupModel>> GetAllLocalContentMechanismPreference()
        {
            var statusList = await _context.LocalContentMechanismPreference.Select(x => new LookupModel { Id = x.Id, Name = x.NameAr }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetAllLimitedTenderReasons()
        {
            var statusList = await _context.ReasonForLimitedTenderTypes
                  .Select(x => new LookupModel { Id = x.Id, Name = x.Name }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetAllSpendingCategories()
        {
            var statusList = await _context.SpendingCategories
                  .Select(x => new LookupModel { Id = x.SpendingCategoryId, Name = x.NameAr }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetAllRoles()
        {
            var statusList = await _context.UserRoles.Where(d => d.IsCombinedRole == false)
                  .Select(x => new LookupModel { Id = x.UserRoleId, Name = x.DisplayNameAr }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetAllOperationCodesCategoies()
        {
            var statusList = await _context.NotificationCategories
                  .Select(x => new LookupModel { Id = x.Id, Name = x.ArabicName }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetAllCancelationReasons()
        {
            var cancelationReasons = await _context.CancelationReasons
                  .Select(x => new LookupModel { Id = x.CancelationReasonId, Name = x.NameAr }).ToListAsync();
            return cancelationReasons;
        }
        public async Task<List<LookupModel>> GetAllBuyerSuppliers(int tenderId)
        {
            var BuyerSuppliers = await _context.ConditionsBooklets.Where(x => x.TenderId == tenderId && x.IsActive == true && x.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid)
                  .Select(x => new LookupModel { Name = x.CommericalRegisterNo, Value = x.Supplier.SelectedCrName }).ToListAsync();
            return BuyerSuppliers;
        }
        public async Task<List<LookupModel>> GetAllSuppliersHasPrequalifications(int preQualificationId)
        {
            var BuyerSuppliers = await _context.SupplierPreQualificationDocument.Where(x => x.PreQualificationId == preQualificationId && x.IsActive == true)
                  .Select(x => new LookupModel { Name = x.SupplierId, Value = x.Supplier.SelectedCrName }).ToListAsync();
            return BuyerSuppliers;
        }
        public async Task<List<LookupModel>> GetAllSuppliersHaveOffers(int tenderId)
        {
            var SuppliersHaveOffers = await _context.Offers.Where(x => x.TenderId == tenderId && x.IsActive == true && (x.OfferStatusId == (int)Enums.OfferStatus.Received || x.OfferStatusId == (int)Enums.OfferStatus.Excluded))
                  .Select(x => new LookupModel { Name = x.CommericalRegisterNo, Value = x.Supplier.SelectedCrName }).ToListAsync();
            return SuppliersHaveOffers;
        }
        public async Task<List<LookupModel>> GetAllDirectPurchaseTenderReasons()
        {
            var statusList = await _context.ReasonForPurchaseTenderTypes
                  .Select(x => new LookupModel { Id = x.Id, Name = x.Name }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetAllQuantityTableRowTypes()
        {
            var statusList = await _context.QuantityTableRowType
                  .Select(x => new LookupModel { Id = x.QuantityTableRowTypeId, Name = x.NameAr }).ToListAsync();
            return statusList;
        }

        public async Task<List<LookupModel>> GetAllPreQualifications(int tenderId, string agencyCode, int branchId)
        {
            var ReservedQualifications = _context.Tenders.Where(x => x.IsActive == true && x.TenderStatusId != (int)Enums.TenderStatus.Canceled
            && x.PreQualificationId != null
            && x.TenderId != tenderId
            ).Select(a => a.PreQualificationId);

            var statusList = await _context.Tenders.Where(
               x => x.TenderTypeId == (int)Enums.TenderType.PreQualification
            && x.IsActive == true
            && x.AgencyCode == agencyCode
            && x.BranchId == branchId
            && x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed && x.QualificationFinalResults.Any(f => f.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded)
            && !ReservedQualifications.Contains(x.TenderId)
            )
                .Select(x => new LookupModel
                {
                    Id = x.TenderId,
                    Name = x.TenderName + " " + x.ReferenceNumber
                }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetFinishedAnnouncementHasOneSupplier(int tenderId, string agencyCode, int branchId)
        {
            var ReservedAnnouncements = _context.Tenders.Where(x => x.IsActive == true && x.TenderStatusId != (int)Enums.TenderStatus.Canceled
            && x.PreAnnouncementId != null
            && x.TenderId != tenderId
            ).Select(a => a.PreAnnouncementId);

            var announcementList = await _context.Announcements
                     .Where(x => x.IsActive == true && (x.PublishedDate.Value.AddDays(x.AnnouncementPeriod)) < DateTime.Now)
                .Where(x => x.AgencyCode == agencyCode && x.BranchId == branchId)
                .Where(x => !ReservedAnnouncements.Contains(x.AnnouncementId))
                 .Where(x => x.AnnouncementJoinRequests.Count(a => a.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent) == (int)Enums.AnnouncementSuppliers.One)
                .Select(x => new LookupModel
                {
                    Id = x.AnnouncementId,
                    Name = x.AnnouncementName
                }).ToListAsync();
            return announcementList;
        }
        public async Task<List<LookupModel>> GetFinishedAnnouncementForLimitedTender(int tenderId, string agencyCode, int branchId)
        {
            var ReservedAnnouncements = _context.Tenders.Where(x => x.IsActive == true && x.TenderStatusId != (int)Enums.TenderStatus.Canceled
            && x.PreAnnouncementId != null
            && x.TenderId != tenderId
            ).Select(a => a.PreAnnouncementId);

            var announcementList = await _context.Announcements
               .Where(x => x.IsActive == true && (x.PublishedDate.Value.AddDays(x.AnnouncementPeriod)) < DateTime.Now)
                .Where(x => x.AgencyCode == agencyCode && x.BranchId == branchId)
                .Where(x => !ReservedAnnouncements.Contains(x.AnnouncementId))
                .Where(x => x.AnnouncementJoinRequests.Count(a => a.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent) > (int)Enums.AnnouncementSuppliers.One
                     && x.AnnouncementJoinRequests.Count(a => a.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent) <= (int)Enums.AnnouncementSuppliers.Five
            )
                .Select(x => new LookupModel
                {
                    Id = x.AnnouncementId,
                    Name = x.AnnouncementName
                }).ToListAsync();
            return announcementList;
        }
        public async Task<List<LookupModel>> GetAnnouncementSupplierTemplateForLimitedTender(int tenderId, string agencyCode, int selectedReasonId)
        {
            var announcementList = await _context.AnnouncementSupplierTemplate
                .Where(a => a.TenderTypesId.Contains(((int)Enums.TenderType.LimitedTender).ToString()))
               .Where(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && (x.EffectiveListDate >= DateTime.Now || x.EffectiveListDate == null))
                .Where(x => x.AgencyCode == agencyCode || x.LinkedAgenciesAnnouncements.Any(a => a.IsActive == true && a.AgencyCode == agencyCode && a.AnnouncementSupplierTemplate.TenderTypesId.Contains(((int)Enums.TenderType.LimitedTender).ToString()) && a.AnnouncementSupplierTemplate.AnnouncementJoinRequests.Any(j => j.IsActive == true && j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted)))
                .Where(x => x.AnnouncementJoinRequests.Count(a => a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) >= (int)Enums.AnnouncementSuppliers.One)
                .WhereIf(selectedReasonId != 0 && selectedReasonId == (int)Enums.ReasonForLimitedTenderType.PurchasesThatAvailableOnlyToLimitedNumberOfContractOrSuppliers,
                x => (x.AnnouncementJoinRequests.Count(a => a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) > (int)Enums.AnnouncementSuppliers.One
                      && x.AnnouncementJoinRequests.Count(a => a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) <= (int)Enums.AnnouncementSuppliers.Five
                      && (x.PublishedDate <= DateTime.Now.AddDays(-20)))
                      ||
               (x.LinkedAgenciesAnnouncements.Any(a => a.IsActive == true && a.AgencyCode == agencyCode && a.AnnouncementSupplierTemplate.PublishedDate <= DateTime.Now.AddDays(-20) && a.AnnouncementSupplierTemplate.AnnouncementJoinRequests.Count(j => j.IsActive == true && j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) > 1)
                      && x.LinkedAgenciesAnnouncements.Any(a => a.IsActive == true && a.AgencyCode == agencyCode && a.AnnouncementSupplierTemplate.PublishedDate <= DateTime.Now.AddDays(-20) && a.AnnouncementSupplierTemplate.AnnouncementJoinRequests.Count(j => j.IsActive == true && j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) <= 5))


                      )

                .Select(x => new LookupModel
                {
                    Id = x.AnnouncementId,
                    Name = x.AnnouncementName
                }).ToListAsync();
            return announcementList;
        }

        public async Task<List<LookupModel>> GetAnnouncementSupplierTemplateForDirectPurchaseTender(string agencyCode)
        {
            var announcementList = await _context.AnnouncementSupplierTemplate
                .Where(a => a.TenderTypesId.Contains(((int)Enums.TenderType.NewDirectPurchase).ToString()))
               .Where(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && (x.EffectiveListDate >= DateTime.Now || x.EffectiveListDate == null))
                .Where(x => x.AgencyCode == agencyCode || x.LinkedAgenciesAnnouncements.Any(a => a.IsActive == true && a.AgencyCode == agencyCode && a.AnnouncementSupplierTemplate.TenderTypesId.Contains(((int)Enums.TenderType.NewDirectPurchase).ToString()) && a.AnnouncementSupplierTemplate.AnnouncementJoinRequests.Any(j => j.IsActive == true && j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted)))
                .Where(x => x.AnnouncementJoinRequests.Any(a => a.IsActive == true && a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted))
                .Select(x => new LookupModel
                {
                    Id = x.AnnouncementId,
                    Name = x.AnnouncementName
                }).ToListAsync();
            return announcementList;
        }


        public async Task<List<LookupModel>> GetSuccededPreQualificationsForTenderCreation(int branchId)
        {
            var days = int.Parse(_rootConfiguration.AwardingConfiguration.QualifationsValidityDays);

            var prequalifications = await _context.Tenders
                .Include(x => x.TenderHistories)
                .Where(x => x.TenderTypeId == (int)Enums.TenderType.PreQualification && x.IsActive == true && x.BranchId == branchId)
                .Where(x => x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed && x.TenderHistories.Any(h => h.StatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed) && x.QualificationFinalResults.Any(f => f.IsActive == true && f.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded))

                .ToListAsync();

            var prequalificationList = prequalifications
                .Where(x => (DateTime.Now - x.TenderHistories.FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed).CreatedAt).TotalDays < days)
                .Select(x => new LookupModel
                {
                    Id = x.TenderId,
                    Name = x.TenderName + " " + x.ReferenceNumber
                }).ToList();
            return prequalificationList;
        }

        public async Task<List<ActivityModel>> FindActivities()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.Activities.Where(a => a.ParentID == null).Include(b => b.SubActivities)
                .Select(x => new ActivityModel
                {
                    ActivityId = x.ActivityId,
                    Name = isArabic ? x.NameAr : x.NameEn,
                    SubActivities = x.SubActivities.Select(s => new ActivityModel
                    {
                        ActivityId = s.ActivityId,
                        Name = isArabic ? s.NameAr : s.NameEn
                    }).ToList()
                }).ToListAsync();
            return result;
        }


        public async Task<List<ActivityModel>> FindActivitiesWithVersion(int activityVersionId)
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.ActivityVersions.Include(act=>act.Activity).ThenInclude(sub=>sub.SubActivities)
                .Where(a => a.VersionId == activityVersionId)
                .Where(a => a.Activity.ParentID == null)
                .Select(x => new ActivityModel
                {
                    ActivityId = x.ActivityId,
                    Name = isArabic ? x.Activity.NameAr : x.Activity.NameEn,
                    SubActivities = x.Activity.SubActivities.Where(sub=>sub.ActivityTemplateVersions.Any(av => av.VersionId == activityVersionId)).Select(s => new ActivityModel
                    {
                        ActivityId = s.ActivityId,
                        Name = isArabic ? s.NameAr : s.NameEn
                    }).ToList()
                }).ToListAsync();
            return result;
        }

        public async Task<List<ActivityModel>> GetMainActivities()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.Activities.Where(a => a.ParentID == null)
                .Select(x => new ActivityModel
                {
                    ActivityId = x.ActivityId,
                    Name = isArabic ? x.NameAr : x.NameEn
                }).ToListAsync();
            return result;
        }
        public async Task<List<LookupModel>> GetMainActivitiesByParentId(int? parentId = null)
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.Activities.Where(a => a.ParentID == (parentId == 0 ? null : parentId))
                .Select(x => new LookupModel
                {
                    Id = x.ActivityId,
                    Name = isArabic ? x.NameAr : x.NameEn
                }).ToListAsync();
            return result;
        }
        public async Task<List<ConstructionWorkModel>> FindConstructionWorks()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.ConstructionWorks.Where(a => a.ParentID == null).Include(b => b.SubWorks)
                .Select(x => new ConstructionWorkModel
                {
                    ConstructionWorkId = x.ConstructionWorkId,
                    Name = isArabic ? x.NameAr : x.NameEn,
                    SubWorks = x.SubWorks.Select(s => new ConstructionWorkModel
                    {
                        ConstructionWorkId = s.ConstructionWorkId,
                        Name = isArabic ? s.NameAr : s.NameEn,
                    }).ToList()
                }).ToListAsync();
            return result;
        }

        public async Task<List<int>> FindActivitiesWithYears()
        {
            var result = await _context.Activities.Where(a => a.ActivityTemplateVersions.Any(a=>a.HasYears == true))
                .Select(x => x.ActivityId).ToListAsync();
            return result;
        }
        public async Task<List<int>> FindActivitiesWithYearsWithVersion(int activityVersionId)
        {
            var result = await _context.Activities.Where(a => a.ActivityTemplateVersions.Any(a => a.HasYears == true && a.VersionId == activityVersionId))
                .Select(x => x.ActivityId).ToListAsync();
            return result;
        }

        public async Task<List<MaintenanceRunningWorkModel>> FindMentainanceWorks()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var result = await _context.MaintenanceRunningWorks
                .Select(x => new MaintenanceRunningWorkModel
                {
                    MaintenanceRunningWorkId = x.MaintenanceRunningWorkId,
                    Name = isArabic ? x.NameAr : x.NameEn
                }).ToListAsync();
            return result;
        }
        public async Task<List<Committee>> GetAllCommittesByAgency(string agencyCode)
        {
            Check.ArgumentNotNullOrEmpty(nameof(agencyCode), agencyCode);
            var committeeList = await _context.Committees
             .Where(t => t.AgencyCode == agencyCode).ToListAsync();
            return committeeList;
        }
        public async Task<List<LookupModel>> GetNegotiationTypes()
        {
            var ReductionReason = await _context.NegotiationTypes.Select(x => new LookupModel { Id = x.NegotiationTypeId, Name = x.Name }).OrderByDescending(d => d.Id).ToListAsync();
            return ReductionReason;
        }
        public async Task<List<LookupModel>> GetReductionReasons()
        {
            var ReductionReason = await _context.NegotiationReasons.Select(x => new LookupModel { Id = x.NegotiationReasonId, Name = x.Name }).ToListAsync();
            return ReductionReason;
        }
        public async Task<List<LookupModel>> FindTechnicalCommitteeByAgencyCode(string agencyCode, int userId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(agencyCode), agencyCode);
            Check.ArgumentNotNullOrEmpty(nameof(userId), userId.ToString());
            var committeeList = await _context.Committees
             .Where(t => t.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee && t.AgencyCode == agencyCode && !t.CommitteeUsers.Any(c => c.UserProfileId == userId))
                  .Select(x => new LookupModel { Id = x.CommitteeId, Name = x.CommitteeName }).ToListAsync();
            return committeeList;
        }
        public async Task<List<LookupModel>> FindTechnicalCommitteeByAgencyCodeAndCommitteeId(string agencyCode, int committeeId)
        {
            var committeeList = await _context.Committees
             .Where(t => t.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee && t.AgencyCode == agencyCode && t.CommitteeId != committeeId && t.IsActive == true
             && t.CommitteeUsers.Any(c => c.IsActive == true) && t.BranchCommittees.Count == 0)
                  .Select(x => new LookupModel { Id = x.CommitteeId, Name = x.CommitteeName }).ToListAsync();
            return committeeList;
        }
        public async Task<List<LookupModel>> FindTechnicalCommitteeByBranchIdAndCommiteeId(int branchId, int committeeId)
        {
            var branchIdList = await _context.BranchCommittees
            .Where(t => t.Committee.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee && t.BranchId == branchId && t.CommitteeId != committeeId && t.IsActive == true)
            .Select(x => new LookupModel { Id = x.CommitteeId, Name = x.Committee.CommitteeName }).ToListAsync();
            return branchIdList;
        }

        public async Task<Tender> FindTenderById(int tenderId)
        {
            var tender = await _context.Tenders.Where(x => x.TenderId == tenderId).SingleOrDefaultAsync();
            return tender;
        }

        public async Task<List<LookupModel>> GetTenderStatusLookup()
        {
            List<LookupModel> excludFromTenderStatus = new List<LookupModel>();
            excludFromTenderStatus.Add(new LookupModel() { Id = (int)Enums.TenderStatus.VROOffersTechnicalChecking });
            excludFromTenderStatus.Add(new LookupModel() { Id = (int)Enums.TenderStatus.PendingQualificationCommitteeManagerApproval });
            excludFromTenderStatus.Add(new LookupModel() { Id = (int)Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager });
            excludFromTenderStatus.Add(new LookupModel() { Id = (int)Enums.TenderStatus.QualificationUnderEstablishingFromCommittee });
            excludFromTenderStatus.Add(new LookupModel() { Id = (int)Enums.TenderStatus.BiddingOffersCheckedPending });
            excludFromTenderStatus.Add(new LookupModel() { Id = (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed });
            excludFromTenderStatus.Add(new LookupModel() { Id = (int)Enums.TenderStatus.BiddingOffersCheckedRejected });
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var statusList = await _context.TenderStatus
                .Where(q => q.TenderStatusId != (int)Enums.TenderStatus.Established)
                  .Select(x => new LookupModel
                  {
                      Id = x.TenderStatusId,
                      Name = isArabic ? x.NameAr : x.NameEn
                  }).ToListAsync();

            return statusList.Where(e1 => excludFromTenderStatus.All(e2 => e2.Id != e1.Id)).ToList();
        }

        public async Task<List<LookupModel>> GetVROTenderStatusLookup()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var statusList = await _context.TenderStatus
                .Where(q => q.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing
                || q.TenderStatusId == (int)Enums.TenderStatus.Approved
                || q.TenderStatusId == (int)Enums.TenderStatus.Pending
                || q.TenderStatusId == (int)Enums.TenderStatus.Rejected
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalChecking
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved
                || q.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingPending
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved
                || q.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected
                || q.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding
                || q.TenderStatusId == (int)Enums.TenderStatus.PendingVROAuditerApprove
                || q.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedPending
                || q.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed
                || q.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedRejected
                )
                  .Select(x => new LookupModel
                  {
                      Id = x.TenderStatusId,
                      Name = isArabic ? x.NameAr : x.NameEn
                  }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetQualificationStatusLookup()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            List<int> QualificationStatusList = new List<int>
            {
                (int)Enums.TenderStatus.Approved,
                (int)Enums.TenderStatus.UnderEstablishing,
                (int)Enums.TenderStatus.DocumentChecking,
                (int)Enums.TenderStatus.DocumentCheckPending,
                (int)Enums.TenderStatus.DocumentCheckConfirmed,
                (int)Enums.TenderStatus.DocumentCheckRejected,
                (int)Enums.TenderStatus.StartNegotiation
            };
            if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.Auditer) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.DataEntry)
               )
            {
                QualificationStatusList.AddRange(new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established,
                    (int)Enums.TenderStatus.Rejected,
                    (int)Enums.TenderStatus.Pending
                });
            }
            if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.FinancialSupervisor))
            {
                QualificationStatusList.AddRange(new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.QualificationCommitteeApproval,
                    (int)Enums.TenderStatus.Rejected,
                    (int)Enums.TenderStatus.Pending,
                    (int)Enums.TenderStatus.Canceled
                });
            }
            var statusList = await _context.TenderStatus
                .Where(q => QualificationStatusList.Contains(q.TenderStatusId))
                  .Select(x => new LookupModel
                  {
                      Id = x.TenderStatusId,
                      Name = isArabic ? x.NameAr : x.NameEn
                  }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetApprovedTenderStatusLookup()
        {
            var isArabic = _httpContextAccessor.HttpContext.IsArabic();
            var statusList = await _context.TenderStatus
                .Where(q => q.TenderStatusId != (int)Enums.TenderStatus.Established && q.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && q.TenderStatusId != (int)Enums.TenderStatus.Rejected && q.TenderStatusId != (int)Enums.TenderStatus.Pending)
                  .Select(x => new LookupModel
                  {
                      Id = x.TenderStatusId,
                      Name = isArabic ? x.NameAr : x.NameEn
                  }).ToListAsync();
            return statusList;
        }
        public async Task<List<LookupModel>> GetCities()
        {
            var citiesList = await _context.Cities.Select(x => new LookupModel
            {
                Id = x.CityID,
                Name = x.NameArabic
            }).ToListAsync();
            return citiesList;
        }
        public async Task<List<LookupModel>> GetAnnounementStatusLookup()
        {
            var announecementStatusList = await _context.AnnouncementStatus.Select(x => new LookupModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return announecementStatusList;
        }

        public async Task<List<LookupModel>> GetAnnouncementStatusSupplierTemplatesLookup()
        {
            var announecementStatusList = await _context.AnnouncementStatusSupplierTemplate
                .WhereIf(!_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.DataEntry) && !_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.Auditer),
                a => a.Id != (int)Enums.AnnouncementSupplierTemplateStatus.UnderCreation
                && a.Id != (int)Enums.AnnouncementSupplierTemplateStatus.Pending
                && a.Id != (int)Enums.AnnouncementSupplierTemplateStatus.Rejected
                && a.Id != (int)Enums.AnnouncementSupplierTemplateStatus.ReadyForApproval)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            return announecementStatusList;
        }

        public async Task<List<LookupModel>> GetBranchAddressTypes()
        {
            var citiesList = await _context.AddressTypes.Select(x => new LookupModel
            {
                Id = x.AddressTypeId,
                Name = x.AddressTypeName
            }).ToListAsync();
            return citiesList;
        }
        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        #region Unit
        public async Task<List<LookupModel>> GetUnitModificationsTypes()
        {
            var ReductionReason = await _context.TenderUnitUpdateTypes.Select(x => new LookupModel { Id = x.TenderUnitUpdateTypeId, Name = x.Name }).ToListAsync();
            return ReductionReason;
        }
        #endregion

        public async Task<List<TenderTypeModel>> GetTenderTypes()
        {
            return await _context.TenderTypes
                .Where(r => r.TenderTypeId != (int)Enums.TenderType.PreQualification && r.TenderTypeId != (int)Enums.TenderType.PostQualification)
                .Select(x => new TenderTypeModel
                {
                    TenderTypeId = x.TenderTypeId,
                    TenderTypeName = x.NameAr
                }).ToListAsync();
        }

    }
}
