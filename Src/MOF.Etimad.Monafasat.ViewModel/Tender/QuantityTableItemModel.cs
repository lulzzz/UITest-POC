using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QuantityTableItemModel
    {
        public int QuantityTableItemId { get; set; }
        //[Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ItemName")]
        [StringLength(1024)]
        //[Display(Name = "إسم الصنف")]
        public string Name { get; set; }
        //[Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Quantity")]
        public int Quantity { get; set; }
        //[Required]
        [StringLength(1024)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Unit")]
        public string Unit { get; set; }
        //[Display(Name = "التفاصيل")]
        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "Details")]
        public string Details { get; set; }
        public string ItemAttachmentId { get; set; }
        public string ItemAttachmentName { get; set; }
        public int QuantityTableID { set; get; }
        public bool? IsActive { get; set; }
        public int ItemChangeStatusId { get; set; }
    }
}
