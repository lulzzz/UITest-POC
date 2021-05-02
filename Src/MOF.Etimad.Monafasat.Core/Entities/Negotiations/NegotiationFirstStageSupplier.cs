using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
{
    [Table("NegotiationFirstStageSupplier", Schema = "CommunicationRequest")]

    public class NegotiationFirstStageSupplier : AuditableEntity
    {
        #region [Propperties]

        [Key]
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int NegotiationId { get; set; }
        public string SupplierCR { get; set; }
        public decimal offerOriginalAmount { get; set; } = 0;
        public int? NegotiationSupplierStatusId { get; private set; }
        public DateTime? PeriodStartDateTime { get; private set; }
        public bool IsReported { get; set; } = false;



        #endregion
        #region [Navigation]

        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; set; }

        [ForeignKey(nameof(SupplierCR))]
        public Supplier Supplier { get; set; }

        [ForeignKey(nameof(NegotiationId))]
        public NegotiationFirstStage NegotiationFirstStage { get; set; }

        [ForeignKey(nameof(NegotiationSupplierStatusId))]
        public NegotiationSupplierStatus NegotiationSupplierStatus { get; set; }
        #endregion
        #region[Methods]
        public NegotiationFirstStageSupplier()
        {

        }
        public NegotiationFirstStageSupplier(int NegotiationFirstStageStatusId, DateTime? PeriodStartDateTime, int offerId, string SupplierCR, int NegotiationId, decimal OriginalAmount)
        {
            IsReported = false;
            this.NegotiationSupplierStatusId = NegotiationFirstStageStatusId;
            this.OfferId = offerId;
            this.SupplierCR = SupplierCR;
            this.offerOriginalAmount = OriginalAmount;
            this.NegotiationId = NegotiationId;
            this.PeriodStartDateTime = PeriodStartDateTime;
            EntityCreated();
        }
        public void UpdateNegotiationFirstStageSupplier(int NegotiationFirstStageStatusId, DateTime? PeriodStartDateTime)
        {
            this.NegotiationSupplierStatusId = NegotiationFirstStageStatusId;
            this.PeriodStartDateTime = PeriodStartDateTime == null ? this.PeriodStartDateTime : PeriodStartDateTime;
            EntityUpdated();
        }
        public void UpdateNegotiationId(int negotiationId)
        {
            this.NegotiationId = negotiationId;
            EntityUpdated();
        }
        public void SetAsReported(bool IsReported)
        {
            this.IsReported = IsReported;
            EntityUpdated();
        }
        public void StartSupplierPeriodService()
        {
            this.PeriodStartDateTime = DateTime.Now;
            this.NegotiationSupplierStatusId = (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply;
            this.IsReported = true;

            EntityUpdated();
        }
        #endregion
    }


}
