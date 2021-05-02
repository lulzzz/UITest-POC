using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderDataForNotification
    {
        public string TenderIdString { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string Purpose { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public DateTime? LastOfferPresentationDate { get; set; }
        public DateTime? OffersOpeningDate { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }
    }
}
