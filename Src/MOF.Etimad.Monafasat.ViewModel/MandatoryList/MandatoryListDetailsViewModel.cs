using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MandatoryListDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonNameAr), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public string DivisionNameAr { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonNameEn), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public string DivisionNameEn { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonCode), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public string DivisionCode { get; set; }

        public List<MandatoryListProductViewModel> Products { get; set; }
    }
}
