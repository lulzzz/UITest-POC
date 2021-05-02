using MOF.Etimad.Monafasat.ViewModel.Invitation;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Reports
{
    public class AppliedSuppliersReportModel
    {
        public string TenderName { get; set; }
        public int BranchId { get; set; }
        public string TenderIdString { get; set; }
        public string TenderNumber { get; set; }
        public string TenderReferenceNumber { get; set; }
        public string TenderStatusName { get; set; }
        public string TenderTypeName { get; set; }
        public int? InvitationsCount { get; set; }
        public int? OffersCount { get; set; }
        public int? BuyersCount { get; set; }
        public List<AppliedSuppliersReportDetailsModel> SuppliersDetails { get; set; }

    }
}
