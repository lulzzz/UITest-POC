
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BranchUser", Schema = "Branch")]
    public class BranchUser : AuditableEntity
    {

        #region Constructors
        public BranchUser() { }
        public BranchUser(int branchId, int userProfileId, int userRoleId, string relatedAgencyCode, decimal? estimatedValueFrom = null, decimal? estimatedValueTo = null)
        {
            BranchId = branchId;
            UserProfileId = userProfileId;
            UserRoleId = userRoleId;
            EstimatedValueFrom = estimatedValueFrom;
            EstimatedValueTo = estimatedValueTo;
            RelatedAgencyCode = relatedAgencyCode;
            EntityCreated();
        }
        #endregion

        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchUserId { get; private set; }
        public decimal? EstimatedValueFrom { get; set; }
        public decimal? EstimatedValueTo { get; set; }
        public string RelatedAgencyCode { get; private set; }
        [ForeignKey(nameof(UserProfile))]
        public int UserProfileId { get; private set; }

        [ForeignKey(nameof(Branch))]
        public int BranchId { get; private set; }

        [ForeignKey(nameof(UserRole))]
        public int UserRoleId { get; private set; }

        public Branch Branch { get; set; }
        public UserProfile UserProfile { get; private set; }
        public UserRole UserRole { get; set; }

        #endregion
        #region Methods

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void CreateUserProfile()
        {
            UserProfile = new UserProfile();
        }

        public void CreateBranchForTest(Branch branch)
        {
            Branch = branch;
        }

        public void CreateUserRoleForTest()
        {
            UserRole = new UserRole ();
        }
        #endregion
    }
}
