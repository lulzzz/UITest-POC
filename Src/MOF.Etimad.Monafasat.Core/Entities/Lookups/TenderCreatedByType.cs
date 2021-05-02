using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("TenderCreatedByType", Schema = "LookUps")]
    public class TenderCreatedByType
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TenderCreatedByTypeId { get; set; }

        [StringLength(100)]
        public string NameAr { get; set; }

        [StringLength(100)]
        public string NameEn { get; set; }

        #endregion

    }
}
