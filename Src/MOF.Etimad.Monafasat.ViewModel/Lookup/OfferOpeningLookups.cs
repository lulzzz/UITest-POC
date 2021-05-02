using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferOpeningLookups
    {
        public List<MaintenanceRunningWorkModel> RunningWorks { set; get; }
        public List<ConstructionWorkModel> ConstructionWorks { set; get; }
        public List<BankModel> Banks { set; get; }
    }
}