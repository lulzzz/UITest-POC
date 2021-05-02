using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ConstructionWorkModel
    {
        public int ConstructionWorkId { get; set; }
        public string Name { get; set; }

        public virtual List<ConstructionWorkModel> SubWorks { get; set; }
    }
}
