using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class UserRoleTest
    {
        [Fact]
        public void Should_Construct_UserRole()
        {
            UserRole userRole = new UserRole();
            _ = userRole.UserRoleId;
            _ = userRole.Name;
            _ = userRole.DisplayNameAr;
            _ = userRole.DisplayNameEn;
            _ = userRole.IsCombinedRole;

            userRole.ShouldNotBeNull();
        }
    }
}
