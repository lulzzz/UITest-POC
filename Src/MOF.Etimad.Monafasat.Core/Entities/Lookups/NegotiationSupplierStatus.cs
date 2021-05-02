using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    /// <summary>
    /// Implemented by 2 Enums [First stage and second stage, one database table]
    /// </summary>
    [Table("NegotiationSupplierStatus", Schema = "LookUps")]
    public class NegotiationSupplierStatus
    {
        public NegotiationSupplierStatus()
        {

        }
        public NegotiationSupplierStatus(int id, string name)
        {
            NegotiationSupplierStatusId = id;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NegotiationSupplierStatusId { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }
    }
}
