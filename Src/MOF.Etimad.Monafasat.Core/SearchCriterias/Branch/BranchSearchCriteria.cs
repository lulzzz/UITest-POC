using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.Core
{
    public class BranchSearchCriteria : SearchCriteria
    {


        #region Fields ========


        #region Basic Search Criteria

        public int BranchId { get; private set; }
        public string BranchName { get; private set; }

        #endregion

        public bool? IsActive { get; private set; }


        #endregion
    }
}
