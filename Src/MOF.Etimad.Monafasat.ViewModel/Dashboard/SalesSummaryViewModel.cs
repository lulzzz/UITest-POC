using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SalesSummaryViewModel
    {
        public QueryResult<SalesListViewModel> Sales { get; set; } = new QueryResult<SalesListViewModel>(new List<SalesListViewModel>(), 0, 1, 10);

        public decimal PriceTotal { get; set; }

        public int Total { get; set; }
        //public string AgencyName { get; set; }
    }
}
