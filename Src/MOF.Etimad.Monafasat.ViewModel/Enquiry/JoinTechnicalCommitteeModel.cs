using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class JoinTechnicalCommitteeModel
    {
        public int JoinTechnicalCommitteeId { get; set; }
        public int EnquiryId { get; set; }
        public int ReplyId { get; set; }
        public string EnquiryIdString { get; set; }
        public int JoinedCommitteeId { get; set; }
        public int SelectedUserCommitteeId { get; set; }
        public int TenderId { get; set; }
        public List<LookupModel> CommitteeList { get; set; }
        public string CommitteeName { get; set; } = "";
        public DateTime? EnquirySendingDate { get; set; }
        public string EnquiryName { get; set; }
        public List<CommentModel> InternelComments { get; set; }
        public string EnquiryComment { get; set; }





    }
    public class CommentModel
    {
        public string InternelComment { get; set; }

    }
}