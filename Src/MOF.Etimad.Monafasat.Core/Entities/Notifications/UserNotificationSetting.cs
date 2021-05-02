using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("UserNotificationSetting", Schema = "Notification")]
    public class UserNotificationSetting : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }

        public int? UserProfileId { get; private set; }

        [ForeignKey(nameof(UserProfileId))]
        public UserProfile User { get; private set; }

        [DefaultValue(true)]
        public bool IsArabic { get; private set; } = true;
        public string OperationCode { get; private set; }

        [ForeignKey(nameof(Supplier))]
        public string Cr { get; private set; }
        public Supplier Supplier { get; private set; }

        public int NotificationCodeId { get; private set; }

        [ForeignKey(nameof(NotificationCodeId))]
        public NotificationOperationCode NotificationOperationCode { get; private set; }

        [DefaultValue(true)]
        public bool Sms { get; private set; }
        [DefaultValue(true)]
        public bool Email { get; private set; }
        [ForeignKey(nameof(UserRoleId))]

        public UserRole UserRole { get; private set; }
        public int UserRoleId { get; private set; }


        public UserNotificationSetting()
        {

        }
        public UserNotificationSetting(string operationCode, string cr, int notificationCodeId, bool sms, bool email, int userRoleId)
        {
            OperationCode = operationCode;
            Cr = cr;
            NotificationCodeId = notificationCodeId;
            Sms = sms;
            Email = email;
            UserRoleId = userRoleId;
            EntityCreated();

        }
        public UserNotificationSetting(string operationCode, int userProfileId, int notificationCodeId, bool sms, bool email, int userRoleId)
        {
            OperationCode = operationCode;
            UserProfileId = userProfileId;
            NotificationCodeId = notificationCodeId;
            Sms = sms;
            Email = email;
            UserRoleId = userRoleId;
            EntityCreated();
        }
        public void Create()
        {
            EntityCreated();
        }
        public void Delete()
        {
            EntityDeleted();
        }

        public void UpdateNotificationSetting(bool emailEnabled, bool _SMSEnabled, bool isAllowedtoEditMail = true, bool isAllowedtoEditSMS = true)
        {

            Email = isAllowedtoEditMail ? emailEnabled : Email;
            Sms = isAllowedtoEditSMS ? _SMSEnabled : Sms;
            EntityUpdated();
        }

        public void SetIdForTest(int id)
        {
            Id = id;
        }

        public void setNotificationOperationCodeForTest()
        {
            NotificationOperationCode  = new NotificationOperationCode("","","","","","","","","",1,1);
            EntityCreated();

        }

    }

}