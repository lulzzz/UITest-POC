using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class EnquiryDomainService : IEnquiryDomainService
    {
        private readonly ITenderQueries _tenderQueries;
        private readonly ICommunicationCommands _communicationCommands; 
        private readonly ICommitteeQueries _committeeQueries;
        private readonly ICommunicationAppService _communicationAppService;

        public EnquiryDomainService(ITenderQueries tenderQueries, ICommunicationAppService communicationAppService, ICommitteeQueries committeeQueries, ICommunicationCommands communicationCommands)
        {
            _tenderQueries = tenderQueries;
            _committeeQueries = committeeQueries; 
            _communicationAppService = communicationAppService;
            _communicationCommands = communicationCommands;
        }

        public async Task<AgencyCommunicationRequest> GetEnquiryCommunicationRequestByRequestId(int communcicationRequestId)
        {
            AgencyCommunicationRequest agencyCommunicationRequest = await _communicationAppService.GetCommunicationRequestByRequestId(communcicationRequestId);
            return agencyCommunicationRequest;

        }
        public async Task UpdateCommunicationRequest(AgencyCommunicationRequest agencyCommunicationRequest)
        {
            await _communicationCommands.UpdateAsync(agencyCommunicationRequest);

        }
        public async Task CheckCanAddEnquiry(Tender tender, string cr)
        {
            if (tender.TenderTypeId == (int)Enums.TenderType.PreQualification)
            {
                if (tender.LastEnqueriesDate < DateTime.Now)
                    throw new BusinessRuleException("لا يمكن اضافة استفسار بعد اخر موعد لتقديم الاستفسار");
            }
            else
            {
                if (!await _tenderQueries.SupplierExistsInInvitationsOrConditionsBookletsByTenderIdAndCR(tender.TenderId, cr))
                    throw new BusinessRuleException("Supplier Has No Access To Tender");
            }
        }

        public async Task UserCanAddReplyToEnquiry(int enquiryId, int userId)
        {
            if (enquiryId == 0 && !await _tenderQueries.UserExistsInTechnicalCommitteeUsersByEnquiryIdAndUserId(enquiryId, userId))
                throw new BusinessRuleException("User Can Not Add Reply");
            if (enquiryId != 0 && !await _tenderQueries.UserExistsInCommitteeUsersByEnquiryIdAndUserId(enquiryId, userId))
                throw new BusinessRuleException("User Can Not Add Reply");
        }

        public async Task CheckCanAddCommittee(int committeeId)
        {
            var result = await _committeeQueries.GetCommitteeUsersByCommitteeId(committeeId);
            if (result == null)
                throw new BusinessRuleException("عفوا لا يمكن اضافه لجنه ليس بها مستخدمين");
        }
    }
}
