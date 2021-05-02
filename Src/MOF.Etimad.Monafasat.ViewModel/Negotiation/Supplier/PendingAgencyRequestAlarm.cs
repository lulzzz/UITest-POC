using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PendingAgencyRequestAlarm
    {
        public string RequestIdString { get; set; }
        public string NegotiationIdString { get; set; }
        public string ExtendOffersValidityIdString { get; set; }
        public string TenderName { get; set; }
        public string NegotiationTypeName { get; set; }
        public string TenderIdString { get; set; }
        public int AgencyRequestTypeId { get; set; }
        public string AgencyRequestTypeIdString { get; set; }
        public string AgencyRequestTypeName { get; set; }
        public DateTime? RequestCreatedTime { get; set; }
    }
}
