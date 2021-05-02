using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ConstructionWork", Schema = "LookUps")]
    public class ConstructionWork
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConstructionWorkId { get; private set; }
        [Required]
        [StringLength(1024)]
        public string NameAr { get; private set; }

        [StringLength(1024)]
        public string NameEn { get; private set; }

        public int? ParentID { get; set; }
        [ForeignKey("ParentID")]
        public virtual ConstructionWork ParentWork { get; set; }
        public virtual List<ConstructionWork> SubWorks { get; set; }
        public List<TenderConstructionWork> TenderConstructionWorks { private set; get; }
        #endregion

        #region NotMapped


        #endregion

    }
}