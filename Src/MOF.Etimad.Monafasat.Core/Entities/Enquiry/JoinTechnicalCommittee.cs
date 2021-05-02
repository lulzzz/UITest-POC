using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("JoinTechnicalCommittee", Schema = "Enquiry")]
    public class JoinTechnicalCommittee : AuditableEntity
    {
        [Key]
        public int JoinTechnicalCommitteeId { get; private set; }

        public int EnquiryId { get; private set; }
        [ForeignKey("EnquiryId")]
        public Enquiry Enquiry { get; private set; }

        public int CommitteeId { get; private set; }
        [ForeignKey("CommitteeId")]
        public Committee Committee { get; private set; }


        public JoinTechnicalCommittee()
        {

        }

        public JoinTechnicalCommittee(int enquiryId, int committeeId)
        {
            EnquiryId = enquiryId;
            CommitteeId = committeeId;
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
    }
}
