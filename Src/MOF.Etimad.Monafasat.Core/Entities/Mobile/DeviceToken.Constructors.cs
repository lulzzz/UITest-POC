
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class DeviceToken
    {
        public DeviceToken() { }
        public DeviceToken(int userProfileId, string deviceTokenValue, string deviceVersion, string deviceName, string model, string userDeviceId, int timeStamp, string sourceIP, int android)
        {
            UserProfileId = userProfileId;
            DeviceTokenValue = deviceTokenValue;
            DeviceVersion = deviceVersion;
            DeviceName = deviceName;
            Model = model;
            UserDeviceId = userDeviceId;
            TimeStamp = timeStamp;
            SourceIP = sourceIP;
            Android = android;
            EntityCreated();
        }


        public DeviceToken(string deviceTokenValue, string deviceVersion, string deviceName, string model, string userDeviceId)
        {
            UserProfileId = null;
            DeviceTokenValue = deviceTokenValue;
            DeviceVersion = deviceVersion;
            DeviceName = deviceName;
            Model = model;
            UserDeviceId = userDeviceId;
            TimeStamp = DateTime.Now.TimeOfDay.Days;
            SourceIP = null;
            Android = model.Trim() == "ANDROID" ? 1 : 0;
            EntityCreated();
        }
        public DeviceToken(string deviceTokenValue, string deviceVersion, string deviceName, string model, string userDeviceId, List<DeviceTokenNotification> notificationSetting)
        {
            UserProfileId = null;
            DeviceTokenValue = deviceTokenValue;
            DeviceVersion = deviceVersion;
            DeviceName = deviceName;
            Model = model;
            UserDeviceId = userDeviceId;
            TimeStamp = DateTime.Now.TimeOfDay.Days;
            SourceIP = null;
            Android = model.Trim() == "ANDROID" ? 1 : 0;
            NotificationSetting = notificationSetting;
            EntityCreated();
        }
    }
}
