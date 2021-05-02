using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class AgencyLogoDetailsModel
    {
        public string baseUrl { get; set; }
        public List<AgencyLogoModel> logos { get; set; }
    }
    public class AgencyLogoModel
    {
        public string agencyCode { get; set; }
        public string logoReferenceId { get; set; }
    }
}
