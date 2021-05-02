using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class BiddingRoundOfferTest
    {
        private readonly int _biddingRoundOfferId = 1;
        private readonly int _biddingRoundId = 2;
        private readonly int _offerId = 3;
        private readonly decimal _offerValue = 2000;

        [Fact]
        public void Should_Empty_Construct_BiddingRoundOffer()
        {
            var biddingRoundOffer = new BiddingRoundOffer();
            biddingRoundOffer.ShouldNotBeNull();
        }

        [Fact]
        public void Should_ContructOfferValue()
        {
            var biddingRoundOffer = new BiddingRoundOffer(_biddingRoundId, _offerId, _offerValue);

            biddingRoundOffer.BiddingRoundId.ShouldBe(_biddingRoundId);
            biddingRoundOffer.OfferId.ShouldBe(_offerId);
            biddingRoundOffer.OfferValue.ShouldBe(_offerValue);
        }

        [Fact]
        public void Should_Update()
        {
            var biddingRoundOffer = new BiddingRoundOffer();

            biddingRoundOffer.Update(_biddingRoundOfferId, _offerId, _offerValue);

            biddingRoundOffer.State.ShouldBe(SharedKernal.ObjectState.Modified);
            biddingRoundOffer.BiddingRoundOfferId.ShouldBe(_biddingRoundOfferId);
            biddingRoundOffer.OfferId.ShouldBe(_offerId);
            biddingRoundOffer.OfferValue.ShouldBe(_offerValue);
        }

        [Fact]
        public void Should_Deactivates()
        {
            var biddingRoundOffer = new BiddingRoundOffer();

            biddingRoundOffer.Deactive();
            biddingRoundOffer.State.ShouldBe(SharedKernal.ObjectState.Modified);
            biddingRoundOffer.IsActive.ShouldBe(false);
        }
    }
}
