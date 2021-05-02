using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SimpleSearchCretriaModel : SearchCriteria
    {
        public int? TenderId { get; set; }
        public int TechnicalCommitteId { get; set; }
        public string EnquiryIdString { get; set; }
        public string TenderIdString { get; set; }
        public int? EnquiryId { get; set; }
        public int? OfferId { get; set; }
        public int UserId { get; set; }
    }
}
