using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierQuantityTableItemModel
    {
        public long Id { get; set; }

        [DisplayName("رقم الجدول")]
        [Required]
        public int SupplierTableQuantityId { get; set; }

        [DisplayName("رقم المنافسة")]
        [Required]
        public int TenderQuantityTableItemId { get; set; }

        [DisplayName("سعر الوحدة")]
        public decimal Price { get; set; }

        [DisplayName("الخصم للوحدة")]
        [Required]
        public decimal Discount { get; set; }
        [Required]
        public string ItemAttachmentId { get; set; }
        public string ItemAttachmentName { get; set; }
        public bool hasAlternative { get; set; }
        public bool isAlternative { get; set; }

        [Required]
        public decimal VAT { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Unit { get; set; }

        public string OriginalItemId { get; set; }
        [DisplayName("المجموع")]
        public decimal Total { get; set; }

        [DisplayName("المجموع بعد الخصم")]
        public decimal DiscountedTotal { get; set; }
        public QuantityTableItemModel TenderQuantityTableItem { get; set; }
    }
}
