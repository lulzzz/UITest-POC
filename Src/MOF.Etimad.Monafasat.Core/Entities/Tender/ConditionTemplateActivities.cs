using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ConditionTemplateActivities", Schema = "Tender")]
    public class ConditionTemplateActivities : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(ActivityTemplate))]
        public int ActivityTemplateId { get; private set; }
        public Template ActivityTemplate { private set; get; }

        [ForeignKey(nameof(ConditionsTemplateSection))]
        public int ConditionsTemplateSectionId { get; private set; }
        public ConditionsTemplateSection ConditionsTemplateSection { private set; get; }


        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public ConditionTemplateActivities()
        {

        }
        #endregion

        #region Methods====================================================
        public void Delete()
        {
            EntityDeleted();
        }
        public void Update()
        {

            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetAddedState()
        {
            Id = 0;
            EntityCreated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        #endregion

    }
}
