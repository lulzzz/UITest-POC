using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CommitteesModel
    {
        public List<LookupModel> TechnicalCommittees { get; set; } = new List<LookupModel>();
        public List<LookupModel> CheckCommittees { get; set; } = new List<LookupModel>();
        public List<LookupModel> OpenCommittees { get; set; } = new List<LookupModel>();

        public List<LookupModel> purchaseCommittees { get; set; } = new List<LookupModel>();

        public List<LookupModel> preQualificationCommittees { get; set; } = new List<LookupModel>();
        public List<LookupModel> VROCommittee { get; set; } = new List<LookupModel>();

    }
}
