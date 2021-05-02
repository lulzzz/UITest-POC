using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementSupplierTemplateQueries : IAnnouncementSupplierTemplateQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AnnouncementSupplierTemplateQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<QueryResult<AllAnnouncementSupplierTemplateAgencyModel>> FindAllAnnouncementSupplierTemplates(AgencyAnnouncementSupplierTemplateSearchCriteria criteria)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            if (user.IsInRole(RoleNames.DataEntry) || user.IsInRole(RoleNames.Auditer))
            {
                criteria.Sort = "CreatedAt";
            }
            else
            {
                criteria.Sort = "UpdatedAt";
            }
            if (user.IsInRole(RoleNames.CustomerService) || user.IsInRole(RoleNames.MonafasatAccountManager) || user.IsInRole(RoleNames.UnitSpecialistLevel1) || user.IsInRole(RoleNames.UnitSpecialistLevel2) || user.IsInRole(RoleNames.UnitManagerUser))
            {
                criteria.Type = "Public";
            }

            var result = await _context.AnnouncementSupplierTemplate.Where(a => a.IsActive == true)
                .WhereIf(!string.IsNullOrEmpty(criteria.AnnouncementName), a => a.AnnouncementName.Contains(criteria.AnnouncementName))
                .WhereIf(criteria.Type == "Public", a =>
                ListOfUndercreationStatuses().Contains(a.StatusId) ?
                (a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Public && a.CreatedById == user.UserId())
                :
                a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Public)
                .WhereIf(criteria.Type == "Private" || string.IsNullOrEmpty(criteria.Type), a =>
                ListOfUndercreationStatuses().Contains(a.StatusId) ?
                (a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Private && a.CreatedById == user.UserId()
                && a.AgencyCode == user.UserAgency())
                :
                a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Private && a.AgencyCode == user.UserAgency())
                .WhereIf(criteria.Type == "UsedPublicAnnouncement", a => a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Public
                         && a.AgencyCode == user.UserAgency() && !(ListOfUndercreationStatuses().Contains(a.StatusId)))
                .WhereIf(!string.IsNullOrEmpty(criteria.ReferenceNumber), a => a.ReferenceNumber == criteria.ReferenceNumber)
                .WhereIf(criteria.StatusId.HasValue, a => a.StatusId == criteria.StatusId)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), a => a.AgencyCode == criteria.AgencyCode)
                .WhereIf(criteria.BranchId.HasValue, a => a.BranchId == criteria.BranchId)
                .WhereIf(criteria.FromPublishDate.HasValue, a => a.PublishedDate.Value.Date >= criteria.FromPublishDate.Value)
                .WhereIf(criteria.ToPublishDate.HasValue, a => a.PublishedDate.Value.Date <= criteria.ToPublishDate.Value)
                .WhereIf(criteria.JoinedCount == (int)Enums.joinedCount.LessThanTen, a =>
                a.AnnouncementJoinRequests.Count(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) < 10)
                .WhereIf(criteria.JoinedCount == (int)Enums.joinedCount.FromTenToThirty, a =>
                a.AnnouncementJoinRequests.Count(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) >= 10
                && a.AnnouncementJoinRequests.Count(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) <= 30)
                .WhereIf(criteria.JoinedCount == (int)Enums.joinedCount.FromThirtyToSixty, a =>
                a.AnnouncementJoinRequests.Count(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) >= 30
                && a.AnnouncementJoinRequests.Count(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) <= 60)
                .WhereIf(criteria.JoinedCount == (int)Enums.joinedCount.FromSixtyToNinty, a =>
                a.AnnouncementJoinRequests.Count(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) >= 60
                && a.AnnouncementJoinRequests.Count(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) <= 90)
                .WhereIf(criteria.JoinedCount == (int)Enums.joinedCount.MoreThanNinty, a =>
                a.AnnouncementJoinRequests.Count(x => x.IsActive == true && x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted) > 90)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(a => new AllAnnouncementSupplierTemplateAgencyModel
                {
                    AnnouncementId = a.AnnouncementId,
                    AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                    AnnouncementName = a.AnnouncementName,
                    ReferenceNumber = a.ReferenceNumber,
                    StatusId = a.StatusId,
                    StatusString = a.AnnouncementStatus.Name,
                    AnnouncementTemplateSuppliersListTypeId = a.AnnouncementTemplateSuppliersListTypeId,
                    AnnouncementTemplateSuppliersListTypeString = a.AnnouncementTemplateListType.NameAr,
                    MainActivity = a.AnnouncementActivities.Select(s => s.Activity.NameAr).FirstOrDefault(),
                    AgencyName = a.Agency.NameArabic,
                    BranchName = a.Branch.BranchName,
                    JoinRequestsCount = a.AnnouncementJoinRequests.Count(d => d.IsActive == true && !(d.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted || d.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn)),
                    canCancel = (a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Ended || a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved)
                    && a.AgencyCode == user.UserAgency(),
                    CanReviewJoinRequests = user.IsInRole(RoleNames.CustomerService) || user.IsInRole(RoleNames.MonafasatAccountManager) || user.IsInRole(RoleNames.UnitSpecialistLevel1) || user.IsInRole(RoleNames.UnitSpecialistLevel2) || user.IsInRole(RoleNames.UnitManagerUser) || a.AgencyCode == criteria.CurrentAgencyCode || a.LinkedAgenciesAnnouncements.Any(x => x.AgencyCode == criteria.CurrentAgencyCode && x.IsActive == true),
                    CanEditAnnouncementTemplate = (a.AgencyCode == agencyCode && a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && ((a.IsEffectiveList.HasValue && a.IsEffectiveList.Value && a.EffectiveListDate > DateTime.Now) || a.EffectiveListDate == null)),
                    isUserAgency = a.AgencyCode == user.UserAgency(),
                    AcceptedJoinRequestsCount = a.AnnouncementJoinRequests.Count(j => j.IsActive == true && j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted),
                    PublishedDateString = a.PublishedDate.HasValue ? a.PublishedDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    isCreator = a.CreatedById == user.UserId(),
                    CanExtendAnnouncementTemplate = a.AgencyCode == agencyCode && a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Ended,
                    IsReadyForApproval = (a.ReadyForApproval.HasValue && a.ReadyForApproval.Value == (int)Enums.AnnouncementSupplierTemplateStatus.ReadyForApproval) ? true : false,
                    CanAddAnnouncementListToAgency = a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && (a.IsEffectiveList != true || (a.IsEffectiveList == true && a.EffectiveListDate.Value.Date >= DateTime.Now.Date)) && a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Public && a.AgencyCode != criteria.CurrentAgencyCode && !a.LinkedAgenciesAnnouncements.Any(x => x.AgencyCode == criteria.CurrentAgencyCode && x.IsActive == true),
                    CanRemoveAnnouncementListFromAgency = a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && (a.IsEffectiveList != true || (a.IsEffectiveList == true && a.EffectiveListDate.Value.Date >= DateTime.Now.Date)) && a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Public && a.AgencyCode != criteria.CurrentAgencyCode && a.LinkedAgenciesAnnouncements.Any(x => x.AgencyCode == criteria.CurrentAgencyCode && x.IsActive == true)
                }).ToListAsync();

            QueryResult<AllAnnouncementSupplierTemplateAgencyModel> resultValue;
            foreach (var item in result)
            {
                item.UsageCount = await TenderCount(item.AnnouncementId);
            }
            resultValue = await result.ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return resultValue;
        }


        public async Task<QueryResult<AnnouncementSupplierTemplateSupplierGridModel>> GetAllAnnouncementSupplierTemplatesForSupplier(AnnouncementSupplierTemplateSupplierSearchCriteriaModel criteria)
        {
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            var result = await _context.AnnouncementSupplierTemplate.Where(a => a.IsActive == true)
                .Where(a => (a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && a.EffectiveListDate == null || (a.EffectiveListDate.HasValue && a.EffectiveListDate >= DateTime.Now))
                || ((a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved || a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Ended || a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Canceled)
                && a.AnnouncementJoinRequests.Any(r => r.IsActive == true && r.Cr == cr)))
                .WhereIf(criteria.AnnouncementTabId.HasValue && criteria.AnnouncementTabId != (int)Enums.AnnouncemetTamplateApplyingStatus.All, a => criteria.AnnouncementTabId == (int)Enums.AnnouncemetTamplateApplyingStatus.TemplatesAvilableToJoin ? (a.IsEffectiveList == null || (a.IsEffectiveList.HasValue && a.IsEffectiveList.Value && a.EffectiveListDate >= DateTime.Now)) : a.AnnouncementJoinRequests.Any(r => r.Cr == cr))
                .WhereIf(criteria.AnnouncementApplyingStatusId.HasValue && criteria.AnnouncementApplyingStatusId != (int)Enums.AnnouncemetTamplateApplyingStatus.All, a => criteria.AnnouncementApplyingStatusId == (int)Enums.AnnouncemetTamplateApplyingStatus.TemplatesAvilableToJoin ? (a.IsEffectiveList == null || (a.IsEffectiveList.HasValue && a.IsEffectiveList.Value && a.EffectiveListDate >= DateTime.Now)) : a.AnnouncementJoinRequests.Any(r => r.Cr == cr))
                .WhereIf(!string.IsNullOrEmpty(criteria.AnnouncementName), a => a.AnnouncementName.Contains(criteria.AnnouncementName))
                .WhereIf(!string.IsNullOrEmpty(criteria.ReferenceNumber), a => a.ReferenceNumber == criteria.ReferenceNumber)
                .WhereIf(!string.IsNullOrEmpty(criteria.AgencyCode), a => a.AgencyCode == criteria.AgencyCode)
                .WhereIf(!string.IsNullOrEmpty(criteria.StatusId.ToString()) && criteria.StatusId != 0, a => a.StatusId == criteria.StatusId)
                .WhereIf(!string.IsNullOrEmpty(criteria.TypeId.ToString()) && criteria.TypeId != 0, a => a.AnnouncementTemplateSuppliersListTypeId == criteria.TypeId)
                .WhereIf(criteria.FromPublishDate.HasValue, a => a.PublishedDate >= criteria.FromPublishDate)
                .WhereIf(!string.IsNullOrEmpty(criteria.ActivityId.ToString()) && criteria.ActivityId != 0, a => a.AnnouncementActivities.Any(x => x.Activity.ParentID == criteria.ActivityId))
                .WhereIf(!string.IsNullOrEmpty(criteria.SubActivityId.ToString()) && criteria.SubActivityId != 0, a => a.AnnouncementActivities.Any(x => x.ActivityId == criteria.SubActivityId))
                .WhereIf(!string.IsNullOrEmpty(criteria.AreaId.ToString()) && criteria.AreaId != 0, a => a.AnnouncementAreas.Any(x => x.AreaId == criteria.AreaId))
                //.SortBy(criteria.Sort, criteria.SortDirection)
                .OrderByDescending(a => criteria.AnnouncementTabId == (int)Enums.AnnouncemetTamplateApplyingStatus.TemplatesAlreadyJoined && a.AnnouncementJoinRequests.FirstOrDefault(x => x.Cr == cr) != null ? a.AnnouncementJoinRequests.FirstOrDefault(x => x.Cr == cr).CreatedAt : a.PublishedDate)
                .Select(a => new AnnouncementSupplierTemplateSupplierGridModel
                {
                    AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                    AnnouncementName = a.AnnouncementName,
                    ReferenceNumber = a.ReferenceNumber,
                    StatusId = a.StatusId,
                    StatusString = a.AnnouncementStatus.Name,
                    AnnouncementTemplateSuppliersListTypeId = a.AnnouncementTemplateSuppliersListTypeId,
                    AnnouncementTemplateSuppliersListTypeString = a.AnnouncementTemplateListType.NameAr,
                    MainActivity = a.AnnouncementActivities.Select(s => s.Activity.NameAr).FirstOrDefault(),
                    AgencyCode = a.AgencyCode,
                    AgencyName = a.Agency.NameArabic,
                    PublishedDateString = a.PublishedDate.HasValue ? a.PublishedDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastDateForJoinRequests = a.EffectiveListDate.HasValue ? a.EffectiveListDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    tenderTypeString = GetTenderName(a.TenderTypesId),
                    IsValidAnnouncement = a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && ((a.IsEffectiveList.HasValue && a.IsEffectiveList.Value && a.EffectiveListDate > DateTime.Now) || a.EffectiveListDate == null),
                    HasApprovedOrPendingRequest = a.AnnouncementJoinRequests.Any(x => x.Cr == cr && x.IsActive == true && (x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Sent || x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted || x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingAcceptance || x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection))
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<JoinRequestModel>> GetJoinRequestsByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {

            return await _context.AnnouncementRequestSupplierTemplate.Where(j => j.IsActive == true && j.AnnouncementId == criteria.announcementId)
                .Select(j => new JoinRequestModel
                {
                    CommericalRegisterName = j.Supplier.SelectedCrName,
                    CommericalRegisterNo = j.Supplier.SelectedCr,
                    RequestStatus = j.JoinRequestStatus.NameAr,
                    RequestIdString = Util.Encrypt(j.Id),
                    RequestStatusId = j.StatusId,
                    //IsOwnerAgency = criteria.UserRole == RoleNames.CustomerService || criteria.UserRole == RoleNames.MonafasatAccountManager || j.AnnouncementSupplierTemplate.AgencyCode == criteria.AgencyCode,
                    RejectionReason = !string.IsNullOrEmpty(j.RejectionReason) && (j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection || j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected) ? j.RejectionReason : Resources.SharedResources.DisplayInputs.NotExist,
                    DeleteReason = !string.IsNullOrEmpty(j.DeleteReason) && j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted ? j.DeleteReason : Resources.SharedResources.DisplayInputs.NotExist,
                    Notes = !string.IsNullOrEmpty(j.Notes) ? j.Notes : Resources.SharedResources.DisplayInputs.NotExist,
                    HasPendingRequests = j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection || j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingAcceptance
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
        }
        public async Task<QueryResult<JoinRequestModel>> GetApprovedSuppliersJoinRequestsByAnnouncementId(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {

            return await _context.AnnouncementRequestSupplierTemplate
                .Where(j => j.IsActive == true && j.AnnouncementId == criteria.announcementId && j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted)
                .Select(j => new JoinRequestModel
                {
                    CommericalRegisterName = j.Supplier.SelectedCrName,
                    CommericalRegisterNo = j.Supplier.SelectedCr
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
        }


        public async Task<QueryResult<JoinRequestModel>> GetJoinRequestsSuppliersByAnnouncementIdAsync(JoinRequestSuppliersSearchCriteria criteria)
        {
            var result = await _context.AnnouncementRequestSupplierTemplate.Where(j => j.IsActive == true && j.AnnouncementId == criteria.announcementId && j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted)
                .WhereIf(!string.IsNullOrEmpty(criteria.CommericalRegisterNo), s => s.Cr.Contains(criteria.CommericalRegisterNo))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.CommericalRegisterName), x => x.Supplier.SelectedCrName.ToUpper().Trim().Contains(criteria.CommericalRegisterName.ToUpper().Trim()))
                .Select(j => new JoinRequestModel
                {
                    CommericalRegisterName = j.Supplier.SelectedCrName,
                    CommericalRegisterNo = j.Supplier.SelectedCr,
                    //IsOwnerAgency = criteria.UserRole == RoleNames.CustomerService || j.AnnouncementSupplierTemplate.AgencyCode == criteria.CurrentAgencyCode,
                    RequestStatusId = j.StatusId
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }


        public async Task<AnnouncementSuppliersTemplateCancelModel> FindAnnouncementDetailsByAnnouncementIdForCancelation(int announcementId)
        {
            var result = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementSuppliersTemplateCancelModel
               {
                   AnnouncementId = a.AnnouncementId,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                   AnnouncementName = a.AnnouncementName,
                   AgencyName = a.Agency.NameArabic,
                   CreatedBy = a.CreatedBy,
                   BranchName = a.Branch.BranchName,
                   Details = a.Details,
                   IntroAboutAnnouncementTemplate = a.IntroAboutAnnouncementTemplate,
                   PublishedDate = a.PublishedDate,
                   ReferenceNumber = a.ReferenceNumber,
                   StatusName = a.AnnouncementStatus.Name,
                   Descriptions = a.Descriptions,
                   EffectiveListDate = a.EffectiveListDate.HasValue ? a.EffectiveListDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                   AnnouncementListTypeName = a.AnnouncementTemplateListType != null ? a.AnnouncementTemplateListType.NameAr : "",
                   TenderTypeName = GetTenderName(a.TenderTypesId),
                   CancelationReason = a.CancelationReason,
                   IsCreatedByAgncy = a.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency(),
               }).FirstOrDefaultAsync();
            return result;
        }



        public async Task<List<string>> GetJoinedAnnouncementSuppliers(int announcementId)
        {

            var announcement = await _context.AnnouncementRequestSupplierTemplate
                .Where(a => a.AnnouncementId == announcementId && a.IsActive == true &&
                (a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Sent
                || a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingAcceptance
                 || a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection
                )).Select(x => x.Cr).ToListAsync();
            return announcement;
        }


        public async Task<List<string>> GetAcceptedAnnouncementSuppliers(int announcementId)
        {
            var announcement = await _context.AnnouncementRequestSupplierTemplate
                .Where(a => a.AnnouncementId == announcementId && a.IsActive == true && a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted).Select(x => x.Cr).ToListAsync();
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

        public async Task<AnnouncementSupplierTemplate> GetAnnouncementByIdForCreationStep(int announcementId)
        {
            var announcement = await _context.AnnouncementSupplierTemplate
                .Include(r => r.AnnouncementAreas)
                .Include(c => c.AnnouncementCountries)
                .Include(c => c.AnnouncementActivities)
                .Include(w => w.AnnouncementConstructionWorks)
                .Include(m => m.AnnouncementMaintenanceRunnigWorks)
                .Include(a => a.Attachments)
                .Where(a => a.IsActive == true && a.AnnouncementId == announcementId).FirstOrDefaultAsync();
            return announcement;
        }

        public async Task<ApproveAnnouncemntSupplierTemplateModel> GetAnnouncementSupplierTemplateDetailsForApproval(int announcementId)
        {
            var tender = await _context.AnnouncementSupplierTemplate.Where(x => x.IsActive == true && x.AnnouncementId == announcementId)
                .Select(s => new ApproveAnnouncemntSupplierTemplateModel
                {
                    AnnouncementId = s.AnnouncementId,
                    AnnouncementIdString = Util.Encrypt(s.AnnouncementId),
                    StatusId = s.StatusId,
                    CreatedById = s.CreatedById.HasValue ? s.CreatedById.Value : 0,
                    UserProfileId = _httpContextAccessor.HttpContext.User.UserId()
                }).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<AddPublicListToAgencyAnnouncementListsModel> GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(int announcementId, string agencyCode)
        {
            var result = await _context.AnnouncementSupplierTemplate
                .Where(x => x.IsActive == true && x.AnnouncementId == announcementId)
                .Select(a => new AddPublicListToAgencyAnnouncementListsModel
                {
                    AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                    StatusId = a.StatusId,
                    CanAddAnnouncementListToAgency = a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && (a.IsEffectiveList != true || (a.IsEffectiveList == true && a.EffectiveListDate.Value.Date >= DateTime.Now.Date)) && a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Public && a.AgencyCode != agencyCode && !a.LinkedAgenciesAnnouncements.Any(x => x.AgencyCode == agencyCode && x.IsActive == true),
                    CanRemoveAnnouncementListFromAgency = a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && (a.IsEffectiveList != true || (a.IsEffectiveList == true && a.EffectiveListDate.Value.Date >= DateTime.Now.Date)) && a.AnnouncementTemplateSuppliersListTypeId == (int)Enums.AnnouncementTemplateSuppliersListTypeId.Public && a.AgencyCode != agencyCode && a.LinkedAgenciesAnnouncements.Any(x => x.AgencyCode == agencyCode && x.IsActive == true)
                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<AnnouncementTemplateMainDetailsModel> GetAnnouncementTemplateDetailsForSuppliers(int announcementId, string cr)
        {

            var result = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementTemplateMainDetailsModel
               {
                   AnnouncementId = a.AnnouncementId,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                   AnnouncementName = a.AnnouncementName,
                   AgencyName = a.Agency.NameArabic,
                   ActivityDescription = a.ActivityDescription,
                   AuditedBy = a.ApprovedBy,
                   CreatedBy = a.CreatedBy,
                   BranchName = a.Branch.BranchName,
                   Details = a.Details,
                   InsideKSA = a.InsidKsa,
                   IntroAboutAnnouncementTemplate = a.IntroAboutAnnouncementTemplate,
                   PublishedDate = a.PublishedDate,
                   ReferenceNumber = a.ReferenceNumber,
                   StatusName = a.AnnouncementStatus.Name,
                   StatusId = a.StatusId,
                   Descriptions = a.Descriptions,
                   RequirementConditionsToJoinList = a.RequirementConditionsToJoinList,
                   TenderTypeName = GetTenderName(a.TenderTypesId),
                   AgencyCode = a.AgencyCode,
                   AgencyEmail = a.AgencyEmail,
                   AgencyPhone = a.AgencyPhone,
                   HasJoinRequest = a.AnnouncementJoinRequests.Any(r => r.Cr == cr && r.IsActive == true),
                   AgencyFax = a.AgencyFax,
                   AgencyAddress = a.AgencyAddress,
                   RequiredAttachment = a.RequiredAttachment,
                   IsEffectiveList = a.IsEffectiveList ?? false,
                   IsRequiredAttachmentToJoinList = a.IsRequiredAttachmentToJoinList,
                   EffectiveListDate = a.EffectiveListDate.HasValue ? a.EffectiveListDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                   AnnouncementListTypeName = a.AnnouncementTemplateListType != null ? a.AnnouncementTemplateListType.NameAr : "",
                   IsApprovedAndValidAnnoncement = a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && ((a.IsEffectiveList.HasValue && a.EffectiveListDate > DateTime.Now) || a.EffectiveListDate == null)
               }).FirstOrDefaultAsync();

            var joinRequests = await _context.AnnouncementRequestSupplierTemplate
                .Where(x => x.AnnouncementId == announcementId && x.Cr == cr)
                .Where(x => x.IsActive == true && x.AnnouncementSupplierTemplate.IsActive == true).ToListAsync();

            if (joinRequests.Any())
            {
                result.HasPendingOrAcceptedJoinRequest = joinRequests.Any(x => x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Sent || x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted || x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingAcceptance || x.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection);
                result.JoinRequestIdString = Util.Encrypt(joinRequests.OrderByDescending(x => x.CreatedAt).Select(x => x.Id).FirstOrDefault());
            }

            result.Attachments = await _context.AnnouncementRequestSupplierTemplate
                .Where(r => r.AnnouncementId == announcementId && r.Cr == cr && r.IsActive == true)
                .Where(r => r.StatusId != (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn && r.StatusId != (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => r.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList()).FirstOrDefaultAsync();

            return result;
        }

        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetails(int joinRequestId, string cr)
        {

            var result = await _context.AnnouncementRequestSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.Id == joinRequestId)
               .Select(a => new AnnouncementSuppliersTemplateJoinRequestsDetailsModel
               {
                   AnnouncementId = a.AnnouncementId,
                   AnnouncementName = a.AnnouncementSupplierTemplate.AnnouncementName,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                   CommericalRegisterName = a.Supplier.SelectedCrName,
                   CommericalRegisterNo = a.Cr,
                   JoinRequestStatusId = a.StatusId,
                   RejectionReason = a.RejectionReason,
                   Notes = a.Notes,
                   JoinRequestId = a.Id,
                   RequestResultId = a.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Sent ? (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingAcceptance : a.StatusId,
                   JoinRequestStatusName = a.JoinRequestStatus.NameAr,
                   Attachments = a.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList()
               }).FirstOrDefaultAsync();


            return result;
        }
        public async Task<AnnouncementSuppliersTemplateJoinRequestsDetailsModel> GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(int announcementId, string agencyCode, string userRole)
        {

            var result = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementSuppliersTemplateJoinRequestsDetailsModel
               {
                   AnnouncementId = a.AnnouncementId,
                   IsOwnerAgency = userRole == RoleNames.CustomerService || userRole == RoleNames.MonafasatAccountManager || a.AgencyCode == agencyCode,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId)
               }).FirstOrDefaultAsync();


            return result;
        }

        public async Task<AnnouncementTemplateActivityAndAreaDetailsModel> GetAnnouncementTemplateActivityAndAddressDetails(int announcementId)
        {
            var result = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementTemplateActivityAndAreaDetailsModel
               {
                   AnnouncementId = a.AnnouncementId,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                   ActivityDescription = a.ActivityDescription,
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
                   Details = a.Details,
                   InsideKSA = a.InsidKsa,
                   Descriptions = a.Descriptions,
                   IsCreatedByAgncy = a.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency(),
                   Attachments = a.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency() ? a.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList() : null,

               }).FirstOrDefaultAsync();
            return result;
        }
        public async Task<AnnouncementSuppliersTemplateDetailsModel> FindAnnouncementDetailsByAnnouncementId(int announcementId)
        {
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            var result = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementSuppliersTemplateDetailsModel
               {
                   AnnouncementId = a.AnnouncementId,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                   AnnouncementName = a.AnnouncementName,
                   AgencyName = a.Agency.NameArabic,
                   ActivityDescription = a.ActivityDescription,
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
                   IntroAboutAnnouncementTemplate = a.IntroAboutAnnouncementTemplate,
                   PublishedDate = a.PublishedDate,
                   ReferenceNumber = a.ReferenceNumber,
                   StatusName = a.AnnouncementStatus.Name,
                   StatusId = a.StatusId,
                   Descriptions = a.Descriptions,
                   RequirementConditionsToJoinList = a.RequirementConditionsToJoinList,
                   TenderTypeName = GetTenderName(a.TenderTypesId),
                   AgencyCode = a.AgencyCode,
                   AgencyEmail = a.AgencyEmail,
                   AgencyPhone = a.AgencyPhone,
                   AgencyFax = a.AgencyFax,
                   AgencyAddress = a.AgencyAddress,
                   RequiredAttachment = a.RequiredAttachment,
                   IsEffectiveList = a.IsEffectiveList ?? false,
                   IsRequiredAttachmentToJoinList = a.IsRequiredAttachmentToJoinList,
                   EffectiveListDate = a.EffectiveListDate.HasValue ? a.EffectiveListDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                   AnnouncementListTypeName = a.AnnouncementTemplateListType != null ? a.AnnouncementTemplateListType.NameAr : "",
                   Attachments = a.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList(),
                   CanEditAnnouncementTemplate = (a.AgencyCode == agencyCode && a.StatusId == (int)Enums.AnnouncementSupplierTemplateStatus.Approved && ((a.IsEffectiveList.HasValue && a.IsEffectiveList.Value && a.EffectiveListDate > DateTime.Now) || a.EffectiveListDate == null)),
                   IsCreatedByAgncy = a.AgencyCode == agencyCode,
               }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<AnnouncementTemplateDetailsForPrintModel> GetAnnouncementDetailsByAnnouncementIdForPrint(int announcementId)
        {
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            var announcementModel = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementTemplateDetailsForPrintModel
               {
                   AnnouncementId = a.AnnouncementId,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                   AnnouncementName = a.AnnouncementName,
                   ReferenceNumber = a.ReferenceNumber,
                   PublishedDate = a.PublishedDate,
                   AgencyName = a.Agency.NameArabic,
                   IntroAboutAnnouncementTemplate = a.IntroAboutAnnouncementTemplate,
                   StatusName = a.AnnouncementStatus.Name,
                   StatusId = a.StatusId,
                   AnnouncementListTypeName = a.AnnouncementTemplateListType != null ? a.AnnouncementTemplateListType.NameAr : "",
                   TenderTypeName = GetTenderName(a.TenderTypesId),
                   CreatedBy = a.CreatedBy,
                   IsEffectiveList = a.IsEffectiveList ?? false,
                   EffectiveListDate = a.EffectiveListDate.HasValue ? a.EffectiveListDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                   Descriptions = a.Descriptions,
                   InsideKSA = a.InsidKsa,
                   Details = a.Details,
                   ActivityDescription = a.ActivityDescription,
                   RequirementConditionsToJoinList = a.RequirementConditionsToJoinList,
                   IsRequiredAttachmentToJoinList = a.IsRequiredAttachmentToJoinList,
                   RequiredAttachment = a.RequiredAttachment,
                   AgencyCode = a.AgencyCode,
                   IsCreatedByAgncy = a.AgencyCode == agencyCode,
               }).FirstOrDefaultAsync();

            announcementModel.JoinRequests = await GetJoinRequestForPrint(announcementId);
            announcementModel.LinkedAgenciesAnnouncementTemplate = await GetBenficiaryAgenciesForPrint(announcementId);
            announcementModel.TenderAnnouncementSuppliersTemplate = await GetTendersAnnouncementSuppliersTemplateForPrint(announcementId, agencyCode);
            var notAllowedTenderStatus = new List<int>() { (int)Enums.TenderStatus.Canceled, (int)Enums.TenderStatus.Established, (int)Enums.TenderStatus.UnderEstablishing };
            announcementModel.JoinRequestCount = await _context.AnnouncementRequestSupplierTemplate.CountAsync(d => d.AnnouncementId == announcementId && d.IsActive == true && !(d.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn || d.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted));
            announcementModel.AcceptedJoinRequestsCount = await _context.AnnouncementRequestSupplierTemplate.CountAsync(d => d.AnnouncementId == announcementId && d.IsActive == true && d.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted);
            announcementModel.UsingListCount = await _context.Tenders.CountAsync(d => d.AnnouncementTemplateId == announcementId && d.IsActive == true && !notAllowedTenderStatus.Any(a => a == d.TenderStatusId));
            announcementModel.UsingListCountInternally = await _context.Tenders.CountAsync(
                d => d.AnnouncementTemplateId == announcementId && d.IsActive == true
                && !notAllowedTenderStatus.Any(a => a == d.TenderStatusId) && d.AgencyCode == announcementModel.AgencyCode);
            return announcementModel;
        }


        private async Task<List<LinkedAgenciesAnnouncementTemplateModel>> GetBenficiaryAgenciesForPrint(int announcementId)
        {
            return await _context.LinkedAgenciesAnnouncementTemplate.Where(a => a.AnnouncementId == announcementId)
                    .Select(a => new LinkedAgenciesAnnouncementTemplateModel
                    {
                        AgencyName = a.Agency.NameArabic,
                        StatusName = a.IsActive == true ? Resources.AnnouncementSupplierTemplateResources.DisplayInputs.BeneficiaryAgencyStatusAdded : Resources.AnnouncementSupplierTemplateResources.DisplayInputs.BeneficiaryAgencyStatusRemoved,
                        CreatedDate = a.CreatedAt.ToHijriDateWithFormat("yyyy-MM-dd"),
                        RemovedDate = a.UpdatedAt.HasValue ? a.UpdatedAt.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "لايوجد",
                    }).ToListAsync();
        }

        private async Task<List<TenderAnnouncementSuppliersTemplateDetails>> GetTendersAnnouncementSuppliersTemplateForPrint(int announcementId, string agencyCode)
        {
            return await _context.Tenders.Where(a => a.IsActive == true && a.AnnouncementTemplateId == announcementId && a.AgencyCode == agencyCode)
                    .Select(a => new TenderAnnouncementSuppliersTemplateDetails
                    {
                        TenderName = a.TenderName,
                        TenderId = a.TenderId,
                        ReferenceNumber = a.ReferenceNumber,
                        MainActivity = a.TenderActivities.Select(s => s.Activity.NameAr).FirstOrDefault(),
                    }).ToListAsync();
        }


        private async Task<List<JoinRequestModel>> GetJoinRequestForPrint(int announcementId)
        {
            return await _context.AnnouncementRequestSupplierTemplate.Where(j => j.IsActive == true && j.AnnouncementId == announcementId && (j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted || j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected || j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn))
                   .Select(j => new JoinRequestModel
                   {
                       CommericalRegisterName = j.Supplier.SelectedCrName,
                       CommericalRegisterNo = j.Supplier.SelectedCr,
                       RequestStatus = j.JoinRequestStatus.NameAr,
                       RequestIdString = Util.Encrypt(j.Id),
                       RequestStatusId = j.StatusId,
                       RequestSendingDate = j.CreatedAt.Date.ToHijriDateWithFormat("yyyy-MM-dd"),
                       WithdrawalDate = j.JoinRequestHistories.Where(h => h.JoinRequestStatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn).OrderByDescending(x => x.CreatedAt).FirstOrDefault().CreatedAt.Date.ToHijriDateWithFormat("yyyy-MM-dd"),
                       RequestAcceptanceDate = j.JoinRequestHistories.Where(h => h.JoinRequestStatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted).OrderByDescending(x => x.CreatedAt).FirstOrDefault().CreatedAt.Date.ToHijriDateWithFormat("yyyy-MM-dd"),
                       RequestRejectionDate = j.JoinRequestHistories.Where(h => h.JoinRequestStatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected).OrderByDescending(x => x.CreatedAt).FirstOrDefault().CreatedAt.Date.ToHijriDateWithFormat("yyyy-MM-dd"),
                       RejectionReason = !string.IsNullOrEmpty(j.RejectionReason) && (j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection || j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected) ? j.RejectionReason : Resources.SharedResources.DisplayInputs.NotExist,
                       Notes = !string.IsNullOrEmpty(j.Notes) ? j.Notes : Resources.SharedResources.DisplayInputs.NotExist,
                       HasPendingRequests = j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingRejection || j.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.PendingAcceptance
                   }).ToListAsync();
        }

        public async Task<AnnouncementTemplateDetailsForSupplierForPrintModel> GetAnnouncementDetailsForSupplierForPrint(int announcementId, string cr)
        {
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();

            var announcementModel = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementTemplateDetailsForSupplierForPrintModel
               {
                   AnnouncementId = a.AnnouncementId,
                   AnnouncementIdString = Util.Encrypt(a.AnnouncementId),
                   AnnouncementName = a.AnnouncementName,
                   ReferenceNumber = a.ReferenceNumber,
                   PublishedDate = a.PublishedDate,
                   AgencyName = a.Agency.NameArabic,
                   IntroAboutAnnouncementTemplate = a.IntroAboutAnnouncementTemplate,
                   StatusName = a.AnnouncementStatus.Name,
                   StatusId = a.StatusId,
                   AnnouncementListTypeName = a.AnnouncementTemplateListType != null ? a.AnnouncementTemplateListType.NameAr : "",
                   TenderTypeName = GetTenderName(a.TenderTypesId),
                   CreatedBy = a.CreatedBy,
                   IsEffectiveList = a.IsEffectiveList ?? false,
                   EffectiveListDate = a.EffectiveListDate.HasValue ? a.EffectiveListDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                   Descriptions = a.Descriptions,

                   InsideKSA = a.InsidKsa,
                   Details = a.Details,
                   Areas = a.AnnouncementAreas.Select(c => new LookupModel
                   {
                       Id = c.Area.AreaId,
                       Name = c.Area.NameAr
                   }).ToList(),
                   Countries = a.AnnouncementCountries.Select(c => new LookupModel
                   {
                       Id = c.Country.CountryId,
                       Name = _httpContextAccessor.HttpContext.IsArabic() ? c.Country.NameAr : c.Country.NameEn
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
                   MaintenanceOperationActions = a.AnnouncementMaintenanceRunnigWorks.Select(c => new MaintenanceRunningWorkModel
                   {
                       MaintenanceRunningWorkId = c.MaintenanceRunningWork.MaintenanceRunningWorkId,
                       Name = _httpContextAccessor.HttpContext.IsArabic() ? c.MaintenanceRunningWork.NameAr : c.MaintenanceRunningWork.NameEn
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
                   ActivityDescription = a.ActivityDescription,
                   RequirementConditionsToJoinList = a.RequirementConditionsToJoinList,
                   IsRequiredAttachmentToJoinList = a.IsRequiredAttachmentToJoinList,
                   RequiredAttachment = a.RequiredAttachment,
                   AgencyCode = a.AgencyCode,
                   AgencyEmail = a.AgencyEmail,
                   AgencyPhone = a.AgencyPhone,
                   AgencyFax = a.AgencyFax,
                   AgencyAddress = a.AgencyAddress,
                   IsCreatedByAgncy = a.AgencyCode == agencyCode,
               }).FirstOrDefaultAsync();


            announcementModel.JoinRequest = await _context.AnnouncementRequestSupplierTemplate
               .Where(r => r.AnnouncementId == announcementId && r.Cr == cr && r.IsActive == true)
               .OrderByDescending(r => r.CreatedAt)
               .Select(r => new AnnouncementSuppliersTemplateJoinRequestsDetailsModel
               {
                   JoinRequestStatusId = r.StatusId,
                   JoinRequestStatusName = r.JoinRequestStatus.NameAr,
                   JoinRequestIdString = Util.Encrypt(r.Id),
                   JoinRequestSendingDate = r.CreatedAt.Date.ToHijriDateWithFormat("yyyy-MM-dd"),
                   WithdrawalDate = r.JoinRequestHistories.Where(h => h.JoinRequestStatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn).OrderByDescending(x => x.CreatedAt).FirstOrDefault().CreatedAt.Date.ToHijriDateWithFormat("yyyy-MM-dd"),
                   RejectionReason = !string.IsNullOrEmpty(r.RejectionReason) && r.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Rejected ? r.RejectionReason : Resources.SharedResources.DisplayInputs.NotExist,
                   DeleteReason = !string.IsNullOrEmpty(r.DeleteReason) && r.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted ? r.DeleteReason : Resources.SharedResources.DisplayInputs.NotExist,
                   TemplateExtendMechanism = !string.IsNullOrEmpty(r.AnnouncementSupplierTemplate.TemplateExtendMechanism) ? r.AnnouncementSupplierTemplate.TemplateExtendMechanism : Resources.SharedResources.DisplayInputs.NotExist,
                   StopUsingAnnouncemetMechanism = !string.IsNullOrEmpty(r.AnnouncementSupplierTemplate.CancelationReason) ? r.AnnouncementSupplierTemplate.CancelationReason : Resources.SharedResources.DisplayInputs.NotExist
               }).FirstOrDefaultAsync();


            return announcementModel;
        }

        public async Task<AnnouncementBasicDetailModel> FindAnnouncementBasicDetailsByAnnouncementId(int announcementId)
        {
            var result = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementBasicDetailModel
               {
                   AnnouncementName = a.AnnouncementName,
                   AgencyName = a.Agency.NameArabic,
                   AgencyCode = a.AgencyCode,
                   CreatedBy = a.CreatedBy,
                   BranchName = a.Branch.BranchName,
                   Details = a.Details,
                   IntroAboutAnnouncementTemplate = a.IntroAboutAnnouncementTemplate,
                   PublishedDate = a.PublishedDate,
                   ReferenceNumber = a.ReferenceNumber,
                   StatusName = a.AnnouncementStatus.Name,
                   Descriptions = a.Descriptions,
                   EffectiveListDate = a.EffectiveListDate.HasValue ? a.EffectiveListDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "لا يوجد تاريخ انتهاء",
                   AnnouncementListTypeName = a.AnnouncementTemplateListType != null ? a.AnnouncementTemplateListType.NameAr : "",
                   TenderTypeName = GetTenderName(a.TenderTypesId),
                   IsCreatedByAgncy = a.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency(),
               }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<AnnouncementTemplateListDetailsModel> FindAnnouncementTemplateListDetailsByAnnouncementId(int announcementId)
        {
            var announcement = await _context.AnnouncementSupplierTemplate.FirstOrDefaultAsync(f => f.AnnouncementId == announcementId);
            if (announcement == null)
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);

            var agencyCode = announcement.AgencyCode;
            AnnouncementTemplateListDetailsModel announcementTemplateListDetailsModel = new AnnouncementTemplateListDetailsModel();
            var notAllowedTenderStatus = new List<int>() { (int)Enums.TenderStatus.Canceled, (int)Enums.TenderStatus.Established, (int)Enums.TenderStatus.UnderEstablishing };
            announcementTemplateListDetailsModel.JoinRequestCount = await _context.AnnouncementRequestSupplierTemplate.CountAsync(d => d.AnnouncementId == announcementId && d.IsActive == true && !(d.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Withdrawn || d.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Deleted));
            announcementTemplateListDetailsModel.AcceptedSuppliersCount = await _context.AnnouncementRequestSupplierTemplate.CountAsync(d => d.AnnouncementId == announcementId && d.IsActive == true && d.StatusId == (int)Enums.AnnouncementTemplateJoinRequestStatus.Accepted);
            announcementTemplateListDetailsModel.UsingListCount = await _context.Tenders.CountAsync(d => d.AnnouncementTemplateId == announcementId && d.IsActive == true && !notAllowedTenderStatus.Any(a => a == d.TenderStatusId));
            announcementTemplateListDetailsModel.UsingListCountInternally = await _context.Tenders.CountAsync(
                d => d.AnnouncementTemplateId == announcementId && d.IsActive == true
                && !notAllowedTenderStatus.Any(a => a == d.TenderStatusId) && d.AgencyCode == agencyCode);
            return announcementTemplateListDetailsModel;
        }

        public async Task<AnnouncementDescriptionModel> FindAnnouncementDescriptionDetailsByAnnouncementId(int announcementId)
        {
            var result = await _context.AnnouncementSupplierTemplate
                .Where(a => a.IsActive == true)
                .Where(a => a.AnnouncementId == announcementId)
               .Select(a => new AnnouncementDescriptionModel
               {
                   IsRequiredAttachmentToJoinList = a.IsRequiredAttachmentToJoinList,
                   RequirementConditionsToJoinList = a.RequirementConditionsToJoinList,
                   RequiredAttachment = a.RequiredAttachment
               }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<AnnouncementSupplierTemplate> GetAnnouncementWithNoIncludsById(int announcementId)
        {
            var announcement = await _context.AnnouncementSupplierTemplate.Where(x => x.IsActive == true && x.AnnouncementId == announcementId).FirstOrDefaultAsync();
            return announcement;
        }

        public async Task<AnnouncementSupplierTemplate> GetAnnouncementWithJoinRequestsById(int announcementId)
        {
            var announcement = await _context.AnnouncementSupplierTemplate.Include(x => x.AnnouncementJoinRequests)
                .Where(x => x.IsActive == true && x.AnnouncementId == announcementId).FirstOrDefaultAsync();
            return announcement;
        }
        public async Task<AnnouncementSupplierTemplate> GetAnnouncementWithLinkedAgencyById(int announcementId)
        {
            var announcement = await _context.AnnouncementSupplierTemplate.Include(x => x.LinkedAgenciesAnnouncements)
                .Where(x => x.IsActive == true && x.AnnouncementId == announcementId).FirstOrDefaultAsync();
            return announcement;
        }


        private static string GetTenderName(string tenderTypesId)
        {
            string tenderNames = "";
            if (!string.IsNullOrEmpty(tenderTypesId))
            {
                if (tenderTypesId.Length > 1)
                {
                    tenderNames = "شراء مباشر و المنافسة المحدودة";
                }
                else
                {
                    var tenderArrayNames = tenderTypesId.Split(",");
                    if (tenderArrayNames[0] == "2")
                    {
                        tenderNames = Resources.TenderResources.DisplayInputs.DirectPurshase;
                    }
                    else
                    {
                        tenderNames = Resources.TenderResources.DisplayInputs.LimitedTender;
                    }
                }
            }
            return tenderNames;
        }

        public async Task<UpdateAnnouncementSupplierTemplateModel> GetAnnouncementByIdForEditApproval(int announcementId)
        {
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            var result = await _context.AnnouncementSupplierTemplate.Where(a => a.IsActive == true && a.AnnouncementId == announcementId && a.AgencyCode == agencyCode).Select(n => new UpdateAnnouncementSupplierTemplateModel
            {
                AnnouncementId = n.AnnouncementId,
                AnnouncementIdString = Util.Encrypt(n.AnnouncementId),
                BranchId = n.BranchId,
                AgencyCode = n.AgencyCode,
                CreatedBy = n.CreatedBy,
                AgencyEmail = n.AgencyEmail,
                AgencyPhone = n.AgencyPhone,
                AgencyFax = n.AgencyFax,
                AgencyAddress = n.AgencyAddress,
                EffectiveListDate = n.EffectiveListDate,
                IsEffectiveList = n.IsEffectiveList ?? false,
                AnnouncementTemplateSuppliersListTypeId = n.AnnouncementTemplateSuppliersListTypeId,
                Attachments = n.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList(),
            }).FirstOrDefaultAsync();
            return result;
        }


        public async Task<CreateAnnouncementSupplierTemplateModel> GetAnnouncementByIdForEdit(int announcementId)
        {
            var result = await _context.AnnouncementSupplierTemplate.Where(a => a.IsActive == true && a.AnnouncementId == announcementId).Select(n => new CreateAnnouncementSupplierTemplateModel
            {
                AnnouncementId = n.AnnouncementId,
                AnnouncementIdString = Util.Encrypt(n.AnnouncementId),
                AnnouncementSupplierTemplateName = n.AnnouncementName,
                InsideKsa = n.InsidKsa,
                IntroAboutAnnouncementTemplate = n.IntroAboutAnnouncementTemplate,
                Details = n.Details,
                ActivityDescription = n.ActivityDescription,
                ActivityIds = n.AnnouncementActivities.Any() ? n.AnnouncementActivities.Select(s => s.ActivityId).ToList() : new List<int>(),
                AreasIds = n.AnnouncementAreas.Any() ? n.AnnouncementAreas.Select(s => s.AreaId).ToList() : new List<int>(),
                CountriesIds = n.AnnouncementCountries.Any() ? n.AnnouncementCountries.Select(s => s.CountryId).ToList() : new List<int>(),
                ConstructionWorkIds = n.AnnouncementConstructionWorks.Any() ? n.AnnouncementConstructionWorks.Select(s => s.ConstructionWorkId).ToList() : new List<int>(),
                MaintenanceRunnigWorkIds = n.AnnouncementMaintenanceRunnigWorks.Any() ? n.AnnouncementMaintenanceRunnigWorks.Select(s => s.MaintenanceRunningWorkId).ToList() : new List<int>(),
                BranchId = n.BranchId,
                AgencyCode = n.AgencyCode,
                AgencyName = n.Agency.NameArabic,
                BranchName = n.Branch.BranchName,
                CreatedBy = n.CreatedBy,
                AgencyEmail = n.AgencyEmail,
                AgencyPhone = n.AgencyPhone,
                AgencyFax = n.AgencyFax,
                AgencyAddress = n.AgencyAddress,
                Descriptions = n.Descriptions,
                RequirementConditionsToJoinList = n.RequirementConditionsToJoinList,
                EffectiveListDate = n.EffectiveListDate,
                IsEffectiveList = n.IsEffectiveList ?? false,
                IsRequiredAttachmentToJoinList = n.IsRequiredAttachmentToJoinList,
                AnnouncementTemplateSuppliersListTypeId = n.AnnouncementTemplateSuppliersListTypeId,
                TenderItemTypes = !string.IsNullOrEmpty(n.TenderTypesId) ? GetTendersTypes(n.TenderTypesId) : null,
                RequiredAttachment = n.RequiredAttachment,
                Attachments = n.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList(),
                EffectiveListTime = n.EffectiveListDate.HasValue ? n.EffectiveListDate.Value.ToGregorianDate("hh:mm tt") : "",
            }).FirstOrDefaultAsync();
            return result;
        }

        private static List<int> GetTendersTypes(string TenderTypesId)
        {
            return TenderTypesId.Split(",").AsEnumerable().Select(r => int.Parse(r)).ToList();
        }

        public async Task<QueryResult<TenderAnnouncementSuppliersTemplateDetails>> GetTendersByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            var agencyCode = _httpContextAccessor.HttpContext.User.UserAgency();
            var result = await _context.Tenders.Where(a => a.IsActive == true && a.AnnouncementTemplateId == criteria.announcementId && a.AgencyCode == agencyCode)
                .Select(a => new TenderAnnouncementSuppliersTemplateDetails
                {
                    TenderName = a.TenderName,
                    TenderId = a.TenderId,
                    ReferenceNumber = a.ReferenceNumber,
                    MainActivity = a.TenderActivities.Select(s => s.Activity.NameAr).FirstOrDefault(),
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        private List<int> ListOfUndercreationStatuses()
        {
            return new List<int>()
            {
                  (int)Enums.AnnouncementSupplierTemplateStatus.UnderCreation
                , (int)Enums.AnnouncementSupplierTemplateStatus.ReadyForApproval
                , (int)Enums.AnnouncementSupplierTemplateStatus.Rejected
                , (int)Enums.AnnouncementSupplierTemplateStatus.Pending
                , (int)Enums.AnnouncementSupplierTemplateStatus.Canceled
            };
        }


        public async Task<ExtendAnnouncementSupplierTemplateModel> GetAnnouncementByIdForExtendAnnouncement(int announcementId)
        {
            var result = await _context.AnnouncementSupplierTemplate.Where(a => a.IsActive == true && a.AnnouncementId == announcementId).Select(n => new ExtendAnnouncementSupplierTemplateModel
            {
                AnnouncementId = n.AnnouncementId,
                AnnouncementIdString = Util.Encrypt(n.AnnouncementId),
                AnnouncementSupplierTemplateName = n.AnnouncementName,
                TemplateExtendMechanism = n.TemplateExtendMechanism,
                InsideKsa = n.InsidKsa,
                IntroAboutAnnouncementTemplate = n.IntroAboutAnnouncementTemplate,
                Details = n.Details,
                ActivityDescription = n.ActivityDescription,
                ActivityIds = n.AnnouncementActivities.Any() ? n.AnnouncementActivities.Select(s => s.ActivityId).ToList() : new List<int>(),
                AreasIds = n.AnnouncementAreas.Any() ? n.AnnouncementAreas.Select(s => s.AreaId).ToList() : new List<int>(),
                CountriesIds = n.AnnouncementCountries.Any() ? n.AnnouncementCountries.Select(s => s.CountryId).ToList() : new List<int>(),
                ConstructionWorkIds = n.AnnouncementConstructionWorks.Any() ? n.AnnouncementConstructionWorks.Select(s => s.ConstructionWorkId).ToList() : new List<int>(),
                MaintenanceRunnigWorkIds = n.AnnouncementMaintenanceRunnigWorks.Any() ? n.AnnouncementMaintenanceRunnigWorks.Select(s => s.MaintenanceRunningWorkId).ToList() : new List<int>(),
                BranchId = n.BranchId,
                AgencyCode = n.AgencyCode,
                AgencyName = n.Agency.NameArabic,
                BranchName = n.Branch.BranchName,
                CreatedBy = n.CreatedBy,
                AgencyEmail = n.AgencyEmail,
                AgencyPhone = n.AgencyPhone,
                AgencyFax = n.AgencyFax,
                AgencyAddress = n.AgencyAddress,
                Descriptions = n.Descriptions,
                RequirementConditionsToJoinList = n.RequirementConditionsToJoinList,
                EffectiveListDate = n.EffectiveListDate,
                IsEffectiveList = n.IsEffectiveList ?? false,
                IsRequiredAttachmentToJoinList = n.IsRequiredAttachmentToJoinList,
                AnnouncementTemplateSuppliersListTypeId = n.AnnouncementTemplateSuppliersListTypeId,
                TenderItemTypes = !string.IsNullOrEmpty(n.TenderTypesId) ? GetTendersTypes(n.TenderTypesId) : null,
                RequiredAttachment = n.RequiredAttachment,
                Attachments = n.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList(),
                EffectiveListTime = n.EffectiveListDate.HasValue ? n.EffectiveListDate.Value.ToGregorianDate("hh:mm tt") : "",
                ReferenceNumber = n.ReferenceNumber
            }).FirstOrDefaultAsync();
            return result;
        }


        public async Task<QueryResult<LinkedAgenciesAnnouncementTemplateModel>> GetBeneficiaryAgencyByAnnouncementIdAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
        {
            var result = await _context.LinkedAgenciesAnnouncementTemplate.Where(a => a.AnnouncementId == criteria.announcementId)
                .Select(a => new LinkedAgenciesAnnouncementTemplateModel
                {
                    AgencyName = a.Agency.NameArabic,
                    StatusName = a.IsActive == true ? Resources.AnnouncementSupplierTemplateResources.DisplayInputs.BeneficiaryAgencyStatusAdded : Resources.AnnouncementSupplierTemplateResources.DisplayInputs.BeneficiaryAgencyStatusRemoved,
                    CreatedDate = a.CreatedAt.ToHijriDateWithFormat("yyyy-MM-dd"),
                    RemovedDate = a.UpdatedAt.HasValue ? a.UpdatedAt.Value.ToHijriDateWithFormat("yyyy-MM-dd") : Resources.SharedResources.DisplayInputs.NotExist,
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }


        public async Task<int> TenderCount(int announcementId)
        {
            var notAllowedTenderStatus = new List<int>() { (int)Enums.TenderStatus.Canceled, (int)Enums.TenderStatus.Established, (int)Enums.TenderStatus.UnderEstablishing };
            return await _context.Tenders.CountAsync(d => d.AnnouncementTemplateId == announcementId && d.IsActive == true && !notAllowedTenderStatus.Any(a => a == d.TenderStatusId));
        }
    }
}