using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    [Table("CancelationReason", Schema = "LookUps")]
    public class CancelationReason
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CancelationReasonId { get; private set; }

        [StringLength(500)]
        public string NameAr { get; private set; }

        [StringLength(500)]
        public string NameEn { get; private set; }

        public CancelationReason()
        {

        }
        public CancelationReason(int canelationReason)
        {
            CancelationReasonId = canelationReason;
        }
    }
}
