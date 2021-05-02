using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TendersSummaryViewModel
    {
        public QueryResult<TendersListViewModel> Tenders { get; set; } = new QueryResult<TendersListViewModel>(new List<TendersListViewModel>(), 0, 1, 10);

        public int Total { get; set; }
    }
}
