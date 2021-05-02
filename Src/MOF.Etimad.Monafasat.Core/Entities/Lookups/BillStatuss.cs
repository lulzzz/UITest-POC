using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core
{
    [Table("BillStatus", Schema = "LookUps")]
    public class BillStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillStatusId { get; set; }

        [StringLength(1024)]
        public string BillStatusNameEn { get; set; }

        [StringLength(1024)]
        public string BillStatusNameAr { get; set; }
    }
}
