using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ConditionsTemplateSections", Schema = "LookUps")]
    public class ConditionsTemplateSection
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConditionsTemplateSectionId { get; private set; }
        [StringLength(1024)]
        public string NameAr { get; private set; }
        [StringLength(1024)]
        public string NameEn { get; private set; }
        public List<ConditionTemplateActivities> ConditionTemplateActivities { get; private set; } = new List<ConditionTemplateActivities>();

        #endregion
        #region NotMapped
        #endregion
    }

}