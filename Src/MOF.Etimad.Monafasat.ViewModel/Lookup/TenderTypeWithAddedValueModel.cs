using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderTypeWithAddedValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "BuyingCost", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public decimal BuyingCost { get; set; }
        [Display(Name = "InvitationCost", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public decimal InvitationCost { get; set; }
    }
}
