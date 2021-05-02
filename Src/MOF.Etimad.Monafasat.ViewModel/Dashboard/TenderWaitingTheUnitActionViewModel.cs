using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderWaitingTheUnitActionViewModel
    {
        public string TenderIdString { get; set; }
        [Display(Name = "إسم المنافسة")]
        public string TenderName { get; set; }
        [Display(Name = "رقم المنافسة")]
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        [Display(Name = "حالة المنافسة")]
        public string Status { get; set; }
        public string RejectionReasonFromUnitSpecialist { get; set; }
        public string RejectionReasonFromUnitManager { get; set; }
        public string OperationType { get; set; }
        public int? TenderUnitStatusId { get; set; }
        public int? TenderStatusId { get; set; }
        public bool CanUnitDoAnyAction { get; set; }
        public string RequestId { get; set; }
    }
}
