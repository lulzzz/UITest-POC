using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class OfferStatusTest
    {
        private const string nameAr = "name";
        private const string nameEn = "name";
        private const int id = 1;

        [Fact]
        public void Should_Construct_OfferStatus()
        {
            OfferStatus offerStatus = new OfferStatus(id, nameEn, nameAr);
            offerStatus.ShouldNotBeNull();
        }
    }
}
