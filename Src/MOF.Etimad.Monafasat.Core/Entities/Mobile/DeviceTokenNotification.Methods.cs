namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class DeviceTokenNotification
    {
        public void Update(int deviceId, int activityId, bool status)
        {
            DeviceId = deviceId;
            ActivityId = activityId;
            Status = status;
            EntityUpdated();
        }
        public void Update(int deviceId, int activityId)
        {
            DeviceId = deviceId;
            ActivityId = activityId;
            EntityUpdated();
        }
        public void Update(bool status)
        {

            Status = status;
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
