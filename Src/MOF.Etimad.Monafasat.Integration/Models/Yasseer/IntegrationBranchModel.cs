namespace MOF.Etimad.Monafasat.Integration
{
    public class IntegrationBranchModel
    {
        public IntegrationBranchModel()
        {

        }
        public IntegrationBranchModel(string branchName, string branchSerial, PartyIdModel branchIdentity, string licenseNumber, IntegrationCityModel city)
        {
            BranchName = branchName;
            BranchSerial = branchSerial;
            BranchIdentity = branchIdentity;
            LicenseNumber = licenseNumber;
            City = city;
        }
        public string BranchName { get; set; }
        public string BranchSerial { get; set; }
        public PartyIdModel BranchIdentity { get; set; }
        public string LicenseNumber { get; set; }
        public IntegrationCityModel City { get; set; }
    }
}
