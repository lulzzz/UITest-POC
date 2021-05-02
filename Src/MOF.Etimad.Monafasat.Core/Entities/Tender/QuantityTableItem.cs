
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("QuantityTableItem", Schema = "Tender")]
    public class QuantityTableItem : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int QuantityTableItemId { get;  private set; }
        [StringLength(1024)]
        [Required]
        public string Name { get;  private set; }
        [Required]
        public int Quantity { get; private set; }
        
        [StringLength(1024)]
        public string Unit { get; private set; }
        [StringLength(1024)]
        public string Details { get; private set; }
        [ForeignKey(nameof( QuantityTable))]
        public int QuantityTableID { private set; get; }

        public string ItemAttachmentId { get; set; }
        public string ItemAttachmentName { get; set; }
        //public int SupplierQuantityTableItemId { get; private set; }
        //[ForeignKey("SupplierQuantityTableItemId")]
        public List<SupplierQuantityTableItem> SupplierQuantitiesTableItem { get; private set; } = new List<SupplierQuantityTableItem>();
        //[ForeignKey(nameof(ItemAttachment))]
        //public long? ItemAttachmentId { private set; get; } 

        public QuantitiesTable QuantityTable { private set; get; }
        //public virtual Attachment ItemAttachment { get; private set; }
    

        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public QuantityTableItem()
        {

        }

        public QuantityTableItem(string name,int quantity,string unit,string details,string fileName,string fileId)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Details = details;
            ItemAttachmentName = fileName;
            ItemAttachmentId = fileId;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================

        public void DeleteItemsSuppierItems()
        {
            SupplierQuantitiesTableItem.ForEach(x=>x.DeleteItemWithTable());
            EntityDeleted();
        }
        public void Update()
        {
            
            EntityUpdated();
        }

        public void Delete()
        {
            EntityDeleted();
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void setActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        public void SetAddedState()
        {
            QuantityTableItemId= 0;
            EntityCreated();
        }
        #endregion
    }
}
