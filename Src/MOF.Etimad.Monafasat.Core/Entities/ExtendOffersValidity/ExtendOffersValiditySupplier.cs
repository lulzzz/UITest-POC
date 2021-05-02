using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ExtendOffersValiditySupplier", Schema = "CommunicationRequest")]
    public class ExtendOffersValiditySupplier : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int ExtendOffersValidityId { get; set; }
        public string SupplierCR { get; set; }
        public int? SupplierExtendOfferValidityStatusId { get; private set; }
        public DateTime? PeriodStartDateTime { get; private set; }
        public bool IsReported { get; set; } = false;

        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; set; }

        [ForeignKey(nameof(SupplierCR))]
        public Supplier Supplier { get; set; }

        [ForeignKey(nameof(ExtendOffersValidityId))]
        public ExtendOffersValidity ExtendOffersValidity { get; set; }

        [ForeignKey(nameof(SupplierExtendOfferValidityStatusId))]
        public SupplierExtendOffersValidityStatus SupplierExtendOffersValidityStatus { get; set; }
        public ExtendOffersValidityAttachment ExtendOffersValidityAttachment { get; private set; }

        public ExtendOffersValiditySupplier()
        {

        }
        public ExtendOffersValiditySupplier(int? ExtendOfferValidityStatusId, DateTime? PeriodStartDateTime, int offerId, string SupplierCR, int ExtendOffersValidityId)
        {
            IsReported = false;
            this.SupplierExtendOfferValidityStatusId = ExtendOfferValidityStatusId;
            this.OfferId = offerId;
            this.SupplierCR = SupplierCR;
            this.ExtendOffersValidityId = ExtendOffersValidityId;
            this.PeriodStartDateTime = PeriodStartDateTime;
            EntityCreated();
        }
        public void UpdateExtendOffersValiditySupplier(int ExtendOfferValidityStatusId)
        {
            this.SupplierExtendOfferValidityStatusId = ExtendOfferValidityStatusId;
            EntityUpdated();
        }
        public void SetAsReported(bool IsReported)
        {
            this.IsReported = IsReported;
            EntityUpdated();
        }
        public void StartSupplierPeriod()
        {
            this.PeriodStartDateTime = DateTime.Now;
            EntityUpdated();
        }
        public void Update()
        {
            EntityUpdated();
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
    }
}
