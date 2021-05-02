using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    /// <summary>
    /// Implemented by 2 Enums [First stage and second stage, one database table]
    /// </summary>
    [Table("NegotiationReason", Schema = "LookUps")]
    public class NegotiationReason
    {

        public NegotiationReason()
        {

        }
        public NegotiationReason(int id, string name)
        {
            NegotiationReasonId = id;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NegotiationReasonId { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }
    }
}
