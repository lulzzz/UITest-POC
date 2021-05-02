namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AmountOfSavingsViewModel
    {
        public string AgencyName { get; set; }

        public string TenderName { get; set; }
        public decimal EstimatedValueBeforeReview { get; set; }
        public decimal EstimatedValueAfterReview { get; set; }
        public decimal TheValueOfTheAward { get; set; }
        public decimal AmountOfSavings { get; set; }


    }
    public class TotalAmountOfSavingsViewModel
    {
        public decimal TotalAmountOfSavings { get; set; }
        public decimal AmountOfSavingsPercentage { get; set; }


    }
}
