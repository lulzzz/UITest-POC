using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Template", Schema = "LookUps")]
    public class Template
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivitytemplatId { get; private set; }
        [StringLength(1024)]
        public string NameAr { get; private set; }
        [StringLength(1024)]
        public string NameEn { get; private set; }
        public bool? HasYears { get; private set; }
        public List<ConditionTemplateActivities> ConditionTemplateActivities { get; private set; } = new List<ConditionTemplateActivities>();
        #endregion
        #region NotMapped
        #endregion
    }

}