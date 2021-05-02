using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UserLookupModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public List<RoleModel> Roles { get; set; }

        public UserLookupModel()
        {
        }
        public UserLookupModel(int userid, string name, string userName, List<RoleModel> roles)
        {
            UserId = userid;
            Name = name;
            UserName = userName;
            Roles = roles;
        }
    }
}