using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class QualificationQueries : IQualificationQueries
    {
        private IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QualificationQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<PreQualificationPartialDetailsModel> GetPrequalificationPartialDetails(int qualificationId, params Expression<Func<Tender, object>>[] includes)
        {

            var query = await _context.Tenders.Includes(includes)
           .Where(w => w.TenderId == qualificationId)
           .Select(w => new PreQualificationPartialDetailsModel
           {
               Activity = string.Join(Resources.SharedResources.DisplayInputs.And, w.TenderActivities.Select(e => e.Activity.NameAr)),
               CategoryName = w.TenderType.NameAr,
               QualificationName = w.TenderName,
               TenderActivities = w.TenderActivities.Select(r => r.Activity.NameAr).ToList(),
               EstablishingActions = w.TenderConstructionWorks.Select(r => r.ConstructionWork.NameAr).ToList(),
               MaintenanceOperationActions = w.TenderMaintenanceRunnigWorks.Select(r => r.MaintenanceRunningWork.NameAr).ToList(),
               CountryName = string.Join(Resources.SharedResources.DisplayInputs.And, w.TenderCountries.Select(s => s.Country.NameAr)),
               AreaName = string.Join(Resources.SharedResources.DisplayInputs.And, w.TenderAreas.Select(s => s.Area.NameAr)),
               InsideKSA = w.InsideKSA,
               Id = w.TenderId,
               IdEncrypted = Util.Encrypt(w.TenderId)
           })
         .FirstOrDefaultAsync();
            return query;

        }

        public async Task<Tender> FindPreQualificationByIdandStatus(int tenderId, int? statusId = 0)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Tenders
                .Include(t => t.OffersOpeningAddress)
                .Include(t => t.TenderAreas)
                .Include(t => t.Attachments)
                .Include(t => t.TenderCountries)
                .Include(t => t.TenderActivities)
                .Include(t => t.TenderHistories)
                .Include(t => t.TenderConstructionWorks)
                .Include(t => t.TenderMaintenanceRunnigWorks)
                                .Include(t => t.QualificationConfigurations)
                .Include(t => t.QualificationSubCategoryConfigurations)
                .WhereIf(statusId == 0, t => t.TenderId == tenderId)
                .WhereIf(statusId != 0, x => x.TenderId == tenderId
                && x.TenderStatusId == statusId).FirstOrDefaultAsync();
            return entities;
        }


        public async Task<Tender> FindTenderById(int tenderId, int? statusId = 0)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Tenders
                .WhereIf(statusId == 0, t => t.TenderId == tenderId)
                .WhereIf(statusId != 0, x => x.TenderId == tenderId
                && x.TenderStatusId == statusId).FirstOrDefaultAsync();
            return entities;
        }

        public async Task<List<string>> GetSubscriptionAwardingInformation(int qualificationId)
        {
            var suppliers = _context.QualificationFinalResult.Where(a => a.IsActive == true && a.TenderId == qualificationId && a.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded)
                 .Include(a => a.Supplier).Select(t => t.Supplier.SelectedCrName).ToList();



            return suppliers;
        }


        public async Task<List<CommitteeUser>> GetCheckingCommitteeMembersOnTender(int PreQualificaionCommiteeId)
        {
            var result = await _context.CommitteeUsers.Where(x => x.CommitteeId == PreQualificaionCommiteeId).ToListAsync();
            return result;
        }

        public async Task<QueryResult<Tender>> FindPreQualificationsBySearchCriteria(PreQualificationSearchCriteriaModel criteria, params Expression<Func<Tender, object>>[] includes)
        {
            Calendar hijri = new UmAlQuraCalendar();
            if (criteria.TenderAreasIdString != null)
            {
                criteria.TenderAreasIds = new List<int>(Array.ConvertAll(criteria.TenderAreasIdString.Split(','), int.Parse));
            }
            if (criteria.TenderStatusIdsString != null)
            {
                criteria.TenderStatusIds = new List<int>(Array.ConvertAll(criteria.TenderStatusIdsString.Split(','), int.Parse));
            }
            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established
                };
            }
            var result = await _context.Tenders
                .Includes(includes)
                .Where(x => x.IsActive == true)
                .Include(x => x.ChangeRequests)
                .ThenInclude(x => x.ChangeRequestStatus)
                .WhereIf(criteria.TenderTypeId == 0, x => x.TenderTypeId == (int)Enums.TenderType.PreQualification)
                .WhereIf(criteria.TenderTypeId > 0, x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(criteria.UserId != 0, q => q.TenderHistories.OrderByDescending(x => x.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.Approved && x.UserId == criteria.UserId) != null)
                .Where(x => x.AgencyCode == criteria.AgencyCode)
                .WhereIf(criteria.TenderStatusId != null, x => x.TenderStatusId == criteria.TenderStatusId)
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.ApprovedBy), q => q.TenderHistories.OrderByDescending(x => x.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.Approved && x.UserId == GetUserIdForApprovedBy(criteria.ApprovedBy)) != null)
                .WhereIf(criteria.CreatedBy != null, x => x.CreatedBy.ToLower() == criteria.CreatedBy.ToLower())
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Contains(criteria.TenderName.ToUpper()))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => criteria.TenderStatusIds.Contains(x.TenderStatusId))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public Task<RegistryReportForQualificationModel> GetQualificationOffersRegistryReportData(int qualificationId)
        {
            var arProvider = CultureInfo.CreateSpecificCulture("ar-SA");

            var result = _context.Tenders.Where(t => t.IsActive == true && t.TenderId == qualificationId)
                .Select(t => new RegistryReportForQualificationModel
                {
                    Id = t.CreatedAt.ToString("ddMMyyyy"),
                    QualificationCheckingDate = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    QualificationName = t.TenderName,
                    QualificationNumber = t.ReferenceNumber,
                    QualificationParticipantsNumber = t.PreQualificationApplyDocuments.Where(x => x.IsActive == true).Count().ToString(),
                    QualificationReqiredPoints = t.TenderPointsToPass.ToString(),
                    QualificationSubmitionDateString = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    QualificationTypeId = t.QualificationTypeId.HasValue ? t.QualificationTypeId.Value : 0,
                    PreviousExperienceYearPercentage = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear).FirstOrDefault().Percentage.ToString() + "%",
                    ExistingContractualObligationsPercentage = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations).FirstOrDefault().Percentage.ToString() + "%",
                    HumanResourcePercentage = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource).FirstOrDefault().Percentage.ToString() + "%",
                    QualityPercentage = t.QualificationTypeId != (int)Enums.PreQualificationType.Small ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Quality).FirstOrDefault().Percentage.ToString() + "%" : "",
                    EnviromentAndHealthyPercentage = t.QualificationTypeId != (int)Enums.PreQualificationType.Small ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.EnviromentAndHealthy).FirstOrDefault().Percentage.ToString() + "%" : "",
                    InsurancePercentage = t.QualificationTypeId == (int)Enums.PreQualificationType.Large ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Insurance).FirstOrDefault().Percentage.ToString() + "%" : "",
                    FinancialStatementsPercentage = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements).FirstOrDefault().Percentage.ToString() + "%",
                    PassedQualificationSubCategoryWeights = t.QualificationFinalResults.Where(q => q.Tender.PreQualificationApplyDocuments.Any(c => c.IsActive ==true && c.StatusId == (int)Enums.QualificationStatus.Received && c.SupplierId == q.SupplierSelectedCr) && q.IsActive == true && q.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded).Select(p => new QualificationSubCategoryWeights
                    {
                        QualificationResultValueString = p.ResultValue.ToString(),
                        QualificationResultString = p.QualificationLookup.Name,
                        PreviousExperienceYearWeight = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString(),
                        ExistingContractualObligationsWeight = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString(),
                        HumanResourceWeight = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString(),
                        QualityWeight = t.QualificationTypeId != (int)Enums.PreQualificationType.Small ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Quality && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString() : "",
                        EnviromentAndHealthyWeight = t.QualificationTypeId != (int)Enums.PreQualificationType.Small ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.EnviromentAndHealthy && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString() : "",
                        InsuranceWeight = t.QualificationTypeId == (int)Enums.PreQualificationType.Large ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Insurance && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString() : "",
                        FinancialStatementsWeight = t.QualificationTypeId == (int)Enums.PreQualificationType.Large ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements).FirstOrDefault().ResultValue.ToString() : "11",
                        SupplierName = p.Supplier.SelectedCrName
                    }).ToList(),
                    FailedQualificationSubCategoryWeights = t.QualificationFinalResults.Where(q => q.Tender.PreQualificationApplyDocuments.Any(c => c.IsActive == true && c.StatusId == (int)Enums.QualificationStatus.Received && c.SupplierId == q.SupplierSelectedCr) && q.IsActive == true && q.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed).Select(p => new QualificationSubCategoryWeights
                    {
                        QualificationResultValueString = p.ResultValue.ToString(),
                        QualificationResultString = p.QualificationLookup.Name,
                        PreviousExperienceYearWeight = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.PreviousExperienceYear && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString(),
                        ExistingContractualObligationsWeight = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.ExistingContractualObligations && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString(),
                        HumanResourceWeight = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.HumanResource && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString(),
                        QualityWeight = t.QualificationTypeId != (int)Enums.PreQualificationType.Small ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Quality && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString() : "",
                        EnviromentAndHealthyWeight = t.QualificationTypeId != (int)Enums.PreQualificationType.Small ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.EnviromentAndHealthy && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString() : "",
                        InsuranceWeight = t.QualificationTypeId == (int)Enums.PreQualificationType.Large ? t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.Insurance && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString() : "",
                        FinancialStatementsWeight = t.QualificationSubCategoryResults.Where(s => s.QualificationSubCategoryId == (int)Enums.QualificationSubCategory.FinancialStatements && s.SupplierSelectedCr == p.Supplier.SelectedCr).FirstOrDefault().ResultValue.ToString(),
                        SupplierName = p.Supplier.SelectedCrName
                    }).ToList(),
                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<QueryResult<PreQualificationCardModel>> FindPreQualificationBySearchCriteriaForCheckingStage(PreQualificationSearchCriteriaModel criteria)
        {
            var arProvider = CultureInfo.CreateSpecificCulture("ar-SA");

            var result = await _context.Tenders
                .Where(x => x.IsActive == true)
                .Where(x => x.BranchId == criteria.BranchId)
                .Where(x => x.TenderTypeId == (int)Enums.TenderType.PreQualification)
                                .WhereIf(criteria.TenderStatusId != 0 && criteria.TenderStatusId != null, x => x.TenderStatusId == criteria.StatusId)
                .WhereIf(criteria.CreatedBy != null, x => x.CreatedBy.ToLower() == criteria.CreatedBy.ToLower())
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.ApprovedBy), q => q.TenderHistories.OrderByDescending(x => x.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.Approved && x.UserId == GetUserIdForApprovedBy(criteria.ApprovedBy)) != null)
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Contains(criteria.TenderName.ToUpper()))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => criteria.TenderStatusIds.Contains(x.TenderStatusId))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(x => new PreQualificationCardModel
                {
                    ActivityDescription = x.ActivityDescription,
                    AgencyCode = x.AgencyCode,
                    AgencyName = x.Agency.NameArabic,
                    InsideKSA = x.InsideKSA,
                    IsFavouriteQualification = x.FavouriteSupplierTenders.Any(f => f.SupplierCRNumber == criteria.cr && f.IsActive == true),
                    LastEnqueriesDate = x.LastEnqueriesDate,
                    LastEnqueriesDateHijri = x.LastEnqueriesDate.HasValue ? x.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDate = x.LastOfferPresentationDate,
                    LastOfferPresentationDateHijri = x.LastOfferPresentationDate.HasValue ? x.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersCheckingDate = x.OffersCheckingDate.Value,
                    OffersCheckingDateHijri = x.OffersCheckingDate.HasValue ? x.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    QualificationName = x.TenderName,
                    QualificationNumber = x.TenderNumber,
                    SubmitionDate = x.SubmitionDate,
                    SubmitionDateHijri = x.SubmitionDate.HasValue ? x.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    SupplierHasOffer = x.PreQualificationApplyDocuments.Any(r => r.IsActive == true && r.SupplierId == criteria.cr),
                    TenderId = x.TenderId,
                    QualificationStatusName = x.Status.NameAr,
                    QualificationTypeName = x.TenderType.NameAr,
                    TenderTypeId = x.TenderTypeId,
                    TenderIdString = Util.Encrypt(x.TenderId),
                    TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                    TenderStatusId = x.TenderStatusId,
                    TenderChangeRequestIds = x.ChangeRequests.Where(c => c.IsActive == true).Select(y => y.TenderChangeRequestId).ToList(),
                    ChangeRequestStatusIds = x.ChangeRequests.Where(c => c.IsActive == true).Select(y => y.ChangeRequestStatusId).ToList(),
                    UserCommitteeType = x.OffersCheckingCommittee != null ?
                    (x.OffersCheckingCommittee.CommitteeUsers.Any(c => c.UserProfileId == criteria.UserId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckManager) ? (int)Enums.UserRole.NewMonafasat_OffersCheckManager : (int)Enums.UserRole.NewMonafasat_OffersCheckSecretary) : (int)Enums.UserRole.Anonymous,
                    QualificationReferenceNumber = x.ReferenceNumber
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }



        public async Task<QueryResult<PreQualificationCardModel>> GetAllSupplierPreQualificationsBySearchCriteria(PreQualificationSearchCriteriaModel criteria, List<SupplierAgencyBlockModel> allSupplierBlock, params Expression<Func<Tender, object>>[] includes)
        {
            if (!string.IsNullOrEmpty(criteria.TenderAreasIdString))
            {
                criteria.TenderAreasIds = new List<int>(Array.ConvertAll(criteria.TenderAreasIdString.Split(','), int.Parse));
            }

            var result = await _context.Tenders
                .Where(x => x.IsActive == true && x.SubmitionDate != null && x.TenderStatusId != (int)Enums.TenderStatus.Rejected && x.TenderStatusId != (int)Enums.TenderStatus.Canceled && x.TenderStatusId != (int)Enums.TenderStatus.QualificationUnderEstablishingFromCommittee && x.TenderStatusId != (int)Enums.TenderStatus.QualificationCommitteeApproval && x.TenderStatusId != (int)Enums.TenderStatus.PendingQualificationCommitteeManagerApproval && x.TenderStatusId != (int)Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager && x.AgencyCode != criteria.cr)

                .WhereIf(criteria.OnlyPersonalQualifications != null && criteria.OnlyPersonalQualifications == true, x => x.QualificationSupplierData.Any(a => a.SupplierSelectedCr == criteria.cr) || x.PostQualificationInvitations.Any(a => a.CommercialNumber == criteria.cr))
                .WhereIf(criteria.OnlyGetFavouriteQualifications == true, x => x.FavouriteSupplierTenders.ToList().Any(t => t.IsActive == true && t.SupplierCRNumber == criteria.cr))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Contains(criteria.TenderName.ToUpper()))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
                .WhereIf(criteria.TenderTypeId == 0, x => (x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification && x.PostQualificationInvitations.Any(a => a.CommercialNumber == criteria.cr)))
                .WhereIf(criteria.TenderTypeId == (int)Enums.TenderType.PreQualification, x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(criteria.TenderTypeId == (int)Enums.TenderType.PostQualification, x => x.TenderTypeId == (int)Enums.TenderType.PostQualification && x.PostQualificationInvitations.Any(a => a.CommercialNumber == criteria.cr))
                .WhereIf(criteria.FromLastOfferPresentationDate != null && criteria.ToLastOfferPresentationDateString != null, x => x.LastOfferPresentationDate >= criteria.FromLastOfferPresentationDate && x.LastOfferPresentationDate <= criteria.ToLastOfferPresentationDate)
                .WhereIf(criteria.TenderActivityId != null && criteria.TenderActivityId != 0, x => x.TenderActivities.Any(a => a.Activity.ActivityId == criteria.TenderActivityId))
                .WhereIf(criteria.TenderAreasIds != null && criteria.TenderAreasIds.Count != 0, x => x.TenderAreas.Any(a => criteria.TenderAreasIds.Contains(a.AreaId)))
                .WhereIf(!String.IsNullOrEmpty(criteria.PublishDate), (x => x.SubmitionDate.Value.AddDays(criteria.SubmitionDays) >= DateTime.Now))
                .WhereIf(criteria.TenderCategory == (int)Enums.TenderCategory.ActiveTenders, x => x.TenderStatusId == (int)Enums.TenderStatus.Approved && x.LastOfferPresentationDate >= DateTime.Now)
                .WhereIf(criteria.TenderCategory == (int)Enums.TenderCategory.EndedTenders, x => x.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed)
                //.WhereIf(criteria.OnlyGetFavouriteQualifications, x => x.FavouriteSupplierTenders.Exists(f => f.TenderId == x.TenderId && f.SupplierCRNumber == criteria.cr && f.IsActive == true))
                .WhereIf(!String.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
                .WhereIf(criteria.TenderStatusId != null && criteria.TenderStatusId != 0, x => x.TenderStatusId == criteria.StatusId)
                                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(s => new PreQualificationCardModel
                {
                    ActivityDescription = s.ActivityDescription,
                    AgencyCode = s.AgencyCode,
                    AgencyName = s.Agency.NameArabic,
                    CreatedDate = s.CreatedAt,
                    TenderStatusId = s.TenderStatusId,
                    LastEnqueriesDate = s.LastEnqueriesDate,
                    LastOfferPresentationDate = s.LastOfferPresentationDate,
                    InsideKSA = s.InsideKSA,
                    TenderId = s.TenderId,
                    TenderTypeId = s.TenderTypeId,
                    TechnicalOrganizationId = s.TechnicalOrganizationId,
                    /*          IsBlocked = CheckBlocked(allSupplierBlock, s.AgencyCode), */
                    SubmitionDate = s.SubmitionDate,
                    OffersCheckingDate = s.OffersCheckingDate.Value,
                    QualificationNumber = s.TenderNumber,
                    TenderChangeRequestIds = s.ChangeRequests.Where(c => c.IsActive == true).Select(r => r.TenderChangeRequestId).ToList(),
                    ChangeRequestStatusIds = s.ChangeRequests.Where(c => c.IsActive == true).Select(r => r.ChangeRequestStatusId).ToList(),
                    QualificationTypeName = s.TenderType.NameAr,
                    IsFavouriteQualification = s.FavouriteSupplierTenders.Any(f => f.SupplierCRNumber == criteria.cr && f.IsActive == true),
                    SupplierHasOffer = s.PreQualificationApplyDocuments.Any(r => r.IsActive == true && r.SupplierId == criteria.cr),
                    QualificationStatusId = s.PreQualificationApplyDocuments.Any(r => r.IsActive == true && r.SupplierId == criteria.cr) ? s.PreQualificationApplyDocuments.FirstOrDefault(r => r.IsActive == true && r.SupplierId == criteria.cr).StatusId : null,
                    QualificationName = s.TenderName,
                    QualificationReferenceNumber = s.ReferenceNumber,
                    CanApplyPostQualificationDocument = (s.PostQualificationInvitations.Any(h => h.CommercialNumber == criteria.cr && s.IsActive == true)
                    && (!s.PreQualificationApplyDocuments.Any(r => r.IsActive == true && r.SupplierId == criteria.cr))
                    && s.LastOfferPresentationDate >= DateTime.Now.Date
                    && s.TenderStatusId == (int)Enums.TenderStatus.Approved && s.TenderTypeId == (int)Enums.TenderType.PostQualification)
                })
                                .ToQueryResult(criteria.PageNumber, criteria.PageSize);

            foreach (var item in result.Items)
            {
                item.OffersCheckingDateHijri = item.OffersCheckingDate.HasValue ? item.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
                item.LastOfferPresentationDateHijri = item.LastOfferPresentationDate.HasValue ? item.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
                item.LastEnqueriesDateHijri = item.LastEnqueriesDate.HasValue ? item.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
                item.TenderIdString = Util.Encrypt(item.TenderId);
                item.TenderStatusIdString = Util.Encrypt(item.TenderStatusId);

                item.IsBlocked = CheckBlocked(allSupplierBlock, item.AgencyCode);
                item.SubmitionDateHijri = item.SubmitionDate.HasValue ? item.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "";
            }
            return result;
        }

        private static bool CheckBlocked(List<SupplierAgencyBlockModel> allSupplierBlock, string agencyCode)
        {
            var blocked = allSupplierBlock.Any(b => b.AgencyCode == agencyCode || b.AgencyCode == null);
            return blocked;
        }

        public async Task<QueryResult<PreQualificationCardModel>> GetAllVistorQualificationBySearchCriteria(PreQualificationSearchCriteriaModel criteria)
        {
            if (criteria.TenderAreasIdString != null)
            {
                criteria.TenderAreasIds = new List<int>(Array.ConvertAll(criteria.TenderAreasIdString.Split(','), int.Parse));
            }
            var result = await _context.Tenders
     .Where(x => x.IsActive == true && x.SubmitionDate != null && x.TenderStatusId != (int)Enums.TenderStatus.QualificationCommitteeApproval && x.TenderStatusId != (int)Enums.TenderStatus.QualificationUnderEstablishingFromCommittee && x.TenderStatusId != (int)Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager && x.TenderStatusId != (int)Enums.TenderStatus.PendingQualificationCommitteeManagerApproval && x.TenderStatusId != (int)Enums.TenderStatus.Rejected && x.TenderStatusId != (int)Enums.TenderStatus.Canceled && x.AgencyCode != criteria.cr)

     .WhereIf(criteria.OnlyPersonalQualifications != null && criteria.OnlyPersonalQualifications == true, x => x.QualificationSupplierData.Any(a => a.SupplierSelectedCr == criteria.cr) || x.PostQualificationInvitations.Any(a => a.CommercialNumber == criteria.cr))
     .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Contains(criteria.TenderName.ToUpper()))
     .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
     .WhereIf(!String.IsNullOrWhiteSpace(criteria.ReferenceNumber), x => x.ReferenceNumber.Contains(criteria.ReferenceNumber))
     .WhereIf(criteria.TenderTypeId == 0, x => (x.TenderTypeId == (int)Enums.TenderType.PreQualification))
      .WhereIf(criteria.TenderTypeId == (int)Enums.TenderType.PreQualification, x => x.TenderTypeId == criteria.TenderTypeId)
                     .WhereIf(criteria.FromLastOfferPresentationDate != null && criteria.ToLastOfferPresentationDateString != null, x => x.LastOfferPresentationDate >= criteria.FromLastOfferPresentationDate && x.LastOfferPresentationDate <= criteria.ToLastOfferPresentationDate)
     .WhereIf(criteria.TenderSubActivityId != null && criteria.TenderSubActivityId != 0, x => x.TenderActivities.Any(a => a.Activity.ActivityId == criteria.TenderSubActivityId))
     .WhereIf(criteria.TenderAreasIds != null && criteria.TenderAreasIds.ToArray().Length != 0, x => x.TenderAreas.Any(a => criteria.TenderAreasIds.ToArray().Contains(a.AreaId)))
     .WhereIf(!String.IsNullOrEmpty(criteria.PublishDate), (x => x.SubmitionDate.Value.AddDays(criteria.SubmitionDays) >= DateTime.Now))
     .WhereIf(criteria.TenderCategory == (int)Enums.TenderCategory.ActiveTenders, x => x.TenderStatusId == (int)Enums.TenderStatus.Approved && x.LastOfferPresentationDate >= DateTime.Now).WhereIf(criteria.TenderCategory == (int)Enums.TenderCategory.EndedTenders, x => x.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed).WhereIf(criteria.OnlyGetFavouriteQualifications, x => x.FavouriteSupplierTenders.Exists(f => f.TenderId == x.TenderId && f.SupplierCRNumber == criteria.cr && f.IsActive == true))
     .WhereIf(!String.IsNullOrEmpty(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
     .WhereIf(criteria.TenderStatusId != null && criteria.TenderStatusId != 0, x => x.TenderStatusId == criteria.StatusId)
                     .SortBy(criteria.Sort, criteria.SortDirection)
     .Select(s => new PreQualificationCardModel
     {
         ActivityDescription = s.ActivityDescription,

         AgencyCode = s.AgencyCode,
         AgencyName = s.Agency.NameArabic,
         CreatedDate = s.CreatedAt,
         TenderStatusId = s.TenderStatusId,
         LastEnqueriesDate = s.LastEnqueriesDate,
         LastOfferPresentationDate = s.LastOfferPresentationDate,
         InsideKSA = s.InsideKSA,
         TenderId = s.TenderId,
         TenderTypeId = s.TenderTypeId,
         TechnicalOrganizationId = s.TechnicalOrganizationId,
         SubmitionDate = s.SubmitionDate,
         OffersCheckingDate = s.OffersCheckingDate.Value,
         QualificationNumber = s.TenderNumber,
         TenderChangeRequestIds = s.ChangeRequests.Where(c => c.IsActive == true).Select(r => r.TenderChangeRequestId).ToList(),
         ChangeRequestStatusIds = s.ChangeRequests.Where(c => c.IsActive == true).Select(r => r.ChangeRequestStatusId).ToList(),
         QualificationTypeName = s.TenderType.NameAr,
         IsFavouriteQualification = s.FavouriteSupplierTenders.Any(f => f.SupplierCRNumber == criteria.cr && f.IsActive == true),
         SupplierHasOffer = s.PreQualificationApplyDocuments.Any(r => r.IsActive == true && r.SupplierId == criteria.cr),
         QualificationName = s.TenderName,
         QualificationReferenceNumber = s.ReferenceNumber,
         CanApplyPostQualificationDocument = (s.PostQualificationInvitations.Any(h => h.CommercialNumber == criteria.cr && s.IsActive == true)
         && (!s.PreQualificationApplyDocuments.Any(r => r.IsActive == true && r.SupplierId == criteria.cr))
         && s.LastOfferPresentationDate >= DateTime.Now.Date
         && s.TenderStatusId == (int)Enums.TenderStatus.Approved && s.TenderTypeId == (int)Enums.TenderType.PostQualification)
     })
     .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            foreach (var item in result.Items)
            {
                item.TenderIdString = Util.Encrypt(item.TenderId);
                item.TenderStatusIdString = Util.Encrypt(item.TenderStatusId);
                item.LastEnqueriesDateHijri = item.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd");
                item.LastOfferPresentationDateHijri = item.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd");
                item.SubmitionDateHijri = item.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd");
                item.OffersCheckingDateHijri = item.OffersCheckingDate != null ? item.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : null;
            }

            return result;
        }

        public async Task<QueryResult<PreQualificationResultForChecking>> GetSupplierPreQualificationRequestByQualificationIdAsync(QualificationIdWithSearchCriteria qualificationSearch)
        {
            var result = await _context.QualificationFinalResult
               .Where(t => t.TenderId == qualificationSearch.TenderId && t.IsActive == true)
               .Where(t => t.Tender.PreQualificationApplyDocuments.Any(p => p.IsActive == true && p.SupplierId == t.SupplierSelectedCr && p.StatusId == (int)Enums.QualificationStatus.Received))
               .Select(x => new PreQualificationResultForChecking
               {
                   SupplierId = x.SupplierSelectedCr,
                   ComercialName = x.Supplier.SelectedCrName,
                   ComercialNumber = x.SupplierSelectedCr,
                   FinalResultPoints = x.ResultValue,
                   FinalResultStatus = x.QualificationLookup.Name,
                   PreQualificationIdString = Util.Encrypt(x.TenderId),
                   PreQualificationId = x.TenderId,
                   TenderPoints = x.Tender.TenderPointsToPass,
               }).ToQueryResult(qualificationSearch.PageNumber, 6);
            return result;
        }
        public async Task<PreQualificationSavingModel> GetPreQualificationEditingData(int Id)
        {

            Check.ArgumentNotNullOrEmpty(nameof(Id), Id.ToString());
            var tender = await _context.Tenders
                .Where(t => t.TenderId == Id && t.IsActive == true)
                .Select(t => new PreQualificationSavingModel
                {
                    TenderStatusId = t.TenderStatusId,
                    TenderAreaIDs = t.TenderAreas.Select(a => a.AreaId).ToList(),
                    TenderCountriesIDs = t.TenderCountries.Select(a => a.CountryId).ToList(),
                    TenderActivitieIDs = t.TenderActivities.Select(a => a.ActivityId).ToList(),
                    TenderConstructionWorkIDs = t.TenderConstructionWorks.Select(a => a.ConstructionWorkId).ToList(),
                    TenderMentainanceRunnigWorkIDs = t.TenderMaintenanceRunnigWorks.Select(a => a.MaintenanceRunningWorkId).ToList(),
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("hh:mm tt") : "",
                    Attachments = t.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList(),
                    AgencyCode = t.AgencyCode,
                    RemainingDays = t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Days : 0,
                    RemainingHours = t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Hours : 0,
                    RemainingMins = t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Minutes : 0,
                    PreQualificationCommitteeId = t.PreQualificationCommitteeId,
                    TechnicalOrganizationId = t.TechnicalOrganizationId,
                    TenderName = t.TenderName,
                    ActivityDescription = t.ActivityDescription,
                    Details = t.Details,
                    ReferenceNumber = t.ReferenceNumber,
                    CreatedBy = t.CreatedBy,
                    QualificationTypeId = t.QualificationTypeId.HasValue ? t.QualificationTypeId.Value : 0,
                    TenderPointsToPass = t.TenderPointsToPass,
                    TechnicalAdministrativeCapacity = t.TechnicalAdministrativeCapacity,
                    FinancialCapacity = t.FinancialCapacity
                }).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<PrequalificationTechnicalExaminationModel> GetPrequalificationTechnicalExamination(int Id)
        {
            var tender = await _context.Tenders
                .Where(t => t.TenderId == Id)
                .Select(t => new PrequalificationTechnicalExaminationModel
                {
                    PrequalificationId = t.TenderId,
                    QualificationNumber = t.ReferenceNumber,
                    SubmitionDate = t.SubmitionDate,
                    SupplierList = t.PreQualificationApplyDocuments.Select(x => new PreQualificationResultForChecking
                    {
                        ComercialName = x.Supplier.SelectedCrName,
                        ComercialNumber = x.Supplier.SelectedCr,
                    }).ToList(),
                }).FirstOrDefaultAsync();
            return tender;
        }
        public async Task<Tender> GetPreQualificationDetailsById(int Id)
        {
            Check.ArgumentNotNullOrEmpty(nameof(Id), Id.ToString());
            var tenderRelationsEntity = await _context.Tenders
                .Include(t => t.TenderActivities)
                .Include(i => i.Agency)
                .Include(t => t.Attachments)
                .Include(t => t.TenderConstructionWorks)
                .Include(t => t.TenderMaintenanceRunnigWorks)
                .Include(t => t.Invitations)
                .Include(t => t.TenderCountries)
                .ThenInclude(c => c.Country)
                .Include(t => t.FavouriteSupplierTenders)
                .Include(t => t.TenderAreas)
                .ThenInclude(a => a.Area)
                .Where(t => t.TenderId == Id/*  && t.TenderTypeId == (int)Enums.TenderType.PreQualification  && t.TenderStatusId == (int)Enums.TenderStatus.Approved*/).FirstOrDefaultAsync();
            return tenderRelationsEntity;
        }


        public async Task<PreQualificationDetailsModel> GetPreQualificationDetailsModelById(int Id, int branchId)
        {


            Check.ArgumentNotNullOrEmpty(nameof(Id), Id.ToString());

            var tenderRelationsEntity = await _context.Tenders
            .Where(t => t.TenderId == Id /*&& t.TenderStatusId == (int)Enums.TenderStatus.Approved*/)
            .WhereIf(branchId != 0, c => c.BranchId == branchId).Select(t => new PreQualificationDetailsModel
            {
                TenderIdString = Util.Encrypt(t.TenderId),
                InsideKSA = t.InsideKSA,
                TenderName = t.TenderName,
                ReferenceNumber = t.ReferenceNumber,
                OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("hh:mm tt") : "",
                LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                AgencyName = t.Agency.NameArabic,
                LastOfferPresentationDateString = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                LastOfferPresentationDate = t.LastOfferPresentationDate,
                LastOfferPresentationDateHijri = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                LastOfferPresentationDateHijriString = (t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                LastEnqueriesDate = t.LastEnqueriesDate,
                LastEnqueriesDateString = (t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                LastEnqueriesDateHijriString = (t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                SubmitionDate = t.SubmitionDate,
                SubmitionDateString = (t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                SubmitionDateHijri = (t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                TenderActivities = t.TenderActivities != null ? t.TenderActivities.Select(r => r.Activity.NameAr).ToList() : null,
                EstablishingActions = t.TenderConstructionWorks != null ? t.TenderConstructionWorks.Select(r => r.ConstructionWork.NameAr).ToList() : null,
                MaintenanceOperationActions = t.TenderMaintenanceRunnigWorks != null ? t.TenderMaintenanceRunnigWorks.Select(r => r.MaintenanceRunningWork.NameAr).ToList() : null,
                RemainingDays = t.LastOfferPresentationDate.HasValue ? (t.LastOfferPresentationDate.Value - DateTime.Now).Days : 0,
                RemainingHours = t.LastOfferPresentationDate.HasValue ? ((t.LastOfferPresentationDate.Value - DateTime.Now).Hours) : 0,
                RemainingMins = t.LastOfferPresentationDate.HasValue ? ((t.LastOfferPresentationDate.Value - DateTime.Now).Minutes) : 0,
                CreatedBy = t.CreatedBy,
                TenderTypeId = t.TenderTypeId,
                CheckingCommitteeName = t.PreQualificationCommitteeId.HasValue ? t.OffersPreQualificationCommittee.CommitteeName : "",
                DirectPurchaseCommitteName = t.DirectPurchaseCommitteeId.HasValue ? t.DirectPurchaseCommittee.CommitteeName : "",
                OffersCheckingCommitteeName = t.OffersCheckingCommitteeId.HasValue ? t.OffersCheckingCommittee.CommitteeName : "",
                TechnicalOrganizationName = t.TechnicalOrganizationId.HasValue ? t.TechnicalOrganization.CommitteeName : "",
                PostQualificationRelatedTenderDetailsModel = (t.TenderTypeId != (int)Enums.TenderType.PostQualification ? null : new PostQualificationRelatedTenderDetailsModel
                {

                    TenderName = t.PostQualificationTender.TenderName,
                    TenderTypeName = t.PostQualificationTender.TenderType.NameAr,
                    TenderReferenceNumber = t.PostQualificationTender.ReferenceNumber,
                    lstOfSupplierInvitation = t.PostQualificationInvitations.Where(S => S.IsActive == true).Select(p => new PostQualificationSuppliersInvitationsModel
                    {
                        SupplierCR = p.CommercialNumber,
                        SupplierName = p.Supplier.SelectedCrName
                    }).ToList(),
                }),
                SupplierPreQualificationDocumentModel = (t.TenderTypeId != (int)Enums.TenderType.PreQualification ? null : t.QualificationFinalResults.Where(S => S.IsActive == true).Select(
                    p => new SupplierPreQualificationDocumentModel
                    {
                        SupplierId = p.Supplier.SelectedCr,
                        SupplierName = p.Supplier.SelectedCrName
                    }).ToList()
                    ),
                ActivityDescription = t.ActivityDescription,
                Details = t.Details,
                OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                DirectPurchaseCommitteeId = t.DirectPurchaseCommitteeId,
                OffersCheckingDateString = (t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("dd/MM/yyyy") : ""),
                OffersCheckingDateHijriString = (t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : ""),
                Areas = t.TenderAreas.Select(s => new AreaModel { NameAr = s.Area.NameAr }).ToList(),
                Countries = t.TenderCountries.Select(c => new CountryModel { Name = c.Country.NameAr }).ToList(),
                Attachments = t.Attachments.Select(a => new TenderAttachmentModel
                {
                    Name = a.Name,
                    FileNetReferenceId = a.FileNetReferenceId
                }).ToList()
            })
                .FirstOrDefaultAsync();
            return tenderRelationsEntity;
        }


        public async Task<int> GetPreQualificationStatus(int Id)
        {
            var res = await _context.Tenders
            .Where(t => t.TenderId == Id)
            .Select(w => w.TenderStatusId)
            .FirstOrDefaultAsync();
            return res;
        }

        public async Task<QualificationStatusModel> GetPrequalificationDetails(int qualificationId)
        {
            var res = await _context.Tenders
            .Where(t => t.TenderId == qualificationId)
            .Select(w => new QualificationStatusModel
            {
                StatusId = w.TenderStatusId,
                PreQualificationTypeId = w.QualificationTypeId,
                PreQualificationCommitteeId = w.PreQualificationCommitteeId,
                tenderId = w.TenderId,
                AgencyCode = w.AgencyCode,
                TenderIdString = Util.Encrypt(w.TenderId),
                IsFavouriteTender = false,
                RejectionReason = (w.TenderStatusId == (int)Enums.TenderStatus.Rejected ? w.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason :
                w.ChangeRequests.OrderByDescending(th => th.TenderChangeRequestId).FirstOrDefault().CancelationReasonDescription),
                TenderTypeId = w.TenderTypeId,
                RejectionReasonSelectedStr = (w.TenderStatusId == (int)Enums.TenderStatus.Rejected ? w.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason :
                w.ChangeRequests.OrderByDescending(th => th.TenderChangeRequestId).FirstOrDefault().CancelationReason.NameAr)
            })
            .FirstOrDefaultAsync();
            return res;
        }

        public async Task<QualificationStatusModel> GetPrequalificationDetailsForSupplier(int qualificationId, string CR)
        {
            var res = await _context.Tenders
            .Where(t => t.TenderId == qualificationId)
            .Select(w => new QualificationStatusModel
            {
                StatusId = w.TenderStatusId,
                PreQualificationTypeId = w.QualificationTypeId,
                tenderId = w.TenderId,
                LastEnqueriesDate = w.LastEnqueriesDate,
                TenderIdString = Util.Encrypt(w.TenderId),
                IsFavouriteTender = w.FavouriteSupplierTenders.Any(f => f.SupplierCRNumber == CR && f.IsActive == true),
                RejectionReason = (w.TenderStatusId == (int)Enums.TenderStatus.Rejected ? w.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason :
                w.ChangeRequests.OrderByDescending(th => th.TenderChangeRequestId).FirstOrDefault().CancelationReasonDescription),
                TenderTypeId = w.TenderTypeId,
                isSupplierPassed = w.QualificationFinalResults.Any(p => p.SupplierSelectedCr == CR && p.QualificationLookupId == (int)Enums.QualificationResultLookup.Succeeded),
                isSupplierParticipating = w.PreQualificationApplyDocuments.Any(p => p.SupplierId == CR)
            })
            .FirstOrDefaultAsync();
            return res;
        }


        public async Task<PreQualificationBasicDetailsModel> GetPreQualificationDetailsByIdForChecking(int tenderId)
        {
            var query = await _context.Tenders
            .Where(t => t.TenderId == tenderId)
            .Select(w => new PreQualificationBasicDetailsModel
            {
                QualificationName = w.TenderName,
                TenderActivities = w.TenderActivities.Select(s => s.Activity.NameAr).ToList(),
                TenderConstructionWorks = w.TenderConstructionWorks.Select(s => s.ConstructionWork.NameAr).ToList(),
                TenderMaintenanceRunnigWorks = w.TenderMaintenanceRunnigWorks.Select(s => s.MaintenanceRunningWork.NameAr).ToList(),
                TenderIdString = Util.Encrypt(w.TenderId),
                Createby = w.CreatedBy,
                TenderStatusId = w.TenderStatusId,
                InsideKSA = w.InsideKSA,
                unCheckedSupplierDocuments = w.PreQualificationApplyDocuments.Where(a => a.PreQualificationResult == 0 || a.PreQualificationResult == null).Count(),
                RejectionReason = w.TenderHistories.Where(h => h.StatusId == w.TenderStatusId).OrderByDescending(h => h.TenderHistoryId).FirstOrDefault().RejectionReason
            })
            .FirstOrDefaultAsync();
            return query;
        }
        public async Task<Tender> GetQualificationById(int Id)
        {
            var tender = await _context.Tenders
                .Where(t => t.IsActive == true && t.TenderId == Id).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<TenderDatesModel> FindQualificationDatesByQualificationId(int qualificationId)
        {
            var entities = await _context.Tenders.AsTracking()
              .Include(t => t.Status)
              .Where(t => t.TenderId == qualificationId)
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
                  LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToString("HH:mm tt") : "",
                  OffersOpeningDate = t.OffersOpeningDate,
                  OffersOpeningTime = t.OffersOpeningDate.HasValue ? t.OffersOpeningDate.Value.ToString("HH:mm tt") : "",
                  OffersCheckingDate = t.OffersCheckingDate,
                  OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("HH:mm tt") : "",
                  TenderName = t.TenderName,
                  ReferenceNumber = t.ReferenceNumber,
                  TenderNumber = t.TenderNumber,
                  PreQualificationId = t.PreQualificationId,
                  InvitationTypeId = t.InvitationTypeId,
                  InitialGuaranteePercentage = t.InitialGuaranteePercentage,
                  SupplierNeedSample = t.SupplierNeedSample,
                  SamplesDeliveryAddress = t.SamplesDeliveryAddress,
                  NeedInitialGuarantee = t.InitialGuaranteeAddress != null ? true : false,
                  InitialGuaranteeAddress = t.InitialGuaranteeAddress,
                  BuildingName = t.BuildingName,
                  FloarNumber = t.FloarNumber,
                  DepartmentName = t.DepartmentName,
                  DeliveryDate = t.DeliveryDate,
                  DeliveryTime = t.DeliveryDate.HasValue ? t.DeliveryDate.Value.ToString("hh:mm tt") : "",
                  QuantityTableVersionId = t.QuantityTableVersionId
              }).FirstOrDefaultAsync();
            return entities;
        }
        public async Task<Tender> GetQualificationByIdWithChangRequest(int Id)
        {
            var tender = await _context.Tenders.Include(c => c.ChangeRequests)
                .Where(t => t.IsActive == true && t.TenderId == Id).FirstOrDefaultAsync();
            return tender;
        }
        public async Task<Tender> GetPostQualificationByTenderId(int Id)
        {
            var tender = await _context.Tenders
                .Where(t => t.IsActive == true && t.PostQualificationTenderId == Id && t.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed).FirstOrDefaultAsync();
            return tender;
        }
        public async Task<PreQulaificationApprovalModel> GetPreQualificationDetailsForPreQualificationApproval(int Id)
        {
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            var tender = await _context.Tenders.Where(x => x.IsActive == true && x.TenderId == Id)
                .Select(s => new PreQulaificationApprovalModel
                {
                    AgencyCode = s.AgencyCode,
                    BranchId = s.BranchId,
                    PreQualificationId = s.TenderId,
                    PreQualificationIdString = Util.Encrypt(s.TenderId),
                    PreQualificationStatusId = s.TenderStatusId,
                    PreQualificationTypeId = s.QualificationTypeId,
                    QualificationTypeId = s.TenderTypeId,
                    QualificationTenderTypeId = s.TenderTypeId == (int)Enums.TenderType.PostQualification ? s.PostQualificationTender.TenderTypeId : s.TenderTypeId,
                    RejectionReason = s.TenderHistories.OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason,
                    IsLowBudgetTender = s.IsLowBudgetDirectPurchase,
                    DirectPurchaseMemberId = s.DirectPurchaseMemberId,
                    IsUserHasAccessToLowBudgetTender = (s.IsLowBudgetDirectPurchase.HasValue && s.IsLowBudgetDirectPurchase == true && s.DirectPurchaseMemberId == userId)
                }).FirstOrDefaultAsync();
            return tender;
        }

        #region Subscriptions
        private int GetUserIdForApprovedBy(string approvedBy)
        {
            return _context.UserProfiles.Where(x => x.FullName == approvedBy).FirstOrDefault().Id;
        }

        #endregion

        #region Prequalification

        public async Task<QueryResult<PreQualificationCardModel>> FindPreQualificationByCriteriaForUnderOperationsStage(PreQualificationSearchCriteriaModel criteria)
        {
            var arProvider = CultureInfo.CreateSpecificCulture("ar-SA");

            Calendar hijri = new UmAlQuraCalendar();
            if (criteria.TenderAreasIdString != null)
            {
                criteria.TenderAreasIds = new List<int>(Array.ConvertAll(criteria.TenderAreasIdString.Split(','), int.Parse));
            }
            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established
                };
            }
            var result = await _context.Tenders
                .Where(x => x.IsActive == true)
                .WhereIf(criteria.TenderTypeId == 0, x => x.TenderTypeId == (int)Enums.TenderType.PreQualification)
                .WhereIf(criteria.TenderTypeId > 0, x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(criteria.UserId != 0, q => q.TenderHistories.OrderByDescending(x => x.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.Approved && x.UserId == criteria.UserId) != null)
                .Where(x => x.AgencyCode == criteria.AgencyCode)
                .Where(d => d.BranchId == criteria.BranchId)
                .WhereIf(criteria.TenderStatusId != null, x => x.TenderStatusId == criteria.TenderStatusId)
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.ApprovedBy), q => q.TenderHistories.OrderByDescending(x => x.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.Approved && x.UserId == GetUserIdForApprovedBy(criteria.ApprovedBy)) != null)
                .WhereIf(criteria.CreatedBy != null, x => x.CreatedBy.ToLower() == criteria.CreatedBy.ToLower())
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Contains(criteria.TenderName.ToUpper()))
                .WhereIf(!String.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => criteria.TenderStatusIds.Contains(x.TenderStatusId))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(x => new PreQualificationCardModel
                {
                    ActivityDescription = x.ActivityDescription,
                    AgencyCode = x.AgencyCode,
                    AgencyName = x.Agency.NameArabic,
                    InsideKSA = x.InsideKSA,
                    IsFavouriteQualification = x.FavouriteSupplierTenders.Any(f => f.SupplierCRNumber == criteria.cr && f.IsActive == true),
                    LastEnqueriesDate = x.LastEnqueriesDate,
                    LastEnqueriesDateHijri = x.LastEnqueriesDate.HasValue ? x.LastEnqueriesDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    LastOfferPresentationDate = x.LastOfferPresentationDate,
                    LastOfferPresentationDateHijri = x.LastOfferPresentationDate.HasValue ? x.LastOfferPresentationDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    OffersCheckingDate = x.OffersCheckingDate.Value,
                    OffersCheckingDateHijri = x.OffersCheckingDate.HasValue ? x.OffersCheckingDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    QualificationName = x.TenderName,
                    QualificationNumber = x.TenderNumber,
                    SubmitionDate = x.SubmitionDate,
                    SubmitionDateHijri = x.SubmitionDate.HasValue ? x.SubmitionDate.Value.ToHijriDateWithFormat("yyyy-MM-dd") : "",
                    SupplierHasOffer = x.PreQualificationApplyDocuments.Any(r => r.IsActive == true && r.SupplierId == criteria.cr),
                    TenderId = x.TenderId,
                    QualificationStatusName = x.Status.NameAr,
                    QualificationTypeName = x.TenderType.NameAr,
                    TenderIdString = Util.Encrypt(x.TenderId),
                    TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                    TenderStatusId = x.TenderStatusId,
                    TenderChangeRequestIds = x.ChangeRequests.Where(c => c.IsActive == true).Select(y => y.TenderChangeRequestId).ToList(),
                    ChangeRequestStatusIds = x.ChangeRequests.Where(c => c.IsActive == true).Select(y => y.ChangeRequestStatusId).ToList(),
                    QualificationReferenceNumber = x.ReferenceNumber
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }


        public async Task<QueryResult<PreQualificationCardModel>> FindPreQualificationsModelBySearchCriteria(PreQualificationSearchCriteriaModel criteria, params Expression<Func<Tender, object>>[] includes)
        {
            var arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
            var userId = _httpContextAccessor.HttpContext.User.UserId();

            Calendar hijri = new UmAlQuraCalendar();
            if (criteria.TenderStatusIdsString != null)
            {
                criteria.TenderStatusIds = new List<int>(Array.ConvertAll(criteria.TenderStatusIdsString.Split(','), int.Parse));
            }
            if (criteria.TenderStatusId == (int)Enums.TenderStatus.UnderEstablishing)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.UnderEstablishing,
                    (int)Enums.TenderStatus.Established
                };
            }
            if (criteria.NotInStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed)
            {
                criteria.TenderStatusIds = new List<int>
                {
                    (int)Enums.TenderStatus.Approved,
                    (int)Enums.TenderStatus.DocumentChecking,
                    (int)Enums.TenderStatus.Pending,
                    (int)Enums.TenderStatus.DocumentCheckPending
                };
            }
            var result = await _context.Tenders
                .Where(x => x.IsActive == true)
                .WhereIf(criteria.TenderTypeId == 0, x => (x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification))
                .WhereIf(criteria.TenderTypeId > 0, x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.AgencyCode), x => x.AgencyCode == criteria.AgencyCode)
                .WhereIf(criteria.BranchId != 0, d => d.BranchId == criteria.BranchId)
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.ReferenceNumber), q => q.ReferenceNumber == criteria.ReferenceNumber)
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.ApprovedBy), q => q.TenderHistories.OrderByDescending(x => x.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.Approved && x.UserId == GetUserIdForApprovedBy(criteria.ApprovedBy)) != null)
                .WhereIf(criteria.CreatedBy != null, x => x.CreatedBy.ToLower().Contains(criteria.CreatedBy.ToLower()))
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderName), x => x.TenderName.ToUpper().Contains(criteria.TenderName.ToUpper()))
                .WhereIf(criteria.TenderStatusId != 0 && criteria.TenderStatusId != null, x => x.TenderStatusId == criteria.TenderStatusId)
                .WhereIf(!string.IsNullOrWhiteSpace(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf(criteria.TenderStatusIds != null && criteria.TenderStatusIds.Count != 0, x => criteria.TenderStatusIds.Contains(x.TenderStatusId))
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(t => new PreQualificationCardModel
                {
                    ActivityDescription = t.ActivityDescription,
                    AgencyCode = t.AgencyCode,
                    AgencyName = t.Agency.NameArabic,
                    QualificationNumber = t.TenderNumber,
                    QualificationReferenceNumber = t.ReferenceNumber,
                    QualificationName = t.TenderName,
                    //TenderStatusIdString = Util.Encrypt(t.TenderStatusId),
                    //TenderIdString = Util.Encrypt(t.TenderId),
                    TenderId = t.TenderId,
                    TenderStatusId = t.Status.TenderStatusId,
                    QualificationStatusName = t.Status.NameAr,
                    QualificationTypeName = t.TenderType.NameAr,
                    SubmitionDate = t.SubmitionDate,
                    //OffersCheckingDateHijri = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToString("dd-MM-yyyy", arProvider) : "",
                    //SubmitionDateHijri = t.SubmitionDate.HasValue ? t.SubmitionDate.Value.ToString("dd-MM-yyyy", arProvider) : "",
                    TenderChangeRequestIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.TenderChangeRequestId).ToList(),
                    ChangeRequestStatusIds = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatusId).ToList(),
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastEnqueriesTime = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToShortTimeString() : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("hh:mm tt") : "",
                    //LastEnqueriesDateHijri = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToString("dd-MM-yyyy", arProvider) : "",
                    OffersCheckingDate = t.OffersCheckingDate,
                    //LastOfferPresentationDateHijri = t.LastOfferPresentationDate.Value != null ? t.LastOfferPresentationDate.Value.ToString("dd-MM-yyyy", arProvider) : "",
                    TenderTypeId = t.TenderTypeId,
                    PostQualificationTenderTypeId = t.TenderTypeId == (int)Enums.TenderType.PostQualification ? t.PostQualificationTender.TenderTypeId : 0,
                    lstOfChangeRequest = t.ChangeRequests.Where(a => a.IsActive == true).Select(
                         a => new TenderChangeRequestModel
                         {
                             ChangeRequestTypeId = a.ChangeRequestTypeId,
                             ChangeRequestStatusId = a.ChangeRequestStatusId,
                             RequestedByRoleName = a.RequestedByRoleName,
                         }
                     ).ToList(),
                    UserCommitteeType = (t.PreQualificationCommitteeId.HasValue && t.PreQualificationCommitteeId == criteria.CommitteeId ? (t.OffersPreQualificationCommittee.CommitteeUsers.Any(c => c.UserProfileId == criteria.UserId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_PreQualificationCommitteeSecretary) ? (int)Enums.UserCommitteeType.NewMonafasat_PreQualificationCommitteeSecretary : (int)UserCommitteeType.NewMonafasat_PreQualificationCommitteeManager) : 0),
                    UserCheckCommitteeType = (t.OffersCheckingCommitteeId != null && t.OffersCheckingCommitteeId.HasValue && t.OffersCheckingCommitteeId == criteria.OffersCheckingCommitteeId ? (t.OffersCheckingCommittee.CommitteeUsers.Any(c => c.UserProfileId == criteria.UserId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckSecretary) ? (int)UserCommitteeType.NewMonafasat_OffersCheckSecretary : (int)UserCommitteeType.NewMonafasat_OffersCheckManager) : 0),
                    UserDirectPurchaseCommitteeType = (t.DirectPurchaseCommitteeId != null && t.DirectPurchaseCommitteeId.HasValue && t.DirectPurchaseCommitteeId == criteria.DirectPurchaseCommitteeId ? (t.DirectPurchaseCommittee.CommitteeUsers.Any(c => c.UserProfileId == criteria.UserId && c.UserRoleId == (int)Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee) ? (int)UserCommitteeType.NewMonafasat_SecretaryDirtectPurshasingCommittee : (int)UserCommitteeType.NewMonafasat_ManagerDirtectPurshasingCommittee) : 0),
                    TenderChangeRequestIdsForAuditor = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.DataEntry && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForDataEntry = t.ChangeRequests.Where(c => c.IsActive == true && c.RequestedByRoleName == RoleNames.DataEntry && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForManager = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.PreQualificationCommitteeSecretary || c.RequestedByRoleName == RoleNames.OffersCheckSecretary || c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForSecretary = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.PreQualificationCommitteeSecretary) && (c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForDirectSecretary = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.OffersPurchaseSecretary) && (c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForQualificationManager = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.PreQualificationCommitteeSecretary) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForQualificationSecretary = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.PreQualificationCommitteeSecretary) && (c.ChangeRequestStatusId == (int)ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.New || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected)).Select(x => x.TenderChangeRequestId).ToList(),
                    TenderChangeRequestIdsForOfferCheckSecretary = t.ChangeRequests.Where(c => c.IsActive == true && (c.RequestedByRoleName == RoleNames.OffersCheckSecretary) && (c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending || c.ChangeRequestStatusId == (int)ChangeStatusType.Rejected || c.ChangeRequestStatusId == (int)ChangeStatusType.New)).Select(x => x.TenderChangeRequestId).ToList(),
                    ChangeRequestStatusNames = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestStatus.NameAr).ToList(),
                    ChangeRequestTypeId = t.ChangeRequests.Where(c => c.IsActive == true).Select(x => x.ChangeRequestTypeId).FirstOrDefault(),
                    QuantitiesTableStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.QuantitiesTables).OrderByDescending(x => x.CreatedAt).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    ExtendDatesStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.ExtendDates).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    AttachmentsStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Attachments).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    CancelRequestStatus = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.ChangeRequestStatusId).FirstOrDefault(),
                    CancelRequestRoleName = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    HasPendingCancelRequest = t.ChangeRequests.Any(c => c.IsActive == true && c.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling && c.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending),
                    ChangeRequestedBy = t.ChangeRequests.Where(x => x.IsActive == true && x.ChangeRequestTypeId == (int)Enums.ChangeRequestType.Canceling).Select(s => s.RequestedByRoleName).FirstOrDefault(),
                    IsUserHasAccessToLowBudgetTender = (t.IsLowBudgetDirectPurchase.HasValue && t.IsLowBudgetDirectPurchase == true && t.DirectPurchaseMemberId == userId),
                    IsLowBudgetTender = t.IsLowBudgetDirectPurchase.HasValue && t.IsLowBudgetDirectPurchase == true,

                })
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            foreach (var item in result.Items)
            {
                item.OffersCheckingDateHijri = item.OffersCheckingDate.HasValue ? item.OffersCheckingDate.Value.ToHijriDateWithFormat("dd-MM-yyyy") : "";
                item.SubmitionDateHijri = item.SubmitionDate.HasValue ? item.SubmitionDate.Value.ToHijriDateWithFormat("dd-MM-yyyy") : "";
                item.LastOfferPresentationTime = item.LastOfferPresentationDate.HasValue ? item.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "";
                item.LastEnqueriesDateHijri = item.LastEnqueriesDate.HasValue ? item.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd-MM-yyyy") : "";
                item.LastOfferPresentationDateHijri = item.LastOfferPresentationDate.HasValue ? item.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd-MM-yyyy") : "";
                item.TenderStatusIdString = Util.Encrypt(item.TenderStatusId);
                item.TenderIdString = Util.Encrypt(item.TenderId);
            }
            return result;
        }

        public async Task<QueryResult<QualificationSupplierProjectModel>> FindSupplierProjectModelBySearchCriteria(PreQualificationSearchCriteriaModel criteria, params Expression<Func<Tender, object>>[] includes)
        {
            var arProvider = CultureInfo.CreateSpecificCulture("ar-SA");



            Calendar hijri = new UmAlQuraCalendar();

            var result = await _context.QualificationSupplierProject.Where(a => a.IsActive == true && a.SupplierSelectedCr == criteria.SupplierCr && a.TenderId == Util.Decrypt(criteria.TenderIdStr))
                                  .Select(e => new QualificationSupplierProjectModel
                                  {
                                      ContractName = e.ContractName,
                                      OwnerName = e.OwnerName,
                                      ContractValue = e.ContractValue,
                                      Description = e.Description,
                                      EmailAddress = e.EmailAddress,
                                      EndDateStr = (e.EndDate != null ? e.EndDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : string.Empty),
                                      StartDateStr = (e.StartDate != null ? e.StartDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : string.Empty),
                                      PhoneNumber = e.PhoneNumber
                                  })
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }



        #endregion

        #region Post-Qualification



        public async Task<PostQualificationApplyDocumentsModel> getQualificationDataToCreatePostQualification(string tenderid, string qualificationId)
        {


            var tender = await _context.Tenders
                .WhereIf(!string.IsNullOrEmpty(tenderid), t => t.TenderId == Util.Decrypt(tenderid) && t.IsActive == true)
                .WhereIf(!string.IsNullOrEmpty(qualificationId), t => t.TenderId == Util.Decrypt(qualificationId) && t.IsActive == true)
                .Select(t => new PostQualificationApplyDocumentsModel
                {
                    TenderStatusId = t.TenderStatusId,
                    TenderAreaIDs = t.TenderAreas.Where(m => m.IsActive == true).Select(a => a.AreaId).ToList(),
                    TenderCountriesIDs = t.TenderCountries.Where(m => m.IsActive == true).Select(a => a.CountryId).ToList(),
                    TenderActivitieIDs = t.TenderActivities.Where(m => m.IsActive == true).Select(a => a.ActivityId).ToList(),
                    TenderConstructionWorkIDs = t.TenderConstructionWorks.Where(m => m.IsActive == true).Select(a => a.ConstructionWorkId).ToList(),
                    TenderMentainanceRunnigWorkIDs = t.TenderMaintenanceRunnigWorks.Where(m => m.IsActive == true).Select(a => a.MaintenanceRunningWorkId).ToList(),
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastEnqueriesDateString = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    LastOfferPresentationDateString = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    OffersCheckingDateString = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("hh:mm tt") : "",
                    AgencyCode = t.AgencyCode,
                    PreQualificationCommitteeId = t.PreQualificationCommitteeId,
                    TechnicalOrganizationId = t.TechnicalOrganizationId.Value,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    TenderName = t.TenderName,
                    CreatedBy = t.CreatedBy,
                    ActivityDescription = t.ActivityDescription,
                    Details = t.Details,
                    ReferenceNumber = t.ReferenceNumber,
                    PostQualificationTenderId = t.PostQualificationTenderId,
                    CommercialNumbers = t.PostQualificationInvitations.Select(x => x.CommercialNumber).ToList(),
                    QualificationTypeId = t.QualificationTypeId.HasValue ? t.QualificationTypeId.Value : 0,
                    TenderPointsToPass = t.TenderPointsToPass,
                    TechnicalAdministrativeCapacity = t.TechnicalAdministrativeCapacity,
                    FinancialCapacity = t.FinancialCapacity,
                    Attachments = qualificationId != null ? t.Attachments.Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList() : new List<TenderAttachmentModel>()
                }).FirstOrDefaultAsync();
            return tender;
        }

        public async Task<PostQualificationApplyDocumentsModel> GetPostQualificationData(int Id)
        {


            Check.ArgumentNotNullOrEmpty(nameof(Id), Id.ToString());
            var tender = await _context.Tenders
                .Where(t => t.TenderId == Id && t.IsActive == true)
                .Select(t => new PostQualificationApplyDocumentsModel
                {
                    TenderStatusId = t.TenderStatusId,
                    TenderAreaIDs = t.TenderAreas.Where(m => m.IsActive == true).Select(a => a.AreaId).ToList(),
                    TenderCountriesIDs = t.TenderCountries.Where(m => m.IsActive == true).Select(a => a.CountryId).ToList(),
                    TenderActivitieIDs = t.TenderActivities.Where(m => m.IsActive == true).Select(a => a.ActivityId).ToList(),
                    TenderConstructionWorkIDs = t.TenderConstructionWorks.Where(m => m.IsActive == true).Select(a => a.ConstructionWorkId).ToList(),
                    TenderMentainanceRunnigWorkIDs = t.TenderMaintenanceRunnigWorks.Where(m => m.IsActive == true).Select(a => a.MaintenanceRunningWorkId).ToList(),
                    LastEnqueriesDate = t.LastEnqueriesDate,
                    LastOfferPresentationDate = t.LastOfferPresentationDate,
                    OffersCheckingDate = t.OffersCheckingDate,
                    LastEnqueriesDateString = t.LastEnqueriesDate.HasValue ? t.LastEnqueriesDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    LastOfferPresentationDateString = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    OffersCheckingDateString = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    LastOfferPresentationTime = t.LastOfferPresentationDate.HasValue ? t.LastOfferPresentationDate.Value.ToGregorianDate("hh:mm tt") : "",
                    OffersCheckingTime = t.OffersCheckingDate.HasValue ? t.OffersCheckingDate.Value.ToGregorianDate("hh:mm tt") : "",
                    Attachments = (t.PreQualificationId != null && t.PreQualificationId != 0 ? t.PreQualification.Attachments.Where(m => m.IsActive == true).Select(a => new TenderAttachmentModel { FileNetReferenceId = a.FileNetReferenceId, Name = a.Name }).ToList() : null),
                    AgencyCode = t.AgencyCode,
                    PreQualificationCommitteeId = t.PreQualificationCommitteeId,
                    TechnicalOrganizationId = t.TechnicalOrganizationId.Value,
                    OffersCheckingCommitteeId = t.OffersCheckingCommitteeId,
                    TenderName = t.TenderName,
                    CreatedBy = t.CreatedBy,
                    ActivityDescription = t.ActivityDescription,
                    Details = t.Details,
                    ReferenceNumber = t.ReferenceNumber,
                    PostQualificationTenderId = t.PostQualificationTenderId,
                    CommercialNumbers = t.PostQualificationInvitations.Select(x => x.CommercialNumber).ToList(),
                    QualificationTypeId = t.QualificationTypeId.HasValue ? t.QualificationTypeId.Value : 0,
                    TenderPointsToPass = t.TenderPointsToPass,
                    TechnicalAdministrativeCapacity = t.TechnicalAdministrativeCapacity,
                    FinancialCapacity = t.FinancialCapacity
                }).FirstOrDefaultAsync();
            return tender;
        }
        public async Task<Tender> GetPostQualificationById(int Id)
        {
            var tender = await _context.Tenders.Include(x => x.TenderHistories).Include(s => s.Agency)
                .Where(t => t.IsActive == true && t.TenderId == Id).FirstOrDefaultAsync();
            return tender;
        }
        public List<string> GetPostQualificationSuppliers(int Id)
        {
            var result = _context.PostQualificationSuppliersInvitations.Where(x => x.IsActive == true && x.PostQualificationId == Id).Select(c => c.CommercialNumber).ToList();
            return result;
        }

        public List<QualificationFinalResult> GetSupplierResultForQualification(int Id)
        {
            var result = _context.QualificationFinalResult.Include(s => s.Supplier).ThenInclude(c => c.offers)
                .Where(x => x.IsActive == true && x.TenderId == Id)
                .Where(t => t.Tender.PreQualificationApplyDocuments.Any(p => p.IsActive == true && p.SupplierId == t.SupplierSelectedCr && p.StatusId == (int)Enums.QualificationStatus.Received))
                .ToList();
            return result;
        }



        public async Task<PreQualificationBasicDetailsModel> GetQualificationForCheckPostQualificationByQualificationId(int tenderId, int userId, string agencyCide, List<string> roles)
        {
            var tenderEntity = await _context.Tenders.Include(w => w.OffersCheckingCommittee).ThenInclude(x => x.CommitteeUsers)
                .Where(t => t.TenderId == tenderId && t.IsActive == true && t.AgencyCode == agencyCide)

                .WhereIf(roles.Any(s => RoleNames.OffersCheckManager.ToString().Contains(s) || RoleNames.OffersCheckSecretary.ToString().Contains(s)), t => t.OffersCheckingCommittee.CommitteeUsers.Any(s => s.UserProfileId == userId
              && s.IsActive == true && (s.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckSecretary || s.UserRoleId == (int)Enums.UserRole.NewMonafasat_OffersCheckManager)))


                .WhereIf(roles.Any(s => RoleNames.OffersPurchaseSecretary.ToString().Contains(s) || RoleNames.OffersPurchaseManager.ToString().Contains(s)), t => t.DirectPurchaseCommittee.CommitteeUsers.Any(s => s.UserProfileId == userId
              && s.IsActive == true && (s.UserRoleId == (int)Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee || s.UserRoleId == (int)Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee)))

                .Select(w => new PreQualificationBasicDetailsModel
                {
                    QualificationName = w.TenderName,
                    TenderActivities = w.TenderActivities.Select(s => s.Activity.NameAr).ToList(),
                    TenderConstructionWorks = w.TenderConstructionWorks.Select(s => s.ConstructionWork.NameAr).ToList(),
                    TenderMaintenanceRunnigWorks = w.TenderMaintenanceRunnigWorks.Select(s => s.MaintenanceRunningWork.NameAr).ToList(),
                    TenderIdString = Util.Encrypt(w.TenderId),
                    TenderStatusId = w.TenderStatusId,
                    RejectionReason = w.TenderHistories.Where(x => x.IsActive == true && x.StatusId == (int)Enums.TenderStatus.DocumentCheckRejected).OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason,
                    unCheckedSupplierDocuments = w.PreQualificationApplyDocuments.Where(a => a.PreQualificationResult == (int)Enums.OfferStatus.Received).Count(),
                    Createby = w.CreatedBy
                })
            .FirstOrDefaultAsync();
            return tenderEntity;
        }

        public async Task<PreQulaificationApprovalModel> GetPostQualificationByQualificationId(int postQualificationId)
        {
            var result = await _context.Tenders.Where(x => x.IsActive == true && x.TenderId == postQualificationId)
                .Select(p => new PreQulaificationApprovalModel
                {
                    PreQualificationId = p.TenderId,
                    BranchId = p.BranchId,
                    PreQualificationTypeId = p.QualificationTypeId,
                    CommitteeId = p.OffersCheckingCommitteeId,
                    PreQualificationIdString = Util.Encrypt(p.TenderId),
                    PreQualificationStatusId = p.TenderStatusId,
                    QualificationTenderTypeId = p.PostQualificationTender.TenderTypeId,
                    RejectionReason = p.TenderHistories.Where(x => x.IsActive == true && (x.StatusId == (int)Enums.TenderStatus.Rejected || x.StatusId == (int)Enums.TenderStatus.RejectedQualificationApprovalByCommitteeManager)).OrderByDescending(th => th.TenderHistoryId).FirstOrDefault().RejectionReason,
                }).FirstOrDefaultAsync();
            return result;
        }
        #endregion Post-Qualification

        #region Qualification


        public async Task<List<QualificationTypeCategory>> FindQualificationItems(int PreQualificationTypeID)
        {
            var result = await _context.QualificationTypeCategory.Where(a => a.QualificationTypeId == PreQualificationTypeID && a.QualificationSubCategory.IsConfigure)
                .Include("QualificationSubCategory.QualificationItems").ToListAsync();
            return result;
        }



        public async Task<List<QualificationSubCategoryConfiguration>> FindSubCategoryConfiguration(int TenderId)
        {
            var result = await _context.QualificationSubCategoryConfiguration.Where(a => a.TenderId == TenderId && a.IsActive == true).ToListAsync();
            return result;
        }

        public async Task<Tender> GetTenderWithQualificationConfigurations(int TenderId)
        {

            var tender = await _context.Tenders.Where(a => a.TenderId == TenderId && a.IsActive == true)
                .Include(q => q.QualificationConfigurations)
                .Include(t => t.QualificationSubCategoryConfigurations)
                .FirstOrDefaultAsync();
            var qualificationFinalResults = await _context.QualificationFinalResult.Include(s => s.QualificationLookup).Where(a => a.TenderId == TenderId && a.IsActive == true).ToListAsync();
            var qualificationCategoryResults = await _context.QualificationCategoryResult.Where(a => a.TenderId == TenderId && a.IsActive == true).ToListAsync();
            var qualificationSubCategoryResults = await _context.QualificationSubCategoryResult.Where(a => a.TenderId == TenderId && a.IsActive == true).ToListAsync();
            tender.QualificationFinalResults.AddRange(qualificationFinalResults);
            tender.QualificationCategoryResults.AddRange(qualificationCategoryResults);
            tender.QualificationSubCategoryResults.AddRange(qualificationSubCategoryResults);
            return tender;
        }


        public async Task<QualificationFinalResult> FindFinalResult(string SupplierId, int QualificationId)
        {
            return await _context.QualificationFinalResult.Where(a => a.TenderId == QualificationId && a.SupplierSelectedCr == SupplierId && a.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<List<QualificationSupplierDataYearly>> GetQualificationSupplierDataYear(int qualificationId, string supplierId)
        {
            return await _context.QualificationSupplierDataYearly.Where(a => a.TenderId == qualificationId && a.SupplierSelectedCr == supplierId && a.IsActive == true).ToListAsync(); ;
        }

        public async Task<List<QualificationSupplierData>> GetQualificationSupplierData(int qualificationId, string supplierId)
        {
            return await _context.QualificationSupplierData.Where(a => a.TenderId == qualificationId && a.SupplierSelectedCr == supplierId && a.IsActive == true).Include(a => a.QualificationConfigurationAttachments).ToListAsync();
        }

        public async Task<List<QualificationSupplierProject>> GetQualificationSupplierProject(int qualificationId, string supplierId)
        {
            return await _context.QualificationSupplierProject.Where(a => a.TenderId == qualificationId && a.SupplierSelectedCr == supplierId && a.IsActive == true).ToListAsync();
        }

        #endregion

    }
}
