using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AgencyCommunicationRequest", Schema = "CommunicationRequest")]
    public class AgencyCommunicationRequest : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int AgencyRequestId { get; private set; }
        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { private set; get; }
        [ForeignKey(nameof(AgencyCommunicationRequestType))]
        public int AgencyRequestTypeId { get; private set; }
        public AgencyCommunicationRequestType AgencyRequestType { private set; get; }
        [StringLength(1000)]
        public string RejectionReason { get; protected set; }
        [StringLength(1000)]
        public string EscalationRejectionReason { get; protected set; }
        public string Details { get; protected set; }
        public bool IsReported { get; set; }
        public virtual List<Negotiation> Negotiations { get; set; }

        [ForeignKey(nameof(TenderPlaintRequestProcedure))]
        public int? TenderPlaintRequestProcedureId { get; private set; }
        public TenderPlaintRequestProcedure TenderPlaintRequestProcedure { private set; get; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; private set; }
        public AgencyCommunicationRequestStatus Status { private set; get; }
        [ForeignKey(nameof(EscalationStatus))]
        public int? EscalationStatusId { get; private set; }
        public AgencyCommunicationRequestStatus EscalationStatus { private set; get; }

        [ForeignKey(nameof(EscalationAcceptanceStatus))]
        public int? EscalationAcceptanceStatusId { get; private set; }
        public AgencyCommunicationPlaintStatus EscalationAcceptanceStatus { private set; get; }
        [ForeignKey(nameof(PlaintAcceptanceStatus))]
        public int? PlaintAcceptanceStatusId { get; private set; }
        public AgencyCommunicationPlaintStatus PlaintAcceptanceStatus { private set; get; }
        public string RequestedByRoleName { get; private set; }
        public List<PlaintRequest> PlaintRequests { get; private set; } = new List<PlaintRequest>();
        public int? SupplierExtendOfferDatesRequestId { get; private set; }
        [ForeignKey(nameof(SupplierExtendOfferDatesRequestId))]
        public SupplierExtendOfferDatesRequest SupplierExtendOfferDatesRequest { get; private set; }
        public PlaintRequestNotification PlaintNotification { get; private set; }
        public ExtendOffersValidity ExtendOffersValidity { get; private set; }
        public virtual List<Enquiry> Enquiries { get; private set; }
        public List<RejectionHistory> RejectionHistories { get; private set; } = new List<RejectionHistory>();
        #endregion

        #region Constructors====================================================
        public AgencyCommunicationRequest(int tenderId, int agencyRequestTypeId, int statusId, string requestedByRoleName)
        {
            TenderId = tenderId;
            AgencyRequestTypeId = agencyRequestTypeId;
            StatusId = statusId;
            RequestedByRoleName = requestedByRoleName;
            EntityCreated();
        }

        /// <summary>
        /// Add Or Update For Extend Offers Validity Requests
        /// </summary>
        public AgencyCommunicationRequest(int agencyRequestId, int tenderId, int agencyRequestTypeId, int statusId, string requestedByRoleName)
        {
            AgencyRequestId = agencyRequestId;
            IsActive = true;
            TenderId = tenderId;
            AgencyRequestTypeId = agencyRequestTypeId;
            StatusId = statusId;
            RequestedByRoleName = requestedByRoleName;
            if (AgencyRequestId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }
        public AgencyCommunicationRequest(int tenderId, int agencyRequestTypeId)
        {
            TenderId = tenderId;
            AgencyRequestTypeId = agencyRequestTypeId;
            EntityCreated();
        }

        public void AddTender(int tenderid, int tenderTypeId, string tenderName, string tenderNumber, string purpose, int? offersCheckingCommitteeId, int offerPresentationWayId, string agencyCode, int branchId)
        {
            Tender = new Tender(tenderTypeId, tenderName, tenderNumber, purpose, null, offersCheckingCommitteeId, null, offerPresentationWayId, null, agencyCode, branchId, null, null, "", null, null);
            EntityCreated();
        }

        public void AddAgencyCommunicationRequestValidity(int ExtendOffersValidityId, int offersDuration, string extendOffersReason, int replyReceivingDurationDays, string replyReceivingDurationTime)
        {
            ExtendOffersValidity = new ExtendOffersValidity(ExtendOffersValidityId, offersDuration, extendOffersReason, replyReceivingDurationDays, replyReceivingDurationTime);
            if (ExtendOffersValidityId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }

        public void SetExtendOffersValidityForUnitTest(ExtendOffersValidity extendOffersValidity)
        {
            ExtendOffersValidity = extendOffersValidity;
        }

        public AgencyCommunicationRequest(int CommunicationRequestId, string EncryptedTenderId, int PlaintRequestId, string EncryptedOfferId, string PlaintReason, string UserRoleName, List<CommunicationAttachmentModel> attachments)
        {
            TenderId = Util.Decrypt(EncryptedTenderId);
            PlaintAcceptanceStatusId = (int)Enums.AgencyPlaintStatus.New;
            AgencyRequestTypeId = (int)Enums.AgencyCommunicationRequestType.Plaint;
            StatusId = (int)Enums.AgencyCommunicationRequestStatus.RequestSent;
            RequestedByRoleName = UserRoleName;
            PlaintRequest plaint = PlaintRequests.FirstOrDefault(p => p.PlainRequestId == PlaintRequestId);
            if (plaint == null)
            {
                PlaintRequests.Add(new PlaintRequest(PlaintRequestId, Util.Decrypt(EncryptedOfferId), PlaintReason, attachments));
            }
            else
            {
                plaint.UpdatePlaintRequest(PlaintRequestId, Util.Decrypt(EncryptedOfferId), PlaintReason, attachments);
            }

            if (PlaintNotification == null)
            {
                PlaintNotification = new PlaintRequestNotification(0, false);
            }
            if (CommunicationRequestId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }
        public AgencyCommunicationRequest()
        {

        }
        #endregion

        #region Methods====================================================

        public void UpdateEscalatePlaint(int escalationAcceptanceStatusId, int escalationStatusId)
        {
            EscalationStatusId = escalationStatusId;
            EscalationAcceptanceStatusId = escalationAcceptanceStatusId;
            EntityUpdated();
        }


        public void UpdateSupplierOfferExtendDatesRequestStatus(int statusId, string rejectionReason = "")
        {
            StatusId = statusId;
            RejectionReason = rejectionReason;
            EntityUpdated();
        }

        public void UpdateAgencyCommunicationRequestStatus(int statusId, string rejectionReason = "")
        {
            StatusId = statusId;
            RejectionReason = rejectionReason;
            EntityUpdated();
        }
        public void UpdateAgencyCommunicationPlaintRequestStatus(int statusId, int plaintStatusId, int? tenderPlaintRequestProcedureId, string details = "", string rejectionReason = "")
        {
            StatusId = statusId;
            PlaintAcceptanceStatusId = plaintStatusId;
            TenderPlaintRequestProcedure = null;
            TenderPlaintRequestProcedureId = tenderPlaintRequestProcedureId;
            PlaintAcceptanceStatus = null;
            RejectionReason = rejectionReason;
            Details = details;

            if (!string.IsNullOrEmpty(RejectionReason))
                RejectionHistories.Add(new RejectionHistory(AgencyRequestId, (int)Enums.RequestRejectionType.Plaint, plaintStatusId, RejectionReason));

            EntityUpdated();
        }

        public void DeleteExtendOfferValidityRequests()
        {
            IsActive = false;
            ExtendOffersValidity.DeActive();
            EntityUpdated();
        }
        public void DeleteNegotiationRequests()
        {
            this.Negotiations.ForEach(x => x.DeActive());
            this.IsActive = false;
            EntityUpdated();
        }

        public void UpdateAgencyCommunicationEscalationRequestStatus(int escalationStatusId, int escalationAcceptanceStatusId, int? tenderPlaintRequestProcedureId, string details = "", string rejectionReason = "")
        {
            TenderPlaintRequestProcedureId = tenderPlaintRequestProcedureId;
            EscalationAcceptanceStatusId = escalationAcceptanceStatusId;
            EscalationStatusId = escalationStatusId;
            EscalationRejectionReason = rejectionReason;
            Details = details;
            EntityUpdated();
        }
        public AgencyCommunicationRequest UpdatePlaintAgencyCommunicationRequest(int CommunicationRequestId, string EncryptedTenderId, int PlaintRequestId, string EncryptedOfferId, string PlaintReason, List<CommunicationAttachmentModel> attachments)
        {
            TenderId = Util.Decrypt(EncryptedTenderId);
            PlaintAcceptanceStatusId = (int)Enums.AgencyPlaintStatus.New;
            AgencyRequestTypeId = (int)Enums.AgencyCommunicationRequestType.Plaint;
            StatusId = (int)Enums.AgencyCommunicationRequestStatus.RequestSent;
            PlaintRequest plaint = PlaintRequests.FirstOrDefault(p => p.PlainRequestId == PlaintRequestId);
            if (plaint == null)
            {
                PlaintRequests.Add(new PlaintRequest(PlaintRequestId, Util.Decrypt(EncryptedOfferId), PlaintReason, attachments));
            }
            else
            {
                plaint.UpdatePlaintRequest(PlaintRequestId, Util.Decrypt(EncryptedOfferId), PlaintReason, attachments);
            }

            if (CommunicationRequestId == 0)
                EntityCreated();
            else
                EntityUpdated();
            return this;
        }

        public AgencyCommunicationRequest UpdatePlaintAgencyCommunicationRequest(int statusId, string rejectReason)
        {
            if (statusId == (int)Enums.AgencyCommunicationRequestStatus.Approved)
            {
                if (TenderPlaintRequestProcedureId == (int)Enums.TenderPlaintRequestProcedure.ReOpenTenderChecking)
                {
                    Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
                }
                else if (TenderPlaintRequestProcedureId == (int)Enums.TenderPlaintRequestProcedure.ReOpenTenderAwarding)
                {
                    Tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwarding);
                }
            }
            if (PlaintNotification == null)
            {
                PlaintNotification = new PlaintRequestNotification(0, false);
            }
            else
            {
                this.PlaintNotification.UpdateApprovalDate();
            }
            StatusId = statusId;
            RejectionReason = rejectReason;
            if (!string.IsNullOrEmpty(RejectionReason))
                RejectionHistories.Add(new RejectionHistory(AgencyRequestId, (int)Enums.RequestRejectionType.Plaint, statusId, RejectionReason));
            EntityUpdated();
            return this;
        }

        public AgencyCommunicationRequest UpdateEscalationRequest(int statusId, string rejectReason)
        {
            EscalationStatusId = statusId;
            EscalationRejectionReason = rejectReason;
            if (!string.IsNullOrEmpty(RejectionReason))
                RejectionHistories.Add(new RejectionHistory(AgencyRequestId, (int)Enums.RequestRejectionType.Escalation, StatusId, RejectionReason));
            EntityUpdated();
            return this;
        }

        public void AddEscalationAcceptanceStatusForUnitTest()
        {
            EscalationAcceptanceStatus = new AgencyCommunicationPlaintStatus();
        }
        #endregion

        #region SupplierExtendDatesRequest

        public void SetSupplierExtendOfferDates(int supplierExtendOfferDatesId)
        {
            SupplierExtendOfferDatesRequestId = supplierExtendOfferDatesId;
            EntityUpdated();
        }


        public void SetSupplierExtendOfferDatesForUnitTest(SupplierExtendOfferDatesRequest supplierExtendOfferDates)
        {
            SupplierExtendOfferDatesRequest = supplierExtendOfferDates;
        }

        #endregion

        public void AddTender(Tender tender)
        {
            this.Tender = tender;
            EntityUpdated();
        }
    }
}
