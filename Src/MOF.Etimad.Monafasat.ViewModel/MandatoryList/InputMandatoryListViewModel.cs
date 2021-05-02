using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class InputMandatoryListViewModel
    {
        public string Id { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonNameAr), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.StringMaximum))]
        [RegularExpression(RegexHelper.ARABIC_ONLY, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.ArabicOnly))]
        public string DivisionNameAr { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonNameEn), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [RegularExpression(RegexHelper.ENGLISH_ONLY, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.EnglishOnly))]
        [StringLength(maximumLength: 50, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.StringMaximum))]
        public string DivisionNameEn { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonCode), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [StringLength(maximumLength: 4, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.IntMaximum))]
        [RegularExpression(RegexHelper.POSITIVE_INT_ONLY, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.NumbersOnly))]
        public string DivisionCode { get; set; }

        public bool SendToApproval { get; set; }
        public int StatusId { get; set; }

        public List<InputMandatoryListProductViewModel> Products { get; set; }
    }
}
