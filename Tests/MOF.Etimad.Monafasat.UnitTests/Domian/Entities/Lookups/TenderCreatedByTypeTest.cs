using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderCreatedByTypeTest
    {
        [Fact]
        public void Should_Construct_TenderConditoinsStatus()
        {
            TenderCreatedByType tenderCreatedByType = new TenderCreatedByType();
            _ = tenderCreatedByType.TenderCreatedByTypeId;
            _ = tenderCreatedByType.NameAr;
            _ = tenderCreatedByType.NameEn;

            tenderCreatedByType.ShouldNotBeNull();
        }
    }
}
