using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AttachmentDetails", Schema = "Offer")]
    public class AttachmentDetails : AuditableEntity
    {
        #region Constructors

        public AttachmentDetails() { }
        public AttachmentDetails(string key, string value, int attachmentId)
        {
            Key = key;
            Value = value;
            AttachmentId = attachmentId;
            IsActive = true;
        }

        #endregion

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        [StringLength(1024)]
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int AttachmentId { get; private set; }
        [ForeignKey("AttachmentId")]
        public SupplierAttachment Attachment { get; set; }

        #region Methods

        public void Update(string key, string value)
        {
            Key = key;
            Value = value;
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
        #endregion
    }
}