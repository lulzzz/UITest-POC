using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class LinkTendersToCommitteeSearchCriteriaModel : SearchCriteria
    {
        public string AgencyCode { get; set; }
        public int? BranchId { get; set; }
        public List<int> BranchIdStringList { get; set; }
        //public string BranchIdStringList { get; set; }

        //public List<int> BranchIds
        //{
        //    get
        //    {
        //        if (BranchIdStringList != null)
        //        {
        //            return new List<int>(Array.ConvertAll(BranchIdStringList.Split(','), int.Parse));
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public string SelectedAgencyCode { get; set; }
        public int? SelectedBranchId { get; set; }
        public int? SelectedCommitteeId { get; set; }
        public int? CommitteeTypeId { get; set; }
        public int CommitteeId { get; set; }

        public bool? isCommitteeAssignedToBranch { get; set; }


    }
}