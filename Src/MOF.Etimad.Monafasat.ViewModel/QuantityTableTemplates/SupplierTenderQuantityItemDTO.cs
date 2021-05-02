namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierTenderQuantityItemDTO
    {
        public long TableId { get; set; }
        public long TenderItemId { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int TenderId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public long ItemNumber { get; set; }
        public string TableName { get; set; }
        public int? TemplateId { get; set; }
    }
}