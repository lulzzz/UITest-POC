namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferDetailsForCheckingTwoFilesModel
    {
        public string OfferIdString { set; get; }
        public int TenderStatusId { get; set; }
        public string TechnicalOfferStatusString { get; set; }
        public int? TechnicalOfferStatusId { get; set; }
        public string RejectionReason { get; set; }
        public string Notes { get; set; }

    }
}
