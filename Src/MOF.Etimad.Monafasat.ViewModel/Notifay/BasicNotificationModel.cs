using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;

namespace MOF.Etimad.Monafasat
{
    public class BasicNotificationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserProfileModel User { get; set; }
        public int NotifacationStatusId { get; set; }
        public Enums.NotifacationStatus NotifacationStatusEntity { get; set; }
        public DateTime? sendAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Link { get; set; }
        public string DisplayDateOrigial { get; set; }
        public string DisplayDate { get; set; }
    }
    public class NotificationEmailModel : BasicNotificationModel
    {
        public NotificationEmailModel()
        {

        }

        public string Title { get; set; }
        public string Content { get; set; }

    }
    public class NotificationSMSModel : BasicNotificationModel
    {
        public NotificationSMSModel()
        {

        }

        public string Content { get; set; }

    }
    public class NotificationPanelModel : BasicNotificationModel
    {
        public NotificationPanelModel()
        {
        }

        public string Title { get; set; }
        public string Content { get; set; }


    }

}
