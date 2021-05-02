using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierTenderQuantityTableDTO
    {
        public long TableId { get; set; }
        public long TableNumber { get; set; }
        public int TenderId { get; set; }
        public string TableName { get; set; }
        public List<SupplierTenderQuantityItemDTO> SupplierQItems { set; get; }
    }
}