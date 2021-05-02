using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("EnquiryReply", Schema = "Enquiry")]
    public class EnquiryReply : AuditableEntity
    {
        [Key]
        public int EnquiryReplyId { get; private set; }

        [StringLength(15000)]
        public string EnquiryReplyMessage { get; private set; }

        public int EnquiryId { get; private set; }
        [ForeignKey("EnquiryId")]
        public Enquiry Enquiry { get; private set; }

        public int EnquiryReplyStatusId { get; private set; }
        [ForeignKey("EnquiryReplyStatusId")]
        public EnquiryReplyStatus EnquiryReplyStatus { get; private set; }

        public int? CommitteeId { get; private set; }
        [ForeignKey("CommitteeId")]
        public Committee Committee { get; private set; }

        public bool? IsComment { get; set; }

        public EnquiryReply()
        {

        }

        public EnquiryReply(string enquiryReplyMessage, int enquiryId, int enquiryReplyStatusId, int committeeId,
            bool isComment)
        {
            EnquiryId = enquiryId;
            CommitteeId = committeeId;
            EnquiryReplyMessage = enquiryReplyMessage;
            EnquiryReplyStatusId = enquiryReplyStatusId;
            IsComment = isComment;
            EntityCreated();
        }

        public EnquiryReply Update(int enquiryReplyId, int enquiryReplyStatusId, string enquiryReplyMessage,
            int committeeId, bool isComment)
        {
            EnquiryReplyId = enquiryReplyId;
            CommitteeId = committeeId;
            EnquiryReplyStatusId = enquiryReplyStatusId;
            EnquiryReplyMessage = enquiryReplyMessage;
            IsComment = isComment;
            EntityUpdated();
            return this;
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
        public void ApproveEnquiryReply()
        {
            EnquiryReplyStatusId = (int)Enums.EnquiryReplyStatus.Approved;
            EntityUpdated();
        }

        public void SetEnquiry(Enquiry enquiry)
        {
            Enquiry = enquiry;
        }
    }
}
