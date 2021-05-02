using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    [Table("NotifacationStatusEntity", Schema = "LookUps")]
    public class NotifacationStatusEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NotifacationStatusEntityId { get; set; }
        public string Name { get; set; }
    }
}
