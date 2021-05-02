using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class NegotiationSupplierStatusTest
    {
        private const int id = 1;
        private const string name = "name";

        [Fact]
        public void Should_Construct_NegotiationSupplierStatus()
        {
            NegotiationSupplierStatus negotiationStatus = new NegotiationSupplierStatus(id, name);
            _ = new NegotiationSupplierStatus();
            _ = negotiationStatus.NegotiationSupplierStatusId;
            _ = negotiationStatus.Name;

            negotiationStatus.ShouldNotBeNull();
        }
    }
}
