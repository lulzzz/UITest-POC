using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ProcessNeedsActionViewModel
    {

        public bool? IsNewNegotiation { get; set; }
        [Display(Name = "إسم المنافسة")]
        public string TenderName { get; set; }
        [Display(Name = "رقم المنافسة")]
        public string TenderNumber { get; set; }
        public string TenderReferenceNumber { get; set; }
        [Display(Name = "آخر موعد لاستلام استفسارات الموردين")]
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(Name = "آخر موعد لاستلام العروض")]
        public DateTime? LastOfferPresentationDate { get; set; }
        [Display(Name = "موعد فتح العروض")]
        public DateTime? OffersOpeningDate { get; set; }
        [Display(Name = "نوع الاعتماد")]
        public string AcceptanceTypeName { get; set; }

        public string ChangeRequestIdString { get; set; }
        public string TenderIdString { get; set; }
        public string TenderStatusIdString { get; set; }
        public int TenderStatusId { get; set; }
        public bool IsAnnouncement { get; set; }
        public int TenderTypeId { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public string RejectionReason { get; set; }

        public int OperationsNeedsApprovalType { get; set; }

        public string ExtendOffersValidityIdString { get; set; }

        public DateTime CreatedAt { get; set; }

        //PrePlaning/////////             
        public bool? InsideKSA { get; set; }
        public string InsideKSAString
        {
            get
            {
                if (InsideKSA.HasValue && InsideKSA.Value)
                {
                    return Resources.TenderResources.DisplayInputs.InsideKSA;
                }
                else
                    return Resources.TenderResources.DisplayInputs.OutsideKSA;
            }
        }
        public string ProjectName { get; set; }
        public int? DurationInDays { get; set; }
        public int? DurationInMonths { get; set; }
        public int? DurationInYears { get; set; }
        public string Duration { get; set; }
        public string EncyptedPrePlanningId { get; set; }
        public int YearQuarterId { get; set; }
        public string YearQuarterName { get; set; }
        public int? PrePlaningStatusId { get; set; }
        public bool IsDeleteRequested { get; set; }
        //PrePlaning/////////
        /////////Supplier Block////////////// 
        public string CommercialRegistrationNo { get; set; }
        public string CommercialSupplierName { get; set; }
        public string BlockReason { get; set; }
        public bool CanLevelTwoApprovementDoAction { get; set; }
        public decimal? EstimatedValue { get; set; }
        public int BranchId { get; set; }
        ////////////////////////////////////


    }
}
