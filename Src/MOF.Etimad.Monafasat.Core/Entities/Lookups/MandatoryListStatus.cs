using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("MandatoryListStatus", Schema = "LookUps")]
    public class MandatoryListStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }

        [StringLength(100)]
        public string NameEn { get; private set; }
    }
}
