using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderUnitStatus", Schema = "LookUps")]
    public class TenderUnitStatus
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TenderUnitStatusId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        #endregion

        #region Constructors
        public TenderUnitStatus(int tenderUnitStatusId, string name)
        {
            TenderUnitStatusId = tenderUnitStatusId;
            Name = name;
        }
        #endregion
    }
}
