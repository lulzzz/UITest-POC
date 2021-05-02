namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferCheckingModel
    {
        public int OfferId { get; set; }
        public int TenderId { get; set; }
        public int? OfferAcceptanceStatusId { get; set; }

        public int? OfferTechnicalEvaluationStatusId { get; set; }
        public string Notes { get; set; }
        public bool? TenderAwardingType { get; set; }
        public decimal? TotalOfferAwardingValue { get; set; }
        public decimal? PartialOfferAwardingValue { get; set; }
        public string AwardingTypeName { get; set; }
    }
}
