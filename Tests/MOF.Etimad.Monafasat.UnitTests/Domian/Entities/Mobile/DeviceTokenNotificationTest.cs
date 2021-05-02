using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Mobile
{
    public class DeviceTokenNotificationTest
    {
        private const int deviceId = 1;
        private const int activityId = 1;
        private const bool deviceNotificationStatus = true;

        [Fact]
        public void Should_Construct_DeviceTokenNotification()
        {
            DeviceTokenNotification deviceTokenNotification = new DeviceTokenNotification(activityId, deviceNotificationStatus);
            _ = new DeviceTokenNotification();
            _ = deviceTokenNotification.DeviceTokenNotificationId;
            _ = deviceTokenNotification.DeviceToken;
            _ = deviceTokenNotification.Activity;

            deviceTokenNotification.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_DeviceTokenNotification_Second()
        {
            DeviceTokenNotification deviceTokenNotification = new DeviceTokenNotification(deviceId, activityId, deviceNotificationStatus);
            deviceTokenNotification.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            DeviceTokenNotification deviceTokenNotification = new DeviceTokenNotification(deviceId, activityId, deviceNotificationStatus);

            deviceTokenNotification.Update(deviceNotificationStatus);
            Assert.True(deviceTokenNotification.Status);
        }

        [Fact]
        public void Should_Update_Second()
        {
            DeviceTokenNotification deviceTokenNotification = new DeviceTokenNotification(deviceId, activityId, deviceNotificationStatus);

            deviceTokenNotification.Update(deviceId, activityId);
            Assert.Equal(deviceId, deviceTokenNotification.DeviceId);
            Assert.Equal(activityId, deviceTokenNotification.ActivityId);
        }

        [Fact]
        public void Should_Update_Third()
        {
            DeviceTokenNotification deviceTokenNotification = new DeviceTokenNotification(deviceId, activityId, deviceNotificationStatus);

            deviceTokenNotification.Update(deviceId, activityId, deviceNotificationStatus);
            Assert.Equal(deviceId, deviceTokenNotification.DeviceId);
            Assert.Equal(activityId, deviceTokenNotification.ActivityId);
            Assert.True(deviceTokenNotification.Status);
        }

        [Fact]
        public void Should_DeActive()
        {
            DeviceTokenNotification deviceTokenNotification = new DeviceTokenNotification(deviceId, activityId, deviceNotificationStatus);

            deviceTokenNotification.DeActive();
            Assert.False(deviceTokenNotification.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            DeviceTokenNotification deviceTokenNotification = new DeviceTokenNotification(deviceId, activityId, deviceNotificationStatus);

            deviceTokenNotification.SetActive();
            Assert.True(deviceTokenNotification.IsActive);
        }
    }
}
