using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class NegotiationReasonTest
    {
        private const int id = 1;
        private const string name = "name";

        [Fact]
        public void Should_Construct_NegotiationReason()
        {
            NegotiationReason negotiationReason = new NegotiationReason(id, name);
            _ = new NegotiationReason();
            _ = negotiationReason.NegotiationReasonId;
            _ = negotiationReason.Name;

            negotiationReason.ShouldNotBeNull();
        }
    }
}
