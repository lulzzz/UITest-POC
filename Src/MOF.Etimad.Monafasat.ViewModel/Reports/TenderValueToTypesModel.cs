namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderValueToTypesModel
    {
        public decimal? PublicTendersValue { get; set; }
        public decimal? DirectPurchaseValue { get; set; }
        public decimal? LimitedTender { get; set; }
        public decimal? ReverseBidding { get; set; }
        public decimal? FrameworkAgreement { get; set; }

        public int PublicTendersCount { get; set; }
        public int DirectPurchaseTendersCount { get; set; }
        public string BrancheName { get; set; }
        public int TendersCount { get; set; }
        public string AgencyName { get; set; }
        public string[] AgencyNames { get; set; }
        public decimal?[] TendersValue { get; set; }
        public int SuppliersCount { get; set; }
    }
}
