namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class OfferAttachmentModelNew
    {
        public string TenderName { get; set; }
        public string TenderIdString { get; set; }
        public string OfferIdString { get; set; }
        public int[] ActivityIds { get; set; }

    }
    public class OfferAttachmentDataModel
    {
        public string fileName { get; set; }
        public string fileReferenceId { get; set; }
        public string fileTypeId { get; set; }
        public string fileIdName { get; set; }
    }
}
