using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class NotificationQueries : INotificationQueries
    {
        private readonly IAppDbContext _context;

        public NotificationQueries(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<List<UserCategoryNotificatioSettingModel>> FindUserNotificationSetting(int userId, string cr, Enums.UserRole userType)
        {
            var result = await _context.UserNotificationSettings
                .WhereIf(userType.ToString() == RoleNames.supplier, x => x.Cr == cr)
                .WhereIf(userType.ToString() != RoleNames.supplier, x => x.UserProfileId == userId && x.UserRoleId == (int)userType)
                .Select(d => new UserNotificatioSettingModel
                {
                    Id = Util.Encrypt(d.Id),
                    CanEditEmail = d.NotificationOperationCode.CanEditEmail,
                    CanEditMessage = d.NotificationOperationCode.CanEditSMS,
                    EmailEnabled = d.Email,
                    categoryName = (CultureInfo.CurrentCulture.Name == "ar-SA" ? d.NotificationOperationCode.NotificationCategory.ArabicName : d.NotificationOperationCode.NotificationCategory.EnglishName),
                    SMSEnabled = d.Sms,
                    OperationName = (CultureInfo.CurrentCulture.Name == "ar-SA" ? d.NotificationOperationCode.ArabicName : d.NotificationOperationCode.EnglishName),
                    IsArabic = d.IsArabic,
                    UserIdString = Util.Encrypt(userId)
                }).ToListAsync();
            var results = result.GroupBy(p => p.categoryName)
            .Select(g => new UserCategoryNotificatioSettingModel { categoryName = g.Key, userNotificatioSettingModels = g.ToList() }).ToList();
            return results;
        }

        public async Task<List<UserNotificationSetting>> GetNotificationSettingByCrAndOperationCode(List<string> cr, int notificationCodeId)
        {
            var result = await _context.UserNotificationSettings.Include(s => s.Supplier)
                    .Where(x => x.NotificationCodeId == notificationCodeId && cr.Contains(x.Cr))
                    .ToListAsync();
            return result;
        }

        public async Task<List<UserNotificationSetting>> GetNotificationSettingByUserIdAndUserType(int notificationCodeId, int branchId, int committeeId)
        {
            var result = await _context.UserNotificationSettings.Include(x => x.User)
                    .WhereIf(branchId != 0, x => x.User.BranchUsers.Any(b => b.BranchId == branchId && x.UserRoleId == b.UserRoleId && b.IsActive == true && b.Branch.IsActive == true))
                    .WhereIf(committeeId != 0, x => x.User.CommitteeUsers.Any(c => c.CommitteeId == committeeId && x.UserRoleId == c.UserRoleId && c.IsActive == true && c.Committee.IsActive == true))
                    .Where(x => x.NotificationCodeId == notificationCodeId)
                    .ToListAsync();
            return result;
        }

        public async Task<List<UserNotificationSetting>> GetNotificationSettingsByCodeId(int notificationCodeId)
        {
            var result = await _context.UserNotificationSettings.Include(x => x.User)
                    .Where(x => x.NotificationCodeId == notificationCodeId)
                    .ToListAsync();
            return result;
        }
        public async Task<List<UserNotificationSetting>> GetDefaultSettingByUserType(Enums.UserRole userType)
        {
            var result = await _context.NotificationOperationCodes
                    .Where(x => x.UserRoleId == (int)userType)
                    .Select(x => new UserNotificationSetting(x.OperationCode, 0, x.NotificationOperationCodeId, x.DefaultSMS, x.DefaultEmail, (int)userType)).ToListAsync();
            return result;
        }
        public async Task<NotificationOperationCode> GetDefaultSettingByCodeId(int CodeId)
        {
            var result = await _context.NotificationOperationCodes
                    .Where(x => x.NotificationOperationCodeId == CodeId)
                    .Select(x => x).FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<UserNotificationSetting>> GetDefaultSettingByCr()
        {
            var result = await _context.NotificationOperationCodes
                    .Where(x => x.UserRoleId == (int)Enums.UserRole.NewMonafasat_Supplier)
                    .Select(x => new UserNotificationSetting(x.OperationCode, null, x.NotificationOperationCodeId, x.DefaultSMS, x.DefaultEmail, (int)Enums.UserRole.NewMonafasat_Supplier)).ToListAsync();
            return result;
        }
        public async Task<List<UserNotificationSetting>> GetDefaultSettingByUserTypes(List<int> userRoleIds)
        {
            var result = await _context.NotificationOperationCodes
                    .Where(x => userRoleIds.Contains(x.UserRoleId) && x.UserRoleId != (int)Enums.UserRole.NewMonafasat_Supplier)
                    .Select(x => new UserNotificationSetting(x.OperationCode, 0, x.NotificationOperationCodeId, x.DefaultSMS, x.DefaultEmail, x.UserRoleId)).ToListAsync();
            return result;
        }
        public async Task<List<NotificationOperationCode>> FindAllNotificationOperationCode()
        {
            var result = await _context.NotificationOperationCodes.ToListAsync();
            return result;
        }
        public async Task<NotificationOperationCode> FindNotificationOperationCode(int Id)
        {
            var result = await _context.NotificationOperationCodes.FirstOrDefaultAsync(d => d.NotificationOperationCodeId == Id);
            return result;
        }

        public async Task<UserProfile> GetUser(int userId)
        {
            try
            {
                var result = await _context.UserProfiles
                          .Where(x => x.Id == userId)
                          .Include("NotificationSetting.NotificationOperationCode")
                          .AsNoTracking()
                          .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception D)
            {
                throw D;
            }

        }

        public async Task<Supplier> GetSupplierUser(string cr)
        {

            var result = await _context.Suppliers
                      .Where(x => x.SelectedCr == cr)
                      .Include("NotificationSetting.NotificationOperationCode")
                      .AsNoTracking()
                      .FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<NotificationPanelModel>> GetNotificationPanel(string userRoleName, int userId, int branchId, int committeeId)
        {

            var result = await _context.PanelNotifications
             .Where(x => x.UserId == userId)
             .WhereIf(branchId != 0, x => x.BranchId == branchId)
             .WhereIf(committeeId != 0, x => x.CommitteeId == committeeId || (x.CommitteeId == null && x.NotificationSetting.UserRole.UserRoleId == (int)Enums.UserRole.CR_DirectPurchaseMember))
             .Where(x => x.Content != "" && x.Content != null)
            .Where(x => x.NotificationSetting.UserRole.Name == userRoleName || (x.CommitteeId == null && x.NotificationSetting.UserRole.UserRoleId == (int)Enums.UserRole.CR_DirectPurchaseMember))
             .OrderByDescending(x => x.Id)
             .Skip(0)
             .Take(10)
             .Select(n => new NotificationPanelModel
             {
                 Link = "/" + n.Link,
                 NotifacationStatusId = n.NotifacationStatusId,
                 Title = n.Title,
                 DisplayDate = n.CreatedAt.ToString("hh:mm:ss yyyy-MM-dd", new CultureInfo("en-US"))
             })
             .ToListAsync();
            return result;
        }
        public async Task<QueryResult<NotificationPanelModel>> GetAllNotificationsAsync(string userRoleName, int userId, int branchId, int committeeId, SearchCriteria criteria)
        {
            var result = await _context.PanelNotifications
              .Where(x => x.UserId == userId)
              .WhereIf(branchId != 0, x => x.BranchId == branchId)
              .WhereIf(committeeId != 0, x => x.CommitteeId == committeeId || (x.CommitteeId == null && x.NotificationSetting.UserRole.UserRoleId == (int)Enums.UserRole.CR_DirectPurchaseMember))
              .Where(x => !string.IsNullOrEmpty(x.Content))
              .Where(x => x.NotificationSetting.UserRole.Name == userRoleName || (x.CommitteeId == null && x.NotificationSetting.UserRole.UserRoleId == (int)Enums.UserRole.CR_DirectPurchaseMember))
              .OrderByDescending(x => x.Id)
                                          .Select(n => new NotificationPanelModel
                                          {
                                              Link = "/account/SetReadStateToNotification?notificationId=" + n.Id + "&Link=/" + n.Link,
                                              NotifacationStatusId = n.NotifacationStatusId,
                                              Title = n.Title,
                                              DisplayDate = n.CreatedAt.ToString("hh:mm:ss yyyy-MM-dd", new CultureInfo("en-US"))
                                          })
              .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }
        public async Task<BaseNotification> GetNotificationById(int notificationId)
        {
            var result = await _context.Notifications
              .Where(x => x.Id == notificationId)
              .FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> GetNotificationPanelCount(int userId, int branchId)
        {

            var result = await _context.PanelNotifications
            .Where(x => x.UserId == userId && x.NotifacationStatusId == (int)Enums.NotifacationStatus.Unread)
            .WhereIf(branchId != 0, x => x.BranchId == branchId)
            .CountAsync();
            return result;
        }
        public async Task<int> GetNotificationPanelCountForSupplier(string cr)
        {
            var result = await _context.PanelNotifications.Where(x => x.NotifacationStatusId == (int)NotifacationStatus.Unread && x.CR == cr).CountAsync();
            return result;
        }
        public async Task<OperationCodeViewModel> GetOperationCodeDetails(int Id)
        {
            var result = await _context.NotificationOperationCodes
              .Where(x => x.NotificationOperationCodeId == Id).Select(d => new OperationCodeViewModel
              {

                  ArabicName = d.ArabicName,
                  EmailBodyTemplateAr = d.EmailBodyTemplateAr,
                  EmailBodyTemplateEn = d.EmailBodyTemplateEn,
                  EnglishName = d.EnglishName,
                  IdString = Util.Encrypt(d.NotificationOperationCodeId),
                  NotificationCategoryId = d.NotificationCategoryId.HasValue ? d.NotificationCategoryId.Value : 0,
                  OperationCode = d.OperationCode,
                  PanelTemplateAr = d.PanelTemplateAr,
                  PanelTemplateEn = d.PanelTemplateEn,
                  SmsTemplateAr = d.SmsTemplateAr,
                  SmsTemplateEn = d.SmsTemplateEn,
                  UserRoleId = d.UserRoleId

              }).FirstOrDefaultAsync()
;
            return result;
        }

        public async Task<List<BaseNotification>> GetNewNotification()
        {
            return await _context.Notifications
                .Where(x => x is NotificationEmail || x is NotificationSMS)
                .Where(x => x.NotifacationStatusId == (int)NotifacationStatus.Unsent).OrderBy(d => d.CreatedAt).Take(1000)
                .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<List<NotificationPanelModel>> GetNotificationPanelForCr(string cr)
        {

            var result = await _context.PanelNotifications
             .Where(x => x.CR == cr)
              .OrderByDescending(x => x.CreatedAt)
             .Skip(0)
             .Take(10)
             .Select(n => new NotificationPanelModel
             {
                 Link = "/account/SetReadStateToNotification?notificationId=" + n.Id + "&Link=/" + n.Link,
                 NotifacationStatusId = n.NotifacationStatusId,
                 Title = n.Title,
                 DisplayDate = n.CreatedAt.ToString("hh:mm:ss yyyy-MM-dd", new CultureInfo("en-US"))
             })
             .ToListAsync();
            return result;
        }
        public async Task<QueryResult<NotificationPanelModel>> GetAllNotificationsForCrAsync(string cr, SearchCriteria criteria)
        {

            var result = await _context.PanelNotifications
              .Where(x => x.CR == cr)
               .OrderByDescending(x => x.CreatedAt)
              .Select(n => new NotificationPanelModel
              {
                  Link = "/account/SetReadStateToNotification?notificationId=" + n.Id + "&Link=/" + n.Link,
                  NotifacationStatusId = n.NotifacationStatusId,
                  Title = n.Title,
                  DisplayDate = n.CreatedAt.ToString("hh:mm:ss yyyy-MM-dd", new CultureInfo("en-US"))
              })
              .ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }
        public async Task<QueryResult<TenderNotificationStatus>> GetNotificationStatusReport(TenderNotificationSearchCriteria criteria)
        {
            if (!string.IsNullOrEmpty(criteria.Email))
            {
                criteria.Email = criteria.Email.Trim();
            }

            var result = await _context.Notifications.Where(x => x.Key == criteria.Key && x is NotificationEmail)
                .WhereIf(criteria.Email != null, x => ((NotificationEmail)x).CurrentEmail == criteria.Email)
                .WhereIf(criteria.SendingStatusId != 0, x => x.NotifacationStatusId == criteria.SendingStatusId)
                .WhereIf(criteria.OperationCode != null, x => x.NotificationSetting.NotificationOperationCode.OperationCode == criteria.OperationCode)
                .Select(x => new TenderNotificationStatus
                {
                    Address = x.NotificationSetting.NotificationOperationCode.EmailSubjectTemplateAr,
                    Consignee = x.Supplier != null ? x.Supplier.SelectedCrName : x.User != null ? x.User.FullName : "",
                    Email = ((NotificationEmail)x).CurrentEmail,
                    SendAs = x.NotificationSetting.NotificationOperationCode.ArabicName,
                    SendAt = x.sendAt,
                    SendStatus = x.NotifacationStatusEntity.Name,
                    TenderName = "",
                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;

        }
        public List<OperationCodeModel> GetOperationCode()
        {
            var result = _context.NotificationOperationCodes.Select(r => new OperationCodeModel { OperationCode = r.OperationCode, OperationCodeArabic = r.ArabicName }).Distinct().ToList();
            return result;
        }
        public Task<QueryResult<OperationCodeViewModel>> GetNotificationCodes(OperationCodeSearchCriteria criteria)
        {
            var result = _context.NotificationOperationCodes.WhereIf(criteria.roleId != 0, x => x.UserRoleId == criteria.roleId)
                .OrderBy(d => d.UserRoleId)
                .Select(x => new OperationCodeViewModel
                {
                    IdString = Util.Encrypt(x.NotificationOperationCodeId),
                    OperationCode = x.OperationCode,
                    roleName = x.UserRole.DisplayNameAr,
                    groupName = x.NotificationCategory.ArabicName,
                    EnglishName = x.EnglishName,
                    ArabicName = x.ArabicName

                }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }
        public List<NotificationStatusModel> GetNotificationStatus()
        {
            var result = _context.NotifacationStatusEntities.Select(r => new NotificationStatusModel { NotificationStatusId = r.NotifacationStatusEntityId, NotificationStatusName = r.Name }).Where(r => r.NotificationStatusId <= 3).ToList();
            return result;
        }
        public async Task<List<UserNotificationSetting>> GetNotificationSettingByRoleAndOperationCode(List<int> userIds, int operationCodeId, int branchId = 0, int? committeeId = 0)
        {
            var result = await _context.UserNotificationSettings.Include(x => x.User)
                .WhereIf(branchId != 0 && branchId != null, x => x.User.BranchUsers.Any(b => b.BranchId == branchId && x.UserRoleId == b.UserRoleId && b.IsActive == true && b.Branch.IsActive == true))
                .WhereIf(committeeId != 0 && committeeId != null, x => x.User.CommitteeUsers.Any(c => c.CommitteeId == committeeId && x.UserRoleId == c.UserRoleId && c.IsActive == true && c.Committee.IsActive == true))
                .Where(x => userIds.Contains(x.UserProfileId.Value) && x.NotificationCodeId == operationCodeId).ToListAsync();
            return result;
        }
        public async Task<List<UserNotificationSetting>> FindUserNotificationSettingbyUserProfileId(int userId)
        {

            var result = await _context.UserNotificationSettings.Where(f => f.UserProfileId == userId && f.IsActive == true).ToListAsync();
            return result;
        }

        public async Task<UserNotificationSetting> GetNotificationSettingByUserId(int notificationCodeId, int userId)
        {
            var result = await _context.UserNotificationSettings.Include(x => x.User)
                    .Where(x => x.NotificationCodeId == notificationCodeId && x.UserProfileId == userId)
                    .FirstOrDefaultAsync();
            return result;
        }
    }
}
