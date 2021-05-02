namespace MOF.Etimad.Monafasat.Integration
{
    public class MembershipAddress
    {
        public MembershipAddress()
        {
        }
        public MembershipAddress(string cityName, string pOBox, string region, string locationInfo, string postalCode)
        {
            CityName = cityName;
            POBox = pOBox;
            Region = region;
            LocationInfo = locationInfo;
            PostalCode = postalCode;
        }
        public string POBox { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public string Region { get; set; }
        public string LocationInfo { get; set; }
    }
}
