
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Attachment", Schema = "Attachment")]
    public class Attachment : AuditableEntity
    {
        [Key]
        public long AttachmentId { get; private set; }

        [StringLength(1000)]
        public string FileName { get; private set; }

        [StringLength(50)]
        public string Extension { get; private set; }

        public int Size { get; set; }

        //public AttachmentBlob AttachmentBlob { get; private set; }
        
        public Attachment()
        {

        }
        public Attachment(string fileName, string extension, int size/*, byte[] attachmentBlob*/)
        { 
            FileName = fileName;
            Size = size;
            Extension = extension;
            //this.AttachmentBlob = new AttachmentBlob(attachmentBlob);
            EntityCreated();
        }


        public Attachment MarkDelete()
        {
            this.EntityDeleted();
            return this;
        }
    }
}



