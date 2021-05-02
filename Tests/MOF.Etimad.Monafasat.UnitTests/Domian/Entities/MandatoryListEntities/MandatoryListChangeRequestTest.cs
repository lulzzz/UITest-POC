using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.MandatoryListEntities
{
    public class MandatoryListChangeRequestTest
    {
        [Fact]
        public void ShouldCreateNewChangeRequest()
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListDefault().GetMandatoryListChangeRequest();

            changeRequest.Add();

            Assert.Equal((int)Enums.MandatoryListChangeRequestStatus.WaitingApproval, changeRequest.StatusId);
            Assert.Equal(ObjectState.Added, changeRequest.State);
            Assert.All(changeRequest.Products, c => Assert.Equal(ObjectState.Added, c.State));
        }

        [Fact]
        public void ShouldThrowBusinessExceptionWhileCreateNewChangeRequestWhenNoProducts()
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListChangeRequest() { Products = new List<MandatoryListProductChangeRequest>() };

            var e = Assert.Throws<BusinessRuleException>(() => changeRequest.Add());

            Assert.Equal(Resources.MandatoryListResources.ErrorMessages.YouHaveToAddOneProductAtleast, e.Message);
        }

        [Fact]
        public void ShouldApproveChangeRequest()
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListDefault().GetMandatoryListChangeRequest();

            changeRequest.Approve();

            Assert.Equal((int)Enums.MandatoryListChangeRequestStatus.Approved, changeRequest.StatusId);
        }

        [Fact]
        public void ShoulsThrowBusinessExceptionWhileApprovingChangeRequestWhenWrongStatus()
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListChangeRequest();

            var e = Assert.Throws<BusinessRuleException>(() => changeRequest.Approve());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Theory]
        [InlineData("Test Rejection Reason")]
        public void ShouldrejectChangeRequest(string rejectionReason)
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListDefault().GetMandatoryListChangeRequest();

            changeRequest.Reject(rejectionReason);

            Assert.Equal((int)Enums.MandatoryListChangeRequestStatus.Rejected, changeRequest.StatusId);
            Assert.Equal(rejectionReason, changeRequest.RejectionReason);
        }

        [Theory]
        [InlineData("Test Rejection Reason")]
        public void ShoulsThrowBusinessExceptionWhileRejectingChangeRequestWhenWrongStatus(string rejectionReason)
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListChangeRequest();

            var e = Assert.Throws<BusinessRuleException>(() => changeRequest.Reject(rejectionReason));

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }

        [Theory]
        [InlineData("")]
        public void ShoulsThrowBusinessExceptionWhileRejectingChangeRequestWhenEmptyReason(string rejectionReason)
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListDefault().GetMandatoryListChangeRequest();

            var e = Assert.Throws<BusinessRuleException>(() => changeRequest.Reject(rejectionReason));

            Assert.Equal(Resources.MandatoryListResources.ErrorMessages.RejectionReasonCannotBeEmpty, e.Message);
        }


        [Fact]
        public void ShouldCloseChangeRequest()
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListDefault().GetMandatoryListChangeRequestWithRejectedStatus();

            changeRequest.Close();

            Assert.Equal((int)Enums.MandatoryListChangeRequestStatus.Closed, changeRequest.StatusId);
        }

        [Fact]
        public void ShoulsThrowBusinessExceptionWhileClosingChangeRequestWhenWrongStatus()
        {
            MandatoryListChangeRequest changeRequest = new MandatoryListDefault().GetMandatoryListChangeRequest();

            var e = Assert.Throws<BusinessRuleException>(() => changeRequest.Close());

            Assert.Equal(Resources.SharedResources.ErrorMessages.NotAddedAuthorized, e.Message);
        }
    }
}
