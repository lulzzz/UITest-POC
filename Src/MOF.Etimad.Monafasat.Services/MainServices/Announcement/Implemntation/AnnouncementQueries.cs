using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementQueries : IAnnouncementQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AnnouncementQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<LookupModel> GetAnnouncementNameByAnnouncementId(int announcementId)
        {
            var announcementName = await _context.Announcements
            .Where(a => a.AnnouncementId == announcementId)
            .Select(a => new LookupModel
            {
                Name = a.AnnouncementName
            }).FirstOrDefaultAsync();
            return announcementName;
        }
        public async Task<AnnouncementDetailsModel> FindAnnouncementDetailsByAnnouncementId(int announcementId)
        {
            var IsAnnouncementLinkedToTender = await _context.Tenders.Where(t => t.IsActive == true && t.PreAnnouncementId == announcementId).AnyAsync();

            var CurrentRoleName = _httpContextAccessor.HttpContext.User.UserRole();
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            var BranchId = _httpContextAccessor.HttpContext.User.UserBranch();

            var result = await _context.Announcements
                .Where(a => a.IsActive == true)
               .WhereIf(!string.IsNullOrEmpty(agencyCode) && !new List<string> { RoleNames.MonafasatAccountManager, RoleNames.CustomerService }.Contains(CurrentRoleName), a => a.AgencyCode == agencyCode)
               .WhereIf(BranchId != 0, a => a.BranchId == BranchId)
               .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementDetailsModel
               {
                   AnnouncementId = a.AnnouncementId,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                   AnnouncementName = a.AnnouncementName,
                   AgencyName = a.Agency.NameArabic,
                   ActivityDescription = a.ActivityDescription,
                   AnnouncementPeriod = a.AnnouncementPeriod,
                   Areas = a.AnnouncementAreas.Select(c => new LookupModel
                   {
                       Id = c.Area.AreaId,
                       Name = c.Area.NameAr
                   }).ToList(),
                   TenderActivities = a.AnnouncementActivities.Select(c => new ActivityModel
                   {
                       ActivityId = c.Activity.ActivityId,
                       Name = _httpContextAccessor.HttpContext.IsArabic() ? c.Activity.NameAr : c.Activity.NameEn,
                       SubActivities = c.Activity.SubActivities.Select(s => new ActivityModel
                       {
                           ActivityId = s.ActivityId,
                           Name = _httpContextAccessor.HttpContext.IsArabic() ? s.NameAr : s.NameEn
                       }).ToList()
                   }).ToList(),
                   ConstructionWorks = a.AnnouncementConstructionWorks.Select(c => new ConstructionWorkModel
                   {
                       ConstructionWorkId = c.ConstructionWork.ConstructionWorkId,
                       Name = _httpContextAccessor.HttpContext.IsArabic() ? c.ConstructionWork.NameAr : c.ConstructionWork.NameEn,
                       SubWorks = c.ConstructionWork.SubWorks.Select(s => new ConstructionWorkModel
                       {
                           ConstructionWorkId = s.ConstructionWorkId,
                           Name = _httpContextAccessor.HttpContext.IsArabic() ? s.NameAr : s.NameEn
                       }).ToList()
                   }).ToList(),
                   HasJoinRequest = a.AnnouncementJoinRequests.Any(d => d.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent),
                   MaintenanceOperationActions = a.AnnouncementMaintenanceRunnigWorks.Select(c => new MaintenanceRunningWorkModel
                   {
                       MaintenanceRunningWorkId = c.MaintenanceRunningWork.MaintenanceRunningWorkId,
                       Name = _httpContextAccessor.HttpContext.IsArabic() ? c.MaintenanceRunningWork.NameAr : c.MaintenanceRunningWork.NameEn
                   }).ToList(),
                   Countries = a.AnnouncementCountries.Select(c => new LookupModel
                   {
                       Id = c.Country.CountryId,
                       Name = _httpContextAccessor.HttpContext.IsArabic() ? c.Country.NameAr : c.Country.NameEn
                   }).ToList(),
                   AuditedBy = a.ApprovedBy,
                   CreatedBy = a.CreatedBy,
                   BranchName = a.Branch.BranchName,
                   Details = a.Details,
                   InsideKSA = a.InsidKsa,
                   IntroAboutTender = a.IntroAboutTender,
                   PublishedDate = a.PublishedDate,
                   ReferenceNumber = a.ReferenceNumber,
                   StatusName = a.AnnouncementStatusId == (int)Enums.AnnouncementStatus.Ended ? "منتهي" : a.AnnouncementStatus.Name,
                   StatusId = a.AnnouncementStatusId,
                   RejectionReason = a.AnnouncementHistories.Where(h => h.StatusId == (int)Enums.AnnouncementStatus.Rejected).OrderByDescending(h => h.Id).FirstOrDefault() != null ? a.AnnouncementHistories.Where(h => h.StatusId == (int)Enums.AnnouncementStatus.Rejected).OrderByDescending(h => h.Id).FirstOrDefault().RejectionReason : null,
                   TenderTypeName = _httpContextAccessor.HttpContext.IsArabic() ? a.TenderType.NameAr : a.TenderType.NameEn,
                   IsAnnouncementLinkedToTender = IsAnnouncementLinkedToTender,
                   AnnouncementJoinRequestsCount = a.AnnouncementJoinRequests.Count(s => s.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent),
                   AgencyCode = agencyCode
               }).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new UnHandledAccessException();
            }
            else
            {
                if (result.AnnouncementJoinRequestsCount > 0)
                    result.JoinedSupplierList = await _context.AnnouncementJoinRequests.Include(a => a.Supplier).Where(j => j.IsActive == true && j.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent
                                                            && j.Announcement.IsActive == true && j.Announcement.AnnouncementId == announcementId).
                                                            ToDictionaryAsync(s => s.Cr, s => s.Supplier.SelectedCrName);
                if (result.AnnouncementJoinRequestsCount == (int)Enums.AnnouncementSuppliers.One)
                {
                    result.TenderTypeIdString = Util.Encrypt((int)Enums.TenderType.NewDirectPurchase);
                    result.PurchaseMethodName = Resources.TenderResources.DisplayInputs.DirectPurshase;
                }
                else if (result.AnnouncementJoinRequestsCount > (int)Enums.AnnouncementSuppliers.One && result.AnnouncementJoinRequestsCount <= (int)Enums.AnnouncementSuppliers.Five)
                {
                    result.TenderTypeIdString = Util.Encrypt((int)Enums.TenderType.LimitedTender);
                    result.PurchaseMethodName = Resources.TenderResources.DisplayInputs.LimitedTender;
                }
                else if (result.AnnouncementJoinRequestsCount > (int)Enums.AnnouncementSuppliers.Five)
                {
                    result.TenderTypeIdString = Util.Encrypt((int)Enums.TenderType.NewTender);
                    result.PurchaseMethodName = Resources.TenderResources.DisplayInputs.GeneralTender;
                }
            }
            return result;
        }
        public async Task<SupplierAnnouncementDetailsModel> FindAnnouncementDetailsForSupplierByAnnouncementId(int announcementId, string Cr)
        {
            var result = await _context.Announcements
               .Where(a => a.AnnouncementId == announcementId)
              .Select(a => new SupplierAnnouncementDetailsModel
              {
                  AgencyCode = a.AgencyCode,
                  AnnouncementId = a.AnnouncementId,
                  AnnouncementStatusId = a.AnnouncementStatusId,
                  AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                  AnnouncementName = a.AnnouncementName,
                  AgencyName = a.Agency.NameArabic,
                  ActivityDescription = a.ActivityDescription,
                  AnnouncementPeriod = a.AnnouncementPeriod,
                  Areas = a.AnnouncementAreas.Select(c => new LookupModel
                  {
                      Id = c.Area.AreaId,
                      Name = c.Area.NameAr
                  }).ToList(),
                  TenderActivities = a.AnnouncementActivities.Select(c => new ActivityModel
                  {
                      ActivityId = c.Activity.ActivityId,
                      Name = _httpContextAccessor.HttpContext.IsArabic() ? c.Activity.NameAr : c.Activity.NameEn,
                      SubActivities = c.Activity.SubActivities.Select(s => new ActivityModel
                      {
                          ActivityId = s.ActivityId,
                          Name = _httpContextAccessor.HttpContext.IsArabic() ? s.NameAr : s.NameEn
                      }).ToList()
                  }).ToList(),
                  ConstructionWorks = a.AnnouncementConstructionWorks.Select(c => new ConstructionWorkModel
                  {
                      ConstructionWorkId = c.ConstructionWork.ConstructionWorkId,
                      Name = _httpContextAccessor.HttpContext.IsArabic() ? c.ConstructionWork.NameAr : c.ConstructionWork.NameEn,
                      SubWorks = c.ConstructionWork.SubWorks.Select(s => new ConstructionWorkModel
                      {
                          ConstructionWorkId = s.ConstructionWorkId,
                          Name = _httpContextAccessor.HttpContext.IsArabic() ? s.NameAr : s.NameEn
                      }).ToList()
                  }).ToList(),
                  hasJoinRequest = a.AnnouncementJoinRequests.Any(d => d.Cr == Cr && d.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent),
                  JoinRequestStatusId = a.AnnouncementJoinRequests.OrderByDescending(d => d.CreatedAt).FirstOrDefault(d => d.Cr == Cr).StatusId,
                  JoinRequestStatusName = a.AnnouncementJoinRequests.Any(d => d.Cr == Cr) ? a.AnnouncementJoinRequests.OrderByDescending(d => d.CreatedAt).FirstOrDefault(d => d.Cr == Cr).joinRequestStatus.NameAr : "",
                  MaintenanceOperationActions = a.AnnouncementMaintenanceRunnigWorks.Select(c => new MaintenanceRunningWorkModel
                  {
                      MaintenanceRunningWorkId = c.MaintenanceRunningWork.MaintenanceRunningWorkId,
                      Name = _httpContextAccessor.HttpContext.IsArabic() ? c.MaintenanceRunningWork.NameAr : c.MaintenanceRunningWork.NameEn
                  }).ToList(),
                  Countries = a.AnnouncementCountries.Select(c => new LookupModel
                  {
                      Id = c.Country.CountryId,
                      Name = _httpContextAccessor.HttpContext.IsArabic() ? c.Country.NameAr : c.Country.NameEn
                  }).ToList(),
                  AuditedBy = a.ApprovedBy,
                  CreatedBy = a.CreatedBy,
                  BranchName = a.Branch.BranchName,
                  Details = a.Details,
                  InsideKSA = a.InsidKsa,
                  IntroAboutTender = a.IntroAboutTender,
                  PublishedDate = a.PublishedDate,
                  ReferenceNumber = a.ReferenceNumber,
                  StatusName = a.AnnouncementStatusId == (int)Enums.AnnouncementStatus.Ended ? "منتهي" : a.AnnouncementStatus.Name,
                  TenderTypeName = _httpContextAccessor.HttpContext.IsArabic() ? a.TenderType.NameAr : a.TenderType.NameEn,
                  AnnouncementJoinRequestsCount = a.AnnouncementJoinRequests.Count(s => s.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent)
              }).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new UnHandledAccessException();
            }
            else
            {
                if (result.AnnouncementJoinRequestsCount > 0)
                    result.JoinedSupplierList = await _context.AnnouncementJoinRequests.Include(a => a.Supplier).Where(j => j.IsActive == true && j.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent
                                                            && j.Announcement.IsActive == true && j.Announcement.AnnouncementId == announcementId).
                                                            ToDictionaryAsync(s => s.Cr, s => s.Supplier.SelectedCrName);
                if (result.AnnouncementJoinRequestsCount == (int)Enums.AnnouncementSuppliers.One)
                {
                    result.PurchaseMethodName = Resources.TenderResources.DisplayInputs.DirectPurshase;
                }
                else if (result.AnnouncementJoinRequestsCount > (int)Enums.AnnouncementSuppliers.One && result.AnnouncementJoinRequestsCount <= (int)Enums.AnnouncementSuppliers.Five)
                {
                    result.PurchaseMethodName = Resources.TenderResources.DisplayInputs.LimitedTender;
                }
                else if (result.AnnouncementJoinRequestsCount > (int)Enums.AnnouncementSuppliers.Five)
                {
                    result.PurchaseMethodName = Resources.TenderResources.DisplayInputs.GeneralTender;
                }
            }
            return result;
        }
        public async Task<Announcement> FindAnnouncementByAnnouncementId(int announcementId)
        {
            var announcement = await _context.Announcements.Include(d => d.AnnouncementJoinRequests).FirstOrDefaultAsync(d => d.AnnouncementId == announcementId);
            return announcement;
        }

        public async Task<QueryResult<SupplierGridAnnouncementModel>> FindSupplierAnnouncements(SupplierAnnouncementSearchCriteria cretria)
        {
            var query = await _context.Announcements.
                Where(d => d.AnnouncementJoinRequests.Any(d => d.Cr == cretria.CommericalRegisterNo && d.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent))
                .WhereIf(!string.IsNullOrEmpty(cretria.AnnouncementName), f => f.AnnouncementName.Contains(cretria.AnnouncementName))
                .WhereIf(!string.IsNullOrEmpty(cretria.ReferenceNumber), f => f.ReferenceNumber == cretria.ReferenceNumber)
                .WhereIf(!string.IsNullOrEmpty(cretria.AgencyCode), f => f.AgencyCode == cretria.AgencyCode)
                .WhereIf(cretria.TenderTypeId.HasValue, a => a.TenderTypeId == cretria.TenderTypeId)
                .OrderByDescending(a => a.PublishedDate)
                .Select(d =>
                new SupplierGridAnnouncementModel
                {
                    ActivityDescription = d.ActivityDescription,
                    agencyName = d.Agency.NameArabic,
                    announcementStatusName = d.AnnouncementStatusId == (int)Enums.AnnouncementStatus.Ended ? "منتهي" : d.AnnouncementStatus.Name,
                    announcementTypeName = d.TenderType.NameAr,
                    ReferenceNumber = d.ReferenceNumber,
                    AnnouncementName = d.AnnouncementName,
                    JoinRequestStatusId = d.AnnouncementJoinRequests.OrderByDescending(d => d.CreatedAt).FirstOrDefault(f => f.Cr == cretria.CommericalRegisterNo).StatusId,
                    JoinRequestStatusName = d.AnnouncementJoinRequests.OrderByDescending(d => d.CreatedAt).FirstOrDefault(f => f.Cr == cretria.CommericalRegisterNo).joinRequestStatus.NameAr,
                    publishDate = d.PublishedDate,
                    lastApplyDate = d.LastApplyingRequestsDate,
                    Id = d.AnnouncementId,
                    announcementPeriod = d.AnnouncementPeriod,
                    AnnouncementStatusId = d.AnnouncementStatusId
                }
            ).ToQueryResult(cretria.PageNumber, cretria.PageSize);
            foreach (var item in query.Items)
            {
                item.lastApplyDateString = item.lastApplyDate.HasValue ? item.lastApplyDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
                item.publishDateString = item.publishDate.HasValue ? item.publishDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
                item.AnnouncementIdString = Util.Encrypt(item.Id);
            }
            return query;
        }

        public async Task<QueryResult<AllAnouuncementsForSupplierVisitorModel>> FindAllSupplierAnnouncements(AllSupplierAnnouncementSearchCriteria criteria)
        {
            var result = await _context.Announcements
                .Where(a => a.IsActive == true)
                .Where(a => a.StatusId == (int)Enums.AnnouncementStatus.Approved || a.StatusId == (int)Enums.AnnouncementStatus.Ended)
                .WhereIf(criteria.ActivityId.HasValue, a => a.AnnouncementActivities.Any(aa => aa.Activity.ParentID == criteria.ActivityId))
                .WhereIf(criteria.SubActivityId.HasValue, a => a.AnnouncementActivities.Any(aa => aa.ActivityId == criteria.SubActivityId))
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), a => a.AgencyCode == criteria.AgencyCode)
                .WhereIf(criteria.AreaId.HasValue, a => a.AnnouncementAreas.Any(aa => aa.AreaId == criteria.AreaId))
                .WhereIf(!string.IsNullOrEmpty(criteria.AnnouncementName), a => a.AnnouncementName.Contains(criteria.AnnouncementName))
                .WhereIf(!string.IsNullOrEmpty(criteria.ReferenceNumber), a => a.ReferenceNumber == criteria.ReferenceNumber)
                .WhereIf(criteria.TenderTypeId.HasValue, a => a.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(criteria.FromLastJoinDate.HasValue, a => a.PublishedDate.Value.Date.AddDays(a.AnnouncementPeriod) >= criteria.FromLastJoinDate.Value)
                .WhereIf(criteria.ToLastJoinDate.HasValue, a => a.PublishedDate.Value.Date.AddDays(a.AnnouncementPeriod) <= criteria.ToLastJoinDate.Value)
                .WhereIf(criteria.FromPublishDate.HasValue, a => a.PublishedDate.Value >= criteria.FromPublishDate.Value)
                .WhereIf(criteria.AnnouncementActiveStatusId.HasValue && criteria.AnnouncementActiveStatusId == (int)Enums.AnnouncementActiveStatus.Active,
                                                a => a.StatusId == (int)Enums.AnnouncementStatus.Approved && a.PublishedDate.HasValue && a.PublishedDate.Value.AddDays(a.AnnouncementPeriod) > DateTime.Now
                                                && (!string.IsNullOrEmpty(criteria.CommericalRegisterNo) || !a.AnnouncementJoinRequests.Any(aj => aj.Cr == criteria.CommericalRegisterNo && aj.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent)))
                .WhereIf(criteria.AnnouncementActiveStatusId.HasValue && criteria.AnnouncementActiveStatusId == (int)Enums.AnnouncementActiveStatus.InActive,
                                                a => (a.PublishedDate.HasValue && a.PublishedDate.Value.AddDays(a.AnnouncementPeriod) < DateTime.Now)
                                                || a.AnnouncementJoinRequests.Any(aj => aj.Cr == criteria.CommericalRegisterNo && aj.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent))
                .OrderByDescending(a => a.PublishedDate)
                .Select(announcement => new AllAnouuncementsForSupplierVisitorModel
                {
                    AnnouncementIdString = Util.Encrypt(announcement.AnnouncementId),
                    AnnouncementName = announcement.AnnouncementName,
                    ReferenceNumber = announcement.ReferenceNumber,
                    AnnouncementStatusId = announcement.AnnouncementStatusId,
                    AgencyName = announcement.Agency.NameArabic,
                    AgencyCode = announcement.AgencyCode,
                    hasApprovedRequest = announcement.AnnouncementJoinRequests.Any(aj => aj.Cr == criteria.CommericalRegisterNo && aj.StatusId == (int)Enums.AnnouncementJoinRequestStatus.Sent),
                    TenderTypeString = announcement.TenderType.NameAr,
                    AnnouncementPeriod = announcement.AnnouncementPeriod,
                    PublishedDate = announcement.PublishedDate,
                    AnnouncementStatusString = announcement.AnnouncementStatusId == (int)Enums.AnnouncementStatus.Ended ? "منتهي" : announcement.AnnouncementStatus.Name
                }
            ).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<AllAnouuncementsForAgencyModel>> FindAllAgencyAnnouncements(AllAgencyAnnouncementSearchCriteriaModel criteria)
        {
            var result = await _context.Announcements
                .Where(a => a.IsActive == true)
                .WhereIf(criteria.Type == "Current", x => x.StatusId == (int)Enums.AnnouncementStatus.Approved && x.PublishedDate.Value.AddDays(x.AnnouncementPeriod) > DateTime.Now)
                .WhereIf(criteria.Type == "Finished", a => a.PublishedDate.Value.AddDays(a.AnnouncementPeriod) < DateTime.Now)
                .WhereIf(!string.IsNullOrEmpty(criteria.CurrentAgencyCode) && !new List<string> { RoleNames.MonafasatAccountManager, RoleNames.CustomerService }.Contains(criteria.CurrentRoleName), a => a.AgencyCode == criteria.CurrentAgencyCode)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), a => a.AgencyCode == criteria.AgencyCode)
                .WhereIf(criteria.BranchId.HasValue, a => a.BranchId == criteria.BranchId)
                .WhereIf(criteria.CurrentBranchId.HasValue && criteria.CurrentBranchId.Value != 0, a => a.BranchId == criteria.CurrentBranchId)
                .WhereIf(!string.IsNullOrEmpty(criteria.AnnouncementName), a => a.AnnouncementName.Contains(criteria.AnnouncementName))
                .WhereIf(!string.IsNullOrEmpty(criteria.ReferenceNumber), a => a.ReferenceNumber == criteria.ReferenceNumber)
                .WhereIf(criteria.FromPublishDate.HasValue, a => a.PublishedDate.Value.Date >= criteria.FromPublishDate.Value)
                .WhereIf(criteria.ToPublishDate.HasValue, a => a.PublishedDate.Value.Date <= criteria.ToPublishDate.Value)
                .WhereIf(criteria.AnnouncementStatusId.HasValue && (criteria.AnnouncementStatusId != (int)Enums.AnnouncementStatus.Ended && criteria.AnnouncementStatusId != (int)Enums.AnnouncementStatus.Approved), a => a.StatusId == criteria.AnnouncementStatusId)
                .WhereIf(criteria.AnnouncementStatusId.HasValue && criteria.AnnouncementStatusId == (int)Enums.AnnouncementStatus.Ended, a => a.PublishedDate.Value.AddDays(a.AnnouncementPeriod) < DateTime.Now)
                .WhereIf(criteria.AnnouncementStatusId.HasValue && criteria.AnnouncementStatusId == (int)Enums.AnnouncementStatus.Approved, a => (a.PublishedDate.Value.AddDays(a.AnnouncementPeriod) >= DateTime.Now))
                .WhereIf(!string.IsNullOrEmpty(criteria.CreatedBy), a => a.CreatedBy.Contains(criteria.CreatedBy))
                .WhereIf(!string.IsNullOrEmpty(criteria.ApprovedBy), a => a.ApprovedBy.Contains(criteria.ApprovedBy))
                .WhereIf(criteria.TenderTypeId.HasValue, a => a.TenderTypeId == criteria.TenderTypeId)
                .OrderByDescending(a => a.PublishedDate).ThenByDescending(a => a.CreatedAt)
                .Select(announcement => new AllAnouuncementsForAgencyModel
                {
                    AnnouncementIdString = Util.Encrypt(announcement.AnnouncementId),
                    AnnouncementName = announcement.AnnouncementName,
                    ReferenceNumber = announcement.ReferenceNumber,
                    AgencyName = announcement.Agency.NameArabic,
                    AgencyCode = announcement.AgencyCode,
                    BranchName = announcement.Branch.BranchName,
                    TenderTypeString = announcement.TenderType.NameAr,
                    AnnouncementPeriod = announcement.AnnouncementPeriod,
                    PublishedDate = announcement.PublishedDate,
                    StatusId = announcement.StatusId,
                    AnnouncementStatusString = announcement.AnnouncementStatusId == (int)Enums.AnnouncementStatus.Ended ? "منتهي" : announcement.AnnouncementStatus.Name
                }
            ).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<UnderOperationAnouuncementsForAgencyModel>> FindUnderOperationAgencyAnnouncements(UnderOperationAgencyAnnouncementSearchCriteria criteria)
        {
            var result = await _context.Announcements
                .Where(a => a.IsActive == true && (a.StatusId == (int)Enums.AnnouncementStatus.UnderCreation || a.StatusId == (int)Enums.AnnouncementStatus.Pending || a.StatusId == (int)Enums.AnnouncementStatus.Rejected))
                .WhereIf(!string.IsNullOrEmpty(criteria.CurrentAgencyCode) && !new List<string> { RoleNames.MonafasatAccountManager, RoleNames.CustomerService }.Contains(criteria.CurrentRoleName), a => a.AgencyCode == criteria.CurrentAgencyCode)
                .WhereIf(criteria.CurrentBranchId.HasValue && criteria.CurrentBranchId.Value != 0, a => a.BranchId == criteria.CurrentBranchId)
                .WhereIf(!string.IsNullOrEmpty(criteria.AnnouncementName), a => a.AnnouncementName == criteria.AnnouncementName)
                .WhereIf(!string.IsNullOrEmpty(criteria.ReferenceNumber), a => a.ReferenceNumber == criteria.ReferenceNumber)
                .WhereIf(criteria.AnnouncementStatusId.HasValue && criteria.AnnouncementStatusId.Value != 0, a => a.StatusId == criteria.AnnouncementStatusId)
                .WhereIf(criteria.TenderTypeId.HasValue && criteria.TenderTypeId.Value != 0, a => a.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(criteria.ActivityId != null, x => x.AnnouncementActivities.Any(a => a.Activity.ActivityId == criteria.ActivityId) || x.AnnouncementActivities.Any(a => a.Activity.ParentID == criteria.ActivityId))
                .WhereIf(criteria.SubActivityIds != null && criteria.SubActivityIds.Any(), x => x.AnnouncementActivities.Any(a => criteria.SubActivityIds.Contains(a.Activity.ActivityId)))
                .OrderByDescending(a => a.CreatedAt)
                .Select(announcement => new UnderOperationAnouuncementsForAgencyModel
                {
                    AnnouncementIdString = Util.Encrypt(announcement.AnnouncementId),
                    AnnouncementName = announcement.AnnouncementName,
                    ReferenceNumber = announcement.ReferenceNumber,
                    AgencyName = announcement.Agency.NameArabic,
                    AgencyCode = announcement.AgencyCode,
                    BranchName = announcement.Branch.BranchName,
                    TenderTypeString = announcement.TenderType.NameAr,
                    AnnouncementPeriod = announcement.AnnouncementPeriod,
                    PublishedDate = announcement.PublishedDate,
                    StatusId = announcement.StatusId,
                    AnnouncementStatusString = announcement.AnnouncementStatus.Name
                }
            ).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<Announcement> GetAnnouncementByIdForCreationStep(int announcementId)
        {
            var announcement = await _context.Announcements
                .Include(r => r.AnnouncementAreas)
                .Include(c => c.AnnouncementCountries)
                .Include(c => c.AnnouncementActivities)
                .Include(w => w.AnnouncementConstructionWorks)
                .Include(m => m.AnnouncementMaintenanceRunnigWorks)
                .Where(a => a.IsActive == true && a.AnnouncementId == announcementId).FirstOrDefaultAsync();
            return announcement;
        }

        public async Task<CreateAnnouncementModel> GetAnnouncementByIdForEdit(int announcementId)
        {
            var result = await _context.Announcements.Where(a => a.IsActive == true && a.AnnouncementId == announcementId).Select(n => new CreateAnnouncementModel
            {
                AnnouncementId = n.AnnouncementId,
                AnnouncementIdString = Util.Encrypt(n.AnnouncementId),
                AnnouncementName = n.AnnouncementName,
                AnnouncementPeriod = n.AnnouncementPeriod,
                TenderTypeId = n.TenderTypeId,
                ReasonIdForSelectingTenderType = n.ReasonForSelectingTenderTypeId,
                InsideKsa = n.InsidKsa,
                IntroAboutTender = n.IntroAboutTender,
                Details = n.Details,
                ActivityDescription = n.ActivityDescription,
                ActivityIds = n.AnnouncementActivities.Any() ? n.AnnouncementActivities.Select(s => s.ActivityId).ToList() : new List<int>(),
                AreasIds = n.AnnouncementAreas.Any() ? n.AnnouncementAreas.Select(s => s.AreaId).ToList() : new List<int>(),
                CountriesIds = n.AnnouncementCountries.Any() ? n.AnnouncementCountries.Select(s => s.CountryId).ToList() : new List<int>(),
                ConstructionWorkIds = n.AnnouncementConstructionWorks.Any() ? n.AnnouncementConstructionWorks.Select(s => s.ConstructionWorkId).ToList() : new List<int>(),
                MaintenanceRunnigWorkIds = n.AnnouncementMaintenanceRunnigWorks.Any() ? n.AnnouncementMaintenanceRunnigWorks.Select(s => s.MaintenanceRunningWorkId).ToList() : new List<int>(),
                BranchId = n.BranchId,
                AgencyCode = n.AgencyCode,
                CreatedBy = n.CreatedBy
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Announcement> GetAnnouncementWithNoIncludsById(int announcementId)
        {
            var announcement = await _context.Announcements.Where(x => x.IsActive == true && x.AnnouncementId == announcementId).FirstOrDefaultAsync();
            return announcement;
        }

        public async Task<List<TenderTypeModel>> GetTenderTypes()
        {
            return await _context.TenderTypes
                .Where(r => r.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || r.TenderTypeId == (int)Enums.TenderType.LimitedTender)
                .Select(x => new TenderTypeModel
                {
                    TenderTypeId = x.TenderTypeId,
                    TenderTypeName = x.NameAr
                }).ToListAsync();
        }
    }
}
