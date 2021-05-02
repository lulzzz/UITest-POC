using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Point", Schema = "Qualification")]
    public partial class Point
    {


        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }


        public int PointValue { get; set; }

    }
}
