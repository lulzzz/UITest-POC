namespace MOF.Etimad.Monafasat.ViewModel
{
    public class FinancialOpenOfferDetails
    {
        public string OfferIdString { get; set; }
        public string Notes { get; set; }
        public string Discount { get; set; }
        public string DiscountNotes { get; set; }
        public decimal? FinalPrice { set; get; }
    }
}
