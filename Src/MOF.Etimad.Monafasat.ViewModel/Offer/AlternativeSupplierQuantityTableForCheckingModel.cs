using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AlternativeSupplierQuantityTableForCheckingModel
    {
        public int TableQuantityId { get; set; }

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
        public List<AlternativeSupplierQuantityTableItemForCheckingModel> AlternativeSupplierQuantityTableItemForCheckingModels { get; set; }
    }
}
