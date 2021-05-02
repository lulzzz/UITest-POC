using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Branch", Schema = "Branch")]
    public class Branch : AuditableEntity
    {
        public Branch() { }
        public Branch(string agencyCode, string branchName, List<BranchAddress> branchAddresses) //,List<BranchAddress> branchAddresses)
        {
            BranchName = branchName;
            AgencyCode = agencyCode;
            BranchAddresses = branchAddresses;
            EntityCreated();
        }

        public Branch(int branchId, string agencyCode, string branchName, List<BranchAddress> branchAddresses)
        {
            BranchId = branchId;
            BranchName = branchName;
            AgencyCode = agencyCode;
            BranchAddresses = branchAddresses;
            EntityCreated();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchId { get; private set; }
        [Required]
        [StringLength(1024)]
        public string BranchName { get; private set; }

        [ForeignKey(nameof(Agency))]
        public string AgencyCode { get; private set; }

        public GovAgency Agency { get; private set; }
        public virtual List<BranchCommittee> BranchCommittees { get; private set; }
        public virtual IList<BranchUser> BranchUsers { get; private set; }
        public List<BranchAddress> BranchAddresses { get; private set; }

        public Branch AddAddresses(List<BranchAddress> addresses)
        {
            foreach (var item in BranchAddresses)
            {
                item.DeActive();
            }
            foreach (var address in addresses)
            {
                BranchAddresses.Add(address);
            }
            EntityUpdated();
            return this;
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void Update(string branchName, List<BranchAddress> branchAddresses)
        {
            BranchName = branchName;
            AddAddresses(branchAddresses);
            EntityUpdated();
        }

        public void AddCommittees(List<BranchCommittee> branchCommittees)
        {
            this.BranchCommittees = this.BranchCommittees ?? new List<BranchCommittee>();
            foreach (var bc in branchCommittees)
            {
                this.BranchCommittees.Add(bc);

            }
            EntityUpdated();
        }
        public void UpdateCommittees(List<BranchCommittee> committieeIds)
        {
            this.BranchCommittees = this.BranchCommittees ?? new List<BranchCommittee>();
            foreach (var bc in this.BranchCommittees)
            {
                bc.Delete();
            }
            AddCommittees(committieeIds);
        }
    }
}
