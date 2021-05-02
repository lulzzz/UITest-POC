using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
namespace MOF.Etimad.Monafasat.Integration
{
    public class SupplierIntegrationSearchCriteria : SearchCriteria
    {
        public List<string> SupplierNumbers { get; set; } = new List<string>();
        public string CrNumber { get; set; }
        public string SupplierName { get; set; }
        public string Activity { get; set; }
        public string activityDescription { get; set; }
        public int isicActivityID { get; set; }
        public int isicActivityLevel { get; set; }
        public string CityName { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public bool IsCountOnly { get; set; } = false;
        public bool IsRandomSort { get; set; }
    }
}
