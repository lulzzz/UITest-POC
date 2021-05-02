namespace MOF.Etimad.Monafasat.ViewModel.CommunicationRequest.ExtendOfferValidity
{
    public class QuantityTableModel
    {
        public string TableQuantityName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPriceAfterDiscount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal OpeningTotalPrice { get; set; }
        public decimal OpeningDiscount { get; set; }
        public decimal OpeningVat { get; set; }

    }
}
