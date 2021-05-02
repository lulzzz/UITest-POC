using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;


namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Notifications
{
    public class UserProfileTest
    {
        private const int userId = 1;
        private const string userName = "name";
        private const string fullName = "fullName";
        private const string mobile = "00111000";
        private const string email = "email";
        private const string defaultUserRole = "role";

        private const int userRoleId = 1;
        private const int id = 1;
        private const bool sms = true;
        private const bool isEmail = true;
        private const int notificationCodeId = 3;
        private const string operationCode = "01100";
        private const string cr = "00000";

        [Fact]
        public void Should_Construct_UserProfile()
        {
            UserProfile userProfile = new UserProfile(userId, userName, fullName, mobile, email, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            _ = new UserProfile();
            _ = userProfile.Notifications;
            _ = userProfile.UserAgencyRoles;
            _ = userProfile.SupplierUserProfiles;
            _ = userProfile.CommitteeUsers;
            _ = userProfile.BranchUsers;
            _ = userProfile.RowVersion;

            userProfile.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateDefaultUserRole()
        {
            UserProfile userProfile = new UserProfile(userId, userName, fullName, mobile, email, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            userProfile.UpdateDefaultUserRole(defaultUserRole);
            Assert.Equal(defaultUserRole, userProfile.DefaultUserRole);
        }

        [Fact]
        public void Should_AddNotificationSettings()
        {
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting(operationCode, cr, notificationCodeId, sms, isEmail, userRoleId);
            UserProfile userProfile = new UserProfile(userId, userName, fullName, mobile, email, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            userProfile.AddNotificationSettings(new List<UserNotificationSetting>() { userNotificationSetting });
            Assert.NotEmpty(userProfile.NotificationSetting);
        }

        [Fact]
        public void Should_AddMissingNotificationSettings()
        {
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting(operationCode, cr, notificationCodeId, sms, isEmail, userRoleId);
            UserProfile userProfile = new UserProfile(userId, userName, fullName, mobile, email, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            userProfile.AddMissingNotificationSettings(new List<UserNotificationSetting>() { userNotificationSetting });
            Assert.NotEmpty(userProfile.NotificationSetting);
        }

        [Fact]
        public void Should_Update()
        {
            UserProfile userProfile = new UserProfile(userId, userName, fullName, mobile, email, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            userProfile.Update(fullName, userName, email, mobile);
            Assert.Equal(fullName, userProfile.FullName);
        }



        [Fact]
        public void Should_RestPermission()
        {
            UserProfile userProfile = new UserProfile(userId, userName, fullName, mobile, email, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            userProfile.BranchUsers = new List<BranchUser>() { new BranchUser() };
            userProfile.CommitteeUsers = new List<CommitteeUser>() { new CommitteeUser() };
            userProfile.RestPermission();
            Assert.False(userProfile.CommitteeUsers[0].IsActive);
        }

        [Fact]
        public void Should_SetNotificationSettings()
        {
            UserProfile userProfile = new UserProfile(userId, userName, fullName, mobile, email, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            userProfile.SetNotificationSettings(new List<UserNotificationSetting>() { new UserNotificationSetting() });
            Assert.NotEmpty(userProfile.NotificationSetting);
        }
    }
}
