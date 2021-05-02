using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.Core
{
    public class AssignUserCommitteeSearchCriteria : SearchCriteria
    {


        #region Fields ========


        #region Basic Search Criteria

        public int CommitteeId { get; private set; }

        public int UserId { get; private set; }

        #endregion

        public bool? IsActive { get; private set; }


        #endregion
    }
}
