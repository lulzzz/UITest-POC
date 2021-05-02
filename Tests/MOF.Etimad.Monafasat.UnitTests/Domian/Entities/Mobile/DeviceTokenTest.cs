using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Mobile
{
    public class DeviceTokenTest
    {
        private const int userProfileId = 1;
        private const string deviceTokenValue = "val";
        private const string deviceVersion = "version";
        private const string deviceName = "name";
        private const string model = "model";
        private const string userDeviceId = "1";
        private const int timeStamp = 1;
        private const string sourceIP = "1";
        private const int android = 1;

        [Fact]
        public void Should_Construct_DeviceToken()
        {
            DeviceToken deviceToken = new DeviceToken(userProfileId, deviceTokenValue, deviceVersion, deviceName, model, userDeviceId, timeStamp, sourceIP, android);
            _ = new DeviceToken();
            _ = deviceToken.DeviceId;
            _ = deviceToken.UserProfile;

            deviceToken.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_DeviceToken_Second()
        {
            DeviceToken deviceToken = new DeviceToken(deviceTokenValue, deviceVersion, deviceName, model, userDeviceId);
            deviceToken.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_DeviceToken_Third()
        {
            DeviceToken deviceToken = new DeviceToken(deviceTokenValue, deviceVersion, deviceName, model, userDeviceId, new List<DeviceTokenNotification>() { });
            deviceToken.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            DeviceToken deviceToken = new DeviceToken(userProfileId, deviceTokenValue, deviceVersion, deviceName, model, userDeviceId, timeStamp, sourceIP, android);
            deviceToken.Update(userProfileId, deviceTokenValue, deviceVersion, deviceName, model, userDeviceId, timeStamp, sourceIP, android);
            Assert.Equal(deviceTokenValue, deviceToken.DeviceTokenValue);
        }

        [Fact]
        public void Should_DeActive()
        {
            DeviceToken deviceToken = new DeviceToken(userProfileId, deviceTokenValue, deviceVersion, deviceName, model, userDeviceId, timeStamp, sourceIP, android);
            deviceToken.DeActive();
            Assert.False(deviceToken.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            DeviceToken deviceToken = new DeviceToken(userProfileId, deviceTokenValue, deviceVersion, deviceName, model, userDeviceId, timeStamp, sourceIP, android);
            deviceToken.SetActive();
            Assert.True(deviceToken.IsActive);
        }


    }
}
