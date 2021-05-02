using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SecondStageNegotiationViewModel
    {
        public string NegotiationId { get; set; }
        public string OfferIdString { get; set; }
        public string TenderIdString { get; set; }
        public List<NegotiationQuantityTableModel> NegotiationQuantityTableModels { get; set; } = new List<NegotiationQuantityTableModel>();

        public Dictionary<string, string> newNegotiationQItems { get; set; }
    }
}
