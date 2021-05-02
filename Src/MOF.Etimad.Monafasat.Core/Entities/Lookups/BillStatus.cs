using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core
{
    [Table("TenderNumberIsertionType", Schema = "LookUps")]
    public class TenderNumberIsertionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(1024)]
        public string TenderNumberIsertionTypeName { get; set; }

        
    }
}
