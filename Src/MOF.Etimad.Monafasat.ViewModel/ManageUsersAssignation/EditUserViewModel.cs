using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class EditUserViewModel
    {
        //public CommitteeUserAssignModel CommitteeUserAssignModel { get; set; }
        //public BranchUserAssignModel BranchUserAssignModel { get; set; }

        public int UserId { get; set; }
        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UserName")]
        public string NationalId { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string UserIdString { get; set; }
        public string AgencyCode { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "RoleName")]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleArName { get; set; }
        public int CommitteeId { get; set; }
        public string CommitteeName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public List<int?> RoleIds { set; get; } = new List<int?>();
        public List<int?> BranchIds { set; get; } = new List<int?>();
        public List<int?> CommitteeIds { set; get; } = new List<int?>();

        public List<RoleModel> Roles { get; set; }
        public List<CommitteeModel> CommitteeNotAssignToUser { get; set; }
        public List<BranchModel> BranchesNotAssignToUser { get; set; }
        public List<AgencyModel> agencies { get; set; }
        public List<AgencyModel> vendors { get; set; }
        public List<AgencyModel> CRs { get; set; }
        public string AgencyNames { get; set; }
        public List<RoleModel> BranchRoles { get; set; }
        public List<RoleModel> CommitteeOpenOfferRoles { get; set; }
        public List<RoleModel> CommitteeAuditOfferRoles { get; set; }
        public List<RoleModel> CommitteeTechnicalRoles { get; set; }
        public List<RoleModel> CommitteeBlockRoles { get; set; }
        public List<RoleModel> AllCommitteeRoles { set; get; } = new List<RoleModel>();
        public List<RoleModel> CommitteeOpenAndCheckRoles { get; set; }
        public List<RoleModel> CommitteePurchaseRoles { get; set; }

        public List<RoleModel> CommitteePreQualificationRoles { get; set; }
    }
}
