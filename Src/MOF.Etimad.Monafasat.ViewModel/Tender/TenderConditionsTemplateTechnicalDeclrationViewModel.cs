using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderConditionsTemplateTechnicalDeclrationViewModel
    {
        public int TenderConditionsTemplateTechnicalDeclrationId { get; set; }
        [Required]
        [StringLength(200)]
        public string Term { get; set; }
        [Required]
        [StringLength(1000)]
        public string Decleration { get; set; }
    }
}
