using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierTenderQuantityTableItemJson", Schema = "Offer")]
    public class SupplierTenderQuantityTableItemJson : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        //[JsonProperty("SupplierTenderQuantityTableItems")]
        public List<SupplierTenderQuantityTableItem> SupplierTenderQuantityTableItems { set; get; } = new List<SupplierTenderQuantityTableItem>();
        public long SupplierTenderQuantityTableId { get; set; }
        [ForeignKey(nameof(SupplierTenderQuantityTableId))]
        public virtual SupplierTenderQuantityTable SupplierTenderQuantityTable { get; set; }

        public SupplierTenderQuantityTableItemJson()
        {
        }

        public SupplierTenderQuantityTableItemJson(List<SupplierTenderQuantityTableItem> items)
        {
            SupplierTenderQuantityTableItems.AddRange(items);
            EntityCreated();
        }

        public void Create()
        {
            EntityCreated();
        }
        public void Update()
        {
            EntityUpdated();
        }

        public void Delete()
        {
            EntityDeleted();
        }
        public void Delete(int itemNumber)
        {
            SupplierTenderQuantityTableItem item = SupplierTenderQuantityTableItems.FirstOrDefault(a => a.ItemNumber == itemNumber);
            if (item != null)
                SupplierTenderQuantityTableItems.Remove(item);
            EntityUpdated();
        }
        public void DeleteRow(int itemNumber)
        {
            List<SupplierTenderQuantityTableItem> items = SupplierTenderQuantityTableItems.Where(a => a.ItemNumber == itemNumber).ToList();
            foreach (var item in items)
            {
                SupplierTenderQuantityTableItems.Remove(item);
            }
            EntityUpdated();
        }

        public void UpadteSupplierQTableItems(List<SupplierTenderQuantityTableItem> supplierTenderQuantityTableItems)
        {
            foreach (var item in supplierTenderQuantityTableItems)
            {
                var SupplierItem = SupplierTenderQuantityTableItems.FirstOrDefault(s => s.Id == item.Id);
                if (SupplierItem == null)
                {
                    continue;
                }
                SupplierItem.UpdateValue(item.Value);
            }
            EntityUpdated();
        }
        public void RemoveAlternative()
        {
            foreach (var item in this.SupplierTenderQuantityTableItems)
            {
                item.AlternativeValue = "";
            }
            EntityUpdated();
        }
        public void UpadteSupplierQTableItemsAlternativeValue(List<SupplierTenderQuantityTableItem> supplierTenderQuantityTableItems)
        {
            foreach (var item in supplierTenderQuantityTableItems)
            {
                var SupplierItem = this.SupplierTenderQuantityTableItems.FirstOrDefault(s => s.Id == item.Id);
                if (SupplierItem == null)
                {
                    continue;
                }
                SupplierItem.UpdateAlternativeValue(item.AlternativeValue);
            }
            EntityUpdated();
        }

    }
}
