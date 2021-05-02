using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class MandatoryListStatusTest
    {
        [Fact]
        public void Should_Construct_MandatoryListStatus()
        {
            MandatoryListStatus mandatoryListStatus = new MandatoryListStatus();
            _ = mandatoryListStatus.StatusId;
            _ = mandatoryListStatus.NameAr;
            _ = mandatoryListStatus.NameEn;

            mandatoryListStatus.ShouldNotBeNull();
        }
    }
}
