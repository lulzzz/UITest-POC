using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AllAgencyAnnouncementSearchCriteria : SearchCriteria
    {
        public string AgencyCode { get; set; }
        public string CurrentAgencyCode { get; set; }
        public int? BranchId { get; set; }
        public int? CurrentBranchId { get; set; }
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FromPublishDate { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ToPublishDate { get; set; }
        public string FromPublishDateString { get; set; }
        public string ToPublishDateString { get; set; }
        public int? AnnouncementStatusId { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string CurrentRoleName { get; set; }
        public int? TenderTypeId { get; set; }
    }

    public class AllAgencyAnnouncementSearchCriteriaModel : SearchCriteria
    {
        public string AgencyCode { get; set; }
        public string CurrentAgencyCode { get; set; }
        public int? BranchId { get; set; }
        public int? CurrentBranchId { get; set; }
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }

        public DateTime? FromPublishDate { get; set; }
        public DateTime? ToPublishDate { get; set; }
        public string FromPublishDateString { get; set; }
        public string ToPublishDateString { get; set; }
        public int? AnnouncementStatusId { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string CurrentRoleName { get; set; }
        public int? TenderTypeId { get; set; }
    }
}
