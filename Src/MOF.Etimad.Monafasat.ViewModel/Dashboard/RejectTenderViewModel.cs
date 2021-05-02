using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class RejectTenderViewModel
    {
        public string TenderIdString { get; set; }
        [Display(Name = "إسم المنافسة")]
        public string TenderName { get; set; }
        [Display(Name = "رقم المنافسة")]
        public string TenderNumber { get; set; }
        [Display(Name = "حالة المنافسة")]
        public string TenderStatusName { get; set; }
        [Display(Name = "سبب الرفض")]
        public string RejectionReason { get; set; }
        public bool? IsNewNegotiation { get; set; }
        public string ChangeType { get; set; }
        public string ReferenceNumber { get; set; }
        public int TenderStatusId { get; set; }
        public int AnnouncementStatusId { get; set; }
        public int TenderTypeId { get; set; }
        public string TenderStatusIdString { get; set; }
        public int? ChangeRequestTypeId { get; set; }

        public int ChangeRequestStatuesId { get; set; }
        public int? AgencyRequestTypeId { get; set; }

        public Enums.OperationsNeedsApproval operationType { get; set; }
        public string ItemIdString { get; set; }

        public string AgencyRequestIdString { get; set; }




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
        public string Duration { get; set; }
        public int? DurationInDays { get; set; }
        public int? DurationInMonths { get; set; }
        public int? DurationInYears { get; set; }
        public string EncyptedPrePlanningId { get; set; }
        public int YearQuarterId { get; set; }
        public string YearQuarterName { get; set; }
        public int? PrePlaningStatusId { get; set; }

        public string DeleteRejectionReason { get; set; }


        public bool? IsDeleteRequested { get; set; }
    }
}
