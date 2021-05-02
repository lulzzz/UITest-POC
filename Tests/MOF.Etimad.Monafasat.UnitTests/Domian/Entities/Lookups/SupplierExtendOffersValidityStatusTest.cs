using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class SupplierExtendOffersValidityStatusTest
    {
        private const int id = 1;
        private const string name = "name";

        [Fact]
        public void Should_Construct_SupplierExtendOffersValidityStatus()
        {
            SupplierExtendOffersValidityStatus supplierExtendOffersValidityStatus = new SupplierExtendOffersValidityStatus(id, name);
            _ = new SupplierExtendOffersValidityStatus();

            supplierExtendOffersValidityStatus.ShouldNotBeNull();
        }
    }
}
