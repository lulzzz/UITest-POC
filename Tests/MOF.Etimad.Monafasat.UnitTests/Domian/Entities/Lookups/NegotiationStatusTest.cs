using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class NegotiationStatusTest
    {
        private const int id = 1;
        private const string name = "name";

        [Fact]
        public void Should_Construct_NegotiationStatus()
        {
            NegotiationStatus negotiationStatus = new NegotiationStatus(id, name);
            _ = new NegotiationStatus();
            _ = negotiationStatus.NegotiationStatusId;
            _ = negotiationStatus.Name;

            negotiationStatus.ShouldNotBeNull();
        }
    }
}
