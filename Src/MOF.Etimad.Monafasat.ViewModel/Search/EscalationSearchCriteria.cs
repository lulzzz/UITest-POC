
using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class EscalationSearchCriteria : SearchCriteria
    {
        #region Fields ========
        #region Basic Search Criteria
        public int TenderId { get; set; }

        public int TenderTypeId { get; set; }
        public decimal? ConditionsBookletPrice { get; set; }
        #endregion
        #region Advanced Search Criteria
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        #endregion
        public string AgencyCode { get; set; }
        #endregion
    }
}
