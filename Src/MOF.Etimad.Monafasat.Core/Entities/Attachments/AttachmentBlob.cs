
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AttachmentBlob", Schema = "Attachment")]
    public class AttachmentBlob : AuditableEntity
    {
        [Key , ForeignKey("Attachment")]
        public long AttachmentId { get; private set; }    

        public byte[] FileContent { get; private set; }

        public Attachment Attachment { get; private set; }


        public AttachmentBlob(byte[] fileContent)
        {
            FileContent = fileContent;
            EntityCreated();
        }

        public AttachmentBlob()
        {
        }

        public AttachmentBlob MarkDelete()
        {
            this.EntityDeleted();
            return this;
        }

    }
}
