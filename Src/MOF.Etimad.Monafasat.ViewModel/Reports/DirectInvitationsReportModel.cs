using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class DirectInvitationsReportModel
    {
        public Dictionary<int, TenderCounts> TenderCounts { get; set; }
        public int TotalInvitationsCount { get; set; }
        public int TotalPurchaseCount { get; set; }
        public string Date { get; set; }
    }
    public class TenderCounts
    {
        public int InvitationsCount { get; set; }
        public int PurchaseCount { get; set; }
    }
}
