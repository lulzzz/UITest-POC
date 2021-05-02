using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BiddingRoundOffer", Schema = "Tender")]
    public class BiddingRoundOffer : AuditableEntity
    {
        #region Fields====================================================

        [Key]
        public int BiddingRoundOfferId { get; private set; }
        public int BiddingRoundId { get; private set; }
        [ForeignKey(nameof(BiddingRoundId))]
        public BiddingRound BiddingRound { get; private set; }
        public int OfferId { get; private set; }
        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; private set; }
        public decimal OfferValue { get; private set; }

        #endregion

        #region Constructors====================================================

        public BiddingRoundOffer()
        {

        }

        public BiddingRoundOffer(int biddingRoundId, int offerId, decimal offerValue)
        {
            BiddingRoundId = biddingRoundId;
            OfferId = offerId;
            OfferValue = offerValue;
            EntityCreated();
        }

        #endregion

        #region Methods====================================================

        public void Update(int biddingRoundOfferId, int offerId, decimal offerValue)
        {
            BiddingRoundOfferId = biddingRoundOfferId;
            OfferId = offerId;
            OfferValue = offerValue;
            EntityUpdated();
        }

        public void Deactive()
        {
            IsActive = false;
            EntityUpdated();
        }

        #endregion
    }
}