namespace MOF.Etimad.Monafasat.Integration
{
    public class MembershipActivity
    {
        public MembershipActivity()
        {
        }
        public MembershipActivity(string mainActivity, string secondActivity)
        {
            MainActivity = mainActivity;
            SecondActivity = secondActivity;
        }
        public string MainActivity { get; set; }
        public string SecondActivity { get; set; }
    }
}
