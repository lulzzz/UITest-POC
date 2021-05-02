using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AreaTest
    {
        [Fact]
        public void Should_Construct_Area()
        {
            Area area = new Area();
            _ = area.AreaId;
            _ = area.NameAr;
            _ = area.NameEn;

            area.ShouldNotBeNull();
        }
    }
}
