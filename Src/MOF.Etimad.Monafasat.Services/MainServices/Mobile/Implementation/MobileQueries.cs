using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.MobileHelper;
using MOF.Etimad.Monafasat.SharedKernel.MobileHelper.Models;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class MobileQueries : IMobileQueries
    {
        private IAppDbContext _context;
        public MobileQueries(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<DeviceTokenModel> FindDeviceTokenByIdAsync(int deviceTokenId)
        {
            return await _context.DeviceTokens.Where(a => a.DeviceId == deviceTokenId)
                .Select(d => new DeviceTokenModel
                {
                    UserName = d.UserProfile.FullName,
                    DeviceName = d.DeviceName,
                    DeviceTokenValue = d.DeviceTokenValue,
                    DeviceVersion = d.DeviceVersion,
                    Model = d.Model,
                    SourceIP = d.SourceIP,
                    TimeStamp = d.TimeStamp,
                    Android = d.Android
                }).FirstOrDefaultAsync();
        }
        public Dictionary<string, string> GetActivities()
        {
            var activities = _context.Activities
                .Where(x => x.NameAr != null)
             .Where(x => x.ParentID == null)
             .Select(x => new { x.ActivityId, x.NameAr })
             .ToDictionary(x => x.ActivityId.ToString(), x => x.NameAr);
            return activities;
        }
        public async Task<List<Activity>> GetActivitiesObj()
        {
            var activities = await _context.Activities
                .Where(x => x.NameAr != null)
             .Where(x => x.ParentID == null)
             .ToListAsync();
            return activities;
        }
        public async Task<List<int>> GetMainActivites(List<int> activites)
        {
            var mainAct = new List<Activity>();
            await _context.Activities.Where(x => activites.Contains(x.ActivityId))
               .Include(x => x.ParentActivity)
               .ThenInclude(x => x.ParentActivity.ParentActivity)
                .ThenInclude(x => x.ParentActivity.ParentActivity.ParentActivity)
                 .ForEachAsync(x =>
                 {
                     mainAct.Add(x.ParentID == null ? x : x.ParentActivity.ParentID == null ? x.ParentActivity : x.ParentActivity.ParentActivity.ParentID == null ? x.ParentActivity.ParentActivity : x.ParentActivity.ParentActivity.ParentActivity);
                 });
            return mainAct.Select(x => x.ActivityId).Distinct().ToList();
        }
        public async Task<Response<TenderModel>> GetTenders(string tendertype, int page, string title, string ref_no, string ga_head, int main_category, TimeSpan publish_date, bool no_old_tenders)
        {
            SharedKernel.Enums.TenderType tender_Type;
            CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
            switch (tendertype)
            {
                case "public":
                    tender_Type = SharedKernel.Enums.TenderType.CurrentTender;
                    break;
                case "directinvitation":
                    tender_Type = SharedKernel.Enums.TenderType.CurrentDirectPurchase;
                    break;
                default:
                    tender_Type = SharedKernel.Enums.TenderType.CurrentTender;
                    break;
            }

            var tenders = await _context
                .Tenders
                .Where(x => x.TenderStatusId == (int)SharedKernel.Enums.TenderStatus.Approved)
                .WhereIf(!string.IsNullOrEmpty(title), x => x.TenderName.Contains(title))
                .WhereIf(!string.IsNullOrEmpty(ref_no), x => x.ReferenceNumber == ref_no)
                .WhereIf(ga_head != "", x => x.AgencyCode == ga_head)
                .WhereIf(!string.IsNullOrEmpty(tendertype), x => x.TenderTypeId == (int)tender_Type)
                .WhereIf(tender_Type == SharedKernel.Enums.TenderType.CurrentDirectPurchase, x => x.InvitationTypeId == (int)SharedKernel.Enums.InvitationType.Specific)
                .WhereIf(publish_date.TotalSeconds > 0, x => x.SubmitionDate.Value.TimeOfDay == publish_date)
                .Select(x => new TenderModel
                {
                    tid = x.TenderId,
                    title = x.TenderName,
                    ref_no = x.TenderNumber,
                    price = x.ConditionsBookletPrice == null ? 0 : x.ConditionsBookletPrice,
                    price_text = x.ConditionsBookletPrice.HasValue ? x.ConditionsBookletPrice.Value.ToString(arProvider) : "",
                    type = x.TenderType.NameEn,
                    type_text = x.TenderType.NameAr,
                    status = x.Status.NameEn,
                    status_text = x.Status.NameAr,
                    gaoid = x.BranchId,
                    gao_name = x.Branch.BranchName,
                    gahid = x.AgencyCode,
                    gah_name = x.Agency.NameArabic,
                    gah_logo_60 = null,
                    gah_logo_150 = null,
                    description = x.Purpose,
                    approved_date = x.SubmitionDate.HasValue ? x.SubmitionDate.Value.TimeOfDay.Days : 0,
                    approved_date_hijri = x.SubmitionDate.HasValue ? x.SubmitionDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    offer_delivery_date = x.LastOfferPresentationDate.HasValue ? x.LastOfferPresentationDate.Value.TimeOfDay.Days : 0,
                    offer_delivery_date_str = x.LastOfferPresentationDate.HasValue ? x.LastOfferPresentationDate.Value.ToString("dd/MM/yyyy") : "",
                    offer_delivery_time_str = x.LastOfferPresentationDate.HasValue ? x.LastOfferPresentationDate.Value.ToString("hh:mm") : "",
                    offer_delivery_date_hijri = x.LastOfferPresentationDate.HasValue ? x.LastOfferPresentationDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    faq_delivery_date = x.LastEnqueriesDate.HasValue ? x.LastEnqueriesDate.Value.TimeOfDay.Days : 0,
                    faq_delivery_date_str = x.SubmitionDate.HasValue ? x.SubmitionDate.Value.ToString("dd/MM/yyyy") : "",
                    faq_delivery_date_hijri = x.SubmitionDate.HasValue ? x.SubmitionDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    open_env_date = x.OffersOpeningDate.HasValue ? x.OffersOpeningDate.Value.TimeOfDay.Days : 0,
                    open_env_date_str = x.OffersOpeningDate.HasValue ? x.OffersOpeningDate.Value.ToString("dd/MM/yyyy") : "",
                    open_env_time_str = x.OffersOpeningDate.HasValue ? x.OffersOpeningDate.Value.ToString("hh:mm") : "",
                    open_env_date_hijri = x.OffersOpeningDate.HasValue ? x.OffersOpeningDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "",
                    open_env_location = x.OffersOpeningAddress != null ? x.OffersOpeningAddress.AddressName : "",
                    offer_delivery_location = x.SamplesDeliveryAddress ?? "",
                    tender_files_delivery_method_manual = x.SupplierNeedSample,
                    tender_files_delivery_location = "",
                    offer_delivery_days = x.LastOfferPresentationDate.HasValue ? (x.LastOfferPresentationDate.Value - DateTime.Now).Days : 0,
                    offer_delivery_hours = x.LastOfferPresentationDate.HasValue ? ((x.LastOfferPresentationDate.Value - DateTime.Now).Days * 24) - (x.LastOfferPresentationDate.Value - DateTime.Now).Hours : 0,
                    execution_location = x.InsideKSA == true ? "داخل المملكة" : "خارج المملكة",
                    execute_location_text = x.InsideKSA == true ? "داخل المملكة" : "خارج المملكة",
                    taxonomies = x.TenderConstructionWorks.Select(c => c.ConstructionWork.NameEn).Union(x.TenderMaintenanceRunnigWorks.Select(m => m.MaintenanceRunningWork.NameEn)) ?? new List<string>(),
                    news = Array.Empty<string>(),
                    tender_purchase_status_text = "Available online",
                    blocked = false,
                    global_black_listed = false,
                    private_black_listed = false,
                    can_purchase = false,
                    can_ask_for_invitation = false,
                    drafts = new List<string>(),
                    awarding_report = new List<string>(),
                }).ToAPIQueryResult(page);
            return tenders;
        }
        public async Task<List<AgencModel>> GovAgencies()
        {

            var agencies = await _context.GovAgencies
                .Where(x => x.IsDeleted == false && x.IsActive == true)
              .Select(x => new AgencModel
              {
                  gahid = x.AgencyCode,
                  name = x.NameArabic,
                  logo = null

              }).ToListAsync();

            return agencies;
        }
        public async Task<List<int>> FindInterstedTDeviceokensInTenderActivity(List<int> activities)
        {
            var result = await _context.DeviceTokenNotifications.Where(a => a.Status == true && activities.Contains(a.ActivityId))
                .Select(s => s.DeviceId).ToListAsync();
            return result;
        }
        public async Task<List<MobileAlert>> GetPendingMessages()
        {
            var result = await _context.MobileAlerts.Include(c => c.DeviceToken)
                .Where(x => x.IsActive == true && x.MessageStatusId == (int)SharedKernel.Enums.MessageStatus.Pending).ToListAsync();
            return result;
        }
        public FetchDataStatus FetchNotificationsStatus(string deviceToken)
        {
            var result = _context.DeviceTokenNotifications
                 .Where(x => x.DeviceToken.DeviceTokenValue == deviceToken)
                 .Select(x =>
                 new NotificationsStatus(x.DeviceTokenNotificationId.ToString(), x.DeviceId.ToString(), x.ActivityId.ToString(), x.Status == true ? "1" : "0")
                 ).ToList();
            if (result.Count > 0)
                return new FetchDataStatus { data = result, status = "success" };
            return new FetchDataStatus { data = null, status = "Failure" };
        }
    }
}