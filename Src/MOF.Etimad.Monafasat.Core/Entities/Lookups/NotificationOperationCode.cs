using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("NotificationOperationCode", Schema = "LookUps")]
    public class NotificationOperationCode : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NotificationOperationCodeId { get; private set; }
        [StringLength(100)]
        public string OperationCode { get; private set; }
        [StringLength(2000)]
        public string ArabicName { get; private set; }
        [StringLength(2000)]
        public string EnglishName { get; private set; }
        public int UserRoleId { get; private set; }
        [ForeignKey(nameof(UserRoleId))]
        public UserRole UserRole { get; private set; }
        [DefaultValue(true)]
        public bool DefaultSMS { get; private set; }
        [DefaultValue(true)]
        public bool DefaultEmail { get; private set; }
        [DefaultValue(true)]
        public bool CanEditEmail { get; private set; }
        [DefaultValue(true)]
        public bool CanEditSMS { get; private set; }
        public int? NotificationCategoryId { get; private set; }
        [StringLength(2000)]
        public string SmsTemplateAr { get; private set; }
        [StringLength(2000)]
        public string SmsTemplateEn { get; private set; }
        [StringLength(2000)]
        public string EmailSubjectTemplateAr { get; private set; }
        [StringLength(2000)]
        public string EmailSubjectTemplateEn { get; private set; }
        [StringLength(10000)]
        public string EmailBodyTemplateAr { get; private set; }
        [StringLength(10000)]
        public string EmailBodyTemplateEn { get; private set; }
        [StringLength(1000)]
        public string PanelTemplateAr { get; private set; }
        [StringLength(1000)]
        public string PanelTemplateEn { get; private set; }
        [ForeignKey(nameof(NotificationCategoryId))]
        public NotificationCategory NotificationCategory { get; private set; }

        public NotificationOperationCode()
        {

        }
        public NotificationOperationCode(string OpCode, string arName, string enName, string panelAr, string panelEn, string emailAr, string emailEn, string smsAr, string smsEn, int catId, int roleId)
        {
            OperationCode = OpCode;
            ArabicName = arName;
            EnglishName = enName;
            PanelTemplateAr = panelAr;
            PanelTemplateEn = panelEn;
            EmailBodyTemplateAr = emailAr;
            EmailBodyTemplateEn = emailEn;
            SmsTemplateEn = smsEn;
            SmsTemplateAr = smsAr;
            NotificationCategoryId = catId;
            UserRoleId = roleId;
            DefaultSMS = false;
            DefaultEmail = true;
            CanEditSMS = true;
            CanEditEmail = false;
            EntityCreated();

        }
        public void UpdateNotificationOperationCode(string OpCode, string arName, string enName, string panelAr, string panelEn, string emailAr, string emailEn, string smsAr, string smsEn, int catId, int roleId)
        {
            OperationCode = OpCode;
            ArabicName = arName;
            EnglishName = enName;
            PanelTemplateAr = panelAr;
            PanelTemplateEn = panelEn;
            EmailBodyTemplateAr = emailAr;
            EmailBodyTemplateEn = emailEn;
            SmsTemplateEn = smsEn;
            SmsTemplateAr = smsAr;
            NotificationCategoryId = catId;
            UserRoleId = roleId;
            DefaultSMS = false;
            DefaultEmail = true;
            CanEditSMS = true;
            CanEditEmail = false;
            EntityUpdated();

        }

        public void setNotificationOperationCodeIdForTest()
        {
            NotificationOperationCodeId = 1;
        }
    }


}
