using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementDomainService : IAnnouncementDomainService
    {
        public IHttpContextAccessor _httpContextAccessor { get; }
        public IVerificationService _verification { get; }
        public INotificationAppService _notificationAppService { get; }

        public AnnouncementDomainService(IHttpContextAccessor httpContextAccessor, IVerificationService verification, INotificationAppService notificationAppService)
        {
            _httpContextAccessor = httpContextAccessor;
            _verification = verification;
            _notificationAppService = notificationAppService;
        }


        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            return userId;
        }
        public string GetUserFullName()
        {
            var name = _httpContextAccessor.HttpContext.User.FullName();
            return name;
        }

        public async Task CreateVerificationCode(int id)
        {
            var typeId = (int)Enums.VerificationType.Announcement;
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            var email = _httpContextAccessor.HttpContext.User.Email();
            var mobile = _httpContextAccessor.HttpContext.User.Mobile();
            await _verification.CreateVerificationCode(id, mobile, email, userId, typeId);
        }
        public async Task<bool> CheckVerificationCode(int id, string verificationCode, int typeId)
        {
            bool check = await _verification.CheckForVerificationCode(id, verificationCode, typeId);
            return check;
        }


        #region Approve-Annpuncement-Notification

        public async Task SendAnnouncementToApptovementNotification(Announcement announcement)
        {

            NotificationArguments notificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { announcement.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { announcement.ReferenceNumber },
                SMSArgs = new object[] { announcement.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(notificationArguments,
                     $"Announcement/GetAnnouncementDetails?AnnouncementIdString={Util.Encrypt(announcement.AnnouncementId)}",
                     NotificationEntityType.Announcement,
                     announcement.AnnouncementId.ToString(),
                     announcement.BranchId);

            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.Auditor.AnnouncementOperations.SendAnnouncementForApproval, announcement.BranchId.Value, mainNotificationTemplateModel);
        }

        public async Task ApproveAnnouncementNotification(Announcement announcement)
        {

            NotificationArguments notificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { announcement.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { announcement.ReferenceNumber },
                SMSArgs = new object[] { announcement.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(notificationArguments,
                     $"Announcement/GetAnnouncementDetails?AnnouncementIdString={Util.Encrypt(announcement.AnnouncementId)}",
                     NotificationEntityType.Announcement,
                     announcement.AnnouncementId.ToString(),
                     announcement.BranchId);

            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.AnnouncementOperations.ApproveAnnouncement, announcement.BranchId.Value, mainNotificationTemplateModel);
        }

        public async Task RejectApproveAnnouncementNotification(Announcement announcement)
        {

            NotificationArguments notificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { announcement.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { announcement.ReferenceNumber },
                SMSArgs = new object[] { announcement.ReferenceNumber }
            };

            MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(notificationArguments,
                     $"Announcement/GetAnnouncementDetails?AnnouncementIdString={Util.Encrypt(announcement.AnnouncementId)}",
                     NotificationEntityType.Announcement,
                     announcement.AnnouncementId.ToString(),
                     announcement.BranchId);

            await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.AnnouncementOperations.RejectApproveAnnouncement, announcement.BranchId.Value, mainNotificationTemplateModel);
        }

        #endregion Approve-Annpuncement-Notification
    }
}
