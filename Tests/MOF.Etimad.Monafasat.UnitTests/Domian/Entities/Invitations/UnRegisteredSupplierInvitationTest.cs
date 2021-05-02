using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using Xunit;
namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Invitations
{
    public class UnRegisteredSupplierInvitationTest
    {
        [Theory]
        [InlineData("0100200200", (int)Enums.InvitationSendType.Mobile)]
        public void ShouldCreateNewUnRegisteredSupplierInvitationObject(string emailOrMobileNo, int sentTypeId)
        {
            UnRegisteredSuppliersInvitation unRegisteredSuppliers = new UnRegisteredSuppliersInvitation(emailOrMobileNo, sentTypeId);

            Assert.Equal(((int)Enums.InvitationSendType.Mobile).ToString(), unRegisteredSuppliers.MobileNo);
            Assert.Equal(emailOrMobileNo, unRegisteredSuppliers.Email);
            Assert.Equal(ObjectState.Added, unRegisteredSuppliers.State);
        }

        [Theory]
        [InlineData("101010200020", (int)Enums.InvitationRequestType.Invitation, "mm@mm.com", "01001001000", Enums.InvitationStatus.New, "Test Description")]
        public void ShouldCreateNewUnRegisteredSupplierInvitationObjectWithCrNumber(string crNumber, int invitationTypeId, string email, string mobileNo, Enums.InvitationStatus statusId, string description)
        {
            UnRegisteredSuppliersInvitation unRegisteredSuppliers = new UnRegisteredSuppliersInvitation(crNumber, invitationTypeId, email, mobileNo, statusId, description);

            Assert.Equal(crNumber, unRegisteredSuppliers.CrNumber);
            Assert.Equal(invitationTypeId, unRegisteredSuppliers.InvitationTypeId);
            Assert.Equal(email, unRegisteredSuppliers.Email);
            Assert.Equal(mobileNo, unRegisteredSuppliers.MobileNo);
            Assert.Equal((int)statusId, unRegisteredSuppliers.InvitationStatusId);
            Assert.Equal(description, unRegisteredSuppliers.Description);
            Assert.Equal(ObjectState.Added, unRegisteredSuppliers.State);
        }

        [Fact]
        public void ShouldDeleteUnRegeisteredSupplier()
        {
            UnRegisteredSuppliersInvitation unRegisteredSuppliers = new UnRegisteredSuppliersInvitation();

            unRegisteredSuppliers.Delete();

            Assert.Equal(ObjectState.Deleted, unRegisteredSuppliers.State);
        }

        [Fact]
        public void ShouldDeActiveUnRegeisteredSupplier()
        {
            UnRegisteredSuppliersInvitation unRegisteredSuppliers = new UnRegisteredSuppliersInvitation();

            unRegisteredSuppliers.DeActive();

            Assert.False(unRegisteredSuppliers.IsActive);
            Assert.Equal(ObjectState.Modified, unRegisteredSuppliers.State);
        }

        [Theory]
        [InlineData((int)Enums.InvitationStatus.New)]
        public void ShouldUpdateUnRegeisteredSupplierStatus(int statusid)
        {
            UnRegisteredSuppliersInvitation unRegisteredSuppliers = new UnRegisteredSuppliersInvitation();

            unRegisteredSuppliers.UpdateStatus(statusid);

            Assert.Equal(statusid, unRegisteredSuppliers.InvitationStatusId);
            Assert.Equal(ObjectState.Modified, unRegisteredSuppliers.State);
        }
    }
}
