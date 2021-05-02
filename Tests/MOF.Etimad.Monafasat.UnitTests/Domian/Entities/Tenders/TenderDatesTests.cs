using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using System;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderDatesTests
    {
         
        [Fact]
        public void ConstructTenderDatesTests()
        {
            TenderDates tenderDates = new TenderDates(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), DateTime.Now.AddDays(7), DateTime.Now.AddDays(7));
            
            tenderDates.ShouldNotBeNull();
            Assert.Equal(DateTime.Now.AddDays(1).Date, tenderDates.AwardingExpectedDate.Value.Date);
            Assert.Equal(ObjectState.Added, tenderDates.State);

        }

        [Fact]
        public void UpdateTenderDatesTest()
        {
            TenderDates tenderDates = new TenderDates();

            tenderDates.UpdateTenderDates(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), DateTime.Now.AddDays(7), DateTime.Now.AddDays(7));
            Assert.Equal(DateTime.Now.AddDays(1).Date, tenderDates.AwardingExpectedDate.Value.Date);

            Assert.Equal(ObjectState.Modified, tenderDates.State);
        }

        [Fact]
        public void UpdateTenderConfirmationLetterDateTest()
        {
            TenderDates tenderDates = new TenderDates();

            tenderDates.UpdateTenderConfirmationLetterDate(DateTime.Now.AddDays(1));
            Assert.Equal(DateTime.Now.AddDays(1).Date, tenderDates.ParticipationConfirmationLetterDate.Value.Date);
            Assert.Equal(ObjectState.Modified, tenderDates.State);
        }


    }
}
