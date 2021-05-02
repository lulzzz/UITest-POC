using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BranchUserSearchCriteriaModel : SearchCriteria
    {
        public int UserProfileId { get; set; }

        public int BranchId { get; set; }
        public string RoleName { get; set; }
        public List<string> RoleNames { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string AgencyId { get; set; }
        public int CommitteeTypeId { get; set; }
        public bool isSemiGovAgency { get; set; }
        public bool isGovVendor { get; set; }
        public AgencyType AgencyType { get; set; }

    }
}
