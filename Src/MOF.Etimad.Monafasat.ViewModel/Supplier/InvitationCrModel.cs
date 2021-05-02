using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class InvitationCrModel
    {

        [Display(Name = "رقم السجل التجارى")]
        public string CrNumber { get; set; }
        [Display(Name = "إسم المورد")]
        public string CrName { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsOwner { get; set; }
        public bool IsChecked { get; set; }
        public int TenderId { get; set; }
        public int StatusId { get; set; }
        public int InvitationId { get; set; }

        public int invitationCount { get; set; }

        public String Email { get; set; }
        public string Mobile { get; set; }
    }
    public class SolidarityInvitedRegisteredSupplierModel
    {

        [Display(Name = "رقم السجل التجارى")]
        public string CrNumber { get; set; }
        [Display(Name = "إسم المورد")]
        public string CrName { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsOwner { get; set; }
        public bool IsChecked { get; set; }
        public int TenderId { get; set; }
        public int StatusId { get; set; }
        public int InvitationId { get; set; }
        public string OfferIdString { get; set; }

        public string StatusName { get; set; }
        public string SolidarityIdString { get; set; }
    }
    public class SolidarityInvitedUnRegisteredSupplierModel
    {

        public string CrName { get; set; }
        public String Email { get; set; }
        public string Mobile { get; set; }
        public string CR { get; set; }
        public string Licience { get; set; }
        public string Description { get; set; }
        public string OfferIdString { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; }
        public string SolidarityIdString { get; set; }
    }
}
