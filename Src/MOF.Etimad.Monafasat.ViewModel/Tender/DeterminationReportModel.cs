using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AwardingReportModel
    {
        public int TenderId { get; set; }
        public string TenderName { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public string OffersOpeningDate { get; set; }
        public string AwardingType { get; set; }
        public string AwardingReasons { get; set; }
        public List<Suppliers> Suppliers { get; set; }
    }

    public class Suppliers
    {
        public string CommericalRegisterName { get; set; }
        public string Amount { get; set; }
        public string AwardingReasons { get; set; }
    }
}
