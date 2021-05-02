using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.ExtendOffersValidity
{
    public class ExtendOffersValidityTests
    {
        private const int ExtendOffersValidityId = 1;
        private const int AgencyCommunicationRequestId = 1;
        private const int TENDER_ID = 1;
        private const int OffersDuration = 10;
        private const string ExtendOffersReason = "ExtendOffersReason";
        private const int ReplyReceivingDurationDays = 10;
        private const string ReplyReceivingDurationTime = "5:00 pm";

        [Fact]
        public void Should_Empty_Construct_ExtendOffersValidity()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity();
            extendOffersValidity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_ExtendOffersValidity_With_Agency()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity(ExtendOffersValidityId,
                AgencyCommunicationRequestId, OffersDuration, ExtendOffersReason,
                ReplyReceivingDurationDays, ReplyReceivingDurationTime);

            extendOffersValidity.ShouldNotBeNull();
            extendOffersValidity.ExtendOffersValidityId.ShouldBe(ExtendOffersValidityId);
            extendOffersValidity.IsActive.ShouldNotBeNull();
            extendOffersValidity.IsActive.Value.ShouldBeTrue();
            extendOffersValidity.AgencyCommunicationRequestId.ShouldBe(AgencyCommunicationRequestId);
            extendOffersValidity.OffersDuration.ShouldBe(OffersDuration);
            extendOffersValidity.NewOffersExpiryDate.Date.ShouldBe(DateTime.Now.AddDays(OffersDuration).Date);
            extendOffersValidity.ExtendOffersReason.ShouldBe(ExtendOffersReason);
            extendOffersValidity.ReplyReceivingDurationDays.ShouldBe(ReplyReceivingDurationDays);
            extendOffersValidity.ReplyReceivingDurationTime.ShouldBe(ReplyReceivingDurationTime);
        }
        [Fact]
        public void Should_Construct_ExtendOffersValidity()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity(ExtendOffersValidityId, OffersDuration, ExtendOffersReason,
                ReplyReceivingDurationDays, ReplyReceivingDurationTime);

            extendOffersValidity.ShouldNotBeNull();
            extendOffersValidity.ExtendOffersValidityId.ShouldBe(ExtendOffersValidityId);
            extendOffersValidity.IsActive.ShouldNotBeNull();
            extendOffersValidity.IsActive.Value.ShouldBeTrue();
            extendOffersValidity.OffersDuration.ShouldBe(OffersDuration);
            extendOffersValidity.NewOffersExpiryDate.Date.ShouldBe(DateTime.Now.AddDays(OffersDuration).Date);
            extendOffersValidity.ExtendOffersReason.ShouldBe(ExtendOffersReason);
            extendOffersValidity.ReplyReceivingDurationDays.ShouldBe(ReplyReceivingDurationDays);
            extendOffersValidity.ReplyReceivingDurationTime.ShouldBe(ReplyReceivingDurationTime);
        }
        [Fact]

        public void Should_Add_AgencyCommunicationRequest()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity(ExtendOffersValidityId, OffersDuration, ExtendOffersReason,
                ReplyReceivingDurationDays, ReplyReceivingDurationTime);

            extendOffersValidity.AddAgencyCommunicationRequest(TENDER_ID, AgencyCommunicationRequestId);
            extendOffersValidity.AgencyCommunicationRequest.ShouldNotBeNull();
            extendOffersValidity.AgencyCommunicationRequest.State.ShouldBe(ObjectState.Added);
        }
        [Fact]
        public void Should_Delete_ExtendOffersValidity()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity(ExtendOffersValidityId, OffersDuration, ExtendOffersReason,
                ReplyReceivingDurationDays, ReplyReceivingDurationTime);
            extendOffersValidity.Delete();

            extendOffersValidity.ShouldNotBeNull();
            extendOffersValidity.State.ShouldBe(ObjectState.Deleted);
        }
        [Fact]
        public void Should_DeActive_ExtendOffersValidity()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity(ExtendOffersValidityId, OffersDuration, ExtendOffersReason,
                ReplyReceivingDurationDays, ReplyReceivingDurationTime);
            extendOffersValidity.DeActive();

            extendOffersValidity.ShouldNotBeNull();
            extendOffersValidity.IsActive.ShouldNotBeNull();
            extendOffersValidity.IsActive.Value.ShouldBeFalse();
        }

        [Fact]
        public void Should_SetActive_ExtendOffersValidity()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity(ExtendOffersValidityId, OffersDuration, ExtendOffersReason,
                ReplyReceivingDurationDays, ReplyReceivingDurationTime);
            extendOffersValidity.DeActive();
            extendOffersValidity.SetActive();

            extendOffersValidity.ShouldNotBeNull();
            extendOffersValidity.IsActive.ShouldNotBeNull();
            extendOffersValidity.IsActive.Value.ShouldBeTrue();
        }
        [Fact]
        public void Should_Update_ExtendOffersValidity()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity(ExtendOffersValidityId, OffersDuration, ExtendOffersReason,
                ReplyReceivingDurationDays, ReplyReceivingDurationTime);
            extendOffersValidity.Update();

            extendOffersValidity.ShouldNotBeNull();
            extendOffersValidity.State.ShouldBe(ObjectState.Modified);
        }
    }
}
