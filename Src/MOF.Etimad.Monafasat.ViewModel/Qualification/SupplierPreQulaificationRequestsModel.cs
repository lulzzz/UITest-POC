namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierPreQulaificationRequestsModel
    {
        public int SupplierPreQualificationDocumentId { get; set; }
        public string SupplierPreQualificationDocumentIdString { get; set; }
        public string ComercialNumber { get; set; }
        public string ComercialName { get; set; }
        public int? PreQualificationResult { get; set; }
        public string RejectionReason { get; set; }

        public bool IsValidToCheck { get; set; }

    }
}
