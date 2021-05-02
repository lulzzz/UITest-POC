namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class OfferQuantityTableModel
    {
        public string OfferIdString { get; set; }
        public bool IsSolidarity { get; set; }
        public OfferStatusModel OfferStatusModel { get; set; }

        public QuantityTableStepDTO QTStep { get; set; }
        public QuantitiesTemplateModel QuantitiesTemplateModel { get; set; }
    }
}
