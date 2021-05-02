using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderDatesChangeTest
    {
        private const int tenderId = 1;
        private const string lastOfferPresentationTime = "1:00";
        private const string offersOpeningTime = "1:00";
        private const string offersCheckingTime = "1:00";
        private const string rejectReason = "reason";

        [Fact]
        public void Should_Construct_TenderDatesChange()
        {
            TenderDatesChange tenderDatesChange = new TenderDatesChange(DateTime.Now.Date, DateTime.Now.Date, lastOfferPresentationTime,
                DateTime.Now.Date, offersOpeningTime, DateTime.Now.Date, offersCheckingTime, tenderId);
            _ = new TenderDatesChange();

            tenderDatesChange.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            TenderDatesChange tenderDatesChange = new TenderDatesChange(DateTime.Now.Date, DateTime.Now.Date, lastOfferPresentationTime,
                DateTime.Now.Date, offersOpeningTime, DateTime.Now.Date, offersCheckingTime, tenderId);
            tenderDatesChange.Delete();
            Assert.Equal(ObjectState.Deleted, tenderDatesChange.State);
        }

        [Fact]
        public void Should_Deactive()
        {
            TenderDatesChange tenderDatesChange = new TenderDatesChange(DateTime.Now.Date, DateTime.Now.Date, lastOfferPresentationTime,
                DateTime.Now.Date, offersOpeningTime, DateTime.Now.Date, offersCheckingTime, tenderId);
            tenderDatesChange.DeActive();
            Assert.False(tenderDatesChange.IsActive);
        }

        [Fact]
        public void Should_AcceptRevision()
        {
            TenderDatesChange tenderDatesChange = new TenderDatesChange(DateTime.Now.Date, DateTime.Now.Date, lastOfferPresentationTime,
                DateTime.Now.Date, offersOpeningTime, DateTime.Now.Date, offersCheckingTime, tenderId);
            tenderDatesChange.AcceptRevision();
            Assert.Equal((int)Enums.TenderStatus.Approved, tenderDatesChange.StatusId);
        }

        [Fact]
        public void Should_RejectRevision()
        {
            TenderDatesChange tenderDatesChange = new TenderDatesChange(DateTime.Now.Date, DateTime.Now.Date, lastOfferPresentationTime,
                DateTime.Now.Date, offersOpeningTime, DateTime.Now.Date, offersCheckingTime, tenderId);
            tenderDatesChange.RejectRevision(rejectReason);
            Assert.Equal((int)Enums.TenderStatus.Rejected, tenderDatesChange.StatusId);
        }
    }
}
