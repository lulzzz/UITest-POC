using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderUnitUpdateType", Schema = "LookUps")]
    public class TenderUnitUpdateType
    {
        #region Fields====================================================

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TenderUnitUpdateTypeId { get; private set; }

        [StringLength(1024)]
        public string Name { get; private set; }

        #endregion

        #region NotMapped


        #endregion
    }

}