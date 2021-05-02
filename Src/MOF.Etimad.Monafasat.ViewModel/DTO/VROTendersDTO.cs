using System;

namespace MOF.Etimad.Monafasat.ViewModel.DTO
{
    public class VROTendersDTO
    {
        public int TenderId { get; set; }
        public string TenderNumber { get; set; }
        public string TenderReferenceNumber { get; set; }
        public string TenderName { get; set; }
        public int TenderTypeId { get; set; }
        public int TenderStatusId { get; set; }
        public Decimal? ConditionsBookletPrice { get; set; }
        public DateTime? SubmitionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public DateTime? OffersOpeningDate { get; set; }
        public DateTime? LastOfferPresentationDate { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
        public string TenderTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public string AgencyName { get; set; }
        public string BranchName { get; set; }
        public DateTime? OffersCheckingDate { get; set; }
    }
}
