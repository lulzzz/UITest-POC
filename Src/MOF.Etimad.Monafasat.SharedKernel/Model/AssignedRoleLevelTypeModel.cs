namespace MOF.Etimad.Monafasat.SharedKernel
{
    public class AssignedRoleLevelTypeModel
    {
        public Enums.AssignedRoleLevelType AssignedRoleLevel { get; set; }
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string DisplayedRoleName { get; set; }

        public string GetDefaultRole
        {
            get { return (int)AssignedRoleLevel + "," + Id + "," + RoleName; }
        }
    }
}
