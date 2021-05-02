using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementFinalApprovalModel
    {
        public List<string> SuppliersIdsString { get; set; }

        public bool? IsMarkedAll { get; set; }

        public string AnnouncementTemplateIdString { get; set; }

        public string VerificationCode { get; set; }
    }
}
