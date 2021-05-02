using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Branches
{
    public class BranchAddressTest
    {
        private readonly BranchDefaults branchDefaults = new BranchDefaults();

        [Fact]
        public void Empty_ConstructBranchAddress()
        {
            var branchAddress = new BranchAddress();

            branchAddress.ShouldNotBeNull();
            branchAddress.BranchAddressId.ShouldBe(0);
            branchAddress.AddressType.ShouldBeNull();
        }

        [Fact]
        public void ConstructBranchAddress()
        {
            var branchAddress = new BranchAddress(branchDefaults.addressTypeId, branchDefaults.position,
                branchDefaults.address, branchDefaults.phone, branchDefaults.fax, branchDefaults.phone2,
                branchDefaults.fax2, branchDefaults.email, branchDefaults.description, branchDefaults.addressName,
                branchDefaults.cityCode, branchDefaults.postalCode, branchDefaults.zipCode, branchDefaults.isActive);

            Assert.Equal(branchDefaults.addressTypeId, branchAddress.AddressTypeId);
            Assert.Equal(branchDefaults.position, branchAddress.Position);
            Assert.Equal(branchDefaults.address, branchAddress.Address);
            Assert.Equal(branchDefaults.phone, branchAddress.Phone);
            Assert.Equal(branchDefaults.fax, branchAddress.Fax);
            Assert.Equal(branchDefaults.phone2, branchAddress.Phone2);
            Assert.Equal(branchDefaults.fax2, branchAddress.Fax2);
            Assert.Equal(branchDefaults.email, branchAddress.Email);
            Assert.Equal(branchDefaults.description, branchAddress.Description);
            Assert.Equal(branchDefaults.addressName, branchAddress.AddressName);
            Assert.Equal(branchDefaults.cityCode, branchAddress.CityCode);
            Assert.Equal(branchDefaults.postalCode, branchAddress.PostalCode);
            Assert.Equal(branchDefaults.zipCode, branchAddress.ZipCode);
            Assert.Equal(DateTime.Now.Date, branchAddress.CreatedAt.Date);
            Assert.True(branchAddress.IsActive);
            Assert.Equal(ObjectState.Added, branchAddress.State);
        }

        [Fact]
        public void DeActiveBranchAddressTest()
        {
            BranchAddress branchAddress = branchDefaults.ReturnBranchAddressDefaults();

            branchAddress.DeActive();

            Assert.Equal(branchDefaults.addressTypeId, branchAddress.AddressTypeId);
            Assert.Equal(branchDefaults.position, branchAddress.Position);
            Assert.Equal(branchDefaults.address, branchAddress.Address);
            Assert.Equal(branchDefaults.phone, branchAddress.Phone);
            Assert.Equal(branchDefaults.fax, branchAddress.Fax);
            Assert.Equal(branchDefaults.phone2, branchAddress.Phone2);
            Assert.Equal(branchDefaults.fax2, branchAddress.Fax2);
            Assert.Equal(branchDefaults.email, branchAddress.Email);
            Assert.Equal(branchDefaults.description, branchAddress.Description);
            Assert.Equal(branchDefaults.addressName, branchAddress.AddressName);
            Assert.Equal(branchDefaults.cityCode, branchAddress.CityCode);
            Assert.Equal(branchDefaults.postalCode, branchAddress.PostalCode);
            Assert.Equal(branchDefaults.zipCode, branchAddress.ZipCode);
            Assert.Equal(DateTime.Now.Date, branchAddress.CreatedAt.Date);
            Assert.Equal(false, branchAddress.IsActive);
        }
    }
}
