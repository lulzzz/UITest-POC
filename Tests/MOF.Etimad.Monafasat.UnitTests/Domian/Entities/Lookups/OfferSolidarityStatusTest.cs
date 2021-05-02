using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class OfferSolidarityStatusTest
    {
        [Fact]
        public void Should_Construct_OfferSolidarityStatus()
        {
            OfferSolidarityStatus offerSolidarityStatus = new OfferSolidarityStatus();
            _ = offerSolidarityStatus.CombinedStatusId;
            _ = offerSolidarityStatus.Name;

            offerSolidarityStatus.ShouldNotBeNull();
        }
    }
}
