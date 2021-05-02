using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QualificationStatusModel
    {
        public int tenderId { set; get; }
        public string TenderIdString { set; get; }
        public int StatusId { set; get; }
        public int? PreQualificationTypeId { set; get; }
        public int? PreQualificationCommitteeId { set; get; }
        public bool IsFavouriteTender { get; set; }
        public string RejectionReason { get; set; }
        public int TenderTypeId { get; set; }
        public string RejectionReasonSelectedStr { get; set; }
        public string AgencyCode { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        public bool isSupplierPassed { get; set; }
        public bool isSupplierParticipating { get; set; }
    }
}
