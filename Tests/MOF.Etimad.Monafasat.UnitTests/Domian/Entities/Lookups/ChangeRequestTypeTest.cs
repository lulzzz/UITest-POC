using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class ChangeRequestTypeTest
    {
        [Fact]
        public void Should_Construct_ChangeRequestType()
        {
            ChangeRequestType changeRequestType = new ChangeRequestType();
            _ = changeRequestType.NameAr;
            _ = changeRequestType.NameEn;
            _ = changeRequestType.Id;

            changeRequestType.ShouldNotBeNull();
        }
    }
}
