using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierAttachment", Schema = "Offer")]
    public abstract class SupplierAttachment : AuditableEntity
    {
        public SupplierAttachment(int offerId, int attachmentId, string fileName, string fileNetReferenceId)
        {
            OfferId = offerId;
            FileName = fileName;
            FileNetReferenceId = fileNetReferenceId;

            if (attachmentId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }
        #region Constructors

        public SupplierAttachment()
        {

        }
        public SupplierAttachment(int attachmentId, string fileName, string fileNetReferenceId)
        {
            FileName = fileName;
            FileNetReferenceId = fileNetReferenceId;

            if (attachmentId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }
        public SupplierAttachment(string fileName, string fileNetReferenceId)
        {
            FileName = fileName;
            FileNetReferenceId = fileNetReferenceId;
            EntityCreated();
        }

        public void DeleteAttachment()
        {
            EntityDeleted();
        }
        #endregion

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttachmentId { get; private set; }

        [StringLength(1024)]
        public string FileName { get; private set; }

        [StringLength(1024)]
        public string FileNetReferenceId { get; set; }

        public int OfferId { get; private set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; private set; }
        #region Methods

        public void UpdatePHPData(string fileNetReferenceId)
        {
            this.FileNetReferenceId = fileNetReferenceId;
            EntityUpdated();
        }
        public void UpdateFileName(string fileName)
        {
            this.FileName = fileName;
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        #endregion
    }

    public class SupplierOriginalAttachment : SupplierAttachment
    {
        public SupplierOriginalAttachment()
        {
        }
        public SupplierOriginalAttachment(string fileName, string fileNetReferenceId) : base
            (fileName, fileNetReferenceId)
        {
        }
        public SupplierOriginalAttachment(int offerId, int attachmentId, string fileName, string fileNetReferenceId) : base
            (offerId, attachmentId, fileName, fileNetReferenceId)
        {
        }
    }
    public class SupplierTechnicalProposalAttachment : SupplierAttachment
    {
        public SupplierTechnicalProposalAttachment()
        {
        }
        public SupplierTechnicalProposalAttachment(string fileName, string fileNetReferenceId) : base
            (fileName, fileNetReferenceId)
        {
        }
    }
    public class SupplierFinancialProposalAttachment : SupplierAttachment
    {
        public SupplierFinancialProposalAttachment()
        {
        }
        public SupplierFinancialProposalAttachment(string fileName, string fileNetReferenceId) : base
            (fileName, fileNetReferenceId)
        {
        }
    }
    public class SupplierFinancialandTechnicalProposalAttachment : SupplierAttachment
    {
        public SupplierFinancialandTechnicalProposalAttachment()
        {
        }
        public SupplierFinancialandTechnicalProposalAttachment(string fileName, string fileNetReferenceId) : base
            (fileName, fileNetReferenceId)
        {
        }
    }
    public class SupplierCombinedAttachment : SupplierAttachment
    {
        public SupplierCombinedAttachment()
        {
        }
        public SupplierCombinedAttachment(string fileName, string fileNetReferenceId) : base
            (fileName, fileNetReferenceId)
        {
        }
    }
}