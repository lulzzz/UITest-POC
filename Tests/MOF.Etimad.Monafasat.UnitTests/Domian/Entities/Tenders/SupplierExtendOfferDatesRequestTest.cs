using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class SupplierExtendOfferDatesRequestTest
    {
        private readonly string _extendOfferDatesReason = "Extend Offer Dates Reason For test";
        private readonly DateTime _extendOfferDatesRequestedDate = DateTime.Now;
        private readonly int _agencyCommunicationRequestId = 100;
        private readonly string _cr = "Cr1001";
        private readonly int _supplierExtendOfferDatesRequestId = 200;

        [Fact]
        public void Should_Empty_Construct_SupplierExtendOfferDatesRequest()
        {
            var supplierExtendOfferDatesRequest = new SupplierExtendOfferDatesRequest();
            supplierExtendOfferDatesRequest.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Constructor_SetValues()
        {
            var supplierExtendOfferDatesRequest = new SupplierExtendOfferDatesRequest(_extendOfferDatesReason, _extendOfferDatesRequestedDate, _agencyCommunicationRequestId, _cr, _supplierExtendOfferDatesRequestId);
            supplierExtendOfferDatesRequest.ShouldNotBeNull();
            supplierExtendOfferDatesRequest.State.ShouldBe(SharedKernal.ObjectState.Added);
            supplierExtendOfferDatesRequest.ExtendOfferDatesRequestReason.ShouldBe(_extendOfferDatesReason);
            supplierExtendOfferDatesRequest.ExtendOfferDatesRequestedDate.ShouldBe(_extendOfferDatesRequestedDate);
            supplierExtendOfferDatesRequest.AgencyCommunicationRequestId.ShouldBe(_agencyCommunicationRequestId);
            supplierExtendOfferDatesRequest.CR.ShouldBe(_cr);
        }

        [Fact]
        public void Should_Delete()
        {
            var supplierExtendOfferDatesRequest = new SupplierExtendOfferDatesRequest();
            supplierExtendOfferDatesRequest.Delete();
            supplierExtendOfferDatesRequest.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }

        [Fact]
        public void Should_Update()
        {
            var supplierExtendOfferDatesRequest = new SupplierExtendOfferDatesRequest();
            supplierExtendOfferDatesRequest.Update();
            supplierExtendOfferDatesRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActive()
        {
            var supplierExtendOfferDatesRequest = new SupplierExtendOfferDatesRequest();
            supplierExtendOfferDatesRequest.DeActive();
            supplierExtendOfferDatesRequest.IsActive.ShouldBe(false);
            supplierExtendOfferDatesRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetActive()
        {
            var supplierExtendOfferDatesRequest = new SupplierExtendOfferDatesRequest();
            supplierExtendOfferDatesRequest.SetActive();
            supplierExtendOfferDatesRequest.IsActive.ShouldBe(true);
            supplierExtendOfferDatesRequest.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }
    }
}
