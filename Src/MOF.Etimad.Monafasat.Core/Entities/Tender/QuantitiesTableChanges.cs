using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("QuantitiesTableChanges", Schema = "Tender")]
    public class QuantitiesTableChanges: AuditableEntity
    {
        #region Fields
        [Key]
        public int QuantitiesTableId { get; private set; }
        [Required]
        [StringLength(1024)]
        public string Name { get; private set; }
        public List<QuantitiesTableItemsChanges> QuantitiesTableItems { get; private set; } = new List<QuantitiesTableItemsChanges>();
        public TenderChangeRequest ChangeRequest { get; private set; }
        [ForeignKey("ChangeRequest")]
        public int TenderChangeRequestId { get; private set; }
        public QuantitiesTable QuantitiesTable { get;private set; }
        [ForeignKey("QuantitiesTable")]
        public int? TenderQuantitiesTableId { get; private set; }
        public int TableChangeStatusId { get; private set; }
        #endregion
        #region Constructors
        public QuantitiesTableChanges()
        {
        }
        public QuantitiesTableChanges(string name, List<QuantitiesTableItemsChanges> items,Enums.TableChangeStatus tableChangeStatus, int? tenderQuantitiesTableId)
        {
            Name = name;
            TableChangeStatusId = (int)tableChangeStatus;
            TenderQuantitiesTableId = tenderQuantitiesTableId==0?null: tenderQuantitiesTableId;
            EntityCreated();
            AddQuantityItems(items);
        }
        #endregion
        #region Methods====================================================
        public void Update(string name)
        {
            Name = name;
            //EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        public void DeleteQuantitiesTableAndItems()
        {
            foreach (var item in QuantitiesTableItems)
            {
                item.Delete();
            }
            EntityDeleted();
        }
        public void DeactiveQuantitiesTableAndItems()
        {
            foreach (var item in QuantitiesTableItems)
            {
                item.DeActive();
            }
            IsActive = false;
            EntityUpdated();
        }
        public void AddQuantityItems(List<QuantitiesTableItemsChanges> quantitiesTableItems)
        {
            foreach (var item in quantitiesTableItems)
            {
                QuantitiesTableItems.Add(new QuantitiesTableItemsChanges(item.Name, item.Quantity, item.Unit, item.Details, item.ItemAttachmentName, item.ItemAttachmentId,(Enums.TableChangeStatus)item.ItemChangeStatusId));
            }
        }
        #endregion
    }
}
