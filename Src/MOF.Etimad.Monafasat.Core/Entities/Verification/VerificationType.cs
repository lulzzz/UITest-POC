using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("VerificationType", Schema = "Verification")]
    public class VerificationType
    {
        [Key]
        public int VerificationTypeId { get; private set; }

        [StringLength(1024)]
        public string VerificationTypeName { get; private set; }
    }
}
