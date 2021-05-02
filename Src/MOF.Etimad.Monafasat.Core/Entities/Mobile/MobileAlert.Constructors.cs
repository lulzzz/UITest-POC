
using System;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class MobileAlert
    {
        public MobileAlert() { }
        public MobileAlert(string message, int deviceId, int? messageId, int messageStatusId, DateTime sendDate)
        {
            Message = message;
            DeviceId = deviceId;
            MessageId = messageId;
            MessageStatusId = messageStatusId;
            SendDate = sendDate;
            EntityCreated();
        }
    }
}
