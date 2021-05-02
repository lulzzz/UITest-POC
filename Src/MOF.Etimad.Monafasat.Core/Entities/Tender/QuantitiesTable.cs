using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("QuantitiesTable", Schema = "Tender")]
    public class QuantitiesTable : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int QuantitiesTableId { get; private set; }

        [Required]
        [StringLength(1024)]
        public string Name { get; private set; }

        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { get; private set; }
        public List<QuantityTableItem> QuantitiesTableItems { get; private set; } = new List<QuantityTableItem>();

        #endregion 

        #region Constructors====================================================

        public QuantitiesTable()
        {

        }

        public QuantitiesTable(string name, int? changeStatusId = null, int? reviewStatusId = null)
        {
            Name = name; 
            EntityCreated(); 
        }
        #endregion

        #region Methods====================================================
         
        public void DeleteQuantitiesTableAndItems()
        {
            foreach (var item in QuantitiesTableItems)
            {
                item.DeleteItemsSuppierItems();
                 
            }
            EntityDeleted();
        }

        public void SetAddedState()
        {
            QuantitiesTableId = 0;
            QuantitiesTableItems.ForEach(x => x.SetAddedState());
            EntityCreated();
        }

        #endregion
    }
}