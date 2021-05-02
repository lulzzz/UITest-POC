using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
{
    public class NegotiationSecondStage : Negotiation
    {
        public int NegotiationFirstStageId { get; private set; }
        public int OfferId { get; private set; }

        public bool IsReported { get; private set; } = false;

        public DateTime? PeriodStartDate { get; set; }
        #region [Navigation]
        [ForeignKey(nameof(NegotiationFirstStageId))]
        public NegotiationFirstStage Negotiationfirststage { get; private set; }
        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; private set; }
        public bool? IsSupplierAgree { get; private set; }
        public List<NegotiationSupplierQuantityTable> negotiationSupplierQuantitiestable { get; private set; } = new List<NegotiationSupplierQuantityTable>();

        #endregion

        #region [Methods]
        public NegotiationSecondStage()
        {

        }
        public NegotiationSecondStage(int agencyRequestId, int? ReasonId, int FirstStageId, int offerId)
        {
            AgencyRequestId = agencyRequestId;
            NegotiationFirstStageId = FirstStageId;
            NegotiationReasonId = ReasonId;
            StatusId = (int)Enums.enNegotiationStatus.UnderUpdate;
            OfferId = offerId;
            IsReported = false;
            EntityCreated();
        }

        public void AddSupplierQuantityTables(List<SupplierTenderQuantityTable> quantitiesTables)
        {
            foreach (var QT in quantitiesTables)
            {
                NegotiationSupplierQuantityTable negotiationQuantityTable = new NegotiationSupplierQuantityTable(
                    this.NegotiationId, QT.Name, QT.Id,
                   QT.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Select(item => new NegotiationSupplierQuantityTableItem(item.Id, item.ColumnId, item.TenderFormHeaderId, item.ActivityTemplateId, (item.IsDefault ? item.Value : item.AlternativeValue), item.ItemNumber)).ToList());
                this.negotiationSupplierQuantitiestable.Add(negotiationQuantityTable);
            }
        }

        public void StartSuppierPeriod()
        {
            PeriodStartDate = DateTime.Now;
            EntityUpdated();

        }
        public void UpdateNegotiationSecondStage(int ReasonId, int StatusId)
        {
            this.StatusId = StatusId;
            this.NegotiationReasonId = ReasonId;
            EntityUpdated();
        }

        public void UpdateNegotiationQItems(List<TenderQuantityItemDTO> quantityTableItems)
        {

            var itemTables = quantityTableItems.Select(d => d.TableId).Distinct().ToList();
            foreach (var tbl in negotiationSupplierQuantitiestable.Where(d => itemTables.Any(dd => dd == d.Id)).ToList())
            {

                // Update Current Tables
                if (itemTables.Any(s => s == tbl.Id))
                {
                    tbl.UpadteNegotiationQTableItems(quantityTableItems.Where(d => d.TableId == tbl.Id).ToList());
                }
                // Delete 
                else
                {
                    tbl.DeleteItems();
                }
            }

            EntityUpdated();
        }
        public void DeleteNegotiationQItems(List<TenderQuantityItemDTO> quantityTableItems)
        {

            var itemTables = quantityTableItems.Select(d => d.TableId).Distinct().ToList();
            foreach (var tbl in negotiationSupplierQuantitiestable.Where(d => itemTables.Any(dd => dd == d.Id)).ToList())
            {
                var items = tbl.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Where(d => quantityTableItems.Any(f => f.Id == d.Id)).ToList();

                tbl.DeleteqtItems(items);
            }

            EntityUpdated();
        }
        public void UpdateSecondNegotiationStatus(int statusId)
        {
            if (statusId == (int)Enums.enNegotiationStatus.SentToSuppliers)
            {
                PeriodStartDate = DateTime.Now;
            }
            StatusId = statusId;
            EntityUpdated();
        }


        #endregion
    }

}
