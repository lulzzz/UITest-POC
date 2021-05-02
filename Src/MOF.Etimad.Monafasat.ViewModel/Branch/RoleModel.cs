using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "RoleName")]

        public string RoleName { get; set; }
        public string RoleNameAr { get; set; }
    }
}
