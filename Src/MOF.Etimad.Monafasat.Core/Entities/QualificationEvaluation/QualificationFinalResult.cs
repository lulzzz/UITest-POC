using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationFinalResult", Schema = "Qualification")]
    public partial class QualificationFinalResult : AuditableEntity
    {

        #region Ctor

        public QualificationFinalResult()
        {

        }

        public QualificationFinalResult(int tenderId, string supplierSelectedCr, decimal resultValue, int qualificationLookupId)
        {
            this.TenderId = tenderId;
            this.SupplierSelectedCr = supplierSelectedCr;
            this.ResultValue = resultValue;
            this.QualificationLookupId = qualificationLookupId;
            EntityCreated();
        }

        #endregion


        [Key]
        public int ID { get; private set; }

        public int TenderId { get; private set; }


        [StringLength(20)]
        public string SupplierSelectedCr { get; private set; }


        public decimal ResultValue { get; private set; }


        public int QualificationLookupId { get; private set; }
        public string FailingReason { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; private set; }


        [ForeignKey(nameof(SupplierSelectedCr))]
        public virtual Supplier Supplier { get; private set; }


        [ForeignKey(nameof(QualificationLookupId))]
        public virtual QualificationLookup QualificationLookup { get; private set; }

        public void Created()
        {
            EntityCreated();
        }

        public void DeActive()
        {
            this.IsActive = false;
            EntityUpdated();
        }

        public void UpdateQualificationLookupId(int qualificationLookupId, string failingReason)
        {
            QualificationLookupId = qualificationLookupId;
            if (qualificationLookupId == (int)Enums.QualificationResultLookup.Failed)
            {
                FailingReason = failingReason;
            }
            else
            {
                FailingReason = "";
            }
            EntityUpdated();
        }

    }
}
