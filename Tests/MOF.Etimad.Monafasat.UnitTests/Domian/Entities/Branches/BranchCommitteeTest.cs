using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Branches
{
    public class BranchCommitteeTest
    {
        private readonly BranchCommitteeDefaults branchCommitteeDefaults = new BranchCommitteeDefaults();

        [Fact]
        public void Empty_ConstructBranchCommittee()
        {
            var branchCommittee = new BranchCommittee();

            branchCommittee.ShouldNotBeNull();
            branchCommittee.BranchCommitteeId.ShouldBe(0);
            branchCommittee.Branch.ShouldBeNull();
            branchCommittee.Committee.ShouldBeNull();
        }

        [Fact]
        public void ConstructBranchCommittee()
        {
            var branchCommittee = new BranchCommittee(branchCommitteeDefaults._branchId, branchCommitteeDefaults._committeeId);

            Assert.Equal(branchCommitteeDefaults._branchId, branchCommittee.BranchId);
            Assert.Equal(branchCommitteeDefaults._committeeId, branchCommittee.CommitteeId);
            Assert.Equal(DateTime.Now.Date, branchCommittee.CreatedAt.Date);
            Assert.True(branchCommittee.IsActive);
            Assert.Equal(SharedKernal.ObjectState.Added, branchCommittee.State);
        }

        [Fact]
        public void DeActiveBranchCommitteeTest()
        {
            BranchCommittee branchCommittee = branchCommitteeDefaults.ReturnBranchCommitteeDefaults();
            branchCommittee.DeActive();

            Assert.Equal(branchCommitteeDefaults._branchId, branchCommittee.BranchId);
            Assert.Equal(branchCommitteeDefaults._committeeId, branchCommittee.CommitteeId);
            Assert.Equal(DateTime.Now.Date, branchCommittee.CreatedAt.Date);
            Assert.False(branchCommittee.IsActive);
            branchCommittee.ShouldNotBeNull();
            branchCommittee.IsActive.ShouldNotBeNull();
            branchCommittee.IsActive.Value.ShouldBeFalse();
        }

        [Fact]
        public void DeleteBranchCommitteeTest()
        {
            BranchCommittee branchCommittee = branchCommitteeDefaults.ReturnBranchCommitteeDefaults();

            branchCommittee.Delete();

            Assert.Equal(ObjectState.Deleted, branchCommittee.State);

        }
    }
}
