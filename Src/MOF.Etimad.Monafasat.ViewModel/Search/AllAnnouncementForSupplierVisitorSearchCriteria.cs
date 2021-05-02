using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Search
{
    public class AllAnnouncementForSupplierVisitorSearchCriteria : SearchCriteria
    {
        public string ActivitiesIdString { get; set; }
        public string SubActivitiesIdString { get; set; }
        public List<int> ActivitiesIds
        {
            get
            {
                if (!string.IsNullOrEmpty(ActivitiesIdString))
                {
                    return new List<int>(Array.ConvertAll(ActivitiesIdString.Split(','), int.Parse));
                }
                else
                {
                    return new List<int>();
                }
            }
            set { }
        }
        public List<int> SubActivitiesIds
        {
            get
            {
                if (!string.IsNullOrEmpty(SubActivitiesIdString))
                {
                    return new List<int>(Array.ConvertAll(SubActivitiesIdString.Split(','), int.Parse));
                }
                else
                {
                    return new List<int>();
                }
            }
            set { }
        }
        public string AgencyCode { get; set; }
        public string AreasIdString { get; set; }
        public List<int> AreasIds
        {
            get
            {
                if (!string.IsNullOrEmpty(AreasIdString))
                {
                    return new List<int>(Array.ConvertAll(AreasIdString.Split(','), int.Parse));
                }
                else
                {
                    return new List<int>();
                }
            }
            set { }
        }
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime FromLastJoinDate { get; set; }
        public string FromLastJoinDateString { get; set; }
        public DateTime ToLastJoinDate { get; set; }
        public string ToLastJoinDateString { get; set; }
        public DateTime PublishDate { get; set; }
        public string PublishDateString { get; set; }
        public int? StatusId { get; set; }
        public string AnnouncementStageIdString { get; set; }
    }
}
