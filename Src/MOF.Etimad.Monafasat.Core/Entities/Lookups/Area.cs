using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Area", Schema = "LookUps")]
    public class Area
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AreaId { get; private set; }

        [StringLength(1024)]
        public string NameEn { get; private set; }

        [StringLength(1024)]
        public string NameAr { get; private set; }

        public List<TenderArea> TenderAreas { private set; get; }
        #endregion

    }
}