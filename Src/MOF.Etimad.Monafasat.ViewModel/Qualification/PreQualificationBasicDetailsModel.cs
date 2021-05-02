

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PreQualificationBasicDetailsModel
    {
        public string QualificationName { get; set; }
        public bool? InsideKSA { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderActivity")]
        public List<string> TenderActivities { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EstablishingActions")]
        public List<string> TenderConstructionWorks { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<string> TenderMaintenanceRunnigWorks { get; set; }
        public string TenderIdString { get; set; }
        public int TenderStatusId { get; set; }
        public int RemainingDays { get; set; }
        public int RemainingHours { get; set; }
        public int RemainingMins { get; set; }
        public string RejectionReason { get; set; }
        public int unCheckedSupplierDocuments { get; set; }
        public bool hasPendingCancelRequest { get; set; }
        public decimal SupplierFinalResult { get; set; }
        public decimal PointToPass { get; set; }
        public string Createby { get; set; }

    }
}
