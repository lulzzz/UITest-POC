using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.ExtendOffersValidity
{
    public class AgencyCommunicationRequestTests
    {
        private const int CommunicationRequestId = 1;
        private const int PLAINT_REQUEST_ID = 1;
        private const int AGENCY_REQUEST__STATUSE_ID = (int)Enums.AgencyCommunicationRequestStatus.RequestSent;
        private const int AGENCY_REQUEST_TYPE_ID = (int)Enums.AgencyCommunicationRequestType.Enquiry;

        private const int TENDER_ID = 1;
        private const string PlaintReason = "Plaint Reason";
        private const string REJECTION_REASON = "Rejection Reason";

        [Fact]
        public void Should_Empty_Construct_ExtendOffersValidity()
        {
            var extendOffersValidity = new Core.Entities.ExtendOffersValidity();
            extendOffersValidity.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_AgencyCommunicationRequest()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest(TENDER_ID, AGENCY_REQUEST_TYPE_ID, AGENCY_REQUEST__STATUSE_ID, RoleNames.supplier);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.StatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.RequestSent);
        }

        [Fact]
        public void Should_Construct_ExtendOfferValidityAgencyCommunicationRequest()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest(CommunicationRequestId, TENDER_ID, (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy, (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate, RoleNames.supplier);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.IsActive.ShouldNotBeNull();
            communicationRequest.IsActive.Value.ShouldBeTrue();
            communicationRequest.StatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.UnderUpdate);
            communicationRequest.AgencyRequestTypeId.ShouldBe((int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy);
        }

        [Fact]
        public void Should_Construct_PlaintAgencyCommunicationRequest()
        {
            var attachments = new List<CommunicationAttachmentModel>();
            attachments.Add(new CommunicationAttachmentModel() { FileNetReferenceId = "id", Name = "Name", AttachmentTypeId = (int)Enums.AttachmentType.PlainRequest });

            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest(CommunicationRequestId, Util.Encrypt(1), PLAINT_REQUEST_ID, Util.Encrypt(1), PlaintReason, RoleNames.supplier, attachments);

            communicationRequest.ShouldNotBeNull();
            communicationRequest.PlaintRequests.Count.ShouldBe(1);
            communicationRequest.PlaintAcceptanceStatusId.ShouldBe((int)Enums.AgencyPlaintStatus.New);
            communicationRequest.AgencyRequestTypeId.ShouldBe((int)Enums.AgencyCommunicationRequestType.Plaint);
            communicationRequest.StatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.RequestSent);
        }

        [Fact]
        public void Should_AddAgencyCommunicationRequestValidity()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.AddAgencyCommunicationRequestValidity(0, 20, "Extend Offers Reason", 10, "5:00 pm");
            communicationRequest.ExtendOffersValidity.ShouldNotBeNull();

            communicationRequest.ExtendOffersValidity.ShouldNotBeNull();
            communicationRequest.ExtendOffersValidity.IsActive.ShouldNotBeNull();
            communicationRequest.ExtendOffersValidity.IsActive.Value.ShouldBeTrue();
            communicationRequest.ExtendOffersValidity.OffersDuration.ShouldBe(20);
            communicationRequest.ExtendOffersValidity.NewOffersExpiryDate.Date.ShouldBe(DateTime.Now.AddDays(20).Date);
            communicationRequest.ExtendOffersValidity.ExtendOffersReason.ShouldBe("Extend Offers Reason");
            communicationRequest.ExtendOffersValidity.ReplyReceivingDurationDays.ShouldBe(10);
        }

        [Fact]
        public void Should_UpdateEscalatePlaint()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.UpdateEscalatePlaint((int)Enums.AgencyPlaintStatus.New, (int)Enums.AgencyCommunicationRequestStatus.RequestSent);

            communicationRequest.ShouldNotBeNull();
            communicationRequest.EscalationStatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.RequestSent);
            communicationRequest.EscalationAcceptanceStatusId.ShouldBe((int)Enums.AgencyPlaintStatus.New);
        }


        [Fact]
        public void Should_UpdateSupplierOfferExtendDatesRequestStatus()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.UpdateSupplierOfferExtendDatesRequestStatus((int)Enums.AgencyCommunicationRequestStatus.UnderRevision);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.StatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.UnderRevision);
        }

        [Fact]
        public void Should_UpdateAgencyCommunicationRequestStatus()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Approved);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.StatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.Approved);
        }

        [Fact]
        public void Should_UpdateAgencyCommunicationPlaintRequestStatus()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.UpdateAgencyCommunicationPlaintRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, (int)Enums.AgencyPlaintStatus.Rejected, null, "details", REJECTION_REASON);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.StatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.Pending);
            communicationRequest.PlaintAcceptanceStatusId.ShouldBe((int)Enums.AgencyPlaintStatus.Rejected);
            communicationRequest.TenderPlaintRequestProcedureId.ShouldBeNull();
            communicationRequest.RejectionReason.ShouldBe(REJECTION_REASON);
        }

        [Fact]
        public void Should_UpdateAgencyCommunicationEscalationRequestStatus()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.UpdateAgencyCommunicationEscalationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.Pending, (int)Enums.AgencyPlaintStatus.Rejected, null, "", REJECTION_REASON);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.EscalationStatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.Pending);
            communicationRequest.EscalationAcceptanceStatusId.ShouldBe((int)Enums.AgencyPlaintStatus.Rejected);
            communicationRequest.TenderPlaintRequestProcedureId.ShouldBeNull();
            communicationRequest.EscalationRejectionReason.ShouldBe(REJECTION_REASON);
        }

        [Fact]
        public void Should_UpdatePlaintAgencyCommunicationRequest()
        {
            var attachments = new List<CommunicationAttachmentModel>();
            attachments.Add(new CommunicationAttachmentModel() { FileNetReferenceId = "id", Name = "Name", AttachmentTypeId = (int)Enums.AttachmentType.PlainRequest });

            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.UpdatePlaintAgencyCommunicationRequest(CommunicationRequestId, Util.Encrypt(1), 1, Util.Encrypt(1), PlaintReason, attachments);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.PlaintAcceptanceStatusId.ShouldBe((int)Enums.AgencyPlaintStatus.New);
            communicationRequest.AgencyRequestTypeId.ShouldBe((int)Enums.AgencyCommunicationRequestType.Plaint);
            communicationRequest.StatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.RequestSent);
        }

        [Fact]
        public void Should_ApprovePlaintAgencyCommunicationRequest()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.UpdatePlaintAgencyCommunicationRequest((int)Enums.AgencyCommunicationRequestStatus.Approved, REJECTION_REASON);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.StatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.Approved);
            communicationRequest.RejectionReason.ShouldBe(REJECTION_REASON);
        }

        [Fact]
        public void Should_UpdateEscalationRequest()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.UpdateEscalationRequest((int)Enums.AgencyCommunicationRequestStatus.Rejected, REJECTION_REASON);
            communicationRequest.ShouldNotBeNull();
            communicationRequest.EscalationStatusId.ShouldBe((int)Enums.AgencyCommunicationRequestStatus.Rejected);
            communicationRequest.EscalationRejectionReason.ShouldBe(REJECTION_REASON);
        }



        [Fact]
        public void Should_DeleteExtendOfferValidityRequests()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.AddAgencyCommunicationRequestValidity(0, 20, "Extend Offers Reason", 10, "5:00 pm");

            communicationRequest.DeleteExtendOfferValidityRequests();
            communicationRequest.ShouldNotBeNull();
            communicationRequest.IsActive.ShouldNotBeNull();
            communicationRequest.IsActive.Value.ShouldBeFalse();
        }

        //[Fact]
        //public void Should_DeleteNegotiationRequests()
        //{
        //    AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
        //    communicationRequest.DeleteNegotiationRequests();
        //    communicationRequest.ShouldNotBeNull(); 
        //}

        [Fact]
        public void Should_SetSupplierExtendOfferDates()
        {
            AgencyCommunicationRequest communicationRequest = new AgencyCommunicationRequest();
            communicationRequest.SetSupplierExtendOfferDates(1);

        }
    }
}
