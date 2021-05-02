using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AllAnouuncementsForSupplierVisitorModel
    {
        public string AnnouncementName { get; set; }
        public string AnnouncementIdString { get; set; }
        public string ReferenceNumber { get; set; }
        public int AnnouncementStatusId { get; set; }
        public string AnnouncementDetails
        {
            get
            {
                return AnnouncementName + " - " + ReferenceNumber;
            }
        }
        public string AgencyName { get; set; }
        public string AgencyCode { get; set; }
        public string TenderTypeString { get; set; }
        public bool hasApprovedRequest { get; set; }
        public string AnnouncementStatusString { get; set; }
        public int? AnnouncementPeriod { get; set; }
        public DateTime? PublishedDate { get; set; }


        public string PublishDateHijriString
        {
            get
            {
                return PublishedDate.HasValue ? PublishedDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
            }
        }
        public string PublishDateString
        {
            get
            {
                return PublishedDate.HasValue ? PublishedDate.Value.ToGregorianDate("dd/MM/yyyy") : "";
            }
        }
        public string LastApplyingRequestsDateString
        {
            get
            {
                return PublishedDate.HasValue && AnnouncementPeriod.HasValue ? PublishedDate.Value.AddDays(AnnouncementPeriod.Value).ToGregorianDate("dd/MM/yyyy") : "";
            }
        }
        public string LastApplyingRequestsHijriDateString
        {
            get
            {
                return PublishedDate.HasValue && AnnouncementPeriod.HasValue ? PublishedDate.Value.AddDays(AnnouncementPeriod.Value).ToHijriDateWithFormat("dd/MM/yyyy") : "";
            }
        }
    }
}
