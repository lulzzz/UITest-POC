namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationOfferModel
    {
        public decimal? DiscountPercent { get; set; }
        public string CommercialCompanyName { get; set; }
        public string CommericalNumber { get; set; }
        public string OfferNumber { get; set; }
        public decimal OfferAmount { get; set; }
        public decimal OfferAmountNP { get; set; }
        public string BuyStatus { get; set; }
        public string OfferCheck { get; set; }
        public string TechnicalCheck { get; set; }
        public decimal AmountAfterNegotiationDiscount { get; set; }
        public string AmountAfterNegotiationDiscountText { get; set; }
        public decimal AmountAfterNegotiationDiscountNP { get; set; }
        public decimal ReductionPercent { get { return (OfferAmount - AmountAfterNegotiationDiscount); } }
        public bool IsTawreed { get; set; }
        public string offerIdString { get; set; }
        public string tenderIdString { get; set; }
        public string solidarityIdString { get; set; }
        public string statusName { get; set; }
        public int? statusId { get; set; }
        public int offerPresentationWay { get; set; }
    }
}
