using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementJoinRequestStatusSupplierTemplate", Schema = "LookUps")]
    public class AnnouncementJoinRequestStatusSupplierTemplate
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }

        [StringLength(1024)]
        public string NameEn { get; private set; }

        [StringLength(1024)]
        public string NameAr { get; private set; }

        #endregion


    }
}
