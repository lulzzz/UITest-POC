using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SimpleTenderSearchCriteria : SearchCriteria
    {
        public int userId;

        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public string CR { get; set; }
        public int SelectedCommittee { set; get; }
        public string currentUserRole { get; set; }
    }
}
