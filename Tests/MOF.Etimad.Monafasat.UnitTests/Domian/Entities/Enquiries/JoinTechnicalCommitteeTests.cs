using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Enquiries
{
    public class JoinTechnicalCommitteeTests
    {
        private const int EnquiryId = 1;
        private const int CommitteeId = 1;


        [Fact]
        public void Should_Empty_Construct_JoinTechnicalCommittee()
        {
            var joinTechnicalCommittee = new JoinTechnicalCommittee();
            joinTechnicalCommittee.ShouldNotBeNull();
            joinTechnicalCommittee.JoinTechnicalCommitteeId.ShouldBe(0);
            joinTechnicalCommittee.Committee.ShouldBeNull();
        }
        [Fact]
        public void Should_Construct_JoinTechnicalCommittee()
        {
            var joinTechnicalCommittee = new JoinTechnicalCommittee(EnquiryId, CommitteeId);

            joinTechnicalCommittee.ShouldNotBeNull();
            joinTechnicalCommittee.EnquiryId.ShouldBe(EnquiryId);
            joinTechnicalCommittee.CommitteeId.ShouldBe(CommitteeId);
        }
        [Fact]
        public void Should_Delete_JoinTechnicalCommittee()
        {
            var joinTechnicalCommittee = new JoinTechnicalCommittee(EnquiryId, CommitteeId);

            joinTechnicalCommittee.Delete();

            joinTechnicalCommittee.ShouldNotBeNull();
            joinTechnicalCommittee.State.ShouldBe(ObjectState.Deleted);
        }
        [Fact]
        public void Should_DeActive_JoinTechnicalCommittee()
        {
            var joinTechnicalCommittee = new JoinTechnicalCommittee(EnquiryId, CommitteeId);

            joinTechnicalCommittee.DeActive();

            joinTechnicalCommittee.ShouldNotBeNull();
            joinTechnicalCommittee.IsActive.ShouldNotBeNull();
            joinTechnicalCommittee.IsActive.Value.ShouldBeFalse();
        }
        [Fact]
        public void Should_SetActive_JoinTechnicalCommittee()
        {
            var joinTechnicalCommittee = new JoinTechnicalCommittee(EnquiryId, CommitteeId);

            joinTechnicalCommittee.DeActive();
            joinTechnicalCommittee.SetActive();

            joinTechnicalCommittee.ShouldNotBeNull();
            joinTechnicalCommittee.IsActive.ShouldNotBeNull();
            joinTechnicalCommittee.IsActive.Value.ShouldBeTrue();
        }
    }
}
