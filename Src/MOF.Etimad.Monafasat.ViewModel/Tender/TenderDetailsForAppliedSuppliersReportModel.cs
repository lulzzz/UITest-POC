namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderDetailsForAppliedSuppliersReportModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public string TenderName { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderNumber { get; set; }
        public string TenderStatusName { get; set; }
        public string TenderTypeName { get; set; }
        public int InvitationsCount { get; set; }
        public int PurchaseCount { get; set; }
        public int OffersCount { get; set; }
    }
}
