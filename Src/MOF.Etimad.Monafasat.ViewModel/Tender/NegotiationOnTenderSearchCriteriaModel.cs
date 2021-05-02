using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationOnTenderSearchCriteriaModel : SearchCriteria
    {
        [StringLength(1024)]
        public string TenderName { get; set; }

        [StringLength(100)]
        public string TenderNumber { get; set; }
        [StringLength(100)]
        public string ReferenceNumber { get; set; }
        public int TenderTypeId { get; set; }
        public string AgencyCode { get; set; }
    }
}

