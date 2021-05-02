using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class MobileAlert
    {
        public void Update(string message, int deviceId, int? messageId, int messageStatusId, DateTime sendDate)
        {
            Message = message;
            DeviceId = deviceId;
            MessageId = messageId;
            MessageStatusId = messageStatusId;
            SendDate = sendDate;
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
        public void MessageSent()
        {
            MessageStatusId = (int)Enums.MessageStatus.Sent;
            EntityUpdated();
        }
    }
}
