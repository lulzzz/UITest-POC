using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("UserAgencyRole", Schema = "IDM")]
    public class UserAgencyRole : AuditableEntity
    {
        [Key]
        public int UserAgencyRoleID { get; private set; }
        public string AgencyCode { get; private set; }
        [ForeignKey(nameof(AgencyCode))]
        public GovAgency Agency { get; private set; }
        public int? BranchId { get; private set; }
        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; private set; }
        public int UserProfileId { get; private set; }
        [ForeignKey(nameof(UserProfileId))]
        public UserProfile UserProfile { get; private set; }
        public string RoleName { get; private set; }

        public UserAgencyRole()
        { }
    }
}
