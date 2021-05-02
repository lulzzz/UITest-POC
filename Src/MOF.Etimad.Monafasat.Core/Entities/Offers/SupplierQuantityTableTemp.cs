using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierQuantityTableTemp", Schema = "Supplier")]
    public class SupplierQuantityTableTemp :AuditableEntity
    {
        #region Fields

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableQuantityId { get; private set; }

        [StringLength(100), Required]
        public string TableQuantityName { get; private set; }

        public decimal TotalPrice { get; private set; }


        public decimal TotalPriceAfterDiscount { get; private set; }
        public decimal Discount { get; private set; }

        public int OfferId { get; private set; }

        [ForeignKey("OfferId")]
        public Offer Offer { get; private set; }
        public int SupplierQuantityTableId { get; private set; }

        [ForeignKey("SupplierQuantityTableId")]
        public SupplierQuantityTable SupplierQuantityTable { get; private set; }
        public List<SupplierQuantityTableItemsTemp> QuantityTableItem { get; private set; } = new List<SupplierQuantityTableItemsTemp>();
        #endregion


        #region Constractor  
        public SupplierQuantityTableTemp()
        {

        }

        public SupplierQuantityTableTemp(string tableQuantityName,decimal totalPrice,decimal discount,bool? isActive, List<SupplierQuantityTableItemsTemp> quantityTableItem, int OfferId = 0)
        {
            TableQuantityName = tableQuantityName;
            IsActive = isActive;
            this.OfferId = OfferId;
            QuantityTableItem = quantityTableItem;
            TotalPrice = totalPrice;
            Discount = discount;
            EntityCreated();
        }
        public SupplierQuantityTableTemp(string tableQuantityName,decimal totalPrice,decimal discount,bool? isActive, List<SaveitemModel> quantityTableItem,int supplierQuantityTableId, int OfferId = 0)
        {
            TableQuantityName = tableQuantityName;
            IsActive = isActive;
            this.OfferId = OfferId;
            SupplierQuantityTableId = supplierQuantityTableId;
            //quantityTableItem.ForEach(x=> QuantityTableItem.Add(new SupplierQuantityTableItemsTemp(x.Id,x.Price,x.Discount,x.itemQuantity, x.Unit)));
            TotalPrice = totalPrice;
            Discount = discount;
            EntityCreated();

        }

        public SupplierQuantityTableTemp(string tableQuantityName, decimal Price, decimal discount, int OfferId = 0)
        {
            TableQuantityName = tableQuantityName;
            IsActive = true;
            this.OfferId = OfferId;
            TotalPrice = Price;
            TotalPriceAfterDiscount = Price - (Price * (discount / 100));
            Discount = discount;
            EntityCreated();
        }
        #endregion

        #region Methods
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void UpdateQuantityTable(string tableName, List<SaveitemModel> QuantityTableItem)
        {
            TableQuantityName = tableName;
            TotalPrice = CalculateTotalPrice(QuantityTableItem);
            Discount = CalculateTotalDiscount(QuantityTableItem);
            TotalPriceAfterDiscount = CalculateTotalQuantityAmountAfetrDiscountAndVAT(QuantityTableItem);
            EntityUpdated();
        }


        public void UpdateTableName(string tableName, decimal totalPrice, decimal discount)
        {
            TableQuantityName = tableName;
            TotalPrice = totalPrice;
            Discount = discount;
            EntityUpdated();
        }

        private decimal CalculateTotalQuantityAmountAfetrDiscountAndVAT(List<SaveitemModel> supplierQuantityTableItems)
        {
            //decimal finalAmount = supplierQuantityTableItems.Select(d => (d.Price * d.itemQuantity - ((d.Price * d.itemQuantity) * (d.Discount / 100))) +
            //((d.Price * d.itemQuantity - ((d.Price * d.itemQuantity) * (d.Discount / 100))) * (d.VAT / 100))).Sum();
            //return finalAmount;
            return 0;
        }

        private decimal CalculateTotalPrice(List<SaveitemModel> supplierQuantityTableItems)
        {
            //decimal TotalPrice = supplierQuantityTableItems.Select(d => d.Price * d.itemQuantity).Sum();
            //return TotalPrice;
            return 0;
        }
        private decimal CalculateTotalDiscount(List<SaveitemModel> supplierQuantityTableItems)
        {
            decimal TotalDiscountValue = 0;// supplierQuantityTableItems.Select(d => (d.Price * d.itemQuantity) * (d.Discount / 100)).Sum();
            decimal TotalDiscountPercent = TotalDiscountValue / CalculateTotalPrice(supplierQuantityTableItems) * 100;
            return TotalDiscountPercent;
        }
        #endregion

    }
}
