using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.Linq;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MandatoryListIndexViewModel
    {

        public string DivisionNameAr { get; set; }

        public string DivisionNameEn { get; set; }

        public string DivisionCode { get; set; }

        public string Status { get; set; }
        public int StatusId { get; set; }
        public string EncryptedStatusId { get; set; }

        public string EncryptedId { get; set; }

        public int ProductsCount { get; set; }

        public List<MandatoryListChangeRequestViewModel> ChangeRequests { get; set; } = new List<MandatoryListChangeRequestViewModel>();

        public bool IsThereActiveChangeRequest => ChangeRequests.Any(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.WaitingApproval);

        public bool IsThereRejectedChangeRequest => ChangeRequests.Any(a => a.StatusId == (int)Enums.MandatoryListChangeRequestStatus.Rejected);

    }
}
