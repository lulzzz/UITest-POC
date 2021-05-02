using MOF.Etimad.Monafasat.Core;
using Shouldly;
using Xunit;


namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.IDM
{
    public class SupplierUserProfileTest
    {
        private const string selectedCr = "0000";
        private const int userProfileId = 1;

        [Fact]
        public void Should_Construct_SupplierUserProfile()
        {
            SupplierUserProfile spplierUserProfile = new SupplierUserProfile(selectedCr, userProfileId);
            _ = new SupplierUserProfile();
            _ = spplierUserProfile.Supplier;
            _ = spplierUserProfile.UserProfile;
            spplierUserProfile.ShouldNotBeNull();
        }
    }
}
