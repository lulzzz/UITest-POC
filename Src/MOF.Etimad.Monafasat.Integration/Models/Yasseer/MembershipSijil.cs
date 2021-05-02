using System;
namespace MOF.Etimad.Monafasat.Integration
{
    public class MembershipSijil
    {
        public MembershipSijil(DateTime sijilFromDt, string sijilFromDtHjr, string sijilNum, string sijilSource, DateTime sijilToDt, string sijilToDtHjr)
        {
            SijilSource = sijilSource;
            SijilNumber = sijilNum;
            SijilFromDate = sijilFromDt;
            SijilToDate = sijilToDt;
            SijilFromDateHjr = sijilFromDtHjr;
            SijilToDateHjr = sijilToDtHjr;
        }
        public string SijilNumber { get; set; }
        public string SijilSource { get; set; }
        public DateTime SijilFromDate { get; set; }
        public DateTime SijilToDate { get; set; }
        public string SijilFromDateHjr { get; set; }
        public string SijilToDateHjr { get; set; }
    }
}
