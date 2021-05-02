using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierTenderQuantityTableItem", Schema = "Offer")]
    public class SupplierTenderQuantityTableItem //: AuditableEntity
    {

        public long Id { get; set; }
        [Required]
        public long ColumnId { get; set; }
        public long? TenderFormHeaderId { get; set; }
        public int? ActivityTemplateId { get; set; }
        public long ItemNumber { get; set; }
        public string Value { get; set; }
        public string AlternativeValue { get; set; }
        public bool IsDefault { get; set; } = true;



        #region Constructors
        public SupplierTenderQuantityTableItem()
        {
        }

        public SupplierTenderQuantityTableItem(long id, long columnId, long? tenderFormHeaderId, int? activityTemplateId, string value, long itemNumber)
        {
            Id = id;
            ColumnId = columnId;
            TenderFormHeaderId = tenderFormHeaderId;
            ActivityTemplateId = activityTemplateId;
            Value = value;
            ItemNumber = itemNumber;

        }

        public void UpdateItems(long columnId, long? tenderFormHeaderId, int? activityTemplateId, string value, long itemNumber)
        {
            ColumnId = columnId;
            TenderFormHeaderId = tenderFormHeaderId;
            ActivityTemplateId = activityTemplateId;
            Value = value;
            ItemNumber = itemNumber;
        }

        public void UpdateValue(string _value)
        {
            this.Value = _value;
        }
        public void UpdateDefault(bool _default)
        {
            this.IsDefault = _default;
        }
        public void UpdateAlternativeValue(string _value)
        {
            this.AlternativeValue = _value;
        }
        public SupplierTenderQuantityTableItem(long _id, long columnId, long? tenderFormHeaderId, int? activityTemplateId, string value, long itemNumber, int TenderItemId)
        {
            Id = _id;
            ColumnId = columnId;
            TenderFormHeaderId = tenderFormHeaderId;
            ActivityTemplateId = activityTemplateId;
            Value = value;
            ItemNumber = itemNumber;

        }


        public void UpdateSupplierTenderQuantityTableItem(long id, long columnId, long? tenderFormHeaderId, int? activityTemplateId, string value, long itemNumber)
        {
            ColumnId = columnId;
            TenderFormHeaderId = tenderFormHeaderId;
            ActivityTemplateId = activityTemplateId;
            Value = value;
            ItemNumber = itemNumber;
        }
        #endregion


    }
}
