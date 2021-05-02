namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class SupplierQItemModel
    {
        public string HtmlBody { get; set; }
    }
    public class SupplierQuantityTableItemDTO
    {

        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int TenderId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public long ItemNumber { get; set; }
        public long SupplierId { get; set; }


    }
}
