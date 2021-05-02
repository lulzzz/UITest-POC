using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{


    [Table("Notification", Schema = "Notification")]
    public abstract class BaseNotification : AuditableEntity
    {
        public BaseNotification()
        {

        }

        [Key]
        public int Id { get; set; }
        public int? UserId { get; protected set; }
        [ForeignKey("UserId")]
        public UserProfile User { get; set; }

        [ForeignKey("Supplier")]
        public string CR { get; protected set; }
        public Supplier Supplier { get; private set; }
        public int NotifacationStatusId { get; set; }
        [ForeignKey("NotifacationStatusId")]
        public NotifacationStatusEntity NotifacationStatusEntity { get; protected set; }

        public DateTime? sendAt { get; protected set; }
        public string Link { get; protected set; }
        public string Key { get; protected set; }

        public int? NotificationSettingId { get; protected set; }
        [ForeignKey(nameof(NotificationSettingId))]
        public UserNotificationSetting NotificationSetting { get; private set; }
    }


    public class NotificationEmail : BaseNotification
    {
        public NotificationEmail()
        {

        }
        public NotificationEmail(int userId, string currentEmail, string title, string content, int? notificationSettingId, string link)
        {
            NotificationSettingId = notificationSettingId;
            UserId = userId;
            Title = title;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unsent;
            Link = link;
            CurrentEmail = currentEmail;
            EntityCreated();

        }
        public NotificationEmail(int userId, string currentEmail, string title, string content, int? notificationSettingId, string link, string key)
        {
            NotificationSettingId = notificationSettingId;
            Key = key;
            UserId = userId;
            Title = title;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unsent;
            Link = link;
            CurrentEmail = currentEmail;
            EntityCreated();

        }
        public NotificationEmail(string cr, string currentEmail, string title, string content, int? notificationSettingId, string link)
        {
            NotificationSettingId = notificationSettingId;
            CR = cr;
            Title = title;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unsent;
            Link = link;
            CurrentEmail = currentEmail;
            EntityCreated();

        }
        public NotificationEmail(string cr, string currentEmail, string title, string content, int? notificationSettingId, string link, string key)
        {
            NotificationSettingId = notificationSettingId;
            Key = key;
            CR = cr;
            Title = title;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unsent;
            Link = link;
            CurrentEmail = currentEmail;
            EntityCreated();

        }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public string CurrentEmail { get; set; }

        public void Update(NotifacationStatus Status)
        {
            if (Status == NotifacationStatus.Send)
                sendAt = DateTime.Now;
            NotifacationStatusId = (int)Status;
            EntityUpdated();
        }
        public void Update(string title, string content)
        {
            Title = title;
            Content = content;
            this.State = SharedKernal.ObjectState.Modified;

        }


    }


    public class NotificationSMS : BaseNotification
    {
        public NotificationSMS()
        {

        }
        public NotificationSMS(int userId, string currentMobile, string content, int? notificationSettingId, string link)
        {
            NotificationSettingId = notificationSettingId;
            UserId = userId;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unsent;
            Link = link;
            CurrentMobile = currentMobile;
            EntityCreated();

        }
        public NotificationSMS(int userId, string currentMobile, string content, int? notificationSettingId, string link, string key)
        {
            NotificationSettingId = notificationSettingId;
            Key = key;
            UserId = userId;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unsent;
            Link = link;
            CurrentMobile = currentMobile;
            EntityCreated();

        }

        public NotificationSMS(string cr, string currentMobile, string content, int? notificationSettingId, string link)
        {
            NotificationSettingId = notificationSettingId;
            CR = cr;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unsent;
            Link = link;
            CurrentMobile = currentMobile;
            EntityCreated();

        }
        public NotificationSMS(string cr, string currentMobile, string content, int? notificationSettingId, string link, string key)
        {
            NotificationSettingId = notificationSettingId;
            Key = key;
            CR = cr;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unsent;
            Link = link;
            CurrentMobile = currentMobile;
            EntityCreated();

        }

        public string Content { get; set; }
        public string CurrentMobile { get; set; }

        public void Update(NotifacationStatus Status)
        {
            if (Status == NotifacationStatus.Send)
                sendAt = DateTime.Now;
            NotifacationStatusId = (int)Status;
            EntityUpdated();
        }
        public void Update(string content)
        {
            Content = content;
            EntityUpdated();
        }

    }


    public class NotificationPanel : BaseNotification
    {
        public NotificationPanel()
        {

        }


        public NotificationPanel(int userId, string title, string content, int notificationSettingId, string link, int? branchId, int? committeeId)
        {
            NotificationSettingId = notificationSettingId;
            UserId = userId;
            Title = title;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unread;
            Link = link;
            BranchId = branchId;
            CommitteeId = committeeId;
            EntityCreated();

        }
        public NotificationPanel(int userId, string title, string content, int? notificationSettingId, string link, int? branchId, int? committeeId, string key)
        {
            NotificationSettingId = notificationSettingId;
            Key = key;
            UserId = userId;
            Title = title;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unread;
            Link = link;
            BranchId = branchId;
            CommitteeId = committeeId;
            EntityCreated();

        }

        public NotificationPanel(string cr, string title, string content, int? notificationSettingId, string link)
        {
            NotificationSettingId = notificationSettingId;
            CR = cr;
            Title = title;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unread;
            Link = link;
            EntityCreated();

        }
        public NotificationPanel(string cr, string title, string content, int? notificationSettingId, string link, int? branchId, int? committeeId, string key)
        {
            NotificationSettingId = notificationSettingId;
            BranchId = branchId;
            CommitteeId = committeeId;
            Key = key;
            CR = cr;
            Title = title;
            Content = content;
            NotifacationStatusId = (int)NotifacationStatus.Unread;
            Link = link;
            EntityCreated();

        }

        public string Title { get; set; }
        public string Content { get; set; }
        public int? BranchId { get; protected set; }
        public int? CommitteeId { get; protected set; }
        public void Update(string title, string content)
        {
            Title = title;
            Content = content;

        }
        public void Update(NotifacationStatus Status)
        {
            if (Status == NotifacationStatus.Read)
                sendAt = DateTime.Now;

            NotifacationStatusId = (int)Status;
            EntityUpdated();
        }

    }

}