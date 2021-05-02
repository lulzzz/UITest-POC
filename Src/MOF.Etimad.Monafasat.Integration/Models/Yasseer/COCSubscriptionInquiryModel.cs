namespace MOF.Etimad.Monafasat.Integration
{
    public class COCSubscriptionInquiryRequestModel
    {
        public string LicenseNumber { get; set; }
        public string CityCode { get; set; }
    }
    public class COCSubscriptionInquiryModel
    {
        public MembershipDetails MembershipDetails { get; set; }
        public MembershipEstablishment MembershipEstablishment { get; set; }
        public MembershipSijil MembershipSijil { get; set; }
        public MembershipActivity MembershipActivity { get; set; }
        public MembershipOwner MembershipOwner { get; set; }
        public MembershipAddress MembershipAddress { get; set; }
        public string[] MembershipPhone { get; set; }
        public string[] MembershipFax { get; set; }
        public string[] MembershipMobile { get; set; }
        public string[] MembershipEmail { get; set; }
        public string[] MembershipWebsite { get; set; }
        public MembershipCapital MembershipCapital { get; set; }
    }
}
