using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage
{
    public class QuantityTableforInsertModel
    {
        public string offerIdString { get; set; }
        public string tenderIdString { get; set; }

        [Display(Name = "Name", ResourceType = typeof(MOF.Etimad.Monafasat.Resources.SharedResources.DisplayInputs))]
        [Required(ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "Required")]
        public string QuantityName { get; set; }


        [Display(Name = "Price", ResourceType = typeof(Resources.SharedResources.DisplayInputs))]
        [Range(0.01, 9999999999999999.99, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "RequiredDecimal")]
        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?$)?", ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "RequiredCorrectDecimal")]
        public decimal Price { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(Resources.SharedResources.DisplayInputs))]
        [Range(0.00, 9999999999999999.99, ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "RequiredDecimal")]
        [RegularExpression(@"[0-9]+(\.[0-9][0-9]?$)?", ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "RequiredCorrectDecimal")]
        public decimal Discount { get; set; }
    }
}
