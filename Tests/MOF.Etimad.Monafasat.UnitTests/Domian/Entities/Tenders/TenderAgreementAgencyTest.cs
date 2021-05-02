using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class TenderAgreementAgencyTest
    {
        private readonly string _agencyCode = "AGC1001";
        private readonly int _tenderId = 100;

        [Fact]
        public void Should_Empty_Construct_TenderAgreementAgency()
        {
            var tenderAgreementAgency = new TenderAgreementAgency();
            tenderAgreementAgency.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetAgencyCodeAndTenderId()
        {
            var tenderAgreementAgency = new TenderAgreementAgency(_agencyCode, _tenderId);
            tenderAgreementAgency.ShouldNotBeNull();
            tenderAgreementAgency.AgencyCode.ShouldBe(_agencyCode);
            tenderAgreementAgency.TenderId.ShouldBe(_tenderId);
            tenderAgreementAgency.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_Constructor_SetAgencyCode()
        {
            var tenderAgreementAgency = new TenderAgreementAgency(_agencyCode);
            tenderAgreementAgency.ShouldNotBeNull();
            tenderAgreementAgency.AgencyCode.ShouldBe(_agencyCode);
            tenderAgreementAgency.State.ShouldBe(SharedKernal.ObjectState.Added);
        }


        [Fact]
        public void Should_UpdateagencyCodeAndTenderId()
        {
            var tenderAgreementAgency = new TenderAgreementAgency();
            tenderAgreementAgency.Update(_agencyCode, _tenderId);
            tenderAgreementAgency.ShouldNotBeNull();
            tenderAgreementAgency.AgencyCode.ShouldBe(_agencyCode);
            tenderAgreementAgency.TenderId.ShouldBe(_tenderId);
            tenderAgreementAgency.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeleteEntity()
        {
            var tenderAgreementAgency = new TenderAgreementAgency();
            tenderAgreementAgency.Delete();
            tenderAgreementAgency.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }

        [Fact]
        public void Should_DeactivateEntity()
        {
            var tenderAgreementAgency = new TenderAgreementAgency();
            tenderAgreementAgency.DeActive();
            tenderAgreementAgency.IsActive.ShouldBe(false);
            tenderAgreementAgency.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetActive()
        {
            var tenderAgreementAgency = new TenderAgreementAgency();
            tenderAgreementAgency.SetActive();
            tenderAgreementAgency.IsActive.ShouldBe(true);
            tenderAgreementAgency.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            var tenderAgreementAgency = new TenderAgreementAgency();
            tenderAgreementAgency.SetAddedState();
            tenderAgreementAgency.TenderAgreementAgencyId.ShouldBe(0);
            tenderAgreementAgency.State.ShouldBe(SharedKernal.ObjectState.Added);
        }
    }
}
