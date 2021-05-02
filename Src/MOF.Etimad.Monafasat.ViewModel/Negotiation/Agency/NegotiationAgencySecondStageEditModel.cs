using Microsoft.AspNetCore.Mvc.Rendering;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationAgencySecondStageEditModel
    {
        public string RejectionReason { get; set; }
        public string TenderIdString { get; set; }
        public string StatusName { get; set; }
        public Enums.enNegotiationStatus Status { get; set; }
        public string NegotiationId { get; set; }
        public bool IsSend { get; set; }
        public bool IsAllSuppliers { get; set; }
        public string NegotiationReasonName { get; set; }
        [Display(Name = "NegotiationReason", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Required(ErrorMessageResourceName = "RequiredNegotiationReason", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]

        public int NegotiationReasonId { get; set; }
        //  public SecondStageNegotiationModel SecondStageNegotiationModel { get; set; }

        [Display(ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs), Name = "SuppliersLists")]
        public List<int> SupplierIDs { set; get; } = new List<int>();


        public List<SelectListItem> ReductionReasons = new List<SelectListItem>();
        public List<SelectListItem> SupplierList = new List<SelectListItem>();
        public List<SameOrderSupplier> SameOrderSuppliers = new List<SameOrderSupplier>();
        public List<NegotiationQuantityTableModel> NegotiationQuantityTableModels = new List<NegotiationQuantityTableModel>();
        public List<DiffrentOrderSupplier> DiffrentOrderSuppliers = new List<DiffrentOrderSupplier>();


    }
    public class SecondStageNegotiationModel
    {
        public int NegotiationReasonId { get; set; }
        public string RejectionReason { get; set; }
        public string TenderIdString { get; set; }
        public string NegotiationId { get; set; }
        public string NegotiationReasonName { get; set; }

    }
    public class SameOrderSupplier { }
    public class DiffrentOrderSupplier { }
    public class NewRequestModel
    {
        public string TenderIdString { get; set; }
    }

}
