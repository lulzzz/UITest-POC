using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Qualification
{
    public class PrequalificationTechnicalExaminationModel
    {
        public int PrequalificationId { get; set; }
        public string QualificationNumber { get; set; }
        public DateTime? SubmitionDate { get; set; }
        public int AnnouncementPlace { get; set; }
        public List<PreQualificationResultForChecking> SupplierList { get; set; }
    }
}
