using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BiddingRoundStatus", Schema = "LookUps")]
    public class BiddingRoundStatus
    {
        #region Fields====================================================

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BiddingRoundStatusId { get; private set; }

        [StringLength(1024)]
        public string Name { get; private set; }

        #endregion
    }
}
