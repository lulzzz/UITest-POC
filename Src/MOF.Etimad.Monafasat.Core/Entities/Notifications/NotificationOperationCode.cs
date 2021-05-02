using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("NotificationOperationCode", Schema = "Notification")]
    public class NotificationOperationCode
    {
        [Key]
        public int Id { get; private set; }
        [StringLength(100)]
        public string OperationCode { get; private set; }
        [StringLength(200)]
        public string ArabicName { get; private set; }
        [StringLength(200)]
        public string EnglishName { get; private set; }
        public int UserRoleId { get; private set; }
        [ForeignKey(nameof(UserRoleId))]
        public UserRole UserRole { get; private set; }
        [DefaultValue(true)]
        public bool DefaultSMS { get; private set; }
        [DefaultValue(true)]
        public bool DefaultEmail { get; private set; }
        [StringLength(200)]
        public string EmailTemplateName { get; private set; }
        [DefaultValue(true)]
        public bool CanEditEmail { get; private set; }
        [DefaultValue(true)]
        public bool CanEditSMS { get; private set; }

        public int? NotificationCategoryId { get; private set; }
        [ForeignKey(nameof(NotificationCategoryId))]
        public NotificationCategory NotificationCategory { get; private set; }
    }

  
}
