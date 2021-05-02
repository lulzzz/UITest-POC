using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierQuantityTableItem", Schema = "Offer")]
    public class SupplierQuantityTableItem : AuditableEntity
    {
        public SupplierQuantityTableItem()
        {
            //EntityCreated();
        }
        public SupplierQuantityTableItem(
      int tenderQuantityTableItemId,
      string price,
      string discount,
       QuantityTableItem xxtem = null)

        {

            TenderQuantityTableItemId = tenderQuantityTableItemId;
            Price = price;
            Discount = discount;
            TenderQuantityTableItem = xxtem;
            ItemAttachmentId = xxtem.ItemAttachmentId;
            ItemAttachmentName = xxtem.ItemAttachmentName;
            Name = xxtem.Name;
            Quantity = xxtem.Quantity;
            Unit = xxtem.Unit;
            if (xxtem == null)
            {
                TenderQuantityTableItemId = null;
            }
            EntityCreated();
        }


        public SupplierQuantityTableItem(
     int tenderQuantityTableItemId,
     int supplierTableId,
     string price,
     string discount,
     string itemAttachmentId, string itemAttachmentName, string name, int quantity, string unit, int originalItemId, decimal vat)

        {
            SupplierTableQuantityId = supplierTableId;
            TenderQuantityTableItemId = tenderQuantityTableItemId;
            Price = price;
            Discount = discount;
            AdjustedDiscount = discount;
            AdjustedPrice = price;
            AdjustedVAT = vat;
            ItemAttachmentId = itemAttachmentId;
            ItemAttachmentName = itemAttachmentName;
            Name = name;
            Quantity = quantity;
            Unit = unit;
            VAT = vat;
            OriginalItemId = originalItemId;
            isSelected = false;
            EntityCreated();
        }
        public void UpdateSupplierQuantityTableItem(
  int tenderQuantityTableItemId,
  string price,
  string discount,
  string itemAttachmentId, string itemAttachmentName, string name, int quantity, string unit, decimal vat)

        {
            AdjustedDiscount = discount;
            AdjustedPrice = price;
            AdjustedVAT = vat;
            TenderQuantityTableItemId = tenderQuantityTableItemId;
            Price = price;
            Discount = discount;
            ItemAttachmentId = itemAttachmentId;
            ItemAttachmentName = itemAttachmentName;
            Name = name;
            Quantity = quantity;
            Unit = unit;
            VAT = vat;
            EntityUpdated();
        }




        //public SupplierQuantityTableItem(int tenderQuantityTableItemId,string price,string discount, QuantityTableItem xxtem = null)
        //{
        //    TenderQuantityTableItemId = tenderQuantityTableItemId;
        //    Price = price;
        //    //Price = Util.EncryptString(price.ToString(), "101010");
        //    Discount = discount;
        //    TenderQuantityTableItem = xxtem;
        //    if (xxtem == null)
        //    {
        //        TenderQuantityTableItemId = null;
        //    }
        //    EntityCreated();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public int SupplierTableQuantityId { get; private set; }
        [Required]
        public int? TenderQuantityTableItemId { get; private set; }
        [ForeignKey("TenderQuantityTableItemId")]
        public QuantityTableItem TenderQuantityTableItem { get; private set; }
        [ForeignKey("SupplierTableQuantityId")]
        public SupplierQuantityTable SupplierQuantityTable { get; private set; }

        public string ItemAttachmentId { get; private set; }
        public string ItemAttachmentName { get; private set; }
        public int? OriginalItemId { get; private set; }

        [ForeignKey("OriginalItemId")]
        public SupplierQuantityTableItem OriginalQuantityItem { get; private set; }
        public string Name { get; private set; }

        public int Quantity { get; private set; }
        public decimal VAT { get; private set; } = 0;
        public string Price { get; private set; }
        public string Discount { get; private set; }
        public string Unit { get; private set; }


        public decimal AdjustedVAT { get; private set; } = 0;
        public string AdjustedPrice { get; private set; }
        public string AdjustedDiscount { get; private set; }





        public bool isSelected { get; private set; } = true;
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }
        public void DeleteItemWithTable()
        {
            SupplierQuantityTable.Delete();
            EntityDeleted();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        public void UpdatePrice(string newPrice)
        {
            //Price = Util.EncryptString(newPrice.ToString(), "E546C8DF278CD5931069B522E695D4F2");
            Price = newPrice;
            EntityUpdated();
        }
        public void UpdateDiscount(string newDiscount)
        {
            Discount = newDiscount;
            EntityUpdated();
        }
        public void UpdateVAT(decimal newVAT)
        {
            VAT = newVAT;
            EntityUpdated();
        }

        public void UpdateOpeningPrice(string newOpeningPrice)
        {
            AdjustedPrice = newOpeningPrice;
            EntityUpdated();
        }
        public void UpdateOpeningDiscount(string newOpeningDiscount)
        {
            AdjustedDiscount = newOpeningDiscount;
            EntityUpdated();
        }
        public void UpdateOpeningVAT(decimal newOpeningVAT)
        {
            AdjustedVAT = newOpeningVAT;
            EntityUpdated();
        }
        public void EncryptDataForMigration(string price, string discount)
        {
            Price = price;
            Discount = discount;
            UpdatedBy = "Encrypted";
            State = ObjectState.Modified;
        }

        public void Select()
        {
            isSelected = true;
            EntityUpdated();
        }

        public void Unselect()
        {
            isSelected = false;
            EntityUpdated();
        }

        [NotMapped]
        public decimal FinalAmountAfterDiscountandVAT
        {
            get
            {
                return (Util.DecryptNewDecimal(this.Price) * this.Quantity - ((Util.DecryptNewDecimal(this.Price) * this.Quantity) * (Util.DecryptNewDecimal(this.Discount) / 100))) +
                    ((Util.DecryptNewDecimal(this.Price) * this.Quantity - ((Util.DecryptNewDecimal(this.Price) * this.Quantity) * (Util.DecryptNewDecimal(this.Discount) / 100))) * (this.VAT / 100));

            }
        }
        [NotMapped]
        public decimal OpenningFinalAmount
        {
            get
            {
                return (Util.DecryptNewDecimal(this.AdjustedPrice) * this.Quantity - ((Util.DecryptNewDecimal(this.AdjustedPrice) * this.Quantity) * (Util.DecryptNewDecimal(this.AdjustedDiscount) / 100))) +
                    ((Util.DecryptNewDecimal(this.AdjustedPrice) * this.Quantity - ((Util.DecryptNewDecimal(this.AdjustedPrice) * this.Quantity) * (Util.DecryptNewDecimal(this.AdjustedDiscount) / 100))) * (this.AdjustedVAT / 100));

            }
        }
    }
}