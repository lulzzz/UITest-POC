namespace MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage
{
    public class QuantityTableModel
    {
        public string QuantityTableName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPriceAfterDiscount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal AdjustedTotalPrice { get; set; }
        public decimal AdjustedDiscount { get; set; }
        public decimal AdjustedVat { get; set; }
        public decimal AdjustedFinalPrice { get; set; }
    }
}
