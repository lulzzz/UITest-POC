using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderConditionsTemplateTechnicalOutput", Schema = "Tender")]
    public class TenderConditionsTemplateTechnicalOutput : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderConditionsTemplateTechnicalOutputId { get; private set; }
        [Required]
        [StringLength(1000)]
        public string OutputStage { get; private set; }
        [Required]
        [StringLength(1000)]
        public string OutputName { get; private set; }
        [Required]
        [StringLength(2000)]
        public string OutputDescriptions { get; private set; }
        [Required]
        [StringLength(100)]
        public string OutputDeliveryTime { get; private set; }

        [ForeignKey(nameof(ConditionsTemplate))]
        public int ConditionsTemplateId { get; private set; }
        public TenderConditionsTemplate ConditionsTemplate { private set; get; }

        #endregion

        #region Constructors====================================================

        public TenderConditionsTemplateTechnicalOutput()
        {

        }
        public TenderConditionsTemplateTechnicalOutput(int tenderConditionsTemplateTechnicalOutputId, string outState, string outputName, string outDescriptions, string outDeliveryTime)
        {
            TenderConditionsTemplateTechnicalOutputId = tenderConditionsTemplateTechnicalOutputId;
            OutputStage = outState;
            OutputName = outputName;
            OutputDescriptions = outDescriptions;
            OutputDeliveryTime = outDeliveryTime != null ? outDeliveryTime : "";
            EntityCreated();
        }
        public TenderConditionsTemplateTechnicalOutput(string outState, string outputName, string outDescriptions, string outDeliveryTime)
        {
            OutputStage = outState;
            OutputName = outputName;
            OutputDescriptions = outDescriptions;
            OutputDeliveryTime = outDeliveryTime;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================

        public void Update(string outState, string outputName, string outDescriptions, string outDeliveryTime)
        {
            OutputStage = outState;
            OutputName = outputName;
            OutputDescriptions = outDescriptions;
            OutputDeliveryTime = outDeliveryTime;

            EntityUpdated();
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void Delete()
        {
            EntityDeleted();
        }

        public void SetAddedState()
        {
            TenderConditionsTemplateTechnicalOutputId = 0;
            EntityCreated();
        }
        #endregion
    }
}