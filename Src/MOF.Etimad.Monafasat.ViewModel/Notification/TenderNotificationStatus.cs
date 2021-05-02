using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderNotificationStatus
    {
        public string Consignee { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TenderName { get; set; }
        public string SendAs { get; set; }
        public DateTime? SendAt { get; set; }
        public string SendStatus { get; set; }

    }
}
