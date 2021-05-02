using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class NotificationOperationCodeTest
    {
        private const string OpCode = "name";
        private const string arName = "name";
        private const string enName = "name";
        private const string panelAr = "name";
        private const string panelEn = "name";
        private const string emailAr = "name";
        private const string emailEn = "name";
        private const string smsAr = "name";
        private const string smsEn = "name";
        private const int catId = 1;
        private const int roleId = 1;


        [Fact]
        public void Should_Construct_NotificationOperationCode()
        {
            NotificationOperationCode notificationOperationCode = new NotificationOperationCode(OpCode, arName, enName, panelAr, panelEn, emailAr, emailEn, smsAr, smsEn, catId, roleId);
            _ = new NotificationOperationCode();
            _ = notificationOperationCode.NotificationOperationCodeId;
            _ = notificationOperationCode.UserRole;
            _ = notificationOperationCode.EmailSubjectTemplateAr;
            _ = notificationOperationCode.EmailSubjectTemplateEn;
            _ = notificationOperationCode.NotificationCategory;

            notificationOperationCode.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateNotificationOperationCode()
        {
            NotificationOperationCode notificationOperationCode = new NotificationOperationCode(OpCode, arName, enName, panelAr, panelEn, emailAr, emailEn, smsAr, smsEn, catId, roleId);
            notificationOperationCode.UpdateNotificationOperationCode(OpCode, arName, enName, panelAr, panelEn, emailAr, emailEn, smsAr, smsEn, catId, roleId);
            notificationOperationCode.ShouldNotBeNull();
        }
    }
}
