using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat
{
    public class OffersCompareViewModel
    {
        public SecondStageNegotiationViewModel secondStageNegotiationViewModel { get; set; }
        public bool isSaved { get; set; }
        public List<OffersCompareGridViewModel> OldOffersCompareGrid { get; set; } = new List<OffersCompareGridViewModel>();
        public List<OffersCompareGridViewModel> NewOffersCompareGrid { get; set; } = new List<OffersCompareGridViewModel>();
    }


    public class NegotiationOffersModel
    {
        public bool isSameOrder { get; set; }
        public string Message { get; set; }
        public List<OffersCompareGridViewModel> NewOffersCompareGrid { get; set; }
    }


}
