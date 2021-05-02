namespace MOF.Etimad.Monafasat.Integration
{
    public class MembershipOwner
    {
        public MembershipOwner(string ownerGender, string ownerName, string ownerNationality, string ownerPrefix, string ownerPosition)
        {
            OwnerGender = ownerGender;
            OwnerName = ownerName;
        }
        public string OwnerName { get; set; }
        public string OwnerGender { get; set; }
    }
}
