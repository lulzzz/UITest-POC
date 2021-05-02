using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class InvitationTypeTest
    {
        [Fact]
        public void Should_Construct_InvitationType()
        {
            InvitationType invitationType = new InvitationType();
            _ = invitationType.InvitationTypeId;
            _ = invitationType.NameAr;
            _ = invitationType.NameEn;

            invitationType.ShouldNotBeNull();
        }
    }
}
