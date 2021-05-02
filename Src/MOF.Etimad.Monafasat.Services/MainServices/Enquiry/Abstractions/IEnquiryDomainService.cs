using MOF.Etimad.Monafasat.Core.Entities;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IEnquiryDomainService
    {
        Task CheckCanAddEnquiry(Tender tender, string cr);
        Task UserCanAddReplyToEnquiry(int enquiryId, int userId);
        Task<AgencyCommunicationRequest> GetEnquiryCommunicationRequestByRequestId(int communcicationRequestId);
        Task UpdateCommunicationRequest(AgencyCommunicationRequest agencyCommunicationRequest);
        Task CheckCanAddCommittee(int committeeId);
    }
}
