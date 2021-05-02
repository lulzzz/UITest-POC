using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("NotificationSetting", Schema = "Notification")]

    public abstract class BaseNotificationSetting : AuditableEntity
    {
        public BaseNotificationSetting()
        {

        }

        [Key]
        public int Id { get; private set; }

        // public NotificationBy ChangePassword { get; protected set; }
        //  public NotificationBy ForgotYourPassword { get; protected set; }
        public NotificationBy DefaultNotify { get; protected set; } = new NotificationBy() { Email = true, Mobile = true };

        public int UserProfileId { get; private set; }

        [ForeignKey("UserProfileId")]
        public UserProfile User { get; private set; }
      

    }
    public class DefaultNotificationSetting : BaseNotificationSetting
    {
        public DefaultNotificationSetting()
        {

        }
        public DefaultNotificationSetting(bool load)
        {
            DefaultNotify = new NotificationBy() { Email = true, Mobile = true };
            EntityCreated();

        }

    }
}
