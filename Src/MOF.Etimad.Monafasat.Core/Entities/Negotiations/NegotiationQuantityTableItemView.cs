namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class NegotiationQuantityTableItemView
    {
        public long NegoTableId { get; set; }
        public long QTableJsonId { get; set; }
        public long Id { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int? ActivityTemplateId { get; set; }
        public long? negotiationsupplierQTId { get; set; }
        public long? ItemNumber { get; set; }
        public string Value { get; set; }
    }
}
