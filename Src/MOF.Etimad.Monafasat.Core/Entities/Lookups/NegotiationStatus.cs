using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    /// <summary>
    /// Implemented by 2 Enums [First stage and second stage, one database table]
    /// </summary>
    [Table("NegotiationStatus", Schema = "LookUps")]
    public class NegotiationStatus
    {
        public NegotiationStatus()
        {

        }
        public NegotiationStatus(int id, string name)
        {
            NegotiationStatusId = id;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NegotiationStatusId { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }
    }
}
