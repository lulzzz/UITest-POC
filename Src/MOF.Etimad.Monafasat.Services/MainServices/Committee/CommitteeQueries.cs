using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommitteeQueries : Data.GenericRepository.GenericQueryRepository, ICommitteeQueries
    {
        private readonly IAppDbContext _context;
        public CommitteeQueries(IAppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<QueryResult<Committee>> Find(CommitteeSearchCriteria criteria, params Expression<Func<Committee, object>>[] includes)
        {
            var res = await _context.Committees
                .Includes(includes)
                .WhereIf(!string.IsNullOrEmpty(criteria.CommitteeName), x => x.CommitteeName.Contains(criteria.CommitteeName))
                .WhereIf(criteria.CommitteeTypeId != 0, x => x.CommitteeTypeId == criteria.CommitteeTypeId)
                .WhereIf(criteria.AgencyCode != "", x => x.AgencyCode == criteria.AgencyCode)
                .Where(x => x.IsActive.HasValue && x.IsActive.Value)
                .OrderBy(x => x.CommitteeId)
                .SortBy(criteria.Sort, criteria.SortDirection).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return res;
        }

        public async Task<List<Committee>> FindAll(string agencyCode)
        {
            var result = await _context.Committees.Where(c => c.IsActive.HasValue && c.IsActive.Value && agencyCode == c.AgencyCode).ToListAsync();
            return result;
        }
        public async Task<List<Committee>> FindByCommitteeTypeId(int committeeTypeId, string agencyCode)
        {
            var result = await _context.Committees.Where(c => c.CommitteeTypeId == committeeTypeId && c.AgencyCode == agencyCode && c.IsActive != false).ToListAsync();
            return result;
        }

        public async Task<GovAgency> FindAgencyCodeByCommitteeId(int? committeeId = 0)
        {
            var result = await _context.Committees.Where(c => c.CommitteeId == committeeId && c.IsActive != false).Select(b => b.Agency).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<CommitteeModel>> GetBasicCommitteesOnBranch(string agencyCode)
        {
            var committeeList = await _context.Committees
                  .Where(c => c.IsActive == true && agencyCode == c.AgencyCode)
                  .Select(c => new CommitteeModel
                  {
                      AgencyCode = c.AgencyCode,
                      BranchIds = c.BranchCommittees.Where(x => x.IsActive == true).Select(bc => bc.BranchId).ToList(),
                      CommitteeName = c.CommitteeName,
                      CommitteeTypeId = c.CommitteeTypeId,
                      CommitteeId = c.CommitteeId
                  }).ToListAsync();
            return committeeList;
        }

        public async Task<List<CommitteeModel>> GetTechincalAndDirectPurchaseCommittees(string agencyCode)
        {
            var committeeList = await _context.Committees
                  .Where(c => c.IsActive == true && agencyCode == c.AgencyCode && (c.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee || c.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee || c.CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee))
                  .Select(c => new CommitteeModel
                  {
                      AgencyCode = c.AgencyCode,
                      BranchIds = c.BranchCommittees.Where(x => x.IsActive == true).Select(bc => bc.BranchId).ToList(),
                      CommitteeName = c.CommitteeName,
                      CommitteeTypeId = c.CommitteeTypeId,
                      CommitteeId = c.CommitteeId
                  }).ToListAsync();
            return committeeList;
        }

        public async Task<List<CommitteeUserModel>> GetDirectPurchaseCommitteesMembers(string agencyCode, int branchId)
        {
            var committeesId = await _context.BranchCommittees.Where(c => c.BranchId == branchId && c.IsActive == true && c.Committee.AgencyCode == agencyCode && c.Committee != null && c.Committee.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee)
                    .Select(e => e.CommitteeId)
                    .Distinct().ToListAsync();
            var membersList = await _context.CommitteeUsers
                  .Where(c => c.IsActive == true && agencyCode == c.Committee.AgencyCode && (c.Committee.CommitteeTypeId == (int)Enums.CommitteeType.PurchaseCommittee) && (c.UserRoleId == (int)Enums.UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee || c.UserRoleId == (int)Enums.UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee))
                  .WhereIf(committeesId.Any(), (c => committeesId.Contains(c.Committee.CommitteeId)))
                  .Select(c => new CommitteeUserModel
                  {
                      UserId = c.UserProfile.Id,
                      UserName = c.UserProfile.FullName
                  }).Distinct().ToListAsync();
            return membersList;
        }
        public async Task<List<CommitteeModel>> GetVROAndTechnicalCommittees(string agencyCode)
        {
            var committeeList = await _context.Committees
                  .Where(c => c.IsActive == true && agencyCode == c.AgencyCode && (c.CommitteeTypeId == (int)Enums.CommitteeType.VROCommittee || c.CommitteeTypeId == (int)Enums.CommitteeType.TechincalCommittee))
                  .Select(c => new CommitteeModel
                  {
                      AgencyCode = c.AgencyCode,
                      BranchIds = c.BranchCommittees.Where(x => x.IsActive == true).Select(bc => bc.BranchId).ToList(),
                      CommitteeName = c.CommitteeName,
                      CommitteeTypeId = c.CommitteeTypeId,
                      CommitteeId = c.CommitteeId
                  }).ToListAsync();
            return committeeList;
        }
        public async Task<QueryResult<CommitteeUserViewModel>> GetCommitteeUsers(CommitteeUserSearchCriteriaModel criteria)
        {
            var result = await _context.CommitteeUsers.Include(d => d.UserProfile)
                 .Where(c => c.CommitteeId == criteria.CommitteeId && criteria.RoleNames.Any(d => d == c.UserRole.Name) && c.IsActive == true)
                 .Select(x => new CommitteeUserViewModel
                 {
                     Id = Util.Encrypt(x.UserProfileId),
                     UserName = x.UserProfile.FullName,
                     UserId = x.UserProfile.UserName,
                     RoleName = x.UserRole.DisplayNameAr,
                     Email = x.UserProfile.Email
                 }).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<CommitteeUserViewModel>> GetCommitteeUsersByCommitteeId(int committeeId)
        {
            var result = await _context.CommitteeUsers.Include(d => d.UserProfile)
                 .Where(c => c.CommitteeId == committeeId && c.IsActive == true)
                 .Select(x => new CommitteeUserViewModel
                 {
                     Id = Util.Encrypt(x.UserProfileId),
                     UserName = x.UserProfile.FullName,
                     UserId = x.UserProfile.UserName,
                     RoleName = x.UserRole.DisplayNameAr,
                     Email = x.UserProfile.Email
                 }).ToQueryResult();
            return result;
        }

        public async Task<bool> HasAssignedUserByCommittee(int committeeId)
        {
            return await _context.CommitteeUsers.Where(c => c.CommitteeId == committeeId && c.IsActive == true).AnyAsync();
        }

        public async Task<CommitteeUser> GetAssignedUserByIdAndCommittee(int userId, int committeeId)
        {
            var result = await _context.CommitteeUsers.Where(x => x.UserProfileId == userId && x.CommitteeId == committeeId && x.IsActive == true).Include(x => x.UserProfile).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Committee> GetById(int id)
        {
            return await _context.Committees.Include(c => c.CommitteeType).Include(x => x.CommitteeUsers).FirstOrDefaultAsync(a => a.CommitteeId == id);
        }
        public async Task<bool> GetByName(string name, string agencyId, int id = 0)
        {
            return await _context.Committees
                .Where(x => x.CommitteeName == name && x.IsActive == true && x.CommitteeId != id)
                .WhereIf(agencyId != "", x => x.AgencyCode == agencyId).AnyAsync();
        }
        public async Task<bool> GetByNameandTypeId(string name, string agentId, int typeid)
        {
            return await _context.Committees.Where(d => d.CommitteeName == name && d.IsActive.HasValue && d.IsActive.Value && d.CommitteeTypeId == (typeid) && d.AgencyCode == (string.IsNullOrEmpty(agentId) ? d.AgencyCode : agentId)).AnyAsync();
        }
    }
}
