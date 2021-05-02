using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierPreQualificationDocument", Schema = "Qualification")]
    public partial class SupplierPreQualificationDocument : AuditableEntity
    {

        #region [Fields]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierPreQualificationDocumentId { get; private set; }
        public string SupplierId { get; private set; }
        public int PreQualificationId { get; private set; }
        public int? StatusId { get; private set; }
        public int? PreQualificationResult { get; private set; }
        public string RejectionReason { get; private set; }
        #endregion
        #region [Navigation]
        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; private set; }
        /// <summary>
        /// Refers To Tender Table 
        /// </summary>
        [ForeignKey(nameof(PreQualificationId))]
        public Tender PreQualification { get; private set; }

        [ForeignKey(nameof(StatusId))]
        public OfferStatus Status { get; private set; }



        public List<SupplierPreQualificationAttachment> supplierPreQualificationAttachments { private set; get; } =
        new List<SupplierPreQualificationAttachment>();
        #endregion
    }
}
