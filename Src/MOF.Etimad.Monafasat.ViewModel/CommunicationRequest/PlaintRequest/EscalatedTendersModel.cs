using MOF.Etimad.Monafasat.SharedKernal;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class EscalatedTendersModel
    {
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public int TenderId { get; set; }
        public string AgencyName { get; set; }
        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
        public string TenderStatusName { get; set; }
        public string TenderIdString { get; set; }
        public int TenderTypeId { get; set; }
        public string TenderTypeName { get; set; }
        public decimal? CondetionalBookletPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmitionDate { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string BranchName { get; set; }
        public int EscalationNumber { get; set; }
        public int AgencyRequestId { get; set; }
        public string AgencyRequestIdString { get { return Util.Encrypt(AgencyRequestId); } }
    }
}
