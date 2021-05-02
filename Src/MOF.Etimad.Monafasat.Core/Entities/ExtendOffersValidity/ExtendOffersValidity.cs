using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("ExtendOffersValidity", Schema = "CommunicationRequest")]
    public class ExtendOffersValidity : AuditableEntity
    {
        [Key]
        public int ExtendOffersValidityId { get; private set; }


        [ForeignKey(nameof(AgencyCommunicationRequest))]
        public int AgencyCommunicationRequestId { get; private set; }
        public AgencyCommunicationRequest AgencyCommunicationRequest { get; private set; }
        public int OffersDuration { get; private set; }
        public DateTime NewOffersExpiryDate { get; private set; }
        public string ExtendOffersReason { get; private set; }
        public int ReplyReceivingDurationDays { get; private set; }
        public string ReplyReceivingDurationTime { get; private set; }
        public List<ExtendOffersValiditySupplier> ExtendOffersValiditySuppliers { get; private set; } = new List<ExtendOffersValiditySupplier>();

        public ExtendOffersValidity()
        {

        }
        public ExtendOffersValidity(int extendOffersValidityId, int agencyRequestId, int offersDuration, string extendOffersReason, int replyReceivingDurationDays,
            string replyReceivingDurationTime)
        {
            var hours = replyReceivingDurationTime.Split(":");
            ExtendOffersValidityId = extendOffersValidityId;
            IsActive = true;
            AgencyCommunicationRequestId = agencyRequestId;
            OffersDuration = offersDuration;
            NewOffersExpiryDate = DateTime.Now.Date.Add(new TimeSpan(int.Parse(hours[0]) + 12, 0, 0)).AddDays(offersDuration);
            ExtendOffersReason = extendOffersReason;
            ReplyReceivingDurationDays = replyReceivingDurationDays;
            ReplyReceivingDurationTime = replyReceivingDurationTime;
            if (ExtendOffersValidityId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }
        public ExtendOffersValidity(int extendOffersValidityId, int offersDuration, string extendOffersReason, int replyReceivingDurationDays, string replyReceivingDurationTime)
        {
            var hours = replyReceivingDurationTime.Split(":");
            ExtendOffersValidityId = extendOffersValidityId;
            IsActive = true;
            OffersDuration = offersDuration;
            NewOffersExpiryDate = DateTime.Now.Date.Add(new TimeSpan(int.Parse(hours[0]) + 12, 0, 0)).AddDays(offersDuration);
            ExtendOffersReason = extendOffersReason;
            ReplyReceivingDurationDays = replyReceivingDurationDays;
            ReplyReceivingDurationTime = replyReceivingDurationTime;
            if (ExtendOffersValidityId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }

        public void AddAgencyCommunicationRequest(int tenderId, int agencyRequestId)
        {
            AgencyCommunicationRequest = new AgencyCommunicationRequest(tenderId, agencyRequestId);
            EntityCreated();
        }

        public void Delete()
        {
            IsActive = false;
            EntityDeleted();
        }
        public void Update()
        {
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
    }
}
