using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UnderOperationAgencyAnnouncementSearchCriteria : SearchCriteria
    {
        public string CurrentAgencyCode { get; set; }
        public int? CurrentBranchId { get; set; }
        public string CurrentRoleName { get; set; }
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }
        public int? AnnouncementStatusId { get; set; }
        public int? TenderTypeId { get; set; }
        public int? ActivityId { get; set; }
        public int? SubActivityId { get; set; }
        public string SubActiviesIdString { get; set; }
        //public List<int> ActivityIds { get; set; }
        public List<int> SubActivityIds
        {
            get
            {
                if (SubActiviesIdString != null)
                {
                    return new List<int>(Array.ConvertAll(SubActiviesIdString.Split(','), int.Parse));
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
