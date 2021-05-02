using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class OrganizationModel
    {
        public OrganizationModel()
        {
        }
        public OrganizationModel(List<IntegrationBranchModel> branches)
        {
            Branches = branches;
        }
        public List<IntegrationBranchModel> Branches { get; set; }
    }
}
