using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommitteeDomainService : ICommitteeDomainService
    {
        private readonly ICommitteeQueries _committeeQueries;
        private readonly ITenderQueries _tenderQueries;
        private readonly IEnquiryQueries _enquiryQueries;

        public CommitteeDomainService(ICommitteeQueries committeeQueries, ITenderQueries tenderQueries, IEnquiryQueries enquiryQueries)
        {
            _committeeQueries = committeeQueries;
            _tenderQueries = tenderQueries;
            _enquiryQueries = enquiryQueries;
        }

        public async Task<bool> CheckNameExist(string name, string agentId, int id = 0)
        {
            var result = await _committeeQueries.GetByName(name, agentId, id);
            if (result)
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.CommitteeNameExist);
            return result;
        }
        public async Task<bool> CheckNameExistbyType(string name, string agentId, int typeId = 0)
        {
            var result = await _committeeQueries.GetByNameandTypeId(name, agentId, typeId);
            if (result)
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.CommitteeNameExist);
            return result;
        }
        public async Task<bool> CheckUserExist(int userId, int committeeId)
        {
            var result = await _committeeQueries.GetAssignedUserByIdAndCommittee(userId, committeeId);
            if (result != null)
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.UserAlreadyExist);
            return true;
        }

        public async Task<bool> CheckIfHasTenders(int committeeId)
        {
            var result = await _tenderQueries.GetHasTendersByCommittee(committeeId);
            if (result)
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.CommitteeUsed);
            return result;
        }

        public async Task<bool> CheckIfHasUsers(int committeeId)
        {
            var result = await _committeeQueries.HasAssignedUserByCommittee(committeeId);
            if (result)
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.CommitteeUsed);
            return result;
        }
        public async Task<bool> CheckIfHasEnqiryReplies(int committeeId)
        {
            var result = await _enquiryQueries.GetHasEnquiryRepliesByCommittee(committeeId);
            if (result)
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.CommitteeUsed);
            return result;
        }
    }
}
