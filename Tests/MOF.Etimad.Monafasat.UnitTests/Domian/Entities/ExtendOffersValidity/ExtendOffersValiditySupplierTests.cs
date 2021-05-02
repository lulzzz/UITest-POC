using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.ExtendOffersValidity
{
    public class ExtendOffersValiditySupplierTests
    {
        private const int SupplierExtendOfferValidityStatusId = 1;
        private readonly DateTime _periodStartDateTime = DateTime.Today;
        private const int OfferId = 1;
        private const string SupplierCR = "SupplierCR";
        private const int ExtendOffersValidityId = 1;

        [Fact]
        public void Should_Empty_Construct_ExtendOffersValiditySupplier()
        {
            var extendOffersValiditySupplier = new ExtendOffersValiditySupplier();
            extendOffersValiditySupplier.ShouldNotBeNull();
        }
        [Fact]

        public void Should_Construct_ExtendOffersValiditySupplier()
        {
            var extendOffersValiditySupplier = new ExtendOffersValiditySupplier(SupplierExtendOfferValidityStatusId, _periodStartDateTime,
                OfferId, SupplierCR, ExtendOffersValidityId);

            extendOffersValiditySupplier.ShouldNotBeNull();
            extendOffersValiditySupplier.IsReported.ShouldBeFalse();
            extendOffersValiditySupplier.SupplierExtendOfferValidityStatusId.ShouldBe(SupplierExtendOfferValidityStatusId);
            extendOffersValiditySupplier.OfferId.ShouldBe(OfferId);
            extendOffersValiditySupplier.SupplierCR.ShouldBe(SupplierCR);
            extendOffersValiditySupplier.ExtendOffersValidityId.ShouldBe(ExtendOffersValidityId);
            extendOffersValiditySupplier.PeriodStartDateTime.ShouldBe(_periodStartDateTime);
        }

        [Fact]
        public void Should_UpdateExtendOffersValiditySupplier()
        {
            int extendOfferValidityStatusId = 2;
            var extendOffersValiditySupplier = new ExtendOffersValiditySupplier(SupplierExtendOfferValidityStatusId, _periodStartDateTime,
                OfferId, SupplierCR, ExtendOffersValidityId);

            extendOffersValiditySupplier.UpdateExtendOffersValiditySupplier(extendOfferValidityStatusId);

            extendOffersValiditySupplier.ShouldNotBeNull();
            extendOffersValiditySupplier.SupplierExtendOfferValidityStatusId.ShouldBe(extendOfferValidityStatusId);
        }
        [Fact]
        public void Should_SetAsReported()
        {
            var extendOffersValiditySupplier = new ExtendOffersValiditySupplier(SupplierExtendOfferValidityStatusId, _periodStartDateTime,
                OfferId, SupplierCR, ExtendOffersValidityId);

            extendOffersValiditySupplier.SetAsReported(true);

            extendOffersValiditySupplier.ShouldNotBeNull();
            extendOffersValiditySupplier.IsReported.ShouldBe(true);
        }
        [Fact]
        public void Should_StartSupplierPeriod()
        {
            var extendOffersValiditySupplier = new ExtendOffersValiditySupplier(SupplierExtendOfferValidityStatusId, _periodStartDateTime,
                OfferId, SupplierCR, ExtendOffersValidityId);

            extendOffersValiditySupplier.StartSupplierPeriod();

            extendOffersValiditySupplier.ShouldNotBeNull();
            extendOffersValiditySupplier.PeriodStartDateTime.ShouldNotBeNull();
            extendOffersValiditySupplier.PeriodStartDateTime.Value.Date.ShouldBe(DateTime.Now.Date);
        }
        [Fact]
        public void Should_Update()
        {
            var extendOffersValiditySupplier = new ExtendOffersValiditySupplier(SupplierExtendOfferValidityStatusId, _periodStartDateTime,
                OfferId, SupplierCR, ExtendOffersValidityId);

            extendOffersValiditySupplier.Update();

            extendOffersValiditySupplier.ShouldNotBeNull();
        }
        [Fact]
        public void Should_DeActive()
        {
            var extendOffersValiditySupplier = new ExtendOffersValiditySupplier(SupplierExtendOfferValidityStatusId, _periodStartDateTime,
                OfferId, SupplierCR, ExtendOffersValidityId);

            extendOffersValiditySupplier.DeActive();

            extendOffersValiditySupplier.ShouldNotBeNull();
            extendOffersValiditySupplier.IsActive.ShouldNotBeNull();
            extendOffersValiditySupplier.IsActive.Value.ShouldBeFalse();
        }
    }
}
