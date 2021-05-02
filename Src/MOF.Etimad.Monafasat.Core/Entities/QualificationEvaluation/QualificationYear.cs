using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{


    [Table("QualificationYear", Schema = "Qualification")]
    public partial class QualificationYear
    {

        [Key]
        public int ID { get; private set; }

        [StringLength(250)]
        public string Name { get; private set; }

    }
}
