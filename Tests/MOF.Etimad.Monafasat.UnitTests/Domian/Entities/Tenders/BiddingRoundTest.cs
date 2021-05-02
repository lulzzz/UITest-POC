using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class BiddingRoundTest
    {
        private readonly DateTime _startDate = DateTime.Now;
        private readonly DateTime _endDate = DateTime.Now;
        private readonly int _biddingRoundStatusId = 1;
        private readonly int _tenderId = 10;

        [Fact]
        public void Should_Empty_Construct_biddingRound()
        {
            var biddingRound = new Core.Entities.BiddingRound();
            biddingRound.ShouldNotBeNull();
        }

        [Fact]
        public void Should_BiddingRound_ContructStartAndEndDate()
        {
            var biddingRound = new Core.Entities.BiddingRound(_startDate, _endDate, _biddingRoundStatusId, _tenderId);

            biddingRound.StartDate.ShouldBe(_startDate);
            biddingRound.EndDate.ShouldBe(_endDate);
            biddingRound.BiddingRoundStatusId.ShouldBe(_biddingRoundStatusId);
            biddingRound.TenderId.ShouldBe(_tenderId);
        }

        [Fact]
        public void Should_Update()
        {
            var biddingRound = new Core.Entities.BiddingRound();
            biddingRound.Update(_startDate, _endDate, _biddingRoundStatusId, _tenderId);

            biddingRound.StartDate.ShouldBe(_startDate);
            biddingRound.EndDate.ShouldBe(_endDate);
            biddingRound.BiddingRoundStatusId.ShouldBe(_biddingRoundStatusId);
            biddingRound.TenderId.ShouldBe(_tenderId);
        }

        [Fact]
        public void Should_UpdateDates()
        {
            var biddingRound = new Core.Entities.BiddingRound();
            biddingRound.UpdateDates(_startDate, _endDate);

            biddingRound.StartDate.ShouldBe(_startDate);
            biddingRound.EndDate.ShouldBe(_endDate);
        }

        [Fact]
        public void Should_UpdateStatus()
        {
            var biddingRound = new Core.Entities.BiddingRound();
            biddingRound.UpdateStatus(_biddingRoundStatusId);

            biddingRound.BiddingRoundStatusId.ShouldBe(_biddingRoundStatusId);
        }

        [Fact]
        public void Should_Deactivates()
        {
            var biddingRound = new Core.Entities.BiddingRound();
            biddingRound.BiddingRoundOffers = new List<BiddingRoundOffer>()
            {
                new BiddingRoundOffer()
            };
            biddingRound.Deactive();

            biddingRound.IsActive.ShouldBe(false);
            biddingRound.BiddingRoundOffers[0].IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetTender()
        {
            var biddingRound = new Core.Entities.BiddingRound();
            Tender tender = new Tender();
            biddingRound.SetTender(tender);
            biddingRound.Tender.ShouldNotBeNull();
        }
    }
}
