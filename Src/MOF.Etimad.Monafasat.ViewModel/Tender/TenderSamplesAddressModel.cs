using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderSamplesAddressModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        [StringLength(1024)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        public string SamplesDeliveryAddress { get; set; }
    }
}
