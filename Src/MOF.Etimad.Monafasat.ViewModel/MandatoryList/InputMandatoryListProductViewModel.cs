using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class InputMandatoryListProductViewModel
    {
        public string Id { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.CSICode), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [StringLength(maximumLength: 4, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.IntMaximum))]
        [RegularExpression(RegexHelper.POSITIVE_INT_ONLY, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.NumbersOnly))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public string CSICode { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.NameAr), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [StringLength(maximumLength: 100, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.StringMaximum))]
        [RegularExpression(RegexHelper.ARABIC_ONLY, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.ArabicOnly))]
        public string NameAr { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.NameEn), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [StringLength(maximumLength: 100, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.StringMaximum))]
        [RegularExpression(RegexHelper.ENGLISH_ONLY, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.EnglishOnly))]
        public string NameEn { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DescriptionAr), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [StringLength(maximumLength: 2000, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.StringMaximum))]
        [RegularExpression(RegexHelper.ARABIC_ONLY, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.ArabicOnly))]
        public string DescriptionAr { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DescriptionEn), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [StringLength(maximumLength: 2000, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.StringMaximum))]
        [RegularExpression(RegexHelper.ENGLISH_ONLY, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.EnglishOnly))]
        public string DescriptionEn { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.PriceCelling), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.PercentageOnly))]
        public double PriceCelling { get; set; }
    }
}
