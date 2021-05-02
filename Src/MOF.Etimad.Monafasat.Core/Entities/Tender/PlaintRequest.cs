using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("PlaintRequest", Schema = "CommunicationRequest")]
    public class PlaintRequest : AuditableEntity
    {
        #region Constructors
        public PlaintRequest()
        {

        }
        public PlaintRequest(int plaintRequestId, int offerId, String plaintReason, List<CommunicationAttachmentModel> attachments, bool isEscalation = false)
        {
            PlaintReason = plaintReason;
            OfferId = offerId;
            UpdateAttachments(attachments);
            if (PlainRequestId != 0)
            {
                EntityUpdated();
            }
            else
            {
                EntityCreated();
            }
        }
        #endregion
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlainRequestId { get; private set; }
        [StringLength(1000)]
        public String PlaintReason { get; private set; }
        [StringLength(1000)]
        public String Notes { get; private set; }
        public int AgencyCommunicationRequestId { get; private set; }
        [ForeignKey(nameof(AgencyCommunicationRequestId))]
        public AgencyCommunicationRequest AgencyCommunicationRequest { get; private set; }
        public bool IsEscalation { get; private set; }
        public int OfferId { get; private set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }
        public EscalationRequest EscalationRequest { get; set; }
        public List<PlaintRequestAttachment> Attachments { get; set; } = new List<PlaintRequestAttachment>();
        #endregion
        #region Methods
        public PlaintRequest UpdatePlaintRequest(int plaintRequestId, int offerId, String plaintReason, List<CommunicationAttachmentModel> attachments, bool isEscalation = false)
        {
            PlaintReason = plaintReason;
            OfferId = offerId;
            UpdateAttachments(attachments);
            if (PlainRequestId != 0)
            {
                EntityUpdated();
            }
            else
            {
                EntityCreated();
            }
            return this;
        }
        public PlaintRequest UpdateNotes(string notes)
        {
            if (String.IsNullOrWhiteSpace(notes))
                throw new BusinessRuleException(Resources.CommunicationRequest.ErrorMessages.EnterNotes);

            Notes = notes;
            if (PlainRequestId != 0)
            {
                EntityUpdated();
            }
            else
            {
                EntityCreated();
            }
            return this;
        }
        public PlaintRequest EscalatePlaintRequest(string attachmentId, string attachmentName)
        {
            IsEscalation = true;
            EscalationRequest = new EscalationRequest(0, PlainRequestId, attachmentId, attachmentName);

            AgencyCommunicationRequest.UpdateEscalatePlaint((int)Enums.AgencyPlaintStatus.New, (int)Enums.AgencyCommunicationRequestStatus.RequestSent);
            List<CommunicationAttachmentModel> lst = new List<CommunicationAttachmentModel>();
            lst.Add(new CommunicationAttachmentModel() { FileNetReferenceId = attachmentId, Name = attachmentName, AttachmentTypeId = (int)Enums.AttachmentType.Escalation });
            UpdateAttachments(lst, (int)Enums.AttachmentType.Escalation);
            EntityUpdated();
            return this;
        }

        public void AcceptRevision()
        {
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public PlaintRequest UpdateAttachments(List<CommunicationAttachmentModel> attachments, int attachmentType = (int)Enums.AttachmentType.PlainRequest)
        {

            foreach (var item in Attachments.Where(a => a.AttachmentTypeId == attachmentType).ToList())
            {
                item.Delete();
            }
            foreach (var item in attachments)
            {
                Attachments.Add(new PlaintRequestAttachment(/*PlainRequestId,*/ item.Name, item.FileNetReferenceId, item.AttachmentTypeId));
            }

            EntityUpdated();
            return this;
        }
        #endregion
    }
}