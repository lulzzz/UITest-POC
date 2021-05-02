using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Negotiation", Schema = "CommunicationRequest")]
    public abstract class Negotiation : AuditableEntity
    {

        [Key]
        public int NegotiationId { get; set; }

        public int SupplierReplyPeriodHours { get; set; }
        
        [ForeignKey(nameof(AgencyCommunicationRequest))]
        public int AgencyRequestId { get; set; }
        public virtual AgencyCommunicationRequest AgencyCommunicationRequest { get; set; }

        public int? NegotiationReasonId { get; set; }
        [ForeignKey(nameof(NegotiationReasonId))]
        public NegotiationReason NegotiationReason { get; set; }
        /// <summary>
        /// Enums.enNegotiationType 
        /// </summary>
        [ForeignKey("NegotiationType")]
        public int NegotiationTypeId { get; set; }
        public NegotiationType NegotiationType { get; set; }

        public string RejectionReason { get; set; }

        [ForeignKey(nameof(NegotiationStatus))]
        public int StatusId { get; set; }
        public NegotiationStatus NegotiationStatus { get; set; }
        public List<NegotiationAttachment> Attachments { get; set; } = new List<NegotiationAttachment>();



        #region Methods====================================================
        public void UpdateNegotiationStatus(int statusId, string rejectionReason = "")
        {
            StatusId = statusId;
            RejectionReason = rejectionReason;
            EntityUpdated();
        }


        public void UpdateReplyPeriod(int _SupplierReplyPeriodHours)
        {
            SupplierReplyPeriodHours = _SupplierReplyPeriodHours;
            EntityUpdated();
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        #endregion

    }
}
