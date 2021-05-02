namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AgencyFileViewModel
    {
        public string AgencyName { get; set; }
        public string BranchName { get; set; }
        public string joinDate { get; set; }
        public string FirstTenderDate { get; set; }
        public string LastTenderDate { get; set; }
        public int TendersCount { get; set; }
        public int DirectPurchaseCount { get; set; }
        public decimal TenderLimitedValue { get; set; }
        public decimal TenderReverseBiddingValue { get; set; }
        public decimal TenderFrameworkAgreementValue { get; set; }
        public int NumberOfPurchasedSuppliers { get; set; }
        public decimal TotalPurchasedValue { get; set; }

    }
}
