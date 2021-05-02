using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationItemCategory", Schema = "Qualification")]
    public partial class QualificationItemCategory
    {

        public QualificationItemCategory()
        {
            QualificationCategoryResults = new HashSet<QualificationCategoryResult>();
            QualificationSubCategories = new HashSet<QualificationSubCategory>();
        }

        [Key]
        public int ID { get; private set; }


        public string Name { get; private set; }

        public string NameEn { get; private set; }


        public virtual ICollection<QualificationCategoryResult> QualificationCategoryResults { get; private set; }

        public virtual ICollection<QualificationSubCategory> QualificationSubCategories { get; private set; }
    }
}
