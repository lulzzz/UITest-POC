using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationCategoryResult", Schema = "Qualification")]
    public partial class QualificationCategoryResult : AuditableEntity
    {

        #region Ctor

        public QualificationCategoryResult()
        {

        }

        public QualificationCategoryResult(int id, int qualificationItemCategoryId, int tenderId, string supplierSelectedCr, decimal resultValue, decimal percentage, decimal weight)
        {
            this.ID = id;
            this.QualificationItemCategoryId = qualificationItemCategoryId;
            this.TenderId = tenderId;
            this.SupplierSelectedCr = supplierSelectedCr;
            this.ResultValue = resultValue;
            this.Percentage = percentage;
            this.Weight = weight;
            EntityCreated();
        }

        #endregion


        [Key]
        public int ID { get; private set; }

        public int QualificationItemCategoryId { get; private set; }

        public int TenderId { get; set; }


        [StringLength(20)]
        public string SupplierSelectedCr { get; private set; }

        public decimal ResultValue { get; private set; }


        public decimal Percentage { get; private set; }

        public decimal Weight { get; private set; }


        [ForeignKey(nameof(QualificationItemCategoryId))]
        public virtual QualificationItemCategory QualificationItemCategory { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; private set; }


        [ForeignKey(nameof(SupplierSelectedCr))]
        public virtual Supplier Supplier { get; private set; }

        public void Created()
        {
            EntityCreated();
        }

    }
}
