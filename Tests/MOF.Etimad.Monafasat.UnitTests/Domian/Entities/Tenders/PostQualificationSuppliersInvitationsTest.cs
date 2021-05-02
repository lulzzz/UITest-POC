using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class PostQualificationSuppliersInvitationsTest
    {
        private readonly string _commercialNumber = "Cr2001";

        [Fact]
        public void Should_Empty_Construct_PostQualificationSuppliersInvitations()
        {
            var postQualificationSuppliersInvitations = new PostQualificationSuppliersInvitations();
            postQualificationSuppliersInvitations.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_CommercialNumber()
        {
            var postQualificationSuppliersInvitations = new PostQualificationSuppliersInvitations(_commercialNumber);
            postQualificationSuppliersInvitations.CommercialNumber.ShouldBe(_commercialNumber);
            postQualificationSuppliersInvitations.StatusId.ShouldBe((int)Enums.InvitationStatus.New);
            postQualificationSuppliersInvitations.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_Delete()
        {
            var postQualificationSuppliersInvitations = new PostQualificationSuppliersInvitations();
            postQualificationSuppliersInvitations.Delete();
            postQualificationSuppliersInvitations.IsActive.ShouldBe(false);
            postQualificationSuppliersInvitations.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
