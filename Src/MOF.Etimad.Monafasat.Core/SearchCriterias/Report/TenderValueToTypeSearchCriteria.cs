using MOF.Etimad.Monafasat.SharedKernal;
using System;

namespace MOF.Etimad.Monafasat.Core.SearchCriterias.Report
{
    public class TenderValueToTypeSearchCriteria : SearchCriteria
    {
        public int? AgencyCode { get; set; }
        public int? AgencyBeanchId { get; set; }
        public DateTime? FromCreatedDate { get; set; }
        public DateTime? ToCreatedDate { get; set; }
        public int? ConditionsBookletPrice { get; set; }
        public int? TenderPrice { get; set; }
    }
}
