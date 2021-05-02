namespace MOF.Etimad.Monafasat.Integration
{
    public class PartyIdModel
    {
        public PartyIdModel()
        {
        }
        public PartyIdModel(string partyIdNumber, PartyType partyIdType)
        {
            PartyIdNumber = partyIdNumber;
            PartyIdType = partyIdType;
        }
        public string PartyIdNumber { get; set; }
        public PartyType PartyIdType { get; set; }
    }
}
