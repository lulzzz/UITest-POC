using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Branches
{
    public class BranchUserTest
    {
        private readonly BranchUserDefaults branchUserDefaults = new BranchUserDefaults();
        [Fact]
        public void Empty_ConstructBranchUser()
        {

            var branchUser = new BranchUser();

            branchUser.ShouldNotBeNull();
            branchUser.BranchUserId.ShouldBe(0);
            branchUser.Branch.ShouldBeNull();
            branchUser.UserProfile.ShouldBeNull();
            branchUser.UserRole.ShouldBeNull();
        }

        [Fact]
        public void ConstructBranchUser()
        {

            BranchUser branchUser = branchUserDefaults.ReturnBranchUserDefaults();

            Assert.Equal(branchUserDefaults._branchId, branchUser.BranchId);
            Assert.Equal(branchUserDefaults._userRoleId, branchUser.UserRoleId);
            Assert.Equal(branchUserDefaults._userProfileId, branchUser.UserProfileId);
            Assert.Equal(branchUserDefaults._relatedAgencyCode, branchUser.RelatedAgencyCode);
            Assert.Equal(branchUserDefaults._estimatedValueFrom, branchUser.EstimatedValueFrom);
            Assert.Equal(branchUserDefaults._estimatedValueTo, branchUser.EstimatedValueTo);
            Assert.Equal(DateTime.Now.Date, branchUser.CreatedAt.Date);
            Assert.True(branchUser.IsActive);
            Assert.Equal(SharedKernal.ObjectState.Added, branchUser.State);
        }

        [Fact]
        public void DeActiveBranchUserTest()
        {
            BranchUser branchUser = branchUserDefaults.ReturnBranchUserDefaults();
            branchUser.DeActive();
            branchUser.ShouldNotBeNull();
            branchUser.IsActive.ShouldNotBeNull();
            branchUser.IsActive.Value.ShouldBeFalse();
        }

        [Fact]
        public void CreateUserProfileTest()
        {
            var branchUser = new BranchUser(branchUserDefaults._branchId, branchUserDefaults._userProfileId,
                branchUserDefaults._userRoleId, branchUserDefaults._relatedAgencyCode,
                branchUserDefaults._estimatedValueFrom, branchUserDefaults._estimatedValueTo);

            branchUser.CreateUserProfile();

            branchUser.ShouldNotBeNull();
            branchUser.UserProfile.ShouldNotBeNull();
        }
    }
}
