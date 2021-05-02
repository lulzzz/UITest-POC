namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AlternativeSupplierQuantityTableItemForCheckingModel
    {
        public bool isSelected { get; set; }
        public long Id { get; set; }
        public string IdString { get; set; }

        // [DisplayName("رقم الجدول")]
        public int SupplierTableQuantityId { get; set; }

        //  [DisplayName("رقم المنافسة")]
        public int TenderQuantityTableItemId { get; set; }

        // [DisplayName("سعر الوحدة")]
        public decimal Price { get; set; }
        //   [DisplayName("الخصم للوحدة")]
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }
        public string ItemAttachmentId { get; set; }
        public string ItemAttachmentName { get; set; }
        public bool hasAlternative { get; set; }
        public bool isAlternative { get; set; }


        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }

        public string OriginalItemId { get; set; }
        //  [DisplayName("المجموع")]
        public decimal Total { get; set; }

        //  [DisplayName("المجموع بعد الخصم")]
        public decimal DiscountedTotal { get; set; }
        // public QuantityTableItemModel TenderQuantityTableItem { get; set; }

        public decimal OpeningVAT { get; set; }
        public decimal OpeningDiscount { get; set; }
        public decimal OpeningPrice { get; set; }
        public decimal OpenningFinalAmount { get; set; }
    }
}
