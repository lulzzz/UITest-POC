using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class OfferSolidarityTest
    {
        private const string email = "test@test.com";
        private const string mobile = "";
        private const UnRegisteredSuppliersInvitationType SupplierInvitationType = UnRegisteredSuppliersInvitationType.Existed;
        private const SupplierSolidarityStatus SolidarityStatus = SupplierSolidarityStatus.New;
        private const int offerId = 1;
        private const string commericalRegisterNo = "0000";
        [Fact]
        public void Should_Construct_SupplierPreQualificationDocument()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(email, mobile, SupplierInvitationType, SolidarityStatus);
            _ = new OfferSolidarity();
            _ = offerSolidarity.Id;
            _ = offerSolidarity.SupplierCombinedDetail;
            _ = offerSolidarity.Offer;
            _ = offerSolidarity.RejectionReason;
            _ = offerSolidarity.SolidarityStatus;
            _ = offerSolidarity.SubmissionDate;
            _ = offerSolidarity.Supplier;

            offerSolidarity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierPreQualificationDocument_Second()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(offerId);

            offerSolidarity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierPreQualificationDocument_Third()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(offerId, commericalRegisterNo);

            offerSolidarity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierPreQualificationDocument_Fourth()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(commericalRegisterNo, (int)SupplierInvitationType);

            offerSolidarity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierPreQualificationDocument_Fifth()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(commericalRegisterNo, SolidarityStatus, SupplierInvitationType);

            offerSolidarity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_UpdateStatus()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(email, mobile, SupplierInvitationType, SolidarityStatus);
            offerSolidarity.UpdateStatus(SupplierSolidarityStatus.Approved);

            Assert.Equal((int)SupplierSolidarityStatus.Approved, offerSolidarity.StatusId);
        }

        [Fact]
        public void Should_Delete()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(email, mobile, SupplierInvitationType, SolidarityStatus);
            offerSolidarity.Delete();

            Assert.False(offerSolidarity.IsActive);
            Assert.Equal(ObjectState.Deleted, offerSolidarity.State);
        }

        [Fact]
        public void Should_Update()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(email, mobile, SupplierInvitationType, SolidarityStatus);
            offerSolidarity.Update();

            offerSolidarity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_DeActive()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(email, mobile, SupplierInvitationType, SolidarityStatus);
            offerSolidarity.DeActive();

            Assert.False(offerSolidarity.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            OfferSolidarity offerSolidarity = new OfferSolidarity(email, mobile, SupplierInvitationType, SolidarityStatus);
            offerSolidarity.SetActive();

            Assert.True(offerSolidarity.IsActive);
        }
    }
}
