using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    /// <summary>
    /// Implemented by 2 Enums [First stage and second stage, one database table]
    /// </summary>
    [Table("NegotiationType", Schema = "LookUps")]
    public class NegotiationType
    {

        public NegotiationType()
        {

        }
        public NegotiationType(int id, string name)
        {
            NegotiationTypeId = id;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NegotiationTypeId { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }
    }
}
