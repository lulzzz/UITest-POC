using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("CommitteeType", Schema = "LookUps")]
    public class CommitteeType
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommitteeTypeId { get; private set; }

        [StringLength(500)]
        public string NameAr { get; private set; }

        [StringLength(500)]
        public string NameEn { get; private set; }

        public List<Committee> Committees { private set; get; }
        #endregion

        #region NotMapped


        #endregion

    }
}