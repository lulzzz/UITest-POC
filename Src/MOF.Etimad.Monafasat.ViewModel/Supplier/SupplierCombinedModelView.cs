using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierCombinedModelView
    {


        [Display(Name = "إسم المورد")]
        public string SupplierName { get; set; }
        [Display(Name = "الإسم التجارى")]
        public string CommericalRegisterName { get; set; }

        [Display(Name = "رقم السجل التجارى")]
        public string CommericalRegisterNo { get; set; }

        [Display(Name = "رقم الجوال")]
        public string MobileNumber { get; set; }
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }

    }

    public class SupplierCombinedRequestModelView
    {


        [Display(Name = "إسم المورد")]
        public string SupplierName { get; set; }
        [Display(Name = "الإسم التجارى")]
        public string CommericalRegisterName { get; set; }

        [Display(Name = "رقم السجل التجارى")]
        public string CommericalRegisterNo { get; set; }
        [Display(Name = "تاريخ الطلب")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "تاريخ الطلب")]
        public string CreatedAtHihri { get; set; }

        [Display(Name = "حالة الطلب")]
        public int StatusId { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierMobileNo { get; set; }
        public bool IsChecked { get; set; }
        public bool IsMoved { get; set; }
        public string TenderName { get; set; }
        public int TenderId { get; set; }
        public string TenderNumber { get; set; }
        public int InvitationId { get; set; }
        public string InvitationIdString { get; set; }

        public InvitationStatusModel InvitationStatus { get; set; }


    }



}
