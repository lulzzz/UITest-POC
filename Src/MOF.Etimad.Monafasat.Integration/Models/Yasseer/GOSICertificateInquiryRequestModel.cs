namespace MOF.Etimad.Monafasat.Integration
{
    public class GOSICertificateInquiryRequestModel
    {
        public GOSICertificateInquiryRequestModel()
        {
        }
        public string GOSIId { get; set; }
        public GOSIIdType GOSIIdType { get; set; }
        public GOSIOwnerPersonIdType GOSIOwnerPersonIdType { get; set; }
    }
}
