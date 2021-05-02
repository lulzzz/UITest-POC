using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QuantityTableSearchCretriaModel : SearchCriteria
    {
        public QuantityTableSearchCretriaModel()
        {
            PageSize = 6;
        }
        public long QuantityTableId { get; set; }
        public int TenderId { get; set; }
        public int OfferId { get; set; }
        public int negotiationId { get; set; }
        public int CellsCount { get; set; }
        public int FormId { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsTableDeleted { get; set; }
        public bool IsNewTable { get; set; }
    }
}
