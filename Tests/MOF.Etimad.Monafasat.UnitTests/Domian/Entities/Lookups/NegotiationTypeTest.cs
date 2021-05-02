using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class NegotiationTypeTest
    {
        private const int id = 1;
        private const string name = "name";

        [Fact]
        public void Should_Construct_NegotiationType()
        {
            NegotiationType negotiationType = new NegotiationType(id, name);
            _ = new NegotiationType();
            _ = negotiationType.NegotiationTypeId;
            _ = negotiationType.Name;

            negotiationType.ShouldNotBeNull();
        }
    }
}
