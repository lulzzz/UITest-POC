using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    [Table("EstablishmentType", Schema = "LookUps")]
    public class EstablishmentType
    {
        public EstablishmentType()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstablishmentTypeId { get; set; }
        [StringLength(20)]
        public string CommercialRegistrationNo { get; private set; }
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Size { get; set; }

    }
}
