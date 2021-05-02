
using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CommitteeSearchCriteriaModel : SearchCriteria
    {
        [DisplayName("اسم اللجنة")]
        public string CommitteeName { get; set; }
        [DisplayName("رقم تسلسل الفرع")]
        public int BranchId { get; set; }
        [DisplayName("رقم تسلسل اللجنة")]
        public int CommitteeTypeId { get; set; }
        public string CommitteeTypeIdString { get; set; }
        public string AgencyCode { get; set; }
    }
}
