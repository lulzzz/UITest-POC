using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SpendingCategory", Schema = "LookUps")]
    public class SpendingCategory
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpendingCategoryId { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }
        [StringLength(100)]
        public string NameEn { get; private set; }

        #endregion



    }
}
