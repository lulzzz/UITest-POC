using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("MobileAlert", Schema = "Mobile")]
    public partial class MobileAlert : AuditableEntity
    {
        #region Fields

        [Key]
        public int MobileAlertId { get; private set; }
        public string Message { get; private set; }

        public int DeviceId { get; private set; }
        [ForeignKey(nameof(DeviceId))]
        public DeviceToken DeviceToken { get; private set; }

        public int? MessageId { get; private set; }

        public int MessageStatusId { get; private set; }
        [ForeignKey(nameof(MessageStatusId))]
        public InvitationStatus MessageStatus { get; private set; }

        public string GroupCode { get; private set; }
        public DateTime SendDate { get; private set; }

        #endregion
    }
}
