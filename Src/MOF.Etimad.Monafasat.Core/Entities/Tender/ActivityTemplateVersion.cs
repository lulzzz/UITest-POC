using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ActivityTemplateVersion", Schema = "Tender")]
    public class ActivityTemplateVersion : BaseEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Activity))]
        public int ActivityId { get; private set; }
        public Activity Activity { private set; get; }

        [ForeignKey(nameof(Template))]
        public int? TemplateId { get; private set; }
        public Template Template { private set; get; }

        [ForeignKey(nameof(Version))]
        public int VersionId { get; private set; }
        public VersionHistory Version { private set; get; }
        public bool? HasYears { get; private set; }

        #endregion

        #region Constructors====================================================

        public ActivityTemplateVersion()
        {

        }
        #endregion

    }
}
