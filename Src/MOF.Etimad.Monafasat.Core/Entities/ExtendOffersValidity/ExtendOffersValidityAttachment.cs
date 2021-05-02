using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ExtendOffersValidityAttachment", Schema = "CommunicationAgency")]
    public class ExtendOffersValidityAttachment : AuditableEntity
    {
        [Key]
        public int AttachmentId { get; private set; }

        [ForeignKey(nameof(ExtendOffersValiditySupplier))]
        public int ExtendOffersValiditySupplierId { get; private set; }
        public ExtendOffersValiditySupplier ExtendOffersValiditySupplier { get; private set; }


        [StringLength(200)]
        [Required]
        public string Name { get; private set; }
        [Required]
        public int AttachmentTypeId { get; private set; }
        [ForeignKey(nameof(AttachmentTypeId))]
        public AttachmentType AttachmentType { get; private set; }
        [StringLength(1024)]
        public string FileNetReferenceId { get; private set; }

        public ExtendOffersValidityAttachment()
        {

        }

        public ExtendOffersValidityAttachment(string name, string fileNetReferenceId, int attachmentTypeId, int extendOffersValidtySupplierId)
        {
            Name = name;
            FileNetReferenceId = fileNetReferenceId;
            AttachmentTypeId = attachmentTypeId;
            ExtendOffersValiditySupplierId = extendOffersValidtySupplierId;
            EntityCreated();
        }

        public ExtendOffersValidityAttachment(string name, string fileNetReferenceId)
        {
            Name = name;
            FileNetReferenceId = fileNetReferenceId;
            EntityCreated();
        }

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
    }
}
