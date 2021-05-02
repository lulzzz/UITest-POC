using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferDetailsModel
    {
        public int Id { get; set; }
        [Display(Name = "رقم المورد")]
        public int SupplierId { get; set; }
        [Display(Name = "رقم المنافسة")]
        public int TenderId { get; set; }
        [Display(Name = "حالة العرض")]
        public int OfferStatusId { get; set; }
        [Display(Name = "حالة العرض")]
        public int OfferStatusName { get; set; }
        [Required]
        [Display(Name = "طريقة استلام العرض")]
        public bool? IsManuallyApplied { get; set; }
        public bool? IsActive { get; set; }

        [Display(Name = "جداول الكميات")]
        public BasicTenderModel Tender { get; set; } = new BasicTenderModel();

        public SupplierModel Supplier { get; set; } = new SupplierModel();
        [Display(Name = "جداول الكميات")]
        public IList<SupplierQuantityTableModel> QuantitiesTables { get; set; } = new List<SupplierQuantityTableModel>();

        [Display(Name = "جداول الكميات")]
        public IList<SupplierAttachmentModel> Attachments { get; set; } = new List<SupplierAttachmentModel>();
    }
}
