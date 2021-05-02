using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("NegotiationAttachment", Schema = "CommunicationAgency")]
    public class NegotiationAttachment : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int AttachmentId { get; private set; }

        [ForeignKey(nameof(Negotiation))]
        public int NegotiationId { get; private set; }
        public Negotiation Negotiation { get; private set; }


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

        public NegotiationAttachment()
        {

        }

        public NegotiationAttachment(string name, string fileNetReferenceId, int attachmentTypeId)
        {
            Name = name;
            FileNetReferenceId = fileNetReferenceId;
            AttachmentTypeId = attachmentTypeId;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================

        internal void Delete()
        {
            EntityDeleted();
        }
        #endregion
    }
}