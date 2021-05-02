
namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class DeviceTokenNotification
    {
        public DeviceTokenNotification() { }
        public DeviceTokenNotification(int deviceId, int activityId, bool deviceNotificationStatus)
        {
            DeviceId = deviceId;
            ActivityId = activityId;
            Status = deviceNotificationStatus;
            EntityCreated();
        }
        public DeviceTokenNotification(int activityId, bool deviceNotificationStatus)
        {

            ActivityId = activityId;
            Status = deviceNotificationStatus;
            EntityCreated();
        }
    }
}
