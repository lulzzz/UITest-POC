namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class QuantityItemModel
    {
        public string QuantityTableItemIdString { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Details { get; set; }
        public int QTY { get; set; }
        public decimal VAT { get; set; }
        public string AttachmentId { get; set; }
        public string AttachmentReferenceId { get; set; }
        public string Unit { get; set; }
        public string AttachmentName { get; set; }

        public decimal FinalPrice { get; set; }
        public decimal TotalBeforDiscountPrice { get; set; }

    }
}
