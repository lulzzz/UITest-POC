using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class ReportsService : IReportsService
    {
        private IAppDbContext _context;
        private ISupplierService _supplierService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportsService(IAppDbContext context, IHttpContextAccessor httpContextAccessor, ISupplierService supplierServic)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _supplierService = supplierServic;
        }

        bool isInRole(string roleName)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(roleName);
        }
        public async Task<List<string>> FinancialYear()
        {
            var result = await _context.TenderHistories
                .Where(t => t.StatusId == (int)Enums.TenderStatus.Approved).Select(t => t.CreatedAt.Year.ToString())
                .Distinct()
                .ToListAsync();
            return result;
        }

        public async Task<Dictionary<int, string>> GetTendersName()
        {
            var result = await _context.Tenders
                 .Where(x => x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
                .ToDictionaryAsync(x => x.TenderId, z => z.TenderName);

            return result;
        }
        public async Task<Dictionary<int, string>> GetTenderStatuses()
        {
            var isAr = CultureInfo.CurrentCulture.Name == "ar-SA";

            var result = await _context.TenderStatus
               .Select(x => new { id = x.TenderStatusId, Name = isAr ? x.NameAr : x.NameEn })
                .ToDictionaryAsync(x => x.id, z => z.Name);

            return result;
        }
        public async Task<List<TenderValueToTypesModel>> GetTendersForTenderToTypesReport(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenderResult = await _context.Tenders.Include(x => x.Agency)
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null), x => x.AgencyCode == searchCriteria.AgencyCode)
                .WhereIf((searchCriteria.BranchId != 0), x => x.BranchId == searchCriteria.BranchId)
                .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                    .WhereIf(!string.IsNullOrEmpty(searchCriteria.ActivityId.ToString()), x => x.TenderActivities.Select(c => c.ActivityId).FirstOrDefault() == searchCriteria.ActivityId)
                    .WhereIf(searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null, x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1))
                   .GroupBy(
                o => o.Agency.NameArabic).Select(x => new TenderValueToTypesModel
                {
                    AgencyName = x.Key,
                    PublicTendersValue = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.NewTender).Select(c => c.ConditionsBookletPrice).Sum(),
                    DirectPurchaseValue = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase).Select(c => c.ConditionsBookletPrice).Sum(),
                    LimitedTender = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.LimitedTender).Select(c => c.ConditionsBookletPrice).Sum(),
                    ReverseBidding = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.ReverseBidding).Select(c => c.ConditionsBookletPrice).Sum(),
                    FrameworkAgreement = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement).Select(c => c.ConditionsBookletPrice).Sum(),

                }).WhereIf(!string.IsNullOrEmpty(searchCriteria.ConditionsBookletPrice.ToString()) && searchCriteria.ConditionsBookletPrice == (int)Enums.ConitionalBookletPriceList.LessThanMilion, (x => x.PublicTendersValue <= searchCriteria.ConditionsBookletPrice || x.DirectPurchaseValue <= searchCriteria.ConditionsBookletPrice))
                    .WhereIf(!string.IsNullOrEmpty(searchCriteria.ConditionsBookletPrice.ToString()) && searchCriteria.ConditionsBookletPrice != (int)Enums.ConitionalBookletPriceList.LessThanMilion, (x => x.PublicTendersValue >= searchCriteria.ConditionsBookletPrice || x.DirectPurchaseValue >= searchCriteria.ConditionsBookletPrice)).ToListAsync();
            return tenderResult;
        }


        public async Task<QueryResult<MessagesModel>> GetMessagesStatusReportAsync(SearchCriteria searchCriteria)
        {
            var result = await _context.Notifications
                  .Where(x => x is NotificationSMS || x is NotificationEmail)
                  .Select(x => new MessagesModel()
                  {
                      MessageType = x is NotificationSMS ? Resources.ReportResources.DisplayInputs.SMS : Resources.ReportResources.DisplayInputs.Email,
                      EndDate = x.sendAt.HasValue ? x.sendAt.ToString() : "",
                      MessageStatue = x.NotifacationStatusEntity.Name,
                      StartDate = x.CreatedAt.ToString(),
                      MessageName = x is NotificationEmail ? ((NotificationEmail)x).Title : ""

                  }).ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);
            return result;
        }

        public async Task<QueryResult<QualificationModel>> GetQualificationReport(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var isAr = CultureInfo.CurrentCulture.Name == "ar-SA";

            var tenderResult = await _context.Tenders
                .Where(x => x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification)
                .Include(x => x.Agency)
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null), x => x.AgencyCode == searchCriteria.AgencyCode)
                .WhereIf((searchCriteria.BranchId != 0), x => x.BranchId == searchCriteria.BranchId)
                .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                    .WhereIf(!string.IsNullOrEmpty(searchCriteria.ActivityId.ToString()), x => x.TenderActivities.Select(c => c.ActivityId).FirstOrDefault() == searchCriteria.ActivityId)
                    .WhereIf(searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null, x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1))
                 .Select(x => new QualificationModel
                 {
                     QualificationName = x.TenderName,
                     QualificationPublishDate = x.SubmitionDate,
                     QualificationStatuse = isAr ? x.Status.NameAr : x.Status.NameEn,
                     QualificationType = x.TenderTypeId ==
                     (int)Enums.TenderType.PreQualification ? Resources.TenderResources.DisplayInputs.PreQualification : Resources.TenderResources.DisplayInputs.PostQualification

                 })
                .ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);
            return tenderResult;
        }

        public async Task<QueryResult<AmountOfSavingsViewModel>> GetAmountOfSavingsAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var isAr = CultureInfo.CurrentCulture.Name == "ar-SA";

            var result = await _context.Tenders
                .Include(x => x.Agency)
                .Include(x => x.Offers)

                .Where(x => x.TenderTypeId != (int)Enums.TenderType.FirstStageTender && x.EstimatedValue.HasValue && x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
                .WhereIf(searchCriteria.TenderId > 0, x => x.TenderId == searchCriteria.TenderId)
                .WhereIf(searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null, x => x.AgencyCode == searchCriteria.AgencyCode)
                .WhereIf(searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null, x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1))
                 .Select(x => new AmountOfSavingsViewModel
                 {
                     TenderName = x.TenderName,
                     AgencyName = isAr ? x.Agency.NameArabic : x.Agency.NameEnglish,
                     EstimatedValueBeforeReview = (decimal)x.EstimatedValue,
                     EstimatedValueAfterReview = (decimal)x.EstimatedValue,
                     TheValueOfTheAward = ((decimal)(x.Offers.Where(o => o.TotalOfferAwardingValue.HasValue || o.PartialOfferAwardingValue.HasValue)
                     .Select(v => v.TotalOfferAwardingValue ?? 0 + v.PartialOfferAwardingValue ?? 0).Sum())),

                     AmountOfSavings = (decimal)x.EstimatedValue - ((x.Offers.Where(o => o.TotalOfferAwardingValue.HasValue || o.PartialOfferAwardingValue.HasValue)
                     .Select(v => v.TotalOfferAwardingValue ?? 0 + v.PartialOfferAwardingValue ?? 0).Sum())),


                 })

                .ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);

            return result;

        }

        public async Task<TotalAmountOfSavingsViewModel> GetTotalAmountOfSavingsAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {



            var result = await _context.Tenders
          .Include(x => x.Agency)
          .Include(x => x.Offers)

          .Where(x => x.TenderTypeId != (int)Enums.TenderType.FirstStageTender && x.EstimatedValue.HasValue && x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
          .WhereIf(searchCriteria.TenderId > 0, x => x.TenderId == searchCriteria.TenderId)
          .WhereIf(searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null, x => x.AgencyCode == searchCriteria.AgencyCode)
          .WhereIf(searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null, x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1))
           .Select(x => new AmountOfSavingsViewModel
           {


               EstimatedValueBeforeReview = (decimal)x.EstimatedValue,
               EstimatedValueAfterReview = (decimal)x.EstimatedValue,
               TheValueOfTheAward = ((decimal)(x.Offers.Where(o => o.TotalOfferAwardingValue.HasValue || o.PartialOfferAwardingValue.HasValue)
               .Select(v => v.TotalOfferAwardingValue ?? 0 + v.PartialOfferAwardingValue ?? 0).Sum())),

               AmountOfSavings = (decimal)x.EstimatedValue - ((x.Offers.Where(o => o.TotalOfferAwardingValue.HasValue || o.PartialOfferAwardingValue.HasValue)
               .Select(v => v.TotalOfferAwardingValue ?? 0 + v.PartialOfferAwardingValue ?? 0).Sum())),


           })
            .GroupBy(x => true)
              .Select(x => new TotalAmountOfSavingsViewModel { TotalAmountOfSavings = x.Sum(s => s.AmountOfSavings), AmountOfSavingsPercentage = 100 })
              .FirstOrDefaultAsync();
            return result;
        }


        public async Task<QueryResult<DirectPurchaseModel>> GetDirectPurchaseReport(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var isAr = CultureInfo.CurrentCulture.Name == "ar-SA";

            var tenderResult = await _context.Tenders
                .Where(x => x.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                .Include(x => x.Agency)
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null), x => x.AgencyCode == searchCriteria.AgencyCode)
                .WhereIf((searchCriteria.BranchId != 0), x => x.BranchId == searchCriteria.BranchId)
                .WhereIf(!(string.IsNullOrEmpty(searchCriteria.FinancailYear)), x => x.SubmitionDate.Value.ToHijriDateWithFormat("yyyy") == searchCriteria.FinancailYear)
                .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                    .WhereIf(!string.IsNullOrEmpty(searchCriteria.ActivityId.ToString()), x => x.TenderActivities.Select(c => c.ActivityId).FirstOrDefault() == searchCriteria.ActivityId)
                    .WhereIf(searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null, x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1))
                 .Select(x => new DirectPurchaseModel
                 {
                     TenderName = x.TenderName,
                     MainActivity = isAr ? x.TenderActivities.Select(a => a.Activity.NameAr).ToList() : x.TenderActivities.Select(a => a.Activity.NameEn).ToList(),
                     ReferenceTenderNumber = x.ReferenceNumber,
                     TenderPurpose = x.Purpose,
                     TenderPurshasingDate = x.SubmitionDate.ToString(),
                     TheWinnerOfthePromotion = x.Offers.Where(o => o.TotalOfferAwardingValue != null || o.PartialOfferAwardingValue != null).FirstOrDefault().Supplier.SelectedCrName

                 })
                .ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);
            return tenderResult;
        }

        public async Task<List<TenderValueToTypesModel>> GetTendersCountForTenderToTypesReport(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenderResult = await _context.Tenders.Include(x => x.Agency)
            .WhereIf((searchCriteria.BranchId != 0), x => x.BranchId == searchCriteria.BranchId)
            .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null), x => x.AgencyCode == searchCriteria.AgencyCode)
            .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
            .WhereIf(
                !string.IsNullOrEmpty(searchCriteria.ActivityId.ToString()),
                x => x.TenderActivities.Select(c => c.ActivityId).FirstOrDefault() == searchCriteria.ActivityId
            )
            .WhereIf(
                searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null,
                x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1)
            )
            .GroupBy(o => o.Agency.NameArabic)
            .Select(x => new TenderValueToTypesModel
            {
                AgencyName = x.Key,
                PublicTendersCount = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.CurrentTender).Count(),
                DirectPurchaseTendersCount = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase).Count(),
                PublicTendersValue = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.CurrentTender).Count(),
                DirectPurchaseValue = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase).Count(),
                LimitedTender = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.LimitedTender).Count(),
                ReverseBidding = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.ReverseBidding).Count(),
                FrameworkAgreement = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement).Count(),
            })
            .WhereIf(
                !string.IsNullOrEmpty(searchCriteria.ConditionsBookletPrice.ToString()) && searchCriteria.ConditionsBookletPrice == (int)Enums.ConitionalBookletPriceList.LessThanMilion,
                (
                    x => x.PublicTendersValue <= searchCriteria.ConditionsBookletPrice || x.DirectPurchaseValue <= searchCriteria.ConditionsBookletPrice
                )
            )
            .WhereIf(
                !string.IsNullOrEmpty(searchCriteria.ConditionsBookletPrice.ToString()) && searchCriteria.ConditionsBookletPrice != (int)Enums.ConitionalBookletPriceList.LessThanMilion,
                (
                    x => x.PublicTendersValue >= searchCriteria.ConditionsBookletPrice || x.DirectPurchaseValue >= searchCriteria.ConditionsBookletPrice
                )
            )
            .ToListAsync();
            return tenderResult;
        }

        public async Task<List<TenderValueToTypesModel>> GetTenderForTenderToAwardedSuppliersReport(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var tenders = _context.Tenders
                 .Where(x => x.BranchId == searchCriteria.BranchId)
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null), x => x.AgencyCode == searchCriteria.AgencyCode)
                .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
               .WhereIf(!string.IsNullOrEmpty(searchCriteria.ActivityId.ToString()), x => x.TenderActivities.Select(c => c.ActivityId).FirstOrDefault() == searchCriteria.ActivityId)
               .WhereIf(searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null, x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1))

           .GroupBy(
        o => o.Branch.BranchName).Select(x => new TenderValueToTypesModel
        {
            AgencyName = x.Key,
            TendersCount = x.Where(i => i.IsActive == true).Count(),
            SuppliersCount = x.Select(u => u.Offers.Where(o => o.PartialOfferAwardingValue != null || o.TotalOfferAwardingValue != null).Count()).Sum(),
            PublicTendersValue = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.CurrentTender).Select(c => c.ConditionsBookletPrice).Sum(),
            DirectPurchaseValue = x.Where(i => i.IsActive == true && i.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase).Select(c => c.ConditionsBookletPrice).Sum(),
        }).AsQueryable();
            var result = tenders.WhereIf(!string.IsNullOrEmpty(searchCriteria.ConditionsBookletPrice.ToString()) && searchCriteria.ConditionsBookletPrice == (int)Enums.ConitionalBookletPriceList.LessThanMilion, (x => x.PublicTendersValue <= searchCriteria.ConditionsBookletPrice || x.DirectPurchaseValue <= searchCriteria.ConditionsBookletPrice))
                  .WhereIf(!string.IsNullOrEmpty(searchCriteria.ConditionsBookletPrice.ToString()) && searchCriteria.ConditionsBookletPrice != (int)Enums.ConitionalBookletPriceList.LessThanMilion, (x => x.PublicTendersValue >= searchCriteria.ConditionsBookletPrice || x.DirectPurchaseValue >= searchCriteria.ConditionsBookletPrice)).ToList();
            return result;
        }

        public async Task<List<TenderValueToTypesModel>> ReportTendersToActivity(TenderValueToTypeSearchCriteria searchCriteria)
        {
            searchCriteria.AgencyCode = searchCriteria.AgencyCode ?? "";
            var query = _context.Tenders
                .Include(x => x.Agency)
                .Include(x => x.Branch)
            .Where(s => s.IsActive == true)
            .WhereIf(!isInRole(RoleNames.MonafasatAccountManager), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
            .WhereIf((searchCriteria.ActivityId > 0 && searchCriteria.ActivityId != null), x => x.TenderActivities.Any(a => a.ActivityId == searchCriteria.ActivityId))
            .WhereIf(!(searchCriteria.FromCreatedDate == null || searchCriteria.FromCreatedDate == DateTime.MinValue), x => x.SubmitionDate >= searchCriteria.FromCreatedDate)
            .WhereIf(!(searchCriteria.ToCreatedDate == null || searchCriteria.ToCreatedDate == DateTime.MinValue), x => x.SubmitionDate < (searchCriteria.ToCreatedDate.Value.AddDays(1)))
            .GroupBy(b => new { b.BranchId, b.Branch.BranchName, AgencyName = b.Agency.NameArabic }, (k, t) =>
                  new TenderValueToTypesModel
                  {
                      AgencyName = k.AgencyName,
                      BrancheName = k.BranchName,
                      TendersCount = t.Count()

                  })
            .ToList();












            return query;
        }


        private IQueryable<Tender> GetTenderQuery(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var query = _context.Tenders.Include(x => x.Offers).Include(x => x.TenderActivities).Include(x => x.Agency).Include(x => x.Branch)
                .Where(s => s.IsActive == true)
                    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null), x => x.AgencyCode == searchCriteria.AgencyCode)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                        .WhereIf(searchCriteria.ActivityId.HasValue && searchCriteria.ActivityId != 0, x => x.TenderActivities.Any(a => a.ActivityId == searchCriteria.ActivityId))
                    .WhereIf(!string.IsNullOrEmpty(searchCriteria.ConditionsBookletPrice.ToString()) && searchCriteria.ConditionsBookletPrice == (int)Enums.ConitionalBookletPriceList.LessThanMilion, x => x.ConditionsBookletPrice <= searchCriteria.ConditionsBookletPrice)
                    .WhereIf(!string.IsNullOrEmpty(searchCriteria.ConditionsBookletPrice.ToString()) && searchCriteria.ConditionsBookletPrice != (int)Enums.ConitionalBookletPriceList.LessThanMilion, x => x.ConditionsBookletPrice >= searchCriteria.ConditionsBookletPrice)
                   .WhereIf(!(searchCriteria.FromCreatedDate == null || searchCriteria.FromCreatedDate == DateTime.MinValue), x => x.SubmitionDate >= searchCriteria.FromCreatedDate)
                            .WhereIf(!(searchCriteria.ToCreatedDate == null || searchCriteria.ToCreatedDate == DateTime.MinValue), x => x.SubmitionDate < (searchCriteria.ToCreatedDate.Value.AddDays(1)))

                            ;
            return query.AsQueryable();
        }

        public async Task<List<MostTendersActivitiesModel>> ReportGetMostTendersActivitiesAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var result = _context.TenderActivities
                .Include(a => a.Activity)
                .Include(a => a.Tender)

                .WhereIf((criteria.BranchId != 0 && criteria.BranchId != null), x => x.Tender.BranchId == criteria.BranchId)
                    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.Tender.AgencyCode == criteria.AgencyCode)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.Tender.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                    .WhereIf(criteria.FromCreatedDate != null && criteria.ToCreatedDate != null,
                    a => a.Tender.CreatedAt >= criteria.FromCreatedDate && a.Tender.CreatedAt <= criteria.ToCreatedDate)

            .ToList();

            var r = result.GroupBy(
               a => a.Activity,
               a => a.Tender,

               (activity, tender) => new MostTendersActivitiesModel
               {
                   Name = activity.NameAr,
                   NumberOfTenders = tender.Count(),
                   NumberOfTendersPercentage = 100.0 * tender.Count() / result.Count(),
                   PublicTendersValue = tender.Where(i => i.IsActive == true).Select(c => c.ConditionsBookletPrice).Sum(),
               }).AsQueryable();
            r = r.WhereIf(!string.IsNullOrEmpty(criteria.ConditionsBookletPrice.ToString()) && criteria.ConditionsBookletPrice == (int)Enums.ConitionalBookletPriceList.LessThanMilion, (x => x.PublicTendersValue <= criteria.ConditionsBookletPrice))
             .WhereIf(!string.IsNullOrEmpty(criteria.ConditionsBookletPrice.ToString()) && criteria.ConditionsBookletPrice != (int)Enums.ConitionalBookletPriceList.LessThanMilion, (x => x.PublicTendersValue >= criteria.ConditionsBookletPrice)).AsQueryable();
            return r.ToList();
        }

        public async Task<List<QualificationCountModel>> QualificationCountAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var isAr = CultureInfo.CurrentCulture.Name == "ar-SA";

            var tenderResult = await _context.Tenders
                .Where(x => x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification)
                .Include(x => x.Agency)
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null), x => x.AgencyCode == searchCriteria.AgencyCode)
                .WhereIf((searchCriteria.BranchId != 0), x => x.BranchId == searchCriteria.BranchId)
                    .WhereIf(!string.IsNullOrEmpty(searchCriteria.ActivityId.ToString()), x => x.TenderActivities.Select(c => c.ActivityId).FirstOrDefault() == searchCriteria.ActivityId)
                    .WhereIf(searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null, x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1))
                 .GroupBy(q => new
                 {
                     q.TenderStatusId,
                     StatusNameEn = q.Status.NameEn,
                     StatusNameAr = q.Status.NameAr,
                     TenderTypeAr = q.TenderType.NameAr,
                     TenderTypeEn = q.TenderType.NameEn
                 }, (k, t) =>

new QualificationCountModel
{
    Percentage = 0,
    QualificationCount = t.Count(),
    QualificationStatues = isAr ? k.StatusNameAr : k.StatusNameEn,
    QualificationType = isAr ? k.TenderTypeAr : k.TenderTypeEn
})
                 .ToListAsync();

            var total = tenderResult.Sum(x => x.QualificationCount);
            tenderResult.ForEach(x =>
            {

                x.Percentage = ((decimal)((float)((x.QualificationCount / total) * 100)));

            });
            return tenderResult;
        }

        public async Task<QueryResult<QualificationCountModel>> QualificationCountListAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var isAr = CultureInfo.CurrentCulture.Name == "ar-SA";

            var tenderResult = await _context.Tenders
                .Where(x => x.TenderTypeId == (int)Enums.TenderType.PreQualification || x.TenderTypeId == (int)Enums.TenderType.PostQualification)
                .Include(x => x.Agency)
                .WhereIf((searchCriteria.AgencyCode != "" && searchCriteria.AgencyCode != null), x => x.AgencyCode == searchCriteria.AgencyCode)
                .WhereIf((searchCriteria.BranchId != 0), x => x.BranchId == searchCriteria.BranchId)
                    .WhereIf(!string.IsNullOrEmpty(searchCriteria.ActivityId.ToString()), x => x.TenderActivities.Select(c => c.ActivityId).FirstOrDefault() == searchCriteria.ActivityId)
                    .WhereIf(searchCriteria.FromCreatedDate != null && searchCriteria.ToCreatedDate != null, x => x.SubmitionDate >= searchCriteria.FromCreatedDate && x.SubmitionDate <= searchCriteria.ToCreatedDate.Value.AddDays(1))
                 .GroupBy(q => new
                 {
                     tenderStatusId = q.TenderStatusId,
                     StatusNameEn = q.Status.NameEn,
                     StatusNameAr = q.Status.NameAr,
                     TenderTypeAr = q.TenderType.NameAr,
                     TenderTypeEn = q.TenderType.NameEn
                 }, (k, t) =>
new QualificationCountModel
{
    Percentage = 0,
    QualificationCount = t.Count(),
    QualificationStatues = isAr ? k.StatusNameAr : k.StatusNameEn,
    QualificationType = isAr ? k.TenderTypeAr : k.TenderTypeEn
})
                     .ToListAsync();

            var total = tenderResult.Sum(x => x.QualificationCount);
            tenderResult.ForEach(x =>
            {

                x.Percentage = ((decimal)((float)((x.QualificationCount / total) * 100)));

            });
            var finalResult = new QueryResult<QualificationCountModel>(tenderResult, 1, 1, 1);
            return finalResult;
        }
         
        public async Task<List<MostTendersActivitiesModel>> ReportGetMostSuppliersActivitiesAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var result = _context.TenderActivities
                .Include(a => a.Activity)
                .Include(a => a.Tender)
                .ThenInclude(a => a.Offers)
                .Where(a => a.Tender.IsActive == true && a.Tender.Offers.Count > 0)
                    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.Tender.AgencyCode == criteria.AgencyCode)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.Tender.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                    .WhereIf(criteria.FromCreatedDate != null && criteria.ToCreatedDate != null,
                    a => a.Tender.CreatedAt >= criteria.FromCreatedDate && a.Tender.CreatedAt <= criteria.ToCreatedDate)

            .ToList();


            var r = result.GroupBy(
               a => a.Activity,
               a => a.Tender,

               (activity, tenders) => new MostTendersActivitiesModel
               {
                   Name = activity.NameAr,
                   NumberOfTenders = tenders.Select(o => o.Offers.Count()).Sum(),
                   NumberOfTendersPercentage = 100.0 * tenders.Select(c => c.Offers).Count() / result.Count(),
                   PublicTendersValue = tenders.Select(o => o.Offers.Select(t => t.Tender.ConditionsBookletPrice).Sum()).Sum(),
               }).AsQueryable();
            r = r.WhereIf(!string.IsNullOrEmpty(criteria.ConditionsBookletPrice.ToString()) && criteria.ConditionsBookletPrice == (int)Enums.ConitionalBookletPriceList.LessThanMilion, (x => x.PublicTendersValue <= criteria.ConditionsBookletPrice))
               .WhereIf(!string.IsNullOrEmpty(criteria.ConditionsBookletPrice.ToString()) && criteria.ConditionsBookletPrice != (int)Enums.ConitionalBookletPriceList.LessThanMilion, (x => x.PublicTendersValue >= criteria.ConditionsBookletPrice)).AsQueryable();
            return r.ToList();
        }

        public async Task<QueryResult<PurchaseModel>> GetTendersPurshasesReportList(TenderSearchCriteria criteria)
        {
            var entities = await _context.ConditionsBooklets
    .Where(f =>
    f.Tender.IsActive == true &&
    !(f.Tender.SubmitionDate == null || f.Tender.SubmitionDate == DateTime.MinValue) &&
    f.IsActive == true &&
    f.BillInfo.PurchaseDate.HasValue == true)
    .WhereIf((criteria.BranchId != null && criteria.BranchId != 0), x => x.Tender.BranchId == criteria.BranchId)
    .WhereIf(!(criteria.FromLastOfferPresentationDate == null || criteria.FromLastOfferPresentationDate == DateTime.MinValue), x => x.CreatedAt >= criteria.FromLastOfferPresentationDate)
    .WhereIf(!(criteria.ToLastOfferPresentationDate == null || criteria.ToLastOfferPresentationDate == DateTime.MinValue), x => x.CreatedAt < (criteria.ToLastOfferPresentationDate.Value.AddDays(1)))
    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.Tender.AgencyCode == criteria.AgencyCode)
    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.Tender.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
    .Select(p => new PurchaseModel
    {
        TenderName = p.Tender.TenderName,
        CommericalRegisterNo = p.Supplier.SelectedCr,
        CommericalRegisterName = p.Supplier.SelectedCrName,
        ConditionsBookletPrice = p.Tender.ConditionsBookletPrice ?? 0,
        PurshaseDateString = p.BillInfo.PurchaseDate.HasValue ? p.BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy") : "",

    }).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            return entities;
        }

        public async Task<decimal> GetTenderPurshasesReportTotalAmount(TenderSearchCriteria criteria)
        {
            var result = await _context.ConditionsBooklets
    .Where(f =>
    f.Tender.IsActive == true &&
    !(f.Tender.SubmitionDate == null || f.Tender.SubmitionDate == DateTime.MinValue) &&
    f.IsActive == true &&
    f.BillInfo.PurchaseDate.HasValue == true)
    .WhereIf((criteria.BranchId != 0 && criteria.BranchId != null), x => x.Tender.BranchId == criteria.BranchId)
    .WhereIf(!(criteria.FromLastOfferPresentationDate == null || criteria.FromLastOfferPresentationDate == DateTime.MinValue), x => x.CreatedAt >= criteria.FromLastOfferPresentationDate)
    .WhereIf(!(criteria.ToLastOfferPresentationDate == null || criteria.ToLastOfferPresentationDate == DateTime.MinValue), x => x.CreatedAt < (criteria.ToLastOfferPresentationDate.Value.AddDays(1)))
    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.Tender.AgencyCode == criteria.AgencyCode)
    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.Tender.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
    .SumAsync(x => x.Tender.ConditionsBookletPrice);


            return result ?? 0;
        }

        public async Task<List<TendersSalesModel>> ReportGetTendersSalesAsync(TenderValueToTypeSearchCriteria criteria)
        {

            List<TendersSalesModel> result;

            var subResult = _context.ConditionsBooklets
             .Include(t => t.Tender)
             .Include(t => t.Supplier)
             .Include(t => t.BillInfo)
             .Where(f =>
             f.Tender.IsActive == true &&
             f.IsActive == true &&
             f.BillInfo.PurchaseDate.HasValue == true &&
             !(f.Tender.SubmitionDate == null || f.Tender.SubmitionDate == DateTime.MinValue))
             .WhereIf(!(criteria.FromCreatedDate == null || criteria.FromCreatedDate == DateTime.MinValue), x => x.Tender.SubmitionDate >= criteria.FromCreatedDate)
             .WhereIf(!(criteria.ToCreatedDate == null || criteria.ToCreatedDate == DateTime.MinValue), x => x.Tender.SubmitionDate < (criteria.ToCreatedDate.Value.AddDays(1)))
             .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.Tender.AgencyCode == criteria.AgencyCode)
             .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.Tender.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency()).ToList();

            if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Daily)
            {
                result = subResult.GroupBy(x =>
                      new { Year = x.BillInfo.PurchaseDate.Value.Year, Month = x.BillInfo.PurchaseDate.Value.Month, Day = x.BillInfo.PurchaseDate.Value.Day })
                     .Select(t => new TendersSalesModel
                     {
                         TenderName = t.FirstOrDefault().Tender.TenderName,
                         BookletCounts = t.Count(),
                         Date = t.FirstOrDefault().BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy"),
                         TenderSales = t.Sum(c => c.BillInfo.AmountDue)
                     }).ToList();
            }
            else if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Weekly)
            {
                result = subResult.GroupBy(x =>
                      new
                      {
                          Year = x.BillInfo.PurchaseDate.Value.Year,
                          Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                          x.BillInfo.PurchaseDate.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday)
                      })
                     .Select(t => new TendersSalesModel
                     {
                         TenderName = t.FirstOrDefault().Tender.TenderName,
                         BookletCounts = t.Count(),
                         Date = t.FirstOrDefault().BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy"),
                         TenderSales = t.Sum(c => c.BillInfo.AmountDue)
                     }).ToList();
            }
            else if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Monthly)
            {
                result = subResult.GroupBy(x =>
                      new { Year = x.BillInfo.PurchaseDate.Value.Year, Month = x.BillInfo.PurchaseDate.Value.Month })
                     .Select(t => new TendersSalesModel
                     {
                         TenderName = t.FirstOrDefault().Tender.TenderName,
                         BookletCounts = t.Count(),
                         Date = t.FirstOrDefault().BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy"),
                         TenderSales = t.Sum(c => c.BillInfo.AmountDue)
                     }).ToList();
            }
            else
                result = subResult.GroupBy(x =>
                      new { Year = x.BillInfo.PurchaseDate.Value.Year, Month = x.BillInfo.PurchaseDate.Value.Month, Day = x.BillInfo.PurchaseDate.Value.Day })
                     .Select(t => new TendersSalesModel
                     {
                         TenderName = t.FirstOrDefault().Tender.TenderName,
                         BookletCounts = t.Count(),
                         Date = t.FirstOrDefault().BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy"),
                         TenderSales = t.Sum(c => c.BillInfo.AmountDue)
                     }).ToList();

            return result;
        }

        public async Task<List<TendersPublishingModel>> ReportGetPublishedTendersAsync(TenderValueToTypeSearchCriteria criteria)
        {
            List<TendersPublishingModel> result;
            var subResult = await _context.Tenders.Include(g => g.Agency)
                .Where(t =>
                t.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing &&
                t.TenderStatusId != (int)Enums.TenderStatus.Established &&
                t.TenderStatusId != (int)Enums.TenderStatus.Pending &&
                t.TenderStatusId != (int)Enums.TenderStatus.Rejected &&
                t.SubmitionDate != null)

                .WhereIf((criteria.BranchId != 0 && criteria.BranchId != null), x => x.BranchId == criteria.BranchId)
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                       .WhereIf(criteria.FromCreatedDate != null && criteria.ToCreatedDate != null,
                     t => t.SubmitionDate >= criteria.FromCreatedDate && t.SubmitionDate <= criteria.ToCreatedDate).ToListAsync();


            if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Daily)
            {
                result = subResult.GroupBy(x =>
                      new { Year = x.SubmitionDate.Value.Year, Month = x.SubmitionDate.Value.Month, Day = x.SubmitionDate.Value.Day })
                     .Select(t => new TendersPublishingModel
                     {
                         AgencyName = t.FirstOrDefault().Agency.NameArabic,
                         TenderCounts = t.Count(),
                         Date = t.FirstOrDefault().SubmitionDate.Value.ToString("dd/MM/yyyy")
                     }).ToList();
            }
            else if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Weekly)
            {
                result = subResult.GroupBy(x =>
                      new
                      {
                          Year = x.SubmitionDate.Value.Year,
                          Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                          x.SubmitionDate.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday)
                      })
                   .Select(t => new TendersPublishingModel
                   {
                       AgencyName = t.FirstOrDefault().Agency.NameArabic,
                       TenderCounts = t.Count(),
                       Date = t.FirstOrDefault().SubmitionDate.Value.ToString("dd/MM/yyyy")
                   }).ToList();
            }
            else if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Monthly)
            {
                result = subResult.GroupBy(x =>
                      new { Year = x.SubmitionDate.Value.Year, Month = x.SubmitionDate.Value.Month })
                   .Select(t => new TendersPublishingModel
                   {
                       AgencyName = t.FirstOrDefault().Agency.NameArabic,
                       TenderCounts = t.Count(),
                       Date = t.FirstOrDefault().SubmitionDate.Value.ToString("dd/MM/yyyy")
                   }).ToList();
            }
            else
                result = subResult.GroupBy(x =>
                      new { Year = x.SubmitionDate.Value.Year, Month = x.SubmitionDate.Value.Month, Day = x.SubmitionDate.Value.Day })
                     .Select(t => new TendersPublishingModel
                     {
                         AgencyName = t.FirstOrDefault(q => q.Agency != null).Agency.NameArabic,
                         TenderCounts = t.Count(),
                         Date = t.FirstOrDefault().SubmitionDate.Value.ToString("dd/MM/yyyy")
                     }).ToList();



            return result;
        }

        public async Task<List<MostSuppliersHaveTendersModel>> ReportGetMostSuppliersHaveTendersAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var result = _context.Offers
                .Include(o => o.Supplier)
                .Where(o => (o.TotalOfferAwardingValue != null && o.TotalOfferAwardingValue > 0) ||
                (o.PartialOfferAwardingValue != null && o.PartialOfferAwardingValue > 0))

                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.Tender.AgencyCode == criteria.AgencyCode)
                .WhereIf((criteria.BranchId != 0), x => x.Tender.BranchId == criteria.BranchId)


                .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.Tender.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                .WhereIf(criteria.FromCreatedDate != null, o => o.Tender.SubmitionDate >= criteria.FromCreatedDate)
                .WhereIf(criteria.ToCreatedDate != null, o => o.Tender.SubmitionDate < criteria.ToCreatedDate.Value.AddDays(1))
                .WhereIf(!string.IsNullOrEmpty(criteria.ConditionsBookletPrice.ToString()) && criteria.ConditionsBookletPrice == (int)Enums.ConitionalBookletPriceList.LessThanMilion, x => x.Tender.ConditionsBookletPrice <= criteria.ConditionsBookletPrice)
                .WhereIf(!string.IsNullOrEmpty(criteria.ConditionsBookletPrice.ToString()) && criteria.ConditionsBookletPrice != (int)Enums.ConitionalBookletPriceList.LessThanMilion, x => x.Tender.ConditionsBookletPrice >= criteria.ConditionsBookletPrice)


                .GroupBy(
                o => o.Supplier,
                o => o.Tender,
                (cr, tender) => new MostSuppliersHaveTendersModel
                {
                    CommericalRegisterNo = cr.SelectedCrName,
                    NumberOfTenders = tender.Count()
                })

             .OrderByDescending(a => a.NumberOfTenders).ToList();

            return result;
        }

        public async Task<List<TenderCountsModel>> ReportGetTendersCountReportAsync(TenderSearchCriteria criteria)
        {
            var entities = _context.Tenders
                .Include(t => t.Status)
                .Where(f => f.IsActive == true)

                .WhereIf((criteria.BranchId != 0 && criteria.BranchId != null), x => x.BranchId == criteria.BranchId)
                .WhereIf(!(criteria.FromLastOfferPresentationDate == null || criteria.FromLastOfferPresentationDate == DateTime.MinValue), x => x.SubmitionDate >= criteria.FromLastOfferPresentationDate)
                .WhereIf(!(criteria.ToLastOfferPresentationDate == null || criteria.ToLastOfferPresentationDate == DateTime.MinValue), x => x.SubmitionDate < criteria.ToLastOfferPresentationDate.Value.AddDays(1))
                    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                .GroupBy(x => x.Status.NameAr)
                .Select(g => new TenderCountsModel { StatusName = g.Key, Count = g.Count(), })
                .ToList();
            return entities;
        }

        public async Task<QueryResult<Tender>> FindSuppliersPurshaseReport(TenderSearchCriteria criteria)
        {
            var entities = await _context.Tenders
                .Include(t => t.TenderType)
                .Include(t => t.Status)
                .Include(t => t.ConditionsBooklets)
                .Include(t => t.Invitations)
                .ThenInclude(x => x.InvitationStatus)
                .Where(f => f.IsActive == true)
                .WhereIf(criteria.TenderTypeId != 0, x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(!string.IsNullOrEmpty(criteria.TenderName), x => x.TenderName.Contains(criteria.TenderName))
                .WhereIf(!string.IsNullOrEmpty(criteria.TenderNumber), x => x.TenderNumber.Contains(criteria.TenderNumber))
                .WhereIf((criteria.TenderTypeId == (int)Enums.TenderType.CurrentTender || criteria.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase), x => x.TenderTypeId == criteria.TenderTypeId)
                .WhereIf(!string.IsNullOrEmpty(criteria.cr), x => x.ConditionsBooklets.Any(i => i.CommericalRegisterNo == criteria.cr && i.IsActive == true))
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return entities;
        }

        public async Task<AgencyTenderStatisticsModel> ReportGetAgencyStatisticsAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var result = await _context.Tenders
                .Include(t => t.Offers)
                .Include(t => t.Invitations)
                .Include(t => t.ConditionsBooklets)
                    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                .WhereIf(criteria.FromCreatedDate != null && criteria.ToCreatedDate != null,
                     t => t.SubmitionDate >= criteria.FromCreatedDate && t.SubmitionDate <= criteria.ToCreatedDate)
                 .Select(t => new
                 {
                     TendersCount = t.TenderTypeId == (int)Enums.TenderType.CurrentTender ? 1 : 0,
                     PurshaseCount = t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase ? 1 : 0,
                     BookletsCount = t.ConditionsBooklets.Count(),
                     OpeningOfferCount = t.TenderStatusId == (int)Enums.TenderStatus.OffersOppening ? 1 : 0,
                     CheckingOfferCount = t.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending ? 1 : 0,
                     AwardingOfferCount = t.TenderStatusId == (int)Enums.TenderStatus.OffersAwarding ? 1 : 0,
                     OfferCount = t.Offers.Count(),
                     ActiveTenderSuppliersCount = t.TenderTypeId == (int)Enums.TenderType.CurrentTender ? t.Offers.Count() : 0,
                     ActivePurshaseSuppliersCount = t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase ? t.Offers.Count() : 0,
                 }).ToListAsync();

            AgencyTenderStatisticsModel data = new AgencyTenderStatisticsModel
            {
                totalTendrs = result.Count(),
                tendersCount = result.Select(x => x.TendersCount).Sum(),
                purshasesCount = result.Select(x => x.PurshaseCount).Sum()
                ,
                offerCheckingCount = result.Select(x => x.CheckingOfferCount).Sum()
                ,
                offerOpeningCount = result.Select(x => x.OpeningOfferCount).Sum()
                ,
                offerAwardingCount = result.Select(x => x.AwardingOfferCount).Sum()
                ,
                offersCount = result.Select(x => x.OfferCount).Sum()
                ,
                suppliersCount = result.Select(x => x.OfferCount).Sum()
                ,
                tenderSuppliersCount = result.Select(x => x.ActiveTenderSuppliersCount).Sum()
                ,
                purshaseSuppliersCount = result.Select(x => x.ActivePurshaseSuppliersCount).Sum()
            };

            return data;
        }
 
        public async Task<QueryResult<NotifyModel>> GetDailyNotificationsList(NotifySearchCriteria criteria)
        {
            var result = await _context.Notifications
                                                .Select(n => new
                                                {
                                                    Title = (n is NotificationEmail) ? ((NotificationEmail)n).Title : (n is NotificationPanel) ? ((NotificationPanel)n).Title : Resources.SharedResources.DisplayInputs.SMS,
                                                    Status = n.NotifacationStatusId == (int)Enums.NotifacationStatus.FailedToSend ? (int)Enums.NotifacationStatus.FailedToSend
                    : n.NotifacationStatusId == (int)Enums.NotifacationStatus.Unsent ? (int)Enums.NotifacationStatus.Unsent :
                    (int)Enums.NotifacationStatus.Send,
                                                    StartDateTime = n.CreatedAt.ToString("dd/MM/yyyy hh:mm", new CultureInfo("en-US")),
                                                    StartDateTimeHijri = n.CreatedAt.ToString("dd/MM/yyyy hh:mm", new CultureInfo("ar-SA")),
                                                    EndDateTime = n.sendAt.HasValue ? n.sendAt.Value.ToString("dd/MM/yyyy hh:mm", new CultureInfo("en-US")) : "",
                                                    EndDateTimeHijri = n.sendAt.HasValue ? n.sendAt.Value.ToString("dd/MM/yyyy hh:mm", new CultureInfo("ar-SA")) : "",
                                                }).GroupBy(n =>
                                                n.Title,
                n => n,
                (Title, Notifications) => new NotifyModel
                {
                    Title = Title,
                    SentCount = Notifications.Where(n => n.Status == (int)Enums.NotifacationStatus.Send).Count(),
                    FailedToSendCount = Notifications.Where(n => n.Status == (int)Enums.NotifacationStatus.FailedToSend).Count(),
                    UnsentCount = Notifications.Where(n => n.Status == (int)Enums.NotifacationStatus.Unsent).Count(),
                    StartDateTime = Notifications.Any(n => n.Status == (int)Enums.NotifacationStatus.Send) ? Notifications.Where(n => n.Status == (int)Enums.NotifacationStatus.Send).FirstOrDefault().StartDateTime : "",
                    StartDateTimeHijri = Notifications.Any(n => n.Status == (int)Enums.NotifacationStatus.Send) ? Notifications.Where(n => n.Status == (int)Enums.NotifacationStatus.Send).FirstOrDefault().StartDateTimeHijri : "",
                    EndDateTime = Notifications.Any(n => n.Status == (int)Enums.NotifacationStatus.Send) ? Notifications.Where(n => n.Status == (int)Enums.NotifacationStatus.Send).LastOrDefault().StartDateTime : "",
                    EndDateTimeHijri = Notifications.Any(n => n.Status == (int)Enums.NotifacationStatus.Send) ? Notifications.Where(n => n.Status == (int)Enums.NotifacationStatus.Send).LastOrDefault().StartDateTimeHijri : ""
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            return result;
        }

        public async Task<QueryResult<TendersSummryReportViewModel>> GetTendersSummaryAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _context.Tenders
                .WhereIf(!string.IsNullOrEmpty(searchCriteria.AgencyCode), a => a.AgencyCode == searchCriteria.AgencyCode)
                .WhereIf(searchCriteria.FromCreatedDate.HasValue == true, d => d.SubmitionDate >= searchCriteria.FromCreatedDate.Value)
                .WhereIf(searchCriteria.ToCreatedDate.HasValue == true, d => d.SubmitionDate <= searchCriteria.ToCreatedDate.Value)
                .GroupBy(t =>
                t.Branch,
                t => t,
                (Branch, tenders) => new TendersSummryReportViewModel
                {
                    AgencyName = tenders.FirstOrDefault().Agency.NameArabic,

                    BranchName = Branch.BranchName,

                    TendersCount = tenders
                        .Count(t => t.TenderTypeId == (int)Enums.TenderType.CurrentTender && t.IsActive == true),

                    DirectPurchaseCount = tenders
                        .Count(t => t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && t.IsActive == true),

                    TenderLimitedValue = tenders.Where(t => t.TenderTypeId == (int)Enums.TenderType.LimitedTender && t.IsActive == true && t.ConditionsBookletPrice.HasValue)
                        .Sum(t => (decimal)t.ConditionsBookletPrice),

                    TenderReverseBiddingValue = tenders.Where(t => t.TenderTypeId == (int)Enums.TenderType.ReverseBidding && t.IsActive == true && t.ConditionsBookletPrice.HasValue)
                        .Sum(t => (decimal)t.ConditionsBookletPrice),

                    TenderFrameworkAgreementValue = tenders.Where(t => t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement && t.IsActive == true && t.ConditionsBookletPrice.HasValue)
                        .Sum(t => (decimal)t.ConditionsBookletPrice),

                    NumberOfPurchasedSuppliers = tenders.Where(t => t.IsActive == true)
                        .SelectMany(t => t.ConditionsBooklets.Where(b => b.IsActive == true && b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid),
                        (parent, child) => new { child.CommericalRegisterNo }).Count(),

                    NumberOfSuppliersQueries = tenders.Where(t => t.IsActive == true)
                        .SelectMany(t => t.Enquiries.Where(b => b.IsActive == true), (parent, child) => new { child.EnquiryId }).Count(),

                    NumberOfOppenedOffers = tenders.Where(t => t.IsActive == true &&
                    t.TenderHistories.Any(h => h.IsActive == true && h.StatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed))
                        .Count(),

                    NumberTechnicalCheckedOfferd = tenders.Where(t => t.IsActive == true &&
                    t.TenderHistories.Any(h => h.IsActive == true && h.StatusId == (int)Enums.TenderStatus.OffersCheckedConfirmed))
                        .Count(),

                    NumberAwardedOffered = tenders.Where(t => t.IsActive == true &&
                    t.TenderHistories.Any(h => h.IsActive == true && h.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed))
                        .Count(),

                    ElectronicOffersOnTenders = tenders.Where(t => t.IsActive == true && t.TenderTypeId == (int)Enums.TenderType.CurrentTender)
                    .SelectMany(t => t.Offers.Where(o => o.IsActive == true), (parent, child) => new { child.OfferId }).Count(),

                    ElectronicOffersOnDirectPurchase = tenders.Where(t => t.IsActive == true && t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
                    .SelectMany(t => t.Offers.Where(o => o.IsActive == true), (parent, child) => new { child.OfferId }).Count(),

                }).ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);

            return result;
        }

        public async Task<TendersSummryReportCountsViewModel> GetTendersSummaryCountsAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            if (!searchCriteria.FromCreatedDate.HasValue)
                searchCriteria.FromCreatedDate = DateTime.MinValue;
            if (!searchCriteria.ToCreatedDate.HasValue)
                searchCriteria.ToCreatedDate = DateTime.MaxValue;
            var result = _context.GovAgencies
                .WhereIf(!string.IsNullOrEmpty(searchCriteria.AgencyCode), a => a.AgencyCode == searchCriteria.AgencyCode)
                .Select(agency => new TendersSummryReportCountsViewModel
                {
                    NumberOfActiveSuppliersOnPublicTenders = agency.Tenders
                    .Where(d => d.SubmitionDate >= searchCriteria.FromCreatedDate.Value)
                    .Where(d => d.SubmitionDate <= searchCriteria.ToCreatedDate.Value)
                    .SelectMany(t => t.ConditionsBooklets.Where(b => b.IsActive == true && b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid),
                    (parent, child) => new { child.CommericalRegisterNo }).Distinct().Count(),

                    NumberOfActiveSuppliersOnDirectPurchase = agency.Tenders
                    .Where(d => d.SubmitionDate >= searchCriteria.FromCreatedDate.Value)
                    .Where(d => d.SubmitionDate <= searchCriteria.ToCreatedDate.Value)
                    .SelectMany(t => t.Invitations.Where(i => i.IsActive == true && i.StatusId == (int)Enums.InvitationStatus.Approved),
                    (parent, child) => new { child.CommericalRegisterNo }).Distinct().Count(),

                    NumberOfActiveSuppliers =
                    agency.Tenders
                    .Where(d => d.SubmitionDate >= searchCriteria.FromCreatedDate.Value)
                    .Where(d => d.SubmitionDate <= searchCriteria.ToCreatedDate.Value)
                    .SelectMany(t => t.ConditionsBooklets.Where(b => b.IsActive == true && b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid),
                    (parent, child) => new { child.CommericalRegisterNo })
                    .Union(
                     agency.Tenders
                    .Where(d => d.SubmitionDate >= searchCriteria.FromCreatedDate.Value)
                    .Where(d => d.SubmitionDate <= searchCriteria.ToCreatedDate.Value)
                    .SelectMany(t => t.Invitations.Where(i => i.IsActive == true && i.StatusId == (int)Enums.InvitationStatus.Approved),
                    (parent, child) => new { child.CommericalRegisterNo })).Count()
                });

            var result1 = await result
            .GroupBy(b => 1)
            .Select(b => new TendersSummryReportCountsViewModel
            {
                NumberOfActiveSuppliers = b.Sum(p => p.NumberOfActiveSuppliers),
                NumberOfActiveSuppliersOnDirectPurchase = b.Sum(p => p.NumberOfActiveSuppliersOnDirectPurchase),
                NumberOfActiveSuppliersOnPublicTenders = b.Sum(p => p.NumberOfActiveSuppliersOnPublicTenders),
            }).FirstOrDefaultAsync();

            result1.NumberOfSuppliers = await _supplierService.GetSuppliersCountFromIDM();
            return result1;
        }


        public async Task<QueryResult<AgencyFileViewModel>> GetAgencyFileAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _context.Tenders
                .WhereIf(!string.IsNullOrEmpty(searchCriteria.AgencyCode), a => a.AgencyCode == searchCriteria.AgencyCode)
                                                .GroupBy(t =>
                t.Branch,
                t => t,
                (Branch, tenders) => new AgencyFileViewModel
                {

                    AgencyName = tenders.FirstOrDefault().Agency.NameArabic,
                    BranchName = Branch.BranchName,

                    TendersCount = tenders
                        .Count(t => t.TenderTypeId == (int)Enums.TenderType.CurrentTender && t.IsActive == true),
                    FirstTenderDate = tenders
                        .Where(t => t.TenderTypeId == (int)Enums.TenderType.CurrentTender && t.IsActive == true).OrderBy(x => x.SubmitionDate).FirstOrDefault().SubmitionDate.ToString(),

                    LastTenderDate = tenders
                        .Where(t => t.TenderTypeId == (int)Enums.TenderType.CurrentTender && t.IsActive == true)
                        .OrderByDescending(x => x.SubmitionDate).FirstOrDefault().SubmitionDate.ToString(),


                    DirectPurchaseCount = tenders
                        .Count(t => t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && t.IsActive == true),

                    TenderLimitedValue = tenders.Where(t => t.TenderTypeId == (int)Enums.TenderType.LimitedTender && t.IsActive == true && t.ConditionsBookletPrice.HasValue)
                        .Sum(t => (decimal)t.ConditionsBookletPrice),

                    TenderReverseBiddingValue = tenders.Where(t => t.TenderTypeId == (int)Enums.TenderType.ReverseBidding && t.IsActive == true && t.ConditionsBookletPrice.HasValue)
                        .Sum(t => (decimal)t.ConditionsBookletPrice),

                    TenderFrameworkAgreementValue = tenders.Where(t => t.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement && t.IsActive == true && t.ConditionsBookletPrice.HasValue)
                        .Sum(t => (decimal)t.ConditionsBookletPrice),

                    NumberOfPurchasedSuppliers = tenders.Where(t => t.IsActive == true)
                        .SelectMany(t => t.ConditionsBooklets.Where(b => b.IsActive == true && b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid),
                        (parent, child) => new { child.CommericalRegisterNo }).Count(),


                    TotalPurchasedValue = tenders.Where(t => t.IsActive == true)
                        .SelectMany(t => t.ConditionsBooklets.Where(b => b.IsActive == true && b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid),
                        (parent, child) => new { child.BillInfo.CurrnetAmount }).Count(),

                }).ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);

            return result;
        }

        public async Task<QueryResult<MostSuppliersHaveTendersModel>> ReportGetMostSuppliersHaveTendersListAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var result = _context.Offers
                .Include(o => o.Supplier)
                .Where(o => (o.TotalOfferAwardingValue != null && o.TotalOfferAwardingValue > 0) ||
                (o.PartialOfferAwardingValue != null && o.PartialOfferAwardingValue > 0))
                    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.Tender.AgencyCode == criteria.AgencyCode)
                    .WhereIf((criteria.BranchId != 0), x => x.Tender.BranchId == criteria.BranchId)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.Tender.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                .WhereIf(criteria.FromCreatedDate != null, o => o.Tender.SubmitionDate >= criteria.FromCreatedDate)
                .WhereIf(criteria.ToCreatedDate != null, o => o.Tender.SubmitionDate < criteria.ToCreatedDate.Value.AddDays(1))
                .WhereIf(!string.IsNullOrEmpty(criteria.ConditionsBookletPrice.ToString()) && criteria.ConditionsBookletPrice == (int)Enums.ConitionalBookletPriceList.LessThanMilion, x => x.Tender.ConditionsBookletPrice <= criteria.ConditionsBookletPrice)
                .WhereIf(!string.IsNullOrEmpty(criteria.ConditionsBookletPrice.ToString()) && criteria.ConditionsBookletPrice != (int)Enums.ConitionalBookletPriceList.LessThanMilion, x => x.Tender.ConditionsBookletPrice >= criteria.ConditionsBookletPrice)


                .GroupBy(
                o => o.Supplier,
                o => o.Tender,
                (cr, tender) => new MostSuppliersHaveTendersModel
                {
                    CommericalRegisterNo = cr.SelectedCrName,
                    NumberOfTenders = tender.Count()
                }).OrderByDescending(a => a.NumberOfTenders).ToQueryResult(criteria.PageNumber, criteria.PageSize);

            var r = await result;
            return r;
        }

        public async Task<QueryResult<TenderCountsModel>> GetTendersCountReportList(TenderSearchCriteria criteria)
        {
            var entities = await _context.Tenders
                     .Include(t => t.Status)
                     .Where(f => f.IsActive == true)
                     .WhereIf((criteria.BranchId != null && criteria.BranchId != 0), x => x.BranchId == criteria.BranchId)
                     .WhereIf(!(criteria.FromLastOfferPresentationDate == null || criteria.FromLastOfferPresentationDate == DateTime.MinValue), x => x.SubmitionDate >= criteria.FromLastOfferPresentationDate)
                     .WhereIf(!(criteria.ToLastOfferPresentationDate == null || criteria.ToLastOfferPresentationDate == DateTime.MinValue), x => x.SubmitionDate < criteria.ToLastOfferPresentationDate.Value.AddDays(1))
                    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                          .GroupBy(x => x.Status.NameAr)
                     .Select(g => new TenderCountsModel { StatusName = g.Key, Count = g.Count(), })
                     .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return entities;
        }

        public int GetTendersCountReportsCount(TenderSearchCriteria criteria)
        {
            var count = _context.Tenders
                     .Include(t => t.Status)
                     .Where(f => f.IsActive == true)
                     .WhereIf((criteria.BranchId != null && criteria.BranchId != 0), x => x.BranchId == criteria.BranchId)
                     .WhereIf(!(criteria.FromLastOfferPresentationDate == null || criteria.FromLastOfferPresentationDate == DateTime.MinValue), x => x.SubmitionDate >= criteria.FromLastOfferPresentationDate)
                     .WhereIf(!(criteria.ToLastOfferPresentationDate == null || criteria.ToLastOfferPresentationDate == DateTime.MinValue), x => x.SubmitionDate < criteria.ToLastOfferPresentationDate.Value.AddDays(1))
                    .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)
                    .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                                          .Count();
            return count;
        }

        public async Task<QueryResult<Tender>> GetTendersReportList(TenderSearchCriteria criteria)
        {
            var entities = await _context.Tenders
                .Include(t => t.Status)
                .Where(f => f.IsActive == true)
                .WhereIf((criteria.BranchId != 0 && criteria.BranchId != null), x => x.BranchId == criteria.BranchId)
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode).WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                .WhereIf(!(criteria.FromLastOfferPresentationDate == null || criteria.FromLastOfferPresentationDate == DateTime.MinValue), x => x.SubmitionDate >= criteria.FromLastOfferPresentationDate)
                .WhereIf(!(criteria.ToLastOfferPresentationDate == null || criteria.ToLastOfferPresentationDate == DateTime.MinValue), x => x.SubmitionDate < criteria.ToLastOfferPresentationDate.Value.AddDays(1))
                .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return entities;
        }

        public async Task<List<DirectInvitationsReportModel>> DirectInvitationsReport(TenderValueToTypeSearchCriteria criteria)
        {
            List<DirectInvitationsReportModel> result;
            var subResult = await _context.Tenders.Include(x => x.Invitations).Include(x => x.ConditionsBooklets)
                .Where(t =>
                t.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing &&
                t.TenderStatusId != (int)Enums.TenderStatus.Established &&
                t.TenderStatusId != (int)Enums.TenderStatus.Pending &&
                t.TenderStatusId != (int)Enums.TenderStatus.Rejected &&
                t.SubmitionDate != null &&
                t.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase &&
                t.InvitationTypeId == (int)Enums.InvitationType.Specific &&
                t.SubmitionDate != null)
                .WhereIf((criteria.BranchId != 0 && criteria.BranchId != null), x => x.BranchId == criteria.BranchId)
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)
                .WhereIf((!isInRole(RoleNames.MonafasatAccountManager)), x => x.AgencyCode == _httpContextAccessor.HttpContext.User.UserAgency())
                .WhereIf(criteria.FromCreatedDate != null && criteria.ToCreatedDate != null,
                     t => t.SubmitionDate.Value.Date >= criteria.FromCreatedDate.Value.Date && t.SubmitionDate.Value.Date <= criteria.ToCreatedDate.Value.Date).ToListAsync();


            if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Daily)
            {
                result = GetInvitationsDaily(subResult).Select(s => new DirectInvitationsReportModel
                {
                    Date = s.Date,
                    TotalInvitationsCount = s.TenderCounts.Select(x => x.Value).Select(k => k.InvitationsCount).Sum(),
                    TotalPurchaseCount = s.TenderCounts.Select(c => c.Value).Count()
                }).OrderBy(c => c.Date).ToList();
            }
            else if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Weekly)
            {
                result = GetInvitationsWeekly(subResult).Select(s => new DirectInvitationsReportModel
                {
                    Date = s.Date,
                    TotalInvitationsCount = s.TenderCounts.Select(x => x.Value).Select(k => k.InvitationsCount).Sum(),
                    TotalPurchaseCount = s.TenderCounts.Select(c => c.Value).Count()
                }).OrderBy(c => c.Date).ToList();
            }
            else if (criteria.IntervalPeriod == (int)Enums.PeriodOfTime.Monthly)
            {
                result = GetInvitationsMonthly(subResult).Select(s => new DirectInvitationsReportModel
                {
                    Date = s.Date,
                    TotalInvitationsCount = s.TenderCounts.Select(x => x.Value).Select(k => k.InvitationsCount).Sum(),
                    TotalPurchaseCount = s.TenderCounts.Select(c => c.Value).Count()
                }).ToList();
            }
            else
                result = GetInvitations(subResult).Select(s => new DirectInvitationsReportModel
                {
                    Date = s.Date,
                    TotalInvitationsCount = s.TenderCounts.Select(x => x.Value).Select(k => k.InvitationsCount).Sum(),
                    TotalPurchaseCount = s.TenderCounts.Select(c => c.Value).Count()
                }).ToList();

            return result;
        }

        public static List<DirectInvitationsReportModel> GetInvitations(List<Tender> subResult)
        {
            List<DirectInvitationsReportModel> result;
            result = subResult.GroupBy(x =>
                   new { Year = x.SubmitionDate.Value.Year, Month = x.SubmitionDate.Value.Month, Day = x.SubmitionDate.Value.Day })
                    .Select(t => new DirectInvitationsReportModel
                    {
                        Date = t.FirstOrDefault().SubmitionDate.Value.ToString("dd/MM/yyyy"),
                        TenderCounts = t.ToDictionary(c => c.TenderId, z => new TenderCounts { PurchaseCount = z.TenderId, InvitationsCount = z.Invitations.Count() })
                    }).OrderByDescending(c => c.Date).ToList();

            return result;
        }

        public static List<DirectInvitationsReportModel> GetInvitationsMonthly(List<Tender> subResult)
        {
            List<DirectInvitationsReportModel> result;
            result = subResult.GroupBy(x =>
               new { x.SubmitionDate.Value.Month })
                    .Select(t => new DirectInvitationsReportModel
                    {
                        Date = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(t.FirstOrDefault().SubmitionDate.Value.Month),
                        TenderCounts = t.ToDictionary(c => c.TenderId, z => new TenderCounts { PurchaseCount = z.ConditionsBooklets.Count(), InvitationsCount = z.Invitations.Count() })
                    }).OrderBy(c => c.Date).ToList();

            return result;
        }

        public static List<DirectInvitationsReportModel> GetInvitationsWeekly(List<Tender> subResult)
        {
            List<DirectInvitationsReportModel> result;
            result = subResult.GroupBy(x =>
                     new
                     {
                         Year = x.SubmitionDate.Value.Year,
                         Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                          x.SubmitionDate.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday)
                     })
                    .Select(t => new DirectInvitationsReportModel
                    {
                        Date = t.FirstOrDefault().SubmitionDate.Value.ToString("dd/MM/yyyy"),
                        TenderCounts = t.ToDictionary(c => c.TenderId, z => new TenderCounts { PurchaseCount = z.ConditionsBooklets.Count(), InvitationsCount = z.Invitations.Count() })
                    }).OrderBy(c => c.Date).ToList();

            return result;
        }

        public static List<DirectInvitationsReportModel> GetInvitationsDaily(List<Tender> subResult)
        {
            List<DirectInvitationsReportModel> result;
            result = subResult.GroupBy(x =>
                      new { Year = x.SubmitionDate.Value.Year, Month = x.SubmitionDate.Value.Month, Day = x.SubmitionDate.Value.Day })
                    .Select(t => new DirectInvitationsReportModel
                    {
                        Date = t.FirstOrDefault().SubmitionDate.Value.ToString("dd/MM/yyyy"),
                        TenderCounts = t.ToDictionary(c => c.TenderId, z => new TenderCounts { PurchaseCount = z.ConditionsBooklets.Count(), InvitationsCount = z.Invitations.Count() })
                    }).OrderBy(c => c.Date).ToList();

            return result;
        }

        #region TenderMonthlySalesReceipteReport
        public async Task<TenderSalesMonthlyCountsPerMonth> GetTenderSalesMonthlyCountsPerMonth(TenderValueToTypeSearchCriteria searchCriteria)
        {
            DateTime date = DateTime.Now.AddMonths(-1);
            if (searchCriteria.FromCreatedDate.HasValue && searchCriteria.FromCreatedDate.Value != DateTime.MinValue)
            {
                date = searchCriteria.FromCreatedDate.Value;
            }
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var result = await _context.ConditionsBooklets
                .Where(b => b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid &&
                            b.BillInfo.PurchaseDate.HasValue ? b.BillInfo.PurchaseDate.Value >= firstDayOfMonth && b.BillInfo.PurchaseDate.Value <= lastDayOfMonth : false)
                .GroupBy(b => 1).Select(b => new TenderSalesMonthlyCountsPerMonth
                {
                    CurrentMonthSalesAmount = b.Sum(p => p.BillInfo.AmountDue),
                    CurrentMonthSalesCount = b.Count(),
                    Month = date.Month + "/" + date.Year
                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<QueryResult<TenderSalesMonthlyRecipetReportPerAgency>> GetTenderSalesMonthlyRecipetReportPerAgency(TenderValueToTypeSearchCriteria searchCriteria)
        {
            DateTime date = DateTime.Now.AddMonths(-1);
            if (searchCriteria.FromCreatedDate.HasValue && searchCriteria.FromCreatedDate.Value != DateTime.MinValue)
            {
                date = searchCriteria.FromCreatedDate.Value;
            }
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


            var result = await _context.ConditionsBooklets
                .Where(b => b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid &&
                            b.BillInfo.PurchaseDate.HasValue ? b.BillInfo.PurchaseDate.Value >= firstDayOfMonth && b.BillInfo.PurchaseDate.Value <= lastDayOfMonth : false)
                .GroupBy(b => b.Tender.Agency)
                .Select(b => new TenderSalesMonthlyRecipetReportPerAgency
                {
                    AgencyCode = b.Key.AgencyCode,
                    AgencyName = b.Key.NameArabic,
                    AgencySector = "",
                    AgencyBranch = "",
                    AgencySection = "",
                    AgencySequence = "",
                    AgencyManagmentFollow = "",
                    AgencySectionFollow = "",
                    NumberOfTransactions = b.Count(),
                    TotalAmount = b.Sum(c => c.BillInfo.AmountDue)
                }).ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);

            return result;
        }

        public async Task<TenderSalesMonthlyCountsPerMonth> GetAllTenderSalesMonthlyCountsPerMonth(TenderValueToTypeSearchCriteria searchCriteria)
        {
            DateTime date = DateTime.Now.AddMonths(-1);
            if (searchCriteria.FromCreatedDate.HasValue && searchCriteria.FromCreatedDate.Value != DateTime.MinValue)
            {
                date = searchCriteria.FromCreatedDate.Value;
            }
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            var result = await _context.ConditionsBooklets
                .Where(b => b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid &&
                            b.BillInfo.PurchaseDate.HasValue ? b.BillInfo.PurchaseDate.Value < firstDayOfMonth : false)
                .GroupBy(b => 1).Select(b => new TenderSalesMonthlyCountsPerMonth
                {
                    AllMonthsSalesAmount = b.Sum(p => p.BillInfo.AmountDue),
                    AllMonthsSalesCount = b.Count()
                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<QueryResult<TenderSalesMonthlyTenderDetails>> GetTenderSalesMonthlyTenderDetails(TenderValueToTypeSearchCriteria searchCriteria)
        {
            DateTime date = DateTime.Now.AddMonths(-1);
            if (searchCriteria.FromCreatedDate.HasValue && searchCriteria.FromCreatedDate.Value != DateTime.MinValue)
            {
                date = searchCriteria.FromCreatedDate.Value;
            }
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var result = await _context.ConditionsBooklets
                .Where(b => b.BillInfo.BillStatusId == (int)Enums.BillStatus.Paid &&
                            b.BillInfo.PurchaseDate.HasValue ? b.BillInfo.PurchaseDate.Value >= firstDayOfMonth && b.BillInfo.PurchaseDate.Value <= lastDayOfMonth : false)
                .Select(b => new TenderSalesMonthlyTenderDetails
                {
                    TenderName = b.Tender.TenderName,
                    PurchaseDate = b.BillInfo.PurchaseDate.Value.ToString("dd/MM/yyyy"),
                    CompanyCommercialName = b.Supplier.SelectedCrName,
                    BookletPrice = b.Tender.ConditionsBookletPrice
                }).ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);

            return result;
        }
        public async Task<QueryResult<PerformanceReportModel>> GetPerformanceReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var isAr = CultureInfo.CurrentCulture.Name == "ar-SA";


            var result = await _context.GovAgencies
                .Select(x => new PerformanceReportModel
                {

                    AgencyName = isAr ? x.NameArabic : x.NameEnglish,
                    TotalCountTheTendersReviewed = 10,
                    AverageDurationReviewOfoneTender = 20,
                    TotalCountTheTendersApproved = 22,
                    TotalCountTheTendersRejected = 10,
                    TotalCountTheTendersReturned = 33,
                    TotalCountTheTendersTransferredToUnitSpecialistLevel2 = 39,
                    TotalEstimatedValue = 50,

                })
               .ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);


            return result;
        }

        public async Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendAgencyAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var result = await _context.Tenders

                .Include(x => x.SpendingCategory)
                 .Include(x => x.Agency)
                .Where(f => f.IsActive == true && f.EstimatedValue != null && f.SubmitionDate != null && f.SpendingCategoryId != null)

                 .WhereIf(!(criteria.FromAverageSpending == null || criteria.FromAverageSpending > 0), x => x.EstimatedValue >= criteria.FromAverageSpending)
                 .WhereIf(!(criteria.ToAverageSpending == null || criteria.ToAverageSpending > 0), x => x.EstimatedValue <= criteria.FromAverageSpending)

            .WhereIf(!(criteria.FromCreatedDate == null || criteria.FromCreatedDate == DateTime.MinValue), x => x.SubmitionDate >= criteria.FromCreatedDate)
             .WhereIf(!(criteria.ToCreatedDate == null || criteria.ToCreatedDate == DateTime.MinValue), x => x.SubmitionDate < (criteria.ToCreatedDate.Value.AddDays(1)))
             .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)

             .GroupBy(x => new { x.AgencyCode, AgencyName = x.Agency.NameArabic }, t => t, (key, tender) => new ReportSpendingBySpendAgencyModel
             {

                 AgencyName = key.AgencyName,
                 AverageExpenditureOfContracts = 10,
                 AverageSpending = 20,
                 DateFrom = tender.Min(x => x.SubmitionDate),
                 DateTo = tender.Max(x => x.SubmitionDate),
                 SizeOfSpending = tender.Sum(x => x.EstimatedValue),
                 TopItemSpend = tender.Count() > 0 ? tender.OrderByDescending(x => x.EstimatedValue).FirstOrDefault().SpendingCategory.NameAr : "",
                 DetailsOfSpend = tender.Select(x => new ItemOfExpenditure { Name = x.SpendingCategory.NameAr, SizeOfSpending = x.EstimatedValue }).ToList(),
                 TenderCount = tender.Count()

             }).ToQueryResult(criteria.PageNumber, criteria.PageSize); ;




            return result;
        }
        public async Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendCategoryAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var result = await _context.Tenders

                .Include(x => x.SpendingCategory)

                .Where(f => f.IsActive == true && f.EstimatedValue != null && f.SubmitionDate != null && f.SpendingCategoryId != null)

                 .WhereIf(!(criteria.FromAverageSpending == null || criteria.FromAverageSpending > 0), x => x.EstimatedValue >= criteria.FromAverageSpending)
                 .WhereIf(!(criteria.ToAverageSpending == null || criteria.ToAverageSpending > 0), x => x.EstimatedValue <= criteria.FromAverageSpending)
                 .WhereIf(criteria.SpendingCategoryId != null && criteria.SpendingCategoryId > 0, x => x.SpendingCategoryId == criteria.SpendingCategoryId)
                .WhereIf(!(criteria.FromCreatedDate == null || criteria.FromCreatedDate == DateTime.MinValue), x => x.SubmitionDate >= criteria.FromCreatedDate)
                .WhereIf(!(criteria.ToCreatedDate == null || criteria.ToCreatedDate == DateTime.MinValue), x => x.SubmitionDate < (criteria.ToCreatedDate.Value.AddDays(1)))
                .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)

             .GroupBy(x => new { x.SpendingCategoryId, Name = x.SpendingCategory.NameAr }, t => t, (key, tender) => new ReportSpendingBySpendAgencyModel
             {

                 AgencyName = key.Name,
                 AverageExpenditureOfContracts = 10,
                 AverageSpending = 20,
                 DateFrom = tender.Min(x => x.SubmitionDate),
                 DateTo = tender.Max(x => x.SubmitionDate),
                 SizeOfSpending = tender.Sum(x => x.EstimatedValue),
                 TopItemSpend = tender.Count() > 0 ? tender.OrderByDescending(x => x.EstimatedValue).FirstOrDefault().SpendingCategory.NameAr : "",
                 DetailsOfSpend = tender.Select(x => new ItemOfExpenditure { Name = x.TenderName, SizeOfSpending = x.EstimatedValue }).ToList(),
                 TenderCount = tender.Count()

             }).ToQueryResult(criteria.PageNumber, criteria.PageSize); ;




            return result;
        }

        public async Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendActivitiesAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var result = await _context.Tenders
             .Include(x => x.SpendingCategory)
             .Include(x => x.Agency)
             .Where(f => f.IsActive == true && f.EstimatedValue != null && f.SubmitionDate != null && f.SpendingCategoryId != null)
             .WhereIf(!(criteria.FromAverageSpending == null || criteria.FromAverageSpending > 0), x => x.EstimatedValue >= criteria.FromAverageSpending)
             .WhereIf(!(criteria.ToAverageSpending == null || criteria.ToAverageSpending > 0), x => x.EstimatedValue <= criteria.FromAverageSpending)
             .WhereIf(!(criteria.FromCreatedDate == null || criteria.FromCreatedDate == DateTime.MinValue), x => x.SubmitionDate >= criteria.FromCreatedDate)
             .WhereIf(!(criteria.ToCreatedDate == null || criteria.ToCreatedDate == DateTime.MinValue), x => x.SubmitionDate < (criteria.ToCreatedDate.Value.AddDays(1)))
             .WhereIf((isInRole(RoleNames.MonafasatAccountManager) && criteria.AgencyCode != "" && criteria.AgencyCode != null), x => x.AgencyCode == criteria.AgencyCode)
             .WhereIf(!string.IsNullOrEmpty(criteria.ActivityId.ToString()), x => x.TenderActivities.Select(c => c.ActivityId).FirstOrDefault() == criteria.ActivityId)
             .GroupBy(x => new { x.AgencyCode, AgencyName = x.Agency.NameArabic }, t => t, (key, tender) => new ReportSpendingBySpendAgencyModel
             {
                 AgencyName = key.AgencyName,
                 AverageSpending = 20,
                 DateFrom = tender.Min(x => x.SubmitionDate),
                 DateTo = tender.Max(x => x.SubmitionDate),
                 SizeOfSpending = tender.Sum(x => x.EstimatedValue),
                 TopItemSpend = tender.Count() > 0 ? tender.OrderByDescending(x => x.EstimatedValue).FirstOrDefault().SpendingCategory.NameAr : "",
                 TenderCount = tender.Count()
             }).ToQueryResult(criteria.PageNumber, criteria.PageSize); ;

            return result;
        }
        #endregion TenderMonthlySalesReceipteReport
    }
}
