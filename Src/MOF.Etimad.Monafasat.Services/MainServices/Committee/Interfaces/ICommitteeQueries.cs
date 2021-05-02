using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface ICommitteeQueries
    {
        Task<List<CommitteeModel>> GetBasicCommitteesOnBranch(string agencyCode);
        Task<List<CommitteeModel>> GetTechincalAndDirectPurchaseCommittees(string agencyCode);
        Task<List<CommitteeUserModel>> GetDirectPurchaseCommitteesMembers(string agencyCode, int branchId);
        Task<List<CommitteeModel>> GetVROAndTechnicalCommittees(string agencyCode);
        Task<QueryResult<Committee>> Find(CommitteeSearchCriteria criteria, params Expression<Func<Committee, object>>[] includes);
        Task<List<Committee>> FindAll(string agencyCode);
        Task<Committee> GetById(int id);
        Task<bool> GetByName(string name, string agencyId, int id = 0);
        Task<bool> GetByNameandTypeId(string name, string agentId, int typeid);
        Task<List<Committee>> FindByCommitteeTypeId(int committeeTypeId, string agencyCode);
        Task<GovAgency> FindAgencyCodeByCommitteeId(int? committeeId = 0);
        Task<QueryResult<CommitteeUserViewModel>> GetCommitteeUsers(CommitteeUserSearchCriteriaModel criteria);
        Task<bool> HasAssignedUserByCommittee(int committeeId);
        Task<CommitteeUser> GetAssignedUserByIdAndCommittee(int userId, int committeeId);
        Task<QueryResult<CommitteeUserViewModel>> GetCommitteeUsersByCommitteeId(int committeeId);
    }
}
