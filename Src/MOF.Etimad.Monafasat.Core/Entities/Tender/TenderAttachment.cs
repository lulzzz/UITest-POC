
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Attachment", Schema = "Tender")]
    public class TenderAttachment : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderAttachmentId { get; private set; }
        [StringLength(200)]
        [Required]
        public string Name { get; private set; }
        [Required]
        public int AttachmentTypeId { get; private set; }
        [ForeignKey(nameof(AttachmentTypeId))]
        public AttachmentType AttachmentType { get; private set; }
        [StringLength(1024)]
        public string FileNetReferenceId { get; private set; }

        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { private set; get; }
        public int? ChangeStatusId { get; private set; }
        public int? ReviewStatusId { get; private set; }
        [StringLength(1024)]
        public string RejectionReason { get; private set; }
        #endregion

        #region NotMapped

        #endregion

        #region Constructors====================================================

        public TenderAttachment()
        {

        }

        public TenderAttachment(string name, /*Attachment attachment,*/string fileNetReferenceId, int attachmentTypeId, int? changeStatusId = null, int? reviewStatusId = null)
        {
            Name = name;
            FileNetReferenceId = fileNetReferenceId;
            ChangeStatusId = changeStatusId;
            ReviewStatusId = reviewStatusId;
            AttachmentTypeId = attachmentTypeId;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================


        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        internal void Delete()
        {
            EntityDeleted();
        }
        public void UpdateChangStatus(int? changeStatusId, int? reviewStatusId, string rejectionReason = "")
        {
            ChangeStatusId = changeStatusId;
            ReviewStatusId = reviewStatusId;
            RejectionReason = rejectionReason;
            EntityUpdated();
        }

        public void UpdatePHPData(string fileNetReferenceId)
        {
            FileNetReferenceId = fileNetReferenceId;
            EntityUpdated();
        }

        public void SetAddedState()
        {
            TenderAttachmentId = 0;
            EntityCreated();
        }
        public void DeleteAttachment()
        {
            EntityDeleted();
        }
        #endregion
    }
}