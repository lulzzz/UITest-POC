using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Enquiries
{
    public class EnquiryTests
    {
        private const string EnquiryName = "EnquiryName";
        private const int TenderId = 1;
        private const string CommericalRegisterNo = "CommericalRegisterNo";

        [Fact]
        public void Should_Empty_Construct_Enquiry()
        {
            Enquiry enquiry = new Enquiry();
            enquiry.ShouldNotBeNull();
        }
        [Fact]
        public void Should_Construct_Enquiry()
        {
            var enquiry = new Enquiry(EnquiryName, TenderId, CommericalRegisterNo);

            enquiry.ShouldNotBeNull();
            enquiry.EnquiryName.ShouldBe(EnquiryName);
            enquiry.TenderId.ShouldBe(TenderId);
            enquiry.CommericalRegisterNo.ShouldBe(CommericalRegisterNo);
        }

        [Fact]
        public void Should_Delete_Enquiry()
        {
            var enquiry = new Enquiry(EnquiryName, TenderId, CommericalRegisterNo);

            enquiry.Delete();

            enquiry.ShouldNotBeNull();
            enquiry.State.ShouldBe(ObjectState.Deleted);
        }
        [Fact]
        public void Should_DeActive_Enquiry()
        {
            var enquiry = new Enquiry(EnquiryName, TenderId, CommericalRegisterNo);

            enquiry.DeActive();

            enquiry.ShouldNotBeNull();
            enquiry.IsActive.ShouldNotBeNull();
            enquiry.IsActive.Value.ShouldBeFalse();
        }
        [Fact]
        public void Should_SetActive_Enquiry()
        {
            var enquiry = new Enquiry(EnquiryName, TenderId, CommericalRegisterNo);
            enquiry.DeActive();

            enquiry.SetActive();
            enquiry.ShouldNotBeNull();
            enquiry.IsActive.ShouldNotBeNull();
            enquiry.IsActive.Value.ShouldBeTrue();
        }

        [Fact]
        public void Should_JoinTechnicalCommitee()
        {
            var enquiry = new Enquiry(EnquiryName, TenderId, CommericalRegisterNo);
            enquiry.SetJoinTechnicalCommittees(new List<JoinTechnicalCommittee>());
            enquiry.SetEnquiryReplies(new List<EnquiryReply>());

            enquiry.JoinTechnicalCommitee(1, 1, 1, "comment");

            enquiry.ShouldNotBeNull();
            enquiry.JoinTechnicalCommittees.Count.ShouldBe(1);
            enquiry.EnquiryReplies.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_SetCommunicationRequest()
        {
            var enquiry = new Enquiry(EnquiryName, TenderId, CommericalRegisterNo);

            enquiry.SetCommunicationRequest(new AgencyCommunicationRequest());

            enquiry.ShouldNotBeNull();
            enquiry.AgencyCommunicationRequest.ShouldNotBeNull();
        }

        [Fact]
        public void Should_RemoveJoinedCommittee()
        {
            var enquiry = new Enquiry(EnquiryName, TenderId, CommericalRegisterNo);
            enquiry.SetJoinTechnicalCommittees(new List<JoinTechnicalCommittee>());
            enquiry.SetEnquiryReplies(new List<EnquiryReply>());

            enquiry.JoinTechnicalCommitee(1, 1, 1, "comment");

            enquiry.RemoveJoinedCommittee(enquiry.JoinTechnicalCommittees, enquiry.EnquiryReplies, 0, 0);

            enquiry.ShouldNotBeNull();
            enquiry.JoinTechnicalCommittees.Count.ShouldBe(1);
            enquiry.JoinTechnicalCommittees[0].IsActive.ShouldBe(false);
            enquiry.EnquiryReplies.Count.ShouldBe(1);
            enquiry.EnquiryReplies[0].IsActive.ShouldBe(false);
        }
    }
}
