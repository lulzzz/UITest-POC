using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderAreasModel
    {
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public bool? InsideKSA { set; get; }
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public List<int> TenderAreaIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        public List<LookupModel> Areas { get; set; }

        public List<int> TenderCountriesIDs { set; get; } = new List<int>();

        public List<int> TenderCountryIDs { set; get; } = new List<int>();


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]
        public List<CountryModel> Countries { get; set; }


    }
}