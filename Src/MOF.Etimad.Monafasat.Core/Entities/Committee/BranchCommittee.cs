using MOF.Etimad.Monafasat.SharedKernel;
  using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BranchCommittee", Schema = "Committee")]
    public class BranchCommittee : AuditableEntity
    {
        #region Constructors


        public BranchCommittee() { }
        public BranchCommittee(int branchCommitteeId, int committeeId, int branchId, bool? isActive = true)
        {
            BranchCommitteeId = branchCommitteeId;
            CommitteeId = committeeId;
            BranchId = branchId;
            IsActive = isActive;
        }

        #endregion

        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchCommitteeId { get; private set; }
        //[ForeignKey("BranchId")]
        public int BranchId { get; private set; }
        //public Branch Branch { get; private set; }


        public int CommitteeId { get; private set; }
        [ForeignKey("CommitteeId")]
        public Committee Committee { get; private set; }

        #endregion

        #region Methods
            
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

        #endregion        
    }
}
