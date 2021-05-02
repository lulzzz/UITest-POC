namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
{
    public class NegotiationSupplierQuantityTableItem
    {
        public long Id { get; set; }
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int? ActivityTemplateId { get; set; }
        public long ItemNumber { get; set; }
        public string Value { get; set; }

        #region [Constructors]
        public NegotiationSupplierQuantityTableItem()
        {

        }
        public NegotiationSupplierQuantityTableItem(long id, long columnId, long? tenderFormHeaderId, int? activityTemplateId, string value, long itemNumber)
        {
            Id = id;
            ColumnId = columnId;
            TenderFormHeaderId = tenderFormHeaderId;
            ActivityTemplateId = activityTemplateId;
            Value = value;
            ItemNumber = itemNumber;
        }

        #endregion
    }

}
