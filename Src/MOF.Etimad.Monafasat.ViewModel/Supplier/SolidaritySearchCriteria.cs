using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SolidaritySearchCriteria : SearchCriteria
    {
        public string offerIdString { get; set; }
        public string CommericalRegisterNo { get; set; }
        public string CommericalRegisterName { get; set; }
        public int? CivilRegisterNo { get; set; }
        public int? MainActivitiesId { get; set; }
        public int? SubActivitesId { get; set; }
        public int? ActivitesLevelId { get; set; }
        public string EMail { get; set; }
        public string ActivityDescription { get; set; }
        public string CityName { get; set; }
        public string CurrentSupplierCR { get; set; }
    }
    public class SolidarityUnregisteredSearchCriteria : SearchCriteria
    {
        public string offerIdString { get; set; }

    }
}
