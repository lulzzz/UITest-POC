using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderStatusTest
    {
        [Fact]
        public void Should_Construct_TenderStatus()
        {
            TenderStatus tenderStatus = new TenderStatus();
            _ = tenderStatus.TenderStatusId;
            _ = tenderStatus.NameAr;
            _ = tenderStatus.NameEn;

            tenderStatus.ShouldNotBeNull();
        }
    }
}
