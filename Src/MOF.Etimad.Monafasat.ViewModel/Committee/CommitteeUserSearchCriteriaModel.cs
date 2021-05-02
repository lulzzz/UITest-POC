using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CommitteeUserSearchCriteriaModel : SearchCriteria
    {
        public int CommitteeId { get; set; }
        public string CommitteeName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> RoleNames { get; set; } = new List<string>();
        public string AgencyId { get; set; }

        public string UserName { get; set; }
        public string EMail { get; set; }
        public int CommitteeTypeId { get; set; }
        public bool isSemiGovAgency { get; set; }
        public bool isGovVendor { get; set; }
        public AgencyType AgencyType { get; set; }
    }
}
