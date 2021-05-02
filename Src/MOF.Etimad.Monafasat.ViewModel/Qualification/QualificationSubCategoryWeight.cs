namespace MOF.Etimad.Monafasat.ViewModel.Qualification
{
    public class QualificationSubCategoryWeight
    {
        public decimal PreviousExperienceWeight { get; set; }
        public decimal PreviousExperiencePoints { get; set; }
        public decimal ExistingContractualObligationsWeight { get; set; }
        public decimal ExistingContractualObligationsPoints { get; set; }
        public decimal HRWeight { get; set; }
        public decimal HRPoints { get; set; }
        public decimal QualityWeight { get; set; }
        public decimal QualityPoints { get; set; }

        public decimal EnviromentAndHealthyWeight { get; set; }
        public decimal EnviromentAndHealthyPoints { get; set; }
        public decimal InsuranceWeight { get; set; }
        public decimal InsurancePoints { get; set; }
        public decimal FinancialStatementsWeight { get; set; }
        public decimal FinancialStatementsPoints { get; set; }
        public decimal FinancialPerformanceIndicatorsWeight { get; set; }
        public decimal FinancialPerformanceIndicatorsPoints { get; set; }
        public decimal BalanceSheetStatementWeight { get; set; }
        public decimal BalanceSheetStatementPoints { get; set; }
        public decimal IncomeListWeight { get; set; }
        public decimal IncomeListPoints { get; set; }

        public decimal SupplierTotalPoints { get; set; }
        public string SupplierResultName { get; set; }
        public string FailingReason { get; set; }

        #region BasicData
        public int SubCategoryId { get; set; }
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public decimal TenderPointsToPass { get; set; }
        public decimal TechnicalAdministrativeCapacity { get; set; }
        public decimal FinancialCapacity { get; set; }
        public string ItemName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }

        //For supplier
        public decimal TechnicalAdministrativeAcquiredPoints { get; set; }
        public decimal FinancialAcquiredPoints { get; set; }
        #endregion
    }
}
