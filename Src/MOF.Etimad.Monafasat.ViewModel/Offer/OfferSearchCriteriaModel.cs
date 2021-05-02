using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferSearchCriteriaModel : SearchCriteria
    {
        public int TenderId { get; set; }
        public int SupplierId { get; set; }
        public bool? IsManuallyApplied { get; set; }
        public bool? IsActive { get; set; }
    }
}
