using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.User
{
    public class LoggedUserModel
    {
        public Dictionary<int, string> BranchRoles { get; set; }
        public Dictionary<int, string> CommitteeRoles { get; set; }
        public string DefaultRoleDetails { get; set; }
        public AssignedRoleLevelTypeModel DefaultRole { get; set; }
        //public Dictionary<string, string> roles { get; set; }
        public List<AssignedRoleLevelTypeModel> AssignedRoleLevelTypeModels { get; set; } = new List<AssignedRoleLevelTypeModel>();
        public bool IsSubscripe { get; set; }
        public string SubscirpeUrl { get; set; }
    }
}
