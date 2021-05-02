using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Enquiries
{
    public class EnquiryReplyTests
    {
        private const string EnquiryReplyMessage = "EnquiryReplyMessage";
        private const int EnquiryId = 1;
        private const int EnquiryReplyStatusId = 1;
        private const int CommitteeId = 1;
        private const bool IsComment = true;

        [Fact]
        public void Should_Empty_Construct_EnquiryReply()
        {
            EnquiryReply enquiryReply = new EnquiryReply();
            enquiryReply.ShouldNotBeNull();
        }
        [Fact]
        public void Should_Construct_EnquiryReply()
        {
            var enquiryReply = new EnquiryReply(EnquiryReplyMessage, EnquiryId, EnquiryReplyStatusId, CommitteeId, IsComment);

            enquiryReply.ShouldNotBeNull();
            enquiryReply.EnquiryId.ShouldBe(EnquiryId);
            enquiryReply.CommitteeId.ShouldBe(CommitteeId);
            enquiryReply.EnquiryReplyMessage.ShouldBe(EnquiryReplyMessage);
            enquiryReply.EnquiryReplyStatusId.ShouldBe(EnquiryReplyStatusId);
            enquiryReply.EnquiryId.ShouldBe(EnquiryId);
        }

        [Fact]
        public void Should_Update_EnquiryReply()
        {
            var enquiryReplyId = 1;
            var enquiryReplyMessage = "EnquiryReplyMessage updated";
            var enquiryReplyStatusId = 2;
            var committeeId = 2;
            var isComment = false;
            var enquiryReply = new EnquiryReply(EnquiryReplyMessage, EnquiryId, EnquiryReplyStatusId, CommitteeId, IsComment);

            var updated = enquiryReply.Update(enquiryReplyId, enquiryReplyStatusId, enquiryReplyMessage, committeeId, isComment);

            updated.ShouldNotBeNull();
            updated.EnquiryReplyId.ShouldBe(enquiryReplyId);
            updated.IsComment.ShouldBe(isComment);
            updated.CommitteeId.ShouldBe(committeeId);
            updated.EnquiryReplyMessage.ShouldBe(enquiryReplyMessage);
            updated.EnquiryReplyStatusId.ShouldBe(enquiryReplyStatusId);
        }

        [Fact]
        public void Should_Delete_EnquiryReply()
        {
            var enquiryReply = new EnquiryReply(EnquiryReplyMessage, EnquiryId, EnquiryReplyStatusId, CommitteeId, IsComment);

            enquiryReply.Delete();

            enquiryReply.ShouldNotBeNull();
            enquiryReply.State.ShouldBe(ObjectState.Deleted);
        }
        [Fact]
        public void Should_DeActive_EnquiryReply()
        {
            var enquiryReply = new EnquiryReply(EnquiryReplyMessage, EnquiryId, EnquiryReplyStatusId, CommitteeId, IsComment);

            enquiryReply.DeActive();

            enquiryReply.ShouldNotBeNull();
            enquiryReply.IsActive.ShouldNotBeNull();
            enquiryReply.IsActive.Value.ShouldBeFalse();
        }
        [Fact]
        public void Should_SetActive_EnquiryReply()
        {
            var enquiryReply = new EnquiryReply(EnquiryReplyMessage, EnquiryId, EnquiryReplyStatusId, CommitteeId, IsComment);

            enquiryReply.DeActive();
            enquiryReply.SetActive();

            enquiryReply.ShouldNotBeNull();
            enquiryReply.IsActive.ShouldNotBeNull();
            enquiryReply.IsActive.Value.ShouldBeTrue();
        }

        [Fact]
        public void Should_ApproveEnquiryReply()
        {
            var enquiryReply = new EnquiryReply(EnquiryReplyMessage, EnquiryId, EnquiryReplyStatusId, CommitteeId, IsComment);

            enquiryReply.ApproveEnquiryReply();

            enquiryReply.ShouldNotBeNull();
            enquiryReply.EnquiryReplyStatusId.ShouldBe((int)Enums.EnquiryReplyStatus.Approved);
        }
    }
}
