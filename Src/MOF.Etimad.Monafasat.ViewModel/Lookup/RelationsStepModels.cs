using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class RelationsStepModels
    {
        public List<LookupModel> Areas { set; get; }
        public List<LookupModel> Countries { get; set; }
        public List<MaintenanceRunningWorkModel> RunningWorks { set; get; }
        public List<ConstructionWorkModel> ConstructionWorks { set; get; }
        public List<ActivityModel> Activities { set; get; }
        public List<int> ActivitiesWithYears { get; set; }
    }
}