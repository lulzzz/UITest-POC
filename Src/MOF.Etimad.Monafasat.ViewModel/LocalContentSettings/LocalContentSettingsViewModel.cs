using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel.LocalContentSettings
{
    public class LocalContentSettingsViewModel
    {
        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.NationalProductPercentageAdvantage), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.PercentageOnly))]
        public decimal Rate { get; set; }

        [Display(Name = nameof(Resources.TenderResources.DisplayInputs.MinimumBaseline), ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? MinimumBaseline { get; set; }

        [Display(Name = nameof(Resources.TenderResources.DisplayInputs.MinimumPercentageLocalContentTarget), ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? MinimumPercentageLocalContentTarget { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.NationalProductPercentageAdvantage), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.PercentageOnly))]
        public decimal NationalProductPercentage { get; set; }
        
        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.HighValueContractsAmmount), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]

        public decimal? HighValueContractsAmmount { get; set; }
        
        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.LocalContentMaximumPercentage), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? LocalContentMaximumPercentage { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.PriceWeightAfterAdjustment), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? PriceWeightAfterAdjustment { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.LocalContentWeightAndFinancialMarket), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? LocalContentWeightAndFinancialMarket { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.BaselineWeight), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        public decimal? BaselineWeight { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.LocalContentTargetWeight), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        public decimal? LocalContentTargetWeight { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.FinancialMarketListedWeight), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        public decimal? FinancialMarketListedWeight { get; set; }

    }
}
