
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("PlaintRequestAttachment", Schema = "CommunicationAgency")]
    public class PlaintRequestAttachment : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int AttachmentId { get; private set; }
        [ForeignKey(nameof(PlaintRequest))]
        public int? PlaintRequestId { get; private set; }
        public PlaintRequest PlaintRequest { get; private set; }
        [StringLength(200)]
        [Required]
        public string Name { get; private set; }
        [Required]
        public int AttachmentTypeId { get; private set; }
        [ForeignKey(nameof(AttachmentTypeId))]
        public AttachmentType AttachmentType { get; private set; }
        [StringLength(1024)]
        public string FileNetReferenceId { get; private set; }
        #endregion

        #region NotMapped

        #endregion

        #region Constructors====================================================

        public PlaintRequestAttachment()
        {

        }

        public PlaintRequestAttachment( /*int plaintRequestId,*/ string name, string fileNetReferenceId, int attachmentTypeId)
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

        public void DeleteAttachment()
        {
            EntityDeleted();
        }
        #endregion
    }
}