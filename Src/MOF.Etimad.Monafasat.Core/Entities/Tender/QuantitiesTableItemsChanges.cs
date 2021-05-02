using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("QuantitiesTableItemsChanges", Schema = "Tender")]
    public class QuantitiesTableItemsChanges:AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int QuantityTableItemId { get; private set; }
        [StringLength(1024)]
        [Required]
        public string Name { get; private set; }
        [Required]
        public int Quantity { get; private set; }
         
        [StringLength(1024)]
        public string Unit { get; private set; }
        [StringLength(1024)]
        public string Details { get; private set; }
        [ForeignKey(nameof(QuantityTable))]
        public int QuantityTableID { private set; get; }

        public string ItemAttachmentId { get; set; }
        public string ItemAttachmentName { get; set; }
        public int ItemChangeStatusId{ get; private set; }
        public QuantitiesTableChanges QuantityTable { private set; get; }

        #endregion

        #region Constructors====================================================

        public QuantitiesTableItemsChanges()
        {

        }

        public QuantitiesTableItemsChanges(string name, int quantity, string unit, string details, string fileName, string fileId, Enums.TableChangeStatus changeStatus)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Details = details;
            ItemAttachmentName = fileName;
            ItemAttachmentId = fileId;
            ItemChangeStatusId = (int)changeStatus;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================


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
        #endregion
    }
}
