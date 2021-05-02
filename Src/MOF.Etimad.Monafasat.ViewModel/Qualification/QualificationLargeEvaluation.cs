namespace MOF.Etimad.Monafasat.ViewModel.Qualification
{
    public class QualificationLargeEvaluation : QualificationSubCategoryWeight
    {
        public int id { get; set; }
        #region Technical and administrative capacity

        public decimal ExperienceYearCountMax { get; set; }
        public decimal ExperienceYearCountMin { get; set; }
        public decimal ExperienceYearCountWeight { get; set; }
        public decimal SupplierExperienceYearCount { get; set; }
        public decimal SupplierPointsExperienceYearCount { get; set; }

        public decimal LastProjectCountMax { get; set; }
        public decimal LastProjectCountMin { get; set; }
        public decimal LastProjectCountWeight { get; set; }
        public decimal SupplierLastProjectCount { get; set; }
        public decimal SupplierPointsLastProjectCount { get; set; }

        public decimal LastProjectCostMax { get; set; }
        public decimal LastProjectCostMin { get; set; }
        public decimal LastProjectCostWeight { get; set; }
        public decimal SupplierLastProjectCost { get; set; }
        public decimal SupplierPointsLastProjectCost { get; set; }

        public decimal CurrentProjectCountMax { get; set; }
        public decimal CurrentProjectCountMin { get; set; }
        public decimal CurrentProjectCountWeight { get; set; }
        public decimal SupplierCurrentProjectCount { get; set; }
        public decimal SupplierPointsCurrentProjectCount { get; set; }

        public decimal CurrentProjectCostMax { get; set; }
        public decimal CurrentProjectCostMin { get; set; }
        public decimal CurrentProjectCostWeight { get; set; }
        public decimal SupplierCurrentProjectCost { get; set; }
        public decimal SupplierPointsCurrentProjectCost { get; set; }

        public decimal TotalEmployeeCountMax { get; set; }
        public decimal TotalEmployeeCountMin { get; set; }
        public decimal TotalEmployeeCountWeight { get; set; }
        public decimal SupplierTotalEmployeeCount { get; set; }
        public decimal SupplierPointsTotalEmployeeCount { get; set; }

        public decimal TotalEmployeePercentageMax { get; set; }
        public decimal TotalEmployeePercentageMin { get; set; }
        public decimal TotalEmployeePercentageWeight { get; set; }
        public decimal SupplierTotalEmployeePercentage { get; set; }
        public decimal SupplierPointsTotalEmployeePercentage { get; set; }

        public decimal InsuranceProfessionalCompensationWeight { get; set; }
        public decimal InsuranceLiabilityWeight { get; set; }
        public decimal InsuranceGeneralCommercialResponsibilityWeight { get; set; }
        public decimal SupplierInsuranceGeneralCommercialResponsibility { get; set; }
        public decimal SupplierPointsInsuranceGeneralCommercialResponsibility { get; set; }

        #endregion


        #region FinancialCapacity

        public decimal CashRateMax { get; set; } = 0.0M;
        public decimal CashRateMin { get; set; } = 0.0M;
        public decimal CashRateWeight { get; set; }
        public decimal SupplierCashRate { get; set; }
        public decimal SupplierPointsCashRate { get; set; }

        public decimal LiquidityRatioMax { get; set; } = 0.0M;
        public decimal LiquidityRatioMin { get; set; } = 0.0M;
        public decimal LiquidityRatioWeight { get; set; }
        public decimal SupplierLiquidityRatio { get; set; }
        public decimal SupplierPointsLiquidityRatio { get; set; }

        public decimal TradeRateMax { get; set; } = 0.0M;
        public decimal TradeRateMin { get; set; } = 0.0M;
        public decimal TradeRateWeight { get; set; }
        public decimal SupplierTradeRate { get; set; }
        public decimal SupplierPointsTradeRate { get; set; }

        public decimal ObligationsRationMax { get; set; } = 0.0M;
        public decimal ObligationsRationMin { get; set; } = 0.0M;
        public decimal ObligationsRationWeight { get; set; }
        public decimal SupplierObligationsRation { get; set; }
        public decimal SupplierPointsObligationsRation { get; set; }

        public decimal ProfitabilityRateMax { get; set; } = 0.0M;
        public decimal ProfitabilityRateMin { get; set; } = 0.0M;
        public decimal ProfitabilityRateWeight { get; set; }
        public decimal SupplierProfitabilityRate { get; set; }
        public decimal SupplierPointsProfitabilityRate { get; set; }
        #endregion
    }
}
