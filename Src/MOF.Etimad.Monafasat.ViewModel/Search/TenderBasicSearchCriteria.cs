
using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderBasicSearchCriteria : SearchCriteria
    {
        public TenderBasicSearchCriteria()
        {
            PageSize = 6;
        }
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
    }
}
