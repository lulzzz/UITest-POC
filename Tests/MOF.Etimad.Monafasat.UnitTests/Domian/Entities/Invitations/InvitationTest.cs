using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Invitations
{
    public class InvitationTest
    {

        [Theory]
        [InlineData("1010000154", Enums.InvitationStatus.New, Enums.InvitationRequestType.Invitation, false)]
        public void ShouldCreateNewInvitation(string commericalRegisterNo, Enums.InvitationStatus statusId, Enums.InvitationRequestType invitationTypeId, bool invitedByQualification)
        {
            Invitation invitation = new Invitation(commericalRegisterNo, statusId, invitationTypeId, invitedByQualification);

            Assert.Equal(commericalRegisterNo, invitation.CommericalRegisterNo);
            Assert.Equal((int)statusId, invitation.StatusId);
            Assert.Equal((int)invitationTypeId, invitation.InvitationTypeId);
            Assert.False(invitation.InvitedByQualification);
            Assert.Equal(ObjectState.Added, invitation.State);

        }


        [Theory]
        [InlineData("1010000154", Enums.InvitationStatus.New, Enums.InvitationRequestType.Invitation, false, (int)Enums.InvititedSupplierTypes.HasCR)]
        public void ShouldCreateNewInvitationWithSupplierType(string commericalRegisterNo, Enums.InvitationStatus statusId, Enums.InvitationRequestType invitationTypeId, bool invitedByQualification, int supplierType)
        {
            Invitation invitation = new Invitation(commericalRegisterNo, statusId, invitationTypeId, invitedByQualification, supplierType);

            Assert.Equal(commericalRegisterNo, invitation.CommericalRegisterNo);
            Assert.Equal((int)statusId, invitation.StatusId);
            Assert.Equal((int)invitationTypeId, invitation.InvitationTypeId);
            Assert.Equal(supplierType, invitation.SupplierType);
            Assert.False(invitation.InvitedByQualification);
            Assert.Equal(ObjectState.Added, invitation.State);

        }

        [Fact]
        public void ShouldSendNewInvitation()
        {
            Invitation invitation = new Invitation();

            invitation.SendInvitation();

            Assert.Equal((int)Enums.InvitationStatus.New, invitation.StatusId);
            Assert.Equal(DateTime.Now.Date, invitation.SendingDate);
            Assert.Equal(ObjectState.Modified, invitation.State);

        }


        [Fact]
        public void ShouldAddBillInfo()
        {
            Invitation invitation = new Invitation();
            BillInfo billInfo = new BillInfo(10, DateTime.Now.Date, DateTime.Now.Date.AddDays(1), "101000010010");
            invitation.AddBillInfo(billInfo);

            Assert.Equal(billInfo.AmountDue, invitation.BillInfo.AmountDue);
            Assert.Equal(billInfo.ExpiryDate, invitation.BillInfo.ExpiryDate);
            Assert.Equal(ObjectState.Added, invitation.BillInfo.State);
            Assert.Equal(ObjectState.Modified, invitation.State);

        }

        [Theory]
        [InlineData((int)Enums.InvitationStatus.Approved, "")]
        public void ShouldUpdateInvitationStatusToBeApproved(int invitationStatusId, string rejectionReason)
        {
            Invitation invitation = new Invitation();

            invitation.UpdateStatus(invitationStatusId, rejectionReason);

            Assert.Equal((int)Enums.InvitationStatus.Approved, invitation.StatusId);
            Assert.Equal(DateTime.Now.Date, invitation.SubmissionDate);
            Assert.Equal(ObjectState.Modified, invitation.State);

        }

        [Theory]
        [InlineData((int)Enums.InvitationStatus.Rejected, "Test Reason")]
        public void ShouldUpdateInvitationStatusToBeRejected(int invitationStatusId, string rejectionReason)
        {
            Invitation invitation = new Invitation();

            invitation.UpdateStatus(invitationStatusId, rejectionReason);

            Assert.Equal((int)Enums.InvitationStatus.Rejected, invitation.StatusId);
            Assert.Equal(DateTime.Now.Date, invitation.SubmissionDate);
            Assert.Equal(rejectionReason, invitation.RejectionReason);
            Assert.Equal(ObjectState.Modified, invitation.State);

        }

        [Theory]
        [InlineData((int)Enums.InvitationStatus.New, "")]
        public void ShouldUpdateInvitationStatusToBeNewAndSendingDate(int invitationStatusId, string rejectionReason)
        {
            Invitation invitation = new Invitation();

            invitation.UpdateStatus(invitationStatusId, rejectionReason);

            Assert.Equal((int)Enums.InvitationStatus.New, invitation.StatusId);
            Assert.Equal(DateTime.Now.Date, invitation.SendingDate);
            Assert.Equal(ObjectState.Modified, invitation.State);
        }


        [Theory]
        [InlineData((int)Enums.InvitationStatus.Withdrawal, "")]
        public void ShouldUpdateInvitationStatusToBeWithdrawal(int invitationStatusId, string rejectionReason)
        {
            Invitation invitation = new Invitation();

            invitation.UpdateStatus(invitationStatusId, rejectionReason);

            Assert.Equal((int)Enums.InvitationStatus.Withdrawal, invitation.StatusId);
            Assert.Equal(DateTime.Now.Date, invitation.WithdrawalDate);
            Assert.Equal(ObjectState.Modified, invitation.State);
        }

        [Theory]
        [InlineData((int)Enums.InvitationStatus.Withdrawal, "")]
        public void ShouldDeleteBillInfoWhenSupplierWithdrawFromInvitation(int invitationStatusId, string rejectionReason)
        {
            Invitation invitation = new Invitation();
            BillInfo billInfo = new BillInfo(10, DateTime.Now.Date, DateTime.Now.Date.AddDays(1), "101000010010");
            invitation.AddBillInfo(billInfo);

            invitation.UpdateStatus(invitationStatusId, rejectionReason);

            Assert.Equal((int)Enums.InvitationStatus.Withdrawal, invitation.StatusId);
            Assert.Equal(DateTime.Now.Date, invitation.WithdrawalDate);
            Assert.All(invitation.BillInfo.BillPaymentDetails, p => Assert.Equal(ObjectState.Deleted, p.State));
            Assert.Equal(ObjectState.Deleted, invitation.BillInfo.State);
            Assert.Equal(ObjectState.Modified, invitation.State);
        }

        [Fact]
        public void ShoulDeleteInvitation()
        {
            Invitation invitation = new Invitation();

            invitation.Delete();

            Assert.Equal(ObjectState.Deleted, invitation.State);
        }

        [Fact]
        public void ShouldUpdateInvitationStatusForResendingInvitationsAgain()
        {
            Invitation invitation = new Invitation();

            invitation.UpdateStatusForResend();

            Assert.Equal((int)Enums.InvitationStatus.New, invitation.StatusId);
            Assert.Null(invitation.WithdrawalDate);
            Assert.Null(invitation.RejectionReason);
            Assert.Null(invitation.SubmissionDate);
            Assert.Equal(ObjectState.Modified, invitation.State);
        }
    }
}
