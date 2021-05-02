namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BranchUserAssignModel
    {
        public int? BranchUserId { get; set; }
        public string UserIdString { get; set; }
        public string NationalIdString { get; set; }
        public int? UserId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameAr { get; set; }

        public decimal? EstimatedValueFrom { get; set; }
        public decimal? EstimatedValueTo { get; set; }

        public string RelatedAgencyCode { get; set; }
    }
}
