using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QuantityTableForOpeningModel
    {
        public int TableQuantityId { get; set; }
        public string TableQuantityName { get; set; }
        public decimal AdjustedTotalPrice { get; set; }
        public decimal AdjustedTotalDiscount { get; set; }
        public decimal AdjustedTotalVat { get; set; }
        public decimal AdjustedFinalPrice { get; set; }
        public bool? IsActive { get; set; }
        public List<QuantityTableItemForOpeningModel> QuantityTableItemForOpeningModel { get; set; }
    }
}
