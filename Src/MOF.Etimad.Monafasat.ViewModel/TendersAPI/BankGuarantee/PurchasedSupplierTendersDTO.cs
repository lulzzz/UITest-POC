using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PurchasedSupplierTendersDto
    {
        public string TenderName { get; set; }
        public string ReferenceNumber { get; set; }
        public int TenderStatsId { get; set; }
        public string TenderStatusName { get; set; }
        public string AgencyName { get; set; }
        public string AgencyCode { get; set; }
        public DateTime OffersOpeningDate { get; set; }
    }
}
