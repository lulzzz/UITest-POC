using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class TenderConditoinsStatusTest
    {
        private const bool isActive = true;
        private const string nameEn = "name";
        private const string nameAr = "name";

        [Fact]
        public void Should_Construct_TenderConditoinsStatus()
        {
            TenderConditoinsStatus tenderConditoinsStatus = new TenderConditoinsStatus(nameEn, nameAr, isActive);
            _ = new TenderConditoinsStatus();
            _ = tenderConditoinsStatus.TenderConditoinsStatusId;
            tenderConditoinsStatus.ShouldNotBeNull();
        }
    }
}
