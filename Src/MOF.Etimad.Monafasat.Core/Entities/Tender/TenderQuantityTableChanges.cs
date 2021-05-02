using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderQuantityTableChanges", Schema = "Tender")]
    public class TenderQuantityTableChanges : AuditableEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public TenderChangeRequest ChangeRequest { get; private set; }
        [ForeignKey(nameof(ChangeRequest))]
        public int TenderChangeRequestId { get; private set; }
        public TenderQuantityTable TenderQuantityTable { get; private set; }
        [ForeignKey(nameof(TenderQuantityTable))]
        public long? TenderQuantitiesTableId { get; private set; }
        public int TableChangeStatusId { get; private set; }
        public int FormId { get; private set; }
        public TenderQuantitiyItemsChangeJson QuantitiyItemsChangeJson { get; set; }

        #region Constructors
        public TenderQuantityTableChanges()
        {
        }
        public TenderQuantityTableChanges(long? id, string name, Enums.QuantityTableChangeStatus quantityTableChangeStatus, int formId)
        {
            Name = string.IsNullOrEmpty(name) ? "اسم الجدول" : name;
            TenderQuantitiesTableId = id == 0 ? null : id;
            TableChangeStatusId = (int)quantityTableChangeStatus;
            QuantitiyItemsChangeJson = new TenderQuantitiyItemsChangeJson(id ?? 0);
            FormId = formId;
            EntityCreated();
            QuantitiyItemsChangeJson.SetAddedState();
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

        public TenderQuantityTableChanges DeleteQuantityTableItem(int itemNumber)
        {
            QuantitiyItemsChangeJson.Delete(itemNumber);
            EntityUpdated();
            return this;
        }
        public TenderQuantityTableChanges SaveQuantityTableItems(long tableId, List<TenderQuantityItemDTO> lst, long currentItemId, out long itemId)
        {
            itemId = 0;
            bool ItemExists = QuantitiyItemsChangeJson != null && QuantitiyItemsChangeJson.TenderQuantityTableItemChanges != null && QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any(a => /*a.IsActive == true && a.TenderQuantityTableChangesId == tableId &&*/ a.ItemNumber == currentItemId);
            var lastIndex = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges/*.Where(a => a.IsActive == true && a.TenderQuantityTableChangesId == tableId)*/.Any() ? QuantitiyItemsChangeJson.TenderQuantityTableItemChanges/*.Where(a => a.IsActive == true && a.TenderQuantityTableChangesId == tableId)*/
                                                    .Max(a => a.ItemNumber) : 0;
            foreach (var item in lst)
            {
                byte[] gb = Guid.NewGuid().ToByteArray();
                long IId = BitConverter.ToInt64(gb, 0);
                var idExsit = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any(x => x.Id == IId);
                while (idExsit)
                {
                    gb = Guid.NewGuid().ToByteArray();
                    IId = BitConverter.ToInt64(gb, 0);
                    idExsit = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any(x => x.Id == IId);
                }
                if (!ItemExists)
                {
                    QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Add(new TenderQuantityTableItemChanges(IId, item.ColumnId, item.TenderFormHeaderId, item.TemplateId, item.ColumnName, item.Value, lastIndex + 1));
                    itemId = lastIndex + 1;
                }
                else
                {
                    TenderQuantityTableItemChanges QItem;
                    if (item.Id != 0)
                        QItem = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.FirstOrDefault(q => q.Id == item.Id );
                    else
                        QItem = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.FirstOrDefault(q =>  q.ItemNumber == item.ItemNumber && q.ColumnId == item.ColumnId && q.TenderFormHeaderId == item.TenderFormHeaderId);
                    if(QItem != null)
                        QItem.UpdateItems(item.ColumnId, item.TenderFormHeaderId, item.TemplateId, item.ColumnName, item.Value, currentItemId);
                }
            }
            if (tableId == 0)
            {
                EntityCreated();
                QuantitiyItemsChangeJson.SetAddedState();
            }
            else
            {
                EntityUpdated();
                QuantitiyItemsChangeJson.SetUpdateState();
            }
            return this;
        }
        public void UpdateName(string name)
        {
            Name = string.IsNullOrEmpty(name) ? "اسم الجدول" : name;
            EntityUpdated();
        }
        public void DeleteTenderQuantityTableWithItems()
        {
            QuantitiyItemsChangeJson.Delete();
            DeActive();
        }
        public void DeleteExistingTenderQuantityTable(long currentTableId)
        {
            TableChangeStatusId = (int)Enums.QuantityTableChangeStatus.Remove;
            TenderQuantitiesTableId = currentTableId;
            EntityUpdated();
        }

        public long LastItemIndex()
        {
            var lastIndex = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any() ? QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Max(a => a.ItemNumber) : 0;
            return lastIndex;
        }

        public TenderQuantityTableChanges SaveQuantityTableBulkItems(long tableId, List<TenderQuantityItemDTO> lst, string tableName)
        {
            Name = string.IsNullOrEmpty(tableName) ? "اسم الجدول" : tableName;
            var lastIndex = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges/*.Where(a => a.IsActive == true && a.TenderQuantityTableChangesId == tableId)*/.Any() ? QuantitiyItemsChangeJson.TenderQuantityTableItemChanges/*.Where(a => a.IsActive == true && a.TenderQuantityTableChangesId == tableId)*/
                                                    .OrderByDescending(t => t.Id).FirstOrDefault().ItemNumber : 0;
            foreach (var item in lst)
            {
                byte[] gb = Guid.NewGuid().ToByteArray();
                long IId = BitConverter.ToInt64(gb, 0);
                var idExsit = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any(x => x.Id == IId);
                while (idExsit)
                {
                    gb = Guid.NewGuid().ToByteArray();
                    IId = BitConverter.ToInt64(gb, 0);
                    idExsit = QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Any(x => x.Id == IId);
                }
                QuantitiyItemsChangeJson.TenderQuantityTableItemChanges.Add(new TenderQuantityTableItemChanges(IId, item.ColumnId, item.TenderFormHeaderId, item.TemplateId, item.ColumnName, item.Value, item.ItemNumber + lastIndex));
            }
            if (tableId == 0)
            {
                EntityCreated();
                QuantitiyItemsChangeJson.SetAddedState();
            }
            else
            {
                EntityUpdated();
                QuantitiyItemsChangeJson.SetUpdateState();
            }
            return this;
        }

        #endregion
    }
}
