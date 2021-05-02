using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
{

    [Table("NegotiationQuantityItemJson", Schema = "CommunicationRequest")]
    public class NegotiationQuantityItemJson : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public long NegotiationSupplierQuantityTableId { get; set; }
        [ForeignKey(nameof(NegotiationSupplierQuantityTableId))]

        public NegotiationSupplierQuantityTable NegotiationSupplierQuantityTable { get; set; }
        public List<NegotiationSupplierQuantityTableItem> NegotiationSupplierQuantityTableItems { set; get; } = new List<NegotiationSupplierQuantityTableItem>();

        public NegotiationQuantityItemJson()
        {
        }

        public NegotiationQuantityItemJson(long tableId)
        {
            NegotiationSupplierQuantityTableId = tableId;
            EntityCreated();
        }
        public void Delete()
        {
            EntityDeleted();
        }
        public void Delete(long itemNumber)
        {
            NegotiationSupplierQuantityTableItems.Where(a => a.ItemNumber == itemNumber).ToList().ForEach(a => NegotiationSupplierQuantityTableItems.Remove(a));
            EntityUpdated();
        }
    }
}
