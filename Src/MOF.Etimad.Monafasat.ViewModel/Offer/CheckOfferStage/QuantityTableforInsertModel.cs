using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CheckOfferQuantityTableforInsertModel
    {
        public string offerIdString { get; set; }
        public string tenderIdString { get; set; }

        [Display(Name = "Name", ResourceType = typeof(MOF.Etimad.Monafasat.Resources.SharedResources.DisplayInputs))]
        [Required(ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "Required")]
        public string QuantityName { get; set; }


        [Display(Name = "Price", ResourceType = typeof(Resources.SharedResources.DisplayInputs))]
        [Required(ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "Required")]
        public decimal Price { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(Resources.SharedResources.DisplayInputs))]
        [Required(ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "Required")]
        public decimal Discount { get; set; }
    }
}
