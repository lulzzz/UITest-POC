using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class OfferMainVModel
    {
        [Required]
        public bool isSolidarity { get; set; } = false;
        public string CR { get; set; }
        public int OfferStatusId { get; set; }
        public int tenderTypeId { get; set; }
        public bool hasOffer { get; set; }
        public bool offerOwner { get; set; }
        public string tenderIdString { get; set; }
        public string offerStatusName { get; set; }
        public string offerIdString { get; set; }
        public string SolidarityMessage { get; set; }
    }
}
