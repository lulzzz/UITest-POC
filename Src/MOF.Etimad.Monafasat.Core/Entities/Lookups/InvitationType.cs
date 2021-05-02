using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("InvitationType", Schema = "LookUps")]
    public class InvitationType
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvitationTypeId { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }
        [StringLength(100)]
        public string NameEn { get; private set; }
        #endregion
    }
}
