using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementStatusSupplierTemplate", Schema = "AnnouncementTemplate")]
    public class AnnouncementStatusSupplierTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }
        [StringLength(1024)]
        public string Name { get; set; }
    }
}
