using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IEnquiryAppService
    {
        Task<Enquiry> SendEnquiry(EnquiryModel enquiryModel);
        Task<EnquiryReply> AddEnquiryReply(EnquiryReplyModel enquiryReplyModel);
        Task AddEnquiryComment(EnquiryReplyModel enquiryReplyModel);
        Task<EnquiryReply> EditEnquiryReply(EnquiryReplyModel enquiryReplyModel);
        Task<EnquiryModel> GetEnquiryById(int enquiryId, int userId);
        Task<EnquiryReplyModel> GetEnquiryReplyById(int enquiryReplyId);
        Task<QueryResult<Enquiry>> GetAllPendingEnquiriesByTenderId(int tenderId);
        Task<QueryResult<EnquiryReplyModel>> GetAllEnquiryRepliesByEnquiryId(SimpleSearchCretriaModel criteria);
        Task<List<Enquiry>> GetAllEnquiryRepliesByTenderId(int tenderId, int userId);
        Task<QueryResult<Enquiry>> GetAllEnquiryRepliesByTenderId(SimpleSearchCretriaModel criteria);
        Task<EnquiryReply> DeleteReply(int enquiryReplyId);
        Task<EnquiryReply> ApproveEnquiryReply(int enquiryReplyId);
        Task<JoinTechnicalCommitteeModel> SendInvitationsToJoinCommittee(JoinTechnicalCommitteeModel joinModel);
        Task<JoinTechnicalCommitteeModel> GetJoinTechnicalCommitteeByEnquiryId(int enquiryId);
        Task RemoveInvitedCommittee(int joinTechnicalCommitteeId, int replyId, int enquiryId);
        Task<QueryResult<JoinTechnicalCommitteeModel>> GetInvitedCommitesByEnquiryId(SimpleSearchCretriaModel criteria);

    }
}
