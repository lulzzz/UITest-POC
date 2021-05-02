using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferReportModel
    {
        public string TenderName { get; set; }
        public string Agency { get; set; }
        public List<Companies> Companies { get; set; }
    }

    public class Companies
    {
        public string CompanyName { get; set; }
        public string CommercialNumber { get; set; }
        public string Phone { get; set; }
        public string BuyingDate { get; set; }
        public string Fax { get; set; }
        public string PostOffice { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
