using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class ChangeRequestStatusTest
    {
        [Fact]
        public void Should_Construct_ChangeRequestStatus()
        {
            ChangeRequestStatus changeRequestStatus = new ChangeRequestStatus();
            _ = changeRequestStatus.NameAr;
            _ = changeRequestStatus.NameEn;
            _ = changeRequestStatus.Id;

            changeRequestStatus.ShouldNotBeNull();
        }
    }
}
