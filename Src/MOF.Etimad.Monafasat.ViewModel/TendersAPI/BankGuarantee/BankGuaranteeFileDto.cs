using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BankGuaranteeFileDto
    {
        [Required(ErrorMessage = "يجب ادخال رقم المنافسة ")]
        public string Tender_number { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم المورد ")]
        public string CR_number { get; set; }
        [Required(ErrorMessage = "يجب ادخال البنك ")]

        public int Bank_identity { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم الضمان البنكي ")]

        public string Guarantee_number { get; set; }
        [Required(ErrorMessage = "يجب ادخال قيمة الضمان البنكي ")]

        public decimal Guarantee_value { get; set; }
        [Required(ErrorMessage = "يجب ادخال تاريخ بداية الضمان البنكي  ")]

        public DateTime Guarantee_start_date { get; set; }
        [Required(ErrorMessage = "يجب ادخال تاريخ انتهاء الضمان البنكي ")]
        public DateTime Guarantee_end_date { get; set; }
        [Required(ErrorMessage = "يجب ادخال ملف الضمان البنكي ")]
        public string Guarantee_file { get; set; }
        public double? GuaranteeDays
        {
            get
            {
                return (Guarantee_end_date - Guarantee_start_date).TotalDays;
            }
        }
    }
}
