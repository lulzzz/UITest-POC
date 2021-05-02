using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    [Table("OfferSolidarityStatus", Schema = "LookUps")]
    public class OfferSolidarityStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CombinedStatusId { get; private set; }
        [StringLength(1024)]
        public string Name { get; private set; }
    }
}
