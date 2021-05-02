using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ReportSpendingBySpendAgencyModel
    {
        public string AgencyName { get; set; }
        public decimal? SizeOfSpending { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? AverageSpending { get; set; }
        public string TopItemSpend { get; set; }
        public List<ItemOfExpenditure> DetailsOfSpend { get; set; }
        public decimal? AverageExpenditureOfContracts { get; set; }
        public int TenderCount { get; set; }

    }
    public class ItemOfExpenditure
    {
        public string Name { get; set; }
        public decimal? SizeOfSpending { get; set; }


    }
}
