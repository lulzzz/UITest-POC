using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ActivityModel
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public virtual List<ActivityModel> SubActivities { get; set; }
    }
}
