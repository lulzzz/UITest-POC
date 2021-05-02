using System.ComponentModel;

namespace MOF.Etimad.Monafasat.ViewModel.Qualification
{
    public class QualificationMediumEvaluation : QualificationSubCategoryWeight
    {
        public int id { get; set; }
        #region Technical and administrative capacity

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal ExperienceYearCountMax { get; set; }
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal ExperienceYearCountMin { get; set; }
        public decimal ExperienceYearCountWeight { get; set; }
        public decimal SupplierExperienceYearCount { get; set; }
        public decimal SupplierPointsExperienceYearCount { get; set; }

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal LastProjectCountMax { get; set; }
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal LastProjectCountMin { get; set; }
        public decimal LastProjectCountWeight { get; set; }
        public decimal SupplierLastProjectCount { get; set; }
        public decimal SupplierPointsLastProjectCount { get; set; }

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal LastProjectCostMax { get; set; }
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal LastProjectCostMin { get; set; }
        public decimal LastProjectCostWeight { get; set; }
        public decimal SupplierLastProjectCost { get; set; }
        public decimal SupplierPointsLastProjectCost { get; set; }

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal CurrentProjectCountMax { get; set; }
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal CurrentProjectCountMin { get; set; }
        public decimal CurrentProjectCountWeight { get; set; }
        public decimal SupplierCurrentProjectCount { get; set; }
        public decimal SupplierPointsCurrentProjectCount { get; set; }

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal CurrentProjectCostMax { get; set; }
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal CurrentProjectCostMin { get; set; }
        public decimal CurrentProjectCostWeight { get; set; }
        public decimal SupplierCurrentProjectCost { get; set; }
        public decimal SupplierPointsCurrentProjectCost { get; set; }

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal TotalEmployeeCountMax { get; set; }
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal TotalEmployeeCountMin { get; set; }
        public decimal TotalEmployeeCountWeight { get; set; }
        public decimal SupplierTotalEmployeeCount { get; set; }
        public decimal SupplierPointsTotalEmployeeCount { get; set; }

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal TotalEmployeePercentageMax { get; set; }
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal TotalEmployeePercentageMin { get; set; }
        public decimal TotalEmployeePercentageWeight { get; set; }
        public decimal SupplierTotalEmployeePercentage { get; set; }
        public decimal SupplierPointsTotalEmployeePercentage { get; set; }

        #endregion


        #region FinancialCapacity

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal CashRateMax { get; set; } = 0.0M;
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal CashRateMin { get; set; } = 0.0M;
        public decimal CashRateWeight { get; set; }
        public decimal SupplierCashRate { get; set; }
        public decimal SupplierPointsCashRate { get; set; }

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal LiquidityRatioMax { get; set; } = 0.0M;
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal LiquidityRatioMin { get; set; } = 0.0M;
        public decimal LiquidityRatioWeight { get; set; }
        public decimal SupplierLiquidityRatio { get; set; }
        public decimal SupplierPointsLiquidityRatio { get; set; }

        [DefaultValue(typeof(decimal), "0.01")]
        public decimal TradeRateMax { get; set; } = 0.0M;
        [DefaultValue(typeof(decimal), "0.01")]
        public decimal TradeRateMin { get; set; } = 0.0M;
        public decimal TradeRateWeight { get; set; }
        public decimal SupplierTradeRate { get; set; }
        public decimal SupplierPointsTradeRate { get; set; }

        #endregion
    }
}
