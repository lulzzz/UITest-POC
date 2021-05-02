using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BillArchive", Schema = "Sadad")]
    public class BillArchive : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }
        public int? ConditionsBookletID { get; private set; }
        public int? InvitationId { get; private set; }
        public int TenderId { get; private set; }
        [StringLength(100)]
        public string TenderReferenceNumber { get; private set; }

        [StringLength(50)]
        public string BillInvoiceNumber { get; private set; }

        [StringLength(50)]
        public string AgencyCode { get; set; }
        [StringLength(20)]
        public string SupplierNumber { get; private set; }

        public BillArchive()
        { }
        public BillArchive(int? conditionsBookletID, int? invitationId, int tenderId, string tenderReferenceNumber, string billInvoiceNumber, string agencyCode, string supplierNumber)
        {
            this.ConditionsBookletID = conditionsBookletID;
            this.InvitationId = invitationId;
            this.TenderId = tenderId;
            this.TenderReferenceNumber = tenderReferenceNumber;
            this.BillInvoiceNumber = billInvoiceNumber;
            this.AgencyCode = agencyCode;
            this.SupplierNumber = supplierNumber;
            EntityCreated();
        }
    }
}
