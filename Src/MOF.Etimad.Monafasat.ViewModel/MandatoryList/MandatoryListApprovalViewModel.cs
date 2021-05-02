using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MandatoryListApprovalViewModel
    {
        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonNameAr), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        public string DivisionNameAr { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonNameEn), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        public string DivisionNameEn { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonCode), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        public string DivisionCode { get; set; }
        public int StatusId { get; set; }
        public string EncryptedStatusId { get; set; }
        public string EncryptedId { get; set; }
        public string RejectionReason { get; set; }

        public List<MandatoryListProductViewModel> Products { get; set; } = new List<MandatoryListProductViewModel>();
    }
}
