namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UserRolesModel
    {
        public string RoleValue { get; set; }
        public string RoleName { get; set; }
        public UserRolesModel()
        {

        }
        public UserRolesModel(string roleValue, string roleName)
        {
            RoleValue = roleValue;
            RoleName = roleName;
        }
    }
}
