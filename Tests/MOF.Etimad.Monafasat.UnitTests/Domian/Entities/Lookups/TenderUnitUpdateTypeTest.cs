using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderUnitUpdateTypeTest
    {
        [Fact]
        public void Should_Construct_TenderUnitUpdateType()
        {
            TenderUnitUpdateType tenderUnitUpdateType = new TenderUnitUpdateType();
            _ = tenderUnitUpdateType.TenderUnitUpdateTypeId;
            _ = tenderUnitUpdateType.Name;
            tenderUnitUpdateType.ShouldNotBeNull();
        }
    }
}
