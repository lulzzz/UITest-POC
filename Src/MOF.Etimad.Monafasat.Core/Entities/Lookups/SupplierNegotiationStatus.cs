using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    [Table("SupplierSecondNegotiationStatus", Schema = "LookUps")]
    public class SupplierSecondNegotiationStatus
    {
        public SupplierSecondNegotiationStatus()
        {

        }
        public SupplierSecondNegotiationStatus(int id, string name)
        {
            SupplierNegotiaitionStatusId = id;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierNegotiaitionStatusId { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }
    }
}
