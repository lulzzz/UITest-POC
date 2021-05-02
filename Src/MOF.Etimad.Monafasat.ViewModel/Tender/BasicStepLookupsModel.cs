using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BasicStepLookupsModel
    {
        public List<CommitteeModel> allCommittees;
        public List<LookupModel> TenderTypes { set; get; }
        public List<LookupModel> ReasonForPurchaseTenderType { set; get; }
        public List<LookupModel> ReasonForLimitedTenderType { set; get; }
        public List<LookupModel> SpendingCategories { set; get; }
        public List<LookupModel> Qualification { set; get; }
        public List<LookupModel> Agencies { set; get; }
    }
}
