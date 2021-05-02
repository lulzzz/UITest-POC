using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage
{
    public class TenderDataTabModel
    {
        public string TenderName { get; set; }
        public string RefNumber { get; set; }
        public string tenderIdString { get; set; }
        public int TenderTypeId { get; set; }
        public int tenderStatusId { get; set; }
        public Enums.TenderStatus tenderStatus { get; set; }
        public Enums.OfferPresentationWayId OfferPresentationWayId { get; set; }
    }
}
