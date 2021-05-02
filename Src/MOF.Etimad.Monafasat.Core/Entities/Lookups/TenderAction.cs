using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderAction", Schema = "LookUps")]
    public class TenderAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TenderActionId { get; set; }

        [StringLength(100)]
        public string NameAr { get; set; }

        [StringLength(100)]
        public string NameEn { get; set; }
    }
}
