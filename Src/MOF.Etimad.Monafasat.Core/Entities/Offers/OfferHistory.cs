using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("OfferHistory", Schema = "Offer")]
    public class OfferHistory : AuditableEntity
    {
        [Key]
        public int OfferHistoryId { get; private set; }
        [Required]
        public int UserId { get; private set; }
        public int TenderId { get; private set; }
        public string CommericalRegisterNo { get; private set; }

        [ForeignKey(nameof(Offer))]
        public int OfferId { get; private set; }
        public Offer Offer { get; private set; }

        [ForeignKey(nameof(TenderStatus))]
        public int TenderStatusId { get; private set; }
        public TenderStatus TenderStatus { get; private set; }

        [ForeignKey(nameof(OfferStatus))]
        public int OfferStatusId { get; private set; }
        public Entities.Lookups.OfferStatus OfferStatus { get; private set; }
        public int? OfferAcceptanceStatusId { get; set; }
        public int? OfferTechnicalEvaluationStatusId { get; set; }

        [StringLength(2000)]
        public string RejectionReason { get; private set; }

        [ForeignKey(nameof(Action))]
        public int ActionId { get; set; }
        public TenderAction Action { get; set; }

        public OfferHistory()
        {

        }

        public OfferHistory(int tenderStatusId, int offerStatusId, TenderActions tenderAction, int userId, string cr)
        {
            TenderStatusId = tenderStatusId;
            OfferStatusId = offerStatusId;
            ActionId = (int)tenderAction;
            UserId = userId;
            CommericalRegisterNo = cr;
            EntityCreated();
        }
    }
}
