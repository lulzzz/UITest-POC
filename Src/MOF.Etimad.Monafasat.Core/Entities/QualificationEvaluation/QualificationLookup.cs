using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("QualificationLookup", Schema = "Qualification")]
    public partial class QualificationLookup
    {
        [Key]
        public int ID { get; private set; }

        public int QualificationLookupId { get; private set; }

        [StringLength(50)]
        public string Name { get; private set; }

        [StringLength(50)]
        public string NameEn { get; private set; }


        [ForeignKey(nameof(QualificationLookupId))]
        public virtual QualificationLookupsName QualificationLookupsName { get; private set; }
    }
}
