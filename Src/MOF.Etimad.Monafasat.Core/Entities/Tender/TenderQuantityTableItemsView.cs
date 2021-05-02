namespace MOF.Etimad.Monafasat.Core.Entities
{
    //[Table("VW_TendersQTJsonItems", Schema = "Tender")]
    public class TenderQuantityTableItemView
    {
        public long QTableId { get; set; }
        public long QTableItemsId { get; set; }
        public long Id { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int? ActivityTemplateId { get; set; }
        public string Value { get; set; }
        public string ColumnName { get; set; }
        public long? ItemNumber { get; set; }
    }
}
