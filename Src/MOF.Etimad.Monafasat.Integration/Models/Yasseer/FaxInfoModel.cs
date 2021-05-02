namespace MOF.Etimad.Monafasat.Integration
{
    public class FaxInfoModel
    {
        public FaxInfoModel() { }
        public PhoneDetailsModel PhoneDetails { get; set; }
        public string PreferPhoneFlag { get; set; }
        public string PhoneFlag { get; set; }
        public string PhoneUse { get; set; }
    }
}