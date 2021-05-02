using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Enquiry", Schema = "Enquiry")]
    public class Enquiry : AuditableEntity
    {
        [Key]
        public int EnquiryId { get; private set; }

        [StringLength(15000)]

        public string EnquiryName { get; private set; }
        [StringLength(20)]
        public string CommericalRegisterNo { get; private set; }
        [ForeignKey(nameof(CommericalRegisterNo))]
        public Supplier Supplier { get; set; }

        public int TenderId { get; private set; }
        [ForeignKey(nameof(TenderId))]
        public Tender Tender { get; private set; }

        public List<EnquiryReply> EnquiryReplies { get; private set; } = new List<EnquiryReply>();
        public List<JoinTechnicalCommittee> JoinTechnicalCommittees { get; private set; } = new List<JoinTechnicalCommittee>();
        public int AgencyCommunicationRequestId { get; private set; }
        [ForeignKey(nameof(AgencyCommunicationRequestId))]
        public AgencyCommunicationRequest AgencyCommunicationRequest { get; private set; }
        public Enquiry()
        {

        }

        public Enquiry(string enquiryName, int tenderId, string commericalRegisterNo)
        {
            EnquiryName = enquiryName;
            TenderId = tenderId;
            CommericalRegisterNo = commericalRegisterNo;

            EntityCreated();
        }

        public void Delete()
        {

            EntityDeleted();
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

        public void JoinTechnicalCommitee(int enquiryId, int joinedCommitteeId, int technicalCommitteeId, string comment)
        {
            JoinTechnicalCommittees.Add(new JoinTechnicalCommittee(enquiryId, joinedCommitteeId));
            EnquiryReplies.Add(new EnquiryReply(comment, enquiryId, (int)Enums.EnquiryReplyStatus.Pending,
                technicalCommitteeId, true));
            EntityUpdated();

        }
        public void RemoveJoinedCommittee(List<JoinTechnicalCommittee> joinedCommittees, List<EnquiryReply> enquiryReplies,
            int joinTechnicalCommitteeId, int commentId)
        {
            foreach (var committee in joinedCommittees)
            {
                if (committee.JoinTechnicalCommitteeId == joinTechnicalCommitteeId)
                    committee.DeActive();
            }
            foreach (var reply in enquiryReplies)
            {
                if (reply.EnquiryReplyId == commentId)
                    reply.DeActive();
            }
            EntityUpdated();
        }
        public void SetCommunicationRequest(AgencyCommunicationRequest request)
        {
            AgencyCommunicationRequest = request;
        }

        public void SetJoinTechnicalCommittees(List<JoinTechnicalCommittee> request)
        {
            JoinTechnicalCommittees = request;
        }

        public void SetEnquiryReplies(List<EnquiryReply> request)
        {
            EnquiryReplies = request;
        }
        public void SetTender(Tender tender)
        {
            Tender = tender;
        }
    }
}
