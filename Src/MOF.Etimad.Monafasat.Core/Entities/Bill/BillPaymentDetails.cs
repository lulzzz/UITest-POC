using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BillPaymentDetails", Schema = "Sadad")]
    public class BillPaymentDetails : AuditableEntity
    {
        public BillPaymentDetails()
        { }
        [Key]
        public int Id { get; private set; }
        [Column(TypeName = "numeric(15, 2)")]
        public decimal AmountDue { get; private set; }
        [StringLength(50)]
        public string BillInvoiceNumber { get; private set; }
        [StringLength(100)]
        public string GFSCode { get; private set; }
        public int FeesTypeId { get; private set; }
        [ForeignKey(nameof(FeesTypeId))]
        public TenderFeesType tenderFeesType { get; private set; }
        public BillPaymentDetails(decimal amount, string billInvoiceNumber, string gFSCode, int feesTypeId)
        {
            AmountDue = amount;
            BillInvoiceNumber = billInvoiceNumber;
            GFSCode = gFSCode;
            FeesTypeId = feesTypeId;
            EntityCreated();
        }
        public void Delete()
        {
            EntityDeleted();
        }
    }
}
