using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.Core
{
    public class BranchCommitteeSearchCriteria : SearchCriteria
    {


        #region Fields ========


        #region Basic Search Criteria

        public int CommitteeId { get; private set; }

        public int BranchId { get; private set; }

        #endregion

        public bool? IsActive { get; private set; }


        #endregion
    }
}
