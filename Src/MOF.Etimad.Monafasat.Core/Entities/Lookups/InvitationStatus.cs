using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    [Table("InvitationStatus", Schema = "LookUps")]
    public class InvitationStatus
    {
        #region Fileds
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvitationStatusId { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }
        [StringLength(100)]
        public string NameEn { get; private set; }
        #endregion

    }
}
