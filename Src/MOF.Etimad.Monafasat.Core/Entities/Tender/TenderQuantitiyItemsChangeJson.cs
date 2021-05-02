using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderQuantitiyItemsChangeJson", Schema = "Tender")]
    public class TenderQuantitiyItemsChangeJson : BaseEntity
    {
        [Key]
        public long Id { get; private set; }
        public long TenderQuantityTableChangeId { get; private set; }
        [ForeignKey(nameof(TenderQuantityTableChangeId))]
        public TenderQuantityTableChanges TenderQuantityTableChanges { get; private set; }
        public List<TenderQuantityTableItemChanges> TenderQuantityTableItemChanges { private set; get; } = new List<TenderQuantityTableItemChanges>();

        public TenderQuantitiyItemsChangeJson()
        {
        }

        public TenderQuantitiyItemsChangeJson(long tableId)
        {
            TenderQuantityTableChangeId = tableId;
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
            TenderQuantityTableItemChanges.Where(a => a.ItemNumber == itemNumber).ToList().ForEach(a => TenderQuantityTableItemChanges.Remove(a));
            EntityUpdated();
        }
    }
}
