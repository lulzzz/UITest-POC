using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationSubCategoryResult", Schema = "Qualification")]
    public partial class QualificationSubCategoryResult : AuditableEntity
    {

        #region Ctor

        public QualificationSubCategoryResult()
        {

        }

        public QualificationSubCategoryResult(int qualificationSubCategoryId, int qualificationCategoryId, int tenderId, string supplierSelectedCr, decimal resultValue, decimal percentage)
        {
            this.QualificationSubCategoryId = qualificationSubCategoryId;
            this.QualificationCategoryId = qualificationCategoryId;
            this.TenderId = tenderId;
            this.SupplierSelectedCr = supplierSelectedCr;
            this.ResultValue = resultValue;
            this.Percentage = percentage;
            EntityCreated();
        }

        #endregion


        [Key]
        public int ID { get; private set; }

        public int QualificationSubCategoryId { get; private set; }

        public int TenderId { get; private set; }


        [StringLength(20)]
        public string SupplierSelectedCr { get; private set; }


        public decimal ResultValue { get; private set; }


        public decimal Percentage { get; private set; }

        [ForeignKey(nameof(QualificationSubCategoryId))]
        public virtual QualificationSubCategory QualificationSubCategory { get; private set; }


        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; private set; }


        [ForeignKey(nameof(SupplierSelectedCr))]
        public virtual Supplier Supplier { get; private set; }



        #region UnMapped Properties

        [NotMapped]
        public int QualificationCategoryId { get; private set; }

        #endregion


        public void Created()
        {
            EntityCreated();
        }


    }
}
