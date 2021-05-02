using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderUnitStatusTest
    {
        private const string name = "name";
        private const int tenderUnitStatusId = 1;

        [Fact]
        public void Should_Construct_TenderUnitStatus()
        {
            TenderUnitStatus tenderUnitStatus = new TenderUnitStatus(tenderUnitStatusId, name);
            tenderUnitStatus.ShouldNotBeNull();
        }
    }
}
