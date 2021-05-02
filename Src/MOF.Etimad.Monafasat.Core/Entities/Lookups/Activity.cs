using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Activity", Schema = "LookUps")]
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivityId { get; private set; }
        [StringLength(1024)]
        public string NameAr { get; private set; }
        [StringLength(1024)]
        public string NameEn { get; private set; }
        public int? ParentID { get; set; }
        [ForeignKey(nameof(ParentID))]
        public virtual Activity ParentActivity { get; set; }
        public virtual List<Activity> SubActivities { get; set; }
        public List<TenderActivity> TenderActivities { get; private set; }
        public List<ActivityTemplateVersion> ActivityTemplateVersions { get; set; } = new List<ActivityTemplateVersion>();
        public int? RelatedActivityId { get; set; }
    }

}