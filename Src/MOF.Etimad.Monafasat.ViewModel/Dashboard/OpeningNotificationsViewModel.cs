using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OpeningNotificationsViewModel
    {
        [Display(Name = "إسم المنافسة")]
        public string TenderName { get; set; }
        [Display(Name = "رقم المنافسة")]
        public string TenderNumber { get; set; }
        [Display(Name = "تاريخ فتح العروض")]
        public DateTime? OffersOpeningDate { get; set; }
    }
}
