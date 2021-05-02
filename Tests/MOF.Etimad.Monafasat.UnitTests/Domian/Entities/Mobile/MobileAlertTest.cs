using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Mobile
{
    public class MobileAlertTest
    {
        private const int deviceId = 1;
        private const string message = "message";
        private const int messageId = 1;
        private const int messageStatusId = 1;

        [Fact]
        public void Should_Construct_MobileAlert()
        {
            MobileAlert mobileAlert = new MobileAlert(message, deviceId, messageId, messageStatusId, DateTime.Now.Date);
            _ = new MobileAlert();
            _ = mobileAlert.MobileAlertId;
            _ = mobileAlert.DeviceToken;
            _ = mobileAlert.MessageStatus;
            _ = mobileAlert.GroupCode;

            mobileAlert.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            MobileAlert mobileAlert = new MobileAlert(message, deviceId, messageId, messageStatusId, DateTime.Now.Date);

            mobileAlert.Update(message, deviceId, messageId, messageStatusId, DateTime.Now.Date);
            Assert.Equal(message, mobileAlert.Message);
        }

        [Fact]
        public void Should_DeActive()
        {
            MobileAlert mobileAlert = new MobileAlert(message, deviceId, messageId, messageStatusId, DateTime.Now.Date);
            mobileAlert.DeActive();
            Assert.False(mobileAlert.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            MobileAlert mobileAlert = new MobileAlert(message, deviceId, messageId, messageStatusId, DateTime.Now.Date);
            mobileAlert.SetActive();
            Assert.True(mobileAlert.IsActive);
        }

        [Fact]
        public void Should_MessageSent()
        {
            MobileAlert mobileAlert = new MobileAlert(message, deviceId, messageId, messageStatusId, DateTime.Now.Date);
            mobileAlert.MessageSent();
            Assert.Equal((int)Enums.MessageStatus.Sent, mobileAlert.MessageStatusId);
        }
    }
}
