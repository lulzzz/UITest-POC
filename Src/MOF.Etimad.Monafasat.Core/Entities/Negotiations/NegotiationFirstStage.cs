using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities.Negotiations
{
    public class NegotiationFirstStage : Negotiation
    {

        public string DiscountLetterRefID { get; private set; }
        public string ProjectNumber { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public bool? IsNewNegotiation { get; set; }
        public decimal? ExtraDiscountValue { get; set; }
        public List<NegotiationFirstStageSupplier> NegotiationFirstStageSuppliers { get; set; }




        #region[Methods]

        public NegotiationFirstStage()
        {

        }
        public NegotiationFirstStage(int supplierReplyPeriod, string DiscountLetterRefID, decimal DiscountAmount, int agencyRequestId, int? ReasonId, List<NegotiationAttachmentViewModel> negotiationAttachments, int StatusId, string _ProjectNumber)
        {
            AgencyRequestId = agencyRequestId;
            this.NegotiationReasonId = ReasonId;
            SupplierReplyPeriodHours = supplierReplyPeriod;
            this.DiscountLetterRefID = DiscountLetterRefID;
            this.StatusId = StatusId;
            this.ProjectNumber = _ProjectNumber;
            this.DiscountAmount = DiscountAmount;
            this.IsNewNegotiation = true;
            UpdateAttachments(negotiationAttachments);
            EntityCreated();
        }



        /// <summary>
        /// Add new 
        /// </summary>
        /// <param name="supplierReplyPeriod"></param>
        /// <param name="DiscountLetterRefID"></param>
        /// <param name="DiscountAmount"></param>
        /// <param name="ReasonId"></param>
        public void UpdateNegotiationFirstStage(int supplierReplyPeriod, string DiscountLetterRefID, decimal DiscountAmount, int ReasonId, int StatusId, List<NegotiationAttachmentViewModel> negotiationAttachments, string _ProjectNumber)
        {
            SupplierReplyPeriodHours = supplierReplyPeriod;
            this.StatusId = StatusId;
            this.ProjectNumber = _ProjectNumber;
            this.NegotiationReasonId = ReasonId;
            this.DiscountLetterRefID = DiscountLetterRefID;
            this.DiscountAmount = DiscountAmount;
            UpdateAttachments(negotiationAttachments);
            EntityUpdated();
        }

        public void UpdateNegotiationFirstStageStatus(int StatusId, string RejectionReason)
        {
            this.StatusId = StatusId;
            this.RejectionReason = RejectionReason;
            EntityUpdated();
        }

        public void AgreeWithExtraDiscount(decimal? extraDiscount)
        {
            if (extraDiscount >= this.DiscountAmount || extraDiscount < 1)
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.NegotiationFirstStageExtraDiscountValueValidation);

            this.StatusId = (int)Enums.enNegotiationStatus.SupplierAgreedWithExtraDiscount;
            this.ExtraDiscountValue = extraDiscount;
            EntityUpdated();
        }

        public void StartNegotiation(int OfferIdId)
        {
            if (this.NegotiationFirstStageSuppliers.FirstOrDefault(d => d.OfferId == OfferIdId) != null)
            {
                this.NegotiationFirstStageSuppliers.FirstOrDefault(d => d.OfferId == OfferIdId).UpdateNegotiationFirstStageSupplier((int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply, DateTime.Now);
                EntityUpdated();
            }

        }

        public void AddSupplier(NegotiationFirstStageSupplier negotiationFirstStageSupplier)
        {
            this.NegotiationFirstStageSuppliers?.Add(negotiationFirstStageSupplier);
            EntityUpdated();
        }

        public bool UpdateSupplierStatus(int id, int statusId, DateTime? startDate)
        {
            var next = this.NegotiationFirstStageSuppliers.FirstOrDefault(w => w.Id == id);
            if (next != null)
            {
                next.UpdateNegotiationFirstStageSupplier(statusId, startDate);
                return true;
            }
            return false;
        }


        public NegotiationFirstStage UpdateAttachments(List<NegotiationAttachmentViewModel> attachments)
        {
            foreach (var item in Attachments ?? new List<NegotiationAttachment>())
            {
                item.Delete();
            }

            foreach (var attachment in attachments)
            {
                Attachments.Add(new NegotiationAttachment(attachment.Name, attachment.FileNetReferenceId, attachment.AttachmentTypeId));
            }
            EntityUpdated();
            return this;
        }
        public NegotiationFirstStage SetNegotiationCommunicationRequest(AgencyCommunicationRequest agencyCommunicationRequest)
        {
            AgencyCommunicationRequest = agencyCommunicationRequest;
            EntityUpdated();
            return this;
        }
        #endregion
    }


}
