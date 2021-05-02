using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderConditionsTemplateOutputsViewModel
    {
        public int TenderConditionsTemplateTechnicalOutputId { get; set; }
        [Required]
        [StringLength(200)]
        public string OutputStage { get; set; }
        [Required]
        [StringLength(200)]
        public string OutputName { get; set; }
        [Required]
        [StringLength(1000)]
        public string OutputDescriptions { get; set; }

        [StringLength(200)]
        public string OutputDeliveryTime { get; set; }
    }
}
