
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AttachmentChanges", Schema = "Tender")]
    public class TenderAttachmentChanges : AuditableEntity
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
        [StringLength(1024)]
        public string RejectionReason { get; private set; }
        public TenderChangeRequest ChangeRequest { get; private set; }
        [ForeignKey("ChangeRequest")]
        public int TenderChangeRequestId { get; private set; }
        public TenderAttachment TenderAttachment { get; private set; }
        [ForeignKey("TenderAttachment")]
        public int? DeletedAttachmentId { get; private set; }
        #endregion

        #region NotMapped

        #endregion

        #region Constructors====================================================

        public TenderAttachmentChanges()
        {

        }

        public TenderAttachmentChanges(string name, /*Attachment attachment,*/string fileNetReferenceId, int attachmentTypeId, int? deletedAttachmentId = null)
        {
            Name = name;
            FileNetReferenceId = fileNetReferenceId;
            AttachmentTypeId = attachmentTypeId;
            DeletedAttachmentId = deletedAttachmentId;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================


        public void Update(string name)
        {
            Name = name;
            EntityUpdated();
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

        internal void Delete()
        {
            EntityDeleted();
        }
        public void UpdateChangStatus(int? changeStatusId, int? reviewStatusId, string rejectionReason = "")
        {
            RejectionReason = rejectionReason;
            EntityUpdated();
        }
        #endregion
    }
}