
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierAttachmentBlob", Schema = "Attachment")]
    public class SupplierAttachmentBlob : AuditableEntity
    {
        [Key , ForeignKey("Attachment")]
        public int AttachmentId { get; private set; }    

        public byte[] FileContent { get; private set; }

        public SupplierAttachment Attachment { get; private set; }


        public SupplierAttachmentBlob(byte[] fileContent)
        {
            FileContent = fileContent;
            EntityCreated();
        }

        public SupplierAttachmentBlob()
        {
        }

        public SupplierAttachmentBlob MarkDelete()
        {
            this.EntityDeleted();
            return this;
        }

    }
}
