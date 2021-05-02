
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class VROOffersTechnicalCheckingViewModel
    {
        public string OfferIdString { get; set; }
        public string TenderIdString { get; set; }
        public int? OfferAcceptanceStatusId { get; set; }
        public int? OfferTechnicalEvaluationStatusId { get; set; }
        public string RejectionReason { get; set; }

        [Required]
        public int TechnicalEvaluationLevel { get; set; }
        public string Notes { get; set; }
        public int TenderStatusId { get; set; }
        public int TenderTypeId { get; set; }
        public DateTime? OffersCheckingDateTime { get; set; }
        public string IsOpened { get; set; }
    }
}
