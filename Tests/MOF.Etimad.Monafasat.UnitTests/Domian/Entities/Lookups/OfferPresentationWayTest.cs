using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class OfferPresentationWayTest
    {
        [Fact]
        public void Should_Construct_OfferPresentationWay()
        {
            OfferPresentationWay offerPresentationWay = new OfferPresentationWay();
            _ = offerPresentationWay.Id;
            _ = offerPresentationWay.Name;

            offerPresentationWay.ShouldNotBeNull();
        }
    }
}
