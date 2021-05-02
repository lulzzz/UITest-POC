using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.IDM
{
    public class SupplierTest
    {
        private const string selectedCr = "768768";
        private const string selectedCrName = "name";

        [Fact]
        public void Should_Construct_Supplier()
        {
            Supplier supplier = new Supplier(selectedCr, selectedCrName, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            _ = new Supplier();
            _ = supplier.SupplierUserProfiles;
            _ = supplier.Invitations;
            _ = supplier.offers;
            _ = supplier.SupplierBlocks;
            _ = supplier.FavouriteSuppliers;

            supplier.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            Supplier supplier = new Supplier(selectedCr, selectedCrName, new List<UserNotificationSetting>() { new UserNotificationSetting() });
            supplier.Update(selectedCr, selectedCrName);
            Assert.Equal(selectedCr, supplier.SelectedCr);
        }

        [Fact]
        public void Should_AddNotificationSettings()
        {
            Supplier supplier = new Supplier(selectedCr, selectedCrName, new List<UserNotificationSetting>() { new UserNotificationSetting("00", 1, 1, true, true, 1) });
            supplier.AddNotificationSettings(new List<UserNotificationSetting>() { new UserNotificationSetting("00", 1, 2, true, true, 1) });
            Assert.NotEmpty(supplier.NotificationSetting);
        }

    }
}
