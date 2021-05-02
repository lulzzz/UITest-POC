using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BranchCommitteeModel
    {
        public int BranchCommitteeId { get; set; }

        public int CommitteeId { get; set; }
        public string CommitteeName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string AgencyCode { get; set; }

        public List<int> CommitteeIds { set; get; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "CommitteeName")]
        public string CommitteeIdsString { set; get; }
        public List<CommitteeModel> Committees { get; set; }
    }
}
