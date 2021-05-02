using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;


namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Branches
{
    public class BranchTests
    {
        private readonly BranchDefaults branchDefaults = new BranchDefaults();

        [Fact]
        public void Empty_ConstructBranch()
        {
            var branch = new Branch();

            branch.ShouldNotBeNull();
            branch.BranchId.ShouldBe(0);
            branch.Agency.ShouldBeNull();
        }

        [Fact]
        public void ConstructBranch()
        {
            var branch = new Branch(branchDefaults._agencyCode, branchDefaults._branchName, branchDefaults._branchAddresses);

            Assert.Equal(branchDefaults._agencyCode, branch.AgencyCode);
            Assert.Equal(branchDefaults._branchName, branch.BranchName);
            Assert.Equal(branchDefaults._branchAddresses, branch.BranchAddresses);
            Assert.Equal(DateTime.Now.Date, branch.CreatedAt.Date);
            Assert.True(branch.IsActive);
            Assert.Equal(ObjectState.Added, branch.State);
        }

        [Fact]
        public void ConstructBranch_BranchId()
        {
            var branch = new Branch(1, branchDefaults._agencyCode, branchDefaults._branchName, branchDefaults._branchAddresses);

            Assert.Equal(1, branch.BranchId);
            Assert.Equal(branchDefaults._agencyCode, branch.AgencyCode);
            Assert.Equal(branchDefaults._branchName, branch.BranchName);
            Assert.Equal(branchDefaults._branchAddresses, branch.BranchAddresses);
            Assert.Equal(DateTime.Now.Date, branch.CreatedAt.Date);
            Assert.True(branch.IsActive);
            Assert.Equal(ObjectState.Added, branch.State);
        }

        [Fact]
        public void AddAddressesToBranchTest()
        {
            Branch branch = branchDefaults.ReturnBranchDefaults();
            branch.AddAddresses(new List<BranchAddress>());

            Assert.Equal(new List<BranchAddress>(), branch.BranchAddresses);
            Assert.Equal(ObjectState.Added, branch.State);
        }

        [Fact]
        public void DeActiveBranchTest()
        {
            Branch branch = branchDefaults.ReturnBranchDefaults();

            branch.DeActive();

            Assert.Equal(branchDefaults._agencyCode, branch.AgencyCode);
            Assert.Equal(branchDefaults._branchName, branch.BranchName);
            Assert.Equal(branchDefaults._branchAddresses, branch.BranchAddresses);
            Assert.Equal(DateTime.Now.Date, branch.CreatedAt.Date);
            branch.ShouldNotBeNull();
            branch.IsActive.ShouldNotBeNull();
            branch.IsActive.Value.ShouldBeFalse();
        }

        [Fact]
        public void UpdateBranchTest()
        {
            Branch branch = branchDefaults.ReturnBranchDefaults();

            branch.Update(branch.BranchName, branch.BranchAddresses);

            Assert.Equal("newbranch", branch.BranchName);
            Assert.Equal(new List<BranchAddress>(), branch.BranchAddresses);
            branch.ShouldNotBeNull();
            branch.BranchName.ShouldBe(branch.BranchName);
            branch.BranchAddresses.ShouldBe(branch.BranchAddresses);
        }

        [Fact]
        public void AddCommitteesToBranchTest()
        {
            Branch branch = branchDefaults.ReturnBranchDefaults();
            branch.AddCommittees(new List<BranchCommittee>());

            Assert.Equal(new List<BranchCommittee>(), branch.BranchCommittees);
            Assert.Equal(ObjectState.Added, branch.State);
        }

        [Fact]
        public void UpdateCommitteesToBranchTest()
        {
            Branch branch = branchDefaults.ReturnBranchDefaults();
            branch.UpdateCommittees(new List<BranchCommittee>());

            Assert.Equal(new List<BranchCommittee>(), branch.BranchCommittees);
            Assert.Equal(ObjectState.Added, branch.State);
        }
    }
}
