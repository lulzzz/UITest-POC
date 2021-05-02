using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class EnquiryQueries : IEnquiryQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EnquiryQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        bool isInRole(string roleName)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(roleName);
        }

        #region Enquiry 

        public async Task<EnquiryModel> FindEnquiryById(int enquiryId, int userId)
        {
            var enquiry = await _context.Enquiries
                .Where(x => x.EnquiryId == enquiryId)
                .Select(x => new EnquiryModel
                {
                    EnquirySendingDate = x.CreatedAt,
                    TenderName = x.Tender.TenderName,
                    EnquiryName = x.EnquiryName,
                    EnquiryId = enquiryId,
                    TenderId = x.TenderId,
                    EnquiryIdString = Util.Encrypt(enquiryId),
                    TenderNumber = x.Tender.TenderNumber,
                    ReferenceNumber = x.Tender.ReferenceNumber,
                    SupplierName = x.Supplier.SelectedCrName,
                    LastEnqueriesDate = x.Tender.LastEnqueriesDate,
                    IsJoinedCommittee = x.Tender.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == userId && c.IsActive == true && x.Tender.TechnicalOrganizationId == _httpContextAccessor.HttpContext.User.SelectedCommittee()) ? false : true,
                    TechnicalCommitteeId = x.Tender.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == userId && x.Tender.TechnicalOrganizationId == _httpContextAccessor.HttpContext.User.SelectedCommittee()) ? x.Tender.TechnicalOrganizationId.Value
                    : x.JoinTechnicalCommittees.Where(t => t.Committee.CommitteeUsers.Any(c => c.UserProfileId == userId) && t.IsActive == true).Select(t => t.CommitteeId).FirstOrDefault(),
                    TechnicalCommitteeName = x.Tender.TechnicalOrganization.CommitteeUsers.Any(c => c.UserProfileId == userId) ? x.Tender.TechnicalOrganization.CommitteeName
                    : x.JoinTechnicalCommittees.Where(t => t.Committee.CommitteeUsers.Any(c => c.UserProfileId == userId) && t.IsActive == true).Select(t => t.Committee.CommitteeName).FirstOrDefault()
                }).FirstOrDefaultAsync();
            return enquiry;


        }

        public async Task<Enquiry> FindEnquiryByEnquiryId(int enquiryId)
        {
            var enquiry = await _context.Enquiries
                .Include(x => x.EnquiryReplies)
                .Include(x => x.JoinTechnicalCommittees)
                .Include(x => x.Tender).ThenInclude(x => x.Agency)
                .Include(x => x.Tender).ThenInclude(x => x.Branch)
                .Where(x => x.EnquiryId == enquiryId).FirstOrDefaultAsync();

            return enquiry;
        }


        public async Task<QueryResult<Enquiry>> GetAllPendingEnquiriesByTenderId(int tenderId)
        {
            var enquiryList = await _context.Enquiries
                .Where(x => x.TenderId == tenderId && x.IsActive == true)
                .ToQueryResult(0, 10);
            return enquiryList;
        }
        #endregion

        #region Enquiry Replies

        public async Task<EnquiryReplyModel> FindEnquiryReplyById(int enquiryReplyId)
        {
            var enquiryReply = await _context.EnquiryReplies
                .Where(x => x.EnquiryReplyId == enquiryReplyId)
                .Select(x => new EnquiryReplyModel
                {
                    EnquiryReplyId = x.EnquiryReplyId,
                    EnquiryReplyMessage = x.EnquiryReplyMessage,
                    EnquiryReplyStatusId = x.EnquiryReplyStatusId,
                    IsComment = (bool)x.IsComment,
                    CommitteeId = (int)x.CommitteeId
                }).FirstOrDefaultAsync();
            return enquiryReply;
        }
        public async Task<EnquiryReply> GetEnquiryReplyWithTender(int enquiryReplyId)
        {
            EnquiryReply enquiryReply = await _context.EnquiryReplies
                .Include(x => x.Enquiry)
                .ThenInclude(x => x.Tender)
                .Where(x => x.EnquiryReplyId == enquiryReplyId).FirstOrDefaultAsync();
            return enquiryReply;
        }
        public async Task<EnquiryReply> GetEnquiryReplyWithCommunicationRequest(int enquiryReplyId)
        {
            EnquiryReply enquiryReply = await _context.EnquiryReplies
                .Include(x => x.Enquiry)
                .ThenInclude(x => x.AgencyCommunicationRequest)
                .Where(x => x.EnquiryReplyId == enquiryReplyId).FirstOrDefaultAsync();
            return enquiryReply;
        }
        public async Task<EnquiryReply> GetEnquiryReplyByReplyId(int enquiryReplyId)
        {
            EnquiryReply enquiryReply = await _context.EnquiryReplies
                .Where(x => x.EnquiryReplyId == enquiryReplyId).FirstOrDefaultAsync();
            return enquiryReply;
        }



        public async Task<bool> GetHasEnquiryRepliesByCommittee(int committeeId)
        {
            return await _context.EnquiryReplies
                .Where(x => x.CommitteeId == committeeId)
                .AnyAsync();
        }

        public async Task<QueryResult<EnquiryReplyModel>> GetAllEnquiryRepliesByEnquiryId(SimpleSearchCretriaModel criteria)
        {
            criteria.EnquiryId = Util.Decrypt(criteria.EnquiryIdString);
            var enquiryReplyList = await _context.EnquiryReplies
                 .Where(t => t.EnquiryId == criteria.EnquiryId && t.IsActive == true)
                                  .Select(t => new EnquiryReplyModel
                                  {
                                      EnquiryReplyStatusId = t.EnquiryReplyStatusId,
                                      EnquiryReplyMessage = t.EnquiryReplyMessage,
                                      EnquiryReplyIdString = Util.Encrypt(t.EnquiryReplyId),
                                      EnquiryReplyDate = t.UpdatedAt,
                                      CreationDate = t.CreatedAt,
                                      IsComment = (bool)t.IsComment,
                                      CommitteeId = (int)t.CommitteeId,
                                      CommitteeName = t.Committee.CommitteeName
                                  }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return enquiryReplyList;
        }


        public async Task<List<Enquiry>> GetAllEnquiryRepliesByTenderId(int tenderId, int userId)
        {
            var tender = await _context.Tenders.Where(x => x.TenderId == tenderId).FirstOrDefaultAsync();
            var TechnicalCommitteIdForTender = tender.TechnicalOrganizationId != null ? tender.TechnicalOrganizationId : null;
            var TechnicalCommitteIdsForUser = (await _context.CommitteeUsers.Where(x => x.UserProfileId == userId).Select(x => x.CommitteeId).ToListAsync());

            var enquiryReplyList = await _context.Enquiries
                .Include(x => x.Tender)
                .ThenInclude(c => c.TechnicalOrganization)
                .ThenInclude(c => c.CommitteeUsers)
                .Include(x => x.JoinTechnicalCommittees)
                .ThenInclude(x => x.Committee.CommitteeUsers)
                .Include(x => x.Supplier)
                .Include(x => x.EnquiryReplies)
                .ThenInclude(x => x.Committee.CommitteeUsers)
                .Where(x => x.TenderId == tenderId && x.IsActive == true && x.Tender.TechnicalOrganizationId != null)
                .WhereIf(isInRole(RoleNames.TechnicalCommitteeUser) && TechnicalCommitteIdForTender != null && !TechnicalCommitteIdsForUser.Contains(TechnicalCommitteIdForTender.Value), x => x.JoinTechnicalCommittees.Any(y => TechnicalCommitteIdsForUser.Contains(y.CommitteeId) && y.IsActive == true))
                .OrderBy(x => x.EnquiryReplies.Count)
                .ThenByDescending(x => x.CreatedAt).ToListAsync();

            return enquiryReplyList;
        }


        public async Task<QueryResult<Enquiry>> GetAllEnquiryRepliesByTenderId(SimpleSearchCretriaModel search)
        {
            var TechnicalCommitteIdForTender = (await _context.Tenders.Where(x => x.TenderId == search.TenderId).FirstOrDefaultAsync()).TechnicalOrganizationId.Value;
            var enquiryReplyList = await _context.Enquiries
                .Include(x => x.Tender)
                .ThenInclude(c => c.TechnicalOrganization)
                .ThenInclude(c => c.CommitteeUsers)
                .Include(x => x.JoinTechnicalCommittees)
                .ThenInclude(x => x.Committee.CommitteeUsers)
                .Include(x => x.Supplier)
                .Include(x => x.EnquiryReplies)
                .ThenInclude(x => x.Committee.CommitteeUsers)
                 .Where(x => x.TenderId == search.TenderId && x.IsActive == true && x.Tender.TechnicalOrganizationId != null)
                 .WhereIf(isInRole(RoleNames.TechnicalCommitteeUser) && _httpContextAccessor.HttpContext.User.SelectedCommittee() != TechnicalCommitteIdForTender, x => x.JoinTechnicalCommittees.Any(y => y.CommitteeId == _httpContextAccessor.HttpContext.User.SelectedCommittee() && y.IsActive == true))
                 .OrderBy(x => x.EnquiryReplies.Count)
                 .ThenByDescending(x => x.CreatedAt)
                 .ToQueryResult(search.PageNumber, search.PageSize);
            return enquiryReplyList;
        }

        #endregion

        #region Join Technical Committee


        public async Task<bool> GetJoinCommitteeByEnquiryIdAndCommitteeId(int enquiryId, int joinedCommitteeId)
        {
            bool joinTechnicalCommittee = await _context.JoinTechnicalCommittees
                .AnyAsync(x => x.EnquiryId == enquiryId && x.CommitteeId == joinedCommitteeId && x.IsActive == true);
            return joinTechnicalCommittee;
        }

        public async Task<JoinTechnicalCommitteeModel> GetJoinCommitteeByEnquiryId(int enquiryId)
        {

            var joinTechnicalCommittee = await _context.JoinTechnicalCommittees
                .Where(x => x.EnquiryId == enquiryId && x.IsActive == true)
                .Select(x => new JoinTechnicalCommitteeModel
                {
                    CommitteeName = x.Committee.CommitteeName,
                    EnquiryIdString = Util.Encrypt(x.EnquiryId),
                    EnquirySendingDate = x.Enquiry.CreatedAt,
                    EnquiryName = x.Enquiry.EnquiryName
                }).FirstOrDefaultAsync();
            return joinTechnicalCommittee;
        }

        public async Task<QueryResult<JoinTechnicalCommitteeModel>> GetInvitedCommitesByEnquiryId(SimpleSearchCretriaModel search)
        {
            var enquiryInternalComments = await _context.JoinTechnicalCommittees
                 .Where(c => c.EnquiryId == search.EnquiryId && c.IsActive == true)
                 .Select(x => new JoinTechnicalCommitteeModel
                 {
                     JoinTechnicalCommitteeId = x.JoinTechnicalCommitteeId,
                     CommitteeName = x.Committee.CommitteeName,
                     InternelComments = x.Enquiry.EnquiryReplies.Where(r => r.EnquiryId == search.EnquiryId && x.IsActive == true && r.IsActive == true && r.IsComment == true)
                     .Select(r => new CommentModel
                     {
                         InternelComment = r.EnquiryReplyMessage
                     }).ToList(),//r => r.EnquiryReplyMessage);.ToList(),
                     //EnquiryComment = x.Enquiry.EnquiryReplies.FirstOrDefault(r => r.EnquiryId == search.EnquiryId && r.IsActive == true && r.IsComment == true).EnquiryReplyMessage,
                     ReplyId = x.Enquiry.EnquiryReplies.FirstOrDefault(r => r.EnquiryId == search.EnquiryId && r.IsActive == true && r.IsComment == true).EnquiryReplyId
                 })
                  .ToQueryResult(search.PageNumber, search.PageSize);
            return enquiryInternalComments;
        }


        #endregion


    }

}
