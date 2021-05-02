using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationSupplierDataYearly", Schema = "Qualification")]
    public partial class QualificationSupplierDataYearly : AuditableEntity

    {

        #region Constructors
        public QualificationSupplierDataYearly()
        {

        }

        public QualificationSupplierDataYearly(int id, int? qualificationItemId, int qualificationYearId, string supplierSelectedCr, decimal supplierValue, int tenderId)
        {
            this.QualificationItemId = qualificationItemId;
            this.QualificationYearId = qualificationYearId;
            this.SupplierSelectedCr = supplierSelectedCr;
            this.SupplierValue = supplierValue;
            this.TenderId = tenderId;
            EntityCreated();
        }
        #endregion region


        [Key]
        public int ID { get; private set; }

        public int TenderId { get; private set; }

        public int QualificationYearId { get; private set; }


        public decimal SupplierValue { get; private set; }

        public int? QualificationItemId { get; private set; }

        [StringLength(20)]
        public string SupplierSelectedCr { get; private set; }



        [ForeignKey(nameof(QualificationYearId))]
        public virtual QualificationYear QualificationYear { get; private set; }

        [ForeignKey(nameof(QualificationItemId))]
        public virtual QualificationItem QualificationItem { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; private set; }

        [ForeignKey(nameof(SupplierSelectedCr))]
        public virtual Supplier Supplier { get; private set; }



        public void DeActive()
        {
            this.IsActive = false;
            EntityUpdated();
        }


    }
}
