using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class DirectInvitationsSummaryViewModel
    {
        public QueryResult<DirectInvitationsListViewModel> DirectInvitations { get; set; } = new QueryResult<DirectInvitationsListViewModel>(new List<DirectInvitationsListViewModel>(), 0, 1, 10);
        public int Total { get; set; }
        //public string AgencyName { get; set; }
    }
}
