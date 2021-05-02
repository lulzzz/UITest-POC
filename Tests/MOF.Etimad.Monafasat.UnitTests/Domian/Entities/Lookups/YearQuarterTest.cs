using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class YearQuarterTest
    {
        private const string nameAr = "name";
        private const string nameEn = "name";
        private const int id = 1;

        [Fact]
        public void Should_Construct_YearQuarter()
        {
            YearQuarter yearQuarter = new YearQuarter(id, nameEn, nameAr);
            _ = new YearQuarter();

            yearQuarter.ShouldNotBeNull();
        }
    }
}
