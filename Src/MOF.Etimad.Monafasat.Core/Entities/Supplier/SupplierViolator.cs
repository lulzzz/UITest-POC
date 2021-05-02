using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierViolator", Schema = "Supplier")]
    public class SupplierViolator : AuditableEntity
    {
        #region Fields
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SupplierViolatorId { get; private set; }

        [ForeignKey(nameof(TenderChangeRequest))]
        public int TenderChangeRequestId { get; private set; }
        public TenderChangeRequest TenderChangeRequest { get; set; }

        [StringLength(20)]
        [ForeignKey(nameof(Supplier))]
        public string CR { get; private set; }
        public Supplier Supplier { get; set; }

        #endregion

        #region Constructors

        public SupplierViolator()
        {

        }

        public SupplierViolator(string supplierCRNumber)
        {
            CR = supplierCRNumber;
            EntityCreated();
        }


        #endregion

    }
}
