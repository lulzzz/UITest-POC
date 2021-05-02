using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationAgencyPageModel
    {
        public NegotiationAgencyFirstStageEditModel NegotiationFirstStageModel { get; set; }
        public List<NegotiationOfferModel> NegotiationOfferModels { get; set; } = new List<NegotiationOfferModel>();
        public NegotiationAgencyTenderModel NegotiationTenderModel { get; set; }
        public CreateNegotiationFirstStageDataModel CreateNegotiationFirstStageData { get; set; }
    }
}
