using MOF.Etimad.Monafasat.Resources.BlockResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AgencyExceptedModel
    {
        public string AgencyIdString { get; set; }
        public string NameArabic { get; set; }
        public string PurchaseMethodString { get; set; }
        public string AgencyCode { get; set; }
        public bool IsVro { get; set; }
        public bool IsRelated { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<PurchaseMethodsModel> TenderTypes { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "SelectPurchaseMethod")]
        [Display(Name = "PurchaseMethod", ResourceType = typeof(DisplayInputs))]
        public List<int> PurchaseMethods { get; set; } = new List<int>();
        public bool IsOldSystem { get; set; }
        public string MobileNumber { get; set; }

        public bool IsRelatedToTender { get; set; } = false;
    }
}
