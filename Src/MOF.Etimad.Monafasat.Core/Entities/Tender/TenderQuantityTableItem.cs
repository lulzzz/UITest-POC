namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class TenderQuantityTableItem 
    {
        public long Id { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int? ActivityTemplateId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public long ItemNumber { get; set; }

        #region Constructors
        public TenderQuantityTableItem()
        {
        }
        public TenderQuantityTableItem(long id, long columnId, long? tenderFormHeaderId, int? activityTemplateId, string columnName, string value, long itemNumber)
        {
            Id = id;
            ColumnId = columnId;
            TenderFormHeaderId = tenderFormHeaderId;
            ActivityTemplateId = activityTemplateId;
            ColumnName = columnName;
            Value = value;
            ItemNumber = itemNumber;
        }

        public void UpdateItems(long columnId, long? tenderFormHeaderId, int? activityTemplateId, string columnName, string value, long itemNumber)
        {
            ColumnId = columnId;
            TenderFormHeaderId = tenderFormHeaderId;
            ActivityTemplateId = activityTemplateId;
            ColumnName = columnName;
            Value = value;
        }

        #endregion
    }
}
