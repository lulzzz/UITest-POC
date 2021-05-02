
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderSearchCriteria : SearchCriteria
    {

        #region Fields ========

        #region Basic Search Criteria

        public int TenderId { get; set; }

        public int TenderTypeId { get; set; } // General Tender - Direct Purchase
        public List<int> TenderTypeIds { get; set; } = new List<int>();

        public string TenderTypeName { get; set; }
        public string InvitationTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public string TenderActivityName { set; get; }
        public string TenderAreaName { set; get; }
        public List<int> TenderAreasIds
        {
            get
            {
                if (TenderAreasIdString != null)
                {
                    return new List<int>(Array.ConvertAll(TenderAreasIdString.Split(','), int.Parse));
                }
                else
                {
                    return null;
                }
            }
        }
        public int InvitationTypeId { get; set; } // [Required] In Case Of Direct purchase (All pageNumbers / Special Invitation)

        public decimal? ConditionsBookletPrice { get; set; } //[Required] In Case Of General Tender (قيمة كراسة الشروط)

        #endregion

        #region Advanced Search Criteria

        public string TenderName { get; set; }

        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }

        // public DateTime TenderPublishDate { get; set; } //تاريخ نشر المنافسة 

        public DateTime? LastOfferPresentationDate { get; set; } //اخر موعد لتقديم العروض


        public DateTime? OffersOpeningDate { get; set; } //Required if there is offer opening Place  (تاريخ فتح العروض)

        //public se OffersOpeningTime { get; set; } //Required if there is offer opening Place
        //public DateTime? TenderPublishDate { get; set; } //CreatedAt

        #endregion

        public bool OnlyGetFavouriteTenders { get; set; }

        public string OfferPresentationPlace { get; set; } // [Required] In Case Of Manual and automatic by system in case of automatic

        public string OffersOpeningAddress { get; set; } //مكان فتح العروض

        public string ConditionsBookletDeliveryAddress { get; set; }

        public DateTime? LastEnqueriesDate { get; set; } //اخر موعد لإستلام استفسارات الموردين وإضافة الملحقات

        // public TimeSpan LastOfferPresentationTime { get; set; } //اخر موعد لتقديم العروض

        public bool? InsideKSA { get; set; } //مكان التنفيذ
        public bool? IsVro { get; set; }

        public int? TenderStatusId { get; set; }

        public bool? IsActive { get; set; }

        public int NotInStatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? SubmitionDate { get; set; }

        public string TenderAreasIdString { get; set; }

        public string AgencyCode { get; set; }
        public int? BranchId { get; set; }

        public string SelectedAgencyCode { get; set; }
        //   public int? SelectedBranchId { get; set; }
        public int? SelectedCommitteeId { get; set; }
        public int? CommitteeTypeId { get; set; }
        public int CommitteeId { get; set; }

        public string cr { get; set; }

        public string TenderStatusIdsString { get; set; }

        public List<int> TenderStatusIds { get; set; } = new List<int>();

        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int? UserId { get; set; }
        public string FinancialYear { get; set; }
        public string ConditionaBookletRange { get; set; }
        public string PublishDate { get; set; }
        public int? TenderCategory { get; set; }
        public int? TenderActivityId { get; set; }
        public int? TenderSubActivityId { get; set; }
        public int? EnquiryReplyStatus { get; set; }

        public DateTime? FromLastOfferPresentationDate { get; set; } //اخر موعد لتقديم العروض
        public DateTime? ToLastOfferPresentationDate { get; set; } //اخر موعد لتقديم العروض
        public string FromLastOfferPresentationDateString { get; set; } //اخر موعد لتقديم العروض
        public string ToLastOfferPresentationDateString { get; set; } //اخر موعد لتقديم العروض

        public string UserRole { get; set; }

        public string crNumber { get; set; }
        #endregion
    }
}
