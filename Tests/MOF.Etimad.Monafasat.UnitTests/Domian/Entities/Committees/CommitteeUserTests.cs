using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Committees
{
    public class CommitteeUserTests
    {
        private const int CommitteeId = 1;
        private const int UserProfileId = 1;
        private const int UserRoleId = 1;
        private const string RelatedAgencyCode = "AgencyCode";

        [Fact]
        public void Should_Empty_Construct_CommitteeUser()
        {
            var committeeUser = new CommitteeUser();

            committeeUser.ShouldNotBeNull();
            committeeUser.CommitteeUserId.ShouldBe(0);
            committeeUser.RoleName.ShouldBeNullOrEmpty();
            committeeUser.UserRole.ShouldBeNull();
            committeeUser.UserProfile.ShouldBeNull();
            committeeUser.Committee.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_CommitteeUser()
        {
            var committeeUser = new CommitteeUser(CommitteeId, UserProfileId, UserRoleId);

            committeeUser.ShouldNotBeNull();
            committeeUser.CommitteeId.ShouldBe(CommitteeId);
            committeeUser.UserProfileId.ShouldBe(UserProfileId);
            committeeUser.UserRoleId.ShouldBe(UserRoleId);
        }
        [Fact]
        public void Should_Construct_CommitteeUser_With_AgencyCode()
        {
            var committeeUser = new CommitteeUser(CommitteeId, UserProfileId, UserRoleId, RelatedAgencyCode);

            committeeUser.ShouldNotBeNull();
            committeeUser.CommitteeId.ShouldBe(CommitteeId);
            committeeUser.UserProfileId.ShouldBe(UserProfileId);
            committeeUser.UserRoleId.ShouldBe(UserRoleId);
            committeeUser.RelatedAgencyCode.ShouldBe(RelatedAgencyCode);
        }

        [Fact]
        public void Should_DeActive_CommitteeUser()
        {
            var committeeUser = new CommitteeUser(CommitteeId, UserProfileId, UserRoleId, RelatedAgencyCode);

            committeeUser.DeActive();

            committeeUser.ShouldNotBeNull();
            committeeUser.IsActive.ShouldNotBeNull();
            committeeUser.IsActive.Value.ShouldBeFalse();
        }

    }
}