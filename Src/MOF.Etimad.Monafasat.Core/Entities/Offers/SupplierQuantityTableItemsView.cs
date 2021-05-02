namespace MOF.Etimad.Monafasat.Core.Entities
{
    //[Table("VW_QTJsonItems", Schema = "Offer")]
    public class SupplierQuantityTableItemView
    {
        public long QTableId { get; set; }
        public long QTableItemsId { get; set; }
        public long Id { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int? ActivityTemplateId { get; set; }
        public string Value { get; set; }
        public string AlternativeValue { get; set; }
        public bool IsDefault { get; set; } = true;
        public long? ItemNumber { get; set; }
    }
}
