using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MandatoryListViewModel
    {
        public string Id { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonNameAr), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public string DivisionNameAr { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonNameEn), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public string DivisionNameEn { get; set; }

        [Display(Name = nameof(Resources.MandatoryListResources.DisplayInputs.DivisonCode), ResourceType = typeof(Resources.MandatoryListResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        public string DivisionCode { get; set; }

        public int StatusId { get; set; }

        public string RejectionReason { get; set; }

        public MandatoryListChangeRequestViewModel ChangeRequest => ChangeRequests.Any(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.Rejected) ?
            ChangeRequests.LastOrDefault(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.Rejected)
            : ChangeRequests.LastOrDefault(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval);

        public List<MandatoryListChangeRequestViewModel> ChangeRequests { get; set; } = new List<MandatoryListChangeRequestViewModel>();

        public List<MandatoryListProductViewModel> Products { get; set; }
    }
}
