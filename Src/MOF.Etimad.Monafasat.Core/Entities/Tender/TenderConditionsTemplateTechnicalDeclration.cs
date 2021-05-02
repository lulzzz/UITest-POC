using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderConditionsTemplateTechnicalDelrations", Schema = "Tender")]
    public class TenderConditionsTemplateTechnicalDeclration : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderConditionsTemplateTechnicalDeclrationId { get; private set; }
        [Required]
        [StringLength(2000)]
        public string Term { get; private set; }
        [Required]
        [StringLength(2000)]
        public string Decleration { get; private set; }

        [ForeignKey(nameof(ConditionsTemplate))]
        public int ConditionsTemplateId { get; private set; }
        public TenderConditionsTemplate ConditionsTemplate { private set; get; }

        #endregion

        #region Constructors====================================================

        public TenderConditionsTemplateTechnicalDeclration()
        {

        }
        public TenderConditionsTemplateTechnicalDeclration(int tenderConditionsTemplateTechnicalDeclrationId, string term, string decleration)
        {
            TenderConditionsTemplateTechnicalDeclrationId = tenderConditionsTemplateTechnicalDeclrationId;
            Term = term;
            Decleration = decleration;
            EntityCreated();
        }
        public TenderConditionsTemplateTechnicalDeclration(string term, string decleration)
        {
            Term = term;
            Decleration = decleration;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================

        public void Update(string term, string decleration)
        {
            Term = term;
            Decleration = decleration;

            EntityUpdated();
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void SetAddedState()
        {
            TenderConditionsTemplateTechnicalDeclrationId = 0;
            EntityCreated();
        }
        #endregion
    }
}