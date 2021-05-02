using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Notifications
{
    public class UserNotificationSettingTest
    {
        private const int userRoleId = 1;
        private const int userProfileId = 1;
        private const bool sms = true;
        private const bool email = true;
        private const int notificationCodeId = 1;
        private const string operationCode = "01100";
        private const string cr = "00000";
        private const bool emailEnabled = true;
        private const bool _SMSEnabled = true;
        private const bool isAllowedtoEditMail = true;
        private const bool isAllowedtoEditSMS = true;

        [Fact]
        public void Should_Construct_UserNotificationSetting()
        {
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting(operationCode, cr, notificationCodeId, sms, email, userRoleId);
            _ = new UserNotificationSetting();
            _ = userNotificationSetting.User;
            _ = userNotificationSetting.Supplier;
            _ = userNotificationSetting.NotificationOperationCode;
            _ = userNotificationSetting.UserRole;
            userNotificationSetting.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_UserNotificationSetting_Second()
        {
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting(operationCode, userProfileId, notificationCodeId, sms, email, userRoleId);
            _ = new UserNotificationSetting();
            _ = userNotificationSetting.User;
            _ = userNotificationSetting.Supplier;
            _ = userNotificationSetting.NotificationOperationCode;
            _ = userNotificationSetting.UserRole;
            userNotificationSetting.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Create()
        {
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting(operationCode, cr, notificationCodeId, sms, email, userRoleId);
            userNotificationSetting.Create();
            Assert.Equal(ObjectState.Added, userNotificationSetting.State);
        }

        [Fact]
        public void Should_Delete()
        {
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting(operationCode, cr, notificationCodeId, sms, email, userRoleId);
            userNotificationSetting.Delete();
            Assert.Equal(ObjectState.Deleted, userNotificationSetting.State);
        }

        [Fact]
        public void Should_UpdateNotificationSetting()
        {
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting(operationCode, cr, notificationCodeId, sms, email, userRoleId);
            userNotificationSetting.UpdateNotificationSetting(emailEnabled, _SMSEnabled, isAllowedtoEditMail, isAllowedtoEditSMS);
            Assert.True(userNotificationSetting.Email);
            Assert.True(userNotificationSetting.Sms);
        }
    }
}
