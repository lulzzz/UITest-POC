namespace MOF.Etimad.Monafasat.ViewModel
{
    public class LocalContentPreferenceModel
    {
        public decimal lowestOfferPrice { get; set; }
        public decimal currentOfferPrice { get; set; }
        public decimal supplierLocalContentPercentage { get; set; }
        public decimal supplierBaselinePercentage { get; set; }
        public bool isIncludedInMarket { get; set; }

        public decimal priceWeight { get; set; }
        public decimal localContentTargetWeight { get; set; }
        public decimal baselineWeight { get; set; }
        public decimal includedInMarketWeight { get; set; }
        public decimal localContentWeight { get; set; }
    }
}
