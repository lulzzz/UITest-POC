using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PlaintSearchCriteria : SearchCriteria
    {
        public int TenderId { get; set; }
        public string AgencyRequestId { get; set; }
        public string TenderIdString { get; set; }
    }
}
