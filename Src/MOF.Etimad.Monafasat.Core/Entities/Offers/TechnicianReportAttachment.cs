using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TechnicianReportAttachments", Schema = "Offer")]
    public class TechnicianReportAttachment : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int AttachmentId { get; private set; }
        [StringLength(200)]
        [Required]
        public string Name { get; private set; }
        [Required]
        public int AttachmentTypeId { get; private set; }
        [ForeignKey(nameof(AttachmentTypeId))]
        public AttachmentType AttachmentType { get; private set; }
        [StringLength(1024)]
        public string FileNetReferenceId { get; private set; }

        [ForeignKey(nameof(Offer))]
        public int OfferId { get; private set; }
        public Offer Offer { private set; get; }
        #endregion

        #region NotMapped

        #endregion

        #region Constructors====================================================

        public TechnicianReportAttachment()
        {

        }

        public TechnicianReportAttachment(string name, string fileNetReferenceId, int attachmentTypeId)
        {
            Name = name;
            FileNetReferenceId = fileNetReferenceId;
            AttachmentTypeId = attachmentTypeId;
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

        public void UpdatefileNetReferenceId(string fileNetReferenceId)
        {
            FileNetReferenceId = fileNetReferenceId;
            EntityUpdated();
        }

        public void SetAddedState()
        {
            AttachmentId = 0;
            EntityCreated();
        }
        public void DeleteAttachment()
        {
            EntityDeleted();
        }
        #endregion
    }
}
