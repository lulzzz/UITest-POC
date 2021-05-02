using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{

    public class TenderOffersStepModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }


        public int TenderTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public int? OffersOpeningAddressId { get; set; }
        //[Display(Name = "عنوان استلام كراسة الشروط")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBookletAddress")]
        public int? ConditionsBookletDeliveryAddressId { get; set; }
        //[Display(Name = "مكان فتح العروض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public string OffersOpeningAddress { get; set; }
        //[Display(Name = "عنوان استلام كراسة الشروط")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBookletAddress")]
        public string ConditionsBookletDeliveryAddress { get; set; }
        [StringLength(1024)]
        //[Display(Name = "بحاجة لتسليم عينات للمورد")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        public bool? SupplierNeedSample { get; set; }
        [StringLength(1024)]
        //[Display(Name = "عنوان تسليم عينات للمورد")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        public string SamplesDeliveryAddress { get; set; }


        //[Display(Name = "اخر موعد لإستلام استفسارات الموردين وإضافة الملحقات")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }

        //[Display(Name = "آخر موعد لتقديم العروض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }

        //[Display(Name = "آخر وقت لتقديم العروض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }

        //[Display(Name = "تاريخ فتح العروض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        public DateTime? OffersOpeningDate { get; set; }
        //[Display(Name = "وقت فتح العروض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningTime")]
        public string OffersOpeningTime { get; set; }
        public List<AddressModel> OfferPresentationPlaceList { get; set; }
        public List<AddressModel> OffersOpeningAddressList { get; set; }
        public List<AddressModel> ConditionsBookletDeliveryAddressList { get; set; }
        public int TenderStatusId { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        public bool? IsPurchased { get; set; }
        public string LastEnqueriesDateString { get; set; }
        public string OffersOpeningDateString { get; set; }
        public string LastOfferPresentationDateString { get; set; }
        public string LastEnqueriesDateHijriString { get; set; }
        public string OffersOpeningDateHijriString { get; set; }
        public string LastOfferPresentationDateHijriString { get; set; }
        public int? InvitationStatusId { get; set; }
        public List<VacationsDateModel> Vacations { get; set; }
        public TenderRevisionDateModel TenderRevisionDate { get; set; }
        public List<int> TenderChangeRequestIds { get; set; }
        public List<int> ChangeRequestStatusIds { get; set; }
        //public List<int> ChangeRequestTypeIds { get; set; }
        public int ChangeRequestTypeId { get; set; }

        public string RejectionReason { get; set; }


    }
}