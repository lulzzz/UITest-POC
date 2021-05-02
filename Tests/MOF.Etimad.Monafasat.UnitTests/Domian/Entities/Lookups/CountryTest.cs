using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class CountryTest
    {
        [Fact]
        public void Should_Construct_Country()
        {
            Country country = new Country();
            _ = country.CountryId;
            _ = country.NameAr;
            _ = country.NameEn;
            _ = country.IsGolf;
            _ = country.TenderCountries;

            country.ShouldNotBeNull();
        }
    }
}
