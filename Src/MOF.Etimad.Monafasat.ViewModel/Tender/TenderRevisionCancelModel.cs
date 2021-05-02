using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderRevisionCancelModel
    {
        public string TenderIdString { get; set; }
        public string CreatedByRoleName { get; set; }
        public string CreatedBy { get; set; }
        public BasicTenderModel Tender { get; set; }
        public string StatusName { get; set; }
        public string RejectionReason { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string CancelationReasonDescription { get; set; }
        public int? CancelationReasonId { get; set; }
        public List<string> SupplierViolatorCRs { get; set; }
        public string SupplierViolatorCR { get; set; }

    }

}