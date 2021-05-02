using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class MandatoryListChangeRequestStatusTest
    {
        [Fact]
        public void Should_Construct_MandatoryListChangeRequestStatus()
        {
            MandatoryListChangeRequestStatus mandatoryListChangeRequestStatus = new MandatoryListChangeRequestStatus();
            _ = mandatoryListChangeRequestStatus.StatusId;
            _ = mandatoryListChangeRequestStatus.NameAr;
            _ = mandatoryListChangeRequestStatus.NameEn;

            mandatoryListChangeRequestStatus.ShouldNotBeNull();
        }
    }
}
