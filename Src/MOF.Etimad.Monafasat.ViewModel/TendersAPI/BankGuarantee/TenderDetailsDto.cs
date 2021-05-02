using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderDetailsDto
    {
        public string TenderName { get; set; }
        public string ReferenceNumber { get; set; }
        public int TenderStatsId { get; set; }
        public string TenderStatusName { get; set; }
        public string AgencyName { get; set; }
        public string AgencyCode { get; set; }
        public DateTime OffersOpeningDate { get; set; }
        public List<AwardedSuppliers> AwardedSuppliers { get; set; }
    }

    public class AwardedSuppliers
    {
        public string SupplierCr { get; set; }
        public string SupplierName { get; set; }
    }
}
