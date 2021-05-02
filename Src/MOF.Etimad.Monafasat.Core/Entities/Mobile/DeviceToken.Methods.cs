namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class DeviceToken
    {
        public void Update(int userProfileId, string deviceTokenValue, string deviceVersion, string deviceName, string model, string userDeviceId, int timeStamp, string sourceIP, int android)
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
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
    }
}
