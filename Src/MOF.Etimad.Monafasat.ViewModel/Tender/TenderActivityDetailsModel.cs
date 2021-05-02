using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderActivityDetailsModel
    {
        public bool ShowGeneralOnly { get; set; }
        public List<int> ListOfSections { get; set; }
        public List<int?> TemplateIds { get; set; }
    }
}
