using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Committee", Schema = "Committee")]
    public partial class Committee : AuditableEntity
    {
        [Key]
        public int CommitteeId { get; private set; }
        [StringLength(1024)]
        public string CommitteeName { get; private set; }
        [StringLength(1024)]
        public string Address { get; private set; }
        [StringLength(100)]
        public string Phone { get; private set; }
        [StringLength(100)]
        public string Fax { get; private set; }
        [StringLength(1024)]
        public string Email { get; private set; }
        [StringLength(1024)]
        public string PostalCode { get; private set; }
        [StringLength(1024)]
        public string ZipCode { get; private set; }
        [ForeignKey(nameof(Agency))]
        public string AgencyCode { get; private set; }
        [ForeignKey(nameof(CommitteeType))]
        public int CommitteeTypeId { get; private set; }
        public GovAgency Agency { get; private set; }
        public CommitteeType CommitteeType { get; private set; }
        public List<CommitteeUser> CommitteeUsers { get; private set; }
        public List<JoinTechnicalCommittee> JoinTechnicalCommittees { get; private set; }
        public virtual List<BranchCommittee> BranchCommittees { get; private set; }
    }
}
