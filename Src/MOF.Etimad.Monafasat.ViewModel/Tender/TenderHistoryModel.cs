using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderHistoryModel
    {
        public int TenderHistoryId { get; set; }
        public int UserId { get; set; }
        public int TenderId { get; set; }
        public int TenderActionId { get; set; }
        public int StatusId { get; set; }
        [StringLength(2000)]
        public string RejectionReason { get; set; }
        public string UnitRejectionReason { get; set; }
        public string UnitModificationType { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<TenderAttachmentModel> UnitModificatationsFiles { get; set; }
    }
}