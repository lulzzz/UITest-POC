using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ExtendOffesValidityDashboard
    {
        public int TenderId { get; set; }
        public int CommunicationRequestId { get; set; }
        public string TenderIdString { get; set; }
        public string CommunicationRequestIdString { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string RefrenceNumber { get; set; }
        public DateTime? LastOfferPresentationDate { get; set; }
        public string AcceptanceTypeName { get; set; }



    }
}
