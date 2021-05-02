using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{


    [Table("QualificationType", Schema = "Qualification")]
    public partial class QualificationType
    {

        public QualificationType()
        {
            QualificationTypeCategories = new HashSet<QualificationTypeCategory>();
        }

        [Key]
        public int ID { get; private set; }


        [StringLength(50)]
        public string Name { get; private set; }

        [StringLength(50)]
        public string NameEn { get; private set; }

        public bool? IsDeleted { get; private set; }

        public virtual ICollection<QualificationTypeCategory> QualificationTypeCategories { get; private set; }
    }
}
