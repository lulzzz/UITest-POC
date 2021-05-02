using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("AnnouncementHistorySupplierTemplate", Schema = "AnnouncementTemplate")]
    public class AnnouncementHistorySupplierTemplate : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(AnnouncementStatusSupplierTemplate))]
        public int StatusId { get; set; }
        [StringLength(2000)]
        public string RejectionReason { get; set; }
        [Required]
        [ForeignKey(nameof(AnnouncementSupplierTemplate))]
        public int AnnouncementId { get; set; }
        [Required]
        public int UserId { get; set; }
        public AnnouncementSupplierTemplate AnnouncementSupplierTemplate { get; set; }
        public AnnouncementStatusSupplierTemplate AnnouncementStatusSupplierTemplate { get; set; }

        public AnnouncementHistorySupplierTemplate()
        {

        }
        public AnnouncementHistorySupplierTemplate(int userId, int statusId)
        {
            UserId = userId;
            StatusId = statusId;
            EntityCreated();
        }
        public AnnouncementHistorySupplierTemplate(int userId, int statusId, string rejectionReason)
        {
            UserId = userId;
            StatusId = statusId;
            RejectionReason = rejectionReason;
            EntityCreated();
        }


        public void Delete()
        {
            EntityDeleted();
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
