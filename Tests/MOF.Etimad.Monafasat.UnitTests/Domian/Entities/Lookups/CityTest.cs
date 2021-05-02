using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class CityTest
    {
        [Fact]
        public void Should_Construct_City()
        {
            City city = new City();
            _ = city.NameEnglish;
            _ = city.NameArabic;
            _ = city.CityID;

            city.ShouldNotBeNull();
        }
    }
}
