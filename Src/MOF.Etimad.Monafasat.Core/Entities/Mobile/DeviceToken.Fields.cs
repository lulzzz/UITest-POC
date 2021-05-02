using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("DeviceToken", Schema = "Mobile")]
    public partial class DeviceToken : AuditableEntity
    {
        #region Fields

        [Key]
        public int DeviceId { get; private set; }

        public int? UserProfileId { get; private set; }
        [ForeignKey(nameof(UserProfileId))]
        public UserProfile UserProfile { get; private set; }

        [MaxLength(500)]
        public string DeviceTokenValue { get; set; }

        [MaxLength(15)]
        public string DeviceVersion { get; set; }

        [MaxLength(100)]
        public string DeviceName { get; set; }

        [MaxLength(30)]
        public string Model { get; set; }

        [MaxLength(60)]
        public string UserDeviceId { get; set; }

        public int TimeStamp { get; set; }

        [MaxLength(20)]
        public string SourceIP { get; set; }

        public int Android { get; set; }
        public List<DeviceTokenNotification> NotificationSetting { get; set; }

        #endregion
    }
}
