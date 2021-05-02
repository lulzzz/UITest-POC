namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TendersSummryReportViewModel
    {
        public string AgencyName { get; set; }
        public string BranchName { get; set; }
        public int TendersCount { get; set; }
        public int DirectPurchaseCount { get; set; }
        public decimal TenderLimitedValue { get; set; }
        public decimal TenderReverseBiddingValue { get; set; }
        public decimal TenderFrameworkAgreementValue { get; set; }

        public int NumberOfPurchasedSuppliers { get; set; }
        public int NumberOfSuppliersQueries { get; set; }
        public int NumberOfOppenedOffers { get; set; }
        public int NumberTechnicalCheckedOfferd { get; set; }
        public int NumberAwardedOffered { get; set; }
        public string ElectricOffersOnTenders { get; set; }
        public string ElectricOffersOnDirectPurchase { get; set; }
        public string AccountManager { get; set; }
        public string AgencyClassification { get; set; }
        public string TrainingStatus { get; set; }
        public int NumberOfTrainees { get; set; }
        public int ElectronicOffersOnTenders { get; set; }
        public int ElectronicOffersOnDirectPurchase { get; set; }
    }
}
