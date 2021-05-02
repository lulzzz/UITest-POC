using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PreQualificationPartialDetailsModel
    {
        public int Id { get; set; }
        public string QualificationName { get; set; }
        public string IdEncrypted { get; set; }
        public string Activity { get; set; }
        public bool? InsideKSA { get; set; }
        public string CategoryName { get; set; }
        public string CountryName { get; set; }
        public string AreaName { get; set; }

        public List<string> TenderActivities { get; set; }
        public List<string> MaintenanceOperationActions { get; set; }
        public List<string> EstablishingActions { get; set; }

    }
}
