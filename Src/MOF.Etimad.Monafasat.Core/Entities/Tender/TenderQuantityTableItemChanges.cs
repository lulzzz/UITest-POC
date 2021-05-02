namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class TenderQuantityTableItemChanges
    {
        public long Id { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int? ActivityTemplateId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public long ItemNumber { get; set; }
        public int ItemChangeStatusId { get; private set; }

        #region Constructors
        public TenderQuantityTableItemChanges()
        {
        }
        public TenderQuantityTableItemChanges(long id, long columnId, long? tenderFormHeaderId, int? activityTemplateId, string columnName, string value, long itemNumber/*, Enums.TableChangeStatus changeStatus*/)
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
            ItemNumber = itemNumber;
        }

        #endregion
    }
}
