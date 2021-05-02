using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ExtendOffersDateRequestModel
    {
        public string TenderIdString { get; set; }
        public int TenderId { get; set; }
        public string RequestReason { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestedTime { get; set; }
        public string Cr { get; set; }
    }
}
