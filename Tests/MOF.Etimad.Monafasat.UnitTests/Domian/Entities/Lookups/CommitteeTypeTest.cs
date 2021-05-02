using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class CommitteeTypeTest
    {
        [Fact]
        public void Should_Construct_CommitteeType()
        {
            CommitteeType committeeType = new CommitteeType();
            _ = committeeType.CommitteeTypeId;
            _ = committeeType.NameAr;
            _ = committeeType.NameEn;
            _ = committeeType.Committees;

            committeeType.ShouldNotBeNull();
        }
    }
}
