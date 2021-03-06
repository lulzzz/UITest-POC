namespace MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage
{
    public class OfferAttachmentModel
    {
        public string FileName { get; set; }

        public string FileNetId { get; set; }
        public string FileSize { get; set; }

        public string Extension { get; set; }

        public int FiletypeID { get; set; }
        public string FiletypeName { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public int? OfferPresentationWay { get; set; }

        public int TenderStatusId { get; set; }
    }
}
