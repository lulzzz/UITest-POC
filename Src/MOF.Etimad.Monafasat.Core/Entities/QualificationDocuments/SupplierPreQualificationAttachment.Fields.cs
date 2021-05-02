using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierPreQualificationAttachment", Schema = "Qualification")]
    public partial class SupplierPreQualificationAttachment : AuditableEntity
    {

        #region [Fields]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttachmentId { get; private set; }

        [StringLength(1024)]
        public string FileName { get; private set; }

        [StringLength(1024)]
        public string FileNetReferenceId { get; private set; }
        public int SupPreQAttachmentId { get; private set; }
        #endregion
        #region [Navigation]

        [ForeignKey("SupPreQAttachmentId")]
        public SupplierPreQualificationDocument SupplierPreQualificationDocument { get; private set; }
        #endregion

        public SupplierPreQualificationAttachment()
        {

        }
        public SupplierPreQualificationAttachment(string fileName, string fileNetReferenceId, int supplierPreQualificationDocumentId)
        {
            FileName = fileName;
            FileNetReferenceId = fileNetReferenceId;
            this.SupPreQAttachmentId = supplierPreQualificationDocumentId;
            EntityCreated();
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
    }

}
