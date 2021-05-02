using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    [Table("NotifayTypeEntity", Schema = "LookUps")]
    public class NotifayTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NotifayTypeId { get; set; }
        public string Name { get; set; }
    }
}
