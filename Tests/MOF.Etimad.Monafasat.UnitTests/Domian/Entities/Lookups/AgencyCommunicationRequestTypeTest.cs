using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AgencyCommunicationRequestTypeTest
    {
        [Fact]
        public void Should_Construct_AgencyCommunicationRequestType()
        {
            AgencyCommunicationRequestType agencyCommunicationRequestType = new AgencyCommunicationRequestType();
            _ = agencyCommunicationRequestType.Id;
            _ = agencyCommunicationRequestType.Name;

            agencyCommunicationRequestType.ShouldNotBeNull();
        }
    }
}
