using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Committees
{
    public class CommitteeTests
    {
        private const int CommitteeId = 1;
        private const string AgencyCode = "agencyCode";
        private const string CommitteeName = "committeeName";
        private const string Address = "address";
        private const string Phone = "phone";
        private const string Fax = "fax";
        private const string Email = "email";
        private const string PostalCode = "postalCode";
        private const string ZipCode = "zipCode";
        private const bool IsActive = true;

        [Fact]
        public void Should_Empty_Construct_Committee()
        {
            var committee = new Committee();

            committee.ShouldNotBeNull();
            committee.CommitteeId.ShouldBe(0);
            committee.Agency.ShouldBeNull();
            committee.CommitteeType.ShouldBeNull();
            committee.CommitteeUsers.ShouldBeNull();
            committee.JoinTechnicalCommittees.ShouldBeNull();
            committee.BranchCommittees.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_Committee()
        {
            var committee = new Committee(CommitteeId, AgencyCode, CommitteeName, Address, Phone, Fax, Email, PostalCode, ZipCode);

            committee.ShouldNotBeNull();
            committee.CommitteeId.ShouldBe(CommitteeId);
            committee.AgencyCode.ShouldBe(AgencyCode);
            committee.CommitteeName.ShouldBe(CommitteeName);
            committee.Address.ShouldBe(Address);
            committee.Phone.ShouldBe(Phone);
            committee.Fax.ShouldBe(Fax);
            committee.Email.ShouldBe(Email);
            committee.PostalCode.ShouldBe(PostalCode);
            committee.ZipCode.ShouldBe(ZipCode);
            committee.IsActive.ShouldBe(IsActive);
        }

        [Fact]
        public void Should_Update_Committee()
        {
            var name = "updated name";
            var address = "updated address";
            var phone = "updated phone";
            var fax = "updated fax";
            var email = "updated email";
            var postalCode = "updated postalCode";
            var zipCode = "updated zipCode";
            var committee = new Committee(CommitteeId, AgencyCode, CommitteeName, Address, Phone, Fax, Email, PostalCode, ZipCode);

            committee.Update(name, address, phone, fax, email, postalCode, zipCode);

            committee.ShouldNotBeNull();
            committee.CommitteeName.ShouldBe(name);
            committee.Address.ShouldBe(address);
            committee.Phone.ShouldBe(phone);
            committee.Fax.ShouldBe(fax);
            committee.Email.ShouldBe(email);
            committee.PostalCode.ShouldBe(postalCode);
            committee.ZipCode.ShouldBe(zipCode);
        }

        [Fact]
        public void Should_DeActive_Committee()
        {
            var committee = new Committee(CommitteeId, AgencyCode, CommitteeName, Address, Phone, Fax, Email, PostalCode, ZipCode);

            committee.DeActive();

            committee.ShouldNotBeNull();
            committee.IsActive.ShouldNotBeNull();
            committee.IsActive.Value.ShouldBeFalse();
        }
    }
}
