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
    [Table("SupplierTenderQuantityTable", Schema = "Offer")]
    public class SupplierTenderQuantityTable : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public virtual Offer Offer { get; set; }
        public long? TenederQuantityId { get; set; }
        [ForeignKey(nameof(TenederQuantityId))]
        public virtual TenderQuantityTable TenderQuantityTable { get; set; }
        public virtual SupplierTenderQuantityTableItemJson QuantitiyItemsJson { get; set; }
        public string AdjustedTotalPrice { get; private set; }
        public decimal AdjustedTotalVAT { get; private set; }
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

        #region Constructors

        public SupplierTenderQuantityTable()
        {

        }


        public SupplierTenderQuantityTable(int offerId, string name)
        {
            Name = name;
            OfferId = offerId;
            EntityCreated();
        }

        public SupplierTenderQuantityTable(string name, long tenderQTableId, List<SupplierTenderQuantityItemDTO> items)
        {
            Name = name;
            TenederQuantityId = tenderQTableId;
            
            EntityCreated();
        }

        public void UpadteSupplierQTableItems(List<SupplierTenderQuantityTableItem> supplierTenderQuantityTableItems)
        {
            this.QuantitiyItemsJson.UpadteSupplierQTableItems(supplierTenderQuantityTableItems);
            EntityUpdated();
        }

        public void AddSupplierQTableItems(List<TenderQuantityItemDTO> supplierTenderQuantityTableItems)
        {
            foreach (var item in supplierTenderQuantityTableItems)
            {
                var obj = new SupplierTenderQuantityTableItem(0, item.ColumnId, null, null, item.Value, 1);
                QuantitiyItemsJson.SupplierTenderQuantityTableItems.Add(obj);
            }
            EntityUpdated();
        }
        public void UpadteSupplierQTableItemsDefault(Dictionary<long, bool> quantityTable)
        {
            foreach (KeyValuePair<long, bool> item in quantityTable)
            {
                var SupplierItem = this.QuantitiyItemsJson.SupplierTenderQuantityTableItems.FirstOrDefault(s => s.Id == item.Key);
                if (SupplierItem == null)
                {
                    continue;
                }
                SupplierItem.UpdateDefault(item.Value);
            }
            EntityUpdated();
        }
        public void UpdateOfferSupplierQItemsIsVerifiedMandatoryList(int itemNumber, bool value)
        {
            var SupplierItem = this.QuantitiyItemsJson.SupplierTenderQuantityTableItems.FirstOrDefault(s => s.ItemNumber == itemNumber);
            if (SupplierItem != null)
                SupplierItem.UpdateDefault(value);
            EntityUpdated();
        }

        public void UpadteSupplierQTableItemsAlternativeValue(List<SupplierTenderQuantityTableItem> supplierTenderQuantityTableItems)
        {
            this.QuantitiyItemsJson.UpadteSupplierQTableItemsAlternativeValue(supplierTenderQuantityTableItems);
            EntityUpdated();
        }

        public void RemoveAlternative()
        {
            this.QuantitiyItemsJson.RemoveAlternative();
            EntityUpdated();
        }

        #endregion

        #region Methods

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void DeleteTableAndItems()
        {
            DeActive();
        }

        public SupplierTenderQuantityTable SaveQuantityTableItems(long tableId, List<TenderQuantityItemDTO> lst, string tableName, long currentItemId, out long itemId)
        {
            itemId = 0;
            Name = tableName;
            if (QuantitiyItemsJson == null)
            {
                QuantitiyItemsJson = new SupplierTenderQuantityTableItemJson();
                QuantitiyItemsJson.Create();
            }
            bool ItemExists = QuantitiyItemsJson.SupplierTenderQuantityTableItems != null && QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(a => /*a.IsActive == true && a.SupplierTenderQuantityTableId == tableId && */a.ItemNumber == currentItemId);
            var lastIndex = QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any() ? QuantitiyItemsJson.SupplierTenderQuantityTableItems.OrderByDescending(t => t.ItemNumber).FirstOrDefault().ItemNumber : 0;
            foreach (var item in lst)
            {
                if (!ItemExists)
                {
                    byte[] gb = Guid.NewGuid().ToByteArray();
                    long IId = BitConverter.ToInt64(gb, 0);
                    var idExsit = QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(x => x.Id == IId);
                    while (idExsit)
                    {
                        gb = Guid.NewGuid().ToByteArray();
                        IId = BitConverter.ToInt64(gb, 0);
                        idExsit = QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(x => x.Id == IId);
                    }
                    QuantitiyItemsJson.SupplierTenderQuantityTableItems.Add(new SupplierTenderQuantityTableItem(IId, item.ColumnId, item.TenderFormHeaderId, item.TemplateId, item.Value, lastIndex + 1));
                    itemId = lastIndex + 1;
                }
                else
                {
                    SupplierTenderQuantityTableItem QItem;
                    if (item.Id != 0)
                        QItem = QuantitiyItemsJson.SupplierTenderQuantityTableItems.FirstOrDefault(q => q.Id == item.Id);
                    else
                        QItem = QuantitiyItemsJson.SupplierTenderQuantityTableItems.FirstOrDefault(q => q.ItemNumber == item.ItemNumber && q.ColumnId == item.ColumnId && q.TenderFormHeaderId == item.TenderFormHeaderId);
                    QItem.UpdateItems(item.ColumnId, item.TenderFormHeaderId, item.TemplateId, item.Value, currentItemId);
                }
            }
            if (tableId == 0)
            {
                EntityCreated();
                QuantitiyItemsJson.Create();
            }
            else
            {
                EntityUpdated();
                QuantitiyItemsJson.Update();
            }
            return this;
        }

        public SupplierTenderQuantityTable DeleteQuantityTableItems(int itemNumber)
        {
            QuantitiyItemsJson.DeleteRow(itemNumber);
            EntityUpdated();
            return this;
        }
        #endregion
    }
}
