using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
{
    [Table("NegotiationQuantityTableItem", Schema = "CommunicationRequest")]
    public class NegotiationQuantityTableItem : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int NegotiationQuantityTableId { get; set; }
        //  public int SupplierQuantityTableItemId { get; set; }
        public int SupplierQuantityTableItemId { get; set; }
        //  public int TenderQuantityTableItemId { get; set; }

        public int Quantity { get; private set; }
        public string Unit { get; private set; }
        public string Name { get; private set; }
        public string Price { get; private set; }

        public int originalQTY { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Details { get; set; }
        public string FileRefId { get; set; }
        public string FileName { get; set; }

        #region [Navigation]
        [ForeignKey(nameof(NegotiationQuantityTableId))]
        public NegotiationQuantityTable NegotiationQuantityTable { get; set; }
        //[ForeignKey(nameof(SupplierQuantityTableItemId))]
        //public SupplierQuantityTableItem SupplierQuantityTableItem { get; set; }


        //[ForeignKey(nameof(SupplierQuantityTableItemId))]
        //public QuantityTableItem TenderQuantityTableItem { get; set; }
        ///public NegotiationSupplierQuantityTableItem  NegotiationSupplierQuantityTable { get; set; }

        #endregion
        #region [Constructors]
        public NegotiationQuantityTableItem()
        {

        }
        public NegotiationQuantityTableItem(int _supplierQuantityTableItemID, int _NegotiationQuantityTableId, int _Quantity, string _unit, string name, string attachmentName, string attachmentRefId, string price, string details)
        {
            this.NegotiationQuantityTableId = _NegotiationQuantityTableId;
            this.SupplierQuantityTableItemId = _supplierQuantityTableItemID;
            this.Unit = _unit;
            originalQTY = _Quantity;
            IsDeleted = false;
            this.Name = name;
            Price = price;
            this.FileRefId = attachmentRefId;
            this.FileName = attachmentName;
            this.Details = details;
            this.Quantity = _Quantity;
            EntityCreated();
        }
        #endregion
        #region [Methods]


        public void DeActive()
        {
            this.IsActive = false;
            EntityUpdated();
        }
        public void Delete()
        {
            // this.NegotiationSupplierQuantityTable.Delete();
            EntityDeleted();
        }
        public void UpdateItemValues(string unit, int Quantity)
        {
            this.Unit = unit;
            this.Quantity = Quantity;
            EntityUpdated();
            //   this.NegotiationQuantityTable.UpdateTotals()
        }

        public void deleteQuantityItem()
        {
            this.IsDeleted = true;
            EntityUpdated();
        }

        public void updateQTY(int qTY)
        {
            this.Quantity = qTY;

        }
        #endregion

    }

}
