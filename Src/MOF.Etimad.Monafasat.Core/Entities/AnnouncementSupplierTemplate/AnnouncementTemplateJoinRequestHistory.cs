using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementTemplateJoinRequestHistory", Schema = "AnnouncementTemplate")]
    public class AnnouncementTemplateJoinRequestHistory : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [StringLength(2000)]
        public string RejectionReason { get; set; }

        [StringLength(2000)]
        public string DeleteReason { get; set; }

        [Required]
        [ForeignKey(nameof(JoinRequestSupplierTemplate))]
        public int JoinRequestId { get; set; }

        [ForeignKey(nameof(AnnouncementJoinRequestStatus))]
        public int JoinRequestStatusId { get; set; }

        public AnnouncementJoinRequestSupplierTemplate JoinRequestSupplierTemplate { get; set; }
        public AnnouncementJoinRequestStatusSupplierTemplate AnnouncementJoinRequestStatus { get; set; }

        public AnnouncementTemplateJoinRequestHistory()
        {

        }

        public AnnouncementTemplateJoinRequestHistory(int userId, int joinRequestId, int joinRequestStatusId, string rejectionReason = "", string deleteReason = "")
        {
            UserId = userId;
            JoinRequestId = joinRequestId;
            JoinRequestStatusId = joinRequestStatusId;
            RejectionReason = rejectionReason;
            DeleteReason = deleteReason;
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