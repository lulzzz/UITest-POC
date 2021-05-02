using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderQuantityTable", Schema = "Tender")]
    public class TenderQuantityTable : AuditableEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int TenderId { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(TenderId))]
        public virtual Tender Tender { get; set; }

        public TenderQuantitiyItemsJson QuantitiyItemsJson { get; set; }

        public int FormId { get; set; }

        #region Constructors
        public TenderQuantityTable()
        {

        }
        public TenderQuantityTable(long id, List<TenderQuantityItemDTO> items, int formId)
        {
            Name = "اسم الجدول";
            FormId = formId;
            QuantitiyItemsJson = new TenderQuantitiyItemsJson(id);
            SaveQuantityTableItems(id, items, 0, out long test);
        }
        public TenderQuantityTable(long id, string name, int formId)
        {
            Name = string.IsNullOrEmpty(name) ? "اسم الجدول" : name;
            FormId = formId;
            QuantitiyItemsJson = new TenderQuantitiyItemsJson(id);
            EntityCreated();
        }
        public TenderQuantityTable(int tender, string name, int formId)
        {
            Name = string.IsNullOrEmpty(name) ? "اسم الجدول" : name;
            TenderId = tender;
            FormId = formId;
            QuantitiyItemsJson = new TenderQuantitiyItemsJson(tender);
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

        public void SetAddedState()
        {
            Id = 0;
            QuantitiyItemsJson.SetAddedState();
            EntityCreated();
        }

        public TenderQuantityTable SaveQuantityTableItems(long tableId, List<TenderQuantityItemDTO> lst, long currentItemId, out long itemId)
        {
            itemId = 0;
            bool ItemExists = QuantitiyItemsJson != null && QuantitiyItemsJson.TenderQuantityTableItems != null && QuantitiyItemsJson.TenderQuantityTableItems.Any(a => a.ItemNumber == currentItemId);
            var lastIndex = QuantitiyItemsJson.TenderQuantityTableItems.Any() ? QuantitiyItemsJson.TenderQuantityTableItems.Max(a => a.ItemNumber) : 0;
            foreach (var item in lst)
            {
                if (!ItemExists)
                {
                    byte[] gb = Guid.NewGuid().ToByteArray();
                    long IId = BitConverter.ToInt64(gb, 0);
                    var idExsit = QuantitiyItemsJson.TenderQuantityTableItems.Any(x => x.Id == IId);
                    while (idExsit)
                    {
                        gb = Guid.NewGuid().ToByteArray();
                        IId = BitConverter.ToInt64(gb, 0);
                        idExsit = QuantitiyItemsJson.TenderQuantityTableItems.Any(x => x.Id == IId);
                    }
                    QuantitiyItemsJson.TenderQuantityTableItems.Add(new TenderQuantityTableItem(IId, item.ColumnId, item.TenderFormHeaderId, item.TemplateId, item.ColumnName, item.Value, lastIndex + 1));
                    itemId = lastIndex + 1;
                }
                else
                {
                    TenderQuantityTableItem QItem;
                    if (item.Id != 0)
                        QItem = QuantitiyItemsJson.TenderQuantityTableItems.FirstOrDefault(q => q.Id == item.Id);
                    else
                        QItem = QuantitiyItemsJson.TenderQuantityTableItems.FirstOrDefault(q => q.ItemNumber == item.ItemNumber && q.ColumnId == item.ColumnId && q.TenderFormHeaderId == item.TenderFormHeaderId);
                    if(QItem != null)
                        QItem.UpdateItems(item.ColumnId, item.TenderFormHeaderId, item.TemplateId, item.ColumnName, item.Value, currentItemId);
                }
            }
            if (tableId == 0)
            {
                EntityCreated();
                QuantitiyItemsJson.SetAddedState();
            }
            else
            {
                EntityUpdated();
                QuantitiyItemsJson.SetUpdateState();
            }
            return this;
        }

        public TenderQuantityTable SaveQuantityTableBulkItems(long tableId, List<TenderQuantityItemDTO> lst, string tableName)
        {
            Name = string.IsNullOrEmpty(tableName) ? "اسم الجدول" : tableName;
            var lastIndex = QuantitiyItemsJson.TenderQuantityTableItems.Count != 0 ? QuantitiyItemsJson.TenderQuantityTableItems.Max(D => D.ItemNumber) : 0;
            foreach (var item in lst)
            {
                byte[] gb = Guid.NewGuid().ToByteArray();
                long IId = BitConverter.ToInt64(gb, 0);
                var idExsit = QuantitiyItemsJson.TenderQuantityTableItems.Any(x => x.Id == IId);
                while (idExsit)
                {
                    gb = Guid.NewGuid().ToByteArray();
                    IId = BitConverter.ToInt64(gb, 0);
                    idExsit = QuantitiyItemsJson.TenderQuantityTableItems.Any(x => x.Id == IId);
                }
                QuantitiyItemsJson.TenderQuantityTableItems.Add(new TenderQuantityTableItem(IId, item.ColumnId, item.TenderFormHeaderId, item.TemplateId, item.ColumnName, item.Value, item.ItemNumber + lastIndex));
            }
            if (tableId == 0)
            {
                EntityCreated();
                QuantitiyItemsJson.SetAddedState();
            }
            else
            {
                EntityUpdated();
                QuantitiyItemsJson.SetUpdateState();
            }
            return this;
        }

        public TenderQuantityTable DeleteQuantityTableItems(int itemNumber)
        {
            QuantitiyItemsJson.Delete(itemNumber);
            EntityUpdated();
            return this;
        }

        public TenderQuantityTable DeleteTenderQuantityTableWithItems()
        {
            if (QuantitiyItemsJson != null)
                QuantitiyItemsJson.Delete();
            DeActive();
            return this;
        }

        public TenderQuantityTable DeleteTenderQuantityTablesItems()
        {
            if (QuantitiyItemsJson != null)
            {
                QuantitiyItemsJson.Delete();
            }
            return this;
        }

        public void UpdateName(string name)
        {
            Name = string.IsNullOrEmpty(name) ? "اسم الجدول" : name;
            EntityUpdated();
        }

        #endregion
    }
}
