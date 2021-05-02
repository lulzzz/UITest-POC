using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class SupplierSecondNegotiationStatusTest
    {
        private const int id = 1;
        private const string name = "name";

        [Fact]
        public void Should_Construct_SupplierSecondNegotiationStatus()
        {
            SupplierSecondNegotiationStatus supplierSecondNegotiationStatus = new SupplierSecondNegotiationStatus(id, name);
            _ = new SupplierSecondNegotiationStatus();

            supplierSecondNegotiationStatus.ShouldNotBeNull();
        }
    }
}
