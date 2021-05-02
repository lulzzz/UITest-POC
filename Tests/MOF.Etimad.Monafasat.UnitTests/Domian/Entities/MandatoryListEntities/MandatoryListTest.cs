using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders;
using System.Linq;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.MandatoryListEntities
{
    public class MandatoryListTest
    {

        [Fact]
        public void ShouldAddNewMandatoryList()
        {
            MandatoryList MandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();

            MandatoryList.Add();

            Assert.Equal((int)Enums.MandatoryListStatus.UnderEstablishing, MandatoryList.StatusId);
            Assert.All(MandatoryList.Products, c => Assert.Equal(ObjectState.Added, c.State));
        }

        [Fact]
        public void ShouldAddMandatoryListChangeRequest()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();
            MandatoryListChangeRequest changeRequest = new MandatoryListDefault().GetMandatoryListChangeRequest();
            mandatoryList.AddChangeRequest(changeRequest);

            Assert.All(mandatoryList.ChangeRequests, c => Assert.Equal((int)Enums.MandatoryListChangeRequestStatus.WaitingApproval, c.StatusId));
            Assert.All(mandatoryList.ChangeRequests, c => Assert.Equal(ObjectState.Added, c.State));
        }

        [Fact]
        public void ShouldUpdateMandatoryList()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();
            MandatoryList updatedMandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();

            mandatoryList.Update(updatedMandatoryList);

            Assert.Equal(updatedMandatoryList.DivisionCode, mandatoryList.DivisionCode);
            Assert.Equal(updatedMandatoryList.DivisionNameAr, mandatoryList.DivisionNameAr);
            Assert.Equal(updatedMandatoryList.DivisionNameEn, mandatoryList.DivisionNameEn);
        }

        [Fact]
        public void UpdateMandatoryListShouldThrowBusinessExceptionWhenWrongStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove();
            MandatoryList updatedMandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.Update(updatedMandatoryList));

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Fact]
        public void ShouldSendMandatoryListToApprove()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();

            mandatoryList.SendToApproval();

            Assert.Equal((int)Enums.MandatoryListStatus.WaitingApproval, mandatoryList.StatusId);
        }

        [Fact]
        public void ShouldThrowBusinessExceptionWhileSendMandatoryListToApproveWithWrongStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusApprove();

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.SendToApproval());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);

        }

        [Fact]
        public void ShouldApproveMandatoryList()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove();

            mandatoryList.Approve();

            Assert.Equal((int)Enums.MandatoryListStatus.Approved, mandatoryList.StatusId);

        }

        [Fact]
        public void ShouldThrowBusinessExceptionWihleApproveAndWrongStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.Approve());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);

        }

        [Theory]
        [InlineData("Test Rejection Reason")]
        public void ShouldRejectMandatoryListCorrectlly(string rejectionReason)
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove();

            mandatoryList.Reject(rejectionReason);

            Assert.Equal(rejectionReason, mandatoryList.RejectionReason);
            Assert.Equal((int)Enums.MandatoryListStatus.Rejected, mandatoryList.StatusId);
        }

        [Theory]
        [InlineData("Test Rejection Reason")]
        public void ShouldThrowBusinessExceptionWhileRejctingMandatoryListWhenWrongStatus(string rejectionReason)
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.Reject(rejectionReason));

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Theory]
        [InlineData("")]
        public void ShouldThrowBusinessExceptionWhileRejctingMandatoryListWithoutReason(string rejectionReason)
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove();

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.Reject(rejectionReason));

            Assert.Equal(Resources.MandatoryListResources.ErrorMessages.RejectionReasonCannotBeEmpty, e.Message);
        }

        [Fact]
        public void ShouldDeleteMandatoryList()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();

            mandatoryList.Delete();

            Assert.False(mandatoryList.IsActive);
        }

        [Fact]
        public void ShouldThrowBusinessExceptionWhileDeletingMandatoryListWhenWrongStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove();

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.Delete());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Fact]
        public void ShouldCreateCancelRequest()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusApprove();

            mandatoryList.RequestDelete();

            Assert.Equal((int)Enums.MandatoryListStatus.WaitingCancelApproval, mandatoryList.StatusId);
        }

        [Fact]
        public void ShouldThrowBusinessExceptionWhileCreatingCancelRequestWhenWrongStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingApprove();

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.RequestDelete());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Fact]
        public void ShouldApproveCancelRequest()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingCancelRequest();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            mandatoryList.ApproveDelete();

            Assert.Equal((int)Enums.MandatoryListStatus.Canceled, mandatoryList.StatusId);
        }


        [Fact]
        public void ShouldThrowBusinessExceptionWhileApprovingCancelRequestWhenWrongStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusApprove();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.ApproveDelete());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Fact]
        public void ShouldApproveEditRequest()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusApprove();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            mandatoryList.ApproveEdit();

            Assert.Equal((int)Enums.MandatoryListChangeRequestStatus.Approved, mandatoryList.ChangeRequests.FirstOrDefault().StatusId);
        }

        [Fact]
        public void ShouldThrowBusinessExceptionWhileApprovingEditRequestWhenWrongStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.ApproveEdit());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Theory]
        [InlineData("Rejection Reason Test")]
        public void ShouldRejectEditRequest(string rejectionReason)
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusApprove();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            mandatoryList.RejectEdit(rejectionReason);

            Assert.Equal((int)Enums.MandatoryListChangeRequestStatus.Rejected, mandatoryList.ChangeRequests.FirstOrDefault().StatusId);
            Assert.Equal(rejectionReason, mandatoryList.ChangeRequests.FirstOrDefault().RejectionReason);
        }

        [Theory]
        [InlineData("Rejection Reason Test")]
        public void ShouldThrowBusinessExceptionWhileRejectingEditRequestWhenWrongStatus(string rejectionReason)
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.RejectEdit(rejectionReason));

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Fact]
        public void ShouldCloseEditRequest()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusApprove();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequestWithRejectedStatus());

            mandatoryList.CloseEdit();

            Assert.Equal((int)Enums.MandatoryListChangeRequestStatus.Closed, mandatoryList.ChangeRequests.FirstOrDefault().StatusId);
        }

        [Fact]
        public void ShouldThrowBusinessExceptionWhileClosingEditRequestWhenWrongStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequestWithRejectedStatus());

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.CloseEdit());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Fact]
        public void ShouldThrowBusinessExceptionWhileClosingEditRequestWhenWrongRequestStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusApprove();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.CloseEdit());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Theory]
        [InlineData("Rejection Reason Test")]
        public void ShouldRejectCancelRequest(string rejectionReason)
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingCancelRequest();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            mandatoryList.RejectDelete(rejectionReason);

            Assert.Equal((int)Enums.MandatoryListStatus.CancelRejected, mandatoryList.StatusId);
            Assert.Equal(rejectionReason, mandatoryList.RejectionReason);
        }

        [Theory]
        [InlineData("Rejection Reason Test")]
        public void ShouldThrowBusinessExceptionWhileRejectingCancelRequestWhenWrongStatus(string rejectionReason)
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithProdcuts();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.RejectDelete(rejectionReason));

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Theory]
        [InlineData("")]
        public void ShouldThrowBusinessExceptionWhileRejectingCancelRequestWhenEmptyReason(string rejectionReason)
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusWaitingCancelRequest();
            mandatoryList.ChangeRequests.Add(new MandatoryListDefault().GetMandatoryListChangeRequest());

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.RejectDelete(rejectionReason));

            Assert.Equal(Resources.MandatoryListResources.ErrorMessages.RejectionReasonCannotBeEmpty, e.Message);
        }


        [Fact]
        public void ShouldReopenMandatoryList()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusRejected();

            mandatoryList.Reopen();

            Assert.Equal((int)Enums.MandatoryListStatus.UnderEstablishing, mandatoryList.StatusId);
        }

        [Fact]
        public void ShouldThrowBusinessExceptionWhileReOpenMandatoryListtWhenWrongRequestStatus()
        {
            MandatoryList mandatoryList = new MandatoryListDefault().GetMandatoryListWithStatusApprove();

            var e = Assert.Throws<BusinessRuleException>(() => mandatoryList.Reopen());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

    }
}
