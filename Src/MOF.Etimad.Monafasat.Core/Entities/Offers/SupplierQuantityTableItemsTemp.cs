using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierQuantityTableItemsTemp", Schema = "Supplier")]
    public class SupplierQuantityTableItemsTemp : AuditableEntity
    {

        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public int SupplierQuantityTableTempId { get; private set; }
        [Required]
        [ForeignKey("SupplierQuantityTableTempId")]
        public SupplierQuantityTableTemp SupplierQuantityTableTemp { get; private set; }

        public decimal Price { get; private set; }
        public decimal Discount { get; private set; }

        public string ItemAttachmentId { get; private set; }
        public string ItemAttachmentName { get; private set; }

        public decimal VAT { get; private set; }
        public string Name { get; private set; }

        public int Quantity { get; private set; }

        public string Unit { get; private set; }
        #endregion
        #region Constractor

        public SupplierQuantityTableItemsTemp()
        {
            //EntityCreated();
        }

        public SupplierQuantityTableItemsTemp(int tenderQuantityTableItemId,decimal price,decimal discount, QuantityTableItem xxtem = null)
        {
            //TenderQuantityTableItemId = tenderQuantityTableItemId;
            Price = price;
            Discount = discount;
            //TenderQuantityTableItem = xxtem;
            ItemAttachmentId = xxtem.ItemAttachmentId;
            ItemAttachmentName = xxtem.ItemAttachmentName;
            Name = xxtem.Name;
            Quantity = xxtem.Quantity;
            Unit = xxtem.Unit;
            //if (xxtem == null)
            //{
            //    TenderQuantityTableItemId = null;
            //}
            EntityCreated();
        }
        public SupplierQuantityTableItemsTemp(int supplierQuantityTableTempId, decimal price, decimal discount,int quantity , string unit)
        {
            SupplierQuantityTableTempId = supplierQuantityTableTempId;
            Price = price;
            Discount = discount;
            Quantity = quantity;
            Unit = unit;
            EntityCreated();
        }
        #endregion
        #region Methods
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
            EntityUpdated();
        }

        public void UpdateDiscountg(decimal newDiscount)
        {
            Discount = newDiscount;
            EntityUpdated();
        }

        public void UpdateVAT(decimal newVAT)
        {
            VAT = newVAT;
            EntityUpdated();
        }
        #endregion

    }
}
