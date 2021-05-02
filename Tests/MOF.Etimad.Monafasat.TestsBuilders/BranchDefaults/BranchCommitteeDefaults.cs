using MOF.Etimad.Monafasat.Core.Entities;

namespace MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults
{
    public class BranchCommitteeDefaults
    {
        public readonly int _branchId = 1;
        public readonly int _committeeId = 1;
        public BranchCommittee ReturnBranchCommitteeDefaults()
        {
            BranchCommittee generalBranchCommittee = new BranchCommittee(_branchId, _committeeId);
            return generalBranchCommittee;
        }
    }
}
