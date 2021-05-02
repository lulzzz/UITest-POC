using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CheckOfferResultModel
    {
        public string OfferIdString { get; set; }
        public int? FinantialOfferStatusId { get; set; }
        public string FinantialOfferStatusString { get; set; }
        public string RejectionReason { get; set; }
        public string Notes { get; set; }
        public int? TechnicalOfferStatusId { get; set; }
        public string TechnicalOfferStatusString { get; set; }
        public string FinantialRejectionReason { get; set; }
        public string FinantialNotes { get; set; }
        public bool IsBindedToMandatoryList { get; set; }
        public List<TechniciansReportAttachmentModel> TechniciansReportAttachments { get; set; }
        [Display(Name = "تقارير الفنيين")]
        public string TechniciansReportAttachmentsRef { get; set; }
    }
    public class OfferCheckingContainer
    {
        public CheckOfferResultModel checkingResult { get; set; }
        public OfferDetailModel model { get; set; }
        //public SaveOpeningOfferFinancialDetails tables { get; set; }
    }
}
