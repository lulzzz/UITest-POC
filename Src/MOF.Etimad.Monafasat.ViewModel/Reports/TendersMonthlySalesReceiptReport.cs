namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderSalesMonthlyCountsPerMonth
    {
        public int CurrentMonthSalesCount { get; set; }
        public decimal? CurrentMonthSalesAmount { get; set; }
        public int AllMonthsSalesCount { get; set; }
        public decimal? AllMonthsSalesAmount { get; set; }
        public string Month { get; set; }
    }

    public class TenderSalesMonthlyRecipetReportPerAgency
    {
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public string AgencySector { get; set; }
        public string AgencyBranch { get; set; }
        public string AgencySection { get; set; }
        public string AgencySequence { get; set; }
        public string AgencyManagmentFollow { get; set; }
        public string AgencySectionFollow { get; set; }
        public int NumberOfTransactions { get; set; }
        public decimal? TotalAmount { get; set; }
    }

    public class TenderSalesMonthlyTenderDetails
    {
        public string TenderName { get; set; }
        public string PurchaseDate { get; set; }
        public string CompanyCommercialName { get; set; }
        public decimal? BookletPrice { get; set; }
    }
}
