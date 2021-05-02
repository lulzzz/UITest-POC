namespace MOF.Etimad.Monafasat.Integration
{
    public class CompanyInformationModel
    {
        public CompanyInformationModel() { }

        public CompanyInformationModel(string businessNameAr, string gOSIRegistrationId, string businessNameEn)
        {
            BusinessNameAr = businessNameAr;
            GOSIRegistrationId = gOSIRegistrationId;
            BusinessNameEn = businessNameEn;
        }

        public string GOSIRegistrationId { get; set; }
        public string BusinessNameAr { get; set; }
        public string BusinessNameEn { get; set; }
    }
}
