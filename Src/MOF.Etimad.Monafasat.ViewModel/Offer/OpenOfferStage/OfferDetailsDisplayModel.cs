namespace MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage
{
    public class OfferDetailsDisplayModel
    {
        public string TenderIdString { get; set; }
        public int TenderTypeId { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public bool IsSolidarity { get; set; }
        public bool IsNegotiation { get; set; } = false;
        public string OfferIdString { get; set; }
        public string CombinedIdString { get; set; }
        public string Discount { get; set; }
        public string DiscountNotes { get; set; }
        public string PreviusAction { get; set; }
    }
}
