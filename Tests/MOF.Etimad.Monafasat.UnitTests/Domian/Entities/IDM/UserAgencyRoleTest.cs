using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.IDM
{
    public class UserAgencyRoleTest
    {
        [Fact]
        public void Should_Construct_UserAgencyRole()
        {
            UserAgencyRole userAgencyRole = new UserAgencyRole();
            _ = userAgencyRole.UserAgencyRoleID;
            _ = userAgencyRole.AgencyCode;
            _ = userAgencyRole.Agency;
            _ = userAgencyRole.BranchId;
            _ = userAgencyRole.Branch;
            _ = userAgencyRole.UserProfileId;
            _ = userAgencyRole.UserProfile;
            _ = userAgencyRole.RoleName;

            userAgencyRole.ShouldNotBeNull();
        }
    }
}
