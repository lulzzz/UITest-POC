using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderViewModel
    {
        public string TenderIdString { get; set; }
        [Display(Name = "إسم المنافسة")]
        public string TenderName { get; set; }
        [Display(Name = "رقم المنافسة")]
        public string TenderNumber { get; set; }
        [Display(Name = "حالة المنافسة")]
        public string TenderStatusName { get; set; }

        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
    }
}
