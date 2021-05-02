namespace MOF.Etimad.Monafasat.Integration
{
    public class PartyModel
    {
        public PartyModel()
        {
        }
        public PartyModel(string fillNameAr, PartyIdModel partyId, OrganizationModel organization)
        {
            FillNameAr = fillNameAr;
            PartyId = partyId;
            Organization = organization;
        }
        public string FillNameAr { get; set; }
        public PartyIdModel PartyId { get; set; }
        public OrganizationModel Organization { get; set; }
    }
}
