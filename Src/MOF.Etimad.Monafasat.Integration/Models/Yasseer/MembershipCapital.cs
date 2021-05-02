namespace MOF.Etimad.Monafasat.Integration
{
    public class MembershipCapital
    {
        public MembershipCapital(string allowedShares, string paidShares, string announcedCapital)
        {
            PaidShares = paidShares;
            AnnouncedCapital = announcedCapital;
        }
        public string AnnouncedCapital { get; set; }
        public int AllowedShares { get; set; }
        public string PaidShares { get; set; }
    }
}
