using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IEnquiryQueries
    {
        Task<bool> GetHasEnquiryRepliesByCommittee(int committeeId);
        Task<QueryResult<Enquiry>> GetAllPendingEnquiriesByTenderId(int tenderId);
        Task<EnquiryModel> FindEnquiryById(int enquiryId, int userId);
        Task<Enquiry> FindEnquiryByEnquiryId(int enquiryId);
        Task<EnquiryReplyModel> FindEnquiryReplyById(int enquiryReplyId);
        Task<EnquiryReply> GetEnquiryReplyWithCommunicationRequest(int enquiryReplyId);
        Task<EnquiryReply> GetEnquiryReplyWithTender(int enquiryReplyId);
        Task<EnquiryReply> GetEnquiryReplyByReplyId(int enquiryReplyId);
        Task<QueryResult<EnquiryReplyModel>> GetAllEnquiryRepliesByEnquiryId(SimpleSearchCretriaModel criteria);
        Task<List<Enquiry>> GetAllEnquiryRepliesByTenderId(int tenderId, int userId);
        Task<QueryResult<Enquiry>> GetAllEnquiryRepliesByTenderId(SimpleSearchCretriaModel search);
        Task<bool> GetJoinCommitteeByEnquiryIdAndCommitteeId(int enquiryId, int joinedCommitteeId);
        Task<JoinTechnicalCommitteeModel> GetJoinCommitteeByEnquiryId(int enquiryId);
        Task<QueryResult<JoinTechnicalCommitteeModel>> GetInvitedCommitesByEnquiryId(SimpleSearchCretriaModel search);

    }
}
