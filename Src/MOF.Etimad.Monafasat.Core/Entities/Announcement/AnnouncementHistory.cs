using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementHistory", Schema = "Announcement")]
    public class AnnouncementHistory : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(AnnouncementStatus))]
        public int StatusId { get; set; }
        [StringLength(2000)]
        public string RejectionReason { get; set; }
        [Required]
        [ForeignKey(nameof(Announcement))]
        public int AnnouncementId { get; set; }
        [Required]
        public int UserId { get; set; }
        public Announcement Announcement { get; set; }
        public AnnouncementStatus AnnouncementStatus { get; set; }

        public AnnouncementHistory()
        {

        }
        public AnnouncementHistory(int userId, int statusId)
        {
            UserId = userId;
            StatusId = statusId;
            EntityCreated();
        }
        public AnnouncementHistory(int userId, int statusId, string rejectionReason)
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