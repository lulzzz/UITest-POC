using MOF.Etimad.Monafasat.SharedKernal;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CommitteeSearchCriteria : SearchCriteria
    {
        #region Constructors ========

        public CommitteeSearchCriteria() { }
        public CommitteeSearchCriteria(string committeeName, bool? isActive = true)
        {
            CommitteeName = committeeName;
        }

        #endregion

        #region Fields ========


        #region Basic Search Criteria

        [MaxLength(200)]
        public string CommitteeName { get; private set; }

        [Required]
        public int CommitteeTypeId { get; private set; }

        [Required]
        public int OrganizationBranchId { get; private set; }

        #endregion

        public bool? IsActive { get; private set; }
        public string AgencyCode { get; private set; }


        #endregion
    }
}
