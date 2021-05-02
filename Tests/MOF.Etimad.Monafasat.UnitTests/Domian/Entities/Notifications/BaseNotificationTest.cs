using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Notifications
{
    public class BaseNotificationTest
    {
        private const int userId = 1;
        private const string currentEmail = "mail";
        private const string title = "title";
        private const string content = "content";
        private const string link = "link";
        private const int notificationSettingId = 1;
        private const string key = "key";
        private const string cr = "00000";
        private const string currentMobile = "00000";
        private const int branchId = 1;
        private const int committeeId = 1;

        [Fact]
        public void Should_Construct_NotificationEmail()
        {
            NotificationEmail notificationEmail = new NotificationEmail(userId, currentEmail, title, content, notificationSettingId, link);
            var notification = new NotificationEmail();
            _ = notification.Id;
            _ = notification.User;
            _ = notification.Supplier;
            _ = notification.NotifacationStatusEntity;
            _ = notification.NotificationSetting;

            notificationEmail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationEmail_Second()
        {
            NotificationEmail notificationEmail = new NotificationEmail(userId, currentEmail, title, content, notificationSettingId, link, key);
            notificationEmail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationEmail_Third()
        {
            NotificationEmail notificationEmail = new NotificationEmail(cr, currentEmail, title, content, notificationSettingId, link);
            notificationEmail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationEmail_fourth()
        {
            NotificationEmail notificationEmail = new NotificationEmail(cr, currentEmail, title, content, notificationSettingId, link, key);
            notificationEmail.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update_Email()
        {
            NotificationEmail notificationEmail = new NotificationEmail(cr, currentEmail, title, content, notificationSettingId, link, key);
            notificationEmail.Update(NotifacationStatus.Send);
            Assert.Equal((int)NotifacationStatus.Send, notificationEmail.NotifacationStatusId);
        }

        [Fact]
        public void Should_Update_Email_Second()
        {
            NotificationEmail notificationEmail = new NotificationEmail(cr, currentEmail, title, content, notificationSettingId, link, key);
            notificationEmail.Update(title, content);
            Assert.Equal(title, notificationEmail.Title);
        }

        [Fact]
        public void Should_Construct_NotificationSMS()
        {
            NotificationSMS notificationSMS = new NotificationSMS(userId, currentMobile, content, notificationSettingId, link);
            _ = new NotificationSMS();

            notificationSMS.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationSMS_Second()
        {
            NotificationSMS notificationSMS = new NotificationSMS(userId, currentMobile, content, notificationSettingId, link, key);
            notificationSMS.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationSMS_Third()
        {
            NotificationSMS notificationSMS = new NotificationSMS(cr, currentMobile, content, notificationSettingId, link);
            notificationSMS.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationSMS_Fourth()
        {
            NotificationSMS notificationSMS = new NotificationSMS(cr, currentMobile, content, notificationSettingId, link, key);
            notificationSMS.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update_SMS()
        {
            NotificationSMS notificationSMS = new NotificationSMS(userId, currentMobile, content, notificationSettingId, link);
            notificationSMS.Update(NotifacationStatus.Send);
            Assert.Equal((int)NotifacationStatus.Send, notificationSMS.NotifacationStatusId);
        }

        [Fact]
        public void Should_Update_SMS_Second()
        {
            NotificationSMS notificationSMS = new NotificationSMS(userId, currentMobile, content, notificationSettingId, link);
            notificationSMS.Update(content);
            Assert.Equal(content, notificationSMS.Content);
        }

        [Fact]
        public void Should_Construct_NotificationPanel()
        {
            NotificationPanel notificationPanel = new NotificationPanel(userId, title, content, notificationSettingId, link, branchId, committeeId);
            _ = new NotificationPanel();

            notificationPanel.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationPanel_Second()
        {
            NotificationPanel notificationPanel = new NotificationPanel(userId, title, content, notificationSettingId, link, branchId, committeeId, key);
            notificationPanel.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationPanel_Third()
        {
            NotificationPanel notificationPanel = new NotificationPanel(cr, title, content, notificationSettingId, link);
            notificationPanel.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_NotificationPanel_Fourth()
        {
            NotificationPanel notificationPanel = new NotificationPanel(cr, title, content, notificationSettingId, link, branchId, committeeId, key);
            notificationPanel.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update_NotificationPanel()
        {
            NotificationPanel notificationPanel = new NotificationPanel(userId, title, content, notificationSettingId, link, branchId, committeeId);
            notificationPanel.Update(NotifacationStatus.Send);
            Assert.Equal((int)NotifacationStatus.Send, notificationPanel.NotifacationStatusId);
        }
    }
}
