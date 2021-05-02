using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
{

    [Table("NegotiationSupplierQuantityTable", Schema = "CommunicationRequest")]
    public class NegotiationSupplierQuantityTable : AuditableEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int refNegotiationSecondStage { get; private set; }

        public long SupplierQuantityTableId { get; set; }
        #region [Navigation]

        [ForeignKey(nameof(SupplierQuantityTableId))]
        public SupplierTenderQuantityTable SupplierQuantityTable { get; private set; }

        [ForeignKey(nameof(refNegotiationSecondStage))]
        public NegotiationSecondStage NegotiationSecondStage { get; private set; }

        public NegotiationQuantityItemJson NegotiationQuantityItemJson { get; set; }

        #endregion


        #region [Methods]



        public NegotiationSupplierQuantityTable()
        {

        }
        public NegotiationSupplierQuantityTable(int negotiationSecondStage, string qtName, long _SupplierQuantityTableId, List<NegotiationSupplierQuantityTableItem> negotiationSupplierQuantityTableItems)
        {

            refNegotiationSecondStage = negotiationSecondStage;
            this.Name = qtName;
            this.SupplierQuantityTableId = _SupplierQuantityTableId;
            NegotiationQuantityItemJson = new NegotiationQuantityItemJson(_SupplierQuantityTableId);
            this.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems = negotiationSupplierQuantityTableItems;
            EntityCreated();
        }
        public void DeleteTable()
        {
            EntityDeleted();
        }

        public void DeleteItems()
        {

            this.NegotiationQuantityItemJson.Delete();

            EntityUpdated();
        }

        public void DeleteqtItems(List<NegotiationSupplierQuantityTableItem> tableItems)
        {
            foreach (var item in tableItems)
            {
                this.NegotiationQuantityItemJson.Delete(item.ItemNumber);
            }

            EntityUpdated();
        }

        public void UpadteNegotiationQTableItems(List<TenderQuantityItemDTO> supplierTenderQuantityTableItems)
        {
            foreach (var item in supplierTenderQuantityTableItems)
            {
                this.NegotiationQuantityItemJson.Delete(item.ItemNumber);
            }

            foreach (var item in supplierTenderQuantityTableItems)
            {
                byte[] gb = Guid.NewGuid().ToByteArray();
                long IId = BitConverter.ToInt64(gb, 0);
                var idExsit = NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Any(x => x.Id == IId);
                while (idExsit)
                {
                    gb = Guid.NewGuid().ToByteArray();
                    IId = BitConverter.ToInt64(gb, 0);
                    idExsit = NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Any(x => x.Id == IId);
                }
                NegotiationSupplierQuantityTableItem negotiationSupplierQuantityTableItem =
                     new NegotiationSupplierQuantityTableItem(IId, item.ColumnId, item.TenderFormHeaderId, 1, item.Value, item.ItemNumber);
                this.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Add(negotiationSupplierQuantityTableItem);

            }
            EntityUpdated();
        }







    }

    #endregion
}


