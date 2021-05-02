using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierPreQualificationDocumentModel
    {
        public int SupplierPreQualificationDocumentId { get; set; }
        public int PreQualificationId { get; set; }
        public int? PreQualificationResult { get; set; }
        public string RejectionReason { get; set; }
        public string SupplierId { get; set; }
        public int? FinalResultStatusId { get; set; }
        public string FinalResultStatusName { get; set; }
        public string SupplierName { get; set; }
        public bool canEditResult { get; set; }
        public string FailingReason { get; set; }
        public string PreQualificationResultString
        {
            get
            {
                if (PreQualificationResult == 1)
                    return Resources.OfferResources.DisplayInputs.Matching;
                else if (PreQualificationResult == 2)
                    return Resources.OfferResources.DisplayInputs.NotMatching;
                else return "0";
            }
        }
        public string SupplierPreQualificationDocumentIdString { get; set; }
        public string PreQualificationIdString { get; set; }
        public int TenderStatusId { get; set; }
        public DateTime? OffersCheckingDate { get; set; }
    }
}
