using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class QuantityTableDTO
    {
        public string QuantityTableIdString { get; set; }
        public string TableName { get; set; }
        public decimal TotalAmount { get; set; }
        public int QuantityItemsCount { get; set; }
        public string intializeDateString { get; set; }
        public string Status { get; set; }
        public List<QuantityItemModel> quantityTableItems { get; set; }
    }
}
