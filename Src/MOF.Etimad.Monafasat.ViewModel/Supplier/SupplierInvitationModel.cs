using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierInvitationModel
    {

        [Display(Name = "رقم المورد")]
        public int SupplierId { get; set; }
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
        public string mobile { get; set; }
        public string Email { get; set; }
        public List<string> SupplierContactOfficersEmails { get; set; }
        public List<string> SupplierMobileNo { get; set; }
        public bool IsChecked { get; set; }
        public bool IsMoved { get; set; }
        public string TenderName { get; set; }
        public int TenderId { get; set; }
        public string TenderNumber { get; set; }
        public int InvitationId { get; set; }
        public string InvitationIdString { get; set; }
        public string InvitationStatusIdString { get; set; }
        public string InvitationTypeIdString { get; set; }
        public int InvitationTypeId { get; set; }
        public int InvitationStatusId { get; set; }
        public InvitationStatusModel InvitationStatus { get; set; }

        public int FavouriteSupplierListId { get; set; }

        public string FavouriteName { get; set; }
        public bool IsFav { get; set; }
        public string SadadNumber { get; set; }
        public string InvitationStatusName { get; set; }

    }
}
