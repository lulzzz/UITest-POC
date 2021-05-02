using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel.Negotiation
{
    public class NegotiationOffersSearchModel : SearchCriteria
    {

        public decimal DiscountValue { get; set; }
        public string TenderIdString { get; set; }
        public string NegotiationIdString { get; set; }
    }
}
