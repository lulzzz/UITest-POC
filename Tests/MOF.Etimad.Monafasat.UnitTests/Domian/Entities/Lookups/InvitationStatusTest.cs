using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class InvitationStatusTest
    {
        [Fact]
        public void Should_Construct_InvitationStatus()
        {
            InvitationStatus invitationStatus = new InvitationStatus();
            _ = invitationStatus.InvitationStatusId;
            _ = invitationStatus.NameAr;
            _ = invitationStatus.NameEn;

            invitationStatus.ShouldNotBeNull();
        }
    }
}
