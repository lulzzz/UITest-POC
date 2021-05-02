namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QuantityTableItemForOpeningModel
    {
        public int Id { get; set; }
        public string IdString { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string OriginalItemId { get; set; }
        public bool hasAlternative { get; set; }
        public bool isAlternative { get; set; }
        public string ItemAttachmentId { get; set; }
        public string ItemAttachmentName { get; set; }
        public decimal AdjustedPrice { get; set; }
        public decimal AdjustedDiscount { get; set; }
        public decimal AdjustedVat { get; set; }
        public decimal AdjustedFinalPrice { get; set; }
    }
}
