using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CommitteeUserModel
    {
        public int CommitteeUserId { get; set; }
        public int CommitteeTypeId { get; set; }
        public string CommitteeTypeIdString { get; set; }

        public int UserId { get; set; }
        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UserName")]

        public string UserName { get; set; }
        public int CommitteeId { get; set; }
        public string CommitteeName { get; set; }
        public string CommitteeAddress { get; set; }
        public string AgencyCode { get; set; }

        public CommitteeTenderModel CommitteeModel { get; set; }
        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        [Required(ErrorMessageResourceType = typeof(Resources.CommitteeResources.DisplayInputs), ErrorMessageResourceName = "SelectUserName")]
        public List<int?> UserIds { set; get; } = new List<int?>();

        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "SelectRole")]
        [Required(ErrorMessageResourceType = typeof(Resources.CommitteeResources.ErrorMessages), ErrorMessageResourceName = "RoleNameRequired")]
        public string RoleName { get; set; }
        public string RoleArName { get; set; }

        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        public List<UserLookupModel> Users { get; set; }

        public string RelatedAgencyCode { get; set; }


    }
}