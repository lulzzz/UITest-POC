namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderQuantityItemDTO
    {
        public long TableId { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int TenderId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public string AlternativeValue { get; set; }
        public long ItemNumber { get; set; }
        public string TableName { get; set; }
        public int? TemplateId { get; set; }
        public long Id { get; set; }
        public bool IsDefault { get; set; }
        //public int? ItemChangeStatusId { get; set; }
        public bool? IsTableDeleted { get; set; }
        public bool? IsNewTable { get; set; }
    }
}