using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{


    [Table("QualificationLookupsNames", Schema = "Qualification")]
    public partial class QualificationLookupsName
    {

        public QualificationLookupsName()
        {
            QualificationLookups = new HashSet<QualificationLookup>();
        }

        [Key]
        public int ID { get; private set; }

        [StringLength(250)]
        public string Name { get; private set; }

        [StringLength(250)]
        public string NameEn { get; private set; }


        public virtual ICollection<QualificationLookup> QualificationLookups { get; private set; }
    }
}
