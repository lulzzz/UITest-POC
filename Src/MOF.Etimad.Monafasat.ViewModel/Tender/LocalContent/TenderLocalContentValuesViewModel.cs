using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.ViewModel.Tender.LocalContent
{
    public class TenderLocalContentValuesViewModel

    {
        public int? QuantityTableVersionId { get; set; }
        public bool IsTenderContainsTawreedTables { get; set; }
        public bool ContainsSupply { get; set; }
        public bool ConShowNationalProduct { get; set; }
        public int TenderTypeId { get; set; }
        public bool IsOldTender
        {
            get => TenderTypeId == (int)Enums.TenderType.CurrentTender || TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
        }

        public int? MinimumBaseline { get; set; }
        public int? MinimumPercentageLocalContentTarget { get; set; }
        public decimal? NationalProductPercentage { get; set; }
        public decimal? HighValueContractsAmmount { get; set; }
        public decimal? LocalContentMaximumPercentage { get; set; }
        public decimal? PriceWeightAfterAdjustment { get; set; }
        public decimal? LocalContentWeightAndFinancialMarket { get; set; }
        public decimal? BaselineWeight { get; set; }
        public decimal? LocalContentTargetWeight { get; set; }
        public decimal? FinancialMarketListedWeight { get; set; }
        public int? TenderLocalContentId { get; set; }
        public List<int> LocalContentMechanismIds { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsTenderNewWithLocalContent { get; set; }
        public string TenderIdString { get; set; }
    }
}
