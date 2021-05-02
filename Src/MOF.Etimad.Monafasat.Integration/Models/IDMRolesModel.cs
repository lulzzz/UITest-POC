namespace MOF.Etimad.Monafasat.Integration
{
    public class IDMRolesModel
    {
        public int Id { get; set; }
        public int Mask { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public int? RoleGroupID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
