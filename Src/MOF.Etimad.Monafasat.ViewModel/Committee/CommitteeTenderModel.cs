using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CommitteeTenderModel
    {
        public int CommitteeTenderId { get; set; }
        public int TenderId { get; set; }
        public List<int> TenderIds { get; set; }
        public int CommitteeId { get; set; }
        public int CommitteeTypeId { get; set; }
        public string CommitteeName { get; set; }
        public string Address { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }

        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        public List<BasicTenderModel> Tenders { get; set; }
    }
}