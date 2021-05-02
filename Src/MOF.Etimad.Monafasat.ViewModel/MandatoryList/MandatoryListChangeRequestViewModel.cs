using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MandatoryListChangeRequestViewModel
    {
        public int Id { get; set; }

        public string DivisionNameAr { get; set; }

        public string DivisionNameEn { get; set; }

        public string DivisionCode { get; set; }

        public string RejectionReason { get; set; }

        public int StatusId { get; set; }

        public List<MandatoryListProductViewModel> Products { get; set; }

    }
}
