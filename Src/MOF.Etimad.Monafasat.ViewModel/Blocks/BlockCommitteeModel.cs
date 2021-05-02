using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BlockCommitteeModel : SearchCriteria
    {
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string CommercialRegistrationNo { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string CommercialSupplierName { get; set; }

    }
}
