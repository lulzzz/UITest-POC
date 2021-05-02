using System.Collections.Generic;
using System.ComponentModel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierQuantityTableModel
    {
        public int TableQuantityId { get; set; }

        [DisplayName("اسم الجدول")]
        public string TableQuantityName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal OpeningTotalPrice { get; set; }
        public decimal OpeningDiscount { get; set; }
        public decimal OpeningVat { get; set; }

        public decimal TotalPriceAfterDiscount { get; set; }
        public decimal TotalAlternativePriceAfterDiscount { get; set; }
        public decimal TotalAlternativePrice { get; set; }
        public int OfferId { get; set; }
        public int? TenderQuantityTableId { get; set; }
        public decimal TotalAlternativeVAT { get; set; }
        public decimal AlternativeDiscount { get; set; }
        public bool? IsActive { get; set; }
        public List<SupplierQuantityTableItemModel> QuantityTableItem { get; set; }


    }
}
