using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BranchUserModel
    {
        public int BranchUserId { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "UserName")]
        public int UserId { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "UserName")]
        public string UserName { get; set; }
        public string FullName { get; set; }


        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string AgencyCode { get; set; }
        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        [Required(ErrorMessageResourceType = typeof(Resources.CommitteeResources.DisplayInputs), ErrorMessageResourceName = "SelectUserName")]
        public List<int?> UserIds { set; get; } = new List<int?>();
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "RoleName")]
        public int RoleId { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "RoleName")]
        public string RoleName { get; set; }
        public string RoleArName { get; set; }
        public List<UserLookupModel> Users { get; set; }
        public List<RoleModel> roles { get; set; }

        public string Email { get; set; }

        public decimal? EstimatedValueFrom { get; set; }
        public decimal? EstimatedValueTo { get; set; }

        public string RelatedAgencyCode { get; set; }

    }
}
