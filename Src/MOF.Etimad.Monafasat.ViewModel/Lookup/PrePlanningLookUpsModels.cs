using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PrePlanningLookUpsModels
    {
        public List<LookupModel> YearQuarters { set; get; }
        public List<LookupModel> Statuses { set; get; }
        public List<ActivityModel> ProjectTypes { set; get; }
        public List<SelectListItem> ProjectTypesList { set; get; }
        public List<GovAgencyModel> Agecies { set; get; }
        public List<LookupModel> Areas { set; get; }
        public List<CountryModel> Countries { set; get; }
    }
}