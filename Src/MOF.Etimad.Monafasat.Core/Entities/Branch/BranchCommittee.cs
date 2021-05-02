using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BranchCommittee", Schema = "Branch")]
    public class BranchCommittee : AuditableEntity
    {
        [Key]
        public int BranchCommitteeId { get; private set; }

        [ForeignKey(nameof(Branch))]
        public int BranchId { get; private set; }

        [ForeignKey(nameof(Committee))]
        public int CommitteeId { get; private set; }

        public Branch Branch { get; private set; }
        public Committee Committee { get; private set; }

        public BranchCommittee()
        {

        }
        public BranchCommittee(int branchId, int committeeId)
        {
            BranchId = branchId;
            CommitteeId = committeeId;
            EntityCreated();
        }
        #region Methods

        public void Delete()
        {
            EntityDeleted();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        #endregion
    }
}
