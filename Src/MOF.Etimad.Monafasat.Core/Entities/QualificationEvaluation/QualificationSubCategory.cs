using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{


    [Table("QualificationSubCategory", Schema = "Qualification")]
    public partial class QualificationSubCategory
    {

        public QualificationSubCategory()
        {
            QualificationItems = new HashSet<QualificationItem>();
            QualificationSubCategoryResults = new HashSet<QualificationSubCategoryResult>();
            QualificationTypeCategories = new HashSet<QualificationTypeCategory>();
        }

        [Key]
        public int ID { get; private set; }

        public int QualificationCategoryId { get; private set; }


        public string Name { get; private set; }

        public string NameEn { get; private set; }

        public bool IsConfigure { get; private set; }


        public virtual ICollection<QualificationItem> QualificationItems { get; private set; }

        [ForeignKey(nameof(QualificationCategoryId))]
        public virtual QualificationItemCategory QualificationItemCategory { get; private set; }


        public virtual ICollection<QualificationSubCategoryResult> QualificationSubCategoryResults { get; private set; }


        public virtual ICollection<QualificationTypeCategory> QualificationTypeCategories { get; private set; }
    }
}
