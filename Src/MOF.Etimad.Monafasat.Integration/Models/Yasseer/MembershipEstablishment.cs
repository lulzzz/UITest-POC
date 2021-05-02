namespace MOF.Etimad.Monafasat.Integration
{
    public class MembershipEstablishment
    {
        public MembershipEstablishment()
        {

        }
        public MembershipEstablishment(string estJuridicalForm, string estName, string estNationality, string estNumOfBranches)
        {
            EstablishmentName = estName;
            EstablishmentJuridicalForm = estJuridicalForm;
            NumberOfBranches = estNumOfBranches;
            EstablishmentNationality = estNationality;
        }
        public string EstablishmentName { get; set; }
        public string EstablishmentJuridicalForm { get; set; }
        public string EstablishmentNationality { get; set; }
        public string NumberOfBranches { get; set; }
    }
}
