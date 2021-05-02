using System;

namespace MOF.Etimad.Monafasat.Integration
{
    public class MembershipDetails
    {
        public MembershipDetails(string membershipCity, string membershipClass, string membershipId, DateTime membershipFromDt, DateTime membershipToDt, string membershipFromDtHjr, string membershipToDtHjr)
        {
            MembershipCity = membershipCity;
            MembershipClass = membershipClass;
            MembershipId = membershipId;
            MembershipId = membershipId;
            MembershipFromDate = membershipFromDt;
            MembershipToDate = membershipToDt;
            MembershipFromHjriDate = membershipFromDtHjr;
        }
        public string MembershipId { get; set; }
        public string MembershipCity { get; set; }
        public string MembershipClass { get; set; }
        public DateTime MembershipFromDate { get; set; }
        public DateTime MembershipToDate { get; set; }
        public string MembershipFromHjriDate { get; set; }
        public string MembershipToHjriDate { get; set; }
    }
}
