using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PrePlanningSearchCriteria : SearchCriteria
    {
        #region Constructors ========

        public PrePlanningSearchCriteria() { }

        #endregion


        [MaxLength(200)]
        public string ProjectName { get; set; }
        public int ProjectTypeId { get; set; }
        public int StatusId { get; set; }
        //public int ProjectNatureId { get; set; }
        public int OrganizationBranchId { get; set; }
        public string AgencyCode { get; set; }
        public int YearQuarterId { get; set; }

    }
}
