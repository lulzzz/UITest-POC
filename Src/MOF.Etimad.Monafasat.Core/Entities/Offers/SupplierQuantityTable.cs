using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierQuantityTable", Schema = "Offer")]
    public class SupplierQuantityTable : AuditableEntity
    {

        public SupplierQuantityTable()
        {

        }
        public SupplierQuantityTable(string tableQuantityName, decimal Price, decimal discount, int OfferId = 0)
        {
            TableQuantityName = tableQuantityName;
            IsActive = true;
            this.OfferId = OfferId;
            TotalPrice = Util.EncryptNew(Price);
            FinalPrice = Util.EncryptNew(Price - (Price * (discount / 100)));
            TotalDiscount = Util.EncryptNew(discount);
            EntityCreated();
        }

        public void UpdateQuantityTable(string tableName, List<SaveitemModel> QuantityTableItem)
        {
            var _TotalPrice = Util.EncryptNew(CalculateTotalPrice(QuantityTableItem));
            var _Discount = Util.EncryptNew(CalculateTotalDiscount(QuantityTableItem));
            var _OpeneningTotalVAT = CalculateTotalVAT(QuantityTableItem);

            TableQuantityName = tableName;
            TotalPrice = _TotalPrice;
            TotalDiscount = _Discount;
            AdjustedTotalDiscount = _Discount;
            AdjustedTotalPrice = _TotalPrice;
            AdjustedTotalVAT = _OpeneningTotalVAT;
            TotalVAT = _OpeneningTotalVAT;
            FinalPrice = Util.EncryptNew(CalculateTotalQuantityAmountAfetrDiscountAndVAT(QuantityTableItem));
            EntityUpdated();
        }

        public void UpdateOpeningQuantityTable(List<SaveitemModel> QuantityTableItem)
        {
            var _TotalPrice = Util.EncryptNew(CalculateTotalPrice(QuantityTableItem));
            var _Discount = Util.EncryptNew(CalculateTotalDiscount(QuantityTableItem));
            var _OpeneningTotalVAT = CalculateTotalVAT(QuantityTableItem);

            AdjustedTotalDiscount = _Discount;
            AdjustedTotalPrice = _TotalPrice;
            AdjustedTotalVAT = _OpeneningTotalVAT;
            EntityUpdated();
        }
        private decimal CalculateTotalQuantityAmountAfetrDiscountAndVAT(List<SaveitemModel> supplierQuantityTableItems)
        {
            decimal finalAmount = supplierQuantityTableItems.Select(d => (d.Price * d.Quantity - ((d.Price * d.Quantity) * (d.Discount / 100))) +
            ((d.Price * d.Quantity - ((d.Price * d.Quantity) * (d.Discount / 100))) * (d.VAT / 100))).Sum();

            return finalAmount;
        }
        private decimal CalculateTotalDiscount(List<SaveitemModel> supplierQuantityTableItems)
        {
            decimal TotalDiscountValue = supplierQuantityTableItems.Select(d => d.Discount).Sum();
            decimal TotalDiscountPercent = TotalDiscountValue == 0 ? 0 : (TotalDiscountValue / CalculateTotalPrice(supplierQuantityTableItems) * 100);
            return TotalDiscountValue;
        }
        private decimal CalculateTotalVAT(List<SaveitemModel> supplierQuantityTableItems)
        {
            decimal TotalVATValue = supplierQuantityTableItems.Select(d => d.VAT).Sum();
            return TotalVATValue;
        }
        private decimal CalculateTotalPrice(List<SaveitemModel> supplierQuantityTableItems)
        {
            decimal TotalPrice = supplierQuantityTableItems.Select(d => d.Price * d.Quantity).Sum();
            return TotalPrice;
        }



        #region [Update Totals For Alternative ]

        public void UpdateAllAlternative()
        {
            UpdateTotalAlternativePrice();
            UpdateTotalAlternativeVAT();
            UpdateTotalAlternativeDiscount();
            UpdateTotalQuantityAmountAfetrDiscountAndVAT();
            EntityUpdated();
        }
        private void UpdateTotalAlternativePrice()
        {
            this.AlternativeTotalPrice = Util.EncryptNew(this.QuantityTableItem.Where(d => d.OriginalItemId.HasValue).Select(d => Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity).Sum());
        }
        private void UpdateTotalAlternativeVAT()
        {
            this.AlternativeTotalVAT = Util.EncryptNew(this.QuantityTableItem.Where(d => d.OriginalItemId.HasValue).Select(d => ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity - ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity) * (Util.DecryptNewDecimal(d.AdjustedDiscount) / 100))) * (d.AdjustedVAT / 100))).Sum());
        }
        private void UpdateTotalAlternativeDiscount()
        {
            var TotalDiscountValue = this.QuantityTableItem.Where(d => d.OriginalItemId.HasValue).Select(d => (Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity) * (Util.DecryptNewDecimal(d.AdjustedDiscount) / 100)).Sum();

            if (this.QuantityTableItem.Where(d => d.OriginalItemId.HasValue).Select(d => ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity - ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity) * (Util.DecryptNewDecimal(d.AdjustedDiscount) / 100))) * (d.AdjustedVAT / 100))).Sum() != 0)
            {
                decimal TotalDiscountPercent = TotalDiscountValue / this.QuantityTableItem.Where(d => d.OriginalItemId.HasValue).Select(d => ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity - ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity) * (Util.DecryptNewDecimal(d.AdjustedDiscount) / 100))) * (d.AdjustedVAT / 100))).Sum() * 100;
                this.AlternativeTotalDiscount = Util.EncryptNew(TotalDiscountPercent);
            }
            else
                this.AlternativeTotalDiscount = Util.EncryptNew(0);
        }
        private void UpdateTotalQuantityAmountAfetrDiscountAndVAT()
        {
            this.AlternativeFinalPrice = Util.EncryptNew(this.QuantityTableItem.Where(d => d.OriginalItemId.HasValue).Select(d => (Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity - ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity) * (Util.DecryptNewDecimal(d.AdjustedDiscount) / 100))) +
            ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity - ((Util.DecryptNewDecimal(d.AdjustedPrice) * d.Quantity) * (Util.DecryptNewDecimal(d.AdjustedDiscount) / 100))) * (d.AdjustedVAT / 100))).Sum());
        }
        #endregion
        public SupplierQuantityTable(string tableQuantityName, string totalPrice, string discount, bool? isActive, List<SupplierQuantityTableItem> quantityTableItem, string openingTotalPrice, string openingDiscount, decimal openingVat, int? QuantityId = null)
        {
            TableQuantityName = tableQuantityName;
            IsActive = isActive;
            QuantityTableItem = quantityTableItem;
            TotalPrice = totalPrice;
            TotalDiscount = discount;
            AdjustedTotalPrice = openingTotalPrice;
            AdjustedTotalDiscount = openingDiscount;
            AdjustedTotalVAT = openingVat;
            TenderQuantityTableId = QuantityId;
            EntityCreated();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableQuantityId { get; private set; }

        [StringLength(100), Required]
        public string TableQuantityName { get; private set; }


        public string TotalPrice { get; private set; }
        public string FinalPrice { get; private set; }
        public decimal TotalVAT { get; private set; }
        public string TotalDiscount { get; private set; }

        public decimal AdjustedTotalVAT { get; private set; }
        public string AdjustedTotalPrice { get; private set; }
        public string AdjustedTotalDiscount { get; private set; }

        [NotMapped]
        public decimal AdjustedFinalPrice
        {
            get
            {
                return 
                 Util.DecryptNewDecimal(this.AdjustedTotalPrice) -
                 Util.DecryptNewDecimal(this.AdjustedTotalPrice) * (Util.DecryptNewDecimal(this.AdjustedTotalDiscount) / 100) + 
                 (Util.DecryptNewDecimal(this.AdjustedTotalPrice) - Util.DecryptNewDecimal(this.AdjustedTotalPrice) * (Util.DecryptNewDecimal(this.AdjustedTotalDiscount) / 100)) * (this.AdjustedTotalVAT / 100);
            }
        }

        public string AlternativeTotalVAT { get; private set; }
        public string AlternativeTotalPrice { get; private set; }
        public string AlternativeTotalDiscount { get; private set; }
        public string AlternativeFinalPrice { get; private set; }

        public string CheckingFinalPrice { get; private set; }



        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; private set; }
        public int OfferId { get; private set; }
        public List<SupplierQuantityTableItem> QuantityTableItem { get; private set; }


        [ForeignKey(nameof(TenderQuantityTable))]
        public int? TenderQuantityTableId { private set; get; }

        public QuantitiesTable TenderQuantityTable { private set; get; }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }

        public void DeleteTableAndItems()
        {
            foreach (var item in QuantityTableItem)
            {
                item.Delete();
            }
            EntityDeleted();
        }
        public void UpdateTableName(string tableName, string totalPrice, string discount, string openingTotalPrice, string openingDiscount, decimal openingTotalVAT)
        {
            TableQuantityName = tableName;
            TotalPrice = totalPrice;
            TotalDiscount = discount;
            AdjustedTotalPrice = openingTotalPrice;
            AdjustedTotalDiscount = openingDiscount;
            AdjustedTotalVAT = openingTotalVAT;
            EntityUpdated();
        }

        public void EncryptData(string totalPrice, string discount, string openingTotalPrice, string openingDiscount, decimal openingTotalVAT)
        {
            TotalPrice = totalPrice;
            TotalDiscount = discount;
            AdjustedTotalPrice = openingTotalPrice;
            AdjustedTotalDiscount = openingDiscount;
            AdjustedTotalVAT = openingTotalVAT;
            UpdatedBy = "Encrypted";
            State = ObjectState.Modified;
            //EntityUpdated();
        }
        public void UpdateTableAmountAfterNegotiationbyName(decimal totalPrice)
        {
            //TotalPriceAfterDiscount = totalPrice;
            EntityUpdated();
        }

        public void AssignChosenTableItems(List<string> chosenItemsIds)
        {
            List<int> xx = chosenItemsIds.Select(c => Convert.ToInt32(c.Split('_')[2])).ToList();
            foreach (var item in this.QuantityTableItem)
            {
                item.Unselect();
            }
            foreach (var item in this.QuantityTableItem.Where(qti => xx.Contains(qti.Id)))
            {
                item.Select();
                this.CheckingFinalPrice += item.FinalAmountAfterDiscountandVAT;
            }
            this.AdjustedTotalPrice = Util.EncryptDecimal(this.QuantityTableItem.Where(qti => qti.isSelected).Sum(qti => Util.DecryptNewDecimal(qti.AdjustedPrice)));
            this.AdjustedTotalVAT = this.QuantityTableItem.Where(qti => qti.isSelected).Sum(qti => qti.AdjustedVAT);
            this.AdjustedTotalDiscount = Util.EncryptDecimal(this.QuantityTableItem.Where(qti => qti.isSelected).Sum(qti => Util.DecryptNewDecimal(qti.AdjustedDiscount)));
        }




    }
}