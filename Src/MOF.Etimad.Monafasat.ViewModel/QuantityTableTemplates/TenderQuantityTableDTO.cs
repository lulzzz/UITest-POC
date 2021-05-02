using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderQuantityTableDTO
    {
        public long TableId { get; set; }
        public long TableNumber { get; set; }
        public int TenderId { get; set; }
        public string TableName { get; set; }
        public List<TenderQuantityItemDTO> QuantityItems { set; get; }
    }
}