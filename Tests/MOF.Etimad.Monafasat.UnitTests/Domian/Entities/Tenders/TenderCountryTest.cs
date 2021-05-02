using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderCountryTest
    {
        private const int CountryId = 1;

        [Fact]
        public void Should_Construct_TenderConstructionWork()
        {
            TenderCountry tenderCountry = new TenderCountry(CountryId);
            _ = new TenderCountry();
            _ = tenderCountry.Country;
            _ = tenderCountry.TenderId;
            _ = tenderCountry.Tender;

            tenderCountry.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            TenderCountry tenderCountry = new TenderCountry(CountryId);
            tenderCountry.Update();
            tenderCountry.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Delete()
        {
            TenderCountry tenderCountry = new TenderCountry(CountryId);
            tenderCountry.Delete();
            Assert.Equal(ObjectState.Deleted, tenderCountry.State);
        }

        [Fact]
        public void Should_Deactive()
        {
            TenderCountry tenderCountry = new TenderCountry(CountryId);
            tenderCountry.DeActive();
            Assert.False(tenderCountry.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            TenderCountry tenderCountry = new TenderCountry(CountryId);
            tenderCountry.SetActive();
            Assert.True(tenderCountry.IsActive);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TenderCountry tenderCountry = new TenderCountry(CountryId);
            tenderCountry.SetAddedState();
            Assert.Equal(0, tenderCountry.Id);
        }
    }
}
