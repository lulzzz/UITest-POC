using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UserCommitteeBranchesModel : SearchCriteria
    {
        public int? BranchUserId { get; set; }
        public int? BranchId { get; set; }
        public int? CommitteeId { get; set; }
        public int? CommitteeUserId { get; set; }
        public string BranchName { get; set; }
        public string CommitteeName { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameAr { get; set; }
        public int? AgencyCode { get; set; }

        public decimal? EstimatedValueFrom { get; set; }
        public decimal? EstimatedValueTo { get; set; }
    }
}
