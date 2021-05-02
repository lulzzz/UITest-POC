using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AgencyCommunicationRequestStatusTest
    {
        [Fact]
        public void Should_Construct_AgencyCommunicationRequestStatus()
        {
            AgencyCommunicationRequestStatus agencyCommunicationRequestStatus = new AgencyCommunicationRequestStatus();
            _ = agencyCommunicationRequestStatus.Id;
            _ = agencyCommunicationRequestStatus.Name;

            agencyCommunicationRequestStatus.ShouldNotBeNull();
        }
    }
}
