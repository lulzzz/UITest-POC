using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("DeviceTokenNotification", Schema = "Mobile")]
    public partial class DeviceTokenNotification : AuditableEntity
    {
        #region Fields

        [Key]
        public int DeviceTokenNotificationId { get; private set; }

        public int DeviceId { get; private set; }
        [ForeignKey(nameof(DeviceId))]
        public DeviceToken DeviceToken { get; private set; }

        public int ActivityId { get; private set; }
        [ForeignKey(nameof(ActivityId))]
        public Activity Activity { get; private set; }

        public bool Status { get; private set; }
        #endregion
    }
}
