using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
{

    [Table("NegotiationQuantityTable", Schema = "CommunicationRequest")]
    public class NegotiationQuantityTable : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int refNegotiationSecondStage { get; set; }
        public int SupplierQuantityTableId { get; set; }
        // public int TenderQuantityTableItemId { get; set; }
        public string Name { get; private set; }
        [NotMapped]
        public DateTime TableCreationDate { get; private set; } = DateTime.Now;
        [NotMapped]
        [StringLength(50)]
        public string TableStatus { get; private set; }
        #region [Navigation]

        //[ForeignKey(nameof(SupplierQuantityTableId))]
        //public SupplierQuantityTable SupplierQuantityTable { get; private set; }

        [ForeignKey(nameof(refNegotiationSecondStage))]
        public NegotiationSecondStage NegotiationSecondStage { get; private set; }
        public List<NegotiationQuantityTableItem> NegotiationQuantityTableItems { get; private set; } = new List<NegotiationQuantityTableItem>();

        // public virtual NegotiationSupplierQuantityTable NegotiationSupplierQuantityTable { get; private set; }

        #endregion

        #region [Constructors]
        public NegotiationQuantityTable()
        {

        }
        public NegotiationQuantityTable(int refNegotiationSecondStage, int supplierQuantityTableId, string name, List<NegotiationQuantityTableItem> negotiationQuantityTableItems, string status, DateTime creationDate)
        {
            this.refNegotiationSecondStage = refNegotiationSecondStage;
            this.SupplierQuantityTableId = supplierQuantityTableId;
            this.TableCreationDate = creationDate;
            this.TableStatus = status;
            this.Name = name;
            this.NegotiationQuantityTableItems = negotiationQuantityTableItems;
            EntityCreated();
        }
        #endregion [Constructors]

        #region [Methods]
        //public void RemoveItem(int itemId)
        //{
        //    var item = this.NegotiationQuantityTableItems.Where(w => w.SupplierQuantityTableItemId == itemId).FirstOrDefault();
        //    item.Delete();
        //    EntityUpdated();
        //}

        public void DeActive()
        {
            this.IsActive = false;
            EntityUpdated();
        }
        public void Delete()
        {
            //this.NegotiationSupplierQuantityTable.Delete();
            foreach (var item in NegotiationQuantityTableItems)
            {
                item.Delete();
            }

            EntityDeleted();
        }

        public void DeleteItems()
        {
            //this.NegotiationSupplierQuantityTable.Delete();
            foreach (var item in NegotiationQuantityTableItems)
            {
                item.Delete();
            }

            EntityUpdated();
        }
        public void AddItems(List<NegotiationQuantityTableItem> negotiationQuantityTableItems)
        {
            this.NegotiationQuantityTableItems = negotiationQuantityTableItems;
            EntityUpdated();
        }




    }

    #endregion
}


