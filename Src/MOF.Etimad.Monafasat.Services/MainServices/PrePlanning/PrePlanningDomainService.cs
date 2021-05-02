using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class PrePlanningDomainService : IPrePlanningDomainService
    {
        private readonly IPrePlanningQueries _prePlanningQueries;

        public PrePlanningDomainService(IPrePlanningQueries prePlanningQueries/*, ITenderQueries tenderQueries, IEnquiryQueries enquiryQueries*/)
        {
            _prePlanningQueries = prePlanningQueries;
        }

        public async Task<bool> CheckNameExist(string name, int branchId, int quarterId, string agentId, int id)
        {
            var result = await _prePlanningQueries.GetByName(name, branchId, agentId, id);
            if (result)
                throw new BusinessRuleException(Resources.PrePlanningResources.ErrorMessages.prePlanningNameExist);
            return result;
        }
    }
}
