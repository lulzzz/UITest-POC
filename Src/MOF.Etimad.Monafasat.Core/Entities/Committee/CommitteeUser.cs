using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("CommitteeUser", Schema = "Committee")]
    public class CommitteeUser : AuditableEntity
    {

        public CommitteeUser() { }
        public CommitteeUser(int committeeId, int userProfileId, int userRoleId)
        {
            CommitteeId = committeeId;
            UserProfileId = userProfileId;
            UserRoleId = userRoleId;
            EntityCreated();
        }

        public CommitteeUser(int committeeId, int userProfileId, int userRoleId, string relatedAgencyCode)
        {
            CommitteeId = committeeId;
            UserProfileId = userProfileId;
            UserRoleId = userRoleId;
            RelatedAgencyCode = relatedAgencyCode;
            EntityCreated();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommitteeUserId { get; private set; }
        public string RelatedAgencyCode { get; private set; }
        [NotMapped]
        [StringLength(500)]
        public string RoleName { get; set; }
        [ForeignKey(nameof(UserRole))]
        public int UserRoleId { get; private set; }
        [ForeignKey(nameof(UserProfile))]
        public int UserProfileId { get; private set; }
        [ForeignKey(nameof(Committee))]
        public int CommitteeId { get; private set; }
        public UserRole UserRole { get; set; }
        public UserProfile UserProfile { get; private set; }
        public Committee Committee { get; set; }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void Test_AddUserProfile(UserProfile userProfile)
        {
            this.UserProfile = userProfile;
        }

        public void Test_AddCommittee(Committee committee)
        {
            this.Committee = committee;
        }
        public void CreateUserRoleForTest()
        {
            UserRole = new UserRole();
        }

    }
}
