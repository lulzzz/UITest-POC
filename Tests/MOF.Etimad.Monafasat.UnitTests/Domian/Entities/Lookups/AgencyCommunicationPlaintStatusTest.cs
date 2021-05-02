using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AgencyCommunicationPlaintStatusTest
    {
        [Fact]
        public void Should_Construct_AgencyCommunicationPlaintStatus()
        {
            AgencyCommunicationPlaintStatus agencyCommunicationPlaintStatus = new AgencyCommunicationPlaintStatus();
            _ = agencyCommunicationPlaintStatus.Id;
            _ = agencyCommunicationPlaintStatus.Name;

            agencyCommunicationPlaintStatus.ShouldNotBeNull();
        }
    }
}
