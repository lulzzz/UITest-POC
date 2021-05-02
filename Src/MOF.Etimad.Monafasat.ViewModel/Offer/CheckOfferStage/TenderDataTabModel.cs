using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CheckOfferTenderDataTabModel
    {
        public string TenderName { get; set; }
        public string RefNumber { get; set; }
        public string tenderIdString { get; set; }

        public Enums.TenderStatus tenderStatus { get; set; }
        public int OfferPresentationWayId { get; set; }
    }
}
