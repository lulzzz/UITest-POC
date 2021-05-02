
using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderSearchCriteriaModel : SearchCriteria
    {

        #region Fields ========

        public bool IsSearch { get; set; }

        #region Basic Search Criteria

        public int TenderId { get; set; }

        public int TenderTypeId { get; set; } // General Tender - Direct Purchase

        public int InvitationTypeId { get; set; } // [Required] In Case Of Direct purchase (All Suppliers / Special Invitation)

        public List<TenderAreaModel> TenderAreas { set; get; }

        public decimal? ConditionsBookletPrice { get; set; } //[Required] In Case Of General Tender (قيمة كراسة الشروط)

        #endregion


        #region Advanced Search Criteria

        [StringLength(1024)]
        public string TenderName { get; set; }

        [StringLength(100)]
        public string TenderNumber { get; set; }
        [StringLength(100)]
        public string ReferenceNumber { get; set; }

        public DateTime TenderPublishDate { get; set; } //تاريخ نشر المنافسة 

        public DateTime LastOfferPresentationDate { get; set; } //اخر موعد لتقديم العروض


        public DateTime OffersOpeningDate { get; set; } //Required if there is offer opening Place  (تاريخ فتح العروض)

        public TimeSpan OffersOpeningTime { get; set; } //Required if there is offer opening Place

        #endregion


        [StringLength(1024)]
        public string OfferPresentationPlace { get; set; } // [Required] In Case Of Manual and automatic by system in case of automatic

        [StringLength(1024)]
        public string OffersOpeningAddress { get; set; } //مكان فتح العروض

        [StringLength(1024)]
        public string ConditionsBookletDeliveryAddress { get; set; } // عنوان استلام كراسة الشروط  والمواصفات يدويا
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastEnqueriesDate { get; set; } //اخر موعد لإستلام استفسارات الموردين وإضافة الملحقات
        public string LastEnqueriesDateString { get; set; }
        public TimeSpan LastOfferPresentationTime { get; set; } //اخر موعد لتقديم العروض

        public bool InsideKSA { get; set; } //مكان التنفيذ
        public bool? IsVro { get; set; }

        public int StatusId { get; set; }

        public TenderStatusModel Status { get; set; }

        public int NotInStatusId { get; set; }

        public List<TenderActivityModel> TenderActivities { set; get; }

        public bool? IsActive { get; set; }

        public int? TenderStatusId { get; set; }
        public string TenderStatusName { get; set; }

        public string TenderActivityName { set; get; }
        public string TenderAreaName { set; get; }
        public string TenderTypeName { get; set; }
        public string InvitationTypeName { get; set; }
        public int? CommitteeTypeId { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? SubmitionDate { get; set; }

        public List<int> TenderAreasIds { get; set; }

        public string TenderAreasIdString { get; set; }

        public string AgencyCode { get; set; }
        public int? BranchId { get; set; }
        public int CommitteeId { get; set; }
        public int SelectedAgencyCode { get; set; }
        public string cr { get; set; }

        public string TenderStatusIdsString { get; set; }

        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }

        public string UpdatedBy { get; set; }
        public int UserId { get; set; }

        public string FinancialYear { get; set; }
        public string TenderTypeString { get; set; }
        public string ConditionaBookletRange { get; set; }
        public string PublishDate { get; set; }
        public int? TenderCategory { get; set; }
        public int? TenderActivityId { get; set; }
        public int? TenderSubActivityId { get; set; }
        public int? EnquiryReplyStatus { get; set; }
        public DateTime? FromLastOfferPresentationDate { get; set; } //اخر موعد لتقديم العروض
        public DateTime? ToLastOfferPresentationDate { get; set; } //اخر موعد لتقديم العروض
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime FromLastOfferPresentationDateString { get; set; } //اخر موعد لتقديم العروض
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime ToLastOfferPresentationDateString { get; set; } //اخر موعد لتقديم العروض
        public bool OnlyGetFavouriteTenders { get; set; }
        #endregion
    }
}

