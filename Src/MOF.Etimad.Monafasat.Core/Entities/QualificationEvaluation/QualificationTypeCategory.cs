using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationTypeCategory", Schema = "Qualification")]
    public partial class QualificationTypeCategory
    {

        #region Ctor


        #endregion

        #region fields
        [Key]
        public int ID { get; private set; }

        public int QualificationTypeId { get; private set; }

        public int QualificationSubCategoryId { get; private set; }

        [ForeignKey(nameof(QualificationSubCategoryId))]
        public virtual QualificationSubCategory QualificationSubCategory { get; private set; }

        [ForeignKey(nameof(QualificationTypeId))]
        public virtual QualificationType QualificationType { get; private set; }

        #endregion


        #region Methods


        #endregion
    }
}
