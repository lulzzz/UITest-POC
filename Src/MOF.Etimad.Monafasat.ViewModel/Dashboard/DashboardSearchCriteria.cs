using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class DashboardSearchCriteria : SearchCriteria
    {
        [Display(Name = "الفترة الزمنية")]
        public string Duration { get; set; }
        [Display(Name = "الجهة الحكومية")]
        public string AgencyCode { get; set; }
        [Display(Name = "فرع الجهة الحكومية")]
        public int? BranchId { get; set; }
    }
}
