using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation
{
    [Table("QualificationSubCategoryConfiguration", Schema = "Qualification")]
    public partial class QualificationSubCategoryConfiguration : AuditableEntity
    {

        #region Ctor

        public QualificationSubCategoryConfiguration()
        {

        }

        public QualificationSubCategoryConfiguration(int prequalificationID, int qualificationSubCategoryId, decimal weight)
        {
            TenderId = prequalificationID;
            QualificationSubCategoryId = qualificationSubCategoryId;
            Weight = weight;
            EntityCreated();
        }

        #endregion


        #region Fields
        [Key]
        public int ID { get; private set; }
        public int TenderId { get; private set; }
        public int QualificationSubCategoryId { get; private set; }
        public decimal Weight { get; private set; }

        [ForeignKey(nameof(QualificationSubCategoryId))]
        public virtual QualificationSubCategory QualificationSubCategory { get; private set; }

        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; private set; }
        #endregion


        #region Methods


        internal void Delete()
        {
            EntityDeleted();
        }
        #endregion

    }
}
