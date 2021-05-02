using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationConfiguration", Schema = "Qualification")]
    public partial class QualificationConfiguration : AuditableEntity
    {

        #region Ctor

        public QualificationConfiguration(int id, int prequalificationID, int qualificationItemId, decimal min, decimal max, decimal weight)
        {
            this.ID = id;
            this.TenderId = prequalificationID;
            this.QualificationItemId = qualificationItemId;
            this.Min = min;
            this.Max = max;
            this.Weight = weight;
            if (id == 0)
            {
                EntityCreated();
            }
            else
            {
                EntityUpdated();
            }
        }

        public QualificationConfiguration()
        {
            QualificationSupplierData = new HashSet<QualificationSupplierData>();
        }

        #endregion


        #region Fields
        [Key]
        public int ID { get; private set; }
        public int TenderId { get; private set; }
        public int QualificationItemId { get; private set; }
        public decimal Min { get; private set; }
        public decimal Max { get; private set; }

        public decimal Value { get; private set; }
        public decimal Weight { get; private set; }

        [ForeignKey(nameof(QualificationItemId))]
        public virtual QualificationItem QualificationItem { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; private set; }

        #endregion
        public virtual ICollection<QualificationSupplierData> QualificationSupplierData { get; private set; }


        #region Methods



        internal void Delete()
        {
            EntityDeleted();
        }

        #endregion
    }
}
