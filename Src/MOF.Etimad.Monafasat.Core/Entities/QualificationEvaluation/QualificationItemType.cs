using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("QualificationItemType", Schema = "Qualification")]
    public partial class QualificationItemType
    {

        public QualificationItemType()
        {
            QualificationItems = new HashSet<QualificationItem>();
        }

        [Key]
        public int ID { get; private set; }


        [StringLength(50)]
        public string Name { get; private set; }

        [StringLength(50)]
        public string NameEn { get; private set; }


        public virtual ICollection<QualificationItem> QualificationItems { get; private set; }
    }
}
