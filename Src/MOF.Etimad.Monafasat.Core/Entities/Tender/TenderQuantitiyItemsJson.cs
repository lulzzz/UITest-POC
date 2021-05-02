using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderQuantitiyItemsJson", Schema = "Tender")]
    public class TenderQuantitiyItemsJson : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public long TenderQuantityTableId { get; set; }
        [ForeignKey(nameof(TenderQuantityTableId))]

        public TenderQuantityTable TenderQuantityTable { get; set; }
        public List<TenderQuantityTableItem> TenderQuantityTableItems { set; get; } = new List<TenderQuantityTableItem>();

        public TenderQuantitiyItemsJson()
        {
        }

        public TenderQuantitiyItemsJson(long tableId)
        {
            TenderQuantityTableId = tableId;
            EntityCreated();
        }

        public void SetAddedState()
        {
            Id = 0;
            EntityCreated();
        }

        public void SetUpdateState()
        {
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }
        public void Delete(int itemNumber)
        {
            TenderQuantityTableItems.Where(a => a.ItemNumber == itemNumber).ToList().ForEach(a => TenderQuantityTableItems.Remove(a));
            TenderQuantityTableItems.Where(a => a.ItemNumber > itemNumber).ToList().ForEach(a => a.ItemNumber = (a.ItemNumber - 1));
            EntityUpdated();
        }
    }
}
