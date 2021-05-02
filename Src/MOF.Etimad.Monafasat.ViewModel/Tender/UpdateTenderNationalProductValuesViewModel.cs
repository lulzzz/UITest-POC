namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UpdateTenderNationalProductValuesViewModel
    {
        public string Id { get; set; }
        public string TenderIdString { get; set; }
        public decimal NationalProductRate { get; set; }
        public decimal? MinimumBaseline { get; set; }

        public decimal? MinimumPercentageLocalContentTarget { get; set; }

        public decimal NationalProductPercentage { get; set; }
         

        public decimal? LocalContentMaximumPercentage { get; set; }

        public decimal? PriceWeightAfterAdjustment { get; set; }

        public decimal? LocalContentWeightAndFinancialMarket { get; set; }

        public decimal? BaselineWeight { get; set; }
        public decimal? LocalContentTargetWeight { get; set; }

        public decimal? FinancialMarketListedWeight { get; set; }
    }
}
